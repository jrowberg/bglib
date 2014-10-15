#!/usr/bin/env python

""" Bluegiga BGAPI/BGLib demo: health thermometer collector

Changelog:
    2014-10-15 - Add logfile output mode
    2014-07-05 - Fix indication subscription to use 2-byte value
    2013-06-07 - Fix "address_type" to support Random (e.g. iPhone as peripheral)
    2013-05-15 - Added comments, script arguments
    2013-04-28 - Initial release

============================================
Bluegiga BGLib Python interface library test health thermometer collector app
2013-07-05 by Jeff Rowberg <jeff@rowberg.net>
Updates should (hopefully) always be available at https://github.com/jrowberg/bglib

============================================
BGLib Python interface library code is placed under the MIT license
Copyright (c) 2014 Jeff Rowberg

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
===============================================

"""

__author__ = "Jeff Rowberg"
__license__ = "MIT"
__version__ = "2014-10-15"
__email__ = "jeff@rowberg.net"

"""
BASIC ARCHITECTURAL OVERVIEW:
    The program starts, initializes the dongle to a known state, then starts
    scanning. Each time an advertisement packet is found, a scan response
    event packet is generated. These packets are read by polling the serial
    port to which the BLE(D)11x is attached.

    The basic process is as follows:
      a. Scan for devices
      b. If the desired UUID is found in an ad packet, connect to that device
      c. Search for all "service" descriptors to find the target service handle range
      d. Search through the target service to find the thermometer measurement attribute handle
      e. Enable notifications on the thermometer measurement attribute
      f. Read and display incoming thermometer values until terminated (Ctrl+C)

FUNCTION ANALYSIS:

1. __main__:
    Initializes the serial port and BGLib object to attach event handlers,
    then sends commands to cause the device to disconnect, stop advertising,
    and stop scanning (i.e. return to a known idle/standby state). Some of
    these commands will fail since the device cannot be doing all of these
    things at the same time, but this is not a problem. __main__ finishes
    by setting scan parameters and initiating a scan with the "gap_discover"
    command.

2. my_ble_evt_gap_scan_response:
    Raised during scanning whenever an advertisement packet is detected. The
    data provided includes the MAC address, RSSI, and ad packet data payload.
    This payload includes fields which contain any services being advertised,
    which allows us to scan for a specific service. In this demo, the service
    we are searching for has a standard 16-bit UUID which is contained in the
    "uuid_htm_service" variable. Once a match is found, the script initiates
    a connection request with the "gap_connect_direct" command.

3. my_ble_evt_connection_status
    Raised when the connection status is updated. This happens when the
    connection is first established, and the "flags" byte will contain 0x05 in
    this instance. However, it will also happen if the connected devices bond
    (i.e. pair), or if encryption is enabled (e.g. with "sm_encrypt_start").
    Once a connection is established, the script begins a service discovery
    with the "attclient_read_by_group_type" command.

4. my_ble_evt_attclient_group_found
    Raised for each group found during the search started in #3. If the right
    service is found (matched by UUID), then its start/end handle values are
    stored for usage later. We cannot use them immediately because the ongoing
    read-by-group-type procedure must finish first.

5. my_ble_evt_attclient_find_information_found
    Raised for each attribute found during the search started after the service
    search completes. We look for two specific attributes during this process;
    the first is the unique health thermometer measurement attribute which has
    a standard 16-bit UUID (contained in the "uuid_htm_measurement_characteristic"
    variable), and the second is the corresponding "client characteristic
    configuration" attribute with a UUID of 0x2902. The correct attribute here
    will always be the first 0x2902 attribute after the measurement attribute
    in question. Typically the CCC handle value will be either +1 or +2 from
    the original handle.

6. my_ble_evt_attclient_procedure_completed
    Raised when an attribute client procedure finishes, which in this script
    means when the "attclient_read_by_group_type" (service search) or the
    "attclient_find_information" (descriptor search) completes. Since both
    processes terminate with this same event, we must keep track of the state
    so we know which one has actually just finished. The completion of the
    service search will (assuming the service is found) trigger the start of
    the descriptor search, and the completion of the descriptor search will
    (assuming the attributes are found) trigger enabling indications on the
    measurement characteristic.

7. my_ble_evt_attclient_attribute_value
    Raised each time the remote device pushes new data via notifications or
    indications. (Notifications and indications are basically the same, except
    that indications are acknowledged while notifications are not--like TCP vs.
    UDP.) In this script, the remote slave device pushes temperature
    measurements out as indications approximately once per second. These values
    are displayed to the console.

"""

