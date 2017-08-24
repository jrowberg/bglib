#!/usr/bin/env python

""" Bluegiga BGAPI/BGLib implementation

Changelog:
    2013-05-04 - Fixed single-item struct.unpack returns (@zwasson on Github)
    2013-04-28 - Fixed numerous uint8array/bd_addr command arg errors
               - Added 'debug' support
    2013-04-16 - Fixed 'bglib_on_idle' to be 'on_idle'
    2013-04-15 - Added wifi BGAPI support in addition to BLE BGAPI
               - Fixed references to 'this' instead of 'self'
    2013-04-11 - Initial release

============================================
Bluegiga BGLib Python interface library
2013-05-04 by Jeff Rowberg <jeff@rowberg.net>
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

# ================================================================

