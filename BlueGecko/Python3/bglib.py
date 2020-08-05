#!/usr/bin/env python

""" Blue Gecko BGAPI/BGLib implementation

Changelog:
    2020-08-03 - Ported to Blue Gecko (Kris Young)
    2017-06-26 - Moved to python3
    2013-05-04 - Fixed single-item struct.unpack returns (@zwasson on Github)
    2013-04-28 - Fixed numerous uint8array/bd_addr command arg errors
               - Added 'debug' support
    2013-04-16 - Fixed 'bglib_on_idle' to be 'on_idle'
    2013-04-15 - Added wifi BGAPI support in addition to BLE BGAPI
               - Fixed references to 'this' instead of 'self'
    2013-04-11 - Initial release

============================================
Blue Gecko BGLib Python interface library
2013-05-04 by Jeff Rowberg <jeff@rowberg.net>
Updates should (hopefully) always be available at https://github.com/jrowberg/bglib

============================================
BGLib Python interface library code is placed under the MIT license
Copyright (c) 2013 Jeff Rowberg
Modifications Copyright (c) 2020 Silicon Laboratories
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
Generated on 2020-Aug-03 22:20:09
===============================================

"""

__author__ = "Jeff Rowberg"
__license__ = "MIT"
__version__ = "2013-05-04"
__email__ = "jeff@rowberg.net"

import struct


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