import bglib, serial, time, datetime, optparse, signal

ble = 0
ser = 0
peripheral_list = []
connection_mac = []
connection_handle = 0
att_handle_start = 0
att_handle_end = 0
att_handle_measurement = 0
att_handle_measurement_ccc = 0

logmode = False

uuid_service = [0x28, 0x00] # 0x2800
uuid_client_characteristic_configuration = [0x29, 0x02] # 0x2902

uuid_htm_service = [0x18, 0x09] # 0x1809
uuid_htm_characteristic = [0x2a, 0x1c] # 0x2A1C

STATE_STANDBY = 0
STATE_CONNECTING = 1
STATE_FINDING_SERVICES = 2
STATE_FINDING_ATTRIBUTES = 3
STATE_LISTENING_MEASUREMENTS = 4
state = STATE_STANDBY

# handler to notify of an API parser timeout condition
def my_timeout(sender, args):
    # might want to try the following lines to reset, though it probably
    # wouldn't work at this point if it's already timed out:
    #ble.send_command(ser, ble.ble_cmd_system_reset(0))
    #ble.check_activity(ser, 1)
    if not logmode:
        print "BGAPI parser timed out. Make sure the BLE device is in a known/idle state."

# gap_scan_response handler
def my_ble_evt_gap_scan_response(sender, args):
    global state, ble, ser, uuid_htm_service

    # pull all advertised service info from ad packet
    ad_services = []
    this_field = []
    bytes_left = 0
    for b in args['data']:
        if bytes_left == 0:
            bytes_left = b
            this_field = []
        else:
            this_field.append(b)
            bytes_left = bytes_left - 1
            if bytes_left == 0:
                if this_field[0] == 0x02 or this_field[0] == 0x03: # partial or complete list of 16-bit UUIDs
                    for i in xrange((len(this_field) - 1) / 2):
                        ad_services.append(this_field[-1 - i*2 : -3 - i*2 : -1])
                if this_field[0] == 0x04 or this_field[0] == 0x05: # partial or complete list of 32-bit UUIDs
                    for i in xrange((len(this_field) - 1) / 4):
                        ad_services.append(this_field[-1 - i*4 : -5 - i*4 : -1])
                if this_field[0] == 0x06 or this_field[0] == 0x07: # partial or complete list of 128-bit UUIDs
                    for i in xrange((len(this_field) - 1) / 16):
                        ad_services.append(this_field[-1 - i*16 : -17 - i*16 : -1])

    # check for 0x1809 (official thermometer service UUID)
    if uuid_htm_service in ad_services:
        if not args['sender'] in peripheral_list:
            peripheral_list.append(args['sender'])
            #print "%s" % ':'.join(['%02X' % b for b in args['sender'][::-1]])

            # connect to this device
            ble.send_command(ser, ble.ble_cmd_gap_connect_direct(args['sender'], args['address_type'], 0x20, 0x30, 0x100, 0))
            ble.check_activity(ser, 1)
            state = STATE_CONNECTING

# connection_status handler
def my_ble_evt_connection_status(sender, args):
    global state, ble, ser, connection_mac, connection_handle

    if (args['flags'] & 0x05) == 0x05:
        # connected, now perform service discovery
        if not logmode:
            print "Connected to %s" % ':'.join(['%02X' % b for b in args['address'][::-1]])
        connection_mac = args['address']
        connection_handle = args['connection']
        ble.send_command(ser, ble.ble_cmd_attclient_read_by_group_type(args['connection'], 0x0001, 0xFFFF, list(reversed(uuid_service))))
        ble.check_activity(ser, 1)
        state = STATE_FINDING_SERVICES

