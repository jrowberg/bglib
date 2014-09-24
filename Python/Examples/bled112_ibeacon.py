#!/usr/bin/env python

""" Barebones BGAPI iBeacon script for Bluegiga BLE modules

This script is designed to be used with a BGAPI-enabled Bluetooth Smart device
from Bluegiga, probably a BLED112 but anything that is connected to a serial
port (real or virtual) and can "speak" the BGAPI protocol. It is tuned for
usable defaults when used on a Raspberry Pi, but can easily be used on other
platforms, including Windows or OS X.

Note that the command functions do *not* incorporate the extra preceding length
byte required when using "packet" mode (only available on USART peripheral ports
on the BLE112/BLE113 module, not applicable to the BLED112). It is built so you
can simply plug in a BLED112 and go, but other kinds of usage may require small
modifications.

Seriously, just download, plug in the dongle, and run. Voila!

Changelog:
    2014-09-24 - Initial release

"""

__author__ = "Jeff Rowberg"
__license__ = "MIT"
__version__ = "2014-09-24"
__email__ = "jeff@rowberg.net"

import sys, optparse, serial, struct, time, datetime, re, signal

options = []

def main():
    global options

    class IndentedHelpFormatterWithNL(optparse.IndentedHelpFormatter):
      def format_description(self, description):
        if not description: return ""
        desc_width = self.width - self.current_indent
        indent = " "*self.current_indent
        bits = description.split('\n')
        formatted_bits = [
          optparse.textwrap.fill(bit,
            desc_width,
            initial_indent=indent,
            subsequent_indent=indent)
          for bit in bits]
        result = "\n".join(formatted_bits) + "\n"
        return result

      def format_option(self, option):
        result = []
        opts = self.option_strings[option]
        opt_width = self.help_position - self.current_indent - 2
        if len(opts) > opt_width:
          opts = "%*s%s\n" % (self.current_indent, "", opts)
          indent_first = self.help_position
        else: # start help on same line as opts
          opts = "%*s%-*s  " % (self.current_indent, "", opt_width, opts)
          indent_first = 0
        result.append(opts)
        if option.help:
          help_text = self.expand_default(option)
          help_lines = []
          for para in help_text.split("\n"):
            help_lines.extend(optparse.textwrap.wrap(para, self.help_width))
          result.append("%*s%s\n" % (
            indent_first, "", help_lines[0]))
          result.extend(["%*s%s\n" % (self.help_position, "", line)
            for line in help_lines[1:]])
        elif opts[-1] != "\n":
          result.append("\n")
        return "".join(result)

    class MyParser(optparse.OptionParser):
        def format_epilog(self, formatter=None):
            return self.epilog

        def format_option_help(self, formatter=None):
            formatter = IndentedHelpFormatterWithNL()
            if formatter is None:
                formatter = self.formatter
            formatter.store_option_strings(self)
            result = []
            result.append(formatter.format_heading(optparse._("Options")))
            formatter.indent()
            if self.option_list:
                result.append(optparse.OptionContainer.format_option_help(self, formatter))
                result.append("\n")
            for group in self.option_groups:
                result.append(group.format_help(formatter))
                result.append("\n")
            formatter.dedent()
            # Drop the last "\n", or the header if no options or option groups:
            return "".join(result[:-1])

    # process script arguments
    p = MyParser(description='Bluetooth Smart iBeacon script for Bluegiga BLED112 v2014-09-24', epilog=
"""
Examples:

    bled112_ibeacon.py

\tDefault options (AirLocate UUID, 1/1 major/minor, 100ms interval)

    bled112_ibeacon.py -p /dev/ttyUSB0 -s -i 1000

\tUse ttyUSB0, display scan requests, 1 second interval

    bled112_ibeacon.py -u f1b41cde-dbf5-4acf-8679-ecb8b4dca700 -j 5 -n 2

\tUse custom UUID, major value of 5, minor value of 2

Scan request output format:

    <timestamp> <rssi> <address> <address_type>
    
Scan request output example with public address 00:07:80:81:44:94 and RSSI -57:

    1364699494.574 -57 000780814494 0

"""
    )

    # set all defaults for options
    p.set_defaults(port="/dev/ttyACM0", baud=115200, interval=100, uuid="", major="0001", minor="0001", quiet=False, scanreq=False)

    # create serial port options argument group
    group = optparse.OptionGroup(p, "Serial Port Options")
    group.add_option('--port', '-p', type="string", help="Serial port device name (default /dev/ttyACM0)", metavar="PORT")
    group.add_option('--baud', '-b', type="int", help="Serial port baud rate (default 115200)", metavar="BAUD")
    p.add_option_group(group)

    # create iBeacon options argument group
    group = optparse.OptionGroup(p, "iBeacon Options")
    group.add_option('--uuid', '-u', type="string", help="iBeacon UUID (default AirLocate)", metavar="UUID")
    group.add_option('--major', '-j', type="string", help="iBeacon Major (default 0001)", metavar="MAJOR")
    group.add_option('--minor', '-n', type="string", help="iBeacon Minor (default 0001)", metavar="MINOR")
    group.add_option('--interval', '-i', type="int", help="Advertisement interval in ms (default 100, min 30, max 10230)", metavar="INTERVAL")
    group.add_option('--end', '-e', action="store_true", help="End beaconing advertisements", metavar="STOP")
    p.add_option_group(group)

    # create output options argument group
    group = optparse.OptionGroup(p, "Output Options")
    group.add_option('--scanreq', '-s', action="store_true", help="Display scan requests (Bluegiga enhanced broadcasting)", metavar="SCANREQ")
    group.add_option('--quiet', '-q', action="store_true", help="Quiet mode (suppress initial parameter display)")
    p.add_option_group(group)
    
    uuid = [ 0xe2, 0xc5, 0x6d, 0xb5, 0xdf, 0xfb, 0x48, 0xd2, 0xb0, 0x60, 0xd0, 0xf5, 0xa7, 0x10, 0x96, 0xe0 ]
    major = 0x0001
    minor = 0x0001
    adv_min = 90
    adv_max = 110

    # actually parse all of the arguments
    options, arguments = p.parse_args()

    # validate UUID if specified
    if len(options.uuid):
        if re.search('[^a-fA-F0-9:\\-]', options.uuid):
            p.print_help()
            print "\n================================================================"
            print "Invalid UUID characters, must be 16 bytes in 0-padded hex form:"
            print "\t-u 0123456789abcdef0123456789abcdef"
            print "================================================================"
            exit(1)
        arg2 = options.uuid.replace(":", "").replace("-", "").upper()
        if len(arg2) != 32:
            p.print_help()
            print "\n================================================================"
            print "Invalid UUID length, must be 16 bytes in 0-padded hex form:"
            print "\t-u 0123456789abcdef0123456789abcdef"
            print "================================================================"
            exit(1)
        uuid = []
        for i in range(0, len(arg2), 2):
            uuid.append(int(arg2[i : i + 2], 16))

    # validate major value if specified
    if len(options.major):
        if re.search('[^a-fA-F0-9:\\-]', options.major):
            p.print_help()
            print "\n================================================================"
            print "Invalid major characters, must be 2 bytes in 0-padded hex form:"
            print "\t-j 01cf"
            print "================================================================"
            exit(1)
        arg2 = options.major.replace(":", "").replace("-", "").upper()
        if len(arg2) != 4:
            p.print_help()
            print "\n================================================================"
            print "Invalid major length, must be 2 bytes in 0-padded hex form:"
            print "\t-j 01cf"
            print "================================================================"
            exit(1)
        major = int(arg2[0:4], 16)
            
    # validate minor value if specified
    if len(options.minor):
        if re.search('[^a-fA-F0-9:\\-]', options.minor):
            p.print_help()
            print "\n================================================================"
            print "Invalid minor characters, must be 2 bytes in 0-padded hex form:"
            print "\t-n 01cf"
            print "================================================================"
            exit(1)
        arg2 = options.minor.replace(":", "").replace("-", "").upper()
        if len(arg2) != 4:
            p.print_help()
            print "\n================================================================"
            print "Invalid minor length, must be 2 bytes in 0-padded hex form:"
            print "\t-n 01cf"
            print "================================================================"
            exit(1)
        minor = int(arg2[0:4], 16)
        
    # validate interval
    if options.interval < 30 or options.interval > 10230:
        p.print_help()
        print "\n================================================================"
        print "Invalid advertisement interval, must be between 30 and 10230"
        print "================================================================"
        exit(1)
    else:
        adv_min = options.interval - 10
        adv_max = adv_min + 20
            
    # display  parameter summary, if not in quiet mode
    if not(options.quiet):
        print "================================================================"
        print "BLED112 iBeacon for Python v%s" % __version__
        print "================================================================"
        print "Serial port:\t%s" % options.port
        print "Baud rate:\t%s" % options.baud
        print "Beacon UUID:\t%s" % ''.join(['%02X' % b for b in uuid])
        print "Beacon Major:\t%04X" % major
        print "Beacon Minor:\t%04X" % minor
        print "Adv. interval:\t%d ms" % options.interval
        print "Scan requests:\t%s" % ['Disabled', 'Enabled'][options.scanreq]
        print "----------------------------------------------------------------"

    # open serial port for BGAPI access
    try:
        ser = serial.Serial(port=options.port, baudrate=options.baud, timeout=1)
    except serial.SerialException as e:
        print "\n================================================================"
        print "Port error (name='%s', baud='%ld'): %s" % (options.port, options.baud, e)
        print "================================================================"
        exit(2)

    # flush buffers
    #print "Flushing serial I/O buffers..."
    ser.flushInput()
    ser.flushOutput()

    # disconnect if we are connected already
    #print "Disconnecting if connected..."
    ble_cmd_connection_disconnect(ser, 0)
    response = ser.read(7) # 7-byte response
    #for b in response: print '%02X' % ord(b),

    # stop advertising if we are advertising already
    #print "Exiting advertising mode if advertising..."
    ble_cmd_gap_set_mode(ser, 0, 0)
    response = ser.read(6) # 6-byte response
    #for b in response: print '%02X' % ord(b),

    # stop scanning if we are scanning already
    #print "Exiting scanning mode if scanning..."
    ble_cmd_gap_end_procedure(ser)
    response = ser.read(6) # 6-byte response
    #for b in response: print '%02X' % ord(b),
    
    if options.end:
        if not options.quiet:
            print "iBeacon advertisements ended"
        exit(0)

    # build main ad packet
    ibeacon_adv = [ 0x02, 0x01, 0x06, 0x1a, 0xff, 0x4c, 0x00, 0x02, 0x15,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    major & 0xFF, major >> 8,
                    minor & 0xFF, minor >> 8,
                    0xC6 ]
                    
    # set UUID specifically
    ibeacon_adv[9:25] = uuid[0:16]

    # set advertisement (min/max interval + all three ad channels)
    ble_cmd_gap_set_adv_parameters(ser, int(adv_min * 0.625), int(adv_max * 0.625), 7)
    response = ser.read(6) # 6-byte response
    #for b in response: print '%02X' % ord(b),
    
    # set beacon data (advertisement packet)
    ble_cmd_gap_set_adv_data(ser, 0, ibeacon_adv)
    response = ser.read(6) # 6-byte response
    #for b in response: print '%02X' % ord(b),
    
    # set local name (scan response packet)
    ble_cmd_gap_set_adv_data(ser, 1, [ 0x09, 0x09, 0x50, 0x69, 0x42, 0x65, 0x61, 0x63, 0x6f, 0x6e ])
    response = ser.read(6) # 6-byte response
    #for b in response: print '%02X' % ord(b),

    # start advertising as non-connectable with userdata and enhanced broadcasting
    #print "Entering advertisement mode..."
    ble_cmd_gap_set_mode(ser, 0x84, 0x03)
    response = ser.read(6) # 6-byte response
    #for b in response: print '%02X' % ord(b),

    if not options.quiet:
        print "iBeacon advertisements started"

    if options.scanreq:
        while (1):
            # catch all incoming data
            while (ser.inWaiting()): bgapi_parse(ord(ser.read()));

            # don't burden the CPU
            time.sleep(0.01)

