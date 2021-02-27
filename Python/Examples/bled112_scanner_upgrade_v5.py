#!/usr/bin/env python

""" Barebones BGAPI scanner script for Bluegiga BLE modules

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

Changelog:
    2013-04-07 - Fixed 128-bit UUID filters
               - Added more verbose output on startup
               - Added "friendly mode" output argument
               - Added "quiet mode" output argument
               - Improved comments in code
    2013-03-30 - Initial release
    2020-11-xx - Massive upgrade 
                    - Fixed to work with python >=3.6
                    - Now works on both MS WinXX and Apple OSX x86 using ActiveState Python ( Not regular Python from Python.org)
                    - dump to excel .cvs file function added so that actually works properly
                    - real time plotting of RSSI to live graph function added 
                    - real time plotting of user selected packet payload against time in milliseconds
                    - bglib.py code merged into this file so the whole thing is in one file

"""

__author__ = "Jeff Rowberg & others"
__license__ = "MIT"
__version__ = "2020-11-xx"
__email__ = "jeff@rowberg.net"

#-------------------------------------------------------------------------------------------------------------------
# 
# Things that need to be installed for this script to run 
#
#
#------------------------------------------------------------------------------------------------------------------
#
# 1) Install activestate ActivePython  from   https://platform.activestate.com/ActiveState/ActivePython-3.8
#
#    This is the only Python version which works straight out of the box with all the features of this script.
#
# 
# 2) pip3 install pyserial future pandas matplotlib 
#
# 3) Know the serial port that your BLE112 device is mount on . 
#
#    USB BLE112  device when installed correctly shows up as a virtual serial port .   
#	 This is the case for Apple Mac OSX and Microsoft windows 
#
#	 REF :  https://www.silabs.com/wireless/bluetooth/bluegiga-low-energy-legacy-modules/device.bled112
#	 REF :  https://www.mouser.dk/ProductDetail/Silicon-Labs/BLED112-V1/?qs=2bnkQPT%252BPaf%252BBQ6Sw8mJqQ%3D%3D
#
#------------------------------------------------------------------------------------------------------------------




import sys, optparse, serial, struct, time, datetime, re, signal, string
import matplotlib.pyplot as plt
import pandas as pd
import tkinter as tk

options = []
filter_uuid = []
filter_mac = []
filter_rssi = 0
# create an empty dataframe that will store streaming data
df = None
ax = None
df_byte = None
bx = None
instr_search = None
byte_switch = False
byte_position_ = None

start_time=datetime.datetime.now()

def remove(string): 
    pattern = re.compile(r'\s+') 
    return re.sub(pattern, '', string) 
    #return strip(string) 



def main():
    global options, filter_uuid, filter_mac, filter_rssi, df, ax, instr_search, byte_switch, byte_position_, df_byte, bx

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
            print
            return "".join(result[:-1])

            # process script arguments
    p = MyParser(description='Bluetooth Smart Scanner Updated script for Bluegiga BLED112 v2020-11-30', epilog=
	"""
				Examples:

					bled112_scanner_upgrade_V5.py

				\tDefault options, passive scan, display all devices

					bled112_scanner_upgrade_V5.py -p /dev/ttyUSB0 -d sd

				\tUse ttyUSB0, display only sender MAC address and ad data payload

					bled112_scanner_upgrade_V5.py  -u 1809 -u 180D

				\tDisplay only devices advertising Health Thermometer service (0x1809)
				\tor the Heart Rate service (0x180D)

					bled112_scanner_upgrade_V5.py  -m 00:07:80 -m 08:57:82:bb:27:37

				\tDisplay only devices with a Bluetooth address (MAC) starting with the
				\tBluegiga OUI (00:07:80), or exactly matching 08:57:82:bb:27:37

				Sample Output Explanation:

					1364699494.574 -57 0 000780814494 0 255 02010603030918

					't' (Unix time):\t1364699464.574, 1364699591.128, etc.
					'r' (RSSI value):\t-57, -80, -92, etc.
					'p' (Packet type):\t0 (advertisement), 4 (scan response)
					's' (Sender MAC):\t000780535BB4, 000780814494, etc. {search in the actual MAC field and not the payload status data 
					'a' (Address type):\t0 (public), 1 (random)
					'b' (Bond status):\t255 (no bond), 0 to 15 if bonded
					'd' (Data payload):\t02010603030918, etc. 
							See BT4.0 Core Spec for details about ad packet format

				======================================================================================================

				Examples graph plot functions running on Microsoft Window WINxx or Apple OSX
				
				( !! NOTE : Must be using Activestate ActivePython and not regular Python from www.python.org !!!! )
				+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

				=======================================================================================================

				Use for plotting rssi to graph plot function in MS WinXX 

					python ./bled112_scanner_upgrade_V5.py -q -f -c -x --time_in_ms -d tr -p com27

				Use for plotting rssi to dos CMD function in MS WinXX 

					python ./bled112_scanner_upgrade_V5.py -q -f -c --time_in_ms -d tr -p com27

				Use for plotting rssi and other data to dos CMD function in MS WinXX onwards to MS EXCEL .CSV file 

					python ./bled112_scanner_upgrade_V5.py -q -f -k --time_in_ms -p com27 1>>./dump.csv 

				Use live RSSI plot function to screen live graph 
				( !! NOTE : Must be using Activestate ActivePython and not regular Python from www.python.org !!!! )

					python ./bled112_scanner_upgrade_V5.py -q -f -c  -x --time_in_ms -d tr -p com27

				Use live RSSI plot function to screen live graph and dump to excel CSV file also 
				(!! See Warning NOTE above !!)    

					For MS WinXx :  python ./bled112_scanner_upgrade_V5.py -q -f -k  -x --time_in_ms  -p com27  1>>./dump.csv

					For Apple OSX :  python ./bled112_scanner_upgrade_V5.py -q -f -k  -x --time_in_ms  -p /dev/cu.usbmodem11  1>>./dump.csv

				Using live search in the Payload_status message with live RSSI plot function to screen 
				live graph and dump to excel CSV file also (!! See Warning NOTE above !!) 

					For MS WinXx :  python3 ./bled112_scanner_upgrade_V5.py -q -f -x -k --time_in_ms  -p com27 --instr=FFFE96B6E511 1>>./dump.csv

					For Apple OSX : python3 ./bled112_scanner_upgrade_V5.py -q -f -x -k --time_in_ms  -p /dev/cu.usbmodem11 --instr=FFFE96B6E511 1>>./dump.csv 
				
				Using live search in the Payload_status message with live RSSI plot function to screen 
				live graph and dump to excel CSV file also (!! See Warning NOTE above !!)
				plus search and find the value in byte position 09 and swap the found byte value 
				convert to decimal with the RSSI value.

					For MS WinXx :  python3 ./bled112_scanner_upgrade_V5.py -q -f -k  -x --time_in_ms  -p com27 --instr=FFFE96B6E511 --byte=09 --switch

					For Apple OSX : python3 ./bled112_scanner_upgrade_V5.py -q -f -k  -x --time_in_ms  -p /dev/cu.usbmodem11 --instr=FFFE96B6E511 --byte=09 --switch

				Using live search in the Payload_status message with live RSSI plot function to screen live graph (graph 1)  
				and Selected Byte from Payload data live to plot to graph (graph 2) and dump to excel CSV file 
				Selected Byte from Payload data  = search payload data and find the value in byte position 09 .
				Also (!! See Warning NOTE above !!)

					For MS WinXx :  python3 ./bled112_scanner_upgrade_V5.py -q -f -k  -x --time_in_ms  -p com27 --instr=FFFE96B6E511 --byte=09 -X

					For Apple OSX : python3 ./bled112_scanner_upgrade_V5.py -q -f -k  -x --time_in_ms  -p /dev/cu.usbmodem11 --instr=FFFE96B6E511 --byte=09 -X
				


	"""
    )

    # set all defaults for options
    p.set_defaults(port="COM5", baud=115200, interval=0xC8, window=0xC8, display="trpsabd", uuid=[], mac=[], rssi=0, active=False, quiet=False, friendly=False)

    # create serial port options argument group
    group = optparse.OptionGroup(p, "Serial Port Options")
    group.add_option('--port', '-p', type="string", help="Serial port device name (default /dev/ttyACM0)", metavar="PORT")
    group.add_option('--baud', '-b', type="int", help="Serial port baud rate (default 115200)", metavar="BAUD")
    p.add_option_group(group)

    # create scan options argument group
    group = optparse.OptionGroup(p, "Scan Options")
    group.add_option('--interval', '-i', type="int", help="Scan interval width in units of 0.625ms (default 200)", metavar="INTERVAL")
    group.add_option('--window', '-w', type="int", help="Scan window width in units of 0.625ms (default 200)", metavar="WINDOW")
    group.add_option('--active', '-a', action="store_true", help="Perform active scan (default passive)\nNOTE: active scans result "
                                                                 "in a 'scan response' request being sent to the slave device, which "
                                                                 "should send a follow-up scan response packet. This will result in "
                                                                 "increased power consumption on the slave device.")
    p.add_option_group(group)

    # create filter options argument group
    group = optparse.OptionGroup(p, "Filter Options")
    group.add_option('--uuid', '-u', type="string", action="append", help="Service UUID(s) to match", metavar="UUID")
    group.add_option('--mac', '-m', type="string", action="append", help="MAC address(es) to match format :: xxXXxxXXxx", metavar="ADDRESS")
    group.add_option('--instr', '-S', type="string", action="append", help="Search match 'Payload data' for match ::format :: xxXXxxXXxx", metavar="payload_instr")
    group.add_option('--byte', '-y', type="string", action="append", help="Select single byte from 'Payload data' when there is a match from '--instr search'  '--byte=byte_position ' output out in byte column , !! first character position is Zero !!   ", metavar="byte_position")
    group.add_option('--rssi', '-R', type="int", help="RSSI minimum filter (-110 to -20), omit to disable", metavar="RSSI")
    group.add_option('--install', '-I' ,action="store_true", help="Guide of how to install ")
    p.add_option_group(group)

    # create output options argument group
    group = optparse.OptionGroup(p, "Output Options")
    group.add_option('--switch', '-Z', action="store_true", help="If options'--instr search' and  '--byte=byte_position ' selected. Put byte value in RSSI column")
    group.add_option('--quiet', '-q', action="store_true", help="Quiet mode (suppress initial scan parameter display)")
    group.add_option('--time_in_ms', '-z', action="store_true", help="time_in_ms (Display time in milliseconds)")
    group.add_option('--csv', '-k', action="store_true", help="CVS mode (If options -q and -f are set output in direclty excel csv file friendly format)")
    group.add_option('--comma', '-c', action="store_true", help="Comma mode (If options -q and -f are set output in basic excel csv file not friendly format)")
    group.add_option('--plot', '-x', action="store_true", help="Plot mode , If options '-q -f -c -x --time_in_ms -d tr' are set use live plot graph of rssi verses time )")
    group.add_option('--plotbyte', '-X', action="store_true", help="Plot mode , If options '-q -f -c -x --time_in_ms -d tr' are set use live plot graph of payload selected byte verses time )")
    group.add_option('--friendly', '-f', action="store_true", help="Friendly mode (output in human-readable format)")
    group.add_option('--display', '-d', type="string", help="Display fields and order (default '%default')\n"
        "  t = Unix time, with milliseconds\n"
        "  r = RSSI measurement (signed integer)\n"
        "  p = Packet type (0 = normal, 4 = scan response)\n"
        "  s = Sender MAC address (hexadecimal)\n"
        "  a = Address type (0 = public, 1 = random)\n"
        "  b = Bonding status (255 = no bond, else bond handle)\n"
        "  d = Advertisement data payload (hexadecimal)", metavar="FIELDS")
    p.add_option_group(group)

    # actually parse all of the arguments
    options, arguments = p.parse_args()

    # validate any supplied MAC address filters
    for arg in options.mac:
        if re.search('[^a-fA-F0-9:]', arg):
            p.print_help()
            print("\n================================================================")
            print("Invalid MAC filter argument '%s'\n-->must be in the form AA:BB:CC:DD:EE:FF" % arg)
            print("================================================================")
            exit(1)
        arg2 = arg.replace(":", "").upper()
        if (len(arg2) % 2) == 1:
            p.print_help()
            print("\n================================================================")
            print("Invalid MAC filter argument '%s'\n--> must be 1-6 full bytes in 0-padded hex form (00:01:02:03:04:05)" % arg)
            print("================================================================")
            exit(1)
        mac = []
        for i in range(0, len(arg2), 2):
            mac.append(int(arg2[i : i + 2], 16))
        filter_mac.append(mac)

    # validate any supplied UUID filters
    for arg in options.uuid:
        if re.search('[^a-fA-F0-9:]', arg):
            p.print_help()
            print("\n================================================================")
            print("Invalid UUID filter argument '%s'\n--> must be 2 or 16 full bytes in 0-padded hex form (180B or 0123456789abcdef0123456789abcdef)" % arg)
            print("================================================================")
            exit(1)
        arg2 = arg.replace(":", "").upper()
        if len(arg2) != 4 and len(arg2) != 32:
            p.print_help()
            print("\n================================================================")
            print("Invalid UUID filter argument '%s'\n--> must be 2 or 16 full bytes in 0-padded hex form (180B or 0123456789abcdef0123456789abcdef)" % arg)
            print("================================================================")
            exit(1)
        uuid = []
        for i in range(0, len(arg2), 2):
            uuid.append(int(arg2[i : i + 2], 16))
        filter_uuid.append(uuid)

    # validate RSSI filter argument
    filter_rssi = abs(int(options.rssi))
    if filter_rssi > 0 and (filter_rssi < 20 or filter_rssi > 110):
        p.print_help()
        print("\n================================================================")
        print("Invalid RSSI filter argument '%s'\n--> must be between 20 and 110" % filter_rssi)
        print("================================================================")
        exit(1)

    # validate field output options
    options.display = options.display.lower()
    if re.search('[^trpsabd]', options.display):
        p.print_help()
        print("\n================================================================")
        print("Invalid display options '%s'\n--> must be some combination of 't', 'r', 'p', 's', 'a', 'b', 'd'" % options.display)
        print("================================================================")
        exit(1)

    if options.install :
        print("================================================================")
        print("Install for BLED112 Scanner for Python v%s" % __version__)
        print("================================================================")
        print(" ")
        Print("Program is designed to use  Activestate ActivePython and not regular Python from www.python.org  ")
        print(" ")
        print(" Go to https://www.activestate.com and download and install the latest version of activepython for your operating system ")
        print(" ")
        print(" Once ActivePython is install in a command window shell  type the follow")
        print(" ")
        print("     pip3 install pyserial future pandas matplotlib ")
        print(" ")
        exit(2)


    # display scan parameter summary, if not in quiet mode
    if not(options.quiet) :
        print("================================================================")
        print("BLED112 Scanner for Python v%s" % __version__)
        print("================================================================")
        #p.set_defaults(port="/dev/ttyACM0", baud=115200, interval=0xC8, window=0xC8, display="trpsabd", uuid=[], mac=[], rssi=0, active=False, quiet=False, friendly=False)
        print("Serial port:\t%s" % options.port)
        print("Baud rate:\t%s" % options.baud)
        print("Scan interval:\t%d (%.02f ms)" % (options.interval, options.interval * 1.25))
        print("Scan window:\t%d (%.02f ms)" % (options.window, options.window * 1.25))
        print("Scan type:\t%s" % ['Passive', 'Active'][options.active])
        print("UUID filters:\t",)
        if len(filter_uuid) > 0:
            print("0x%s" % ", 0x".join([''.join(['%02X' % b for b in uuid]) for uuid in filter_uuid]))
        else:
            print("None")
        print("MAC filter(s):\t",)
        if len(filter_mac) > 0:
            print(", ".join([':'.join(['%02X' % b for b in mac]) for mac in filter_mac]))
        else:
            print("None")
        print("RSSI filter:\t",)
        if filter_rssi > 0:
            print("-%d dBm minimum"% filter_rssi)
        else:
            print("None")
        print("Display fields:\t-",)
        field_dict = { 't':'Time', 'r':'RSSI', 'p':'Packet type', 's':'Sender MAC', 'a':'Address type', 'b':'Bond status', 'd':'Payload data' }
        print("\n\t\t- ".join([field_dict[c] for c in options.display]))
        print("Friendly mode:\t%s" % ['Disabled', 'Enabled'][options.friendly])
        print("----------------------------------------------------------------")
        print("Starting scan for BLE advertisements...")

    # open serial port for BGAPI access
    try:
        ser = serial.Serial(port=options.port, baudrate=options.baud, timeout=1)
    except serial.SerialException as e:
        print("\n================================================================")
        print("Port error (name='%s', baud='%ld'): %s" % (options.port, options.baud, e))
        print("================================================================")
        exit(2)

    #=========================================================================================================
    #
    #  Make initial communications with the BLE112 USB device and set up the comms process 
    #
    #=========================================================================================================

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

    # set scan parameters
    #print "Setting scanning parameters..."
    ble_cmd_gap_set_scan_parameters(ser, options.interval, options.window, options.active)
    response = ser.read(6) # 6-byte response
    #for b in response: print '%02X' % ord(b),

    # start scanning now
    #print "Entering scanning mode for general discoverable..."
    ble_cmd_gap_discover(ser, 1)

    
    #=========================================================================================================
    #=========================================================================================================
	
    if not(options.byte) : 
        if options.quiet and options.friendly and options.csv and options.time_in_ms :
            print("\"Time_in_Milliseconds\";\"RSSI\";\"Packet_type\";\"Sender_MAC\";\"Address_type\";\"Bond_status\";\"Payload_status\"")

        if options.quiet and options.friendly and options.csv and not(options.time_in_ms) :
            print("\"Time\";\"RSSI\";\"Packet_type\";\"Sender_MAC\";\"Address_type\";\"Bond_status\";\"Payload_status\"")

    if options.quiet and options.friendly and options.csv and options.time_in_ms and options.instr and options.byte :
        print("\"Time_in_Milliseconds\";\"RSSI\";\"Packet_type\";\"Sender_MAC\";\"Address_type\";\"Bond_status\";\"Payload_status\";\"Selected_Byte(Dec)\"")

    if options.quiet and options.friendly and options.csv and not(options.time_in_ms) and options.instr and options.byte :
        print("\"Time\";\"RSSI\";\"Packet_type\";\"Sender_MAC\";\"Address_type\";\"Bond_status\";\"Payload_status\";\"Selected_Byte(Dec) \"")


    if options.instr : 
        instr_search = str(options.instr)
        instr_search=instr_search[2:len(instr_search)]    #  Original  "['FFFE96B6E511']"   strip and remove [' '] bits 
        instr_search=instr_search[0:(len(instr_search)-2)]
    else :
        instr_search = ""

    if options.byte and (len(str(options.byte)) > 4) :
        byte_str=(str(options.byte))[2:len((str(options.byte)))]
        byte_str=byte_str[0:(len(byte_str)-2)]
        byte_position_=abs(int(byte_str))

        byte_position_=(byte_position_ -1 ) *2

        if (byte_position_ < 0) : byte_position_ = 0

        # print("byte to pick up from payload_status is  :: " + str(byte_position_))  #Debug
    else :
        byte_position_ = -1

    if options.instr and options.byte and options.switch :   byte_switch = True

    #-------------------------------------------------------------------------------------------------------------------
    # 
    # Real time graph plotting routine setup section
    #
    #
    #-------------------------------------------------------------------------------------------------------------------

    if options.plot  :
        
        # create plot
        plt.ion() # <-- work in "interactive mode"
        fig, ax = plt.subplots()
        fig.canvas.set_window_title('Live BLE RSSI level Chart')
        ax.set_title("Primary RSSI level in dB verse time in Milliseconds")

        # create an empty pandas dataframe that will store streaming data
        df = pd.DataFrame()

    if options.instr and options.byte and  options.plotbyte :
        
        # create plot
        plt.ion() # <-- work in "interactive mode"
        fig, bx = plt.subplots()
        fig.canvas.set_window_title('Live BLE Payload data selected Byte Chart [ Byte position in Payload data = ' + byte_str + ' ] ')
        bx.set_title("Selected Byte value (0-255) verse time in Milliseconds")

        # create an empty pandas dataframe that will store streaming data
        df_byte = pd.DataFrame()
    
    #-------------------------------------------------------------------------------------------------------------------
    #-------------------------------------------------------------------------------------------------------------------
    #-------------------------------------------------------------------------------------------------------------------

    while (1):
        # catch all incoming data

        # if options.quiet and options.friendly and options.plot :
        #        while (ser.inWaiting()): bgapi_parse_plot(ord(ser.read()));
        #else:
        #    if options.quiet and options.friendly and options.comma :
        #        while (ser.inWaiting()): bgapi_parse_csv(ord(ser.read()));
        #    else:
        #        if options.quiet and options.friendly and options.csv :
        #            while (ser.inWaiting()): bgapi_parse_csv(ord(ser.read()));
        #        else:
        #            while (ser.inWaiting()): bgapi_parse(ord(ser.read()));

        while (ser.inWaiting()): bgapi_parse_plot(ord(ser.read()));

        # don't burden the CPU
        time.sleep(0.0001)


    # thanks to Masaaki Shibata for Python event handler code