# attclient_group_found handler
def my_ble_evt_attclient_group_found(sender, args):
    global ble, ser, att_handle_start, att_handle_end

    # found "service" attribute groups (UUID=0x2800), check for thermometer service
    if args['uuid'] == list(reversed(uuid_htm_service)):
        if not logmode:
            print "Found attribute group for service w/UUID=0x1809: start=%d, end=%d" % (args['start'], args['end'])
        att_handle_start = args['start']
        att_handle_end = args['end']

# attclient_find_information_found handler
def my_ble_evt_attclient_find_information_found(sender, args):
    global state, ble, ser, att_handle_measurement, att_handle_measurement_ccc

    # check for thermometer measurement characteristic
    if args['uuid'] == list(reversed(uuid_htm_characteristic)):
        if not logmode:
            print "Found attribute w/UUID=0x2A1C: handle=%d" % args['chrhandle']
        att_handle_measurement = args['chrhandle']

    # check for subsequent client characteristic configuration
    elif args['uuid'] == list(reversed(uuid_client_characteristic_configuration)) and att_handle_measurement > 0:
        if not logmode:
            print "Found attribute w/UUID=0x2902: handle=%d" % args['chrhandle']
        att_handle_measurement_ccc = args['chrhandle']

# attclient_procedure_completed handler
def my_ble_evt_attclient_procedure_completed(sender, args):
    global state, ble, ser, connection_handle, att_handle_start, att_handle_end, att_handle_measurement, att_handle_measurement_ccc

    # check if we just finished searching for services
    if state == STATE_FINDING_SERVICES:
        if att_handle_end > 0:
            if not logmode:
                print "Found 'Health Thermometer' service with UUID 0x1809"

            # found the Health Thermometer service, so now search for the attributes inside
            state = STATE_FINDING_ATTRIBUTES
            ble.send_command(ser, ble.ble_cmd_attclient_find_information(connection_handle, att_handle_start, att_handle_end))
            ble.check_activity(ser, 1)
        else:
            if not logmode:
                print "Could not find 'Health Thermometer' service with UUID 0x1809"

    # check if we just finished searching for attributes within the thermometer service
    elif state == STATE_FINDING_ATTRIBUTES:
        if att_handle_measurement_ccc > 0:
            if not logmode:
                print "Found 'Health Thermometer' measurement attribute with UUID 0x2A1C"

            # found the measurement + client characteristic configuration, so enable indications
            # (this is done by writing 0x0002 to the client characteristic configuration attribute)
            state = STATE_LISTENING_MEASUREMENTS
            ble.send_command(ser, ble.ble_cmd_attclient_attribute_write(connection_handle, att_handle_measurement_ccc, [0x02, 0x00]))
            ble.check_activity(ser, 1)
        else:
            if not logmode:
                print "Could not find 'Health Thermometer' measurement attribute with UUID 0x2A1C"

# attclient_attribute_value handler
def my_ble_evt_attclient_attribute_value(sender, args):
    global state, ble, ser, connection_mac, connection_handle, att_handle_measurement

    # check for a new value from the connected peripheral's temperature measurement attribute
    if args['connection'] == connection_handle and args['atthandle'] == att_handle_measurement:
        htm_flags = args['value'][0]
        htm_exponent = args['value'][4]
        htm_mantissa = (args['value'][3] << 16) | (args['value'][2] << 8) | args['value'][1]
        if htm_exponent > 127: # convert to signed 8-bit int
            htm_exponent = htm_exponent - 256
        htm_measurement = htm_mantissa * pow(10, htm_exponent)
        temp_type = 'C'
        if htm_flags & 0x01: # value sent is Fahrenheit, not Celsius
            temp_type = 'F'
        if not logmode:
            print "Temperature: %.1f%c %c" % (htm_measurement, chr(248), temp_type)
        else:
            t = datetime.datetime.now()
            print "%ld.%03ld %s %.1f %c" % (time.mktime(t.timetuple()), t.microsecond/1000, ':'.join(['%02X' % b for b in connection_mac[::-1]]), htm_measurement, temp_type)