# define API commands we might use for this script
def ble_cmd_system_reset(p, boot_in_dfu):
    p.write(struct.pack('5B', 0, 1, 0, 0, boot_in_dfu))
def ble_cmd_connection_disconnect(p, connection):
    p.write(struct.pack('5B', 0, 1, 3, 0, connection))
def ble_cmd_gap_set_mode(p, discover, connect):
    p.write(struct.pack('6B', 0, 2, 6, 1, discover, connect))
def ble_cmd_gap_end_procedure(p):
    p.write(struct.pack('4B', 0, 0, 6, 4))
def ble_cmd_gap_set_adv_parameters(p, adv_interval_min, adv_interval_max, adv_channels):
    p.write(struct.pack('<4BHHB', 0, 5, 6, 8, adv_interval_min, adv_interval_max, adv_channels))
def ble_cmd_gap_set_adv_data(p, set_scanrsp, adv_data):
    p.write(struct.pack('<4BBB' + str(len(adv_data)) + 's', 0, 2 + len(adv_data), 6, 9, set_scanrsp, len(adv_data), b''.join(chr(i) for i in adv_data)))

# define basic BGAPI parser
bgapi_rx_buffer = []
bgapi_rx_expected_length = 0
def bgapi_parse(b):
    global bgapi_rx_buffer, bgapi_rx_expected_length, options
    if len(bgapi_rx_buffer) == 0 and (b == 0x00 or b == 0x80):
        bgapi_rx_buffer.append(b)
    elif len(bgapi_rx_buffer) == 1:
        bgapi_rx_buffer.append(b)
        bgapi_rx_expected_length = 4 + (bgapi_rx_buffer[0] & 0x07) + bgapi_rx_buffer[1]
    elif len(bgapi_rx_buffer) > 1:
        bgapi_rx_buffer.append(b)

    #print '%02X: %d, %d' % (b, len(bgapi_rx_buffer), bgapi_rx_expected_length)
    if bgapi_rx_expected_length > 0 and len(bgapi_rx_buffer) == bgapi_rx_expected_length:
        #print '<=[ ' + ' '.join(['%02X' % b for b in bgapi_rx_buffer ]) + ' ]'
        packet_type, payload_length, packet_class, packet_command = bgapi_rx_buffer[:4]
        bgapi_rx_payload = b''.join(chr(i) for i in bgapi_rx_buffer[4:])
        if packet_type & 0x80 == 0x00: # response
            bgapi_filler = 0
        else: # event
            if packet_class == 0x06: # gap
                if packet_command == 0x00: # scan_response
                    rssi, packet_type, sender, address_type, bond, data_len = struct.unpack('<bB6sBBB', bgapi_rx_payload[:11])
                    sender = [ord(b) for b in sender]
                    data_data = [ord(b) for b in bgapi_rx_payload[11:]]
                    display = 1

                    # parse all ad fields from ad packet
                    ad_fields = []
                    this_field = []
                    ad_flags = 0
                    ad_services = []
                    ad_local_name = []
                    ad_tx_power_level = 0
                    ad_manufacturer = []

                    bytes_left = 0
                    for b in data_data:
                        if bytes_left == 0:
                            bytes_left = b
                            this_field = []
                        else:
                            this_field.append(b)
                            bytes_left = bytes_left - 1
                            if bytes_left == 0:
                                ad_fields.append(this_field)
                                if this_field[0] == 0x01: # flags
                                    ad_flags = this_field[1]
                                if this_field[0] == 0x02 or this_field[0] == 0x03: # partial or complete list of 16-bit UUIDs
                                    for i in xrange((len(this_field) - 1) / 2):
                                        ad_services.append(this_field[-1 - i*2 : -3 - i*2 : -1])
                                if this_field[0] == 0x04 or this_field[0] == 0x05: # partial or complete list of 32-bit UUIDs
                                    for i in xrange((len(this_field) - 1) / 4):
                                        ad_services.append(this_field[-1 - i*4 : -5 - i*4 : -1])
                                if this_field[0] == 0x06 or this_field[0] == 0x07: # partial or complete list of 128-bit UUIDs
                                    for i in xrange((len(this_field) - 1) / 16):
                                        ad_services.append(this_field[-1 - i*16 : -17 - i*16 : -1])
                                if this_field[0] == 0x08 or this_field[0] == 0x09: # shortened or complete local name
                                    ad_local_name = this_field[1:]
                                if this_field[0] == 0x0A: # TX power level
                                    ad_tx_power_level = this_field[1]

                                # OTHER AD PACKET TYPES NOT HANDLED YET

                                if this_field[0] == 0xFF: # manufactuerer specific data
                                    ad_manufacturer.append(this_field[1:])

                    #print "gap_scan_response: rssi: %d, packet_type: %d, sender: %s, address_type: %d, bond: %d, data_len: %d" % \
                    #    (rssi, packet_type, ':'.join(['%02X' % ord(b) for b in sender[::-1]]), address_type, bond, data_len)
                    t = datetime.datetime.now()

                    disp_list = []
                    disp_list.append("%ld.%03ld" % (time.mktime(t.timetuple()), t.microsecond/1000))
                    disp_list.append("%d" % rssi)
                    disp_list.append("%s" % ''.join(['%02X' % b for b in sender[::-1]]))
                    disp_list.append("%d" % address_type)

                    print ' '.join(disp_list)

        bgapi_rx_buffer = []

# gracefully exit without a big exception message if possible
def ctrl_c_handler(signal, frame):
    #print 'Goodbye, cruel world!'
    exit(0)

signal.signal(signal.SIGINT, ctrl_c_handler)

if __name__ == '__main__':
    main()