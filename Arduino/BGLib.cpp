// Arduino BGLib code library source file

#include "BGLib.h"

BGLib::BGLib(HardwareSerial *module, HardwareSerial *output, uint8_t pMode) {
    uModule = module;
    uOutput = output;
    packetMode = pMode;

    // initialize packet buffers
    bgapiRXBuffer = (uint8_t *)malloc(bgapiRXBufferSize = 32);
    bgapiTXBuffer = (uint8_t *)malloc(bgapiTXBufferSize = 32);
    bgapiRXBufferPos = bgapiTXBufferPos = 0;

    onBusy = 0;
    onIdle = 0;
    onTimeout = 0;
    onBeforeTXCommand = 0;
    onTXCommandComplete = 0;

    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_RESET
        ble_rsp_system_reset = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_HELLO
        ble_rsp_system_hello = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_ADDRESS_GET
        ble_rsp_system_address_get = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_REG_WRITE
        ble_rsp_system_reg_write = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_REG_READ
        ble_rsp_system_reg_read = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_GET_COUNTERS
        ble_rsp_system_get_counters = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_GET_CONNECTIONS
        ble_rsp_system_get_connections = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_READ_MEMORY
        ble_rsp_system_read_memory = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_GET_INFO
        ble_rsp_system_get_info = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_ENDPOINT_TX
        ble_rsp_system_endpoint_tx = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_WHITELIST_APPEND
        ble_rsp_system_whitelist_append = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_WHITELIST_REMOVE
        ble_rsp_system_whitelist_remove = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_WHITELIST_CLEAR
        ble_rsp_system_whitelist_clear = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_ENDPOINT_RX
        ble_rsp_system_endpoint_rx = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_ENDPOINT_SET_WATERMARKS
        ble_rsp_system_endpoint_set_watermarks = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_DEFRAG
        ble_rsp_flash_ps_defrag = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_DUMP
        ble_rsp_flash_ps_dump = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_ERASE_ALL
        ble_rsp_flash_ps_erase_all = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_SAVE
        ble_rsp_flash_ps_save = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_LOAD
        ble_rsp_flash_ps_load = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_ERASE
        ble_rsp_flash_ps_erase = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_FLASH_ERASE_PAGE
        ble_rsp_flash_erase_page = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_FLASH_WRITE_WORDS
        ble_rsp_flash_write_words = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_WRITE
        ble_rsp_attributes_write = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_READ
        ble_rsp_attributes_read = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_READ_TYPE
        ble_rsp_attributes_read_type = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_USER_READ_RESPONSE
        ble_rsp_attributes_user_read_response = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_USER_WRITE_RESPONSE
        ble_rsp_attributes_user_write_response = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_DISCONNECT
        ble_rsp_connection_disconnect = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_GET_RSSI
        ble_rsp_connection_get_rssi = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_UPDATE
        ble_rsp_connection_update = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_VERSION_UPDATE
        ble_rsp_connection_version_update = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_CHANNEL_MAP_GET
        ble_rsp_connection_channel_map_get = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_CHANNEL_MAP_SET
        ble_rsp_connection_channel_map_set = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_FEATURES_GET
        ble_rsp_connection_features_get = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_GET_STATUS
        ble_rsp_connection_get_status = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_RAW_TX
        ble_rsp_connection_raw_tx = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_FIND_BY_TYPE_VALUE
        ble_rsp_attclient_find_by_type_value = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_BY_GROUP_TYPE
        ble_rsp_attclient_read_by_group_type = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_BY_TYPE
        ble_rsp_attclient_read_by_type = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_FIND_INFORMATION
        ble_rsp_attclient_find_information = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_BY_HANDLE
        ble_rsp_attclient_read_by_handle = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_ATTRIBUTE_WRITE
        ble_rsp_attclient_attribute_write = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_WRITE_COMMAND
        ble_rsp_attclient_write_command = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_INDICATE_CONFIRM
        ble_rsp_attclient_indicate_confirm = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_LONG
        ble_rsp_attclient_read_long = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_PREPARE_WRITE
        ble_rsp_attclient_prepare_write = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_EXECUTE_WRITE
        ble_rsp_attclient_execute_write = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_MULTIPLE
        ble_rsp_attclient_read_multiple = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SM_ENCRYPT_START
        ble_rsp_sm_encrypt_start = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SM_SET_BONDABLE_MODE
        ble_rsp_sm_set_bondable_mode = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SM_DELETE_BONDING
        ble_rsp_sm_delete_bonding = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SM_SET_PARAMETERS
        ble_rsp_sm_set_parameters = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SM_PASSKEY_ENTRY
        ble_rsp_sm_passkey_entry = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SM_GET_BONDS
        ble_rsp_sm_get_bonds = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_SM_SET_OOB_DATA
        ble_rsp_sm_set_oob_data = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_PRIVACY_FLAGS
        ble_rsp_gap_set_privacy_flags = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_MODE
        ble_rsp_gap_set_mode = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_GAP_DISCOVER
        ble_rsp_gap_discover = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_GAP_CONNECT_DIRECT
        ble_rsp_gap_connect_direct = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_GAP_END_PROCEDURE
        ble_rsp_gap_end_procedure = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_GAP_CONNECT_SELECTIVE
        ble_rsp_gap_connect_selective = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_FILTERING
        ble_rsp_gap_set_filtering = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_SCAN_PARAMETERS
        ble_rsp_gap_set_scan_parameters = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_ADV_PARAMETERS
        ble_rsp_gap_set_adv_parameters = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_ADV_DATA
        ble_rsp_gap_set_adv_data = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_DIRECTED_CONNECTABLE_MODE
        ble_rsp_gap_set_directed_connectable_mode = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_CONFIG_IRQ
        ble_rsp_hardware_io_port_config_irq = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_SET_SOFT_TIMER
        ble_rsp_hardware_set_soft_timer = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_ADC_READ
        ble_rsp_hardware_adc_read = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_CONFIG_DIRECTION
        ble_rsp_hardware_io_port_config_direction = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_CONFIG_FUNCTION
        ble_rsp_hardware_io_port_config_function = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_CONFIG_PULL
        ble_rsp_hardware_io_port_config_pull = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_WRITE
        ble_rsp_hardware_io_port_write = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_READ
        ble_rsp_hardware_io_port_read = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_SPI_CONFIG
        ble_rsp_hardware_spi_config = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_SPI_TRANSFER
        ble_rsp_hardware_spi_transfer = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_I2C_READ
        ble_rsp_hardware_i2c_read = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_I2C_WRITE
        ble_rsp_hardware_i2c_write = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_SET_TXPOWER
        ble_rsp_hardware_set_txpower = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_TIMER_COMPARATOR
        ble_rsp_hardware_timer_comparator = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_TEST_PHY_TX
        ble_rsp_test_phy_tx = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_TEST_PHY_RX
        ble_rsp_test_phy_rx = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_TEST_PHY_END
        ble_rsp_test_phy_end = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_TEST_PHY_RESET
        ble_rsp_test_phy_reset = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_TEST_GET_CHANNEL_MAP
        ble_rsp_test_get_channel_map = 0;
    #endif
    #ifdef BGLIB_ENABLE_COMMAND_TEST_DEBUG
        ble_rsp_test_debug = 0;
    #endif

    #ifdef BGLIB_ENABLE_EVENT_SYSTEM_BOOT
        ble_evt_system_boot = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_SYSTEM_DEBUG
        ble_evt_system_debug = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_SYSTEM_ENDPOINT_WATERMARK_RX
        ble_evt_system_endpoint_watermark_rx = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_SYSTEM_ENDPOINT_WATERMARK_TX
        ble_evt_system_endpoint_watermark_tx = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_SYSTEM_SCRIPT_FAILURE
        ble_evt_system_script_failure = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_SYSTEM_NO_LICENSE_KEY
        ble_evt_system_no_license_key = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_FLASH_PS_KEY
        ble_evt_flash_ps_key = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_ATTRIBUTES_VALUE
        ble_evt_attributes_value = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_ATTRIBUTES_USER_READ_REQUEST
        ble_evt_attributes_user_read_request = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_ATTRIBUTES_STATUS
        ble_evt_attributes_status = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_CONNECTION_STATUS
        ble_evt_connection_status = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_CONNECTION_VERSION_IND
        ble_evt_connection_version_ind = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_CONNECTION_FEATURE_IND
        ble_evt_connection_feature_ind = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_CONNECTION_RAW_RX
        ble_evt_connection_raw_rx = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_CONNECTION_DISCONNECTED
        ble_evt_connection_disconnected = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_INDICATED
        ble_evt_attclient_indicated = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_PROCEDURE_COMPLETED
        ble_evt_attclient_procedure_completed = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_GROUP_FOUND
        ble_evt_attclient_group_found = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_ATTRIBUTE_FOUND
        ble_evt_attclient_attribute_found = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_FIND_INFORMATION_FOUND
        ble_evt_attclient_find_information_found = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_ATTRIBUTE_VALUE
        ble_evt_attclient_attribute_value = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_READ_MULTIPLE_RESPONSE
        ble_evt_attclient_read_multiple_response = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_SM_SMP_DATA
        ble_evt_sm_smp_data = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_SM_BONDING_FAIL
        ble_evt_sm_bonding_fail = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_SM_PASSKEY_DISPLAY
        ble_evt_sm_passkey_display = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_SM_PASSKEY_REQUEST
        ble_evt_sm_passkey_request = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_SM_BOND_STATUS
        ble_evt_sm_bond_status = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_GAP_SCAN_RESPONSE
        ble_evt_gap_scan_response = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_GAP_MODE_CHANGED
        ble_evt_gap_mode_changed = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_HARDWARE_IO_PORT_STATUS
        ble_evt_hardware_io_port_status = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_HARDWARE_SOFT_TIMER
        ble_evt_hardware_soft_timer = 0;
    #endif
    #ifdef BGLIB_ENABLE_EVENT_HARDWARE_ADC_RESULT
        ble_evt_hardware_adc_result = 0;
    #endif
}