# http://www.emptypage.jp/notes/pyevent.en.html

class BGAPIEvent(object):

    def __init__(self, doc=None):
        self.__doc__ = doc

    def __get__(self, obj, objtype=None):
        if obj is None:
            return self
        return BGAPIEventHandler(self, obj)

    def __set__(self, obj, value):
        pass


class BGAPIEventHandler(object):

    def __init__(self, event, obj):

        self.event = event
        self.obj = obj

    def _getfunctionlist(self):

        """(internal use) """

        try:
            eventhandler = self.obj.__eventhandler__
        except AttributeError:
            eventhandler = self.obj.__eventhandler__ = {}
        return eventhandler.setdefault(self.event, [])

    def add(self, func):

        """Add new event handler function.

        Event handler function must be defined like func(sender, earg).
        You can add handler also by using '+=' operator.
        """

        self._getfunctionlist().append(func)
        return self

    def remove(self, func):

        """Remove existing event handler function.

        You can remove handler also by using '-=' operator.
        """

        self._getfunctionlist().remove(func)
        return self

    def fire(self, earg=None):

        """Fire event and call all handler functions

        You can call EventHandler object itself like e(earg) instead of
        e.fire(earg).
        """

        for func in self._getfunctionlist():
            func(self.obj, earg)

    __iadd__ = add
    __isub__ = remove
    __call__ = fire

#==================================================================================================================================================
#==================================================================================================================================================
#==================================================================================================================================================

#=====================================================
#
# define API commands we might use for this script
#
#=====================================================

def ble_cmd_system_reset(p, boot_in_dfu):
    p.write(struct.pack('5B', 0, 1, 0, 0, boot_in_dfu))

def ble_cmd_connection_disconnect(p, connection):
    p.write(struct.pack('5B', 0, 1, 3, 0, connection))

def ble_cmd_gap_set_mode(p, discover, connect):
    p.write(struct.pack('6B', 0, 2, 6, 1, discover, connect))

def ble_cmd_gap_end_procedure(p):
    p.write(struct.pack('4B', 0, 0, 6, 4))

def ble_cmd_gap_set_scan_parameters(p, scan_interval, scan_window, active):
    p.write(struct.pack('<4BHHB', 0, 5, 6, 7, scan_interval, scan_window, active))

def ble_cmd_gap_discover(p, mode):
    p.write(struct.pack('5B', 0, 1, 6, 2, mode))

#=====================================================
#=====================================================

# define basic BGAPI parser
bgapi_rx_buffer = []
bgapi_rx_expected_length = 0

