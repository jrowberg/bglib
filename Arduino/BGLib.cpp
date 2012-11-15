// Bluegiga BGLib Arduino interface library
// 2012-11-14 by Jeff Rowberg <jeff@rowberg.net>
// Updates should (hopefully) always be available at https://github.com/jrowberg/bglib

/* ============================================
BGLib library code is placed under the MIT license
Copyright (c) 2012 Jeff Rowberg

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

#include "BGLib.h"

BGLib::BGLib(HardwareSerial *module, HardwareSerial *output) {
    uModule = module;
    uOutput = output;

    // initialize packet buffers
    bgapiRXBuffer = (uint8_t *)malloc(bgapiRXBufferSize = 32);
    bgapiTXBuffer = (uint8_t *)malloc(bgapiTXBufferSize = 32);
    bgapiRXBufferPos = bgapiTXBufferPos = 0;

    onBusy = 0;
    onIdle = 0;
    onTimeout = 0;

    ble_rsp_system_reset = 0;
    ble_rsp_system_hello = 0;
    ble_rsp_system_address_get = 0;
    ble_rsp_system_reg_write = 0;
    ble_rsp_system_reg_read = 0;
    ble_rsp_system_get_counters = 0;
    ble_rsp_system_get_connections = 0;
    ble_rsp_system_read_memory = 0;
    ble_rsp_system_get_info = 0;
    ble_rsp_system_endpoint_tx = 0;
    ble_rsp_system_whitelist_append = 0;
    ble_rsp_system_whitelist_remove = 0;
    ble_rsp_system_whitelist_clear = 0;
    ble_rsp_system_endpoint_rx = 0;
    ble_rsp_system_endpoint_set_watermarks = 0;
    ble_rsp_flash_ps_defrag = 0;
    ble_rsp_flash_ps_dump = 0;
    ble_rsp_flash_ps_erase_all = 0;
    ble_rsp_flash_ps_save = 0;
    ble_rsp_flash_ps_load = 0;
    ble_rsp_flash_ps_erase = 0;
    ble_rsp_flash_erase_page = 0;
    ble_rsp_flash_write_words = 0;
    ble_rsp_attributes_write = 0;
    ble_rsp_attributes_read = 0;
    ble_rsp_attributes_read_type = 0;
    ble_rsp_attributes_user_read_response = 0;
    ble_rsp_attributes_user_write_response = 0;
    ble_rsp_connection_disconnect = 0;
    ble_rsp_connection_get_rssi = 0;
    ble_rsp_connection_update = 0;
    ble_rsp_connection_version_update = 0;
    ble_rsp_connection_channel_map_get = 0;
    ble_rsp_connection_channel_map_set = 0;
    ble_rsp_connection_features_get = 0;
    ble_rsp_connection_get_status = 0;
    ble_rsp_connection_raw_tx = 0;
    ble_rsp_attclient_find_by_type_value = 0;
    ble_rsp_attclient_read_by_group_type = 0;
    ble_rsp_attclient_read_by_type = 0;
    ble_rsp_attclient_find_information = 0;
    ble_rsp_attclient_read_by_handle = 0;
    ble_rsp_attclient_attribute_write = 0;
    ble_rsp_attclient_write_command = 0;
    ble_rsp_attclient_indicate_confirm = 0;
    ble_rsp_attclient_read_long = 0;
    ble_rsp_attclient_prepare_write = 0;
    ble_rsp_attclient_execute_write = 0;
    ble_rsp_attclient_read_multiple = 0;
    ble_rsp_sm_encrypt_start = 0;
    ble_rsp_sm_set_bondable_mode = 0;
    ble_rsp_sm_delete_bonding = 0;
    ble_rsp_sm_set_parameters = 0;
    ble_rsp_sm_passkey_entry = 0;
    ble_rsp_sm_get_bonds = 0;
    ble_rsp_sm_set_oob_data = 0;
    ble_rsp_gap_set_privacy_flags = 0;
    ble_rsp_gap_set_mode = 0;
    ble_rsp_gap_discover = 0;
    ble_rsp_gap_connect_direct = 0;
    ble_rsp_gap_end_procedure = 0;
    ble_rsp_gap_connect_selective = 0;
    ble_rsp_gap_set_filtering = 0;
    ble_rsp_gap_set_scan_parameters = 0;
    ble_rsp_gap_set_adv_parameters = 0;
    ble_rsp_gap_set_adv_data = 0;
    ble_rsp_gap_set_directed_connectable_mode = 0;
    ble_rsp_hardware_io_port_config_irq = 0;
    ble_rsp_hardware_set_soft_timer = 0;
    ble_rsp_hardware_adc_read = 0;
    ble_rsp_hardware_io_port_config_direction = 0;
    ble_rsp_hardware_io_port_config_function = 0;
    ble_rsp_hardware_io_port_config_pull = 0;
    ble_rsp_hardware_io_port_write = 0;
    ble_rsp_hardware_io_port_read = 0;
    ble_rsp_hardware_spi_config = 0;
    ble_rsp_hardware_spi_transfer = 0;
    ble_rsp_hardware_i2c_read = 0;
    ble_rsp_hardware_i2c_write = 0;
    ble_rsp_hardware_set_txpower = 0;
    ble_rsp_hardware_timer_comparator = 0;
    ble_rsp_test_phy_tx = 0;
    ble_rsp_test_phy_rx = 0;
    ble_rsp_test_phy_end = 0;
    ble_rsp_test_phy_reset = 0;
    ble_rsp_test_get_channel_map = 0;
    ble_rsp_test_debug = 0;

    ble_evt_system_boot = 0;
    ble_evt_system_debug = 0;
    ble_evt_system_endpoint_watermark_rx = 0;
    ble_evt_system_endpoint_watermark_tx = 0;
    ble_evt_system_script_failure = 0;
    ble_evt_flash_ps_key = 0;
    ble_evt_attributes_value = 0;
    ble_evt_attributes_user_read_request = 0;
    ble_evt_attributes_status = 0;
    ble_evt_connection_status = 0;
    ble_evt_connection_version_ind = 0;
    ble_evt_connection_feature_ind = 0;
    ble_evt_connection_raw_rx = 0;
    ble_evt_connection_disconnected = 0;
    ble_evt_attclient_indicated = 0;
    ble_evt_attclient_procedure_completed = 0;
    ble_evt_attclient_group_found = 0;
    ble_evt_attclient_attribute_found = 0;
    ble_evt_attclient_find_information_found = 0;
    ble_evt_attclient_attribute_value = 0;
    ble_evt_attclient_read_multiple_response = 0;
    ble_evt_sm_smp_data = 0;
    ble_evt_sm_bonding_fail = 0;
    ble_evt_sm_passkey_display = 0;
    ble_evt_sm_passkey_request = 0;
    ble_evt_sm_bond_status = 0;
    ble_evt_gap_scan_response = 0;
    ble_evt_gap_mode_changed = 0;
    ble_evt_hardware_io_port_status = 0;
    ble_evt_hardware_soft_timer = 0;
    ble_evt_hardware_adc_result = 0;
}

uint8_t BGLib::checkActivity(uint16_t timeout) {
    while (uModule -> available() && (timeout == 0 || millis() - timeoutStart < timeout)) {
        parse(uModule -> read());
        if (timeout > 0) timeoutStart = millis();
    }
    if (timeout > 0 && busy && millis() - timeoutStart >= timeout) {
        lastTimeout = true;
        if (onTimeout != 0) onTimeout();
        setBusy(false);
    }
    return busy;
}

uint8_t BGLib::checkError() {
    return lastError;
}
uint8_t BGLib::checkTimeout() {
    return lastTimeout;
}

void BGLib::setBusy(bool busyEnabled) {
    busy = busyEnabled;
    if (busy) {
        timeoutStart = millis();
        lastTimeout = false;
        if (onBusy) onBusy();
    } else {
        lastError = false;
        if (onIdle) onIdle();
    }
}

// set/update UART port objects
void BGLib::setModuleUART(HardwareSerial *module) {
    uModule = module;
}

void BGLib::setOutputUART(HardwareSerial *output) {
    uOutput = output;
}

uint8_t BGLib::parse(uint8_t ch, uint8_t packetMode) {
    #ifdef DEBUG
        // DEBUG: output hex value of incoming character
        if (ch < 16) Serial.write(0x30);    // leading '0'
        Serial.print(ch, HEX);              // actual hex value
        Serial.write(0x20);                 // trailing ' '
    #endif

    if (bgapiRXBufferPos == bgapiRXBufferSize) {
        // expand RX buffer to prevent overflow
        bgapiRXBuffer = (uint8_t *)realloc(bgapiRXBuffer, bgapiRXBufferSize += 16);
    }

    /*
    BGAPI packet structure (as of 2012-11-07):
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
            #ifdef DEBUG
                Serial.print("*** Packet frame sync error! Expected .0000... binary, got 0x");
                Serial.println(ch, HEX);
            #endif
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
            #ifdef DEBUG
                Serial.print("\n<- RX [ ");
                for (uint8_t i = 0; i < bgapiRXBufferPos; i++) {
                    if (bgapiRXBuffer[i] < 16) Serial.write(0x30);
                    Serial.print(bgapiRXBuffer[i], HEX);
                    Serial.write(0x20);
                }
                Serial.println("]");
            #endif

            // check packet type
            if ((bgapiRXBuffer[0] & 0x80) == 0) {
                // 0x00 = Response packet
                if (bgapiRXBuffer[2] == 0) {
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
                }
                setBusy(false);
            } else {
                // 0x80 = Event packet
                if (bgapiRXBuffer[2] == 0) {
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
                }
            }

            // reset RX packet buffer position to be ready for new packet
            bgapiRXBufferPos = 0;
        }
    }

    return 0; // parsed successfully
}

uint8_t BGLib::sendCommand(uint16_t len, uint8_t commandClass, uint8_t commandId, void *payload) {
    bgapiTXBuffer = (uint8_t *)malloc(len + 4);
    bgapiTXBuffer[0] = 0x00;
    bgapiTXBuffer[1] = (len & 0xFF);
    bgapiTXBuffer[2] = commandClass;
    bgapiTXBuffer[3] = commandId;
    lastCommand[0] = commandClass;
    lastCommand[1] = commandId;
    setBusy(true);
    if (len > 0) memcpy(bgapiTXBuffer + 4, payload, len);
    uModule -> write(bgapiTXBuffer, len + 4);
    free(bgapiTXBuffer);
    return 0;
}

uint8_t BGLib::ble_cmd_system_reset(uint8 boot_in_dfu) {
    ble_msg_system_reset_cmd_t payload;
    payload.boot_in_dfu = boot_in_dfu;
    return sendCommand(1, 0, 0, &payload);
}

uint8_t BGLib::ble_cmd_system_hello() {
    return sendCommand(0, 0, 1);
}

uint8_t BGLib::ble_cmd_system_address_get() {
    return sendCommand(0, 0, 2);
}

uint8_t BGLib::ble_cmd_system_reg_write(uint16 address, uint8 value) {
    ble_msg_system_reg_write_cmd_t payload;
    payload.address = address;
    payload.value = value;
    return sendCommand(3, 0, 3, &payload);
}

uint8_t BGLib::ble_cmd_system_reg_read(uint16 address) {
    ble_msg_system_reg_read_cmd_t payload;
    payload.address = address;
    return sendCommand(2, 0, 4, &payload);
}

uint8_t BGLib::ble_cmd_system_get_counters() {
    return sendCommand(0, 0, 5);
}

uint8_t BGLib::ble_cmd_system_get_connections() {
    return sendCommand(0, 0, 6);
}

uint8_t BGLib::ble_cmd_system_read_memory(uint32 address, uint8 length) {
    ble_msg_system_read_memory_cmd_t payload;
    payload.address = address;
    payload.length = length;
    return sendCommand(5, 0, 7, &payload);
}

uint8_t BGLib::ble_cmd_system_get_info() {
    return sendCommand(0, 0, 8);
}

uint8_t BGLib::ble_cmd_system_endpoint_tx(uint8 endpoint, uint8array data) {
    ble_msg_system_endpoint_tx_cmd_t payload;
    payload.endpoint = endpoint;
    payload.data = data;
    return sendCommand(2 + data.len, 0, 9, &payload);
}

uint8_t BGLib::ble_cmd_system_whitelist_append(bd_addr address, uint8 address_type) {
    ble_msg_system_whitelist_append_cmd_t payload;
    payload.address = address;
    payload.address_type = address_type;
    return sendCommand(7, 0, 10, &payload);
}

uint8_t BGLib::ble_cmd_system_whitelist_remove(bd_addr address, uint8 address_type) {
    ble_msg_system_whitelist_remove_cmd_t payload;
    payload.address = address;
    payload.address_type = address_type;
    return sendCommand(7, 0, 11, &payload);
}

uint8_t BGLib::ble_cmd_system_whitelist_clear() {
    return sendCommand(0, 0, 12);
}

uint8_t BGLib::ble_cmd_system_endpoint_rx(uint8 endpoint, uint8 size) {
    ble_msg_system_endpoint_rx_cmd_t payload;
    payload.endpoint = endpoint;
    payload.size = size;
    return sendCommand(2, 0, 13, &payload);
}

uint8_t BGLib::ble_cmd_system_endpoint_set_watermarks(uint8 endpoint, uint8 rx, uint8 tx) {
    ble_msg_system_endpoint_set_watermarks_cmd_t payload;
    payload.endpoint = endpoint;
    payload.rx = rx;
    payload.tx = tx;
    return sendCommand(3, 0, 14, &payload);
}

uint8_t BGLib::ble_cmd_flash_ps_defrag() {
    return sendCommand(0, 1, 0);
}

uint8_t BGLib::ble_cmd_flash_ps_dump() {
    return sendCommand(0, 1, 1);
}

uint8_t BGLib::ble_cmd_flash_ps_erase_all() {
    return sendCommand(0, 1, 2);
}

uint8_t BGLib::ble_cmd_flash_ps_save(uint16 key, uint8array value) {
    ble_msg_flash_ps_save_cmd_t payload;
    payload.key = key;
    payload.value = value;
    return sendCommand(3 + value.len, 1, 3, &payload);
}

uint8_t BGLib::ble_cmd_flash_ps_load(uint16 key) {
    ble_msg_flash_ps_load_cmd_t payload;
    payload.key = key;
    return sendCommand(2, 1, 4, &payload);
}

uint8_t BGLib::ble_cmd_flash_ps_erase(uint16 key) {
    ble_msg_flash_ps_erase_cmd_t payload;
    payload.key = key;
    return sendCommand(2, 1, 5, &payload);
}

uint8_t BGLib::ble_cmd_flash_erase_page(uint8 page) {
    ble_msg_flash_erase_page_cmd_t payload;
    payload.page = page;
    return sendCommand(1, 1, 6, &payload);
}

uint8_t BGLib::ble_cmd_flash_write_words(uint16 address, uint8array words) {
    ble_msg_flash_write_words_cmd_t payload;
    payload.address = address;
    payload.words = words;
    return sendCommand(3 + words.len, 1, 7, &payload);
}

uint8_t BGLib::ble_cmd_attributes_write(uint16 handle, uint8 offset, uint8array value) {
    ble_msg_attributes_write_cmd_t payload;
    payload.handle = handle;
    payload.offset = offset;
    payload.value = value;
    return sendCommand(4 + value.len, 2, 0, &payload);
}

uint8_t BGLib::ble_cmd_attributes_read(uint16 handle, uint16 offset) {
    ble_msg_attributes_read_cmd_t payload;
    payload.handle = handle;
    payload.offset = offset;
    return sendCommand(4, 2, 1, &payload);
}

uint8_t BGLib::ble_cmd_attributes_read_type(uint16 handle) {
    ble_msg_attributes_read_type_cmd_t payload;
    payload.handle = handle;
    return sendCommand(2, 2, 2, &payload);
}

uint8_t BGLib::ble_cmd_attributes_user_read_response(uint8 connection, uint8 att_error, uint8array value) {
    ble_msg_attributes_user_read_response_cmd_t payload;
    payload.connection = connection;
    payload.att_error = att_error;
    payload.value = value;
    return sendCommand(3 + value.len, 2, 3, &payload);
}

uint8_t BGLib::ble_cmd_attributes_user_write_response(uint8 connection, uint8 att_error) {
    ble_msg_attributes_user_write_response_cmd_t payload;
    payload.connection = connection;
    payload.att_error = att_error;
    return sendCommand(2, 2, 4, &payload);
}

uint8_t BGLib::ble_cmd_connection_disconnect(uint8 connection) {
    ble_msg_connection_disconnect_cmd_t payload;
    payload.connection = connection;
    return sendCommand(1, 3, 0, &payload);
}

uint8_t BGLib::ble_cmd_connection_get_rssi(uint8 connection) {
    ble_msg_connection_get_rssi_cmd_t payload;
    payload.connection = connection;
    return sendCommand(1, 3, 1, &payload);
}

uint8_t BGLib::ble_cmd_connection_update(uint8 connection, uint16 interval_min, uint16 interval_max, uint16 latency, uint16 timeout) {
    ble_msg_connection_update_cmd_t payload;
    payload.connection = connection;
    payload.interval_min = interval_min;
    payload.interval_max = interval_max;
    payload.latency = latency;
    payload.timeout = timeout;
    return sendCommand(9, 3, 2, &payload);
}

uint8_t BGLib::ble_cmd_connection_version_update(uint8 connection) {
    ble_msg_connection_version_update_cmd_t payload;
    payload.connection = connection;
    return sendCommand(1, 3, 3, &payload);
}

uint8_t BGLib::ble_cmd_connection_channel_map_get(uint8 connection) {
    ble_msg_connection_channel_map_get_cmd_t payload;
    payload.connection = connection;
    return sendCommand(1, 3, 4, &payload);
}

uint8_t BGLib::ble_cmd_connection_channel_map_set(uint8 connection, uint8array map) {
    ble_msg_connection_channel_map_set_cmd_t payload;
    payload.connection = connection;
    payload.map = map;
    return sendCommand(2 + map.len, 3, 5, &payload);
}

uint8_t BGLib::ble_cmd_connection_features_get(uint8 connection) {
    ble_msg_connection_features_get_cmd_t payload;
    payload.connection = connection;
    return sendCommand(1, 3, 6, &payload);
}

uint8_t BGLib::ble_cmd_connection_get_status(uint8 connection) {
    ble_msg_connection_get_status_cmd_t payload;
    payload.connection = connection;
    return sendCommand(1, 3, 7, &payload);
}

uint8_t BGLib::ble_cmd_connection_raw_tx(uint8 connection, uint8array data) {
    ble_msg_connection_raw_tx_cmd_t payload;
    payload.connection = connection;
    payload.data = data;
    return sendCommand(2 + data.len, 3, 8, &payload);
}

uint8_t BGLib::ble_cmd_attclient_find_by_type_value(uint8 connection, uint16 start, uint16 end, uint16 uuid, uint8array value) {
    ble_msg_attclient_find_by_type_value_cmd_t payload;
    payload.connection = connection;
    payload.start = start;
    payload.end = end;
    payload.uuid = uuid;
    payload.value = value;
    return sendCommand(8 + value.len, 4, 0, &payload);
}

uint8_t BGLib::ble_cmd_attclient_read_by_group_type(uint8 connection, uint16 start, uint16 end, uint8array uuid) {
    ble_msg_attclient_read_by_group_type_cmd_t payload;
    payload.connection = connection;
    payload.start = start;
    payload.end = end;
    payload.uuid = uuid;
    return sendCommand(6 + uuid.len, 4, 1, &payload);
}

uint8_t BGLib::ble_cmd_attclient_read_by_type(uint8 connection, uint16 start, uint16 end, uint8array uuid) {
    ble_msg_attclient_read_by_type_cmd_t payload;
    payload.connection = connection;
    payload.start = start;
    payload.end = end;
    payload.uuid = uuid;
    return sendCommand(6 + uuid.len, 4, 2, &payload);
}

uint8_t BGLib::ble_cmd_attclient_find_information(uint8 connection, uint16 start, uint16 end) {
    ble_msg_attclient_find_information_cmd_t payload;
    payload.connection = connection;
    payload.start = start;
    payload.end = end;
    return sendCommand(5, 4, 3, &payload);
}

uint8_t BGLib::ble_cmd_attclient_read_by_handle(uint8 connection, uint16 chrhandle) {
    ble_msg_attclient_read_by_handle_cmd_t payload;
    payload.connection = connection;
    payload.chrhandle = chrhandle;
    return sendCommand(3, 4, 4, &payload);
}

uint8_t BGLib::ble_cmd_attclient_attribute_write(uint8 connection, uint16 atthandle, uint8array data) {
    ble_msg_attclient_attribute_write_cmd_t payload;
    payload.connection = connection;
    payload.atthandle = atthandle;
    payload.data = data;
    return sendCommand(4 + data.len, 4, 5, &payload);
}

uint8_t BGLib::ble_cmd_attclient_write_command(uint8 connection, uint16 atthandle, uint8array data) {
    ble_msg_attclient_write_command_cmd_t payload;
    payload.connection = connection;
    payload.atthandle = atthandle;
    payload.data = data;
    return sendCommand(4 + data.len, 4, 6, &payload);
}

uint8_t BGLib::ble_cmd_attclient_indicate_confirm(uint8 connection) {
    ble_msg_attclient_indicate_confirm_cmd_t payload;
    payload.connection = connection;
    return sendCommand(1, 4, 7, &payload);
}

uint8_t BGLib::ble_cmd_attclient_read_long(uint8 connection, uint16 chrhandle) {
    ble_msg_attclient_read_long_cmd_t payload;
    payload.connection = connection;
    payload.chrhandle = chrhandle;
    return sendCommand(3, 4, 8, &payload);
}

uint8_t BGLib::ble_cmd_attclient_prepare_write(uint8 connection, uint16 atthandle, uint16 offset, uint8array data) {
    ble_msg_attclient_prepare_write_cmd_t payload;
    payload.connection = connection;
    payload.atthandle = atthandle;
    payload.offset = offset;
    payload.data = data;
    return sendCommand(6 + data.len, 4, 9, &payload);
}

uint8_t BGLib::ble_cmd_attclient_execute_write(uint8 connection, uint8 commit) {
    ble_msg_attclient_execute_write_cmd_t payload;
    payload.connection = connection;
    payload.commit = commit;
    return sendCommand(2, 4, 10, &payload);
}

uint8_t BGLib::ble_cmd_attclient_read_multiple(uint8 connection, uint8array handles) {
    ble_msg_attclient_read_multiple_cmd_t payload;
    payload.connection = connection;
    payload.handles = handles;
    return sendCommand(2 + handles.len, 4, 11, &payload);
}

uint8_t BGLib::ble_cmd_sm_encrypt_start(uint8 handle, uint8 bonding) {
    ble_msg_sm_encrypt_start_cmd_t payload;
    payload.handle = handle;
    payload.bonding = bonding;
    return sendCommand(2, 5, 0, &payload);
}

uint8_t BGLib::ble_cmd_sm_set_bondable_mode(uint8 bondable) {
    ble_msg_sm_set_bondable_mode_cmd_t payload;
    payload.bondable = bondable;
    return sendCommand(1, 5, 1, &payload);
}

uint8_t BGLib::ble_cmd_sm_delete_bonding(uint8 handle) {
    ble_msg_sm_delete_bonding_cmd_t payload;
    payload.handle = handle;
    return sendCommand(1, 5, 2, &payload);
}

uint8_t BGLib::ble_cmd_sm_set_parameters(uint8 mitm, uint8 min_key_size, uint8 io_capabilities) {
    ble_msg_sm_set_parameters_cmd_t payload;
    payload.mitm = mitm;
    payload.min_key_size = min_key_size;
    payload.io_capabilities = io_capabilities;
    return sendCommand(3, 5, 3, &payload);
}

uint8_t BGLib::ble_cmd_sm_passkey_entry(uint8 handle, uint32 passkey) {
    ble_msg_sm_passkey_entry_cmd_t payload;
    payload.handle = handle;
    payload.passkey = passkey;
    return sendCommand(5, 5, 4, &payload);
}

uint8_t BGLib::ble_cmd_sm_get_bonds() {
    return sendCommand(0, 5, 5);
}

uint8_t BGLib::ble_cmd_sm_set_oob_data(uint8array oob) {
    ble_msg_sm_set_oob_data_cmd_t payload;
    payload.oob = oob;
    return sendCommand(1 + oob.len, 5, 6, &payload);
}

uint8_t BGLib::ble_cmd_gap_set_privacy_flags(uint8 peripheral_privacy, uint8 central_privacy) {
    ble_msg_gap_set_privacy_flags_cmd_t payload;
    payload.peripheral_privacy = peripheral_privacy;
    payload.central_privacy = central_privacy;
    return sendCommand(2, 6, 0, &payload);
}

uint8_t BGLib::ble_cmd_gap_set_mode(uint8 discover, uint8 connect) {
    ble_msg_gap_set_mode_cmd_t payload;
    payload.discover = discover;
    payload.connect = connect;
    return sendCommand(2, 6, 1, &payload);
}

uint8_t BGLib::ble_cmd_gap_discover(uint8 mode) {
    ble_msg_gap_discover_cmd_t payload;
    payload.mode = mode;
    return sendCommand(1, 6, 2, &payload);
}

uint8_t BGLib::ble_cmd_gap_connect_direct(bd_addr address, uint8 addr_type, uint16 conn_interval_min, uint16 conn_interval_max, uint16 timeout, uint16 latency) {
    ble_msg_gap_connect_direct_cmd_t payload;
    payload.address = address;
    payload.addr_type = addr_type;
    payload.conn_interval_min = conn_interval_min;
    payload.conn_interval_max = conn_interval_max;
    payload.timeout = timeout;
    payload.latency = latency;
    return sendCommand(15, 6, 3, &payload);
}

uint8_t BGLib::ble_cmd_gap_end_procedure() {
    return sendCommand(0, 6, 4);
}

uint8_t BGLib::ble_cmd_gap_connect_selective(uint16 conn_interval_min, uint16 conn_interval_max, uint16 timeout, uint16 latency) {
    ble_msg_gap_connect_selective_cmd_t payload;
    payload.conn_interval_min = conn_interval_min;
    payload.conn_interval_max = conn_interval_max;
    payload.timeout = timeout;
    payload.latency = latency;
    return sendCommand(8, 6, 5, &payload);
}

uint8_t BGLib::ble_cmd_gap_set_filtering(uint8 scan_policy, uint8 adv_policy, uint8 scan_duplicate_filtering) {
    ble_msg_gap_set_filtering_cmd_t payload;
    payload.scan_policy = scan_policy;
    payload.adv_policy = adv_policy;
    payload.scan_duplicate_filtering = scan_duplicate_filtering;
    return sendCommand(3, 6, 6, &payload);
}

uint8_t BGLib::ble_cmd_gap_set_scan_parameters(uint16 scan_interval, uint16 scan_window, uint8 active) {
    ble_msg_gap_set_scan_parameters_cmd_t payload;
    payload.scan_interval = scan_interval;
    payload.scan_window = scan_window;
    payload.active = active;
    return sendCommand(5, 6, 7, &payload);
}

uint8_t BGLib::ble_cmd_gap_set_adv_parameters(uint16 adv_interval_min, uint16 adv_interval_max, uint8 adv_channels) {
    ble_msg_gap_set_adv_parameters_cmd_t payload;
    payload.adv_interval_min = adv_interval_min;
    payload.adv_interval_max = adv_interval_max;
    payload.adv_channels = adv_channels;
    return sendCommand(5, 6, 8, &payload);
}

uint8_t BGLib::ble_cmd_gap_set_adv_data(uint8 set_scanrsp, uint8array adv_data) {
    ble_msg_gap_set_adv_data_cmd_t payload;
    payload.set_scanrsp = set_scanrsp;
    payload.adv_data = adv_data;
    return sendCommand(2 + adv_data.len, 6, 9, &payload);
}

uint8_t BGLib::ble_cmd_gap_set_directed_connectable_mode(bd_addr address, uint8 addr_type) {
    ble_msg_gap_set_directed_connectable_mode_cmd_t payload;
    payload.address = address;
    payload.addr_type = addr_type;
    return sendCommand(7, 6, 10, &payload);
}

uint8_t BGLib::ble_cmd_hardware_io_port_config_irq(uint8 port, uint8 enable_bits, uint8 falling_edge) {
    ble_msg_hardware_io_port_config_irq_cmd_t payload;
    payload.port = port;
    payload.enable_bits = enable_bits;
    payload.falling_edge = falling_edge;
    return sendCommand(3, 7, 0, &payload);
}

uint8_t BGLib::ble_cmd_hardware_set_soft_timer(uint32 time, uint8 handle, uint8 single_shot) {
    ble_msg_hardware_set_soft_timer_cmd_t payload;
    payload.time = time;
    payload.handle = handle;
    payload.single_shot = single_shot;
    return sendCommand(6, 7, 1, &payload);
}

uint8_t BGLib::ble_cmd_hardware_adc_read(uint8 input, uint8 decimation, uint8 reference_selection) {
    ble_msg_hardware_adc_read_cmd_t payload;
    payload.input = input;
    payload.decimation = decimation;
    payload.reference_selection = reference_selection;
    return sendCommand(3, 7, 2, &payload);
}

uint8_t BGLib::ble_cmd_hardware_io_port_config_direction(uint8 port, uint8 direction) {
    ble_msg_hardware_io_port_config_direction_cmd_t payload;
    payload.port = port;
    payload.direction = direction;
    return sendCommand(2, 7, 3, &payload);
}

uint8_t BGLib::ble_cmd_hardware_io_port_config_function(uint8 port, uint8 function) {
    ble_msg_hardware_io_port_config_function_cmd_t payload;
    payload.port = port;
    payload.function = function;
    return sendCommand(2, 7, 4, &payload);
}

uint8_t BGLib::ble_cmd_hardware_io_port_config_pull(uint8 port, uint8 tristate_mask, uint8 pull_up) {
    ble_msg_hardware_io_port_config_pull_cmd_t payload;
    payload.port = port;
    payload.tristate_mask = tristate_mask;
    payload.pull_up = pull_up;
    return sendCommand(3, 7, 5, &payload);
}

uint8_t BGLib::ble_cmd_hardware_io_port_write(uint8 port, uint8 mask, uint8 data) {
    ble_msg_hardware_io_port_write_cmd_t payload;
    payload.port = port;
    payload.mask = mask;
    payload.data = data;
    return sendCommand(3, 7, 6, &payload);
}

uint8_t BGLib::ble_cmd_hardware_io_port_read(uint8 port, uint8 mask) {
    ble_msg_hardware_io_port_read_cmd_t payload;
    payload.port = port;
    payload.mask = mask;
    return sendCommand(2, 7, 7, &payload);
}

uint8_t BGLib::ble_cmd_hardware_spi_config(uint8 channel, uint8 polarity, uint8 phase, uint8 bit_order, uint8 baud_e, uint8 baud_m) {
    ble_msg_hardware_spi_config_cmd_t payload;
    payload.channel = channel;
    payload.polarity = polarity;
    payload.phase = phase;
    payload.bit_order = bit_order;
    payload.baud_e = baud_e;
    payload.baud_m = baud_m;
    return sendCommand(6, 7, 8, &payload);
}

uint8_t BGLib::ble_cmd_hardware_spi_transfer(uint8 channel, uint8array data) {
    ble_msg_hardware_spi_transfer_cmd_t payload;
    payload.channel = channel;
    payload.data = data;
    return sendCommand(2 + data.len, 7, 9, &payload);
}

uint8_t BGLib::ble_cmd_hardware_i2c_read(uint8 address, uint8 stop, uint8 length) {
    ble_msg_hardware_i2c_read_cmd_t payload;
    payload.address = address;
    payload.stop = stop;
    payload.length = length;
    return sendCommand(3, 7, 10, &payload);
}

uint8_t BGLib::ble_cmd_hardware_i2c_write(uint8 address, uint8 stop, uint8array data) {
    ble_msg_hardware_i2c_write_cmd_t payload;
    payload.address = address;
    payload.stop = stop;
    payload.data = data;
    return sendCommand(3 + data.len, 7, 11, &payload);
}

uint8_t BGLib::ble_cmd_hardware_set_txpower(int8 power) {
    ble_msg_hardware_set_txpower_cmd_t payload;
    payload.power = power;
    return sendCommand(1, 7, 12, &payload);
}

uint8_t BGLib::ble_cmd_hardware_timer_comparator(uint8 timer, uint8 channel, uint8 mode, uint16 comparator_value) {
    ble_msg_hardware_timer_comparator_cmd_t payload;
    payload.timer = timer;
    payload.channel = channel;
    payload.mode = mode;
    payload.comparator_value = comparator_value;
    return sendCommand(5, 7, 13, &payload);
}

uint8_t BGLib::ble_cmd_test_phy_tx(uint8 channel, uint8 length, uint8 type) {
    ble_msg_test_phy_tx_cmd_t payload;
    payload.channel = channel;
    payload.length = length;
    payload.type = type;
    return sendCommand(3, 8, 0, &payload);
}

uint8_t BGLib::ble_cmd_test_phy_rx(uint8 channel) {
    ble_msg_test_phy_rx_cmd_t payload;
    payload.channel = channel;
    return sendCommand(1, 8, 1, &payload);
}

uint8_t BGLib::ble_cmd_test_phy_end() {
    return sendCommand(0, 8, 2);
}

uint8_t BGLib::ble_cmd_test_phy_reset() {
    return sendCommand(0, 8, 3);
}

uint8_t BGLib::ble_cmd_test_get_channel_map() {
    return sendCommand(0, 8, 4);
}

uint8_t BGLib::ble_cmd_test_debug(uint8array input) {
    ble_msg_test_debug_cmd_t payload;
    payload.input = input;
    return sendCommand(1 + input.len, 8, 5, &payload);
}