uint8_t BGLib::checkActivity(uint16_t timeout) {
    uint16_t ch;
    while ((ch = uModule -> read()) < 256 && (timeout == 0 || millis() - timeoutStart < timeout)) {
        parse(ch);
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

uint8_t *BGLib::getLastCommand() {
    return lastCommand;
}
uint8_t *BGLib::getLastResponse() {
    return lastResponse;
}
uint8_t *BGLib::getLastEvent() {
    return lastEvent;
}
void *BGLib::getLastRXPayload() {
    return bgapiRXBuffer + 4;
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
                Serial.print("\n<=[ ");
                for (uint8_t i = 0; i < bgapiRXBufferPos; i++) {
                    if (bgapiRXBuffer[i] < 16) Serial.write(0x30);
                    Serial.print(bgapiRXBuffer[i], HEX);
                    Serial.write(0x20);
                }
                Serial.println("]");
            #endif

            // reset RX packet buffer position to be ready for new packet
            bgapiRXBufferPos = 0;

            // check packet type
            if ((bgapiRXBuffer[0] & 0x80) == 0) {
                // 0x00 = Response packet

                // capture last response class/command bytes
                lastResponse[0] = bgapiRXBuffer[2];
                lastResponse[1] = bgapiRXBuffer[3];

                if (bgapiRXBuffer[2] == 0) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_RESET
                        else if (bgapiRXBuffer[3] == 0) { if (ble_rsp_system_reset) ble_rsp_system_reset((const struct ble_msg_system_reset_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_HELLO
                        else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_system_hello) ble_rsp_system_hello((const struct ble_msg_system_hello_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_ADDRESS_GET
                        else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_system_address_get) ble_rsp_system_address_get((const struct ble_msg_system_address_get_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_REG_WRITE
                        else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_system_reg_write) ble_rsp_system_reg_write((const struct ble_msg_system_reg_write_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_REG_READ
                        else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_system_reg_read) ble_rsp_system_reg_read((const struct ble_msg_system_reg_read_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_GET_COUNTERS
                        else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_system_get_counters) ble_rsp_system_get_counters((const struct ble_msg_system_get_counters_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_GET_CONNECTIONS
                        else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_system_get_connections) ble_rsp_system_get_connections((const struct ble_msg_system_get_connections_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_READ_MEMORY
                        else if (bgapiRXBuffer[3] == 7) { if (ble_rsp_system_read_memory) ble_rsp_system_read_memory((const struct ble_msg_system_read_memory_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_GET_INFO
                        else if (bgapiRXBuffer[3] == 8) { if (ble_rsp_system_get_info) ble_rsp_system_get_info((const struct ble_msg_system_get_info_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_ENDPOINT_TX
                        else if (bgapiRXBuffer[3] == 9) { if (ble_rsp_system_endpoint_tx) ble_rsp_system_endpoint_tx((const struct ble_msg_system_endpoint_tx_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_WHITELIST_APPEND
                        else if (bgapiRXBuffer[3] == 10) { if (ble_rsp_system_whitelist_append) ble_rsp_system_whitelist_append((const struct ble_msg_system_whitelist_append_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_WHITELIST_REMOVE
                        else if (bgapiRXBuffer[3] == 11) { if (ble_rsp_system_whitelist_remove) ble_rsp_system_whitelist_remove((const struct ble_msg_system_whitelist_remove_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_WHITELIST_CLEAR
                        else if (bgapiRXBuffer[3] == 12) { if (ble_rsp_system_whitelist_clear) ble_rsp_system_whitelist_clear((const struct ble_msg_system_whitelist_clear_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_ENDPOINT_RX
                        else if (bgapiRXBuffer[3] == 13) { if (ble_rsp_system_endpoint_rx) ble_rsp_system_endpoint_rx((const struct ble_msg_system_endpoint_rx_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SYSTEM_ENDPOINT_SET_WATERMARKS
                        else if (bgapiRXBuffer[3] == 14) { if (ble_rsp_system_endpoint_set_watermarks) ble_rsp_system_endpoint_set_watermarks((const struct ble_msg_system_endpoint_set_watermarks_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 1) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_DEFRAG
                        else if (bgapiRXBuffer[3] == 0) { if (ble_rsp_flash_ps_defrag) ble_rsp_flash_ps_defrag((const struct ble_msg_flash_ps_defrag_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_DUMP
                        else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_flash_ps_dump) ble_rsp_flash_ps_dump((const struct ble_msg_flash_ps_dump_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_ERASE_ALL
                        else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_flash_ps_erase_all) ble_rsp_flash_ps_erase_all((const struct ble_msg_flash_ps_erase_all_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_SAVE
                        else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_flash_ps_save) ble_rsp_flash_ps_save((const struct ble_msg_flash_ps_save_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_LOAD
                        else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_flash_ps_load) ble_rsp_flash_ps_load((const struct ble_msg_flash_ps_load_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_ERASE
                        else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_flash_ps_erase) ble_rsp_flash_ps_erase((const struct ble_msg_flash_ps_erase_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_FLASH_ERASE_PAGE
                        else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_flash_erase_page) ble_rsp_flash_erase_page((const struct ble_msg_flash_erase_page_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_FLASH_WRITE_WORDS
                        else if (bgapiRXBuffer[3] == 7) { if (ble_rsp_flash_write_words) ble_rsp_flash_write_words((const struct ble_msg_flash_write_words_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 2) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_WRITE
                        else if (bgapiRXBuffer[3] == 0) { if (ble_rsp_attributes_write) ble_rsp_attributes_write((const struct ble_msg_attributes_write_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_READ
                        else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_attributes_read) ble_rsp_attributes_read((const struct ble_msg_attributes_read_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_READ_TYPE
                        else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_attributes_read_type) ble_rsp_attributes_read_type((const struct ble_msg_attributes_read_type_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_USER_READ_RESPONSE
                        else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_attributes_user_read_response) ble_rsp_attributes_user_read_response((const struct ble_msg_attributes_user_read_response_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_USER_WRITE_RESPONSE
                        else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_attributes_user_write_response) ble_rsp_attributes_user_write_response((const struct ble_msg_attributes_user_write_response_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 3) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_DISCONNECT
                        else if (bgapiRXBuffer[3] == 0) { if (ble_rsp_connection_disconnect) ble_rsp_connection_disconnect((const struct ble_msg_connection_disconnect_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_GET_RSSI
                        else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_connection_get_rssi) ble_rsp_connection_get_rssi((const struct ble_msg_connection_get_rssi_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_UPDATE
                        else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_connection_update) ble_rsp_connection_update((const struct ble_msg_connection_update_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_VERSION_UPDATE
                        else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_connection_version_update) ble_rsp_connection_version_update((const struct ble_msg_connection_version_update_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_CHANNEL_MAP_GET
                        else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_connection_channel_map_get) ble_rsp_connection_channel_map_get((const struct ble_msg_connection_channel_map_get_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_CHANNEL_MAP_SET
                        else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_connection_channel_map_set) ble_rsp_connection_channel_map_set((const struct ble_msg_connection_channel_map_set_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_FEATURES_GET
                        else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_connection_features_get) ble_rsp_connection_features_get((const struct ble_msg_connection_features_get_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_GET_STATUS
                        else if (bgapiRXBuffer[3] == 7) { if (ble_rsp_connection_get_status) ble_rsp_connection_get_status((const struct ble_msg_connection_get_status_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_CONNECTION_RAW_TX
                        else if (bgapiRXBuffer[3] == 8) { if (ble_rsp_connection_raw_tx) ble_rsp_connection_raw_tx((const struct ble_msg_connection_raw_tx_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 4) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_FIND_BY_TYPE_VALUE
                        else if (bgapiRXBuffer[3] == 0) { if (ble_rsp_attclient_find_by_type_value) ble_rsp_attclient_find_by_type_value((const struct ble_msg_attclient_find_by_type_value_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_BY_GROUP_TYPE
                        else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_attclient_read_by_group_type) ble_rsp_attclient_read_by_group_type((const struct ble_msg_attclient_read_by_group_type_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_BY_TYPE
                        else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_attclient_read_by_type) ble_rsp_attclient_read_by_type((const struct ble_msg_attclient_read_by_type_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_FIND_INFORMATION
                        else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_attclient_find_information) ble_rsp_attclient_find_information((const struct ble_msg_attclient_find_information_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_BY_HANDLE
                        else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_attclient_read_by_handle) ble_rsp_attclient_read_by_handle((const struct ble_msg_attclient_read_by_handle_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_ATTRIBUTE_WRITE
                        else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_attclient_attribute_write) ble_rsp_attclient_attribute_write((const struct ble_msg_attclient_attribute_write_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_WRITE_COMMAND
                        else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_attclient_write_command) ble_rsp_attclient_write_command((const struct ble_msg_attclient_write_command_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_INDICATE_CONFIRM
                        else if (bgapiRXBuffer[3] == 7) { if (ble_rsp_attclient_indicate_confirm) ble_rsp_attclient_indicate_confirm((const struct ble_msg_attclient_indicate_confirm_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_LONG
                        else if (bgapiRXBuffer[3] == 8) { if (ble_rsp_attclient_read_long) ble_rsp_attclient_read_long((const struct ble_msg_attclient_read_long_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_PREPARE_WRITE
                        else if (bgapiRXBuffer[3] == 9) { if (ble_rsp_attclient_prepare_write) ble_rsp_attclient_prepare_write((const struct ble_msg_attclient_prepare_write_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_EXECUTE_WRITE
                        else if (bgapiRXBuffer[3] == 10) { if (ble_rsp_attclient_execute_write) ble_rsp_attclient_execute_write((const struct ble_msg_attclient_execute_write_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_MULTIPLE
                        else if (bgapiRXBuffer[3] == 11) { if (ble_rsp_attclient_read_multiple) ble_rsp_attclient_read_multiple((const struct ble_msg_attclient_read_multiple_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 5) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_COMMAND_SM_ENCRYPT_START
                        else if (bgapiRXBuffer[3] == 0) { if (ble_rsp_sm_encrypt_start) ble_rsp_sm_encrypt_start((const struct ble_msg_sm_encrypt_start_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SM_SET_BONDABLE_MODE
                        else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_sm_set_bondable_mode) ble_rsp_sm_set_bondable_mode((const struct ble_msg_sm_set_bondable_mode_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SM_DELETE_BONDING
                        else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_sm_delete_bonding) ble_rsp_sm_delete_bonding((const struct ble_msg_sm_delete_bonding_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SM_SET_PARAMETERS
                        else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_sm_set_parameters) ble_rsp_sm_set_parameters((const struct ble_msg_sm_set_parameters_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SM_PASSKEY_ENTRY
                        else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_sm_passkey_entry) ble_rsp_sm_passkey_entry((const struct ble_msg_sm_passkey_entry_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SM_GET_BONDS
                        else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_sm_get_bonds) ble_rsp_sm_get_bonds((const struct ble_msg_sm_get_bonds_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_SM_SET_OOB_DATA
                        else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_sm_set_oob_data) ble_rsp_sm_set_oob_data((const struct ble_msg_sm_set_oob_data_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 6) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_PRIVACY_FLAGS
                        else if (bgapiRXBuffer[3] == 0) { if (ble_rsp_gap_set_privacy_flags) ble_rsp_gap_set_privacy_flags((const struct ble_msg_gap_set_privacy_flags_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_MODE
                        else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_gap_set_mode) ble_rsp_gap_set_mode((const struct ble_msg_gap_set_mode_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_GAP_DISCOVER
                        else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_gap_discover) ble_rsp_gap_discover((const struct ble_msg_gap_discover_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_GAP_CONNECT_DIRECT
                        else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_gap_connect_direct) ble_rsp_gap_connect_direct((const struct ble_msg_gap_connect_direct_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_GAP_END_PROCEDURE
                        else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_gap_end_procedure) ble_rsp_gap_end_procedure((const struct ble_msg_gap_end_procedure_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_GAP_CONNECT_SELECTIVE
                        else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_gap_connect_selective) ble_rsp_gap_connect_selective((const struct ble_msg_gap_connect_selective_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_FILTERING
                        else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_gap_set_filtering) ble_rsp_gap_set_filtering((const struct ble_msg_gap_set_filtering_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_SCAN_PARAMETERS
                        else if (bgapiRXBuffer[3] == 7) { if (ble_rsp_gap_set_scan_parameters) ble_rsp_gap_set_scan_parameters((const struct ble_msg_gap_set_scan_parameters_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_ADV_PARAMETERS
                        else if (bgapiRXBuffer[3] == 8) { if (ble_rsp_gap_set_adv_parameters) ble_rsp_gap_set_adv_parameters((const struct ble_msg_gap_set_adv_parameters_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_ADV_DATA
                        else if (bgapiRXBuffer[3] == 9) { if (ble_rsp_gap_set_adv_data) ble_rsp_gap_set_adv_data((const struct ble_msg_gap_set_adv_data_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_GAP_SET_DIRECTED_CONNECTABLE_MODE
                        else if (bgapiRXBuffer[3] == 10) { if (ble_rsp_gap_set_directed_connectable_mode) ble_rsp_gap_set_directed_connectable_mode((const struct ble_msg_gap_set_directed_connectable_mode_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 7) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_CONFIG_IRQ
                        else if (bgapiRXBuffer[3] == 0) { if (ble_rsp_hardware_io_port_config_irq) ble_rsp_hardware_io_port_config_irq((const struct ble_msg_hardware_io_port_config_irq_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_SET_SOFT_TIMER
                        else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_hardware_set_soft_timer) ble_rsp_hardware_set_soft_timer((const struct ble_msg_hardware_set_soft_timer_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_ADC_READ
                        else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_hardware_adc_read) ble_rsp_hardware_adc_read((const struct ble_msg_hardware_adc_read_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_CONFIG_DIRECTION
                        else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_hardware_io_port_config_direction) ble_rsp_hardware_io_port_config_direction((const struct ble_msg_hardware_io_port_config_direction_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_CONFIG_FUNCTION
                        else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_hardware_io_port_config_function) ble_rsp_hardware_io_port_config_function((const struct ble_msg_hardware_io_port_config_function_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_CONFIG_PULL
                        else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_hardware_io_port_config_pull) ble_rsp_hardware_io_port_config_pull((const struct ble_msg_hardware_io_port_config_pull_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_WRITE
                        else if (bgapiRXBuffer[3] == 6) { if (ble_rsp_hardware_io_port_write) ble_rsp_hardware_io_port_write((const struct ble_msg_hardware_io_port_write_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_READ
                        else if (bgapiRXBuffer[3] == 7) { if (ble_rsp_hardware_io_port_read) ble_rsp_hardware_io_port_read((const struct ble_msg_hardware_io_port_read_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_SPI_CONFIG
                        else if (bgapiRXBuffer[3] == 8) { if (ble_rsp_hardware_spi_config) ble_rsp_hardware_spi_config((const struct ble_msg_hardware_spi_config_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_SPI_TRANSFER
                        else if (bgapiRXBuffer[3] == 9) { if (ble_rsp_hardware_spi_transfer) ble_rsp_hardware_spi_transfer((const struct ble_msg_hardware_spi_transfer_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_I2C_READ
                        else if (bgapiRXBuffer[3] == 10) { if (ble_rsp_hardware_i2c_read) ble_rsp_hardware_i2c_read((const struct ble_msg_hardware_i2c_read_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_I2C_WRITE
                        else if (bgapiRXBuffer[3] == 11) { if (ble_rsp_hardware_i2c_write) ble_rsp_hardware_i2c_write((const struct ble_msg_hardware_i2c_write_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_SET_TXPOWER
                        else if (bgapiRXBuffer[3] == 12) { if (ble_rsp_hardware_set_txpower) ble_rsp_hardware_set_txpower((const struct ble_msg_hardware_set_txpower_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_HARDWARE_TIMER_COMPARATOR
                        else if (bgapiRXBuffer[3] == 13) { if (ble_rsp_hardware_timer_comparator) ble_rsp_hardware_timer_comparator((const struct ble_msg_hardware_timer_comparator_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 8) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_COMMAND_TEST_PHY_TX
                        else if (bgapiRXBuffer[3] == 0) { if (ble_rsp_test_phy_tx) ble_rsp_test_phy_tx((const struct ble_msg_test_phy_tx_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_TEST_PHY_RX
                        else if (bgapiRXBuffer[3] == 1) { if (ble_rsp_test_phy_rx) ble_rsp_test_phy_rx((const struct ble_msg_test_phy_rx_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_TEST_PHY_END
                        else if (bgapiRXBuffer[3] == 2) { if (ble_rsp_test_phy_end) ble_rsp_test_phy_end((const struct ble_msg_test_phy_end_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_TEST_PHY_RESET
                        else if (bgapiRXBuffer[3] == 3) { if (ble_rsp_test_phy_reset) ble_rsp_test_phy_reset((const struct ble_msg_test_phy_reset_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_TEST_GET_CHANNEL_MAP
                        else if (bgapiRXBuffer[3] == 4) { if (ble_rsp_test_get_channel_map) ble_rsp_test_get_channel_map((const struct ble_msg_test_get_channel_map_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_COMMAND_TEST_DEBUG
                        else if (bgapiRXBuffer[3] == 5) { if (ble_rsp_test_debug) ble_rsp_test_debug((const struct ble_msg_test_debug_rsp_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }

                setBusy(false);
            } else {
                // 0x80 = Event packet

                // capture last event class/command bytes
                lastEvent[0] = bgapiRXBuffer[2];
                lastEvent[1] = bgapiRXBuffer[3];

                if (bgapiRXBuffer[2] == 0) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_EVENT_SYSTEM_BOOT
                        else if (bgapiRXBuffer[3] == 0) { if (ble_evt_system_boot) { ble_evt_system_boot((const struct ble_msg_system_boot_evt_t *)(bgapiRXBuffer + 4)); } setBusy(false); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_SYSTEM_DEBUG
                        else if (bgapiRXBuffer[3] == 1) { if (ble_evt_system_debug) ble_evt_system_debug((const struct ble_msg_system_debug_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_SYSTEM_ENDPOINT_WATERMARK_RX
                        else if (bgapiRXBuffer[3] == 2) { if (ble_evt_system_endpoint_watermark_rx) ble_evt_system_endpoint_watermark_rx((const struct ble_msg_system_endpoint_watermark_rx_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_SYSTEM_ENDPOINT_WATERMARK_TX
                        else if (bgapiRXBuffer[3] == 3) { if (ble_evt_system_endpoint_watermark_tx) ble_evt_system_endpoint_watermark_tx((const struct ble_msg_system_endpoint_watermark_tx_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_SYSTEM_SCRIPT_FAILURE
                        else if (bgapiRXBuffer[3] == 4) { if (ble_evt_system_script_failure) ble_evt_system_script_failure((const struct ble_msg_system_script_failure_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_SYSTEM_NO_LICENSE_KEY
                        else if (bgapiRXBuffer[3] == 5) { if (ble_evt_system_no_license_key) ble_evt_system_no_license_key((const struct ble_msg_system_no_license_key_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 1) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_EVENT_FLASH_PS_KEY
                        else if (bgapiRXBuffer[3] == 0) { if (ble_evt_flash_ps_key) ble_evt_flash_ps_key((const struct ble_msg_flash_ps_key_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 2) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_EVENT_ATTRIBUTES_VALUE
                        else if (bgapiRXBuffer[3] == 0) { if (ble_evt_attributes_value) ble_evt_attributes_value((const struct ble_msg_attributes_value_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_ATTRIBUTES_USER_READ_REQUEST
                        else if (bgapiRXBuffer[3] == 1) { if (ble_evt_attributes_user_read_request) ble_evt_attributes_user_read_request((const struct ble_msg_attributes_user_read_request_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_ATTRIBUTES_STATUS
                        else if (bgapiRXBuffer[3] == 2) { if (ble_evt_attributes_status) ble_evt_attributes_status((const struct ble_msg_attributes_status_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 3) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_EVENT_CONNECTION_STATUS
                        else if (bgapiRXBuffer[3] == 0) { if (ble_evt_connection_status) ble_evt_connection_status((const struct ble_msg_connection_status_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_CONNECTION_VERSION_IND
                        else if (bgapiRXBuffer[3] == 1) { if (ble_evt_connection_version_ind) ble_evt_connection_version_ind((const struct ble_msg_connection_version_ind_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_CONNECTION_FEATURE_IND
                        else if (bgapiRXBuffer[3] == 2) { if (ble_evt_connection_feature_ind) ble_evt_connection_feature_ind((const struct ble_msg_connection_feature_ind_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_CONNECTION_RAW_RX
                        else if (bgapiRXBuffer[3] == 3) { if (ble_evt_connection_raw_rx) ble_evt_connection_raw_rx((const struct ble_msg_connection_raw_rx_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_CONNECTION_DISCONNECTED
                        else if (bgapiRXBuffer[3] == 4) { if (ble_evt_connection_disconnected) ble_evt_connection_disconnected((const struct ble_msg_connection_disconnected_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 4) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_INDICATED
                        else if (bgapiRXBuffer[3] == 0) { if (ble_evt_attclient_indicated) ble_evt_attclient_indicated((const struct ble_msg_attclient_indicated_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_PROCEDURE_COMPLETED
                        else if (bgapiRXBuffer[3] == 1) { if (ble_evt_attclient_procedure_completed) ble_evt_attclient_procedure_completed((const struct ble_msg_attclient_procedure_completed_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_GROUP_FOUND
                        else if (bgapiRXBuffer[3] == 2) { if (ble_evt_attclient_group_found) ble_evt_attclient_group_found((const struct ble_msg_attclient_group_found_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_ATTRIBUTE_FOUND
                        else if (bgapiRXBuffer[3] == 3) { if (ble_evt_attclient_attribute_found) ble_evt_attclient_attribute_found((const struct ble_msg_attclient_attribute_found_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_FIND_INFORMATION_FOUND
                        else if (bgapiRXBuffer[3] == 4) { if (ble_evt_attclient_find_information_found) ble_evt_attclient_find_information_found((const struct ble_msg_attclient_find_information_found_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_ATTRIBUTE_VALUE
                        else if (bgapiRXBuffer[3] == 5) { if (ble_evt_attclient_attribute_value) ble_evt_attclient_attribute_value((const struct ble_msg_attclient_attribute_value_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_ATTCLIENT_READ_MULTIPLE_RESPONSE
                        else if (bgapiRXBuffer[3] == 6) { if (ble_evt_attclient_read_multiple_response) ble_evt_attclient_read_multiple_response((const struct ble_msg_attclient_read_multiple_response_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 5) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_EVENT_SM_SMP_DATA
                        else if (bgapiRXBuffer[3] == 0) { if (ble_evt_sm_smp_data) ble_evt_sm_smp_data((const struct ble_msg_sm_smp_data_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_SM_BONDING_FAIL
                        else if (bgapiRXBuffer[3] == 1) { if (ble_evt_sm_bonding_fail) ble_evt_sm_bonding_fail((const struct ble_msg_sm_bonding_fail_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_SM_PASSKEY_DISPLAY
                        else if (bgapiRXBuffer[3] == 2) { if (ble_evt_sm_passkey_display) ble_evt_sm_passkey_display((const struct ble_msg_sm_passkey_display_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_SM_PASSKEY_REQUEST
                        else if (bgapiRXBuffer[3] == 3) { if (ble_evt_sm_passkey_request) ble_evt_sm_passkey_request((const struct ble_msg_sm_passkey_request_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_SM_BOND_STATUS
                        else if (bgapiRXBuffer[3] == 4) { if (ble_evt_sm_bond_status) ble_evt_sm_bond_status((const struct ble_msg_sm_bond_status_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 6) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_EVENT_GAP_SCAN_RESPONSE
                        else if (bgapiRXBuffer[3] == 0) { if (ble_evt_gap_scan_response) ble_evt_gap_scan_response((const struct ble_msg_gap_scan_response_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_GAP_MODE_CHANGED
                        else if (bgapiRXBuffer[3] == 1) { if (ble_evt_gap_mode_changed) ble_evt_gap_mode_changed((const struct ble_msg_gap_mode_changed_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 7) {
                    if (false) { }
                    #ifdef BGLIB_ENABLE_EVENT_HARDWARE_IO_PORT_STATUS
                        else if (bgapiRXBuffer[3] == 0) { if (ble_evt_hardware_io_port_status) ble_evt_hardware_io_port_status((const struct ble_msg_hardware_io_port_status_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_HARDWARE_SOFT_TIMER
                        else if (bgapiRXBuffer[3] == 1) { if (ble_evt_hardware_soft_timer) ble_evt_hardware_soft_timer((const struct ble_msg_hardware_soft_timer_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                    #ifdef BGLIB_ENABLE_EVENT_HARDWARE_ADC_RESULT
                        else if (bgapiRXBuffer[3] == 2) { if (ble_evt_hardware_adc_result) ble_evt_hardware_adc_result((const struct ble_msg_hardware_adc_result_evt_t *)(bgapiRXBuffer + 4)); }
                    #endif
                }
                else if (bgapiRXBuffer[2] == 8) {
                    if (false) { }
                }
            }
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
    if (len > 0) memcpy(bgapiTXBuffer + 4, payload, len);
    #ifdef DEBUG
        Serial.print("\n=>[ ");
        if (packetMode) {
            if (len + 4 < 16) Serial.write(0x30);
            Serial.print(len + 4, HEX);
            Serial.write(0x20);
        }
        for (uint8_t i = 0; i < len + 4; i++) {
            if (bgapiTXBuffer[i] < 16) Serial.write(0x30);
            Serial.print(bgapiTXBuffer[i], HEX);
            Serial.write(0x20);
        }
        Serial.println("]");
    #endif
    if (onBeforeTXCommand) onBeforeTXCommand();
    setBusy(true);
    if (packetMode) uModule -> write(len + 4); // outgoing packet length byte first
    uModule -> write(bgapiTXBuffer, len + 4);

    if (onTXCommandComplete) onTXCommandComplete();
    free(bgapiTXBuffer);
    return 0;
}

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_RESET
uint8_t BGLib::ble_cmd_system_reset(uint8 boot_in_dfu) {
    uint8_t d[1];
    memcpy(d + 0, &boot_in_dfu, sizeof(uint8));
    return sendCommand(1, 0, 0, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_HELLO
uint8_t BGLib::ble_cmd_system_hello() {
    return sendCommand(0, 0, 1);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_ADDRESS_GET
uint8_t BGLib::ble_cmd_system_address_get() {
    return sendCommand(0, 0, 2);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_REG_WRITE
uint8_t BGLib::ble_cmd_system_reg_write(uint16 address, uint8 value) {
    uint8_t d[3];
    memcpy(d + 0, &address, sizeof(uint16));
    memcpy(d + 2, &value, sizeof(uint8));
    return sendCommand(3, 0, 3, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_REG_READ
uint8_t BGLib::ble_cmd_system_reg_read(uint16 address) {
    uint8_t d[2];
    memcpy(d + 0, &address, sizeof(uint16));
    return sendCommand(2, 0, 4, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_GET_COUNTERS
uint8_t BGLib::ble_cmd_system_get_counters() {
    return sendCommand(0, 0, 5);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_GET_CONNECTIONS
uint8_t BGLib::ble_cmd_system_get_connections() {
    return sendCommand(0, 0, 6);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_READ_MEMORY
uint8_t BGLib::ble_cmd_system_read_memory(uint32 address, uint8 length) {
    uint8_t d[5];
    memcpy(d + 0, &address, sizeof(uint32));
    memcpy(d + 4, &length, sizeof(uint8));
    return sendCommand(5, 0, 7, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_GET_INFO
uint8_t BGLib::ble_cmd_system_get_info() {
    return sendCommand(0, 0, 8);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_ENDPOINT_TX
uint8_t BGLib::ble_cmd_system_endpoint_tx(uint8 endpoint, uint8 data_len, const uint8 *data_data) {
    uint8_t d[2 + data_len];
    memcpy(d + 0, &endpoint, sizeof(uint8));
    memcpy(d + 1, &data_len, sizeof(uint8));
    memcpy(d + 2, data_data, data_len);
    return sendCommand(2 + data_len + data_len, 0, 9, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_WHITELIST_APPEND
uint8_t BGLib::ble_cmd_system_whitelist_append(bd_addr address, uint8 address_type) {
    uint8_t d[7];
    memcpy(d + 0, &address, sizeof(bd_addr));
    memcpy(d + 6, &address_type, sizeof(uint8));
    return sendCommand(7, 0, 10, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_WHITELIST_REMOVE
uint8_t BGLib::ble_cmd_system_whitelist_remove(bd_addr address, uint8 address_type) {
    uint8_t d[7];
    memcpy(d + 0, &address, sizeof(bd_addr));
    memcpy(d + 6, &address_type, sizeof(uint8));
    return sendCommand(7, 0, 11, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_WHITELIST_CLEAR
uint8_t BGLib::ble_cmd_system_whitelist_clear() {
    return sendCommand(0, 0, 12);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_ENDPOINT_RX
uint8_t BGLib::ble_cmd_system_endpoint_rx(uint8 endpoint, uint8 size) {
    uint8_t d[2];
    memcpy(d + 0, &endpoint, sizeof(uint8));
    memcpy(d + 1, &size, sizeof(uint8));
    return sendCommand(2, 0, 13, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SYSTEM_ENDPOINT_SET_WATERMARKS
uint8_t BGLib::ble_cmd_system_endpoint_set_watermarks(uint8 endpoint, uint8 rx, uint8 tx) {
    uint8_t d[3];
    memcpy(d + 0, &endpoint, sizeof(uint8));
    memcpy(d + 1, &rx, sizeof(uint8));
    memcpy(d + 2, &tx, sizeof(uint8));
    return sendCommand(3, 0, 14, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_DEFRAG
uint8_t BGLib::ble_cmd_flash_ps_defrag() {
    return sendCommand(0, 1, 0);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_DUMP
uint8_t BGLib::ble_cmd_flash_ps_dump() {
    return sendCommand(0, 1, 1);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_ERASE_ALL
uint8_t BGLib::ble_cmd_flash_ps_erase_all() {
    return sendCommand(0, 1, 2);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_SAVE
uint8_t BGLib::ble_cmd_flash_ps_save(uint16 key, uint8 value_len, const uint8 *value_data) {
    uint8_t d[3 + value_len];
    memcpy(d + 0, &key, sizeof(uint16));
    memcpy(d + 2, &value_len, sizeof(uint8));
    memcpy(d + 3, value_data, value_len);
    return sendCommand(3 + value_len + value_len, 1, 3, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_LOAD
uint8_t BGLib::ble_cmd_flash_ps_load(uint16 key) {
    uint8_t d[2];
    memcpy(d + 0, &key, sizeof(uint16));
    return sendCommand(2, 1, 4, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_FLASH_PS_ERASE
uint8_t BGLib::ble_cmd_flash_ps_erase(uint16 key) {
    uint8_t d[2];
    memcpy(d + 0, &key, sizeof(uint16));
    return sendCommand(2, 1, 5, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_FLASH_ERASE_PAGE
uint8_t BGLib::ble_cmd_flash_erase_page(uint8 page) {
    uint8_t d[1];
    memcpy(d + 0, &page, sizeof(uint8));
    return sendCommand(1, 1, 6, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_FLASH_WRITE_WORDS
uint8_t BGLib::ble_cmd_flash_write_words(uint16 address, uint8 words_len, const uint8 *words_data) {
    uint8_t d[3 + words_len];
    memcpy(d + 0, &address, sizeof(uint16));
    memcpy(d + 2, &words_len, sizeof(uint8));
    memcpy(d + 3, words_data, words_len);
    return sendCommand(3 + words_len + words_len, 1, 7, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_WRITE
uint8_t BGLib::ble_cmd_attributes_write(uint16 handle, uint8 offset, uint8 value_len, const uint8 *value_data) {
    uint8_t d[4 + value_len];
    memcpy(d + 0, &handle, sizeof(uint16));
    memcpy(d + 2, &offset, sizeof(uint8));
    memcpy(d + 3, &value_len, sizeof(uint8));
    memcpy(d + 4, value_data, value_len);
    return sendCommand(4 + value_len + value_len, 2, 0, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_READ
uint8_t BGLib::ble_cmd_attributes_read(uint16 handle, uint16 offset) {
    uint8_t d[4];
    memcpy(d + 0, &handle, sizeof(uint16));
    memcpy(d + 2, &offset, sizeof(uint16));
    return sendCommand(4, 2, 1, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_READ_TYPE
uint8_t BGLib::ble_cmd_attributes_read_type(uint16 handle) {
    uint8_t d[2];
    memcpy(d + 0, &handle, sizeof(uint16));
    return sendCommand(2, 2, 2, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_USER_READ_RESPONSE
uint8_t BGLib::ble_cmd_attributes_user_read_response(uint8 connection, uint8 att_error, uint8 value_len, const uint8 *value_data) {
    uint8_t d[3 + value_len];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &att_error, sizeof(uint8));
    memcpy(d + 2, &value_len, sizeof(uint8));
    memcpy(d + 3, value_data, value_len);
    return sendCommand(3 + value_len + value_len, 2, 3, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTRIBUTES_USER_WRITE_RESPONSE
uint8_t BGLib::ble_cmd_attributes_user_write_response(uint8 connection, uint8 att_error) {
    uint8_t d[2];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &att_error, sizeof(uint8));
    return sendCommand(2, 2, 4, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_CONNECTION_DISCONNECT
uint8_t BGLib::ble_cmd_connection_disconnect(uint8 connection) {
    uint8_t d[1];
    memcpy(d + 0, &connection, sizeof(uint8));
    return sendCommand(1, 3, 0, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_CONNECTION_GET_RSSI
uint8_t BGLib::ble_cmd_connection_get_rssi(uint8 connection) {
    uint8_t d[1];
    memcpy(d + 0, &connection, sizeof(uint8));
    return sendCommand(1, 3, 1, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_CONNECTION_UPDATE
uint8_t BGLib::ble_cmd_connection_update(uint8 connection, uint16 interval_min, uint16 interval_max, uint16 latency, uint16 timeout) {
    uint8_t d[9];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &interval_min, sizeof(uint16));
    memcpy(d + 3, &interval_max, sizeof(uint16));
    memcpy(d + 5, &latency, sizeof(uint16));
    memcpy(d + 7, &timeout, sizeof(uint16));
    return sendCommand(9, 3, 2, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_CONNECTION_VERSION_UPDATE
uint8_t BGLib::ble_cmd_connection_version_update(uint8 connection) {
    uint8_t d[1];
    memcpy(d + 0, &connection, sizeof(uint8));
    return sendCommand(1, 3, 3, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_CONNECTION_CHANNEL_MAP_GET
uint8_t BGLib::ble_cmd_connection_channel_map_get(uint8 connection) {
    uint8_t d[1];
    memcpy(d + 0, &connection, sizeof(uint8));
    return sendCommand(1, 3, 4, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_CONNECTION_CHANNEL_MAP_SET
uint8_t BGLib::ble_cmd_connection_channel_map_set(uint8 connection, uint8 map_len, const uint8 *map_data) {
    uint8_t d[2 + map_len];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &map_len, sizeof(uint8));
    memcpy(d + 2, map_data, map_len);
    return sendCommand(2 + map_len + map_len, 3, 5, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_CONNECTION_FEATURES_GET
uint8_t BGLib::ble_cmd_connection_features_get(uint8 connection) {
    uint8_t d[1];
    memcpy(d + 0, &connection, sizeof(uint8));
    return sendCommand(1, 3, 6, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_CONNECTION_GET_STATUS
uint8_t BGLib::ble_cmd_connection_get_status(uint8 connection) {
    uint8_t d[1];
    memcpy(d + 0, &connection, sizeof(uint8));
    return sendCommand(1, 3, 7, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_CONNECTION_RAW_TX
uint8_t BGLib::ble_cmd_connection_raw_tx(uint8 connection, uint8 data_len, const uint8 *data_data) {
    uint8_t d[2 + data_len];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &data_len, sizeof(uint8));
    memcpy(d + 2, data_data, data_len);
    return sendCommand(2 + data_len + data_len, 3, 8, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_FIND_BY_TYPE_VALUE
uint8_t BGLib::ble_cmd_attclient_find_by_type_value(uint8 connection, uint16 start, uint16 end, uint16 uuid, uint8 value_len, const uint8 *value_data) {
    uint8_t d[8 + value_len];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &start, sizeof(uint16));
    memcpy(d + 3, &end, sizeof(uint16));
    memcpy(d + 5, &uuid, sizeof(uint16));
    memcpy(d + 7, &value_len, sizeof(uint8));
    memcpy(d + 8, value_data, value_len);
    return sendCommand(8 + value_len + value_len, 4, 0, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_BY_GROUP_TYPE
uint8_t BGLib::ble_cmd_attclient_read_by_group_type(uint8 connection, uint16 start, uint16 end, uint8 uuid_len, const uint8 *uuid_data) {
    uint8_t d[6 + uuid_len];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &start, sizeof(uint16));
    memcpy(d + 3, &end, sizeof(uint16));
    memcpy(d + 5, &uuid_len, sizeof(uint8));
    memcpy(d + 6, uuid_data, uuid_len);
    return sendCommand(6 + uuid_len + uuid_len, 4, 1, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_BY_TYPE
uint8_t BGLib::ble_cmd_attclient_read_by_type(uint8 connection, uint16 start, uint16 end, uint8 uuid_len, const uint8 *uuid_data) {
    uint8_t d[6 + uuid_len];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &start, sizeof(uint16));
    memcpy(d + 3, &end, sizeof(uint16));
    memcpy(d + 5, &uuid_len, sizeof(uint8));
    memcpy(d + 6, uuid_data, uuid_len);
    return sendCommand(6 + uuid_len + uuid_len, 4, 2, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_FIND_INFORMATION
uint8_t BGLib::ble_cmd_attclient_find_information(uint8 connection, uint16 start, uint16 end) {
    uint8_t d[5];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &start, sizeof(uint16));
    memcpy(d + 3, &end, sizeof(uint16));
    return sendCommand(5, 4, 3, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_BY_HANDLE
uint8_t BGLib::ble_cmd_attclient_read_by_handle(uint8 connection, uint16 chrhandle) {
    uint8_t d[3];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &chrhandle, sizeof(uint16));
    return sendCommand(3, 4, 4, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_ATTRIBUTE_WRITE
uint8_t BGLib::ble_cmd_attclient_attribute_write(uint8 connection, uint16 atthandle, uint8 data_len, const uint8 *data_data) {
    uint8_t d[4 + data_len];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &atthandle, sizeof(uint16));
    memcpy(d + 3, &data_len, sizeof(uint8));
    memcpy(d + 4, data_data, data_len);
    return sendCommand(4 + data_len + data_len, 4, 5, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_WRITE_COMMAND
uint8_t BGLib::ble_cmd_attclient_write_command(uint8 connection, uint16 atthandle, uint8 data_len, const uint8 *data_data) {
    uint8_t d[4 + data_len];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &atthandle, sizeof(uint16));
    memcpy(d + 3, &data_len, sizeof(uint8));
    memcpy(d + 4, data_data, data_len);
    return sendCommand(4 + data_len + data_len, 4, 6, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_INDICATE_CONFIRM
uint8_t BGLib::ble_cmd_attclient_indicate_confirm(uint8 connection) {
    uint8_t d[1];
    memcpy(d + 0, &connection, sizeof(uint8));
    return sendCommand(1, 4, 7, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_LONG
uint8_t BGLib::ble_cmd_attclient_read_long(uint8 connection, uint16 chrhandle) {
    uint8_t d[3];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &chrhandle, sizeof(uint16));
    return sendCommand(3, 4, 8, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_PREPARE_WRITE
uint8_t BGLib::ble_cmd_attclient_prepare_write(uint8 connection, uint16 atthandle, uint16 offset, uint8 data_len, const uint8 *data_data) {
    uint8_t d[6 + data_len];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &atthandle, sizeof(uint16));
    memcpy(d + 3, &offset, sizeof(uint16));
    memcpy(d + 5, &data_len, sizeof(uint8));
    memcpy(d + 6, data_data, data_len);
    return sendCommand(6 + data_len + data_len, 4, 9, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_EXECUTE_WRITE
uint8_t BGLib::ble_cmd_attclient_execute_write(uint8 connection, uint8 commit) {
    uint8_t d[2];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &commit, sizeof(uint8));
    return sendCommand(2, 4, 10, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_ATTCLIENT_READ_MULTIPLE
uint8_t BGLib::ble_cmd_attclient_read_multiple(uint8 connection, uint8 handles_len, const uint8 *handles_data) {
    uint8_t d[2 + handles_len];
    memcpy(d + 0, &connection, sizeof(uint8));
    memcpy(d + 1, &handles_len, sizeof(uint8));
    memcpy(d + 2, handles_data, handles_len);
    return sendCommand(2 + handles_len + handles_len, 4, 11, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SM_ENCRYPT_START
uint8_t BGLib::ble_cmd_sm_encrypt_start(uint8 handle, uint8 bonding) {
    uint8_t d[2];
    memcpy(d + 0, &handle, sizeof(uint8));
    memcpy(d + 1, &bonding, sizeof(uint8));
    return sendCommand(2, 5, 0, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SM_SET_BONDABLE_MODE
uint8_t BGLib::ble_cmd_sm_set_bondable_mode(uint8 bondable) {
    uint8_t d[1];
    memcpy(d + 0, &bondable, sizeof(uint8));
    return sendCommand(1, 5, 1, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SM_DELETE_BONDING
uint8_t BGLib::ble_cmd_sm_delete_bonding(uint8 handle) {
    uint8_t d[1];
    memcpy(d + 0, &handle, sizeof(uint8));
    return sendCommand(1, 5, 2, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SM_SET_PARAMETERS
uint8_t BGLib::ble_cmd_sm_set_parameters(uint8 mitm, uint8 min_key_size, uint8 io_capabilities) {
    uint8_t d[3];
    memcpy(d + 0, &mitm, sizeof(uint8));
    memcpy(d + 1, &min_key_size, sizeof(uint8));
    memcpy(d + 2, &io_capabilities, sizeof(uint8));
    return sendCommand(3, 5, 3, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SM_PASSKEY_ENTRY
uint8_t BGLib::ble_cmd_sm_passkey_entry(uint8 handle, uint32 passkey) {
    uint8_t d[5];
    memcpy(d + 0, &handle, sizeof(uint8));
    memcpy(d + 1, &passkey, sizeof(uint32));
    return sendCommand(5, 5, 4, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SM_GET_BONDS
uint8_t BGLib::ble_cmd_sm_get_bonds() {
    return sendCommand(0, 5, 5);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_SM_SET_OOB_DATA
uint8_t BGLib::ble_cmd_sm_set_oob_data(uint8 oob_len, const uint8 *oob_data) {
    uint8_t d[1 + oob_len];
    memcpy(d + 0, &oob_len, sizeof(uint8));
    memcpy(d + 1, oob_data, oob_len);
    return sendCommand(1 + oob_len + oob_len, 5, 6, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_GAP_SET_PRIVACY_FLAGS
uint8_t BGLib::ble_cmd_gap_set_privacy_flags(uint8 peripheral_privacy, uint8 central_privacy) {
    uint8_t d[2];
    memcpy(d + 0, &peripheral_privacy, sizeof(uint8));
    memcpy(d + 1, &central_privacy, sizeof(uint8));
    return sendCommand(2, 6, 0, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_GAP_SET_MODE
uint8_t BGLib::ble_cmd_gap_set_mode(uint8 discover, uint8 connect) {
    uint8_t d[2];
    memcpy(d + 0, &discover, sizeof(uint8));
    memcpy(d + 1, &connect, sizeof(uint8));
    return sendCommand(2, 6, 1, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_GAP_DISCOVER
uint8_t BGLib::ble_cmd_gap_discover(uint8 mode) {
    uint8_t d[1];
    memcpy(d + 0, &mode, sizeof(uint8));
    return sendCommand(1, 6, 2, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_GAP_CONNECT_DIRECT
uint8_t BGLib::ble_cmd_gap_connect_direct(bd_addr address, uint8 addr_type, uint16 conn_interval_min, uint16 conn_interval_max, uint16 timeout, uint16 latency) {
    uint8_t d[15];
    memcpy(d + 0, &address, sizeof(bd_addr));
    memcpy(d + 6, &addr_type, sizeof(uint8));
    memcpy(d + 7, &conn_interval_min, sizeof(uint16));
    memcpy(d + 9, &conn_interval_max, sizeof(uint16));
    memcpy(d + 11, &timeout, sizeof(uint16));
    memcpy(d + 13, &latency, sizeof(uint16));
    return sendCommand(15, 6, 3, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_GAP_END_PROCEDURE
uint8_t BGLib::ble_cmd_gap_end_procedure() {
    return sendCommand(0, 6, 4);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_GAP_CONNECT_SELECTIVE
uint8_t BGLib::ble_cmd_gap_connect_selective(uint16 conn_interval_min, uint16 conn_interval_max, uint16 timeout, uint16 latency) {
    uint8_t d[8];
    memcpy(d + 0, &conn_interval_min, sizeof(uint16));
    memcpy(d + 2, &conn_interval_max, sizeof(uint16));
    memcpy(d + 4, &timeout, sizeof(uint16));
    memcpy(d + 6, &latency, sizeof(uint16));
    return sendCommand(8, 6, 5, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_GAP_SET_FILTERING
uint8_t BGLib::ble_cmd_gap_set_filtering(uint8 scan_policy, uint8 adv_policy, uint8 scan_duplicate_filtering) {
    uint8_t d[3];
    memcpy(d + 0, &scan_policy, sizeof(uint8));
    memcpy(d + 1, &adv_policy, sizeof(uint8));
    memcpy(d + 2, &scan_duplicate_filtering, sizeof(uint8));
    return sendCommand(3, 6, 6, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_GAP_SET_SCAN_PARAMETERS
uint8_t BGLib::ble_cmd_gap_set_scan_parameters(uint16 scan_interval, uint16 scan_window, uint8 active) {
    uint8_t d[5];
    memcpy(d + 0, &scan_interval, sizeof(uint16));
    memcpy(d + 2, &scan_window, sizeof(uint16));
    memcpy(d + 4, &active, sizeof(uint8));
    return sendCommand(5, 6, 7, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_GAP_SET_ADV_PARAMETERS
uint8_t BGLib::ble_cmd_gap_set_adv_parameters(uint16 adv_interval_min, uint16 adv_interval_max, uint8 adv_channels) {
    uint8_t d[5];
    memcpy(d + 0, &adv_interval_min, sizeof(uint16));
    memcpy(d + 2, &adv_interval_max, sizeof(uint16));
    memcpy(d + 4, &adv_channels, sizeof(uint8));
    return sendCommand(5, 6, 8, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_GAP_SET_ADV_DATA
uint8_t BGLib::ble_cmd_gap_set_adv_data(uint8 set_scanrsp, uint8 adv_data_len, const uint8 *adv_data_data) {
    uint8_t d[2 + adv_data_len];
    memcpy(d + 0, &set_scanrsp, sizeof(uint8));
    memcpy(d + 1, &adv_data_len, sizeof(uint8));
    memcpy(d + 2, adv_data_data, adv_data_len);
    return sendCommand(2 + adv_data_len + adv_data_len, 6, 9, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_GAP_SET_DIRECTED_CONNECTABLE_MODE
uint8_t BGLib::ble_cmd_gap_set_directed_connectable_mode(bd_addr address, uint8 addr_type) {
    uint8_t d[7];
    memcpy(d + 0, &address, sizeof(bd_addr));
    memcpy(d + 6, &addr_type, sizeof(uint8));
    return sendCommand(7, 6, 10, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_CONFIG_IRQ
uint8_t BGLib::ble_cmd_hardware_io_port_config_irq(uint8 port, uint8 enable_bits, uint8 falling_edge) {
    uint8_t d[3];
    memcpy(d + 0, &port, sizeof(uint8));
    memcpy(d + 1, &enable_bits, sizeof(uint8));
    memcpy(d + 2, &falling_edge, sizeof(uint8));
    return sendCommand(3, 7, 0, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_SET_SOFT_TIMER
uint8_t BGLib::ble_cmd_hardware_set_soft_timer(uint32 time, uint8 handle, uint8 single_shot) {
    uint8_t d[6];
    memcpy(d + 0, &time, sizeof(uint32));
    memcpy(d + 4, &handle, sizeof(uint8));
    memcpy(d + 5, &single_shot, sizeof(uint8));
    return sendCommand(6, 7, 1, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_ADC_READ
uint8_t BGLib::ble_cmd_hardware_adc_read(uint8 input, uint8 decimation, uint8 reference_selection) {
    uint8_t d[3];
    memcpy(d + 0, &input, sizeof(uint8));
    memcpy(d + 1, &decimation, sizeof(uint8));
    memcpy(d + 2, &reference_selection, sizeof(uint8));
    return sendCommand(3, 7, 2, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_CONFIG_DIRECTION
uint8_t BGLib::ble_cmd_hardware_io_port_config_direction(uint8 port, uint8 direction) {
    uint8_t d[2];
    memcpy(d + 0, &port, sizeof(uint8));
    memcpy(d + 1, &direction, sizeof(uint8));
    return sendCommand(2, 7, 3, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_CONFIG_FUNCTION
uint8_t BGLib::ble_cmd_hardware_io_port_config_function(uint8 port, uint8 function) {
    uint8_t d[2];
    memcpy(d + 0, &port, sizeof(uint8));
    memcpy(d + 1, &function, sizeof(uint8));
    return sendCommand(2, 7, 4, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_CONFIG_PULL
uint8_t BGLib::ble_cmd_hardware_io_port_config_pull(uint8 port, uint8 tristate_mask, uint8 pull_up) {
    uint8_t d[3];
    memcpy(d + 0, &port, sizeof(uint8));
    memcpy(d + 1, &tristate_mask, sizeof(uint8));
    memcpy(d + 2, &pull_up, sizeof(uint8));
    return sendCommand(3, 7, 5, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_WRITE
uint8_t BGLib::ble_cmd_hardware_io_port_write(uint8 port, uint8 mask, uint8 data) {
    uint8_t d[3];
    memcpy(d + 0, &port, sizeof(uint8));
    memcpy(d + 1, &mask, sizeof(uint8));
    memcpy(d + 2, &data, sizeof(uint8));
    return sendCommand(3, 7, 6, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_IO_PORT_READ
uint8_t BGLib::ble_cmd_hardware_io_port_read(uint8 port, uint8 mask) {
    uint8_t d[2];
    memcpy(d + 0, &port, sizeof(uint8));
    memcpy(d + 1, &mask, sizeof(uint8));
    return sendCommand(2, 7, 7, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_SPI_CONFIG
uint8_t BGLib::ble_cmd_hardware_spi_config(uint8 channel, uint8 polarity, uint8 phase, uint8 bit_order, uint8 baud_e, uint8 baud_m) {
    uint8_t d[6];
    memcpy(d + 0, &channel, sizeof(uint8));
    memcpy(d + 1, &polarity, sizeof(uint8));
    memcpy(d + 2, &phase, sizeof(uint8));
    memcpy(d + 3, &bit_order, sizeof(uint8));
    memcpy(d + 4, &baud_e, sizeof(uint8));
    memcpy(d + 5, &baud_m, sizeof(uint8));
    return sendCommand(6, 7, 8, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_SPI_TRANSFER
uint8_t BGLib::ble_cmd_hardware_spi_transfer(uint8 channel, uint8 data_len, const uint8 *data_data) {
    uint8_t d[2 + data_len];
    memcpy(d + 0, &channel, sizeof(uint8));
    memcpy(d + 1, &data_len, sizeof(uint8));
    memcpy(d + 2, data_data, data_len);
    return sendCommand(2 + data_len + data_len, 7, 9, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_I2C_READ
uint8_t BGLib::ble_cmd_hardware_i2c_read(uint8 address, uint8 stop, uint8 length) {
    uint8_t d[3];
    memcpy(d + 0, &address, sizeof(uint8));
    memcpy(d + 1, &stop, sizeof(uint8));
    memcpy(d + 2, &length, sizeof(uint8));
    return sendCommand(3, 7, 10, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_I2C_WRITE
uint8_t BGLib::ble_cmd_hardware_i2c_write(uint8 address, uint8 stop, uint8 data_len, const uint8 *data_data) {
    uint8_t d[3 + data_len];
    memcpy(d + 0, &address, sizeof(uint8));
    memcpy(d + 1, &stop, sizeof(uint8));
    memcpy(d + 2, &data_len, sizeof(uint8));
    memcpy(d + 3, data_data, data_len);
    return sendCommand(3 + data_len + data_len, 7, 11, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_SET_TXPOWER
uint8_t BGLib::ble_cmd_hardware_set_txpower(uint8 power) {
    uint8_t d[1];
    memcpy(d + 0, &power, sizeof(uint8));
    return sendCommand(1, 7, 12, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_HARDWARE_TIMER_COMPARATOR
uint8_t BGLib::ble_cmd_hardware_timer_comparator(uint8 timer, uint8 channel, uint8 mode, uint16 comparator_value) {
    uint8_t d[5];
    memcpy(d + 0, &timer, sizeof(uint8));
    memcpy(d + 1, &channel, sizeof(uint8));
    memcpy(d + 2, &mode, sizeof(uint8));
    memcpy(d + 3, &comparator_value, sizeof(uint16));
    return sendCommand(5, 7, 13, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_TEST_PHY_TX
uint8_t BGLib::ble_cmd_test_phy_tx(uint8 channel, uint8 length, uint8 type) {
    uint8_t d[3];
    memcpy(d + 0, &channel, sizeof(uint8));
    memcpy(d + 1, &length, sizeof(uint8));
    memcpy(d + 2, &type, sizeof(uint8));
    return sendCommand(3, 8, 0, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_TEST_PHY_RX
uint8_t BGLib::ble_cmd_test_phy_rx(uint8 channel) {
    uint8_t d[1];
    memcpy(d + 0, &channel, sizeof(uint8));
    return sendCommand(1, 8, 1, d);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_TEST_PHY_END
uint8_t BGLib::ble_cmd_test_phy_end() {
    return sendCommand(0, 8, 2);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_TEST_PHY_RESET
uint8_t BGLib::ble_cmd_test_phy_reset() {
    return sendCommand(0, 8, 3);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_TEST_GET_CHANNEL_MAP
uint8_t BGLib::ble_cmd_test_get_channel_map() {
    return sendCommand(0, 8, 4);
}
#endif

#ifdef BGLIB_ENABLE_COMMAND_TEST_DEBUG
uint8_t BGLib::ble_cmd_test_debug(uint8 input_len, const uint8 *input_data) {
    uint8_t d[1 + input_len];
    memcpy(d + 0, &input_len, sizeof(uint8));
    memcpy(d + 1, input_data, input_len);
    return sendCommand(1 + input_len + input_len, 8, 5, d);
}
#endif