def bgapi_parse_plot(b):
    global bgapi_rx_buffer, bgapi_rx_expected_length, df, ax, instr_search, byte_switch, byte_position_ , df_byte, bx

    data_packet=None
    select_byte_data_as_str=None
    select_byte_date_as_int=None

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
        bgapi_rx_payload = bytes(bgapi_rx_buffer[4:])
        if packet_type & 0x80 == 0x00: # response
            bgapi_filler = 0
        else: # event
            if packet_class == 0x06: # gap
                if packet_command == 0x00: # scan_response
                    rssi, packet_type, sender, address_type, bond, data_len = struct.unpack('<bB6sBBB', bgapi_rx_payload[:11])
                    sender = [b for b in sender]
                    data_data = [b for b in bgapi_rx_payload[11:]]
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
									#  for i in xrange((len(this_field) - 1) / 2):
                                    for i in range(int((len(this_field) - 1) / 2)):
                                        ad_services.append(this_field[-1 - i*2 : -3 - i*2 : -1])
                                if this_field[0] == 0x04 or this_field[0] == 0x05: # partial or complete list of 32-bit UUIDs
									# for i in xrange((len(this_field) - 1) / 4):
                                    for i in range(int((len(this_field) - 1) / 4)):
                                        ad_services.append(this_field[-1 - i*4 : -5 - i*4 : -1])
                                if this_field[0] == 0x06 or this_field[0] == 0x07: # partial or complete list of 128-bit UUIDs
									# for i in xrange((len(this_field) - 1) / 16):
                                    for i in range(int((len(this_field) - 1) / 16)):
                                        ad_services.append(this_field[-1 - i*16 : -17 - i*16 : -1])
                                if this_field[0] == 0x08 or this_field[0] == 0x09: # shortened or complete local name
                                    ad_local_name = this_field[1:]
                                if this_field[0] == 0x0A: # TX power level
                                    ad_tx_power_level = this_field[1]

                                # OTHER AD PACKET TYPES NOT HANDLED YET

                                if this_field[0] == 0xFF: # manufactuerer specific data
                                    ad_manufacturer.append(this_field[1:])

                    if len(filter_mac) > 0:
                        match = 0
                        for mac in filter_mac:
                            if mac == sender[:-len(mac) - 1:-1]:
                                match = 1
                                break

                        if match == 0: display = 0

                    if display and len(filter_uuid) > 0:
                        if not [i for i in filter_uuid if i in ad_services]: display = 0

                    if display and filter_rssi > 0:
                        if -filter_rssi > rssi: display = 0

                    data_packet=""
                    data_packet=str(data_packet.join(['%02X' % b for b in data_data]))
                    # print ( " Data packet : " + data_packet  + "  instr search :: " + instr_search)  ## debug line
                    select_byte_data_as_int=None
                    #-------------------------------------------------------------------------------------------------------------------
                    #-------------------------------------------------------------------------------------------------------------------
                    #-------------------------------------------------------------------------------------------------------------------

                    if display and len(instr_search)>0 :   
                        if (data_packet.find(instr_search) > -1)   :

                            #-------------------------------------------------------------------------------------------------------------------
                            #-------------------------------------------------------------------------------------------------------------------

                            if (byte_position_ > -1 ) :

                                select_byte_data_as_str=data_packet[ byte_position_ : ( byte_position_ +2 )]

                                if (all(c in string.hexdigits for c in select_byte_data_as_str)) :
                                    #check that the selected data at byte_position is valid hex data 
                                    select_byte_data_as_int=int(select_byte_data_as_str, 16)

                                    # print("Raw packet data :: " + data_packet)  # Debug
                                    # print("byte to pick up from payload_status is  :: " + str(byte_position_) +  "recovered data :: " + select_byte_data_as_str)
                                    # print("recovered data :: " + str(select_byte_data_as_int))

                                    if byte_switch == True :  
                                        #-------------------------------------------------------------------------------------------------------------------
                                        # If there is conversion method to convert the selected byte to a RSSI value it should 
                                        # added here 
                                        #-------------------------------------------------------------------------------------------------------------------
                                        rssi = select_byte_data_as_int
                                else :
                                    if byte_switch == True : 
                                        byte_switch = False 

                            t = datetime.datetime.now()
                            t_now= float("%ld.%03ld" % (time.mktime(t.timetuple()), t.microsecond/1000))
                            t_start=float("%ld.%03ld" % (time.mktime(start_time.timetuple()), start_time.microsecond/1000))
                            t_run=t_now-t_start

                            disp_list = []
                            for c in options.display:
                                if c == 't':
                                    if options.time_in_ms:
                                        disp_list.append("%f" % t_run )
                                    if not(options.time_in_ms):
                                        disp_list.append("%ld.%03ld" % (time.mktime(t.timetuple()), t.microsecond/1000)  )
                                elif c == 'r':
                                    disp_list.append("%d" % rssi)
                                elif c == 'p':
                                    disp_list.append("%d" % packet_type)
                                elif c == 's':
                                    disp_list.append("%s" % ''.join(['%02X' % b for b in sender[::-1]]))
                                elif c == 'a':
                                    disp_list.append("%d" % address_type)
                                elif c == 'b':
                                    disp_list.append("%d" % bond)
                                elif c == 'd':
                                    disp_list.append("%s" % ''.join(['%02X' % b for b in data_data]))
                                    #disp_list.append("%s" % ''.join([data_packet]))

                            if options.csv : 
                                if options.byte : 
                                    op_str= "\"" +  remove('\";\"'.join(disp_list)) + "\"" + ';\"' + str(select_byte_data_as_int) + "\""
                                else :
                                    op_str= "\"" +  remove('\";\"'.join(disp_list)) + "\""

                                print(op_str) 
                            else:
                                if (options.comma or not(options.comma)) and not(options.csv): 
                                    if options.byte : 
                                        op_str= " " +  remove(', '.join(disp_list)) + "," + str(select_byte_data_as_int)
                                    else :
                                        op_str= " " +  remove(', '.join(disp_list)) + ""
                                    print(op_str)

                            #   print "gap_scan_response: rssi: %d, packet_type: %d, sender: %s, address_type: %d, bond: %d, data_len: %d" % \
                            #    (rssi, packet_type, ':'.join(['%02X' % ord(b) for b in sender[::-1]]), address_type, bond, data_len) #                        

                                               

                            #-------------------------------------------------------------------------------------------------------------------
                            # 
                            # Real time graph plotting routine
                            #
                            #
                            #-------------------------------------------------------------------------------------------------------------------

                            # receive python object
                            if options.plot and options.time_in_ms : 

                                # print ("Raw data :: " , t_run, rssi  )

                                #print(op_str)  

                                row =pd.DataFrame({'x':[t_run] ,'y':[rssi]})
                                df = pd.concat([df, row])

                                #df = pd.append([df, row])

                                # print ("df :: "  , df )

                                # print (df.dtypes)


                                #plot all data
                                ax.plot(df['x'] ,df['y'], color='r')

                                # show the plot
                                plt.show()
                                plt.pause(0.0001) # <-- sets the current plot until refreshed
                        
                            #
                            #-------------------------------------------------------------------------------------------------------------------

                            if options.instr and options.byte and  options.plotbyte and options.time_in_ms :

                                                               
                                # print ("Raw data :: " , t_run, rssi  )

                                #print(op_str)  

                                row2 =pd.DataFrame({'x':[t_run] ,'y':[select_byte_data_as_int]})
                                df_byte = pd.concat([df_byte, row2])

                                #df = pd.append([df, row])

                                # print ("df :: "  , df )

                                # print (df.dtypes)


                                #plot all data
                                bx.plot(df_byte['x'] ,df_byte['y'], color='r')

                                # show the plot
                                plt.show()
                                plt.pause(0.0001) # <-- sets the current plot until refreshed

                            #-------------------------------------------------------------------------------------------------------------------
                            #-------------------------------------------------------------------------------------------------------------------
                    
                    else :
                        #-------------------------------------------------------------------------------------------------------------------
                        #-------------------------------------------------------------------------------------------------------------------
                        #-------------------------------------------------------------------------------------------------------------------
                        if display:
                            #print "gap_scan_response: rssi: %d, packet_type: %d, sender: %s, address_type: %d, bond: %d, data_len: %d" % \
                            #    (rssi, packet_type, ':'.join(['%02X' % ord(b) for b in sender[::-1]]), address_type, bond, data_len)
                            t = datetime.datetime.now()
                            t_now= float("%ld.%03ld" % (time.mktime(t.timetuple()), t.microsecond/1000))
                            t_start=float("%ld.%03ld" % (time.mktime(start_time.timetuple()), start_time.microsecond/1000))
                            t_run=t_now-t_start

                            disp_list = []
                            for c in options.display:
                                if c == 't':
                                    if options.time_in_ms:
                                        disp_list.append("%f" % t_run )
                                    if not(options.time_in_ms):
                                        disp_list.append("%ld.%03ld" % (time.mktime(t.timetuple()), t.microsecond/1000)  )
                                elif c == 'r':
                                    disp_list.append("%d" % rssi)
                                elif c == 'p':
                                    disp_list.append("%d" % packet_type)
                                elif c == 's':
                                    disp_list.append("%s" % ''.join(['%02X' % b for b in sender[::-1]]))
                                elif c == 'a':
                                    disp_list.append("%d" % address_type)
                                elif c == 'b':
                                    disp_list.append("%d" % bond)
                                elif c == 'd':
                                    disp_list.append("%s" % ''.join(['%02X' % b for b in data_data]))
                                    #disp_list.append("%s" % ''.join([data_packet]))

                            if (options.comma or not(options.comma)) and not(options.csv): 
                                op_str= " " +  remove(', '.join(disp_list)) + ""
                                print(op_str) 

                            if options.csv : 
                                op_str= "\"" +  remove('\";\"'.join(disp_list)) + "\""
                                print(op_str) 

                        

                                               

                            #-------------------------------------------------------------------------------------------------------------------
                            # 
                            # Real time graph plotting routine
                            #
                            #
                            #-------------------------------------------------------------------------------------------------------------------

                            # receive python object
                            if options.plot and options.time_in_ms : 

                                # print ("Raw data :: " , t_run, rssi  )

                                print(op_str) 
                                row =pd.DataFrame({'x':[t_run] ,'y':[rssi]})
                                df = pd.concat([df, row])

                                #df = pd.append([df, row])

                                # print ("df :: "  , df )

                                # print (df.dtypes)


                                #plot all data
                                ax.plot(df['x'] ,df['y'], color='r')

                                # show the plot
                                plt.show()
                                plt.pause(0.0001) # <-- sets the current plot until refreshed
                        
                            #
                            #-------------------------------------------------------------------------------------------------------------------
                            #-------------------------------------------------------------------------------------------------------------------
                            #-------------------------------------------------------------------------------------------------------------------

        bgapi_rx_buffer = []




