// Bluegiga BGLib C# interface library
// 2013-01-15 by Jeff Rowberg <jeff@rowberg.net>
// Updates should (hopefully) always be available at https://github.com/jrowberg/bglib

/* ============================================
BGLib C# interface library code is placed under the MIT license
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
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluegiga {

    namespace BLE {

        public delegate void SystemBootEventHandler(object sender, SystemBootEventArgs e);
        public class SystemBootEventArgs : EventArgs {
            public readonly UInt16 major;
            public readonly UInt16 minor;
            public readonly UInt16 patch;
            public readonly UInt16 build;
            public readonly UInt16 ll_version;
            public readonly Byte protocol_version;
            public readonly Byte hw;

            public SystemBootEventArgs(UInt16 major, UInt16 minor, UInt16 patch, UInt16 build, UInt16 ll_version, Byte protocol_version, Byte hw)
            {
                this.major = major;
                this.minor = minor;
                this.patch = patch;
                this.build = build;
                this.ll_version = ll_version;
                this.protocol_version = protocol_version;
                this.hw = hw;
            }
        }

        public delegate void GAPScanResponseEventHandler(object sender, GAPScanResponseEventArgs e);
        public class GAPScanResponseEventArgs : EventArgs
        {
            public readonly Byte rssi;
            public readonly Byte packet_type;
            public readonly Byte[] bd_addr;
            public readonly Byte address_type;
            public readonly Byte bond;
            public readonly Byte[] data;

            public GAPScanResponseEventArgs(Byte rssi, Byte packet_type, Byte[] bd_addr, Byte address_type, Byte bond, Byte[] data)
            {
                this.rssi = rssi;
                this.packet_type = packet_type;
                this.bd_addr = bd_addr;
                this.address_type = address_type;
                this.bond = bond;
                this.data = data;
            }
        }

    }

    //public delegate void AlarmEventHandler(object sender, AlarmEventArgs e);

    public class BGLib
    {
        private Byte[] bgapiRXBuffer = new Byte[65];
        private int bgapiRXBufferPos = 0;
        private int bgapiRXDataLen = 0;

        private Byte[] bgapiTXBuffer = new Byte[65];

        public event BLE.SystemBootEventHandler SystemBootEvent;
        public event BLE.GAPScanResponseEventHandler GAPScanResponseEvent;

        public UInt16 parse(Byte ch) {
            /*#ifdef DEBUG
                // DEBUG: output hex value of incoming character
                if (ch < 16) Serial.write(0x30);    // leading '0'
                Serial.print(ch, HEX);              // actual hex value
                Serial.write(0x20);                 // trailing ' '
            #endif*/

            /*
            BGAPI packet structure (as of 2013-01-15):
                Byte -1: (optional, offsets others and present when packet mode enabled)
                            8 bits, length of following data (1-64 max)
                            NOTE: PACKET MODE ONLY GOES FROM THE MODULE TO THE MCU, NOT
                            THE OTHER WAY AROUND. PACKET MODE *ONLY* AFFECTS COMMANDS,
                            NOT EITHER RESPONSES OR EVENTS.
                Byte 0:
                        [7] - 1 bit, Message Type (MT)         0 = Command/Response, 1 = Event
                    [6:3] - 4 bits, Technology Type (TT)     0000 = Bluetooth 4.0 single mode, 0001 = Wi-Fi
                    [2:0] - 3 bits, Length High (LH)         Payload length (high bits)
                Byte 1:     8 bits, Length Low (LL)          Payload length (low bits)
                Byte 2:     8 bits, Class ID (CID)           Command class ID
                Byte 3:     8 bits, Command ID (CMD)         Command ID
                Bytes 4-n:  0 - 2048 Bytes, Payload (PL)     Up to 2048 bytes of payload
            */

            // check packet position
            if (bgapiRXBufferPos == 0) {
                // beginning of packet, check for correct framing/expected byte(s)
                // BGAPI packet for Bluetooth Smart Single Mode must be either Command/Response (0x00) or Event (0x80)
                if ((ch & 0x78) == 0x00) {
                    // store new character in RX buffer
                    bgapiRXBuffer[bgapiRXBufferPos++] = ch;
                } else {
                    /*#ifdef DEBUG
                        Serial.print("*** Packet frame sync error! Expected .0000... binary, got 0x");
                        Serial.println(ch, HEX);
                    #endif*/
                    return 1; // packet format error
                }
            } else {
                // middle of packet, assume we're okay
                bgapiRXBuffer[bgapiRXBufferPos++] = ch;
                if (bgapiRXBufferPos == 2) {
                    // just received "Length Low" byte, so store expected packet length
                    bgapiRXDataLen = ch + ((bgapiRXBuffer[0] & 0x03) << 8);
                } else if (bgapiRXBufferPos == bgapiRXDataLen + 4) {
                    // just received last expected byte
                    /*#ifdef DEBUG
                        Serial.print("\n<- RX [ ");
                        for (uint8_t i = 0; i < bgapiRXBufferPos; i++) {
                            if (bgapiRXBuffer[i] < 16) Serial.write(0x30);
                            Serial.print(bgapiRXBuffer[i], HEX);
                            Serial.write(0x20);
                        }
                        Serial.println("]");
                    #endif*/

                    // check packet type
                    if ((bgapiRXBuffer[0] & 0x80) == 0) {
                        // 0x00 = Response packet
                        /*if (bgapiRXBuffer[2] == 0) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_rsp_system_reset) ble_rsp_system_reset((const struct ble_msg_system_reset_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_system_hello) ble_rsp_system_hello((const struct ble_msg_system_hello_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_system_address_get) ble_rsp_system_address_get((const struct ble_msg_system_address_get_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_system_reg_write) ble_rsp_system_reg_write((const struct ble_msg_system_reg_write_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_system_reg_read) ble_rsp_system_reg_read((const struct ble_msg_system_reg_read_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_system_get_counters) ble_rsp_system_get_counters((const struct ble_msg_system_get_counters_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_system_get_connections) ble_rsp_system_get_connections((const struct ble_msg_system_get_connections_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 7) { if (ble_rsp_system_read_memory) ble_rsp_system_read_memory((const struct ble_msg_system_read_memory_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 8) { if (ble_rsp_system_get_info) ble_rsp_system_get_info((const struct ble_msg_system_get_info_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 9) { if (ble_rsp_system_endpoint_tx) ble_rsp_system_endpoint_tx((const struct ble_msg_system_endpoint_tx_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 10) { if (ble_rsp_system_whitelist_append) ble_rsp_system_whitelist_append((const struct ble_msg_system_whitelist_append_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 11) { if (ble_rsp_system_whitelist_remove) ble_rsp_system_whitelist_remove((const struct ble_msg_system_whitelist_remove_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 12) { if (ble_rsp_system_whitelist_clear) ble_rsp_system_whitelist_clear((const struct ble_msg_system_whitelist_clear_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 13) { if (ble_rsp_system_endpoint_rx) ble_rsp_system_endpoint_rx((const struct ble_msg_system_endpoint_rx_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 14) { if (ble_rsp_system_endpoint_set_watermarks) ble_rsp_system_endpoint_set_watermarks((const struct ble_msg_system_endpoint_set_watermarks_rsp_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 1) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_rsp_flash_ps_defrag) ble_rsp_flash_ps_defrag((const struct ble_msg_flash_ps_defrag_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_flash_ps_dump) ble_rsp_flash_ps_dump((const struct ble_msg_flash_ps_dump_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_flash_ps_erase_all) ble_rsp_flash_ps_erase_all((const struct ble_msg_flash_ps_erase_all_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_flash_ps_save) ble_rsp_flash_ps_save((const struct ble_msg_flash_ps_save_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_flash_ps_load) ble_rsp_flash_ps_load((const struct ble_msg_flash_ps_load_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_flash_ps_erase) ble_rsp_flash_ps_erase((const struct ble_msg_flash_ps_erase_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_flash_erase_page) ble_rsp_flash_erase_page((const struct ble_msg_flash_erase_page_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 7) { if (ble_rsp_flash_write_words) ble_rsp_flash_write_words((const struct ble_msg_flash_write_words_rsp_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 2) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_rsp_attributes_write) ble_rsp_attributes_write((const struct ble_msg_attributes_write_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_attributes_read) ble_rsp_attributes_read((const struct ble_msg_attributes_read_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_attributes_read_type) ble_rsp_attributes_read_type((const struct ble_msg_attributes_read_type_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_attributes_user_read_response) ble_rsp_attributes_user_read_response((const struct ble_msg_attributes_user_read_response_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_attributes_user_write_response) ble_rsp_attributes_user_write_response((const struct ble_msg_attributes_user_write_response_rsp_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 3) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_rsp_connection_disconnect) ble_rsp_connection_disconnect((const struct ble_msg_connection_disconnect_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_connection_get_rssi) ble_rsp_connection_get_rssi((const struct ble_msg_connection_get_rssi_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_connection_update) ble_rsp_connection_update((const struct ble_msg_connection_update_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_connection_version_update) ble_rsp_connection_version_update((const struct ble_msg_connection_version_update_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_connection_channel_map_get) ble_rsp_connection_channel_map_get((const struct ble_msg_connection_channel_map_get_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_connection_channel_map_set) ble_rsp_connection_channel_map_set((const struct ble_msg_connection_channel_map_set_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_connection_features_get) ble_rsp_connection_features_get((const struct ble_msg_connection_features_get_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 7) { if (ble_rsp_connection_get_status) ble_rsp_connection_get_status((const struct ble_msg_connection_get_status_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 8) { if (ble_rsp_connection_raw_tx) ble_rsp_connection_raw_tx((const struct ble_msg_connection_raw_tx_rsp_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 4) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_rsp_attclient_find_by_type_value) ble_rsp_attclient_find_by_type_value((const struct ble_msg_attclient_find_by_type_value_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_attclient_read_by_group_type) ble_rsp_attclient_read_by_group_type((const struct ble_msg_attclient_read_by_group_type_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_attclient_read_by_type) ble_rsp_attclient_read_by_type((const struct ble_msg_attclient_read_by_type_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_attclient_find_information) ble_rsp_attclient_find_information((const struct ble_msg_attclient_find_information_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_attclient_read_by_handle) ble_rsp_attclient_read_by_handle((const struct ble_msg_attclient_read_by_handle_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_attclient_attribute_write) ble_rsp_attclient_attribute_write((const struct ble_msg_attclient_attribute_write_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_attclient_write_command) ble_rsp_attclient_write_command((const struct ble_msg_attclient_write_command_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 7) { if (ble_rsp_attclient_indicate_confirm) ble_rsp_attclient_indicate_confirm((const struct ble_msg_attclient_indicate_confirm_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 8) { if (ble_rsp_attclient_read_long) ble_rsp_attclient_read_long((const struct ble_msg_attclient_read_long_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 9) { if (ble_rsp_attclient_prepare_write) ble_rsp_attclient_prepare_write((const struct ble_msg_attclient_prepare_write_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 10) { if (ble_rsp_attclient_execute_write) ble_rsp_attclient_execute_write((const struct ble_msg_attclient_execute_write_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 11) { if (ble_rsp_attclient_read_multiple) ble_rsp_attclient_read_multiple((const struct ble_msg_attclient_read_multiple_rsp_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 5) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_rsp_sm_encrypt_start) ble_rsp_sm_encrypt_start((const struct ble_msg_sm_encrypt_start_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_sm_set_bondable_mode) ble_rsp_sm_set_bondable_mode((const struct ble_msg_sm_set_bondable_mode_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_sm_delete_bonding) ble_rsp_sm_delete_bonding((const struct ble_msg_sm_delete_bonding_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_sm_set_parameters) ble_rsp_sm_set_parameters((const struct ble_msg_sm_set_parameters_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_sm_passkey_entry) ble_rsp_sm_passkey_entry((const struct ble_msg_sm_passkey_entry_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_sm_get_bonds) ble_rsp_sm_get_bonds((const struct ble_msg_sm_get_bonds_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_sm_set_oob_data) ble_rsp_sm_set_oob_data((const struct ble_msg_sm_set_oob_data_rsp_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 6) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_rsp_gap_set_privacy_flags) ble_rsp_gap_set_privacy_flags((const struct ble_msg_gap_set_privacy_flags_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_gap_set_mode) ble_rsp_gap_set_mode((const struct ble_msg_gap_set_mode_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_gap_discover) ble_rsp_gap_discover((const struct ble_msg_gap_discover_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_gap_connect_direct) ble_rsp_gap_connect_direct((const struct ble_msg_gap_connect_direct_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_gap_end_procedure) ble_rsp_gap_end_procedure((const struct ble_msg_gap_end_procedure_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_gap_connect_selective) ble_rsp_gap_connect_selective((const struct ble_msg_gap_connect_selective_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_gap_set_filtering) ble_rsp_gap_set_filtering((const struct ble_msg_gap_set_filtering_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 7) { if (ble_rsp_gap_set_scan_parameters) ble_rsp_gap_set_scan_parameters((const struct ble_msg_gap_set_scan_parameters_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 8) { if (ble_rsp_gap_set_adv_parameters) ble_rsp_gap_set_adv_parameters((const struct ble_msg_gap_set_adv_parameters_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 9) { if (ble_rsp_gap_set_adv_data) ble_rsp_gap_set_adv_data((const struct ble_msg_gap_set_adv_data_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 10) { if (ble_rsp_gap_set_directed_connectable_mode) ble_rsp_gap_set_directed_connectable_mode((const struct ble_msg_gap_set_directed_connectable_mode_rsp_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 7) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_rsp_hardware_io_port_config_irq) ble_rsp_hardware_io_port_config_irq((const struct ble_msg_hardware_io_port_config_irq_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_hardware_set_soft_timer) ble_rsp_hardware_set_soft_timer((const struct ble_msg_hardware_set_soft_timer_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_hardware_adc_read) ble_rsp_hardware_adc_read((const struct ble_msg_hardware_adc_read_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_hardware_io_port_config_direction) ble_rsp_hardware_io_port_config_direction((const struct ble_msg_hardware_io_port_config_direction_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_hardware_io_port_config_function) ble_rsp_hardware_io_port_config_function((const struct ble_msg_hardware_io_port_config_function_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_hardware_io_port_config_pull) ble_rsp_hardware_io_port_config_pull((const struct ble_msg_hardware_io_port_config_pull_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_hardware_io_port_write) ble_rsp_hardware_io_port_write((const struct ble_msg_hardware_io_port_write_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 7) { if (ble_rsp_hardware_io_port_read) ble_rsp_hardware_io_port_read((const struct ble_msg_hardware_io_port_read_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 8) { if (ble_rsp_hardware_spi_config) ble_rsp_hardware_spi_config((const struct ble_msg_hardware_spi_config_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 9) { if (ble_rsp_hardware_spi_transfer) ble_rsp_hardware_spi_transfer((const struct ble_msg_hardware_spi_transfer_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 10) { if (ble_rsp_hardware_i2c_read) ble_rsp_hardware_i2c_read((const struct ble_msg_hardware_i2c_read_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 11) { if (ble_rsp_hardware_i2c_write) ble_rsp_hardware_i2c_write((const struct ble_msg_hardware_i2c_write_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 12) { if (ble_rsp_hardware_set_txpower) ble_rsp_hardware_set_txpower((const struct ble_msg_hardware_set_txpower_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 13) { if (ble_rsp_hardware_timer_comparator) ble_rsp_hardware_timer_comparator((const struct ble_msg_hardware_timer_comparator_rsp_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 8) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_rsp_test_phy_tx) ble_rsp_test_phy_tx((const struct ble_msg_test_phy_tx_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_test_phy_rx) ble_rsp_test_phy_rx((const struct ble_msg_test_phy_rx_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_test_phy_end) ble_rsp_test_phy_end((const struct ble_msg_test_phy_end_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_test_phy_reset) ble_rsp_test_phy_reset((const struct ble_msg_test_phy_reset_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_test_get_channel_map) ble_rsp_test_get_channel_map((const struct ble_msg_test_get_channel_map_rsp_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_test_debug) ble_rsp_test_debug((const struct ble_msg_test_debug_rsp_t *)(bgapiRXBuffer + 4)); }
                        }*/
                        //setBusy(false);
                    } else {
                        // 0x80 = Event packet
                        if (bgapiRXBuffer[2] == 0)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (SystemBootEvent != null)
                                {
                                    SystemBootEvent(this, new BLE.SystemBootEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8)),
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8)),
                                        (UInt16)(bgapiRXBuffer[10] + (bgapiRXBuffer[11] << 8)),
                                        (UInt16)(bgapiRXBuffer[12] + (bgapiRXBuffer[13] << 8)),
                                        bgapiRXBuffer[14],
                                        bgapiRXBuffer[15]
                                        ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 6)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (GAPScanResponseEvent != null)
                                {
                                    GAPScanResponseEvent(this, new BLE.GAPScanResponseEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5],
                                        (Byte[])(bgapiRXBuffer.Skip(6).Take(6).ToArray()),
                                        bgapiRXBuffer[12],
                                        bgapiRXBuffer[13],
                                        (Byte[])(bgapiRXBuffer.Skip(15).Take(bgapiRXBuffer[14]).ToArray())
                                        ));
                                }
                            }
                        }
                        /*if (bgapiRXBuffer[2] == 0) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_evt_system_boot) { ble_evt_system_boot((const struct ble_msg_system_boot_evt_t *)(bgapiRXBuffer + 4)); } setBusy(false); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_evt_system_debug) ble_evt_system_debug((const struct ble_msg_system_debug_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_evt_system_endpoint_watermark_rx) ble_evt_system_endpoint_watermark_rx((const struct ble_msg_system_endpoint_watermark_rx_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 3) { if (ble_evt_system_endpoint_watermark_tx) ble_evt_system_endpoint_watermark_tx((const struct ble_msg_system_endpoint_watermark_tx_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 4) { if (ble_evt_system_script_failure) ble_evt_system_script_failure((const struct ble_msg_system_script_failure_evt_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 1) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_evt_flash_ps_key) ble_evt_flash_ps_key((const struct ble_msg_flash_ps_key_evt_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 2) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_evt_attributes_value) ble_evt_attributes_value((const struct ble_msg_attributes_value_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_evt_attributes_user_read_request) ble_evt_attributes_user_read_request((const struct ble_msg_attributes_user_read_request_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_evt_attributes_status) ble_evt_attributes_status((const struct ble_msg_attributes_status_evt_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 3) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_evt_connection_status) ble_evt_connection_status((const struct ble_msg_connection_status_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_evt_connection_version_ind) ble_evt_connection_version_ind((const struct ble_msg_connection_version_ind_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_evt_connection_feature_ind) ble_evt_connection_feature_ind((const struct ble_msg_connection_feature_ind_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 3) { if (ble_evt_connection_raw_rx) ble_evt_connection_raw_rx((const struct ble_msg_connection_raw_rx_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 4) { if (ble_evt_connection_disconnected) ble_evt_connection_disconnected((const struct ble_msg_connection_disconnected_evt_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 4) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_evt_attclient_indicated) ble_evt_attclient_indicated((const struct ble_msg_attclient_indicated_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_evt_attclient_procedure_completed) ble_evt_attclient_procedure_completed((const struct ble_msg_attclient_procedure_completed_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_evt_attclient_group_found) ble_evt_attclient_group_found((const struct ble_msg_attclient_group_found_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 3) { if (ble_evt_attclient_attribute_found) ble_evt_attclient_attribute_found((const struct ble_msg_attclient_attribute_found_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 4) { if (ble_evt_attclient_find_information_found) ble_evt_attclient_find_information_found((const struct ble_msg_attclient_find_information_found_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 5) { if (ble_evt_attclient_attribute_value) ble_evt_attclient_attribute_value((const struct ble_msg_attclient_attribute_value_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 6) { if (ble_evt_attclient_read_multiple_response) ble_evt_attclient_read_multiple_response((const struct ble_msg_attclient_read_multiple_response_evt_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 5) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_evt_sm_smp_data) ble_evt_sm_smp_data((const struct ble_msg_sm_smp_data_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_evt_sm_bonding_fail) ble_evt_sm_bonding_fail((const struct ble_msg_sm_bonding_fail_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_evt_sm_passkey_display) ble_evt_sm_passkey_display((const struct ble_msg_sm_passkey_display_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 3) { if (ble_evt_sm_passkey_request) ble_evt_sm_passkey_request((const struct ble_msg_sm_passkey_request_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 4) { if (ble_evt_sm_bond_status) ble_evt_sm_bond_status((const struct ble_msg_sm_bond_status_evt_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 6) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_evt_gap_scan_response) ble_evt_gap_scan_response((const struct ble_msg_gap_scan_response_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_evt_gap_mode_changed) ble_evt_gap_mode_changed((const struct ble_msg_gap_mode_changed_evt_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 7) {
                            if (bgapiRXBuffer[3] == 0) { if (ble_evt_hardware_io_port_status) ble_evt_hardware_io_port_status((const struct ble_msg_hardware_io_port_status_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 1) { if (ble_evt_hardware_soft_timer) ble_evt_hardware_soft_timer((const struct ble_msg_hardware_soft_timer_evt_t *)(bgapiRXBuffer + 4)); }
                            else if (bgapiRXBuffer[3] == 2) { if (ble_evt_hardware_adc_result) ble_evt_hardware_adc_result((const struct ble_msg_hardware_adc_result_evt_t *)(bgapiRXBuffer + 4)); }
                        }
                        else if (bgapiRXBuffer[2] == 8) {
                        }*/
                    }

                    Console.WriteLine("<= RX BGLIB PACKET");

                    // reset RX packet buffer position to be ready for new packet
                    bgapiRXBufferPos = 0;
                }
            }

            return 0; // parsed successfully
        }
 
    }

}