class BGLib(object):

    def gecko_cmd_dfu_reset(self, dfu):
        return struct.pack('<4BB', 0x20, 1, 0, 0, dfu)
    def gecko_cmd_dfu_flash_set_address(self, address):
        return struct.pack('<4BI', 0x20, 4, 0, 1, address)
    def gecko_cmd_dfu_flash_upload(self, data):
        return struct.pack('<4BB' + str(len(data)) + 's', 0x20, 1 + len(data), 0, 2, len(data), bytes(i for i in data))
    def gecko_cmd_dfu_flash_upload_finish(self):
        return struct.pack('<4B', 0x20, 0, 0, 3)
    def gecko_cmd_system_hello(self):
        return struct.pack('<4B', 0x20, 0, 1, 0)
    def gecko_cmd_system_reset(self, dfu):
        return struct.pack('<4BB', 0x20, 1, 1, 1, dfu)
    def gecko_cmd_system_get_bt_address(self):
        return struct.pack('<4B', 0x20, 0, 1, 3)
    def gecko_cmd_system_set_bt_address(self, address):
        return struct.pack('<4B6s', 0x20, 6, 1, 4, bytes(i for i in address))
    def gecko_cmd_system_set_tx_power(self, power):
        return struct.pack('<4Bh', 0x20, 2, 1, 10, power)
    def gecko_cmd_system_get_random_data(self, length):
        return struct.pack('<4BB', 0x20, 1, 1, 11, length)
    def gecko_cmd_system_halt(self, halt):
        return struct.pack('<4BB', 0x20, 1, 1, 12, halt)
    def gecko_cmd_system_set_device_name(self, type, name):
        return struct.pack('<4BBB' + str(len(name)) + 's', 0x20, 2 + len(name), 1, 13, type, len(name), bytes(i for i in name))
    def gecko_cmd_system_linklayer_configure(self, key, data):
        return struct.pack('<4BBB' + str(len(data)) + 's', 0x20, 2 + len(data), 1, 14, key, len(data), bytes(i for i in data))
    def gecko_cmd_system_get_counters(self, reset):
        return struct.pack('<4BB', 0x20, 1, 1, 15, reset)
    def gecko_cmd_system_data_buffer_write(self, data):
        return struct.pack('<4BB' + str(len(data)) + 's', 0x20, 1 + len(data), 1, 18, len(data), bytes(i for i in data))
    def gecko_cmd_system_set_identity_address(self, address, type):
        return struct.pack('<4B6sB', 0x20, 7, 1, 19, bytes(i for i in address), type)
    def gecko_cmd_system_data_buffer_clear(self):
        return struct.pack('<4B', 0x20, 0, 1, 20)
    def gecko_cmd_le_gap_open(self, address, address_type):
        return struct.pack('<4B6sB', 0x20, 7, 3, 0, bytes(i for i in address), address_type)
    def gecko_cmd_le_gap_set_mode(self, discover, connect):
        return struct.pack('<4BBB', 0x20, 2, 3, 1, discover, connect)
    def gecko_cmd_le_gap_discover(self, mode):
        return struct.pack('<4BB', 0x20, 1, 3, 2, mode)
    def gecko_cmd_le_gap_end_procedure(self):
        return struct.pack('<4B', 0x20, 0, 3, 3)
    def gecko_cmd_le_gap_set_adv_parameters(self, interval_min, interval_max, channel_map):
        return struct.pack('<4BHHB', 0x20, 5, 3, 4, interval_min, interval_max, channel_map)
    def gecko_cmd_le_gap_set_conn_parameters(self, min_interval, max_interval, latency, timeout):
        return struct.pack('<4BHHHH', 0x20, 8, 3, 5, min_interval, max_interval, latency, timeout)
    def gecko_cmd_le_gap_set_scan_parameters(self, scan_interval, scan_window, active):
        return struct.pack('<4BHHB', 0x20, 5, 3, 6, scan_interval, scan_window, active)
    def gecko_cmd_le_gap_set_adv_data(self, scan_rsp, adv_data):
        return struct.pack('<4BBB' + str(len(adv_data)) + 's', 0x20, 2 + len(adv_data), 3, 7, scan_rsp, len(adv_data), bytes(i for i in adv_data))
    def gecko_cmd_le_gap_set_adv_timeout(self, maxevents):
        return struct.pack('<4BB', 0x20, 1, 3, 8, maxevents)
    def gecko_cmd_le_gap_set_conn_phy(self, preferred_phy, accepted_phy):
        return struct.pack('<4BBB', 0x20, 2, 3, 9, preferred_phy, accepted_phy)
    def gecko_cmd_le_gap_bt5_set_mode(self, handle, discover, connect, maxevents, address_type):
        return struct.pack('<4BBBBHB', 0x20, 6, 3, 10, handle, discover, connect, maxevents, address_type)
    def gecko_cmd_le_gap_bt5_set_adv_parameters(self, handle, interval_min, interval_max, channel_map, report_scan):
        return struct.pack('<4BBHHBB', 0x20, 7, 3, 11, handle, interval_min, interval_max, channel_map, report_scan)
    def gecko_cmd_le_gap_bt5_set_adv_data(self, handle, scan_rsp, adv_data):
        return struct.pack('<4BBBB' + str(len(adv_data)) + 's', 0x20, 3 + len(adv_data), 3, 12, handle, scan_rsp, len(adv_data), bytes(i for i in adv_data))
    def gecko_cmd_le_gap_set_privacy_mode(self, privacy, interval):
        return struct.pack('<4BBB', 0x20, 2, 3, 13, privacy, interval)
    def gecko_cmd_le_gap_set_advertise_timing(self, handle, interval_min, interval_max, duration, maxevents):
        return struct.pack('<4BBIIHB', 0x20, 12, 3, 14, handle, interval_min, interval_max, duration, maxevents)
    def gecko_cmd_le_gap_set_advertise_channel_map(self, handle, channel_map):
        return struct.pack('<4BBB', 0x20, 2, 3, 15, handle, channel_map)
    def gecko_cmd_le_gap_set_advertise_report_scan_request(self, handle, report_scan_req):
        return struct.pack('<4BBB', 0x20, 2, 3, 16, handle, report_scan_req)
    def gecko_cmd_le_gap_set_advertise_phy(self, handle, primary_phy, secondary_phy):
        return struct.pack('<4BBBB', 0x20, 3, 3, 17, handle, primary_phy, secondary_phy)
    def gecko_cmd_le_gap_set_advertise_configuration(self, handle, configurations):
        return struct.pack('<4BBI', 0x20, 5, 3, 18, handle, configurations)
    def gecko_cmd_le_gap_clear_advertise_configuration(self, handle, configurations):
        return struct.pack('<4BBI', 0x20, 5, 3, 19, handle, configurations)
    def gecko_cmd_le_gap_start_advertising(self, handle, discover, connect):
        return struct.pack('<4BBBB', 0x20, 3, 3, 20, handle, discover, connect)
    def gecko_cmd_le_gap_stop_advertising(self, handle):
        return struct.pack('<4BB', 0x20, 1, 3, 21, handle)
    def gecko_cmd_le_gap_set_discovery_timing(self, phys, scan_interval, scan_window):
        return struct.pack('<4BBHH', 0x20, 5, 3, 22, phys, scan_interval, scan_window)
    def gecko_cmd_le_gap_set_discovery_type(self, phys, scan_type):
        return struct.pack('<4BBB', 0x20, 2, 3, 23, phys, scan_type)
    def gecko_cmd_le_gap_start_discovery(self, scanning_phy, mode):
        return struct.pack('<4BBB', 0x20, 2, 3, 24, scanning_phy, mode)
    def gecko_cmd_le_gap_set_data_channel_classification(self, channel_map):
        return struct.pack('<4BB' + str(len(channel_map)) + 's', 0x20, 1 + len(channel_map), 3, 25, len(channel_map), bytes(i for i in channel_map))
    def gecko_cmd_le_gap_connect(self, address, address_type, initiating_phy):
        return struct.pack('<4B6sBB', 0x20, 8, 3, 26, bytes(i for i in address), address_type, initiating_phy)
    def gecko_cmd_le_gap_set_advertise_tx_power(self, handle, power):
        return struct.pack('<4BBh', 0x20, 3, 3, 27, handle, power)
    def gecko_cmd_le_gap_set_discovery_extended_scan_response(self, enable):
        return struct.pack('<4BB', 0x20, 1, 3, 28, enable)
    def gecko_cmd_le_gap_start_periodic_advertising(self, handle, interval_min, interval_max, flags):
        return struct.pack('<4BBHHI', 0x20, 9, 3, 29, handle, interval_min, interval_max, flags)
    def gecko_cmd_le_gap_stop_periodic_advertising(self, handle):
        return struct.pack('<4BB', 0x20, 1, 3, 31, handle)
    def gecko_cmd_le_gap_set_long_advertising_data(self, handle, packet_type):
        return struct.pack('<4BBB', 0x20, 2, 3, 32, handle, packet_type)
    def gecko_cmd_le_gap_enable_whitelisting(self, enable):
        return struct.pack('<4BB', 0x20, 1, 3, 33, enable)
    def gecko_cmd_le_gap_set_conn_timing_parameters(self, min_interval, max_interval, latency, timeout, min_ce_length, max_ce_length):
        return struct.pack('<4BHHHHHH', 0x20, 12, 3, 34, min_interval, max_interval, latency, timeout, min_ce_length, max_ce_length)
    def gecko_cmd_le_gap_set_advertise_random_address(self, handle, addr_type, address):
        return struct.pack('<4BBB6s', 0x20, 8, 3, 37, handle, addr_type, bytes(i for i in address))
    def gecko_cmd_le_gap_clear_advertise_random_address(self, handle):
        return struct.pack('<4BB', 0x20, 1, 3, 38, handle)
    def gecko_cmd_sync_open(self, adv_sid, skip, timeout, address, address_type):
        return struct.pack('<4BBHH6sB', 0x20, 12, 66, 0, adv_sid, skip, timeout, bytes(i for i in address), address_type)
    def gecko_cmd_sync_close(self, sync):
        return struct.pack('<4BB', 0x20, 1, 66, 1, sync)
    def gecko_cmd_le_connection_set_parameters(self, connection, min_interval, max_interval, latency, timeout):
        return struct.pack('<4BBHHHH', 0x20, 9, 8, 0, connection, min_interval, max_interval, latency, timeout)
    def gecko_cmd_le_connection_get_rssi(self, connection):
        return struct.pack('<4BB', 0x20, 1, 8, 1, connection)
    def gecko_cmd_le_connection_disable_slave_latency(self, connection, disable):
        return struct.pack('<4BBB', 0x20, 2, 8, 2, connection, disable)
    def gecko_cmd_le_connection_set_phy(self, connection, phy):
        return struct.pack('<4BBB', 0x20, 2, 8, 3, connection, phy)
    def gecko_cmd_le_connection_close(self, connection):
        return struct.pack('<4BB', 0x20, 1, 8, 4, connection)
    def gecko_cmd_le_connection_set_timing_parameters(self, connection, min_interval, max_interval, latency, timeout, min_ce_length, max_ce_length):
        return struct.pack('<4BBHHHHHH', 0x20, 13, 8, 5, connection, min_interval, max_interval, latency, timeout, min_ce_length, max_ce_length)
    def gecko_cmd_le_connection_read_channel_map(self, connection):
        return struct.pack('<4BB', 0x20, 1, 8, 6, connection)
    def gecko_cmd_le_connection_set_preferred_phy(self, connection, preferred_phy, accepted_phy):
        return struct.pack('<4BBBB', 0x20, 3, 8, 7, connection, preferred_phy, accepted_phy)
    def gecko_cmd_gatt_set_max_mtu(self, max_mtu):
        return struct.pack('<4BH', 0x20, 2, 9, 0, max_mtu)
    def gecko_cmd_gatt_discover_primary_services(self, connection):
        return struct.pack('<4BB', 0x20, 1, 9, 1, connection)
    def gecko_cmd_gatt_discover_primary_services_by_uuid(self, connection, uuid):
        return struct.pack('<4BBB' + str(len(uuid)) + 's', 0x20, 2 + len(uuid), 9, 2, connection, len(uuid), bytes(i for i in uuid))
    def gecko_cmd_gatt_discover_characteristics(self, connection, service):
        return struct.pack('<4BBI', 0x20, 5, 9, 3, connection, service)
    def gecko_cmd_gatt_discover_characteristics_by_uuid(self, connection, service, uuid):
        return struct.pack('<4BBIB' + str(len(uuid)) + 's', 0x20, 6 + len(uuid), 9, 4, connection, service, len(uuid), bytes(i for i in uuid))
    def gecko_cmd_gatt_set_characteristic_notification(self, connection, characteristic, flags):
        return struct.pack('<4BBHB', 0x20, 4, 9, 5, connection, characteristic, flags)
    def gecko_cmd_gatt_discover_descriptors(self, connection, characteristic):
        return struct.pack('<4BBH', 0x20, 3, 9, 6, connection, characteristic)
    def gecko_cmd_gatt_read_characteristic_value(self, connection, characteristic):
        return struct.pack('<4BBH', 0x20, 3, 9, 7, connection, characteristic)
    def gecko_cmd_gatt_read_characteristic_value_by_uuid(self, connection, service, uuid):
        return struct.pack('<4BBIB' + str(len(uuid)) + 's', 0x20, 6 + len(uuid), 9, 8, connection, service, len(uuid), bytes(i for i in uuid))
    def gecko_cmd_gatt_write_characteristic_value(self, connection, characteristic, value):
        return struct.pack('<4BBHB' + str(len(value)) + 's', 0x20, 4 + len(value), 9, 9, connection, characteristic, len(value), bytes(i for i in value))
    def gecko_cmd_gatt_write_characteristic_value_without_response(self, connection, characteristic, value):
        return struct.pack('<4BBHB' + str(len(value)) + 's', 0x20, 4 + len(value), 9, 10, connection, characteristic, len(value), bytes(i for i in value))
    def gecko_cmd_gatt_prepare_characteristic_value_write(self, connection, characteristic, offset, value):
        return struct.pack('<4BBHHB' + str(len(value)) + 's', 0x20, 6 + len(value), 9, 11, connection, characteristic, offset, len(value), bytes(i for i in value))
    def gecko_cmd_gatt_execute_characteristic_value_write(self, connection, flags):
        return struct.pack('<4BBB', 0x20, 2, 9, 12, connection, flags)
    def gecko_cmd_gatt_send_characteristic_confirmation(self, connection):
        return struct.pack('<4BB', 0x20, 1, 9, 13, connection)
    def gecko_cmd_gatt_read_descriptor_value(self, connection, descriptor):
        return struct.pack('<4BBH', 0x20, 3, 9, 14, connection, descriptor)
    def gecko_cmd_gatt_write_descriptor_value(self, connection, descriptor, value):
        return struct.pack('<4BBHB' + str(len(value)) + 's', 0x20, 4 + len(value), 9, 15, connection, descriptor, len(value), bytes(i for i in value))
    def gecko_cmd_gatt_find_included_services(self, connection, service):
        return struct.pack('<4BBI', 0x20, 5, 9, 16, connection, service)
    def gecko_cmd_gatt_read_multiple_characteristic_values(self, connection, characteristic_list):
        return struct.pack('<4BBB' + str(len(characteristic_list)) + 's', 0x20, 2 + len(characteristic_list), 9, 17, connection, len(characteristic_list), bytes(i for i in characteristic_list))
    def gecko_cmd_gatt_read_characteristic_value_from_offset(self, connection, characteristic, offset, maxlen):
        return struct.pack('<4BBHHH', 0x20, 7, 9, 18, connection, characteristic, offset, maxlen)
    def gecko_cmd_gatt_prepare_characteristic_value_reliable_write(self, connection, characteristic, offset, value):
        return struct.pack('<4BBHHB' + str(len(value)) + 's', 0x20, 6 + len(value), 9, 19, connection, characteristic, offset, len(value), bytes(i for i in value))
    def gecko_cmd_gatt_server_read_attribute_value(self, attribute, offset):
        return struct.pack('<4BHH', 0x20, 4, 10, 0, attribute, offset)
    def gecko_cmd_gatt_server_read_attribute_type(self, attribute):
        return struct.pack('<4BH', 0x20, 2, 10, 1, attribute)
    def gecko_cmd_gatt_server_write_attribute_value(self, attribute, offset, value):
        return struct.pack('<4BHHB' + str(len(value)) + 's', 0x20, 5 + len(value), 10, 2, attribute, offset, len(value), bytes(i for i in value))
    def gecko_cmd_gatt_server_send_user_read_response(self, connection, characteristic, att_errorcode, value):
        return struct.pack('<4BBHBB' + str(len(value)) + 's', 0x20, 5 + len(value), 10, 3, connection, characteristic, att_errorcode, len(value), bytes(i for i in value))
    def gecko_cmd_gatt_server_send_user_write_response(self, connection, characteristic, att_errorcode):
        return struct.pack('<4BBHB', 0x20, 4, 10, 4, connection, characteristic, att_errorcode)
    def gecko_cmd_gatt_server_send_characteristic_notification(self, connection, characteristic, value):
        return struct.pack('<4BBHB' + str(len(value)) + 's', 0x20, 4 + len(value), 10, 5, connection, characteristic, len(value), bytes(i for i in value))
    def gecko_cmd_gatt_server_find_attribute(self, start, type):
        return struct.pack('<4BHB' + str(len(type)) + 's', 0x20, 3 + len(type), 10, 6, start, len(type), bytes(i for i in type))
    def gecko_cmd_gatt_server_set_capabilities(self, caps, reserved):
        return struct.pack('<4BII', 0x20, 8, 10, 8, caps, reserved)
    def gecko_cmd_gatt_server_find_primary_service(self, start, uuid):
        return struct.pack('<4BHB' + str(len(uuid)) + 's', 0x20, 3 + len(uuid), 10, 9, start, len(uuid), bytes(i for i in uuid))
    def gecko_cmd_gatt_server_set_max_mtu(self, max_mtu):
        return struct.pack('<4BH', 0x20, 2, 10, 10, max_mtu)
    def gecko_cmd_gatt_server_get_mtu(self, connection):
        return struct.pack('<4BB', 0x20, 1, 10, 11, connection)
    def gecko_cmd_gatt_server_enable_capabilities(self, caps):
        return struct.pack('<4BI', 0x20, 4, 10, 12, caps)
    def gecko_cmd_gatt_server_disable_capabilities(self, caps):
        return struct.pack('<4BI', 0x20, 4, 10, 13, caps)
    def gecko_cmd_gatt_server_get_enabled_capabilities(self):
        return struct.pack('<4B', 0x20, 0, 10, 14)
    def gecko_cmd_hardware_set_soft_timer(self, time, handle, single_shot):
        return struct.pack('<4BIBB', 0x20, 6, 12, 0, time, handle, single_shot)
    def gecko_cmd_hardware_get_time(self):
        return struct.pack('<4B', 0x20, 0, 12, 11)
    def gecko_cmd_hardware_set_lazy_soft_timer(self, time, slack, handle, single_shot):
        return struct.pack('<4BIIBB', 0x20, 10, 12, 12, time, slack, handle, single_shot)
    def gecko_cmd_flash_ps_erase_all(self):
        return struct.pack('<4B', 0x20, 0, 13, 1)
    def gecko_cmd_flash_ps_save(self, key, value):
        return struct.pack('<4BHB' + str(len(value)) + 's', 0x20, 3 + len(value), 13, 2, key, len(value), bytes(i for i in value))
    def gecko_cmd_flash_ps_load(self, key):
        return struct.pack('<4BH', 0x20, 2, 13, 3, key)
    def gecko_cmd_flash_ps_erase(self, key):
        return struct.pack('<4BH', 0x20, 2, 13, 4, key)
    def gecko_cmd_test_dtm_tx(self, packet_type, length, channel, phy):
        return struct.pack('<4BBBBB', 0x20, 4, 14, 0, packet_type, length, channel, phy)
    def gecko_cmd_test_dtm_rx(self, channel, phy):
        return struct.pack('<4BBB', 0x20, 2, 14, 1, channel, phy)
    def gecko_cmd_test_dtm_end(self):
        return struct.pack('<4B', 0x20, 0, 14, 2)
    def gecko_cmd_test_debug_command(self, id, debugdata):
        return struct.pack('<4BBB' + str(len(debugdata)) + 's', 0x20, 2 + len(debugdata), 14, 7, id, len(debugdata), bytes(i for i in debugdata))
    def gecko_cmd_test_debug_counter(self, id):
        return struct.pack('<4BI', 0x20, 4, 14, 12, id)
    def gecko_cmd_sm_set_bondable_mode(self, bondable):
        return struct.pack('<4BB', 0x20, 1, 15, 0, bondable)
    def gecko_cmd_sm_configure(self, flags, io_capabilities):
        return struct.pack('<4BBB', 0x20, 2, 15, 1, flags, io_capabilities)
    def gecko_cmd_sm_store_bonding_configuration(self, max_bonding_count, policy_flags):
        return struct.pack('<4BBB', 0x20, 2, 15, 2, max_bonding_count, policy_flags)
    def gecko_cmd_sm_increase_security(self, connection):
        return struct.pack('<4BB', 0x20, 1, 15, 4, connection)
    def gecko_cmd_sm_delete_bonding(self, bonding):
        return struct.pack('<4BB', 0x20, 1, 15, 6, bonding)
    def gecko_cmd_sm_delete_bondings(self):
        return struct.pack('<4B', 0x20, 0, 15, 7)
    def gecko_cmd_sm_enter_passkey(self, connection, passkey):
        return struct.pack('<4BB', 0x20, 1, 15, 8, connection)
    def gecko_cmd_sm_passkey_confirm(self, connection, confirm):
        return struct.pack('<4BBB', 0x20, 2, 15, 9, connection, confirm)
    def gecko_cmd_sm_set_oob_data(self, oob_data):
        return struct.pack('<4BB' + str(len(oob_data)) + 's', 0x20, 1 + len(oob_data), 15, 10, len(oob_data), bytes(i for i in oob_data))
    def gecko_cmd_sm_list_all_bondings(self):
        return struct.pack('<4B', 0x20, 0, 15, 11)
    def gecko_cmd_sm_bonding_confirm(self, connection, confirm):
        return struct.pack('<4BBB', 0x20, 2, 15, 14, connection, confirm)
    def gecko_cmd_sm_set_debug_mode(self):
        return struct.pack('<4B', 0x20, 0, 15, 15)
    def gecko_cmd_sm_set_passkey(self, passkey):
        return struct.pack('<4B', 0x20, 0, 15, 16)
    def gecko_cmd_sm_use_sc_oob(self, enable):
        return struct.pack('<4BB', 0x20, 1, 15, 17, enable)
    def gecko_cmd_sm_set_sc_remote_oob_data(self, oob_data):
        return struct.pack('<4BB' + str(len(oob_data)) + 's', 0x20, 1 + len(oob_data), 15, 18, len(oob_data), bytes(i for i in oob_data))
    def gecko_cmd_sm_add_to_whitelist(self, address, address_type):
        return struct.pack('<4B6sB', 0x20, 7, 15, 19, bytes(i for i in address), address_type)
    def gecko_cmd_sm_set_minimum_key_size(self, minimum_key_size):
        return struct.pack('<4BB', 0x20, 1, 15, 20, minimum_key_size)
    def gecko_cmd_homekit_configure(self, i2c_address, support_display, hap_attribute_features, category, configuration_number, fast_advert_interval, fast_advert_timeout, flag, broadcast_advert_timeout, model_name):
        return struct.pack('<4BBBBHBHHIHB' + str(len(model_name)) + 's', 0x20, 17 + len(model_name), 19, 0, i2c_address, support_display, hap_attribute_features, category, configuration_number, fast_advert_interval, fast_advert_timeout, flag, broadcast_advert_timeout, len(model_name), bytes(i for i in model_name))
    def gecko_cmd_homekit_advertise(self, enable, interval_min, interval_max, channel_map):
        return struct.pack('<4BBHHB', 0x20, 6, 19, 1, enable, interval_min, interval_max, channel_map)
    def gecko_cmd_homekit_delete_pairings(self):
        return struct.pack('<4B', 0x20, 0, 19, 2)
    def gecko_cmd_homekit_check_authcp(self):
        return struct.pack('<4B', 0x20, 0, 19, 3)
    def gecko_cmd_homekit_get_pairing_id(self, connection):
        return struct.pack('<4BB', 0x20, 1, 19, 4, connection)
    def gecko_cmd_homekit_send_write_response(self, connection, characteristic, status_code):
        return struct.pack('<4BBHB', 0x20, 4, 19, 5, connection, characteristic, status_code)
    def gecko_cmd_homekit_send_read_response(self, connection, characteristic, status_code, attribute_size, value):
        return struct.pack('<4BBHBHB' + str(len(value)) + 's', 0x20, 7 + len(value), 19, 6, connection, characteristic, status_code, attribute_size, len(value), bytes(i for i in value))
    def gecko_cmd_homekit_gsn_action(self, action):
        return struct.pack('<4BB', 0x20, 1, 19, 7, action)
    def gecko_cmd_homekit_event_notification(self, connection, characteristic, change_originator, value):
        return struct.pack('<4BBHBB' + str(len(value)) + 's', 0x20, 5 + len(value), 19, 8, connection, characteristic, change_originator, len(value), bytes(i for i in value))
    def gecko_cmd_homekit_broadcast_action(self, action, params):
        return struct.pack('<4BBB' + str(len(params)) + 's', 0x20, 2 + len(params), 19, 9, action, len(params), bytes(i for i in params))
    def gecko_cmd_homekit_configure_product_data(self, product_data):
        return struct.pack('<4BB' + str(len(product_data)) + 's', 0x20, 1 + len(product_data), 19, 10, len(product_data), bytes(i for i in product_data))
    def gecko_cmd_coex_set_options(self, mask, options):
        return struct.pack('<4BII', 0x20, 8, 32, 0, mask, options)
    def gecko_cmd_coex_get_counters(self, reset):
        return struct.pack('<4BB', 0x20, 1, 32, 1, reset)
    def gecko_cmd_coex_set_parameters(self, priority, request, pwm_period, pwm_dutycycle):
        return struct.pack('<4BBBBB', 0x20, 4, 32, 2, priority, request, pwm_period, pwm_dutycycle)
    def gecko_cmd_coex_set_directional_priority_pulse(self, pulse):
        return struct.pack('<4BB', 0x20, 1, 32, 3, pulse)
    def gecko_cmd_l2cap_coc_send_connection_request(self, connection, le_psm, mtu, mps, initial_credit):
        return struct.pack('<4BBHHHH', 0x20, 9, 67, 1, connection, le_psm, mtu, mps, initial_credit)
    def gecko_cmd_l2cap_coc_send_connection_response(self, connection, cid, mtu, mps, initial_credit, result):
        return struct.pack('<4BBHHHHH', 0x20, 11, 67, 2, connection, cid, mtu, mps, initial_credit, result)
    def gecko_cmd_l2cap_coc_send_le_flow_control_credit(self, connection, cid, credits):
        return struct.pack('<4BBHH', 0x20, 5, 67, 3, connection, cid, credits)
    def gecko_cmd_l2cap_coc_send_disconnection_request(self, connection, cid):
        return struct.pack('<4BBH', 0x20, 3, 67, 4, connection, cid)
    def gecko_cmd_l2cap_coc_send_data(self, connection, cid, data):
        return struct.pack('<4BBHB' + str(len(data)) + 's', 0x20, 4 + len(data), 67, 5, connection, cid, len(data), bytes(i for i in data))
    def gecko_cmd_cte_transmitter_enable_cte_response(self, connection, cte_types, switching_pattern):
        return struct.pack('<4BBBB' + str(len(switching_pattern)) + 's', 0x20, 3 + len(switching_pattern), 68, 0, connection, cte_types, len(switching_pattern), bytes(i for i in switching_pattern))
    def gecko_cmd_cte_transmitter_disable_cte_response(self, connection):
        return struct.pack('<4BB', 0x20, 1, 68, 1, connection)
    def gecko_cmd_cte_transmitter_start_connectionless_cte(self, adv, cte_length, cte_type, cte_count, switching_pattern):
        return struct.pack('<4BBBBBB' + str(len(switching_pattern)) + 's', 0x20, 5 + len(switching_pattern), 68, 2, adv, cte_length, cte_type, cte_count, len(switching_pattern), bytes(i for i in switching_pattern))
    def gecko_cmd_cte_transmitter_stop_connectionless_cte(self, adv):
        return struct.pack('<4BB', 0x20, 1, 68, 3, adv)
    def gecko_cmd_cte_transmitter_set_dtm_parameters(self, cte_length, cte_type, switching_pattern):
        return struct.pack('<4BBBB' + str(len(switching_pattern)) + 's', 0x20, 3 + len(switching_pattern), 68, 4, cte_length, cte_type, len(switching_pattern), bytes(i for i in switching_pattern))
    def gecko_cmd_cte_transmitter_clear_dtm_parameters(self):
        return struct.pack('<4B', 0x20, 0, 68, 5)
    def gecko_cmd_cte_receiver_configure(self, flags):
        return struct.pack('<4BB', 0x20, 1, 69, 0, flags)
    def gecko_cmd_cte_receiver_start_iq_sampling(self, connection, interval, cte_length, cte_type, slot_durations, switching_pattern):
        return struct.pack('<4BBHBBBB' + str(len(switching_pattern)) + 's', 0x20, 7 + len(switching_pattern), 69, 1, connection, interval, cte_length, cte_type, slot_durations, len(switching_pattern), bytes(i for i in switching_pattern))
    def gecko_cmd_cte_receiver_stop_iq_sampling(self, connection):
        return struct.pack('<4BB', 0x20, 1, 69, 2, connection)
    def gecko_cmd_cte_receiver_start_connectionless_iq_sampling(self, sync, slot_durations, cte_count, switching_pattern):
        return struct.pack('<4BBBBB' + str(len(switching_pattern)) + 's', 0x20, 4 + len(switching_pattern), 69, 3, sync, slot_durations, cte_count, len(switching_pattern), bytes(i for i in switching_pattern))
    def gecko_cmd_cte_receiver_stop_connectionless_iq_sampling(self, sync):
        return struct.pack('<4BB', 0x20, 1, 69, 4, sync)
    def gecko_cmd_cte_receiver_set_dtm_parameters(self, cte_length, cte_type, slot_durations, switching_pattern):
        return struct.pack('<4BBBBB' + str(len(switching_pattern)) + 's', 0x20, 4 + len(switching_pattern), 69, 5, cte_length, cte_type, slot_durations, len(switching_pattern), bytes(i for i in switching_pattern))
    def gecko_cmd_cte_receiver_clear_dtm_parameters(self):
        return struct.pack('<4B', 0x20, 0, 69, 6)
    def gecko_cmd_qualtester_configure(self, group, id, value, data):
        return struct.pack('<4BIIIB' + str(len(data)) + 's', 0x20, 13 + len(data), 254, 0, group, id, value, len(data), bytes(i for i in data))
    def gecko_cmd_user_message_to_target(self, data):
        return struct.pack('<4BB' + str(len(data)) + 's', 0x20, 1 + len(data), 255, 0, len(data), bytes(i for i in data))

    gecko_rsp_dfu_reset = BGAPIEvent()
    gecko_rsp_dfu_flash_set_address = BGAPIEvent()
    gecko_rsp_dfu_flash_upload = BGAPIEvent()
    gecko_rsp_dfu_flash_upload_finish = BGAPIEvent()
    gecko_rsp_system_hello = BGAPIEvent()
    gecko_rsp_system_reset = BGAPIEvent()
    gecko_rsp_system_get_bt_address = BGAPIEvent()
    gecko_rsp_system_set_bt_address = BGAPIEvent()
    gecko_rsp_system_set_tx_power = BGAPIEvent()
    gecko_rsp_system_get_random_data = BGAPIEvent()
    gecko_rsp_system_halt = BGAPIEvent()
    gecko_rsp_system_set_device_name = BGAPIEvent()
    gecko_rsp_system_linklayer_configure = BGAPIEvent()
    gecko_rsp_system_get_counters = BGAPIEvent()
    gecko_rsp_system_data_buffer_write = BGAPIEvent()
    gecko_rsp_system_set_identity_address = BGAPIEvent()
    gecko_rsp_system_data_buffer_clear = BGAPIEvent()
    gecko_rsp_le_gap_open = BGAPIEvent()
    gecko_rsp_le_gap_set_mode = BGAPIEvent()
    gecko_rsp_le_gap_discover = BGAPIEvent()
    gecko_rsp_le_gap_end_procedure = BGAPIEvent()
    gecko_rsp_le_gap_set_adv_parameters = BGAPIEvent()
    gecko_rsp_le_gap_set_conn_parameters = BGAPIEvent()
    gecko_rsp_le_gap_set_scan_parameters = BGAPIEvent()
    gecko_rsp_le_gap_set_adv_data = BGAPIEvent()
    gecko_rsp_le_gap_set_adv_timeout = BGAPIEvent()
    gecko_rsp_le_gap_set_conn_phy = BGAPIEvent()
    gecko_rsp_le_gap_bt5_set_mode = BGAPIEvent()
    gecko_rsp_le_gap_bt5_set_adv_parameters = BGAPIEvent()
    gecko_rsp_le_gap_bt5_set_adv_data = BGAPIEvent()
    gecko_rsp_le_gap_set_privacy_mode = BGAPIEvent()
    gecko_rsp_le_gap_set_advertise_timing = BGAPIEvent()
    gecko_rsp_le_gap_set_advertise_channel_map = BGAPIEvent()
    gecko_rsp_le_gap_set_advertise_report_scan_request = BGAPIEvent()
    gecko_rsp_le_gap_set_advertise_phy = BGAPIEvent()
    gecko_rsp_le_gap_set_advertise_configuration = BGAPIEvent()
    gecko_rsp_le_gap_clear_advertise_configuration = BGAPIEvent()
    gecko_rsp_le_gap_start_advertising = BGAPIEvent()
    gecko_rsp_le_gap_stop_advertising = BGAPIEvent()
    gecko_rsp_le_gap_set_discovery_timing = BGAPIEvent()
    gecko_rsp_le_gap_set_discovery_type = BGAPIEvent()
    gecko_rsp_le_gap_start_discovery = BGAPIEvent()
    gecko_rsp_le_gap_set_data_channel_classification = BGAPIEvent()
    gecko_rsp_le_gap_connect = BGAPIEvent()
    gecko_rsp_le_gap_set_advertise_tx_power = BGAPIEvent()
    gecko_rsp_le_gap_set_discovery_extended_scan_response = BGAPIEvent()
    gecko_rsp_le_gap_start_periodic_advertising = BGAPIEvent()
    gecko_rsp_le_gap_stop_periodic_advertising = BGAPIEvent()
    gecko_rsp_le_gap_set_long_advertising_data = BGAPIEvent()
    gecko_rsp_le_gap_enable_whitelisting = BGAPIEvent()
    gecko_rsp_le_gap_set_conn_timing_parameters = BGAPIEvent()
    gecko_rsp_le_gap_set_advertise_random_address = BGAPIEvent()
    gecko_rsp_le_gap_clear_advertise_random_address = BGAPIEvent()
    gecko_rsp_sync_open = BGAPIEvent()
    gecko_rsp_sync_close = BGAPIEvent()
    gecko_rsp_le_connection_set_parameters = BGAPIEvent()
    gecko_rsp_le_connection_get_rssi = BGAPIEvent()
    gecko_rsp_le_connection_disable_slave_latency = BGAPIEvent()
    gecko_rsp_le_connection_set_phy = BGAPIEvent()
    gecko_rsp_le_connection_close = BGAPIEvent()
    gecko_rsp_le_connection_set_timing_parameters = BGAPIEvent()
    gecko_rsp_le_connection_read_channel_map = BGAPIEvent()
    gecko_rsp_le_connection_set_preferred_phy = BGAPIEvent()
    gecko_rsp_gatt_set_max_mtu = BGAPIEvent()
    gecko_rsp_gatt_discover_primary_services = BGAPIEvent()
    gecko_rsp_gatt_discover_primary_services_by_uuid = BGAPIEvent()
    gecko_rsp_gatt_discover_characteristics = BGAPIEvent()
    gecko_rsp_gatt_discover_characteristics_by_uuid = BGAPIEvent()
    gecko_rsp_gatt_set_characteristic_notification = BGAPIEvent()
    gecko_rsp_gatt_discover_descriptors = BGAPIEvent()
    gecko_rsp_gatt_read_characteristic_value = BGAPIEvent()
    gecko_rsp_gatt_read_characteristic_value_by_uuid = BGAPIEvent()
    gecko_rsp_gatt_write_characteristic_value = BGAPIEvent()
    gecko_rsp_gatt_write_characteristic_value_without_response = BGAPIEvent()
    gecko_rsp_gatt_prepare_characteristic_value_write = BGAPIEvent()
    gecko_rsp_gatt_execute_characteristic_value_write = BGAPIEvent()
    gecko_rsp_gatt_send_characteristic_confirmation = BGAPIEvent()
    gecko_rsp_gatt_read_descriptor_value = BGAPIEvent()
    gecko_rsp_gatt_write_descriptor_value = BGAPIEvent()
    gecko_rsp_gatt_find_included_services = BGAPIEvent()
    gecko_rsp_gatt_read_multiple_characteristic_values = BGAPIEvent()
    gecko_rsp_gatt_read_characteristic_value_from_offset = BGAPIEvent()
    gecko_rsp_gatt_prepare_characteristic_value_reliable_write = BGAPIEvent()
    gecko_rsp_gatt_server_read_attribute_value = BGAPIEvent()
    gecko_rsp_gatt_server_read_attribute_type = BGAPIEvent()
    gecko_rsp_gatt_server_write_attribute_value = BGAPIEvent()
    gecko_rsp_gatt_server_send_user_read_response = BGAPIEvent()
    gecko_rsp_gatt_server_send_user_write_response = BGAPIEvent()
    gecko_rsp_gatt_server_send_characteristic_notification = BGAPIEvent()
    gecko_rsp_gatt_server_find_attribute = BGAPIEvent()
    gecko_rsp_gatt_server_set_capabilities = BGAPIEvent()
    gecko_rsp_gatt_server_find_primary_service = BGAPIEvent()
    gecko_rsp_gatt_server_set_max_mtu = BGAPIEvent()
    gecko_rsp_gatt_server_get_mtu = BGAPIEvent()
    gecko_rsp_gatt_server_enable_capabilities = BGAPIEvent()
    gecko_rsp_gatt_server_disable_capabilities = BGAPIEvent()
    gecko_rsp_gatt_server_get_enabled_capabilities = BGAPIEvent()
    gecko_rsp_hardware_set_soft_timer = BGAPIEvent()
    gecko_rsp_hardware_get_time = BGAPIEvent()
    gecko_rsp_hardware_set_lazy_soft_timer = BGAPIEvent()
    gecko_rsp_flash_ps_erase_all = BGAPIEvent()
    gecko_rsp_flash_ps_save = BGAPIEvent()
    gecko_rsp_flash_ps_load = BGAPIEvent()
    gecko_rsp_flash_ps_erase = BGAPIEvent()
    gecko_rsp_test_dtm_tx = BGAPIEvent()
    gecko_rsp_test_dtm_rx = BGAPIEvent()
    gecko_rsp_test_dtm_end = BGAPIEvent()
    gecko_rsp_test_debug_command = BGAPIEvent()
    gecko_rsp_test_debug_counter = BGAPIEvent()
    gecko_rsp_sm_set_bondable_mode = BGAPIEvent()
    gecko_rsp_sm_configure = BGAPIEvent()
    gecko_rsp_sm_store_bonding_configuration = BGAPIEvent()
    gecko_rsp_sm_increase_security = BGAPIEvent()
    gecko_rsp_sm_delete_bonding = BGAPIEvent()
    gecko_rsp_sm_delete_bondings = BGAPIEvent()
    gecko_rsp_sm_enter_passkey = BGAPIEvent()
    gecko_rsp_sm_passkey_confirm = BGAPIEvent()
    gecko_rsp_sm_set_oob_data = BGAPIEvent()
    gecko_rsp_sm_list_all_bondings = BGAPIEvent()
    gecko_rsp_sm_bonding_confirm = BGAPIEvent()
    gecko_rsp_sm_set_debug_mode = BGAPIEvent()
    gecko_rsp_sm_set_passkey = BGAPIEvent()
    gecko_rsp_sm_use_sc_oob = BGAPIEvent()
    gecko_rsp_sm_set_sc_remote_oob_data = BGAPIEvent()
    gecko_rsp_sm_add_to_whitelist = BGAPIEvent()
    gecko_rsp_sm_set_minimum_key_size = BGAPIEvent()
    gecko_rsp_homekit_configure = BGAPIEvent()
    gecko_rsp_homekit_advertise = BGAPIEvent()
    gecko_rsp_homekit_delete_pairings = BGAPIEvent()
    gecko_rsp_homekit_check_authcp = BGAPIEvent()
    gecko_rsp_homekit_get_pairing_id = BGAPIEvent()
    gecko_rsp_homekit_send_write_response = BGAPIEvent()
    gecko_rsp_homekit_send_read_response = BGAPIEvent()
    gecko_rsp_homekit_gsn_action = BGAPIEvent()
    gecko_rsp_homekit_event_notification = BGAPIEvent()
    gecko_rsp_homekit_broadcast_action = BGAPIEvent()
    gecko_rsp_homekit_configure_product_data = BGAPIEvent()
    gecko_rsp_coex_set_options = BGAPIEvent()
    gecko_rsp_coex_get_counters = BGAPIEvent()
    gecko_rsp_coex_set_parameters = BGAPIEvent()
    gecko_rsp_coex_set_directional_priority_pulse = BGAPIEvent()
    gecko_rsp_l2cap_coc_send_connection_request = BGAPIEvent()
    gecko_rsp_l2cap_coc_send_connection_response = BGAPIEvent()
    gecko_rsp_l2cap_coc_send_le_flow_control_credit = BGAPIEvent()
    gecko_rsp_l2cap_coc_send_disconnection_request = BGAPIEvent()
    gecko_rsp_l2cap_coc_send_data = BGAPIEvent()
    gecko_rsp_cte_transmitter_enable_cte_response = BGAPIEvent()
    gecko_rsp_cte_transmitter_disable_cte_response = BGAPIEvent()
    gecko_rsp_cte_transmitter_start_connectionless_cte = BGAPIEvent()
    gecko_rsp_cte_transmitter_stop_connectionless_cte = BGAPIEvent()
    gecko_rsp_cte_transmitter_set_dtm_parameters = BGAPIEvent()
    gecko_rsp_cte_transmitter_clear_dtm_parameters = BGAPIEvent()
    gecko_rsp_cte_receiver_configure = BGAPIEvent()
    gecko_rsp_cte_receiver_start_iq_sampling = BGAPIEvent()
    gecko_rsp_cte_receiver_stop_iq_sampling = BGAPIEvent()
    gecko_rsp_cte_receiver_start_connectionless_iq_sampling = BGAPIEvent()
    gecko_rsp_cte_receiver_stop_connectionless_iq_sampling = BGAPIEvent()
    gecko_rsp_cte_receiver_set_dtm_parameters = BGAPIEvent()
    gecko_rsp_cte_receiver_clear_dtm_parameters = BGAPIEvent()
    gecko_rsp_qualtester_configure = BGAPIEvent()
    gecko_rsp_user_message_to_target = BGAPIEvent()

    gecko_evt_dfu_boot = BGAPIEvent()
    gecko_evt_dfu_boot_failure = BGAPIEvent()
    gecko_evt_system_boot = BGAPIEvent()
    gecko_evt_system_external_signal = BGAPIEvent()
    gecko_evt_system_awake = BGAPIEvent()
    gecko_evt_system_hardware_error = BGAPIEvent()
    gecko_evt_system_error = BGAPIEvent()
    gecko_evt_le_gap_scan_response = BGAPIEvent()
    gecko_evt_le_gap_adv_timeout = BGAPIEvent()
    gecko_evt_le_gap_scan_request = BGAPIEvent()
    gecko_evt_le_gap_extended_scan_response = BGAPIEvent()
    gecko_evt_le_gap_periodic_advertising_status = BGAPIEvent()
    gecko_evt_sync_opened = BGAPIEvent()
    gecko_evt_sync_closed = BGAPIEvent()
    gecko_evt_sync_data = BGAPIEvent()
    gecko_evt_le_connection_opened = BGAPIEvent()
    gecko_evt_le_connection_closed = BGAPIEvent()
    gecko_evt_le_connection_parameters = BGAPIEvent()
    gecko_evt_le_connection_rssi = BGAPIEvent()
    gecko_evt_le_connection_phy_status = BGAPIEvent()
    gecko_evt_gatt_mtu_exchanged = BGAPIEvent()
    gecko_evt_gatt_service = BGAPIEvent()
    gecko_evt_gatt_characteristic = BGAPIEvent()
    gecko_evt_gatt_descriptor = BGAPIEvent()
    gecko_evt_gatt_characteristic_value = BGAPIEvent()
    gecko_evt_gatt_descriptor_value = BGAPIEvent()
    gecko_evt_gatt_procedure_completed = BGAPIEvent()
    gecko_evt_gatt_server_attribute_value = BGAPIEvent()
    gecko_evt_gatt_server_user_read_request = BGAPIEvent()
    gecko_evt_gatt_server_user_write_request = BGAPIEvent()
    gecko_evt_gatt_server_characteristic_status = BGAPIEvent()
    gecko_evt_gatt_server_execute_write_completed = BGAPIEvent()
    gecko_evt_hardware_soft_timer = BGAPIEvent()
    gecko_evt_test_dtm_completed = BGAPIEvent()
    gecko_evt_sm_passkey_display = BGAPIEvent()
    gecko_evt_sm_passkey_request = BGAPIEvent()
    gecko_evt_sm_confirm_passkey = BGAPIEvent()
    gecko_evt_sm_bonded = BGAPIEvent()
    gecko_evt_sm_bonding_failed = BGAPIEvent()
    gecko_evt_sm_list_bonding_entry = BGAPIEvent()
    gecko_evt_sm_list_all_bondings_complete = BGAPIEvent()
    gecko_evt_sm_confirm_bonding = BGAPIEvent()
    gecko_evt_homekit_setupcode_display = BGAPIEvent()
    gecko_evt_homekit_paired = BGAPIEvent()
    gecko_evt_homekit_pair_verified = BGAPIEvent()
    gecko_evt_homekit_connection_opened = BGAPIEvent()
    gecko_evt_homekit_connection_closed = BGAPIEvent()
    gecko_evt_homekit_identify = BGAPIEvent()
    gecko_evt_homekit_write_request = BGAPIEvent()
    gecko_evt_homekit_read_request = BGAPIEvent()
    gecko_evt_homekit_disconnection_required = BGAPIEvent()
    gecko_evt_homekit_pairing_removed = BGAPIEvent()
    gecko_evt_homekit_setuppayload_display = BGAPIEvent()
    gecko_evt_l2cap_coc_connection_request = BGAPIEvent()
    gecko_evt_l2cap_coc_connection_response = BGAPIEvent()
    gecko_evt_l2cap_coc_le_flow_control_credit = BGAPIEvent()
    gecko_evt_l2cap_coc_channel_disconnected = BGAPIEvent()
    gecko_evt_l2cap_coc_data = BGAPIEvent()
    gecko_evt_l2cap_command_rejected = BGAPIEvent()
    gecko_evt_cte_receiver_iq_report = BGAPIEvent()
    gecko_evt_qualtester_state_changed = BGAPIEvent()
    gecko_evt_user_message_to_host = BGAPIEvent()

    on_busy = BGAPIEvent()
    on_idle = BGAPIEvent()
    on_timeout = BGAPIEvent()
    on_before_tx_command = BGAPIEvent()
    on_tx_command_complete = BGAPIEvent()

    bgapi_rx_buffer = b""
    bgapi_rx_expected_length = 0
    busy = False
    debug = False

    def send_command(self, ser, packet):
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
        if len(self.bgapi_rx_buffer) == 0 and (b == 0xa0 or b == 0x20):
            self.bgapi_rx_buffer+=bytes([b])
        elif len(self.bgapi_rx_buffer) == 1:
            self.bgapi_rx_buffer+=bytes([b])
            self.bgapi_rx_expected_length = 4 + (self.bgapi_rx_buffer[0] & 0x07) + self.bgapi_rx_buffer[1]
        elif len(self.bgapi_rx_buffer) > 1:
            self.bgapi_rx_buffer+=bytes([b])

        """
        BGAPI packet structure (as of 2020-06-12):
            Byte 0:
                  [7] - 1 bit, Message Type (MT)         0 = Command/Response, 1 = Event
                [6:3] - 4 bits, Technology Type (TT)     0010 - Blue Gecko
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
            if packet_type & 0xa0 == 0x20:
                # 0x20 = Blue Gecko response packet
                if packet_class == 0:
                    if packet_command == 0: # gecko_rsp_dfu_reset
                        self.gecko_rsp_dfu_reset({  })
                        self.busy = False
                        self.on_idle()
                    elif packet_command == 1: # gecko_rsp_dfu_flash_set_address
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_dfu_flash_set_address({ 'result': result })
                    elif packet_command == 2: # gecko_rsp_dfu_flash_upload
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_dfu_flash_upload({ 'result': result })
                    elif packet_command == 3: # gecko_rsp_dfu_flash_upload_finish
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_dfu_flash_upload_finish({ 'result': result })
                elif packet_class == 1:
                    if packet_command == 0: # gecko_rsp_system_hello
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_system_hello({ 'result': result })
                    elif packet_command == 1: # gecko_rsp_system_reset
                        self.gecko_rsp_system_reset({  })
                    elif packet_command == 3: # gecko_rsp_system_get_bt_address
                        address = struct.unpack('<6s', self.bgapi_rx_payload[:6])[0]
                        address = address
                        self.gecko_rsp_system_get_bt_address({ 'address': address })
                    elif packet_command == 4: # gecko_rsp_system_set_bt_address
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_system_set_bt_address({ 'result': result })
                    elif packet_command == 10: # gecko_rsp_system_set_tx_power
                        set_power = struct.unpack('<h', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_system_set_tx_power({ 'set_power': set_power })
                    elif packet_command == 11: # gecko_rsp_system_get_random_data
                        result, data_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        data_data = self.bgapi_rx_payload[3:]
                        self.gecko_rsp_system_get_random_data({ 'result': result, 'data': data_data })
                    elif packet_command == 12: # gecko_rsp_system_halt
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_system_halt({ 'result': result })
                    elif packet_command == 13: # gecko_rsp_system_set_device_name
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_system_set_device_name({ 'result': result })
                    elif packet_command == 14: # gecko_rsp_system_linklayer_configure
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_system_linklayer_configure({ 'result': result })
                    elif packet_command == 15: # gecko_rsp_system_get_counters
                        result, tx_packets, rx_packets, crc_errors, failures = struct.unpack('<HHHHH', self.bgapi_rx_payload[:10])
                        self.gecko_rsp_system_get_counters({ 'result': result, 'tx_packets': tx_packets, 'rx_packets': rx_packets, 'crc_errors': crc_errors, 'failures': failures })
                    elif packet_command == 18: # gecko_rsp_system_data_buffer_write
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_system_data_buffer_write({ 'result': result })
                    elif packet_command == 19: # gecko_rsp_system_set_identity_address
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_system_set_identity_address({ 'result': result })
                    elif packet_command == 20: # gecko_rsp_system_data_buffer_clear
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_system_data_buffer_clear({ 'result': result })
                elif packet_class == 3:
                    if packet_command == 0: # gecko_rsp_le_gap_open
                        result, connection = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.gecko_rsp_le_gap_open({ 'result': result, 'connection': connection })
                    elif packet_command == 1: # gecko_rsp_le_gap_set_mode
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_mode({ 'result': result })
                    elif packet_command == 2: # gecko_rsp_le_gap_discover
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_discover({ 'result': result })
                    elif packet_command == 3: # gecko_rsp_le_gap_end_procedure
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_end_procedure({ 'result': result })
                    elif packet_command == 4: # gecko_rsp_le_gap_set_adv_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_adv_parameters({ 'result': result })
                    elif packet_command == 5: # gecko_rsp_le_gap_set_conn_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_conn_parameters({ 'result': result })
                    elif packet_command == 6: # gecko_rsp_le_gap_set_scan_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_scan_parameters({ 'result': result })
                    elif packet_command == 7: # gecko_rsp_le_gap_set_adv_data
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_adv_data({ 'result': result })
                    elif packet_command == 8: # gecko_rsp_le_gap_set_adv_timeout
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_adv_timeout({ 'result': result })
                    elif packet_command == 9: # gecko_rsp_le_gap_set_conn_phy
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_conn_phy({ 'result': result })
                    elif packet_command == 10: # gecko_rsp_le_gap_bt5_set_mode
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_bt5_set_mode({ 'result': result })
                    elif packet_command == 11: # gecko_rsp_le_gap_bt5_set_adv_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_bt5_set_adv_parameters({ 'result': result })
                    elif packet_command == 12: # gecko_rsp_le_gap_bt5_set_adv_data
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_bt5_set_adv_data({ 'result': result })
                    elif packet_command == 13: # gecko_rsp_le_gap_set_privacy_mode
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_privacy_mode({ 'result': result })
                    elif packet_command == 14: # gecko_rsp_le_gap_set_advertise_timing
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_advertise_timing({ 'result': result })
                    elif packet_command == 15: # gecko_rsp_le_gap_set_advertise_channel_map
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_advertise_channel_map({ 'result': result })
                    elif packet_command == 16: # gecko_rsp_le_gap_set_advertise_report_scan_request
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_advertise_report_scan_request({ 'result': result })
                    elif packet_command == 17: # gecko_rsp_le_gap_set_advertise_phy
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_advertise_phy({ 'result': result })
                    elif packet_command == 18: # gecko_rsp_le_gap_set_advertise_configuration
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_advertise_configuration({ 'result': result })
                    elif packet_command == 19: # gecko_rsp_le_gap_clear_advertise_configuration
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_clear_advertise_configuration({ 'result': result })
                    elif packet_command == 20: # gecko_rsp_le_gap_start_advertising
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_start_advertising({ 'result': result })
                    elif packet_command == 21: # gecko_rsp_le_gap_stop_advertising
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_stop_advertising({ 'result': result })
                    elif packet_command == 22: # gecko_rsp_le_gap_set_discovery_timing
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_discovery_timing({ 'result': result })
                    elif packet_command == 23: # gecko_rsp_le_gap_set_discovery_type
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_discovery_type({ 'result': result })
                    elif packet_command == 24: # gecko_rsp_le_gap_start_discovery
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_start_discovery({ 'result': result })
                    elif packet_command == 25: # gecko_rsp_le_gap_set_data_channel_classification
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_data_channel_classification({ 'result': result })
                    elif packet_command == 26: # gecko_rsp_le_gap_connect
                        result, connection = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.gecko_rsp_le_gap_connect({ 'result': result, 'connection': connection })
                    elif packet_command == 27: # gecko_rsp_le_gap_set_advertise_tx_power
                        result, set_power = struct.unpack('<Hh', self.bgapi_rx_payload[:4])
                        self.gecko_rsp_le_gap_set_advertise_tx_power({ 'result': result, 'set_power': set_power })
                    elif packet_command == 28: # gecko_rsp_le_gap_set_discovery_extended_scan_response
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_discovery_extended_scan_response({ 'result': result })
                    elif packet_command == 29: # gecko_rsp_le_gap_start_periodic_advertising
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_start_periodic_advertising({ 'result': result })
                    elif packet_command == 31: # gecko_rsp_le_gap_stop_periodic_advertising
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_stop_periodic_advertising({ 'result': result })
                    elif packet_command == 32: # gecko_rsp_le_gap_set_long_advertising_data
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_long_advertising_data({ 'result': result })
                    elif packet_command == 33: # gecko_rsp_le_gap_enable_whitelisting
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_enable_whitelisting({ 'result': result })
                    elif packet_command == 34: # gecko_rsp_le_gap_set_conn_timing_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_set_conn_timing_parameters({ 'result': result })
                    elif packet_command == 37: # gecko_rsp_le_gap_set_advertise_random_address
                        result, address_out = struct.unpack('<H6s', self.bgapi_rx_payload[:8])
                        address_out = address_out
                        self.gecko_rsp_le_gap_set_advertise_random_address({ 'result': result, 'address_out': address_out })
                    elif packet_command == 38: # gecko_rsp_le_gap_clear_advertise_random_address
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_gap_clear_advertise_random_address({ 'result': result })
                elif packet_class == 66:
                    if packet_command == 0: # gecko_rsp_sync_open
                        result, sync = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.gecko_rsp_sync_open({ 'result': result, 'sync': sync })
                    elif packet_command == 1: # gecko_rsp_sync_close
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sync_close({ 'result': result })
                elif packet_class == 8:
                    if packet_command == 0: # gecko_rsp_le_connection_set_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_connection_set_parameters({ 'result': result })
                    elif packet_command == 1: # gecko_rsp_le_connection_get_rssi
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_connection_get_rssi({ 'result': result })
                    elif packet_command == 2: # gecko_rsp_le_connection_disable_slave_latency
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_connection_disable_slave_latency({ 'result': result })
                    elif packet_command == 3: # gecko_rsp_le_connection_set_phy
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_connection_set_phy({ 'result': result })
                    elif packet_command == 4: # gecko_rsp_le_connection_close
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_connection_close({ 'result': result })
                    elif packet_command == 5: # gecko_rsp_le_connection_set_timing_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_connection_set_timing_parameters({ 'result': result })
                    elif packet_command == 6: # gecko_rsp_le_connection_read_channel_map
                        result, channel_map_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        channel_map_data = self.bgapi_rx_payload[3:]
                        self.gecko_rsp_le_connection_read_channel_map({ 'result': result, 'channel_map': channel_map_data })
                    elif packet_command == 7: # gecko_rsp_le_connection_set_preferred_phy
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_le_connection_set_preferred_phy({ 'result': result })
                elif packet_class == 9:
                    if packet_command == 0: # gecko_rsp_gatt_set_max_mtu
                        result, max_mtu = struct.unpack('<HH', self.bgapi_rx_payload[:4])
                        self.gecko_rsp_gatt_set_max_mtu({ 'result': result, 'max_mtu': max_mtu })
                    elif packet_command == 1: # gecko_rsp_gatt_discover_primary_services
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_discover_primary_services({ 'result': result })
                    elif packet_command == 2: # gecko_rsp_gatt_discover_primary_services_by_uuid
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_discover_primary_services_by_uuid({ 'result': result })
                    elif packet_command == 3: # gecko_rsp_gatt_discover_characteristics
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_discover_characteristics({ 'result': result })
                    elif packet_command == 4: # gecko_rsp_gatt_discover_characteristics_by_uuid
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_discover_characteristics_by_uuid({ 'result': result })
                    elif packet_command == 5: # gecko_rsp_gatt_set_characteristic_notification
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_set_characteristic_notification({ 'result': result })
                    elif packet_command == 6: # gecko_rsp_gatt_discover_descriptors
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_discover_descriptors({ 'result': result })
                    elif packet_command == 7: # gecko_rsp_gatt_read_characteristic_value
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_read_characteristic_value({ 'result': result })
                    elif packet_command == 8: # gecko_rsp_gatt_read_characteristic_value_by_uuid
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_read_characteristic_value_by_uuid({ 'result': result })
                    elif packet_command == 9: # gecko_rsp_gatt_write_characteristic_value
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_write_characteristic_value({ 'result': result })
                    elif packet_command == 10: # gecko_rsp_gatt_write_characteristic_value_without_response
                        result, sent_len = struct.unpack('<HH', self.bgapi_rx_payload[:4])
                        self.gecko_rsp_gatt_write_characteristic_value_without_response({ 'result': result, 'sent_len': sent_len })
                    elif packet_command == 11: # gecko_rsp_gatt_prepare_characteristic_value_write
                        result, sent_len = struct.unpack('<HH', self.bgapi_rx_payload[:4])
                        self.gecko_rsp_gatt_prepare_characteristic_value_write({ 'result': result, 'sent_len': sent_len })
                    elif packet_command == 12: # gecko_rsp_gatt_execute_characteristic_value_write
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_execute_characteristic_value_write({ 'result': result })
                    elif packet_command == 13: # gecko_rsp_gatt_send_characteristic_confirmation
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_send_characteristic_confirmation({ 'result': result })
                    elif packet_command == 14: # gecko_rsp_gatt_read_descriptor_value
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_read_descriptor_value({ 'result': result })
                    elif packet_command == 15: # gecko_rsp_gatt_write_descriptor_value
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_write_descriptor_value({ 'result': result })
                    elif packet_command == 16: # gecko_rsp_gatt_find_included_services
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_find_included_services({ 'result': result })
                    elif packet_command == 17: # gecko_rsp_gatt_read_multiple_characteristic_values
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_read_multiple_characteristic_values({ 'result': result })
                    elif packet_command == 18: # gecko_rsp_gatt_read_characteristic_value_from_offset
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_read_characteristic_value_from_offset({ 'result': result })
                    elif packet_command == 19: # gecko_rsp_gatt_prepare_characteristic_value_reliable_write
                        result, sent_len = struct.unpack('<HH', self.bgapi_rx_payload[:4])
                        self.gecko_rsp_gatt_prepare_characteristic_value_reliable_write({ 'result': result, 'sent_len': sent_len })
                elif packet_class == 10:
                    if packet_command == 0: # gecko_rsp_gatt_server_read_attribute_value
                        result, value_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        value_data = self.bgapi_rx_payload[3:]
                        self.gecko_rsp_gatt_server_read_attribute_value({ 'result': result, 'value': value_data })
                    elif packet_command == 1: # gecko_rsp_gatt_server_read_attribute_type
                        result, type_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        type_data = self.bgapi_rx_payload[3:]
                        self.gecko_rsp_gatt_server_read_attribute_type({ 'result': result, 'type': type_data })
                    elif packet_command == 2: # gecko_rsp_gatt_server_write_attribute_value
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_server_write_attribute_value({ 'result': result })
                    elif packet_command == 3: # gecko_rsp_gatt_server_send_user_read_response
                        result, sent_len = struct.unpack('<HH', self.bgapi_rx_payload[:4])
                        self.gecko_rsp_gatt_server_send_user_read_response({ 'result': result, 'sent_len': sent_len })
                    elif packet_command == 4: # gecko_rsp_gatt_server_send_user_write_response
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_server_send_user_write_response({ 'result': result })
                    elif packet_command == 5: # gecko_rsp_gatt_server_send_characteristic_notification
                        result, sent_len = struct.unpack('<HH', self.bgapi_rx_payload[:4])
                        self.gecko_rsp_gatt_server_send_characteristic_notification({ 'result': result, 'sent_len': sent_len })
                    elif packet_command == 6: # gecko_rsp_gatt_server_find_attribute
                        result, attribute = struct.unpack('<HH', self.bgapi_rx_payload[:4])
                        self.gecko_rsp_gatt_server_find_attribute({ 'result': result, 'attribute': attribute })
                    elif packet_command == 8: # gecko_rsp_gatt_server_set_capabilities
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_server_set_capabilities({ 'result': result })
                    elif packet_command == 9: # gecko_rsp_gatt_server_find_primary_service
                        result, start, end = struct.unpack('<HHH', self.bgapi_rx_payload[:6])
                        self.gecko_rsp_gatt_server_find_primary_service({ 'result': result, 'start': start, 'end': end })
                    elif packet_command == 10: # gecko_rsp_gatt_server_set_max_mtu
                        result, max_mtu = struct.unpack('<HH', self.bgapi_rx_payload[:4])
                        self.gecko_rsp_gatt_server_set_max_mtu({ 'result': result, 'max_mtu': max_mtu })
                    elif packet_command == 11: # gecko_rsp_gatt_server_get_mtu
                        result, mtu = struct.unpack('<HH', self.bgapi_rx_payload[:4])
                        self.gecko_rsp_gatt_server_get_mtu({ 'result': result, 'mtu': mtu })
                    elif packet_command == 12: # gecko_rsp_gatt_server_enable_capabilities
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_server_enable_capabilities({ 'result': result })
                    elif packet_command == 13: # gecko_rsp_gatt_server_disable_capabilities
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_gatt_server_disable_capabilities({ 'result': result })
                    elif packet_command == 14: # gecko_rsp_gatt_server_get_enabled_capabilities
                        result, caps = struct.unpack('<HI', self.bgapi_rx_payload[:6])
                        self.gecko_rsp_gatt_server_get_enabled_capabilities({ 'result': result, 'caps': caps })
                elif packet_class == 12:
                    if packet_command == 0: # gecko_rsp_hardware_set_soft_timer
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_hardware_set_soft_timer({ 'result': result })
                    elif packet_command == 11: # gecko_rsp_hardware_get_time
                        seconds, ticks = struct.unpack('<IH', self.bgapi_rx_payload[:6])
                        self.gecko_rsp_hardware_get_time({ 'seconds': seconds, 'ticks': ticks })
                    elif packet_command == 12: # gecko_rsp_hardware_set_lazy_soft_timer
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_hardware_set_lazy_soft_timer({ 'result': result })
                elif packet_class == 13:
                    if packet_command == 1: # gecko_rsp_flash_ps_erase_all
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_flash_ps_erase_all({ 'result': result })
                    elif packet_command == 2: # gecko_rsp_flash_ps_save
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_flash_ps_save({ 'result': result })
                    elif packet_command == 3: # gecko_rsp_flash_ps_load
                        result, value_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        value_data = self.bgapi_rx_payload[3:]
                        self.gecko_rsp_flash_ps_load({ 'result': result, 'value': value_data })
                    elif packet_command == 4: # gecko_rsp_flash_ps_erase
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_flash_ps_erase({ 'result': result })
                elif packet_class == 14:
                    if packet_command == 0: # gecko_rsp_test_dtm_tx
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_test_dtm_tx({ 'result': result })
                    elif packet_command == 1: # gecko_rsp_test_dtm_rx
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_test_dtm_rx({ 'result': result })
                    elif packet_command == 2: # gecko_rsp_test_dtm_end
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_test_dtm_end({ 'result': result })
                    elif packet_command == 7: # gecko_rsp_test_debug_command
                        result, id, debugdata_len = struct.unpack('<HBB', self.bgapi_rx_payload[:4])
                        debugdata_data = self.bgapi_rx_payload[4:]
                        self.gecko_rsp_test_debug_command({ 'result': result, 'id': id, 'debugdata': debugdata_data })
                    elif packet_command == 12: # gecko_rsp_test_debug_counter
                        result, value = struct.unpack('<HI', self.bgapi_rx_payload[:6])
                        self.gecko_rsp_test_debug_counter({ 'result': result, 'value': value })
                elif packet_class == 15:
                    if packet_command == 0: # gecko_rsp_sm_set_bondable_mode
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_set_bondable_mode({ 'result': result })
                    elif packet_command == 1: # gecko_rsp_sm_configure
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_configure({ 'result': result })
                    elif packet_command == 2: # gecko_rsp_sm_store_bonding_configuration
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_store_bonding_configuration({ 'result': result })
                    elif packet_command == 4: # gecko_rsp_sm_increase_security
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_increase_security({ 'result': result })
                    elif packet_command == 6: # gecko_rsp_sm_delete_bonding
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_delete_bonding({ 'result': result })
                    elif packet_command == 7: # gecko_rsp_sm_delete_bondings
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_delete_bondings({ 'result': result })
                    elif packet_command == 8: # gecko_rsp_sm_enter_passkey
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_enter_passkey({ 'result': result })
                    elif packet_command == 9: # gecko_rsp_sm_passkey_confirm
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_passkey_confirm({ 'result': result })
                    elif packet_command == 10: # gecko_rsp_sm_set_oob_data
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_set_oob_data({ 'result': result })
                    elif packet_command == 11: # gecko_rsp_sm_list_all_bondings
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_list_all_bondings({ 'result': result })
                    elif packet_command == 14: # gecko_rsp_sm_bonding_confirm
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_bonding_confirm({ 'result': result })
                    elif packet_command == 15: # gecko_rsp_sm_set_debug_mode
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_set_debug_mode({ 'result': result })
                    elif packet_command == 16: # gecko_rsp_sm_set_passkey
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_set_passkey({ 'result': result })
                    elif packet_command == 17: # gecko_rsp_sm_use_sc_oob
                        result, oob_data_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        oob_data_data = self.bgapi_rx_payload[3:]
                        self.gecko_rsp_sm_use_sc_oob({ 'result': result, 'oob_data': oob_data_data })
                    elif packet_command == 18: # gecko_rsp_sm_set_sc_remote_oob_data
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_set_sc_remote_oob_data({ 'result': result })
                    elif packet_command == 19: # gecko_rsp_sm_add_to_whitelist
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_add_to_whitelist({ 'result': result })
                    elif packet_command == 20: # gecko_rsp_sm_set_minimum_key_size
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_sm_set_minimum_key_size({ 'result': result })
                elif packet_class == 19:
                    if packet_command == 0: # gecko_rsp_homekit_configure
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_homekit_configure({ 'result': result })
                    elif packet_command == 1: # gecko_rsp_homekit_advertise
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_homekit_advertise({ 'result': result })
                    elif packet_command == 2: # gecko_rsp_homekit_delete_pairings
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_homekit_delete_pairings({ 'result': result })
                    elif packet_command == 3: # gecko_rsp_homekit_check_authcp
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_homekit_check_authcp({ 'result': result })
                    elif packet_command == 4: # gecko_rsp_homekit_get_pairing_id
                        result, pairing_id_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        pairing_id_data = self.bgapi_rx_payload[3:]
                        self.gecko_rsp_homekit_get_pairing_id({ 'result': result, 'pairing_id': pairing_id_data })
                    elif packet_command == 5: # gecko_rsp_homekit_send_write_response
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_homekit_send_write_response({ 'result': result })
                    elif packet_command == 6: # gecko_rsp_homekit_send_read_response
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_homekit_send_read_response({ 'result': result })
                    elif packet_command == 7: # gecko_rsp_homekit_gsn_action
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_homekit_gsn_action({ 'result': result })
                    elif packet_command == 8: # gecko_rsp_homekit_event_notification
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_homekit_event_notification({ 'result': result })
                    elif packet_command == 9: # gecko_rsp_homekit_broadcast_action
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_homekit_broadcast_action({ 'result': result })
                    elif packet_command == 10: # gecko_rsp_homekit_configure_product_data
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_homekit_configure_product_data({ 'result': result })
                elif packet_class == 32:
                    if packet_command == 0: # gecko_rsp_coex_set_options
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_coex_set_options({ 'result': result })
                    elif packet_command == 1: # gecko_rsp_coex_get_counters
                        result, counters_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        counters_data = self.bgapi_rx_payload[3:]
                        self.gecko_rsp_coex_get_counters({ 'result': result, 'counters': counters_data })
                    elif packet_command == 2: # gecko_rsp_coex_set_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_coex_set_parameters({ 'result': result })
                    elif packet_command == 3: # gecko_rsp_coex_set_directional_priority_pulse
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_coex_set_directional_priority_pulse({ 'result': result })
                elif packet_class == 67:
                    if packet_command == 1: # gecko_rsp_l2cap_coc_send_connection_request
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_l2cap_coc_send_connection_request({ 'result': result })
                    elif packet_command == 2: # gecko_rsp_l2cap_coc_send_connection_response
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_l2cap_coc_send_connection_response({ 'result': result })
                    elif packet_command == 3: # gecko_rsp_l2cap_coc_send_le_flow_control_credit
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_l2cap_coc_send_le_flow_control_credit({ 'result': result })
                    elif packet_command == 4: # gecko_rsp_l2cap_coc_send_disconnection_request
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_l2cap_coc_send_disconnection_request({ 'result': result })
                    elif packet_command == 5: # gecko_rsp_l2cap_coc_send_data
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_l2cap_coc_send_data({ 'result': result })
                elif packet_class == 68:
                    if packet_command == 0: # gecko_rsp_cte_transmitter_enable_cte_response
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_cte_transmitter_enable_cte_response({ 'result': result })
                    elif packet_command == 1: # gecko_rsp_cte_transmitter_disable_cte_response
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_cte_transmitter_disable_cte_response({ 'result': result })
                    elif packet_command == 2: # gecko_rsp_cte_transmitter_start_connectionless_cte
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_cte_transmitter_start_connectionless_cte({ 'result': result })
                    elif packet_command == 3: # gecko_rsp_cte_transmitter_stop_connectionless_cte
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_cte_transmitter_stop_connectionless_cte({ 'result': result })
                    elif packet_command == 4: # gecko_rsp_cte_transmitter_set_dtm_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_cte_transmitter_set_dtm_parameters({ 'result': result })
                    elif packet_command == 5: # gecko_rsp_cte_transmitter_clear_dtm_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_cte_transmitter_clear_dtm_parameters({ 'result': result })
                elif packet_class == 69:
                    if packet_command == 0: # gecko_rsp_cte_receiver_configure
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_cte_receiver_configure({ 'result': result })
                    elif packet_command == 1: # gecko_rsp_cte_receiver_start_iq_sampling
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_cte_receiver_start_iq_sampling({ 'result': result })
                    elif packet_command == 2: # gecko_rsp_cte_receiver_stop_iq_sampling
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_cte_receiver_stop_iq_sampling({ 'result': result })
                    elif packet_command == 3: # gecko_rsp_cte_receiver_start_connectionless_iq_sampling
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_cte_receiver_start_connectionless_iq_sampling({ 'result': result })
                    elif packet_command == 4: # gecko_rsp_cte_receiver_stop_connectionless_iq_sampling
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_cte_receiver_stop_connectionless_iq_sampling({ 'result': result })
                    elif packet_command == 5: # gecko_rsp_cte_receiver_set_dtm_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_cte_receiver_set_dtm_parameters({ 'result': result })
                    elif packet_command == 6: # gecko_rsp_cte_receiver_clear_dtm_parameters
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_cte_receiver_clear_dtm_parameters({ 'result': result })
                elif packet_class == 254:
                    if packet_command == 0: # gecko_rsp_qualtester_configure
                        result = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_rsp_qualtester_configure({ 'result': result })
                elif packet_class == 255:
                    if packet_command == 0: # gecko_rsp_user_message_to_target
                        result, data_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        data_data = self.bgapi_rx_payload[3:]
                        self.gecko_rsp_user_message_to_target({ 'result': result, 'data': data_data })
                self.busy = False
                self.on_idle()
            elif packet_type & 0xa0 == 0xa0:
                # 0xa0 = Blue Gecko event packet
                if packet_class == 0:
                    if packet_command == 0: # gecko_evt_dfu_boot
                        version = struct.unpack('<I', self.bgapi_rx_payload[:4])[0]
                        self.gecko_evt_dfu_boot({ 'version': version })
                        self.busy = False
                        self.on_idle()
                    elif packet_command == 1: # gecko_evt_dfu_boot_failure
                        reason = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_evt_dfu_boot_failure({ 'reason': reason })
                elif packet_class == 1:
                    if packet_command == 0: # gecko_evt_system_boot
                        major, minor, patch, build, bootloader, hw, hash = struct.unpack('<HHHHIHI', self.bgapi_rx_payload[:18])
                        self.gecko_evt_system_boot({ 'major': major, 'minor': minor, 'patch': patch, 'build': build, 'bootloader': bootloader, 'hw': hw, 'hash': hash })
                    elif packet_command == 3: # gecko_evt_system_external_signal
                        extsignals = struct.unpack('<I', self.bgapi_rx_payload[:4])[0]
                        self.gecko_evt_system_external_signal({ 'extsignals': extsignals })
                    elif packet_command == 4: # gecko_evt_system_awake
                        self.gecko_evt_system_awake({  })
                    elif packet_command == 5: # gecko_evt_system_hardware_error
                        status = struct.unpack('<H', self.bgapi_rx_payload[:2])[0]
                        self.gecko_evt_system_hardware_error({ 'status': status })
                    elif packet_command == 6: # gecko_evt_system_error
                        reason, data_len = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        data_data = self.bgapi_rx_payload[3:]
                        self.gecko_evt_system_error({ 'reason': reason, 'data': data_data })
                elif packet_class == 3:
                    if packet_command == 0: # gecko_evt_le_gap_scan_response
                        rssi, packet_type, address, address_type, bonding, data_len = struct.unpack('<bB6sBBB', self.bgapi_rx_payload[:11])
                        address = address
                        data_data = self.bgapi_rx_payload[11:]
                        self.gecko_evt_le_gap_scan_response({ 'rssi': rssi, 'packet_type': packet_type, 'address': address, 'address_type': address_type, 'bonding': bonding, 'data': data_data })
                    elif packet_command == 1: # gecko_evt_le_gap_adv_timeout
                        handle = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.gecko_evt_le_gap_adv_timeout({ 'handle': handle })
                    elif packet_command == 2: # gecko_evt_le_gap_scan_request
                        handle, address, address_type, bonding = struct.unpack('<B6sBB', self.bgapi_rx_payload[:9])
                        address = address
                        self.gecko_evt_le_gap_scan_request({ 'handle': handle, 'address': address, 'address_type': address_type, 'bonding': bonding })
                    elif packet_command == 4: # gecko_evt_le_gap_extended_scan_response
                        packet_type, address, address_type, bonding, primary_phy, secondary_phy, adv_sid, tx_power, rssi, channel, periodic_interval, data_len = struct.unpack('<B6sBBBBBbbBHB', self.bgapi_rx_payload[:18])
                        address = address
                        data_data = self.bgapi_rx_payload[18:]
                        self.gecko_evt_le_gap_extended_scan_response({ 'packet_type': packet_type, 'address': address, 'address_type': address_type, 'bonding': bonding, 'primary_phy': primary_phy, 'secondary_phy': secondary_phy, 'adv_sid': adv_sid, 'tx_power': tx_power, 'rssi': rssi, 'channel': channel, 'periodic_interval': periodic_interval, 'data': data_data })
                    elif packet_command == 5: # gecko_evt_le_gap_periodic_advertising_status
                        sid, status = struct.unpack('<BI', self.bgapi_rx_payload[:5])
                        self.gecko_evt_le_gap_periodic_advertising_status({ 'sid': sid, 'status': status })
                elif packet_class == 66:
                    if packet_command == 0: # gecko_evt_sync_opened
                        sync, adv_sid, address, address_type, adv_phy, adv_interval, clock_accuracy = struct.unpack('<BB6sBBHH', self.bgapi_rx_payload[:14])
                        address = address
                        self.gecko_evt_sync_opened({ 'sync': sync, 'adv_sid': adv_sid, 'address': address, 'address_type': address_type, 'adv_phy': adv_phy, 'adv_interval': adv_interval, 'clock_accuracy': clock_accuracy })
                    elif packet_command == 1: # gecko_evt_sync_closed
                        reason, sync = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.gecko_evt_sync_closed({ 'reason': reason, 'sync': sync })
                    elif packet_command == 2: # gecko_evt_sync_data
                        sync, tx_power, rssi, data_status, data_len = struct.unpack('<BbbBB', self.bgapi_rx_payload[:5])
                        data_data = self.bgapi_rx_payload[5:]
                        self.gecko_evt_sync_data({ 'sync': sync, 'tx_power': tx_power, 'rssi': rssi, 'data_status': data_status, 'data': data_data })
                elif packet_class == 8:
                    if packet_command == 0: # gecko_evt_le_connection_opened
                        address, address_type, master, connection, bonding, advertiser = struct.unpack('<6sBBBBB', self.bgapi_rx_payload[:11])
                        address = address
                        self.gecko_evt_le_connection_opened({ 'address': address, 'address_type': address_type, 'master': master, 'connection': connection, 'bonding': bonding, 'advertiser': advertiser })
                    elif packet_command == 1: # gecko_evt_le_connection_closed
                        reason, connection = struct.unpack('<HB', self.bgapi_rx_payload[:3])
                        self.gecko_evt_le_connection_closed({ 'reason': reason, 'connection': connection })
                    elif packet_command == 2: # gecko_evt_le_connection_parameters
                        connection, interval, latency, timeout, security_mode, txsize = struct.unpack('<BHHHBH', self.bgapi_rx_payload[:10])
                        self.gecko_evt_le_connection_parameters({ 'connection': connection, 'interval': interval, 'latency': latency, 'timeout': timeout, 'security_mode': security_mode, 'txsize': txsize })
                    elif packet_command == 3: # gecko_evt_le_connection_rssi
                        connection, status, rssi = struct.unpack('<BBb', self.bgapi_rx_payload[:3])
                        self.gecko_evt_le_connection_rssi({ 'connection': connection, 'status': status, 'rssi': rssi })
                    elif packet_command == 4: # gecko_evt_le_connection_phy_status
                        connection, phy = struct.unpack('<BB', self.bgapi_rx_payload[:2])
                        self.gecko_evt_le_connection_phy_status({ 'connection': connection, 'phy': phy })
                elif packet_class == 9:
                    if packet_command == 0: # gecko_evt_gatt_mtu_exchanged
                        connection, mtu = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.gecko_evt_gatt_mtu_exchanged({ 'connection': connection, 'mtu': mtu })
                    elif packet_command == 1: # gecko_evt_gatt_service
                        connection, service, uuid_len = struct.unpack('<BIB', self.bgapi_rx_payload[:6])
                        uuid_data = self.bgapi_rx_payload[6:]
                        self.gecko_evt_gatt_service({ 'connection': connection, 'service': service, 'uuid': uuid_data })
                    elif packet_command == 2: # gecko_evt_gatt_characteristic
                        connection, characteristic, properties, uuid_len = struct.unpack('<BHBB', self.bgapi_rx_payload[:5])
                        uuid_data = self.bgapi_rx_payload[5:]
                        self.gecko_evt_gatt_characteristic({ 'connection': connection, 'characteristic': characteristic, 'properties': properties, 'uuid': uuid_data })
                    elif packet_command == 3: # gecko_evt_gatt_descriptor
                        connection, descriptor, uuid_len = struct.unpack('<BHB', self.bgapi_rx_payload[:4])
                        uuid_data = self.bgapi_rx_payload[4:]
                        self.gecko_evt_gatt_descriptor({ 'connection': connection, 'descriptor': descriptor, 'uuid': uuid_data })
                    elif packet_command == 4: # gecko_evt_gatt_characteristic_value
                        connection, characteristic, att_opcode, offset, value_len = struct.unpack('<BHBHB', self.bgapi_rx_payload[:7])
                        value_data = self.bgapi_rx_payload[7:]
                        self.gecko_evt_gatt_characteristic_value({ 'connection': connection, 'characteristic': characteristic, 'att_opcode': att_opcode, 'offset': offset, 'value': value_data })
                    elif packet_command == 5: # gecko_evt_gatt_descriptor_value
                        connection, descriptor, offset, value_len = struct.unpack('<BHHB', self.bgapi_rx_payload[:6])
                        value_data = self.bgapi_rx_payload[6:]
                        self.gecko_evt_gatt_descriptor_value({ 'connection': connection, 'descriptor': descriptor, 'offset': offset, 'value': value_data })
                    elif packet_command == 6: # gecko_evt_gatt_procedure_completed
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.gecko_evt_gatt_procedure_completed({ 'connection': connection, 'result': result })
                elif packet_class == 10:
                    if packet_command == 0: # gecko_evt_gatt_server_attribute_value
                        connection, attribute, att_opcode, offset, value_len = struct.unpack('<BHBHB', self.bgapi_rx_payload[:7])
                        value_data = self.bgapi_rx_payload[7:]
                        self.gecko_evt_gatt_server_attribute_value({ 'connection': connection, 'attribute': attribute, 'att_opcode': att_opcode, 'offset': offset, 'value': value_data })
                    elif packet_command == 1: # gecko_evt_gatt_server_user_read_request
                        connection, characteristic, att_opcode, offset = struct.unpack('<BHBH', self.bgapi_rx_payload[:6])
                        self.gecko_evt_gatt_server_user_read_request({ 'connection': connection, 'characteristic': characteristic, 'att_opcode': att_opcode, 'offset': offset })
                    elif packet_command == 2: # gecko_evt_gatt_server_user_write_request
                        connection, characteristic, att_opcode, offset, value_len = struct.unpack('<BHBHB', self.bgapi_rx_payload[:7])
                        value_data = self.bgapi_rx_payload[7:]
                        self.gecko_evt_gatt_server_user_write_request({ 'connection': connection, 'characteristic': characteristic, 'att_opcode': att_opcode, 'offset': offset, 'value': value_data })
                    elif packet_command == 3: # gecko_evt_gatt_server_characteristic_status
                        connection, characteristic, status_flags, client_config_flags = struct.unpack('<BHBH', self.bgapi_rx_payload[:6])
                        self.gecko_evt_gatt_server_characteristic_status({ 'connection': connection, 'characteristic': characteristic, 'status_flags': status_flags, 'client_config_flags': client_config_flags })
                    elif packet_command == 4: # gecko_evt_gatt_server_execute_write_completed
                        connection, result = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.gecko_evt_gatt_server_execute_write_completed({ 'connection': connection, 'result': result })
                elif packet_class == 12:
                    if packet_command == 0: # gecko_evt_hardware_soft_timer
                        handle = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.gecko_evt_hardware_soft_timer({ 'handle': handle })
                elif packet_class == 14:
                    if packet_command == 0: # gecko_evt_test_dtm_completed
                        result, number_of_packets = struct.unpack('<HH', self.bgapi_rx_payload[:4])
                        self.gecko_evt_test_dtm_completed({ 'result': result, 'number_of_packets': number_of_packets })
                elif packet_class == 15:
                    if packet_command == 0: # gecko_evt_sm_passkey_display
                        connection, passkey = struct.unpack('<BI', self.bgapi_rx_payload[:5])
                        self.gecko_evt_sm_passkey_display({ 'connection': connection, 'passkey': passkey })
                    elif packet_command == 1: # gecko_evt_sm_passkey_request
                        connection = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.gecko_evt_sm_passkey_request({ 'connection': connection })
                    elif packet_command == 2: # gecko_evt_sm_confirm_passkey
                        connection, passkey = struct.unpack('<BI', self.bgapi_rx_payload[:5])
                        self.gecko_evt_sm_confirm_passkey({ 'connection': connection, 'passkey': passkey })
                    elif packet_command == 3: # gecko_evt_sm_bonded
                        connection, bonding = struct.unpack('<BB', self.bgapi_rx_payload[:2])
                        self.gecko_evt_sm_bonded({ 'connection': connection, 'bonding': bonding })
                    elif packet_command == 4: # gecko_evt_sm_bonding_failed
                        connection, reason = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.gecko_evt_sm_bonding_failed({ 'connection': connection, 'reason': reason })
                    elif packet_command == 5: # gecko_evt_sm_list_bonding_entry
                        bonding, address, address_type = struct.unpack('<B6sB', self.bgapi_rx_payload[:8])
                        address = address
                        self.gecko_evt_sm_list_bonding_entry({ 'bonding': bonding, 'address': address, 'address_type': address_type })
                    elif packet_command == 6: # gecko_evt_sm_list_all_bondings_complete
                        self.gecko_evt_sm_list_all_bondings_complete({  })
                    elif packet_command == 9: # gecko_evt_sm_confirm_bonding
                        connection, bonding_handle = struct.unpack('<Bb', self.bgapi_rx_payload[:2])
                        self.gecko_evt_sm_confirm_bonding({ 'connection': connection, 'bonding_handle': bonding_handle })
                elif packet_class == 19:
                    if packet_command == 0: # gecko_evt_homekit_setupcode_display
                        connection, setupcode_len = struct.unpack('<BB', self.bgapi_rx_payload[:2])
                        setupcode_data = self.bgapi_rx_payload[2:]
                        self.gecko_evt_homekit_setupcode_display({ 'connection': connection, 'setupcode': setupcode_data })
                    elif packet_command == 1: # gecko_evt_homekit_paired
                        connection, reason = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.gecko_evt_homekit_paired({ 'connection': connection, 'reason': reason })
                    elif packet_command == 2: # gecko_evt_homekit_pair_verified
                        connection, reason = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.gecko_evt_homekit_pair_verified({ 'connection': connection, 'reason': reason })
                    elif packet_command == 3: # gecko_evt_homekit_connection_opened
                        connection = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.gecko_evt_homekit_connection_opened({ 'connection': connection })
                    elif packet_command == 4: # gecko_evt_homekit_connection_closed
                        connection, reason = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.gecko_evt_homekit_connection_closed({ 'connection': connection, 'reason': reason })
                    elif packet_command == 5: # gecko_evt_homekit_identify
                        connection = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        self.gecko_evt_homekit_identify({ 'connection': connection })
                    elif packet_command == 6: # gecko_evt_homekit_write_request
                        connection, characteristic, chr_value_size, authorization_size, value_offset, value_len = struct.unpack('<BHHHHB', self.bgapi_rx_payload[:10])
                        value_data = self.bgapi_rx_payload[10:]
                        self.gecko_evt_homekit_write_request({ 'connection': connection, 'characteristic': characteristic, 'chr_value_size': chr_value_size, 'authorization_size': authorization_size, 'value_offset': value_offset, 'value': value_data })
                    elif packet_command == 7: # gecko_evt_homekit_read_request
                        connection, characteristic, offset = struct.unpack('<BHH', self.bgapi_rx_payload[:5])
                        self.gecko_evt_homekit_read_request({ 'connection': connection, 'characteristic': characteristic, 'offset': offset })
                    elif packet_command == 8: # gecko_evt_homekit_disconnection_required
                        connection, reason = struct.unpack('<BH', self.bgapi_rx_payload[:3])
                        self.gecko_evt_homekit_disconnection_required({ 'connection': connection, 'reason': reason })
                    elif packet_command == 9: # gecko_evt_homekit_pairing_removed
                        connection, remaining_pairings, pairing_id_len = struct.unpack('<BHB', self.bgapi_rx_payload[:4])
                        pairing_id_data = self.bgapi_rx_payload[4:]
                        self.gecko_evt_homekit_pairing_removed({ 'connection': connection, 'remaining_pairings': remaining_pairings, 'pairing_id': pairing_id_data })
                    elif packet_command == 10: # gecko_evt_homekit_setuppayload_display
                        connection, setuppayload_len = struct.unpack('<BB', self.bgapi_rx_payload[:2])
                        setuppayload_data = self.bgapi_rx_payload[2:]
                        self.gecko_evt_homekit_setuppayload_display({ 'connection': connection, 'setuppayload': setuppayload_data })
                elif packet_class == 67:
                    if packet_command == 1: # gecko_evt_l2cap_coc_connection_request
                        connection, le_psm, source_cid, mtu, mps, initial_credit, flags, encryption_key_size = struct.unpack('<BHHHHHBB', self.bgapi_rx_payload[:13])
                        self.gecko_evt_l2cap_coc_connection_request({ 'connection': connection, 'le_psm': le_psm, 'source_cid': source_cid, 'mtu': mtu, 'mps': mps, 'initial_credit': initial_credit, 'flags': flags, 'encryption_key_size': encryption_key_size })
                    elif packet_command == 2: # gecko_evt_l2cap_coc_connection_response
                        connection, destination_cid, mtu, mps, initial_credit, result = struct.unpack('<BHHHHH', self.bgapi_rx_payload[:11])
                        self.gecko_evt_l2cap_coc_connection_response({ 'connection': connection, 'destination_cid': destination_cid, 'mtu': mtu, 'mps': mps, 'initial_credit': initial_credit, 'result': result })
                    elif packet_command == 3: # gecko_evt_l2cap_coc_le_flow_control_credit
                        connection, cid, credits = struct.unpack('<BHH', self.bgapi_rx_payload[:5])
                        self.gecko_evt_l2cap_coc_le_flow_control_credit({ 'connection': connection, 'cid': cid, 'credits': credits })
                    elif packet_command == 4: # gecko_evt_l2cap_coc_channel_disconnected
                        connection, cid, reason = struct.unpack('<BHH', self.bgapi_rx_payload[:5])
                        self.gecko_evt_l2cap_coc_channel_disconnected({ 'connection': connection, 'cid': cid, 'reason': reason })
                    elif packet_command == 5: # gecko_evt_l2cap_coc_data
                        connection, cid, data_len = struct.unpack('<BHB', self.bgapi_rx_payload[:4])
                        data_data = self.bgapi_rx_payload[4:]
                        self.gecko_evt_l2cap_coc_data({ 'connection': connection, 'cid': cid, 'data': data_data })
                    elif packet_command == 6: # gecko_evt_l2cap_command_rejected
                        connection, code, reason = struct.unpack('<BBH', self.bgapi_rx_payload[:4])
                        self.gecko_evt_l2cap_command_rejected({ 'connection': connection, 'code': code, 'reason': reason })
                elif packet_class == 69:
                    if packet_command == 0: # gecko_evt_cte_receiver_iq_report
                        status, packet_type, handle, phy, channel, rssi, rssi_antenna_id, cte_type, slot_durations, event_counter, completeness, samples_len = struct.unpack('<HBBBBbBBBHBB', self.bgapi_rx_payload[:14])
                        samples_data = self.bgapi_rx_payload[14:]
                        self.gecko_evt_cte_receiver_iq_report({ 'status': status, 'packet_type': packet_type, 'handle': handle, 'phy': phy, 'channel': channel, 'rssi': rssi, 'rssi_antenna_id': rssi_antenna_id, 'cte_type': cte_type, 'slot_durations': slot_durations, 'event_counter': event_counter, 'completeness': completeness, 'samples': samples_data })
                elif packet_class == 254:
                    if packet_command == 0: # gecko_evt_qualtester_state_changed
                        group, id, value, data_len = struct.unpack('<IIIB', self.bgapi_rx_payload[:13])
                        data_data = self.bgapi_rx_payload[13:]
                        self.gecko_evt_qualtester_state_changed({ 'group': group, 'id': id, 'value': value, 'data': data_data })
                elif packet_class == 255:
                    if packet_command == 0: # gecko_evt_user_message_to_host
                        data_len = struct.unpack('<B', self.bgapi_rx_payload[:1])[0]
                        data_data = self.bgapi_rx_payload[1:]
                        self.gecko_evt_user_message_to_host({ 'data': data_data })

# ================================================================