def main():
    global ble, ser, logmode

    # create option parser
    p = optparse.OptionParser(description='BGLib Demo: Health Thermometer Collector v' + __version__)

    # set defaults for options
    p.set_defaults(port="/dev/ttyACM0", baud=115200, packet=False, debug=False)

    # create serial port options argument group
    group = optparse.OptionGroup(p, "Connection Options")
    group.add_option('--port', '-p', type="string", help="Serial port device name (default /dev/ttyACM0)", metavar="PORT")
    group.add_option('--baud', '-b', type="int", help="Serial port baud rate (default 115200)", metavar="BAUD")
    group.add_option('--packet', '-k', action="store_true", help="Packet mode (prefix API packets with <length> byte)")
    group.add_option('--debug', '-d', action="store_true", help="Debug mode (show raw RX/TX API packets)")
    group.add_option('--log', '-l', action="store_true", help="Logfile output mode (use to send stdout into log file)")
    p.add_option_group(group)

    # actually parse all of the arguments
    options, arguments = p.parse_args()

    # create and setup BGLib object
    ble = bglib.BGLib()
    ble.packet_mode = options.packet
    ble.debug = options.debug
    logmode = options.log

    # add handler for BGAPI timeout condition (hopefully won't happen)
    ble.on_timeout += my_timeout

    # add handlers for BGAPI events
    ble.ble_evt_gap_scan_response += my_ble_evt_gap_scan_response
    ble.ble_evt_connection_status += my_ble_evt_connection_status
    ble.ble_evt_attclient_group_found += my_ble_evt_attclient_group_found
    ble.ble_evt_attclient_find_information_found += my_ble_evt_attclient_find_information_found
    ble.ble_evt_attclient_procedure_completed += my_ble_evt_attclient_procedure_completed
    ble.ble_evt_attclient_attribute_value += my_ble_evt_attclient_attribute_value

    # create serial port object
    try:
        ser = serial.Serial(port=options.port, baudrate=options.baud, timeout=1, writeTimeout=1)
    except serial.SerialException as e:
        print "\n================================================================"
        print "Port error (name='%s', baud='%ld'): %s" % (options.port, options.baud, e)
        print "================================================================"
        exit(2)

    # flush buffers
    ser.flushInput()
    ser.flushOutput()

    # disconnect if we are connected already
    ble.send_command(ser, ble.ble_cmd_connection_disconnect(0))
    ble.check_activity(ser, 1)

    # stop advertising if we are advertising already
    ble.send_command(ser, ble.ble_cmd_gap_set_mode(0, 0))
    ble.check_activity(ser, 1)

    # stop scanning if we are scanning already
    ble.send_command(ser, ble.ble_cmd_gap_end_procedure())
    ble.check_activity(ser, 1)

    # set scan parameters
    ble.send_command(ser, ble.ble_cmd_gap_set_scan_parameters(0xC8, 0xC8, 1))
    ble.check_activity(ser, 1)

    # start scanning now
    if not logmode:
        print "Scanning for BLE peripherals..."
    ble.send_command(ser, ble.ble_cmd_gap_discover(1))
    ble.check_activity(ser, 1)

    while (1):
        # check for all incoming data (no timeout, non-blocking)
        ble.check_activity(ser)

        # don't burden the CPU
        time.sleep(0.01)

# gracefully exit without a big exception message if possible
def ctrl_c_handler(signal, frame):
    if not logmode:
        print 'Goodbye!'
    exit(0)

signal.signal(signal.SIGINT, ctrl_c_handler)

if __name__ == '__main__':
    main()