class BGLib(object):

    def ble_cmd_system_reset(self, boot_in_dfu):
        return struct.pack('<4BB', 0, 1, 0, 0, boot_in_dfu)
    def ble_cmd_system_hello(self):
        return struct.pack('<4B', 0, 0, 0, 1)
    def ble_cmd_system_address_get(self):
        return struct.pack('<4B', 0, 0, 0, 2)
    def ble_cmd_system_reg_write(self, address, value):
        return struct.pack('<4BHB', 0, 3, 0, 3, address, value)
    def ble_cmd_system_reg_read(self, address):
        return struct.pack('<4BH', 0, 2, 0, 4, address)
    def ble_cmd_system_get_counters(self):
        return struct.pack('<4B', 0, 0, 0, 5)
    def ble_cmd_system_get_connections(self):
        return struct.pack('<4B', 0, 0, 0, 6)
    def ble_cmd_system_read_memory(self, address, length):
        return struct.pack('<4BIB', 0, 5, 0, 7, address, length)
    def ble_cmd_system_get_info(self):
        return struct.pack('<4B', 0, 0, 0, 8)
    def ble_cmd_system_endpoint_tx(self, endpoint, data):
        return struct.pack('<4BBB' + str(len(data)) + 's', 0, 2 + len(data), 0, 9, endpoint, len(data), bytes(i for i in data))
    def ble_cmd_system_whitelist_append(self, address, address_type):
        return struct.pack('<4B6sB', 0, 7, 0, 10, bytes(i for i in address), address_type)
    def ble_cmd_system_whitelist_remove(self, address, address_type):
        return struct.pack('<4B6sB', 0, 7, 0, 11, bytes(i for i in address), address_type)
    def ble_cmd_system_whitelist_clear(self):
        return struct.pack('<4B', 0, 0, 0, 12)
    def ble_cmd_system_endpoint_rx(self, endpoint, size):
        return struct.pack('<4BBB', 0, 2, 0, 13, endpoint, size)
    def ble_cmd_system_endpoint_set_watermarks(self, endpoint, rx, tx):
        return struct.pack('<4BBBB', 0, 3, 0, 14, endpoint, rx, tx)
    def ble_cmd_flash_ps_defrag(self):
        return struct.pack('<4B', 0, 0, 1, 0)
    def ble_cmd_flash_ps_dump(self):
        return struct.pack('<4B', 0, 0, 1, 1)
    def ble_cmd_flash_ps_erase_all(self):
        return struct.pack('<4B', 0, 0, 1, 2)
    def ble_cmd_flash_ps_save(self, key, value):
        return struct.pack('<4BHB' + str(len(value)) + 's', 0, 3 + len(value), 1, 3, key, len(value), bytes(i for i in value))
    def ble_cmd_flash_ps_load(self, key):
        return struct.pack('<4BH', 0, 2, 1, 4, key)
    def ble_cmd_flash_ps_erase(self, key):
        return struct.pack('<4BH', 0, 2, 1, 5, key)
    def ble_cmd_flash_erase_page(self, page):
        return struct.pack('<4BB', 0, 1, 1, 6, page)
    def ble_cmd_flash_write_words(self, address, words):
        return struct.pack('<4BHB' + str(len(words)) + 's', 0, 3 + len(words), 1, 7, address, len(words), bytes(i for i in words))
    def ble_cmd_attributes_write(self, handle, offset, value):
        return struct.pack('<4BHBB' + str(len(value)) + 's', 0, 4 + len(value), 2, 0, handle, offset, len(value), bytes(i for i in value))
    def ble_cmd_attributes_read(self, handle, offset):
        return struct.pack('<4BHH', 0, 4, 2, 1, handle, offset)
    def ble_cmd_attributes_read_type(self, handle):
        return struct.pack('<4BH', 0, 2, 2, 2, handle)
    def ble_cmd_attributes_user_read_response(self, connection, att_error, value):
        return struct.pack('<4BBBB' + str(len(value)) + 's', 0, 3 + len(value), 2, 3, connection, att_error, len(value), bytes(i for i in value))
    def ble_cmd_attributes_user_write_response(self, connection, att_error):
        return struct.pack('<4BBB', 0, 2, 2, 4, connection, att_error)
    def ble_cmd_connection_disconnect(self, connection):
        return struct.pack('<4BB', 0, 1, 3, 0, connection)
    def ble_cmd_connection_get_rssi(self, connection):
        return struct.pack('<4BB', 0, 1, 3, 1, connection)
    def ble_cmd_connection_update(self, connection, interval_min, interval_max, latency, timeout):
        return struct.pack('<4BBHHHH', 0, 9, 3, 2, connection, interval_min, interval_max, latency, timeout)
    def ble_cmd_connection_version_update(self, connection):
        return struct.pack('<4BB', 0, 1, 3, 3, connection)
    def ble_cmd_connection_channel_map_get(self, connection):
        return struct.pack('<4BB', 0, 1, 3, 4, connection)
    def ble_cmd_connection_channel_map_set(self, connection, map):
        return struct.pack('<4BBB' + str(len(map)) + 's', 0, 2 + len(map), 3, 5, connection, len(map), bytes(i for i in map))
    def ble_cmd_connection_features_get(self, connection):
        return struct.pack('<4BB', 0, 1, 3, 6, connection)
    def ble_cmd_connection_get_status(self, connection):
        return struct.pack('<4BB', 0, 1, 3, 7, connection)
    def ble_cmd_connection_raw_tx(self, connection, data):
        return struct.pack('<4BBB' + str(len(data)) + 's', 0, 2 + len(data), 3, 8, connection, len(data), bytes(i for i in data))
    def ble_cmd_attclient_find_by_type_value(self, connection, start, end, uuid, value):
        return struct.pack('<4BBHHHB' + str(len(value)) + 's', 0, 8 + len(value), 4, 0, connection, start, end, uuid, len(value), bytes(i for i in value))
    def ble_cmd_attclient_read_by_group_type(self, connection, start, end, uuid):
        return struct.pack('<4BBHHB' + str(len(uuid)) + 's', 0, 6 + len(uuid), 4, 1, connection, start, end, len(uuid), bytes(i for i in uuid))
    def ble_cmd_attclient_read_by_type(self, connection, start, end, uuid):
        return struct.pack('<4BBHHB' + str(len(uuid)) + 's', 0, 6 + len(uuid), 4, 2, connection, start, end, len(uuid), bytes(i for i in uuid))
    def ble_cmd_attclient_find_information(self, connection, start, end):
        return struct.pack('<4BBHH', 0, 5, 4, 3, connection, start, end)
    def ble_cmd_attclient_read_by_handle(self, connection, chrhandle):
        return struct.pack('<4BBH', 0, 3, 4, 4, connection, chrhandle)
    def ble_cmd_attclient_attribute_write(self, connection, atthandle, data):
        return struct.pack('<4BBHB' + str(len(data)) + 's', 0, 4 + len(data), 4, 5, connection, atthandle, len(data), bytes(i for i in data))
    def ble_cmd_attclient_write_command(self, connection, atthandle, data):
        return struct.pack('<4BBHB' + str(len(data)) + 's', 0, 4 + len(data), 4, 6, connection, atthandle, len(data), bytes(i for i in data))
    def ble_cmd_attclient_indicate_confirm(self, connection):
        return struct.pack('<4BB', 0, 1, 4, 7, connection)
    def ble_cmd_attclient_read_long(self, connection, chrhandle):
        return struct.pack('<4BBH', 0, 3, 4, 8, connection, chrhandle)
    def ble_cmd_attclient_prepare_write(self, connection, atthandle, offset, data):
        return struct.pack('<4BBHHB' + str(len(data)) + 's', 0, 6 + len(data), 4, 9, connection, atthandle, offset, len(data), bytes(i for i in data))
    def ble_cmd_attclient_execute_write(self, connection, commit):
        return struct.pack('<4BBB', 0, 2, 4, 10, connection, commit)
    def ble_cmd_attclient_read_multiple(self, connection, handles):
        return struct.pack('<4BBB' + str(len(handles)) + 's', 0, 2 + len(handles), 4, 11, connection, len(handles), bytes(i for i in handles))
    def ble_cmd_sm_encrypt_start(self, handle, bonding):
        return struct.pack('<4BBB', 0, 2, 5, 0, handle, bonding)
    def ble_cmd_sm_set_bondable_mode(self, bondable):
        return struct.pack('<4BB', 0, 1, 5, 1, bondable)
    def ble_cmd_sm_delete_bonding(self, handle):
        return struct.pack('<4BB', 0, 1, 5, 2, handle)
    def ble_cmd_sm_set_parameters(self, mitm, min_key_size, io_capabilities):
        return struct.pack('<4BBBB', 0, 3, 5, 3, mitm, min_key_size, io_capabilities)
    def ble_cmd_sm_passkey_entry(self, handle, passkey):
        return struct.pack('<4BBI', 0, 5, 5, 4, handle, passkey)
    def ble_cmd_sm_get_bonds(self):
        return struct.pack('<4B', 0, 0, 5, 5)
    def ble_cmd_sm_set_oob_data(self, oob):
        return struct.pack('<4BB' + str(len(oob)) + 's', 0, 1 + len(oob), 5, 6, len(oob), bytes(i for i in oob))
    def ble_cmd_gap_set_privacy_flags(self, peripheral_privacy, central_privacy):
        return struct.pack('<4BBB', 0, 2, 6, 0, peripheral_privacy, central_privacy)
    def ble_cmd_gap_set_mode(self, discover, connect):
        return struct.pack('<4BBB', 0, 2, 6, 1, discover, connect)
    def ble_cmd_gap_discover(self, mode):
        return struct.pack('<4BB', 0, 1, 6, 2, mode)
    def ble_cmd_gap_connect_direct(self, address, addr_type, conn_interval_min, conn_interval_max, timeout, latency):
        return struct.pack('<4B6sBHHHH', 0, 15, 6, 3, bytes(i for i in address), addr_type, conn_interval_min, conn_interval_max, timeout, latency)
    def ble_cmd_gap_end_procedure(self):
        return struct.pack('<4B', 0, 0, 6, 4)
    def ble_cmd_gap_connect_selective(self, conn_interval_min, conn_interval_max, timeout, latency):
        return struct.pack('<4BHHHH', 0, 8, 6, 5, conn_interval_min, conn_interval_max, timeout, latency)
    def ble_cmd_gap_set_filtering(self, scan_policy, adv_policy, scan_duplicate_filtering):
        return struct.pack('<4BBBB', 0, 3, 6, 6, scan_policy, adv_policy, scan_duplicate_filtering)
    def ble_cmd_gap_set_scan_parameters(self, scan_interval, scan_window, active):
        return struct.pack('<4BHHB', 0, 5, 6, 7, scan_interval, scan_window, active)
    def ble_cmd_gap_set_adv_parameters(self, adv_interval_min, adv_interval_max, adv_channels):
        return struct.pack('<4BHHB', 0, 5, 6, 8, adv_interval_min, adv_interval_max, adv_channels)
    def ble_cmd_gap_set_adv_data(self, set_scanrsp, adv_data):
        return struct.pack('<4BBB' + str(len(adv_data)) + 's', 0, 2 + len(adv_data), 6, 9, set_scanrsp, len(adv_data), bytes(i for i in adv_data))
    def ble_cmd_gap_set_directed_connectable_mode(self, address, addr_type):
        return struct.pack('<4B6sB', 0, 7, 6, 10, bytes(i for i in address), addr_type)
    def ble_cmd_hardware_io_port_config_irq(self, port, enable_bits, falling_edge):
        return struct.pack('<4BBBB', 0, 3, 7, 0, port, enable_bits, falling_edge)
    def ble_cmd_hardware_set_soft_timer(self, time, handle, single_shot):
        return struct.pack('<4BIBB', 0, 6, 7, 1, time, handle, single_shot)
    def ble_cmd_hardware_adc_read(self, input, decimation, reference_selection):
        return struct.pack('<4BBBB', 0, 3, 7, 2, input, decimation, reference_selection)
    def ble_cmd_hardware_io_port_config_direction(self, port, direction):
        return struct.pack('<4BBB', 0, 2, 7, 3, port, direction)
    def ble_cmd_hardware_io_port_config_function(self, port, function):
        return struct.pack('<4BBB', 0, 2, 7, 4, port, function)
    def ble_cmd_hardware_io_port_config_pull(self, port, tristate_mask, pull_up):
        return struct.pack('<4BBBB', 0, 3, 7, 5, port, tristate_mask, pull_up)
    def ble_cmd_hardware_io_port_write(self, port, mask, data):
        return struct.pack('<4BBBB', 0, 3, 7, 6, port, mask, data)
    def ble_cmd_hardware_io_port_read(self, port, mask):
        return struct.pack('<4BBB', 0, 2, 7, 7, port, mask)
    def ble_cmd_hardware_spi_config(self, channel, polarity, phase, bit_order, baud_e, baud_m):
        return struct.pack('<4BBBBBBB', 0, 6, 7, 8, channel, polarity, phase, bit_order, baud_e, baud_m)
    def ble_cmd_hardware_spi_transfer(self, channel, data):
        return struct.pack('<4BBB' + str(len(data)) + 's', 0, 2 + len(data), 7, 9, channel, len(data), bytes(i for i in data))
    def ble_cmd_hardware_i2c_read(self, address, stop, length):
        return struct.pack('<4BBBB', 0, 3, 7, 10, address, stop, length)
    def ble_cmd_hardware_i2c_write(self, address, stop, data):
        return struct.pack('<4BBBB' + str(len(data)) + 's', 0, 3 + len(data), 7, 11, address, stop, len(data), bytes(i for i in data))
    def ble_cmd_hardware_set_txpower(self, power):
        return struct.pack('<4BB', 0, 1, 7, 12, power)
    def ble_cmd_hardware_timer_comparator(self, timer, channel, mode, comparator_value):
        return struct.pack('<4BBBBH', 0, 5, 7, 13, timer, channel, mode, comparator_value)
    def ble_cmd_test_phy_tx(self, channel, length, type):
        return struct.pack('<4BBBB', 0, 3, 8, 0, channel, length, type)
    def ble_cmd_test_phy_rx(self, channel):
        return struct.pack('<4BB', 0, 1, 8, 1, channel)
    def ble_cmd_test_phy_end(self):
        return struct.pack('<4B', 0, 0, 8, 2)
    def ble_cmd_test_phy_reset(self):
        return struct.pack('<4B', 0, 0, 8, 3)
    def ble_cmd_test_get_channel_map(self):
        return struct.pack('<4B', 0, 0, 8, 4)
    def ble_cmd_test_debug(self, input):
        return struct.pack('<4BB' + str(len(input)) + 's', 0, 1 + len(input), 8, 5, len(input), bytes(i for i in input))

    ble_rsp_system_reset = BGAPIEvent()
    ble_rsp_system_hello = BGAPIEvent()
    ble_rsp_system_address_get = BGAPIEvent()
    ble_rsp_system_reg_write = BGAPIEvent()
    ble_rsp_system_reg_read = BGAPIEvent()
    ble_rsp_system_get_counters = BGAPIEvent()
    ble_rsp_system_get_connections = BGAPIEvent()
    ble_rsp_system_read_memory = BGAPIEvent()
    ble_rsp_system_get_info = BGAPIEvent()
    ble_rsp_system_endpoint_tx = BGAPIEvent()
    ble_rsp_system_whitelist_append = BGAPIEvent()
    ble_rsp_system_whitelist_remove = BGAPIEvent()
    ble_rsp_system_whitelist_clear = BGAPIEvent()
    ble_rsp_system_endpoint_rx = BGAPIEvent()
    ble_rsp_system_endpoint_set_watermarks = BGAPIEvent()
    ble_rsp_flash_ps_defrag = BGAPIEvent()
    ble_rsp_flash_ps_dump = BGAPIEvent()
    ble_rsp_flash_ps_erase_all = BGAPIEvent()
    ble_rsp_flash_ps_save = BGAPIEvent()
    ble_rsp_flash_ps_load = BGAPIEvent()
    ble_rsp_flash_ps_erase = BGAPIEvent()
    ble_rsp_flash_erase_page = BGAPIEvent()
    ble_rsp_flash_write_words = BGAPIEvent()
    ble_rsp_attributes_write = BGAPIEvent()
    ble_rsp_attributes_read = BGAPIEvent()
    ble_rsp_attributes_read_type = BGAPIEvent()
    ble_rsp_attributes_user_read_response = BGAPIEvent()
    ble_rsp_attributes_user_write_response = BGAPIEvent()
    ble_rsp_connection_disconnect = BGAPIEvent()
    ble_rsp_connection_get_rssi = BGAPIEvent()
    ble_rsp_connection_update = BGAPIEvent()
    ble_rsp_connection_version_update = BGAPIEvent()
    ble_rsp_connection_channel_map_get = BGAPIEvent()
    ble_rsp_connection_channel_map_set = BGAPIEvent()
    ble_rsp_connection_features_get = BGAPIEvent()
    ble_rsp_connection_get_status = BGAPIEvent()
    ble_rsp_connection_raw_tx = BGAPIEvent()
    ble_rsp_attclient_find_by_type_value = BGAPIEvent()
    ble_rsp_attclient_read_by_group_type = BGAPIEvent()
    ble_rsp_attclient_read_by_type = BGAPIEvent()
    ble_rsp_attclient_find_information = BGAPIEvent()
    ble_rsp_attclient_read_by_handle = BGAPIEvent()
    ble_rsp_attclient_attribute_write = BGAPIEvent()
    ble_rsp_attclient_write_command = BGAPIEvent()
    ble_rsp_attclient_indicate_confirm = BGAPIEvent()
    ble_rsp_attclient_read_long = BGAPIEvent()
    ble_rsp_attclient_prepare_write = BGAPIEvent()
    ble_rsp_attclient_execute_write = BGAPIEvent()
    ble_rsp_attclient_read_multiple = BGAPIEvent()
    ble_rsp_sm_encrypt_start = BGAPIEvent()
    ble_rsp_sm_set_bondable_mode = BGAPIEvent()
    ble_rsp_sm_delete_bonding = BGAPIEvent()
    ble_rsp_sm_set_parameters = BGAPIEvent()
    ble_rsp_sm_passkey_entry = BGAPIEvent()
    ble_rsp_sm_get_bonds = BGAPIEvent()
    ble_rsp_sm_set_oob_data = BGAPIEvent()
    ble_rsp_gap_set_privacy_flags = BGAPIEvent()
    ble_rsp_gap_set_mode = BGAPIEvent()
    ble_rsp_gap_discover = BGAPIEvent()
    ble_rsp_gap_connect_direct = BGAPIEvent()
    ble_rsp_gap_end_procedure = BGAPIEvent()
    ble_rsp_gap_connect_selective = BGAPIEvent()
    ble_rsp_gap_set_filtering = BGAPIEvent()
    ble_rsp_gap_set_scan_parameters = BGAPIEvent()
    ble_rsp_gap_set_adv_parameters = BGAPIEvent()
    ble_rsp_gap_set_adv_data = BGAPIEvent()
    ble_rsp_gap_set_directed_connectable_mode = BGAPIEvent()
    ble_rsp_hardware_io_port_config_irq = BGAPIEvent()
    ble_rsp_hardware_set_soft_timer = BGAPIEvent()
    ble_rsp_hardware_adc_read = BGAPIEvent()
    ble_rsp_hardware_io_port_config_direction = BGAPIEvent()
    ble_rsp_hardware_io_port_config_function = BGAPIEvent()
    ble_rsp_hardware_io_port_config_pull = BGAPIEvent()
    ble_rsp_hardware_io_port_write = BGAPIEvent()
    ble_rsp_hardware_io_port_read = BGAPIEvent()
    ble_rsp_hardware_spi_config = BGAPIEvent()
    ble_rsp_hardware_spi_transfer = BGAPIEvent()
    ble_rsp_hardware_i2c_read = BGAPIEvent()
    ble_rsp_hardware_i2c_write = BGAPIEvent()
    ble_rsp_hardware_set_txpower = BGAPIEvent()
    ble_rsp_hardware_timer_comparator = BGAPIEvent()
    ble_rsp_test_phy_tx = BGAPIEvent()
    ble_rsp_test_phy_rx = BGAPIEvent()
    ble_rsp_test_phy_end = BGAPIEvent()
    ble_rsp_test_phy_reset = BGAPIEvent()
    ble_rsp_test_get_channel_map = BGAPIEvent()
    ble_rsp_test_debug = BGAPIEvent()

    ble_evt_system_boot = BGAPIEvent()
    ble_evt_system_debug = BGAPIEvent()
    ble_evt_system_endpoint_watermark_rx = BGAPIEvent()
    ble_evt_system_endpoint_watermark_tx = BGAPIEvent()
    ble_evt_system_script_failure = BGAPIEvent()
    ble_evt_system_no_license_key = BGAPIEvent()
    ble_evt_flash_ps_key = BGAPIEvent()
    ble_evt_attributes_value = BGAPIEvent()
    ble_evt_attributes_user_read_request = BGAPIEvent()
    ble_evt_attributes_status = BGAPIEvent()
    ble_evt_connection_status = BGAPIEvent()
    ble_evt_connection_version_ind = BGAPIEvent()
    ble_evt_connection_feature_ind = BGAPIEvent()
    ble_evt_connection_raw_rx = BGAPIEvent()
    ble_evt_connection_disconnected = BGAPIEvent()
    ble_evt_attclient_indicated = BGAPIEvent()
    ble_evt_attclient_procedure_completed = BGAPIEvent()
    ble_evt_attclient_group_found = BGAPIEvent()
    ble_evt_attclient_attribute_found = BGAPIEvent()
    ble_evt_attclient_find_information_found = BGAPIEvent()
    ble_evt_attclient_attribute_value = BGAPIEvent()
    ble_evt_attclient_read_multiple_response = BGAPIEvent()
    ble_evt_sm_smp_data = BGAPIEvent()
    ble_evt_sm_bonding_fail = BGAPIEvent()
    ble_evt_sm_passkey_display = BGAPIEvent()
    ble_evt_sm_passkey_request = BGAPIEvent()
    ble_evt_sm_bond_status = BGAPIEvent()
    ble_evt_gap_scan_response = BGAPIEvent()
    ble_evt_gap_mode_changed = BGAPIEvent()
    ble_evt_hardware_io_port_status = BGAPIEvent()
    ble_evt_hardware_soft_timer = BGAPIEvent()
    ble_evt_hardware_adc_result = BGAPIEvent()

    def wifi_cmd_dfu_reset(self, dfu):
        return struct.pack('<4BB', 0, 1, 0, 0, dfu)
    def wifi_cmd_dfu_flash_set_address(self, address):
        return struct.pack('<4BI', 0, 4, 0, 1, address)
    def wifi_cmd_dfu_flash_upload(self):
        return struct.pack('<4BB' + str(len(data)) + 's', 0, 1 + len(data), 0, 2, data, len(data), bytes(i for i in data))
    def wifi_cmd_dfu_flash_upload_finish(self):
        return struct.pack('<4B', 0, 0, 0, 3)
    def wifi_cmd_system_sync(self):
        return struct.pack('<4B', 0, 0, 1, 0)
    def wifi_cmd_system_reset(self, dfu):
        return struct.pack('<4BB', 0, 1, 1, 1, dfu)
    def wifi_cmd_system_hello(self):
        return struct.pack('<4B', 0, 0, 1, 2)
    def wifi_cmd_system_set_max_power_saving_state(self, state):
        return struct.pack('<4BB', 0, 1, 1, 3, state)
    def wifi_cmd_config_get_mac(self, hw_interface):
        return struct.pack('<4BB', 0, 1, 2, 0, hw_interface)
    def wifi_cmd_config_set_mac(self, hw_interface):
        return struct.pack('<4BB', 0, 1, 2, 1, hw_interface, mac)
    def wifi_cmd_sme_wifi_on(self):
        return struct.pack('<4B', 0, 0, 3, 0)
    def wifi_cmd_sme_wifi_off(self):
        return struct.pack('<4B', 0, 0, 3, 1)
    def wifi_cmd_sme_power_on(self, enable):
        return struct.pack('<4BB', 0, 1, 3, 2, enable)
    def wifi_cmd_sme_start_scan(self, hw_interface):
        return struct.pack('<4BBB' + str(len(chList)) + 's', 0, 2 + len(chList), 3, 3, hw_interface, chList, len(chList), bytes(i for i in chList))
    def wifi_cmd_sme_stop_scan(self):
        return struct.pack('<4B', 0, 0, 3, 4)
    def wifi_cmd_sme_set_password(self):
        return struct.pack('<4BB' + str(len(password)) + 's', 0, 1 + len(password), 3, 5, password, len(password), bytes(i for i in password))
    def wifi_cmd_sme_connect_bssid(self):
        return struct.pack('<4B', 0, 0, 3, 6, bssid)
    def wifi_cmd_sme_connect_ssid(self):
        return struct.pack('<4BB' + str(len(ssid)) + 's', 0, 1 + len(ssid), 3, 7, ssid, len(ssid), bytes(i for i in ssid))
    def wifi_cmd_sme_disconnect(self):
        return struct.pack('<4B', 0, 0, 3, 8)
    def wifi_cmd_sme_set_scan_channels(self, hw_interface):
        return struct.pack('<4BBB' + str(len(chList)) + 's', 0, 2 + len(chList), 3, 9, hw_interface, chList, len(chList), bytes(i for i in chList))
    def wifi_cmd_tcpip_start_tcp_server(self, port, default_destination):
        return struct.pack('<4BHb', 0, 3, 4, 0, port, default_destination)
    def wifi_cmd_tcpip_tcp_connect(self, port, routing):
        return struct.pack('<4BHb', 0, 3, 4, 1, address, port, routing)
    def wifi_cmd_tcpip_start_udp_server(self, port, default_destination):
        return struct.pack('<4BHb', 0, 3, 4, 2, port, default_destination)
    def wifi_cmd_tcpip_udp_connect(self, port, routing):
        return struct.pack('<4BHb', 0, 3, 4, 3, address, port, routing)
    def wifi_cmd_tcpip_configure(self, use_dhcp):
        return struct.pack('<4BB', 0, 1, 4, 4, address, netmask, gateway, use_dhcp)
    def wifi_cmd_tcpip_dns_configure(self, index):
        return struct.pack('<4BB', 0, 1, 4, 5, index, address)
    def wifi_cmd_tcpip_dns_gethostbyname(self):
        return struct.pack('<4BB' + str(len(name)) + 's', 0, 1 + len(name), 4, 6, name, len(name), bytes(i for i in name))
    def wifi_cmd_endpoint_send(self, endpoint):
        return struct.pack('<4BBB' + str(len(data)) + 's', 0, 2 + len(data), 5, 0, endpoint, data, len(data), bytes(i for i in data))
    def wifi_cmd_endpoint_set_streaming(self, endpoint, streaming):
        return struct.pack('<4BBB', 0, 2, 5, 1, endpoint, streaming)
    def wifi_cmd_endpoint_set_active(self, endpoint, active):
        return struct.pack('<4BBB', 0, 2, 5, 2, endpoint, active)
    def wifi_cmd_endpoint_set_streaming_destination(self, endpoint, streaming_destination):
        return struct.pack('<4BBb', 0, 2, 5, 3, endpoint, streaming_destination)
    def wifi_cmd_endpoint_close(self, endpoint):
        return struct.pack('<4BB', 0, 1, 5, 4, endpoint)
    def wifi_cmd_hardware_set_soft_timer(self, time, handle, single_shot):
        return struct.pack('<4BIBB', 0, 6, 6, 0, time, handle, single_shot)
    def wifi_cmd_hardware_external_interrupt_config(self, enable, polarity):
        return struct.pack('<4BBB', 0, 2, 6, 1, enable, polarity)
    def wifi_cmd_hardware_change_notification_config(self, enable):
        return struct.pack('<4BI', 0, 4, 6, 2, enable)
    def wifi_cmd_hardware_change_notification_pullup(self, pullup):
        return struct.pack('<4BI', 0, 4, 6, 3, pullup)
    def wifi_cmd_hardware_io_port_config_direction(self, port, mask, direction):
        return struct.pack('<4BBHH', 0, 5, 6, 4, port, mask, direction)
    def wifi_cmd_hardware_io_port_config_open_drain(self, port, mask, open_drain):
        return struct.pack('<4BBHH', 0, 5, 6, 5, port, mask, open_drain)
    def wifi_cmd_hardware_io_port_write(self, port, mask, data):
        return struct.pack('<4BBHH', 0, 5, 6, 6, port, mask, data)
    def wifi_cmd_hardware_io_port_read(self, port, mask):
        return struct.pack('<4BBH', 0, 3, 6, 7, port, mask)
    def wifi_cmd_hardware_output_compare(self, index, bit32, timer, mode, compare_value):
        return struct.pack('<4BBBBBI', 0, 8, 6, 8, index, bit32, timer, mode, compare_value)
    def wifi_cmd_hardware_adc_read(self, input):
        return struct.pack('<4BB', 0, 1, 6, 9, input)
    def wifi_cmd_flash_ps_defrag(self):
        return struct.pack('<4B', 0, 0, 7, 0)
    def wifi_cmd_flash_ps_dump(self):
        return struct.pack('<4B', 0, 0, 7, 1)
    def wifi_cmd_flash_ps_erase_all(self):
        return struct.pack('<4B', 0, 0, 7, 2)
    def wifi_cmd_flash_ps_save(self, key):
        return struct.pack('<4BHB' + str(len(value)) + 's', 0, 3 + len(value), 7, 3, key, value, len(value), bytes(i for i in value))
    def wifi_cmd_flash_ps_load(self, key):
        return struct.pack('<4BH', 0, 2, 7, 4, key)
    def wifi_cmd_flash_ps_erase(self, key):
        return struct.pack('<4BH', 0, 2, 7, 5, key)
    def wifi_cmd_i2c_start_read(self, endpoint, slave_address, length):
        return struct.pack('<4BBHB', 0, 4, 8, 0, endpoint, slave_address, length)
    def wifi_cmd_i2c_start_write(self, endpoint, slave_address):
        return struct.pack('<4BBH', 0, 3, 8, 1, endpoint, slave_address)
    def wifi_cmd_i2c_stop(self, endpoint):
        return struct.pack('<4BB', 0, 1, 8, 2, endpoint)

    wifi_rsp_dfu_reset = BGAPIEvent()
    wifi_rsp_dfu_flash_set_address = BGAPIEvent()
    wifi_rsp_dfu_flash_upload = BGAPIEvent()
    wifi_rsp_dfu_flash_upload_finish = BGAPIEvent()
    wifi_rsp_system_sync = BGAPIEvent()
    wifi_rsp_system_reset = BGAPIEvent()
    wifi_rsp_system_hello = BGAPIEvent()
    wifi_rsp_system_set_max_power_saving_state = BGAPIEvent()
    wifi_rsp_config_get_mac = BGAPIEvent()
    wifi_rsp_config_set_mac = BGAPIEvent()
    wifi_rsp_sme_wifi_on = BGAPIEvent()
    wifi_rsp_sme_wifi_off = BGAPIEvent()
    wifi_rsp_sme_power_on = BGAPIEvent()
    wifi_rsp_sme_start_scan = BGAPIEvent()
    wifi_rsp_sme_stop_scan = BGAPIEvent()
    wifi_rsp_sme_set_password = BGAPIEvent()
    wifi_rsp_sme_connect_bssid = BGAPIEvent()
    wifi_rsp_sme_connect_ssid = BGAPIEvent()
    wifi_rsp_sme_disconnect = BGAPIEvent()
    wifi_rsp_sme_set_scan_channels = BGAPIEvent()
    wifi_rsp_tcpip_start_tcp_server = BGAPIEvent()
    wifi_rsp_tcpip_tcp_connect = BGAPIEvent()
    wifi_rsp_tcpip_start_udp_server = BGAPIEvent()
    wifi_rsp_tcpip_udp_connect = BGAPIEvent()
    wifi_rsp_tcpip_configure = BGAPIEvent()
    wifi_rsp_tcpip_dns_configure = BGAPIEvent()
    wifi_rsp_tcpip_dns_gethostbyname = BGAPIEvent()
    wifi_rsp_endpoint_send = BGAPIEvent()
    wifi_rsp_endpoint_set_streaming = BGAPIEvent()
    wifi_rsp_endpoint_set_active = BGAPIEvent()
    wifi_rsp_endpoint_set_streaming_destination = BGAPIEvent()
    wifi_rsp_endpoint_close = BGAPIEvent()
    wifi_rsp_hardware_set_soft_timer = BGAPIEvent()
    wifi_rsp_hardware_external_interrupt_config = BGAPIEvent()
    wifi_rsp_hardware_change_notification_config = BGAPIEvent()
    wifi_rsp_hardware_change_notification_pullup = BGAPIEvent()
    wifi_rsp_hardware_io_port_config_direction = BGAPIEvent()
    wifi_rsp_hardware_io_port_config_open_drain = BGAPIEvent()
    wifi_rsp_hardware_io_port_write = BGAPIEvent()
    wifi_rsp_hardware_io_port_read = BGAPIEvent()
    wifi_rsp_hardware_output_compare = BGAPIEvent()
    wifi_rsp_hardware_adc_read = BGAPIEvent()
    wifi_rsp_flash_ps_defrag = BGAPIEvent()
    wifi_rsp_flash_ps_dump = BGAPIEvent()
    wifi_rsp_flash_ps_erase_all = BGAPIEvent()
    wifi_rsp_flash_ps_save = BGAPIEvent()
    wifi_rsp_flash_ps_load = BGAPIEvent()
    wifi_rsp_flash_ps_erase = BGAPIEvent()
    wifi_rsp_i2c_start_read = BGAPIEvent()
    wifi_rsp_i2c_start_write = BGAPIEvent()
    wifi_rsp_i2c_stop = BGAPIEvent()

    wifi_evt_dfu_boot = BGAPIEvent()
    wifi_evt_system_boot = BGAPIEvent()
    wifi_evt_system_state = BGAPIEvent()
    wifi_evt_system_sw_exception = BGAPIEvent()
    wifi_evt_system_power_saving_state = BGAPIEvent()
    wifi_evt_config_mac_address = BGAPIEvent()
    wifi_evt_sme_wifi_is_on = BGAPIEvent()
    wifi_evt_sme_wifi_is_off = BGAPIEvent()
    wifi_evt_sme_scan_result = BGAPIEvent()
    wifi_evt_sme_scan_result_drop = BGAPIEvent()
    wifi_evt_sme_scanned = BGAPIEvent()
    wifi_evt_sme_connected = BGAPIEvent()
    wifi_evt_sme_disconnected = BGAPIEvent()
    wifi_evt_sme_interface_status = BGAPIEvent()
    wifi_evt_sme_connect_failed = BGAPIEvent()
    wifi_evt_sme_connect_retry = BGAPIEvent()
    wifi_evt_tcpip_configuration = BGAPIEvent()
    wifi_evt_tcpip_dns_configuration = BGAPIEvent()
    wifi_evt_tcpip_endpoint_status = BGAPIEvent()
    wifi_evt_tcpip_dns_gethostbyname_result = BGAPIEvent()
    wifi_evt_endpoint_syntax_error = BGAPIEvent()
    wifi_evt_endpoint_data = BGAPIEvent()
    wifi_evt_endpoint_status = BGAPIEvent()
    wifi_evt_endpoint_closing = BGAPIEvent()
    wifi_evt_hardware_soft_timer = BGAPIEvent()
    wifi_evt_hardware_change_notification = BGAPIEvent()
    wifi_evt_hardware_external_interrupt = BGAPIEvent()
    wifi_evt_flash_ps_key = BGAPIEvent()

    on_busy = BGAPIEvent()
    on_idle = BGAPIEvent()
    on_timeout = BGAPIEvent()
    on_before_tx_command = BGAPIEvent()
    on_tx_command_complete = BGAPIEvent()

    bgapi_rx_buffer = b""
    bgapi_rx_expected_length = 0
    busy = False
    packet_mode = False
    debug = False

    def send_command(self, ser, packet):
        if self.packet_mode: packet = chr(len(packet) & 0xFF) + packet
        if self.debug: print('=>[ ' + ' '.join(['%02X' % b for b in packet]) + ' ]')
        self.on_before_tx_command()
        self.busy = True
        self.on_busy()
        ser.write(packet)
        self.on_tx_command_complete()

    def check_activity(self, ser, timeout=0):
        if timeout > 0:
            ser.timeout = timeout
            while 1:
                x = ser.read()
                if len(x) > 0:
                    self.parse(x)
                else: # timeout
                    self.busy = False
                    self.on_idle()
                    self.on_timeout()
                if not self.busy: # finished
                    break
        else:
            while ser.inWaiting(): self.parse(ser.read())
        return self.busy

    def parse(self, barray):
        b=barray[0]
        if len(self.bgapi_rx_buffer) == 0 and (b == 0x00 or b == 0x80 or b == 0x08 or b == 0x88):
            self.bgapi_rx_buffer+=bytes([b])
        elif len(self.bgapi_rx_buffer) == 1:
            self.bgapi_rx_buffer+=bytes([b])
            self.bgapi_rx_expected_length = 4 + (self.bgapi_rx_buffer[0] & 0x07) + self.bgapi_rx_buffer[1]
        elif len(self.bgapi_rx_buffer) > 1:
            self.bgapi_rx_buffer+=bytes([b])

        """
        BGAPI packet structure (as of 2012-11-07):
            Byte 0:
                  [7] - 1 bit, Message Type (MT)         0 = Command/Response, 1 = Event
                [6:3] - 4 bits, Technology Type (TT)     0000 = Bluetooth 4.0 single mode, 0001 = Wi-Fi
                [2:0] - 3 bits, Length High (LH)         Payload length (high bits)
            Byte 1:     8 bits, Length Low (LL)          Payload length (low bits)
            Byte 2:     8 bits, Class ID (CID)           Command class ID
            Byte 3:     8 bits, Command ID (CMD)         Command ID
            Bytes 4-n:  0 - 2048 Bytes, Payload (PL)     Up to 2048 bytes of payload
        """

        #print'%02X: %d, %d' % (b, len(self.bgapi_rx_buffer), self.bgapi_rx_expected_length)
        if self.bgapi_rx_expected_length > 0 and len(self.bgapi_rx_buffer) == self.bgapi_rx_expected_length:
            if self.debug: print('<=[ ' + ' '.join(['%02X' % b for b in self.bgapi_rx_buffer ]) + ' ]')
            packet_type, payload_length, packet_class, packet_command = self.bgapi_rx_buffer[:4]
            self.bgapi_rx_payload = self.bgapi_rx_buffer[4:]
            self.bgapi_rx_buffer = b""
            if packet_type & 0x88 == 0x00:
                # 0x00 = BLE response packet
                if packet_class == 0:
                    if packet_command == 0: # ble_rsp_system_reset
                        self.ble_rsp_system_reset({  })
                        self.busy = False
                        self.on_idle()
                    elif packet_command == 1: # ble_rsp_system_hello
                        self.ble_rsp_system_hello({  })
                    elif packet_command == 2: # ble_rsp_system_address_get
                        address = struct.unpack('<6s', self.bgapi_rx_payload[:6])[0]
                        address = address
                        self.ble_rsp_system_address_get({ 'address': address })
                    elif packet_command == 3: # ble_rsp_system_reg_write
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_system_reg_write({ 'result': result })
                    elif packet_command == 4: # ble_rsp_system_reg_read
                        address, value = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.ble_rsp_system_reg_read({ 'address': address, 'value': value })
                    elif packet_command == 5: # ble_rsp_system_get_counters
                        txok, txretry, rxok, rxfail, mbuf = struct.unpack('<BBBBB', self.bgapi_rx_payload[:5])
                        self.ble_rsp_system_get_counters({ 'txok': txok, 'txretry': txretry, 'rxok': rxok, 'rxfail': rxfail, 'mbuf': mbuf })
                    elif packet_command == 6: # ble_rsp_system_get_connections
                        maxconn = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.ble_rsp_system_get_connections({ 'maxconn': maxconn })
                    elif packet_command == 7: # ble_rsp_system_read_memory
                        address, data_len = struct.unpack('<IB', self.bgapi_rx_payload[:5])
                        data_data = self.bgapi_rx_payload[5:]
                        self.ble_rsp_system_read_memory({ 'address': address, 'data': data_data })
                    elif packet_command == 8: # ble_rsp_system_get_info
                        major, minor, patch, build, ll_version, protocol_version, hw = struct.unpack('<HHHHHBB', self.bgapi_rx_payload[:12])
                        self.ble_rsp_system_get_info({ 'major': major, 'minor': minor, 'patch': patch, 'build': build, 'll_version': ll_version, 'protocol_version': protocol_version, 'hw': hw })
                    elif packet_command == 9: # ble_rsp_system_endpoint_tx
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_system_endpoint_tx({ 'result': result })
                    elif packet_command == 10: # ble_rsp_system_whitelist_append
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_system_whitelist_append({ 'result': result })
                    elif packet_command == 11: # ble_rsp_system_whitelist_remove
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_system_whitelist_remove({ 'result': result })
                    elif packet_command == 12: # ble_rsp_system_whitelist_clear
                        self.ble_rsp_system_whitelist_clear({  })
                    elif packet_command == 13: # ble_rsp_system_endpoint_rx
                        result, data_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        data_data = self.bgapi_rx_payload[3:]
                        self.ble_rsp_system_endpoint_rx({ 'result': result, 'data': data_data })
                    elif packet_command == 14: # ble_rsp_system_endpoint_set_watermarks
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_system_endpoint_set_watermarks({ 'result': result })
                elif packet_class == 1:
                    if packet_command == 0: # ble_rsp_flash_ps_defrag
                        self.ble_rsp_flash_ps_defrag({  })
                    elif packet_command == 1: # ble_rsp_flash_ps_dump
                        self.ble_rsp_flash_ps_dump({  })
                    elif packet_command == 2: # ble_rsp_flash_ps_erase_all
                        self.ble_rsp_flash_ps_erase_all({  })
                    elif packet_command == 3: # ble_rsp_flash_ps_save
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_flash_ps_save({ 'result': result })
                    elif packet_command == 4: # ble_rsp_flash_ps_load
                        result, value_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        value_data = self.bgapi_rx_payload[3:]
                        self.ble_rsp_flash_ps_load({ 'result': result, 'value': value_data })
                    elif packet_command == 5: # ble_rsp_flash_ps_erase
                        self.ble_rsp_flash_ps_erase({  })
                    elif packet_command == 6: # ble_rsp_flash_erase_page
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_flash_erase_page({ 'result': result })
                    elif packet_command == 7: # ble_rsp_flash_write_words
                        self.ble_rsp_flash_write_words({  })
                elif packet_class == 2:
                    if packet_command == 0: # ble_rsp_attributes_write
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_attributes_write({ 'result': result })
                    elif packet_command == 1: # ble_rsp_attributes_read
                        handle, offset, result, value_len = struct.unpack('<HHHB', self.bgapi_rx_payload[:7])
                        value_data = self.bgapi_rx_payload[7:]
                        self.ble_rsp_attributes_read({ 'handle': handle, 'offset': offset, 'result': result, 'value': value_data })
                    elif packet_command == 2: # ble_rsp_attributes_read_type
                        handle, result, value_len = struct.unpack('<HHB', self.bgapi_rx_payload[:5])
                        value_data = self.bgapi_rx_payload[5:]
                        self.ble_rsp_attributes_read_type({ 'handle': handle, 'result': result, 'value': value_data })
                    elif packet_command == 3: # ble_rsp_attributes_user_read_response
                        self.ble_rsp_attributes_user_read_response({  })
                    elif packet_command == 4: # ble_rsp_attributes_user_write_response
                        self.ble_rsp_attributes_user_write_response({  })
                elif packet_class == 3:
                    if packet_command == 0: # ble_rsp_connection_disconnect
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_connection_disconnect({ 'connection': connection, 'result': result })
                    elif packet_command == 1: # ble_rsp_connection_get_rssi
                        connection, rssi = struct.unpack('<Bb', self.bgapi_rx_payload[:2])
                        self.ble_rsp_connection_get_rssi({ 'connection': connection, 'rssi': rssi })
                    elif packet_command == 2: # ble_rsp_connection_update
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_connection_update({ 'connection': connection, 'result': result })
                    elif packet_command == 3: # ble_rsp_connection_version_update
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_connection_version_update({ 'connection': connection, 'result': result })
                    elif packet_command == 4: # ble_rsp_connection_channel_map_get
                        connection, map_len = struct.unpack('<BB', self.bgapi_rx_payload[:2])
                        map_data = self.bgapi_rx_payload[2:]
                        self.ble_rsp_connection_channel_map_get({ 'connection': connection, 'map': map_data })
                    elif packet_command == 5: # ble_rsp_connection_channel_map_set
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_connection_channel_map_set({ 'connection': connection, 'result': result })
                    elif packet_command == 6: # ble_rsp_connection_features_get
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_connection_features_get({ 'connection': connection, 'result': result })
                    elif packet_command == 7: # ble_rsp_connection_get_status
                        connection = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.ble_rsp_connection_get_status({ 'connection': connection })
                    elif packet_command == 8: # ble_rsp_connection_raw_tx
                        connection = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.ble_rsp_connection_raw_tx({ 'connection': connection })
                elif packet_class == 4:
                    if packet_command == 0: # ble_rsp_attclient_find_by_type_value
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_attclient_find_by_type_value({ 'connection': connection, 'result': result })
                    elif packet_command == 1: # ble_rsp_attclient_read_by_group_type
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_attclient_read_by_group_type({ 'connection': connection, 'result': result })
                    elif packet_command == 2: # ble_rsp_attclient_read_by_type
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_attclient_read_by_type({ 'connection': connection, 'result': result })
                    elif packet_command == 3: # ble_rsp_attclient_find_information
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_attclient_find_information({ 'connection': connection, 'result': result })
                    elif packet_command == 4: # ble_rsp_attclient_read_by_handle
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_attclient_read_by_handle({ 'connection': connection, 'result': result })
                    elif packet_command == 5: # ble_rsp_attclient_attribute_write
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_attclient_attribute_write({ 'connection': connection, 'result': result })
                    elif packet_command == 6: # ble_rsp_attclient_write_command
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_attclient_write_command({ 'connection': connection, 'result': result })
                    elif packet_command == 7: # ble_rsp_attclient_indicate_confirm
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_attclient_indicate_confirm({ 'result': result })
                    elif packet_command == 8: # ble_rsp_attclient_read_long
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_attclient_read_long({ 'connection': connection, 'result': result })
                    elif packet_command == 9: # ble_rsp_attclient_prepare_write
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_attclient_prepare_write({ 'connection': connection, 'result': result })
                    elif packet_command == 10: # ble_rsp_attclient_execute_write
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_attclient_execute_write({ 'connection': connection, 'result': result })
                    elif packet_command == 11: # ble_rsp_attclient_read_multiple
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_attclient_read_multiple({ 'connection': connection, 'result': result })
                elif packet_class == 5:
                    if packet_command == 0: # ble_rsp_sm_encrypt_start
                        handle, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_rsp_sm_encrypt_start({ 'handle': handle, 'result': result })
                    elif packet_command == 1: # ble_rsp_sm_set_bondable_mode
                        self.ble_rsp_sm_set_bondable_mode({  })
                    elif packet_command == 2: # ble_rsp_sm_delete_bonding
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_sm_delete_bonding({ 'result': result })
                    elif packet_command == 3: # ble_rsp_sm_set_parameters
                        self.ble_rsp_sm_set_parameters({  })
                    elif packet_command == 4: # ble_rsp_sm_passkey_entry
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_sm_passkey_entry({ 'result': result })
                    elif packet_command == 5: # ble_rsp_sm_get_bonds
                        bonds = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.ble_rsp_sm_get_bonds({ 'bonds': bonds })
                    elif packet_command == 6: # ble_rsp_sm_set_oob_data
                        self.ble_rsp_sm_set_oob_data({  })
                elif packet_class == 6:
                    if packet_command == 0: # ble_rsp_gap_set_privacy_flags
                        self.ble_rsp_gap_set_privacy_flags({  })
                    elif packet_command == 1: # ble_rsp_gap_set_mode
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_gap_set_mode({ 'result': result })
                    elif packet_command == 2: # ble_rsp_gap_discover
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_gap_discover({ 'result': result })
                    elif packet_command == 3: # ble_rsp_gap_connect_direct
                        result, connection_handle = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.ble_rsp_gap_connect_direct({ 'result': result, 'connection_handle': connection_handle })
                    elif packet_command == 4: # ble_rsp_gap_end_procedure
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_gap_end_procedure({ 'result': result })
                    elif packet_command == 5: # ble_rsp_gap_connect_selective
                        result, connection_handle = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.ble_rsp_gap_connect_selective({ 'result': result, 'connection_handle': connection_handle })
                    elif packet_command == 6: # ble_rsp_gap_set_filtering
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_gap_set_filtering({ 'result': result })
                    elif packet_command == 7: # ble_rsp_gap_set_scan_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_gap_set_scan_parameters({ 'result': result })
                    elif packet_command == 8: # ble_rsp_gap_set_adv_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_gap_set_adv_parameters({ 'result': result })
                    elif packet_command == 9: # ble_rsp_gap_set_adv_data
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_gap_set_adv_data({ 'result': result })
                    elif packet_command == 10: # ble_rsp_gap_set_directed_connectable_mode
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_gap_set_directed_connectable_mode({ 'result': result })
                elif packet_class == 7:
                    if packet_command == 0: # ble_rsp_hardware_io_port_config_irq
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_hardware_io_port_config_irq({ 'result': result })
                    elif packet_command == 1: # ble_rsp_hardware_set_soft_timer
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_hardware_set_soft_timer({ 'result': result })
                    elif packet_command == 2: # ble_rsp_hardware_adc_read
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_hardware_adc_read({ 'result': result })
                    elif packet_command == 3: # ble_rsp_hardware_io_port_config_direction
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_hardware_io_port_config_direction({ 'result': result })
                    elif packet_command == 4: # ble_rsp_hardware_io_port_config_function
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_hardware_io_port_config_function({ 'result': result })
                    elif packet_command == 5: # ble_rsp_hardware_io_port_config_pull
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_hardware_io_port_config_pull({ 'result': result })
                    elif packet_command == 6: # ble_rsp_hardware_io_port_write
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_hardware_io_port_write({ 'result': result })
                    elif packet_command == 7: # ble_rsp_hardware_io_port_read
                        result, port, data = struct.unpack('<HBB', self.bgapi_rx_payload[:4])
                        self.ble_rsp_hardware_io_port_read({ 'result': result, 'port': port, 'data': data })
                    elif packet_command == 8: # ble_rsp_hardware_spi_config
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_hardware_spi_config({ 'result': result })
                    elif packet_command == 9: # ble_rsp_hardware_spi_transfer
                        result, channel, data_len = struct.unpack('<HBB', self.bgapi_rx_payload[:4])
                        data_data = self.bgapi_rx_payload[4:]
                        self.ble_rsp_hardware_spi_transfer({ 'result': result, 'channel': channel, 'data': data_data })
                    elif packet_command == 10: # ble_rsp_hardware_i2c_read
                        result, data_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        data_data = self.bgapi_rx_payload[3:]
                        self.ble_rsp_hardware_i2c_read({ 'result': result, 'data': data_data })
                    elif packet_command == 11: # ble_rsp_hardware_i2c_write
                        written = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.ble_rsp_hardware_i2c_write({ 'written': written })
                    elif packet_command == 12: # ble_rsp_hardware_set_txpower
                        self.ble_rsp_hardware_set_txpower({  })
                    elif packet_command == 13: # ble_rsp_hardware_timer_comparator
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_hardware_timer_comparator({ 'result': result })
                elif packet_class == 8:
                    if packet_command == 0: # ble_rsp_test_phy_tx
                        self.ble_rsp_test_phy_tx({  })
                    elif packet_command == 1: # ble_rsp_test_phy_rx
                        self.ble_rsp_test_phy_rx({  })
                    elif packet_command == 2: # ble_rsp_test_phy_end
                        counter = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.ble_rsp_test_phy_end({ 'counter': counter })
                    elif packet_command == 3: # ble_rsp_test_phy_reset
                        self.ble_rsp_test_phy_reset({  })
                    elif packet_command == 4: # ble_rsp_test_get_channel_map
                        channel_map_len = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        channel_map_data = self.bgapi_rx_payload[1:]
                        self.ble_rsp_test_get_channel_map({ 'channel_map': channel_map_data })
                    elif packet_command == 5: # ble_rsp_test_debug
                        output_len = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        output_data = self.bgapi_rx_payload[1:]
                        self.ble_rsp_test_debug({ 'output': output_data })
                self.busy = False
                self.on_idle()
            elif packet_type & 0x88 == 0x80:
                # 0x80 = BLE event packet
                if packet_class == 0:
                    if packet_command == 0: # ble_evt_system_boot
                        major, minor, patch, build, ll_version, protocol_version, hw = struct.unpack('<HHHHHBB', self.bgapi_rx_payload[:12])
                        self.ble_evt_system_boot({ 'major': major, 'minor': minor, 'patch': patch, 'build': build, 'll_version': ll_version, 'protocol_version': protocol_version, 'hw': hw })
                        self.busy = False
                        self.on_idle()
                    elif packet_command == 1: # ble_evt_system_debug
                        data_len = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        data_data = self.bgapi_rx_payload[1:]
                        self.ble_evt_system_debug({ 'data': data_data })
                    elif packet_command == 2: # ble_evt_system_endpoint_watermark_rx
                        endpoint, data = struct.unpack('<BB', self.bgapi_rx_payload[:2])
                        self.ble_evt_system_endpoint_watermark_rx({ 'endpoint': endpoint, 'data': data })
                    elif packet_command == 3: # ble_evt_system_endpoint_watermark_tx
                        endpoint, data = struct.unpack('<BB', self.bgapi_rx_payload[:2])
                        self.ble_evt_system_endpoint_watermark_tx({ 'endpoint': endpoint, 'data': data })
                    elif packet_command == 4: # ble_evt_system_script_failure
                        address, reason = struct.unpack('<HH', self.bgapi_rx_payload[:4])
                        self.ble_evt_system_script_failure({ 'address': address, 'reason': reason })
                    elif packet_command == 5: # ble_evt_system_no_license_key
                        self.ble_evt_system_no_license_key({  })
                elif packet_class == 1:
                    if packet_command == 0: # ble_evt_flash_ps_key
                        key, value_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        value_data = self.bgapi_rx_payload[3:]
                        self.ble_evt_flash_ps_key({ 'key': key, 'value': value_data })
                elif packet_class == 2:
                    if packet_command == 0: # ble_evt_attributes_value
                        connection, reason, handle, offset, value_len = struct.unpack('<BBHHB', self.bgapi_rx_payload[:7])
                        value_data = self.bgapi_rx_payload[7:]
                        self.ble_evt_attributes_value({ 'connection': connection, 'reason': reason, 'handle': handle, 'offset': offset, 'value': value_data })
                    elif packet_command == 1: # ble_evt_attributes_user_read_request
                        connection, handle, offset, maxsize = struct.unpack('<BHHB', self.bgapi_rx_payload[:6])
                        self.ble_evt_attributes_user_read_request({ 'connection': connection, 'handle': handle, 'offset': offset, 'maxsize': maxsize })
                    elif packet_command == 2: # ble_evt_attributes_status
                        handle, flags = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.ble_evt_attributes_status({ 'handle': handle, 'flags': flags })
                elif packet_class == 3:
                    if packet_command == 0: # ble_evt_connection_status
                        connection, flags, address, address_type, conn_interval, timeout, latency, bonding = struct.unpack('<BB6sBHHHB', self.bgapi_rx_payload[:16])
                        address = address
                        self.ble_evt_connection_status({ 'connection': connection, 'flags': flags, 'address': address, 'address_type': address_type, 'conn_interval': conn_interval, 'timeout': timeout, 'latency': latency, 'bonding': bonding })
                    elif packet_command == 1: # ble_evt_connection_version_ind
                        connection, vers_nr, comp_id, sub_vers_nr = struct.unpack('<BBHH', self.bgapi_rx_payload[:6])
                        self.ble_evt_connection_version_ind({ 'connection': connection, 'vers_nr': vers_nr, 'comp_id': comp_id, 'sub_vers_nr': sub_vers_nr })
                    elif packet_command == 2: # ble_evt_connection_feature_ind
                        connection, features_len = struct.unpack('<BB', self.bgapi_rx_payload[:2])
                        features_data = self.bgapi_rx_payload[2:]
                        self.ble_evt_connection_feature_ind({ 'connection': connection, 'features': features_data })
                    elif packet_command == 3: # ble_evt_connection_raw_rx
                        connection, data_len = struct.unpack('<BB', self.bgapi_rx_payload[:2])
                        data_data = self.bgapi_rx_payload[2:]
                        self.ble_evt_connection_raw_rx({ 'connection': connection, 'data': data_data })
                    elif packet_command == 4: # ble_evt_connection_disconnected
                        connection, reason = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_evt_connection_disconnected({ 'connection': connection, 'reason': reason })
                elif packet_class == 4:
                    if packet_command == 0: # ble_evt_attclient_indicated
                        connection, attrhandle = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_evt_attclient_indicated({ 'connection': connection, 'attrhandle': attrhandle })
                    elif packet_command == 1: # ble_evt_attclient_procedure_completed
                        connection, result, chrhandle = struct.unpack('<BHH', self.bgapi_rx_payload[:5])
                        self.ble_evt_attclient_procedure_completed({ 'connection': connection, 'result': result, 'chrhandle': chrhandle })
                    elif packet_command == 2: # ble_evt_attclient_group_found
                        connection, start, end, uuid_len = struct.unpack('<BHHB', self.bgapi_rx_payload[:6])
                        uuid_data = self.bgapi_rx_payload[6:]
                        self.ble_evt_attclient_group_found({ 'connection': connection, 'start': start, 'end': end, 'uuid': uuid_data })
                    elif packet_command == 3: # ble_evt_attclient_attribute_found
                        connection, chrdecl, value, properties, uuid_len = struct.unpack('<BHHBB', self.bgapi_rx_payload[:7])
                        uuid_data = self.bgapi_rx_payload[7:]
                        self.ble_evt_attclient_attribute_found({ 'connection': connection, 'chrdecl': chrdecl, 'value': value, 'properties': properties, 'uuid': uuid_data })
                    elif packet_command == 4: # ble_evt_attclient_find_information_found
                        connection, chrhandle, uuid_len = struct.unpack('<BHB', self.bgapi_rx_payload[:4])
                        uuid_data = self.bgapi_rx_payload[4:]
                        self.ble_evt_attclient_find_information_found({ 'connection': connection, 'chrhandle': chrhandle, 'uuid': uuid_data })
                    elif packet_command == 5: # ble_evt_attclient_attribute_value
                        connection, atthandle, type, value_len = struct.unpack('<BHBB', self.bgapi_rx_payload[:5])
                        value_data = self.bgapi_rx_payload[5:]
                        self.ble_evt_attclient_attribute_value({ 'connection': connection, 'atthandle': atthandle, 'type': type, 'value': value_data })
                    elif packet_command == 6: # ble_evt_attclient_read_multiple_response
                        connection, handles_len = struct.unpack('<BB', self.bgapi_rx_payload[:2])
                        handles_data = self.bgapi_rx_payload[2:]
                        self.ble_evt_attclient_read_multiple_response({ 'connection': connection, 'handles': handles_data })
                elif packet_class == 5:
                    if packet_command == 0: # ble_evt_sm_smp_data
                        handle, packet, data_len = struct.unpack('<BBB', self.bgapi_rx_payload[:3])
                        data_data = self.bgapi_rx_payload[3:]
                        self.ble_evt_sm_smp_data({ 'handle': handle, 'packet': packet, 'data': data_data })
                    elif packet_command == 1: # ble_evt_sm_bonding_fail
                        handle, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.ble_evt_sm_bonding_fail({ 'handle': handle, 'result': result })
                    elif packet_command == 2: # ble_evt_sm_passkey_display
                        handle, passkey = struct.unpack('<BI', self.bgapi_rx_payload[:5])
                        self.ble_evt_sm_passkey_display({ 'handle': handle, 'passkey': passkey })
                    elif packet_command == 3: # ble_evt_sm_passkey_request
                        handle = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.ble_evt_sm_passkey_request({ 'handle': handle })
                    elif packet_command == 4: # ble_evt_sm_bond_status
                        bond, keysize, mitm, keys = struct.unpack('<BBBB', self.bgapi_rx_payload[:4])
                        self.ble_evt_sm_bond_status({ 'bond': bond, 'keysize': keysize, 'mitm': mitm, 'keys': keys })
                elif packet_class == 6:
                    if packet_command == 0: # ble_evt_gap_scan_response
                        rssi, packet_type, sender, address_type, bond, data_len = struct.unpack('<bB6sBBB', self.bgapi_rx_payload[:11])
                        sender = sender
                        data_data = self.bgapi_rx_payload[11:]
                        self.ble_evt_gap_scan_response({ 'rssi': rssi, 'packet_type': packet_type, 'sender': sender, 'address_type': address_type, 'bond': bond, 'data': data_data })
                    elif packet_command == 1: # ble_evt_gap_mode_changed
                        discover, connect = struct.unpack('<BB', self.bgapi_rx_payload[:2])
                        self.ble_evt_gap_mode_changed({ 'discover': discover, 'connect': connect })
                elif packet_class == 7:
                    if packet_command == 0: # ble_evt_hardware_io_port_status
                        timestamp, port, irq, state = struct.unpack('<IBBB', self.bgapi_rx_payload[:7])
                        self.ble_evt_hardware_io_port_status({ 'timestamp': timestamp, 'port': port, 'irq': irq, 'state': state })
                    elif packet_command == 1: # ble_evt_hardware_soft_timer
                        handle = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.ble_evt_hardware_soft_timer({ 'handle': handle })
                    elif packet_command == 2: # ble_evt_hardware_adc_result
                        input, value = struct.unpack('<Bh', self.bgapi_rx_payload[:3])
                        self.ble_evt_hardware_adc_result({ 'input': input, 'value': value })
            elif packet_type & 0x88 == 0x08:
                # 0x08 = wifi response packet
                if packet_class == 0:
                    if packet_command == 0: # wifi_rsp_dfu_reset
                        self.wifi_rsp_dfu_reset({  })
                        self.busy = False
                        self.on_idle()
                    elif packet_command == 1: # wifi_rsp_dfu_flash_set_address
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_dfu_flash_set_address({ 'result': result })
                    elif packet_command == 2: # wifi_rsp_dfu_flash_upload
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_dfu_flash_upload({ 'result': result })
                    elif packet_command == 3: # wifi_rsp_dfu_flash_upload_finish
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_dfu_flash_upload_finish({ 'result': result })
                elif packet_class == 1:
                    if packet_command == 0: # wifi_rsp_system_sync
                        self.wifi_rsp_system_sync({  })
                    elif packet_command == 1: # wifi_rsp_system_reset
                        self.wifi_rsp_system_reset({  })
                    elif packet_command == 2: # wifi_rsp_system_hello
                        self.wifi_rsp_system_hello({  })
                    elif packet_command == 3: # wifi_rsp_system_set_max_power_saving_state
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_system_set_max_power_saving_state({ 'result': result })
                elif packet_class == 2:
                    if packet_command == 0: # wifi_rsp_config_get_mac
                        result, hw_interface = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_config_get_mac({ 'result': result, 'hw_interface': hw_interface })
                    elif packet_command == 1: # wifi_rsp_config_set_mac
                        result, hw_interface = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_config_set_mac({ 'result': result, 'hw_interface': hw_interface })
                elif packet_class == 3:
                    if packet_command == 0: # wifi_rsp_sme_wifi_on
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_sme_wifi_on({ 'result': result })
                    elif packet_command == 1: # wifi_rsp_sme_wifi_off
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_sme_wifi_off({ 'result': result })
                    elif packet_command == 2: # wifi_rsp_sme_power_on
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_sme_power_on({ 'result': result })
                    elif packet_command == 3: # wifi_rsp_sme_start_scan
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_sme_start_scan({ 'result': result })
                    elif packet_command == 4: # wifi_rsp_sme_stop_scan
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_sme_stop_scan({ 'result': result })
                    elif packet_command == 5: # wifi_rsp_sme_set_password
                        status = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.wifi_rsp_sme_set_password({ 'status': status })
                    elif packet_command == 6: # wifi_rsp_sme_connect_bssid
                        result, hw_interface = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_sme_connect_bssid({ 'result': result, 'hw_interface': hw_interface })
                    elif packet_command == 7: # wifi_rsp_sme_connect_ssid
                        result, hw_interface = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_sme_connect_ssid({ 'result': result, 'hw_interface': hw_interface })
                    elif packet_command == 8: # wifi_rsp_sme_disconnect
                        result, hw_interface = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_sme_disconnect({ 'result': result, 'hw_interface': hw_interface })
                    elif packet_command == 9: # wifi_rsp_sme_set_scan_channels
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_sme_set_scan_channels({ 'result': result })
                elif packet_class == 4:
                    if packet_command == 0: # wifi_rsp_tcpip_start_tcp_server
                        result, endpoint = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_tcpip_start_tcp_server({ 'result': result, 'endpoint': endpoint })
                    elif packet_command == 1: # wifi_rsp_tcpip_tcp_connect
                        result, endpoint = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_tcpip_tcp_connect({ 'result': result, 'endpoint': endpoint })
                    elif packet_command == 2: # wifi_rsp_tcpip_start_udp_server
                        result, endpoint = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_tcpip_start_udp_server({ 'result': result, 'endpoint': endpoint })
                    elif packet_command == 3: # wifi_rsp_tcpip_udp_connect
                        result, endpoint = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_tcpip_udp_connect({ 'result': result, 'endpoint': endpoint })
                    elif packet_command == 4: # wifi_rsp_tcpip_configure
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_tcpip_configure({ 'result': result })
                    elif packet_command == 5: # wifi_rsp_tcpip_dns_configure
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_tcpip_dns_configure({ 'result': result })
                    elif packet_command == 6: # wifi_rsp_tcpip_dns_gethostbyname
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_tcpip_dns_gethostbyname({ 'result': result })
                elif packet_class == 5:
                    if packet_command == 0: # wifi_rsp_endpoint_send
                        result, endpoint = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_endpoint_send({ 'result': result, 'endpoint': endpoint })
                    elif packet_command == 1: # wifi_rsp_endpoint_set_streaming
                        result, endpoint = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_endpoint_set_streaming({ 'result': result, 'endpoint': endpoint })
                    elif packet_command == 2: # wifi_rsp_endpoint_set_active
                        result, endpoint = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_endpoint_set_active({ 'result': result, 'endpoint': endpoint })
                    elif packet_command == 3: # wifi_rsp_endpoint_set_streaming_destination
                        result, endpoint = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_endpoint_set_streaming_destination({ 'result': result, 'endpoint': endpoint })
                    elif packet_command == 4: # wifi_rsp_endpoint_close
                        result, endpoint = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_rsp_endpoint_close({ 'result': result, 'endpoint': endpoint })
                elif packet_class == 6:
                    if packet_command == 0: # wifi_rsp_hardware_set_soft_timer
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_hardware_set_soft_timer({ 'result': result })
                    elif packet_command == 1: # wifi_rsp_hardware_external_interrupt_config
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_hardware_external_interrupt_config({ 'result': result })
                    elif packet_command == 2: # wifi_rsp_hardware_change_notification_config
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_hardware_change_notification_config({ 'result': result })
                    elif packet_command == 3: # wifi_rsp_hardware_change_notification_pullup
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_hardware_change_notification_pullup({ 'result': result })
                    elif packet_command == 4: # wifi_rsp_hardware_io_port_config_direction
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_hardware_io_port_config_direction({ 'result': result })
                    elif packet_command == 5: # wifi_rsp_hardware_io_port_config_open_drain
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_hardware_io_port_config_open_drain({ 'result': result })
                    elif packet_command == 6: # wifi_rsp_hardware_io_port_write
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_hardware_io_port_write({ 'result': result })
                    elif packet_command == 7: # wifi_rsp_hardware_io_port_read
                        result, port, data = struct.unpack('<HBH', self.bgapi_rx_payload[:5])
                        self.wifi_rsp_hardware_io_port_read({ 'result': result, 'port': port, 'data': data })
                    elif packet_command == 8: # wifi_rsp_hardware_output_compare
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_hardware_output_compare({ 'result': result })
                    elif packet_command == 9: # wifi_rsp_hardware_adc_read
                        result, input, value = struct.unpack('<HBH', self.bgapi_rx_payload[:5])
                        self.wifi_rsp_hardware_adc_read({ 'result': result, 'input': input, 'value': value })
                elif packet_class == 7:
                    if packet_command == 0: # wifi_rsp_flash_ps_defrag
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_flash_ps_defrag({ 'result': result })
                    elif packet_command == 1: # wifi_rsp_flash_ps_dump
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_flash_ps_dump({ 'result': result })
                    elif packet_command == 2: # wifi_rsp_flash_ps_erase_all
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_flash_ps_erase_all({ 'result': result })
                    elif packet_command == 3: # wifi_rsp_flash_ps_save
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_flash_ps_save({ 'result': result })
                    elif packet_command == 4: # wifi_rsp_flash_ps_load
                        result, value_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        value_data = self.bgapi_rx_payload[3:]
                        self.wifi_rsp_flash_ps_load({ 'result': result, 'value': value_data })
                    elif packet_command == 5: # wifi_rsp_flash_ps_erase
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_flash_ps_erase({ 'result': result })
                elif packet_class == 8:
                    if packet_command == 0: # wifi_rsp_i2c_start_read
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_i2c_start_read({ 'result': result })
                    elif packet_command == 1: # wifi_rsp_i2c_start_write
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_i2c_start_write({ 'result': result })
                    elif packet_command == 2: # wifi_rsp_i2c_stop
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_rsp_i2c_stop({ 'result': result })
                self.busy = False
                self.on_idle()
            else:
                # 0x88 = wifi event packet
                if packet_class == 0:
                    if packet_command == 0: # wifi_evt_dfu_boot
                        version = struct.unpack('<I', self.bgapi_rx_payload[:4])[0]
                        self.wifi_evt_dfu_boot({ 'version': version })
                        self.busy = False
                        self.on_idle()
                elif packet_class == 1:
                    if packet_command == 0: # wifi_evt_system_boot
                        major, minor, patch, build, bootloader_version, tcpip_version, hw = struct.unpack('<HHHHHHH', self.bgapi_rx_payload[:14])
                        self.wifi_evt_system_boot({ 'major': major, 'minor': minor, 'patch': patch, 'build': build, 'bootloader_version': bootloader_version, 'tcpip_version': tcpip_version, 'hw': hw })
                    elif packet_command == 1: # wifi_evt_system_state
                        state = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_evt_system_state({ 'state': state })
                    elif packet_command == 2: # wifi_evt_system_sw_exception
                        address, type = struct.unpack('<IB', self.bgapi_rx_payload[:5])
                        self.wifi_evt_system_sw_exception({ 'address': address, 'type': type })
                    elif packet_command == 3: # wifi_evt_system_power_saving_state
                        state = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.wifi_evt_system_power_saving_state({ 'state': state })
                elif packet_class == 2:
                    if packet_command == 0: # wifi_evt_config_mac_address
                        hw_interface = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.wifi_evt_config_mac_address({ 'hw_interface': hw_interface })
                elif packet_class == 3:
                    if packet_command == 0: # wifi_evt_sme_wifi_is_on
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_evt_sme_wifi_is_on({ 'result': result })
                    elif packet_command == 1: # wifi_evt_sme_wifi_is_off
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.wifi_evt_sme_wifi_is_off({ 'result': result })
                    elif packet_command == 2: # wifi_evt_sme_scan_result
                        channel, rssi, snr, secure, ssid_len = struct.unpack('<bhbBB', self.bgapi_rx_payload[:6])
                        ssid_data = self.bgapi_rx_payload[6:]
                        self.wifi_evt_sme_scan_result({ 'channel': channel, 'rssi': rssi, 'snr': snr, 'secure': secure, 'ssid': ssid_data })
                    elif packet_command == 3: # wifi_evt_sme_scan_result_drop
                        self.wifi_evt_sme_scan_result_drop({  })
                    elif packet_command == 4: # wifi_evt_sme_scanned
                        status = struct.unpack('<b', self.bgapi_rx_payload[:1])[0]
                        self.wifi_evt_sme_scanned({ 'status': status })
                    elif packet_command == 5: # wifi_evt_sme_connected
                        status, hw_interface = struct.unpack('<bB', self.bgapi_rx_payload[:2])
                        self.wifi_evt_sme_connected({ 'status': status, 'hw_interface': hw_interface })
                    elif packet_command == 6: # wifi_evt_sme_disconnected
                        reason, hw_interface = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_evt_sme_disconnected({ 'reason': reason, 'hw_interface': hw_interface })
                    elif packet_command == 7: # wifi_evt_sme_interface_status
                        hw_interface, status = struct.unpack('<BB', self.bgapi_rx_payload[:2])
                        self.wifi_evt_sme_interface_status({ 'hw_interface': hw_interface, 'status': status })
                    elif packet_command == 8: # wifi_evt_sme_connect_failed
                        reason, hw_interface = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_evt_sme_connect_failed({ 'reason': reason, 'hw_interface': hw_interface })
                    elif packet_command == 9: # wifi_evt_sme_connect_retry
                        hw_interface = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.wifi_evt_sme_connect_retry({ 'hw_interface': hw_interface })
                elif packet_class == 4:
                    if packet_command == 0: # wifi_evt_tcpip_configuration
                        use_dhcp = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.wifi_evt_tcpip_configuration({ 'use_dhcp': use_dhcp })
                    elif packet_command == 1: # wifi_evt_tcpip_dns_configuration
                        index = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.wifi_evt_tcpip_dns_configuration({ 'index': index })
                    elif packet_command == 2: # wifi_evt_tcpip_endpoint_status
                        endpoint, local_port, remote_port = struct.unpack('<BHH', self.bgapi_rx_payload[:5])
                        self.wifi_evt_tcpip_endpoint_status({ 'endpoint': endpoint, 'local_port': local_port, 'remote_port': remote_port })
                    elif packet_command == 3: # wifi_evt_tcpip_dns_gethostbyname_result
                        result, name_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        name_data = self.bgapi_rx_payload[3:]
                        self.wifi_evt_tcpip_dns_gethostbyname_result({ 'result': result, 'name': name_data })
                elif packet_class == 5:
                    if packet_command == 0: # wifi_evt_endpoint_syntax_error
                        endpoint = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.wifi_evt_endpoint_syntax_error({ 'endpoint': endpoint })
                    elif packet_command == 1: # wifi_evt_endpoint_data
                        endpoint, data_len = struct.unpack('<BB', self.bgapi_rx_payload[:2])
                        data_data = self.bgapi_rx_payload[2:]
                        self.wifi_evt_endpoint_data({ 'endpoint': endpoint, 'data': data_data })
                    elif packet_command == 2: # wifi_evt_endpoint_status
                        endpoint, type, streaming, destination, active = struct.unpack('<BIBbB', self.bgapi_rx_payload[:8])
                        self.wifi_evt_endpoint_status({ 'endpoint': endpoint, 'type': type, 'streaming': streaming, 'destination': destination, 'active': active })
                    elif packet_command == 3: # wifi_evt_endpoint_closing
                        reason, endpoint = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.wifi_evt_endpoint_closing({ 'reason': reason, 'endpoint': endpoint })
                elif packet_class == 6:
                    if packet_command == 0: # wifi_evt_hardware_soft_timer
                        handle = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.wifi_evt_hardware_soft_timer({ 'handle': handle })
                    elif packet_command == 1: # wifi_evt_hardware_change_notification
                        timestamp = struct.unpack('<I', self.bgapi_rx_payload[:4])[0]
                        self.wifi_evt_hardware_change_notification({ 'timestamp': timestamp })
                    elif packet_command == 2: # wifi_evt_hardware_external_interrupt
                        irq, timestamp = struct.unpack('<BI', self.bgapi_rx_payload[:5])
                        self.wifi_evt_hardware_external_interrupt({ 'irq': irq, 'timestamp': timestamp })
                elif packet_class == 7:
                    if packet_command == 0: # wifi_evt_flash_ps_key
                        key, value_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        value_data = self.bgapi_rx_payload[3:]
                        self.wifi_evt_flash_ps_key({ 'key': key, 'value': value_data })





# gracefully exit without a big exception message if possible
def ctrl_c_handler(signal, frame):
    #print 'Goodbye, cruel world!'
    exit(0)

signal.signal(signal.SIGINT, ctrl_c_handler)

if __name__ == '__main__':
    main()
