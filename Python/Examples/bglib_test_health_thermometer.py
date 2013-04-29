#!/usr/bin/env python

""" Bluegiga BGAPI/BGLib demo: health thermometer service collector

Changelog:
    2013-04-28 - Initial release

============================================
Bluegiga BGLib Python interface library test health thermometer collector app
2013-04-28 by Jeff Rowberg <jeff@rowberg.net>
Updates should (hopefully) always be available at https://github.com/jrowberg/bglib

============================================
BGLib Python interface library code is placed under the MIT license
Copyright (c) 2013 Jeff Rowberg

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
__version__ = "2013-04-28"
__email__ = "jeff@rowberg.net"

import bglib, serial, time, datetime

ble = 0
ser = 0
peripheral_list = []
connection_handle = 0
att_handle_start = 0
att_handle_end = 0
att_handle_measurement = 0
att_handle_measurement_ccc = 0

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
    print "BGAPI parser timed out. Make sure the BLE device is in a known/idle state."

# gap_scan_response handler
def my_ble_evt_gap_scan_response(sender, args):
    global state, ble, ser

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
    if [0x18, 0x09] in ad_services:
        if not args['sender'] in peripheral_list:
            peripheral_list.append(args['sender'])
            #print "%s" % ':'.join(['%02X' % b for b in args['sender'][::-1]])

            # connect to this device
            ble.send_command(ser, ble.ble_cmd_gap_connect_direct(args['sender'], 0, 0x20, 0x30, 0x100, 0))
            ble.check_activity(ser, 1)
            state = STATE_CONNECTING

# connection_status handler
def my_ble_evt_connection_status(sender, args):
    global state, ble, ser, connection_handle

    if (args['flags'] & 0x05) == 0x05:
        # connected, now perform service discovery
        print "Connected to %s" % ':'.join(['%02X' % b for b in args['address'][::-1]])
        connection_handle = args['connection']
        ble.send_command(ser, ble.ble_cmd_attclient_read_by_group_type(args['connection'], 0x0001, 0xFFFF, [0x00, 0x28]))
        ble.check_activity(ser, 1)
        state = STATE_FINDING_SERVICES

# attclient_group_found handler
def my_ble_evt_attclient_group_found(sender, args):
    global ble, ser, att_handle_start, att_handle_end

    # found "service" attribute groups (UUID=0x2800), check for thermometer service
    if args['uuid'] == [0x09, 0x18]:
        print "Found attribute group for service w/UUID=0x1809: start=%d, end=%d" % (args['start'], args['end'])
        att_handle_start = args['start']
        att_handle_end = args['end']

# attclient_find_information_found handler
def my_ble_evt_attclient_find_information_found(sender, args):
    global state, ble, ser, att_handle_measurement, att_handle_measurement_ccc

    # check for thermometer measurement characteristic
    if args['uuid'] == [0x1C, 0x2A]:
        print "Found attribute w/UUID=0x2A1C: handle=%d" % args['chrhandle']
        att_handle_measurement = args['chrhandle']

    # check for thermometer measurement characteristic
    elif args['uuid'] == [0x02, 0x29] and att_handle_measurement > 0:
        print "Found attribute w/UUID=0x2902: handle=%d" % args['chrhandle']
        att_handle_measurement_ccc = args['chrhandle']

# attclient_procedure_completed handler
def my_ble_evt_attclient_procedure_completed(sender, args):
    global state, ble, ser, connection_handle, att_handle_start, att_handle_end, att_handle_measurement, att_handle_measurement_ccc

    # check if we just finished searching for services
    if state == STATE_FINDING_SERVICES:
        if att_handle_end > 0:
            # found the Health Thermometer service, so now search for the attributes inside
            state = STATE_FINDING_ATTRIBUTES
            ble.send_command(ser, ble.ble_cmd_attclient_find_information(connection_handle, att_handle_start, att_handle_end))
            ble.check_activity(ser, 1)
        else:
            print "Could not find 'Health Thermometer' service with UUID 0x1809"

    # check if we just finished searching for attributes within the thermometer service
    elif state == STATE_FINDING_ATTRIBUTES:
        if att_handle_measurement_ccc > 0:
            # found the measurement + client characteristic configuration, so enable indications
            # (this is done by writing 0x02 to the client characteristic configuration attribute)
            state = STATE_LISTENING_MEASUREMENTS
            ble.send_command(ser, ble.ble_cmd_attclient_attribute_write(connection_handle, att_handle_measurement_ccc, [0x02]))
            ble.check_activity(ser, 1)
        else:
            print "Could not find 'Health Thermometer' measurement attribute with UUID 0x2A1C"

# attclient_attribute_value handler
def my_ble_evt_attclient_attribute_value(sender, args):
    global state, ble, ser, connection_handle, att_handle_measurement

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
        print "Temperature: %.1f%c %c" % (htm_measurement, chr(248), temp_type)


def main():
    global ble, ser

    # NOTE: CHANGE THESE TO FIT YOUR TEST SYSTEM
    port_name = "com10"
    baud_rate = 115200
    packet_mode = False
    enable_debug = True

    # create and setup BGLib object
    ble = bglib.BGLib()
    ble.packet_mode = packet_mode
    ble.debug = enable_debug

    # add handler for BGAPI timeout condition (hopefully won't happen)
    ble.on_timeout += my_timeout

    # add handler for BGAPI event
    ble.ble_evt_gap_scan_response += my_ble_evt_gap_scan_response
    ble.ble_evt_connection_status += my_ble_evt_connection_status
    ble.ble_evt_attclient_group_found += my_ble_evt_attclient_group_found
    ble.ble_evt_attclient_find_information_found += my_ble_evt_attclient_find_information_found
    ble.ble_evt_attclient_procedure_completed += my_ble_evt_attclient_procedure_completed
    ble.ble_evt_attclient_attribute_value += my_ble_evt_attclient_attribute_value

    # create serial port object and flush buffers
    ser = serial.Serial(port=port_name, baudrate=baud_rate, timeout=1, writeTimeout=1)
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
    ble.send_command(ser, ble.ble_cmd_gap_discover(1))
    ble.check_activity(ser, 1)

    while (1):
        # check for all incoming data (no timeout, non-blocking)
        ble.check_activity(ser)

        # don't burden the CPU
        time.sleep(0.01)

if __name__ == '__main__':
    main()
