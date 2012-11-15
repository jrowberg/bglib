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

#ifndef __BGLIB_H__
#define __BGLIB_H__

#include <Arduino.h>

// uncomment this line for Serial.println() debug output
//#define DEBUG

#define BGLIB_SYSTEM_ENDPOINT_API                                   0
#define BGLIB_SYSTEM_ENDPOINT_TEST                                  1
#define BGLIB_SYSTEM_ENDPOINT_SCRIPT                                2
#define BGLIB_SYSTEM_ENDPOINT_USB                                   3
#define BGLIB_SYSTEM_ENDPOINT_UART0                                 4
#define BGLIB_SYSTEM_ENDPOINT_UART1                                 5

#define BGLIB_ATTRIBUTES_ATTRIBUTE_CHANGE_REASON_WRITE_REQUEST      0
#define BGLIB_ATTRIBUTES_ATTRIBUTE_CHANGE_REASON_WRITE_COMMAND      1
#define BGLIB_ATTRIBUTES_ATTRIBUTE_CHANGE_REASON_WRITE_REQUEST_USER 2
#define BGLIB_ATTRIBUTES_ATTRIBUTE_STATUS_FLAG_NOTIFY               1
#define BGLIB_ATTRIBUTES_ATTRIBUTE_STATUS_FLAG_INDICATE             2

#define BGLIB_CONNECTION_CONNECTED                                  1
#define BGLIB_CONNECTION_ENCRYPTED                                  2
#define BGLIB_CONNECTION_COMPLETED                                  4
#define BGLIB_CONNECTION_PARAMETERS_CHANGE                          8

#define BGLIB_ATTCLIENT_ATTRIBUTE_VALUE_TYPE_READ                   0
#define BGLIB_ATTCLIENT_ATTRIBUTE_VALUE_TYPE_NOTIFY                 1
#define BGLIB_ATTCLIENT_ATTRIBUTE_VALUE_TYPE_INDICATE               2
#define BGLIB_ATTCLIENT_ATTRIBUTE_VALUE_TYPE_READ_BY_TYPE           3
#define BGLIB_ATTCLIENT_ATTRIBUTE_VALUE_TYPE_READ_BLOB              4
#define BGLIB_ATTCLIENT_ATTRIBUTE_VALUE_TYPE_INDICATE_RSP_REQ       5

#define BGLIB_SM_BONDING_KEY_LTK                                    0x01
#define BGLIB_SM_BONDING_KEY_ADDR_PUBLIC                            0x02
#define BGLIB_SM_BONDING_KEY_ADDR_STATIC                            0x04
#define BGLIB_SM_BONDING_KEY_IRK                                    0x08
#define BGLIB_SM_BONDING_KEY_EDIVRAND                               0x10
#define BGLIB_SM_BONDING_KEY_CSRK                                   0x20
#define BGLIB_SM_BONDING_KEY_MASTERID                               0x40
#define BGLIB_SM_IO_CAPABILITY_DISPLAYONLY                          0
#define BGLIB_SM_IO_CAPABILITY_DISPLAYYESNO                         1
#define BGLIB_SM_IO_CAPABILITY_KEYBOARDONLY                         2
#define BGLIB_SM_IO_CAPABILITY_NOINPUTNOOUTPUT                      3
#define BGLIB_SM_IO_CAPABILITY_KEYBOARDDISPLAY                      4

#define BGLIB_GAP_ADDRESS_TYPE_PUBLIC                               0
#define BGLIB_GAP_ADDRESS_TYPE_RANDOM                               1
#define BGLIB_GAP_NON_DISCOVERABLE                                  0
#define BGLIB_GAP_LIMITED_DISCOVERABLE                              1
#define BGLIB_GAP_GENERAL_DISCOVERABLE                              2
#define BGLIB_GAP_BROADCAST                                         3
#define BGLIB_GAP_USER_DATA                                         4
#define BGLIB_GAP_NON_CONNECTABLE                                   0
#define BGLIB_GAP_DIRECTED_CONNECTABLE                              1
#define BGLIB_GAP_UNDIRECTED_CONNECTABLE                            2
#define BGLIB_GAP_SCANNABLE_CONNECTABLE                             3
#define BGLIB_GAP_DISCOVER_LIMITED                                  0
#define BGLIB_GAP_DISCOVER_GENERIC                                  1
#define BGLIB_GAP_DISCOVER_OBSERVATION                              2
#define BGLIB_GAP_AD_TYPE_NONE                                      0
#define BGLIB_GAP_AD_TYPE_FLAGS                                     1
#define BGLIB_GAP_AD_TYPE_SERVICES_16BIT_MORE                       2
#define BGLIB_GAP_AD_TYPE_SERVICES_16BIT_ALL                        3
#define BGLIB_GAP_AD_TYPE_SERVICES_32BIT_MORE                       4
#define BGLIB_GAP_AD_TYPE_SERVICES_32BIT_ALL                        5
#define BGLIB_GAP_AD_TYPE_SERVICES_128BIT_MORE                      6
#define BGLIB_GAP_AD_TYPE_SERVICES_128BIT_ALL                       7
#define BGLIB_GAP_AD_TYPE_LOCALNAME_SHORT                           8
#define BGLIB_GAP_AD_TYPE_LOCALNAME_COMPLETE                        9
#define BGLIB_GAP_AD_TYPE_TXPOWER                                   10
#define BGLIB_GAP_ADV_POLICY_ALL                                    0
#define BGLIB_GAP_ADV_POLICY_WHITELIST_SCAN                         1
#define BGLIB_GAP_ADV_POLICY_WHITELIST_CONNECT                      2
#define BGLIB_GAP_ADV_POLICY_WHITELIST_ALL                          3
#define BGLIB_GAP_SCAN_POLICY_ALL                                   0
#define BGLIB_GAP_SCAN_POLICY_WHITELIST                             1
#define BGLIB_GAP_SCAN_HEADER_ADV_IND                               0
#define BGLIB_GAP_SCAN_HEADER_ADV_DIRECT_IND                        1
#define BGLIB_GAP_SCAN_HEADER_ADV_NONCONN_IND                       2
#define BGLIB_GAP_SCAN_HEADER_SCAN_REQ                              3
#define BGLIB_GAP_SCAN_HEADER_SCAN_RSP                              4
#define BGLIB_GAP_SCAN_HEADER_CONNECT_REQ                           5
#define BGLIB_GAP_SCAN_HEADER_ADV_DISCOVER_IND                      6
#define BGLIB_GAP_AD_FLAG_LIMITED_DISCOVERABLE                      0x01
#define BGLIB_GAP_AD_FLAG_GENERAL_DISCOVERABLE                      0x02
#define BGLIB_GAP_AD_FLAG_BREDR_NOT_SUPPORTED                       0x04
#define BGLIB_GAP_AD_FLAG_SIMULTANEOUS_LEBREDR_CTRL                 0x10
#define BGLIB_GAP_AD_FLAG_SIMULTANEOUS_LEBREDR_HOST                 0x20
#define BGLIB_GAP_AD_FLAG_MASK                                      0x1f

#define PACKED __attribute__((packed))
#define ALIGNED __attribute__((aligned(0x4)))

typedef uint8_t    uint8;
typedef uint16_t   uint16;
typedef int16_t    int16;
typedef uint32_t   uint32;
typedef int8_t     int8;

typedef struct bd_addr_t {
    uint8 addr[6];
} bd_addr;

typedef bd_addr hwaddr;
typedef struct {
    uint8 len;
    uint8 data[];
} uint8array;

typedef struct {
    uint8 len;
    int8 data[];
} string;

struct ble_header {
    uint8  type_hilen;
    uint8  lolen;
    uint8  cls;
    uint8  command;
};

struct ble_msg_system_reset_cmd_t {
    uint8 boot_in_dfu;
} PACKED;
struct ble_msg_system_address_get_rsp_t {
    bd_addr address;
} PACKED;
struct ble_msg_system_reg_write_cmd_t {
    uint16 address;
    uint8 value;
} PACKED;
struct ble_msg_system_reg_write_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_system_reg_read_cmd_t {
    uint16 address;
} PACKED;
struct ble_msg_system_reg_read_rsp_t {
    uint16 address;
    uint8 value;
} PACKED;
struct ble_msg_system_get_counters_rsp_t {
    uint8 txok;
    uint8 txretry;
    uint8 rxok;
    uint8 rxfail;
    uint8 mbuf;
} PACKED;
struct ble_msg_system_get_connections_rsp_t {
    uint8 maxconn;
} PACKED;
struct ble_msg_system_read_memory_cmd_t {
    uint32 address;
    uint8 length;
} PACKED;
struct ble_msg_system_read_memory_rsp_t {
    uint32 address;
    uint8array data;
} PACKED;
struct ble_msg_system_get_info_rsp_t {
    uint16 major;
    uint16 minor;
    uint16 patch;
    uint16 build;
    uint16 ll_version;
    uint8 protocol_version;
    uint8 hw;
} PACKED;
struct ble_msg_system_endpoint_tx_cmd_t {
    uint8 endpoint;
    uint8array data;
} PACKED;
struct ble_msg_system_endpoint_tx_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_system_whitelist_append_cmd_t {
    bd_addr address;
    uint8 address_type;
} PACKED;
struct ble_msg_system_whitelist_append_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_system_whitelist_remove_cmd_t {
    bd_addr address;
    uint8 address_type;
} PACKED;
struct ble_msg_system_whitelist_remove_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_system_endpoint_rx_cmd_t {
    uint8 endpoint;
    uint8 size;
} PACKED;
struct ble_msg_system_endpoint_rx_rsp_t {
    uint16 result;
    uint8array data;
} PACKED;
struct ble_msg_system_endpoint_set_watermarks_cmd_t {
    uint8 endpoint;
    uint8 rx;
    uint8 tx;
} PACKED;
struct ble_msg_system_endpoint_set_watermarks_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_system_boot_evt_t {
    uint16 major;
    uint16 minor;
    uint16 patch;
    uint16 build;
    uint16 ll_version;
    uint8 protocol_version;
    uint8 hw;
} PACKED;
struct ble_msg_system_debug_evt_t {
    uint8array data;
} PACKED;
struct ble_msg_system_endpoint_watermark_rx_evt_t {
    uint8 endpoint;
    uint8 data;
} PACKED;
struct ble_msg_system_endpoint_watermark_tx_evt_t {
    uint8 endpoint;
    uint8 data;
} PACKED;
struct ble_msg_system_script_failure_evt_t {
    uint16 address;
    uint16 reason;
} PACKED;
struct ble_msg_flash_ps_save_cmd_t {
    uint16 key;
    uint8array value;
} PACKED;
struct ble_msg_flash_ps_save_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_flash_ps_load_cmd_t {
    uint16 key;
} PACKED;
struct ble_msg_flash_ps_load_rsp_t {
    uint16 result;
    uint8array value;
} PACKED;
struct ble_msg_flash_ps_erase_cmd_t {
    uint16 key;
} PACKED;
struct ble_msg_flash_erase_page_cmd_t {
    uint8 page;
} PACKED;
struct ble_msg_flash_erase_page_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_flash_write_words_cmd_t {
    uint16 address;
    uint8array words;
} PACKED;
struct ble_msg_flash_ps_key_evt_t {
    uint16 key;
    uint8array value;
} PACKED;
struct ble_msg_attributes_write_cmd_t {
    uint16 handle;
    uint8 offset;
    uint8array value;
} PACKED;
struct ble_msg_attributes_write_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_attributes_read_cmd_t {
    uint16 handle;
    uint16 offset;
} PACKED;
struct ble_msg_attributes_read_rsp_t {
    uint16 handle;
    uint16 offset;
    uint16 result;
    uint8array value;
} PACKED;
struct ble_msg_attributes_read_type_cmd_t {
    uint16 handle;
} PACKED;
struct ble_msg_attributes_read_type_rsp_t {
    uint16 handle;
    uint16 result;
    uint8array value;
} PACKED;
struct ble_msg_attributes_user_read_response_cmd_t {
    uint8 connection;
    uint8 att_error;
    uint8array value;
} PACKED;
struct ble_msg_attributes_user_write_response_cmd_t {
    uint8 connection;
    uint8 att_error;
} PACKED;
struct ble_msg_attributes_value_evt_t {
    uint8 connection;
    uint8 reason;
    uint16 handle;
    uint16 offset;
    uint8array value;
} PACKED;
struct ble_msg_attributes_user_read_request_evt_t {
    uint8 connection;
    uint16 handle;
    uint16 offset;
} PACKED;
struct ble_msg_attributes_status_evt_t {
    uint16 handle;
    uint8 flags;
} PACKED;
struct ble_msg_connection_disconnect_cmd_t {
    uint8 connection;
} PACKED;
struct ble_msg_connection_disconnect_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_connection_get_rssi_cmd_t {
    uint8 connection;
} PACKED;
struct ble_msg_connection_get_rssi_rsp_t {
    uint8 connection;
    int8 rssi;
} PACKED;
struct ble_msg_connection_update_cmd_t {
    uint8 connection;
    uint16 interval_min;
    uint16 interval_max;
    uint16 latency;
    uint16 timeout;
} PACKED;
struct ble_msg_connection_update_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_connection_version_update_cmd_t {
    uint8 connection;
} PACKED;
struct ble_msg_connection_version_update_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_connection_channel_map_get_cmd_t {
    uint8 connection;
} PACKED;
struct ble_msg_connection_channel_map_get_rsp_t {
    uint8 connection;
    uint8array map;
} PACKED;
struct ble_msg_connection_channel_map_set_cmd_t {
    uint8 connection;
    uint8array map;
} PACKED;
struct ble_msg_connection_channel_map_set_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_connection_features_get_cmd_t {
    uint8 connection;
} PACKED;
struct ble_msg_connection_features_get_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_connection_get_status_cmd_t {
    uint8 connection;
} PACKED;
struct ble_msg_connection_get_status_rsp_t {
    uint8 connection;
} PACKED;
struct ble_msg_connection_raw_tx_cmd_t {
    uint8 connection;
    uint8array data;
} PACKED;
struct ble_msg_connection_raw_tx_rsp_t {
    uint8 connection;
} PACKED;
struct ble_msg_connection_status_evt_t {
    uint8 connection;
    uint8 flags;
    bd_addr address;
    uint8 address_type;
    uint16 conn_interval;
    uint16 timeout;
    uint16 latency;
    uint8 bonding;
} PACKED;
struct ble_msg_connection_version_ind_evt_t {
    uint8 connection;
    uint8 vers_nr;
    uint16 comp_id;
    uint16 sub_vers_nr;
} PACKED;
struct ble_msg_connection_feature_ind_evt_t {
    uint8 connection;
    uint8array features;
} PACKED;
struct ble_msg_connection_raw_rx_evt_t {
    uint8 connection;
    uint8array data;
} PACKED;
struct ble_msg_connection_disconnected_evt_t {
    uint8 connection;
    uint16 reason;
} PACKED;
struct ble_msg_attclient_find_by_type_value_cmd_t {
    uint8 connection;
    uint16 start;
    uint16 end;
    uint16 uuid;
    uint8array value;
} PACKED;
struct ble_msg_attclient_find_by_type_value_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_attclient_read_by_group_type_cmd_t {
    uint8 connection;
    uint16 start;
    uint16 end;
    uint8array uuid;
} PACKED;
struct ble_msg_attclient_read_by_group_type_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_attclient_read_by_type_cmd_t {
    uint8 connection;
    uint16 start;
    uint16 end;
    uint8array uuid;
} PACKED;
struct ble_msg_attclient_read_by_type_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_attclient_find_information_cmd_t {
    uint8 connection;
    uint16 start;
    uint16 end;
} PACKED;
struct ble_msg_attclient_find_information_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_attclient_read_by_handle_cmd_t {
    uint8 connection;
    uint16 chrhandle;
} PACKED;
struct ble_msg_attclient_read_by_handle_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_attclient_attribute_write_cmd_t {
    uint8 connection;
    uint16 atthandle;
    uint8array data;
} PACKED;
struct ble_msg_attclient_attribute_write_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_attclient_write_command_cmd_t {
    uint8 connection;
    uint16 atthandle;
    uint8array data;
} PACKED;
struct ble_msg_attclient_write_command_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_attclient_indicate_confirm_cmd_t {
    uint8 connection;
} PACKED;
struct ble_msg_attclient_indicate_confirm_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_attclient_read_long_cmd_t {
    uint8 connection;
    uint16 chrhandle;
} PACKED;
struct ble_msg_attclient_read_long_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_attclient_prepare_write_cmd_t {
    uint8 connection;
    uint16 atthandle;
    uint16 offset;
    uint8array data;
} PACKED;
struct ble_msg_attclient_prepare_write_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_attclient_execute_write_cmd_t {
    uint8 connection;
    uint8 commit;
} PACKED;
struct ble_msg_attclient_execute_write_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_attclient_read_multiple_cmd_t {
    uint8 connection;
    uint8array handles;
} PACKED;
struct ble_msg_attclient_read_multiple_rsp_t {
    uint8 connection;
    uint16 result;
} PACKED;
struct ble_msg_attclient_indicated_evt_t {
    uint8 connection;
    uint16 attrhandle;
} PACKED;
struct ble_msg_attclient_procedure_completed_evt_t {
    uint8 connection;
    uint16 result;
    uint16 chrhandle;
} PACKED;
struct ble_msg_attclient_group_found_evt_t {
    uint8 connection;
    uint16 start;
    uint16 end;
    uint8array uuid;
} PACKED;
struct ble_msg_attclient_attribute_found_evt_t {
    uint8 connection;
    uint16 chrdecl;
    uint16 value;
    uint8 properties;
    uint8array uuid;
} PACKED;
struct ble_msg_attclient_find_information_found_evt_t {
    uint8 connection;
    uint16 chrhandle;
    uint8array uuid;
} PACKED;
struct ble_msg_attclient_attribute_value_evt_t {
    uint8 connection;
    uint16 atthandle;
    uint8 type;
    uint8array value;
} PACKED;
struct ble_msg_attclient_read_multiple_response_evt_t {
    uint8 connection;
    uint8array handles;
} PACKED;
struct ble_msg_sm_encrypt_start_cmd_t {
    uint8 handle;
    uint8 bonding;
} PACKED;
struct ble_msg_sm_encrypt_start_rsp_t {
    uint8 handle;
    uint16 result;
} PACKED;
struct ble_msg_sm_set_bondable_mode_cmd_t {
    uint8 bondable;
} PACKED;
struct ble_msg_sm_delete_bonding_cmd_t {
    uint8 handle;
} PACKED;
struct ble_msg_sm_delete_bonding_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_sm_set_parameters_cmd_t {
    uint8 mitm;
    uint8 min_key_size;
    uint8 io_capabilities;
} PACKED;
struct ble_msg_sm_passkey_entry_cmd_t {
    uint8 handle;
    uint32 passkey;
} PACKED;
struct ble_msg_sm_passkey_entry_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_sm_get_bonds_rsp_t {
    uint8 bonds;
} PACKED;
struct ble_msg_sm_set_oob_data_cmd_t {
    uint8array oob;
} PACKED;
struct ble_msg_sm_smp_data_evt_t {
    uint8 handle;
    uint8 packet;
    uint8array data;
} PACKED;
struct ble_msg_sm_bonding_fail_evt_t {
    uint8 handle;
    uint16 result;
} PACKED;
struct ble_msg_sm_passkey_display_evt_t {
    uint8 handle;
    uint32 passkey;
} PACKED;
struct ble_msg_sm_passkey_request_evt_t {
    uint8 handle;
} PACKED;
struct ble_msg_sm_bond_status_evt_t {
    uint8 bond;
    uint8 keysize;
    uint8 mitm;
    uint8 keys;
} PACKED;
struct ble_msg_gap_set_privacy_flags_cmd_t {
    uint8 peripheral_privacy;
    uint8 central_privacy;
} PACKED;
struct ble_msg_gap_set_mode_cmd_t {
    uint8 discover;
    uint8 connect;
} PACKED;
struct ble_msg_gap_set_mode_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_gap_discover_cmd_t {
    uint8 mode;
} PACKED;
struct ble_msg_gap_discover_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_gap_connect_direct_cmd_t {
    bd_addr address;
    uint8 addr_type;
    uint16 conn_interval_min;
    uint16 conn_interval_max;
    uint16 timeout;
    uint16 latency;
} PACKED;
struct ble_msg_gap_connect_direct_rsp_t {
    uint16 result;
    uint8 connection_handle;
} PACKED;
struct ble_msg_gap_end_procedure_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_gap_connect_selective_cmd_t {
    uint16 conn_interval_min;
    uint16 conn_interval_max;
    uint16 timeout;
    uint16 latency;
} PACKED;
struct ble_msg_gap_connect_selective_rsp_t {
    uint16 result;
    uint8 connection_handle;
} PACKED;
struct ble_msg_gap_set_filtering_cmd_t {
    uint8 scan_policy;
    uint8 adv_policy;
    uint8 scan_duplicate_filtering;
} PACKED;
struct ble_msg_gap_set_filtering_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_gap_set_scan_parameters_cmd_t {
    uint16 scan_interval;
    uint16 scan_window;
    uint8 active;
} PACKED;
struct ble_msg_gap_set_scan_parameters_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_gap_set_adv_parameters_cmd_t {
    uint16 adv_interval_min;
    uint16 adv_interval_max;
    uint8 adv_channels;
} PACKED;
struct ble_msg_gap_set_adv_parameters_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_gap_set_adv_data_cmd_t {
    uint8 set_scanrsp;
    uint8array adv_data;
} PACKED;
struct ble_msg_gap_set_adv_data_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_gap_set_directed_connectable_mode_cmd_t {
    bd_addr address;
    uint8 addr_type;
} PACKED;
struct ble_msg_gap_set_directed_connectable_mode_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_gap_scan_response_evt_t {
    int8 rssi;
    uint8 packet_type;
    bd_addr sender;
    uint8 address_type;
    uint8 bond;
    uint8array data;
} PACKED;
struct ble_msg_gap_mode_changed_evt_t {
    uint8 discover;
    uint8 connect;
} PACKED;
struct ble_msg_hardware_io_port_config_irq_cmd_t {
    uint8 port;
    uint8 enable_bits;
    uint8 falling_edge;
} PACKED;
struct ble_msg_hardware_io_port_config_irq_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_hardware_set_soft_timer_cmd_t {
    uint32 time;
    uint8 handle;
    uint8 single_shot;
} PACKED;
struct ble_msg_hardware_set_soft_timer_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_hardware_adc_read_cmd_t {
    uint8 input;
    uint8 decimation;
    uint8 reference_selection;
} PACKED;
struct ble_msg_hardware_adc_read_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_hardware_io_port_config_direction_cmd_t {
    uint8 port;
    uint8 direction;
} PACKED;
struct ble_msg_hardware_io_port_config_direction_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_hardware_io_port_config_function_cmd_t {
    uint8 port;
    uint8 function;
} PACKED;
struct ble_msg_hardware_io_port_config_function_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_hardware_io_port_config_pull_cmd_t {
    uint8 port;
    uint8 tristate_mask;
    uint8 pull_up;
} PACKED;
struct ble_msg_hardware_io_port_config_pull_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_hardware_io_port_write_cmd_t {
    uint8 port;
    uint8 mask;
    uint8 data;
} PACKED;
struct ble_msg_hardware_io_port_write_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_hardware_io_port_read_cmd_t {
    uint8 port;
    uint8 mask;
} PACKED;
struct ble_msg_hardware_io_port_read_rsp_t {
    uint16 result;
    uint8 port;
    uint8 data;
} PACKED;
struct ble_msg_hardware_spi_config_cmd_t {
    uint8 channel;
    uint8 polarity;
    uint8 phase;
    uint8 bit_order;
    uint8 baud_e;
    uint8 baud_m;
} PACKED;
struct ble_msg_hardware_spi_config_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_hardware_spi_transfer_cmd_t {
    uint8 channel;
    uint8array data;
} PACKED;
struct ble_msg_hardware_spi_transfer_rsp_t {
    uint16 result;
    uint8 channel;
    uint8array data;
} PACKED;
struct ble_msg_hardware_i2c_read_cmd_t {
    uint8 address;
    uint8 stop;
    uint8 length;
} PACKED;
struct ble_msg_hardware_i2c_read_rsp_t {
    uint16 result;
    uint8array data;
} PACKED;
struct ble_msg_hardware_i2c_write_cmd_t {
    uint8 address;
    uint8 stop;
    uint8array data;
} PACKED;
struct ble_msg_hardware_i2c_write_rsp_t {
    uint8 written;
} PACKED;
struct ble_msg_hardware_set_txpower_cmd_t {
    int8 power;
} PACKED;
struct ble_msg_hardware_timer_comparator_cmd_t {
    uint8 timer;
    uint8 channel;
    uint8 mode;
    uint16 comparator_value;
} PACKED;
struct ble_msg_hardware_timer_comparator_rsp_t {
    uint16 result;
} PACKED;
struct ble_msg_hardware_io_port_status_evt_t {
    uint32 timestamp;
    uint8 port;
    uint8 irq;
    uint8 state;
} PACKED;
struct ble_msg_hardware_soft_timer_evt_t {
    uint8 handle;
} PACKED;
struct ble_msg_hardware_adc_result_evt_t {
    uint8 input;
    int16 value;
} PACKED;
struct ble_msg_test_phy_tx_cmd_t {
    uint8 channel;
    uint8 length;
    uint8 type;
} PACKED;
struct ble_msg_test_phy_rx_cmd_t {
    uint8 channel;
} PACKED;
struct ble_msg_test_phy_end_rsp_t {
    uint16 counter;
} PACKED;
struct ble_msg_test_get_channel_map_rsp_t {
    uint8array channel_map;
} PACKED;
struct ble_msg_test_debug_cmd_t {
    uint8array input;
} PACKED;
struct ble_msg_test_debug_rsp_t {
    uint8array output;
} PACKED;

class BGLib {
    public:
        BGLib(HardwareSerial *module=0, HardwareSerial *output=0);
        uint8_t checkActivity(uint16_t timeout=0);
        uint8_t checkError();
        uint8_t checkTimeout();
        void setBusy(bool busyEnabled);

        // set/update UART port objects
        void setModuleUART(HardwareSerial *module);
        void setOutputUART(HardwareSerial *debug);

        uint8_t parse(uint8_t ch, uint8_t packetMode=0);
        uint8_t sendCommand(uint16_t len, uint8_t commandClass, uint8_t commandId, void *payload=0);

        void (*onBusy)();
        void (*onIdle)();
        void (*onTimeout)();

        uint8_t ble_cmd_system_reset(uint8 boot_in_dfu);
        uint8_t ble_cmd_system_hello();
        uint8_t ble_cmd_system_address_get();
        uint8_t ble_cmd_system_reg_write(uint16 address, uint8 value);
        uint8_t ble_cmd_system_reg_read(uint16 address);
        uint8_t ble_cmd_system_get_counters();
        uint8_t ble_cmd_system_get_connections();
        uint8_t ble_cmd_system_read_memory(uint32 address, uint8 length);
        uint8_t ble_cmd_system_get_info();
        uint8_t ble_cmd_system_endpoint_tx(uint8 endpoint, uint8array data);
        uint8_t ble_cmd_system_whitelist_append(bd_addr address, uint8 address_type);
        uint8_t ble_cmd_system_whitelist_remove(bd_addr address, uint8 address_type);
        uint8_t ble_cmd_system_whitelist_clear();
        uint8_t ble_cmd_system_endpoint_rx(uint8 endpoint, uint8 size);
        uint8_t ble_cmd_system_endpoint_set_watermarks(uint8 endpoint, uint8 rx, uint8 tx);
        uint8_t ble_cmd_flash_ps_defrag();
        uint8_t ble_cmd_flash_ps_dump();
        uint8_t ble_cmd_flash_ps_erase_all();
        uint8_t ble_cmd_flash_ps_save(uint16 key, uint8array value);
        uint8_t ble_cmd_flash_ps_load(uint16 key);
        uint8_t ble_cmd_flash_ps_erase(uint16 key);
        uint8_t ble_cmd_flash_erase_page(uint8 page);
        uint8_t ble_cmd_flash_write_words(uint16 address, uint8array words);
        uint8_t ble_cmd_attributes_write(uint16 handle, uint8 offset, uint8array value);
        uint8_t ble_cmd_attributes_read(uint16 handle, uint16 offset);
        uint8_t ble_cmd_attributes_read_type(uint16 handle);
        uint8_t ble_cmd_attributes_user_read_response(uint8 connection, uint8 att_error, uint8array value);
        uint8_t ble_cmd_attributes_user_write_response(uint8 connection, uint8 att_error);
        uint8_t ble_cmd_connection_disconnect(uint8 connection);
        uint8_t ble_cmd_connection_get_rssi(uint8 connection);
        uint8_t ble_cmd_connection_update(uint8 connection, uint16 interval_min, uint16 interval_max, uint16 latency, uint16 timeout);
        uint8_t ble_cmd_connection_version_update(uint8 connection);
        uint8_t ble_cmd_connection_channel_map_get(uint8 connection);
        uint8_t ble_cmd_connection_channel_map_set(uint8 connection, uint8array map);
        uint8_t ble_cmd_connection_features_get(uint8 connection);
        uint8_t ble_cmd_connection_get_status(uint8 connection);
        uint8_t ble_cmd_connection_raw_tx(uint8 connection, uint8array data);
        uint8_t ble_cmd_attclient_find_by_type_value(uint8 connection, uint16 start, uint16 end, uint16 uuid, uint8array value);
        uint8_t ble_cmd_attclient_read_by_group_type(uint8 connection, uint16 start, uint16 end, uint8array uuid);
        uint8_t ble_cmd_attclient_read_by_type(uint8 connection, uint16 start, uint16 end, uint8array uuid);
        uint8_t ble_cmd_attclient_find_information(uint8 connection, uint16 start, uint16 end);
        uint8_t ble_cmd_attclient_read_by_handle(uint8 connection, uint16 chrhandle);
        uint8_t ble_cmd_attclient_attribute_write(uint8 connection, uint16 atthandle, uint8array data);
        uint8_t ble_cmd_attclient_write_command(uint8 connection, uint16 atthandle, uint8array data);
        uint8_t ble_cmd_attclient_indicate_confirm(uint8 connection);
        uint8_t ble_cmd_attclient_read_long(uint8 connection, uint16 chrhandle);
        uint8_t ble_cmd_attclient_prepare_write(uint8 connection, uint16 atthandle, uint16 offset, uint8array data);
        uint8_t ble_cmd_attclient_execute_write(uint8 connection, uint8 commit);
        uint8_t ble_cmd_attclient_read_multiple(uint8 connection, uint8array handles);
        uint8_t ble_cmd_sm_encrypt_start(uint8 handle, uint8 bonding);
        uint8_t ble_cmd_sm_set_bondable_mode(uint8 bondable);
        uint8_t ble_cmd_sm_delete_bonding(uint8 handle);
        uint8_t ble_cmd_sm_set_parameters(uint8 mitm, uint8 min_key_size, uint8 io_capabilities);
        uint8_t ble_cmd_sm_passkey_entry(uint8 handle, uint32 passkey);
        uint8_t ble_cmd_sm_get_bonds();
        uint8_t ble_cmd_sm_set_oob_data(uint8array oob);
        uint8_t ble_cmd_gap_set_privacy_flags(uint8 peripheral_privacy, uint8 central_privacy);
        uint8_t ble_cmd_gap_set_mode(uint8 discover, uint8 connect);
        uint8_t ble_cmd_gap_discover(uint8 mode);
        uint8_t ble_cmd_gap_connect_direct(bd_addr address, uint8 addr_type, uint16 conn_interval_min, uint16 conn_interval_max, uint16 timeout, uint16 latency);
        uint8_t ble_cmd_gap_end_procedure();
        uint8_t ble_cmd_gap_connect_selective(uint16 conn_interval_min, uint16 conn_interval_max, uint16 timeout, uint16 latency);
        uint8_t ble_cmd_gap_set_filtering(uint8 scan_policy, uint8 adv_policy, uint8 scan_duplicate_filtering);
        uint8_t ble_cmd_gap_set_scan_parameters(uint16 scan_interval, uint16 scan_window, uint8 active);
        uint8_t ble_cmd_gap_set_adv_parameters(uint16 adv_interval_min, uint16 adv_interval_max, uint8 adv_channels);
        uint8_t ble_cmd_gap_set_adv_data(uint8 set_scanrsp, uint8array adv_data);
        uint8_t ble_cmd_gap_set_directed_connectable_mode(bd_addr address, uint8 addr_type);
        uint8_t ble_cmd_hardware_io_port_config_irq(uint8 port, uint8 enable_bits, uint8 falling_edge);
        uint8_t ble_cmd_hardware_set_soft_timer(uint32 time, uint8 handle, uint8 single_shot);
        uint8_t ble_cmd_hardware_adc_read(uint8 input, uint8 decimation, uint8 reference_selection);
        uint8_t ble_cmd_hardware_io_port_config_direction(uint8 port, uint8 direction);
        uint8_t ble_cmd_hardware_io_port_config_function(uint8 port, uint8 function);
        uint8_t ble_cmd_hardware_io_port_config_pull(uint8 port, uint8 tristate_mask, uint8 pull_up);
        uint8_t ble_cmd_hardware_io_port_write(uint8 port, uint8 mask, uint8 data);
        uint8_t ble_cmd_hardware_io_port_read(uint8 port, uint8 mask);
        uint8_t ble_cmd_hardware_spi_config(uint8 channel, uint8 polarity, uint8 phase, uint8 bit_order, uint8 baud_e, uint8 baud_m);
        uint8_t ble_cmd_hardware_spi_transfer(uint8 channel, uint8array data);
        uint8_t ble_cmd_hardware_i2c_read(uint8 address, uint8 stop, uint8 length);
        uint8_t ble_cmd_hardware_i2c_write(uint8 address, uint8 stop, uint8array data);
        uint8_t ble_cmd_hardware_set_txpower(int8 power);
        uint8_t ble_cmd_hardware_timer_comparator(uint8 timer, uint8 channel, uint8 mode, uint16 comparator_value);
        uint8_t ble_cmd_test_phy_tx(uint8 channel, uint8 length, uint8 type);
        uint8_t ble_cmd_test_phy_rx(uint8 channel);
        uint8_t ble_cmd_test_phy_end();
        uint8_t ble_cmd_test_phy_reset();
        uint8_t ble_cmd_test_get_channel_map();
        uint8_t ble_cmd_test_debug(uint8array input);

        void (*ble_rsp_system_reset)(const struct ble_msg_system_reset_rsp_t *msg);
        void (*ble_rsp_system_hello)(const struct ble_msg_system_hello_rsp_t *msg);
        void (*ble_rsp_system_address_get)(const struct ble_msg_system_address_get_rsp_t *msg);
        void (*ble_rsp_system_reg_write)(const struct ble_msg_system_reg_write_rsp_t *msg);
        void (*ble_rsp_system_reg_read)(const struct ble_msg_system_reg_read_rsp_t *msg);
        void (*ble_rsp_system_get_counters)(const struct ble_msg_system_get_counters_rsp_t *msg);
        void (*ble_rsp_system_get_connections)(const struct ble_msg_system_get_connections_rsp_t *msg);
        void (*ble_rsp_system_read_memory)(const struct ble_msg_system_read_memory_rsp_t *msg);
        void (*ble_rsp_system_get_info)(const struct ble_msg_system_get_info_rsp_t *msg);
        void (*ble_rsp_system_endpoint_tx)(const struct ble_msg_system_endpoint_tx_rsp_t *msg);
        void (*ble_rsp_system_whitelist_append)(const struct ble_msg_system_whitelist_append_rsp_t *msg);
        void (*ble_rsp_system_whitelist_remove)(const struct ble_msg_system_whitelist_remove_rsp_t *msg);
        void (*ble_rsp_system_whitelist_clear)(const struct ble_msg_system_whitelist_clear_rsp_t *msg);
        void (*ble_rsp_system_endpoint_rx)(const struct ble_msg_system_endpoint_rx_rsp_t *msg);
        void (*ble_rsp_system_endpoint_set_watermarks)(const struct ble_msg_system_endpoint_set_watermarks_rsp_t *msg);
        void (*ble_rsp_flash_ps_defrag)(const struct ble_msg_flash_ps_defrag_rsp_t *msg);
        void (*ble_rsp_flash_ps_dump)(const struct ble_msg_flash_ps_dump_rsp_t *msg);
        void (*ble_rsp_flash_ps_erase_all)(const struct ble_msg_flash_ps_erase_all_rsp_t *msg);
        void (*ble_rsp_flash_ps_save)(const struct ble_msg_flash_ps_save_rsp_t *msg);
        void (*ble_rsp_flash_ps_load)(const struct ble_msg_flash_ps_load_rsp_t *msg);
        void (*ble_rsp_flash_ps_erase)(const struct ble_msg_flash_ps_erase_rsp_t *msg);
        void (*ble_rsp_flash_erase_page)(const struct ble_msg_flash_erase_page_rsp_t *msg);
        void (*ble_rsp_flash_write_words)(const struct ble_msg_flash_write_words_rsp_t *msg);
        void (*ble_rsp_attributes_write)(const struct ble_msg_attributes_write_rsp_t *msg);
        void (*ble_rsp_attributes_read)(const struct ble_msg_attributes_read_rsp_t *msg);
        void (*ble_rsp_attributes_read_type)(const struct ble_msg_attributes_read_type_rsp_t *msg);
        void (*ble_rsp_attributes_user_read_response)(const struct ble_msg_attributes_user_read_response_rsp_t *msg);
        void (*ble_rsp_attributes_user_write_response)(const struct ble_msg_attributes_user_write_response_rsp_t *msg);
        void (*ble_rsp_connection_disconnect)(const struct ble_msg_connection_disconnect_rsp_t *msg);
        void (*ble_rsp_connection_get_rssi)(const struct ble_msg_connection_get_rssi_rsp_t *msg);
        void (*ble_rsp_connection_update)(const struct ble_msg_connection_update_rsp_t *msg);
        void (*ble_rsp_connection_version_update)(const struct ble_msg_connection_version_update_rsp_t *msg);
        void (*ble_rsp_connection_channel_map_get)(const struct ble_msg_connection_channel_map_get_rsp_t *msg);
        void (*ble_rsp_connection_channel_map_set)(const struct ble_msg_connection_channel_map_set_rsp_t *msg);
        void (*ble_rsp_connection_features_get)(const struct ble_msg_connection_features_get_rsp_t *msg);
        void (*ble_rsp_connection_get_status)(const struct ble_msg_connection_get_status_rsp_t *msg);
        void (*ble_rsp_connection_raw_tx)(const struct ble_msg_connection_raw_tx_rsp_t *msg);
        void (*ble_rsp_attclient_find_by_type_value)(const struct ble_msg_attclient_find_by_type_value_rsp_t *msg);
        void (*ble_rsp_attclient_read_by_group_type)(const struct ble_msg_attclient_read_by_group_type_rsp_t *msg);
        void (*ble_rsp_attclient_read_by_type)(const struct ble_msg_attclient_read_by_type_rsp_t *msg);
        void (*ble_rsp_attclient_find_information)(const struct ble_msg_attclient_find_information_rsp_t *msg);
        void (*ble_rsp_attclient_read_by_handle)(const struct ble_msg_attclient_read_by_handle_rsp_t *msg);
        void (*ble_rsp_attclient_attribute_write)(const struct ble_msg_attclient_attribute_write_rsp_t *msg);
        void (*ble_rsp_attclient_write_command)(const struct ble_msg_attclient_write_command_rsp_t *msg);
        void (*ble_rsp_attclient_indicate_confirm)(const struct ble_msg_attclient_indicate_confirm_rsp_t *msg);
        void (*ble_rsp_attclient_read_long)(const struct ble_msg_attclient_read_long_rsp_t *msg);
        void (*ble_rsp_attclient_prepare_write)(const struct ble_msg_attclient_prepare_write_rsp_t *msg);
        void (*ble_rsp_attclient_execute_write)(const struct ble_msg_attclient_execute_write_rsp_t *msg);
        void (*ble_rsp_attclient_read_multiple)(const struct ble_msg_attclient_read_multiple_rsp_t *msg);
        void (*ble_rsp_sm_encrypt_start)(const struct ble_msg_sm_encrypt_start_rsp_t *msg);
        void (*ble_rsp_sm_set_bondable_mode)(const struct ble_msg_sm_set_bondable_mode_rsp_t *msg);
        void (*ble_rsp_sm_delete_bonding)(const struct ble_msg_sm_delete_bonding_rsp_t *msg);
        void (*ble_rsp_sm_set_parameters)(const struct ble_msg_sm_set_parameters_rsp_t *msg);
        void (*ble_rsp_sm_passkey_entry)(const struct ble_msg_sm_passkey_entry_rsp_t *msg);
        void (*ble_rsp_sm_get_bonds)(const struct ble_msg_sm_get_bonds_rsp_t *msg);
        void (*ble_rsp_sm_set_oob_data)(const struct ble_msg_sm_set_oob_data_rsp_t *msg);
        void (*ble_rsp_gap_set_privacy_flags)(const struct ble_msg_gap_set_privacy_flags_rsp_t *msg);
        void (*ble_rsp_gap_set_mode)(const struct ble_msg_gap_set_mode_rsp_t *msg);
        void (*ble_rsp_gap_discover)(const struct ble_msg_gap_discover_rsp_t *msg);
        void (*ble_rsp_gap_connect_direct)(const struct ble_msg_gap_connect_direct_rsp_t *msg);
        void (*ble_rsp_gap_end_procedure)(const struct ble_msg_gap_end_procedure_rsp_t *msg);
        void (*ble_rsp_gap_connect_selective)(const struct ble_msg_gap_connect_selective_rsp_t *msg);
        void (*ble_rsp_gap_set_filtering)(const struct ble_msg_gap_set_filtering_rsp_t *msg);
        void (*ble_rsp_gap_set_scan_parameters)(const struct ble_msg_gap_set_scan_parameters_rsp_t *msg);
        void (*ble_rsp_gap_set_adv_parameters)(const struct ble_msg_gap_set_adv_parameters_rsp_t *msg);
        void (*ble_rsp_gap_set_adv_data)(const struct ble_msg_gap_set_adv_data_rsp_t *msg);
        void (*ble_rsp_gap_set_directed_connectable_mode)(const struct ble_msg_gap_set_directed_connectable_mode_rsp_t *msg);
        void (*ble_rsp_hardware_io_port_config_irq)(const struct ble_msg_hardware_io_port_config_irq_rsp_t *msg);
        void (*ble_rsp_hardware_set_soft_timer)(const struct ble_msg_hardware_set_soft_timer_rsp_t *msg);
        void (*ble_rsp_hardware_adc_read)(const struct ble_msg_hardware_adc_read_rsp_t *msg);
        void (*ble_rsp_hardware_io_port_config_direction)(const struct ble_msg_hardware_io_port_config_direction_rsp_t *msg);
        void (*ble_rsp_hardware_io_port_config_function)(const struct ble_msg_hardware_io_port_config_function_rsp_t *msg);
        void (*ble_rsp_hardware_io_port_config_pull)(const struct ble_msg_hardware_io_port_config_pull_rsp_t *msg);
        void (*ble_rsp_hardware_io_port_write)(const struct ble_msg_hardware_io_port_write_rsp_t *msg);
        void (*ble_rsp_hardware_io_port_read)(const struct ble_msg_hardware_io_port_read_rsp_t *msg);
        void (*ble_rsp_hardware_spi_config)(const struct ble_msg_hardware_spi_config_rsp_t *msg);
        void (*ble_rsp_hardware_spi_transfer)(const struct ble_msg_hardware_spi_transfer_rsp_t *msg);
        void (*ble_rsp_hardware_i2c_read)(const struct ble_msg_hardware_i2c_read_rsp_t *msg);
        void (*ble_rsp_hardware_i2c_write)(const struct ble_msg_hardware_i2c_write_rsp_t *msg);
        void (*ble_rsp_hardware_set_txpower)(const struct ble_msg_hardware_set_txpower_rsp_t *msg);
        void (*ble_rsp_hardware_timer_comparator)(const struct ble_msg_hardware_timer_comparator_rsp_t *msg);
        void (*ble_rsp_test_phy_tx)(const struct ble_msg_test_phy_tx_rsp_t *msg);
        void (*ble_rsp_test_phy_rx)(const struct ble_msg_test_phy_rx_rsp_t *msg);
        void (*ble_rsp_test_phy_end)(const struct ble_msg_test_phy_end_rsp_t *msg);
        void (*ble_rsp_test_phy_reset)(const struct ble_msg_test_phy_reset_rsp_t *msg);
        void (*ble_rsp_test_get_channel_map)(const struct ble_msg_test_get_channel_map_rsp_t *msg);
        void (*ble_rsp_test_debug)(const struct ble_msg_test_debug_rsp_t *msg);

        void (*ble_evt_system_boot)(const struct ble_msg_system_boot_evt_t *msg);
        void (*ble_evt_system_debug)(const struct ble_msg_system_debug_evt_t *msg);
        void (*ble_evt_system_endpoint_watermark_rx)(const struct ble_msg_system_endpoint_watermark_rx_evt_t *msg);
        void (*ble_evt_system_endpoint_watermark_tx)(const struct ble_msg_system_endpoint_watermark_tx_evt_t *msg);
        void (*ble_evt_system_script_failure)(const struct ble_msg_system_script_failure_evt_t *msg);
        void (*ble_evt_flash_ps_key)(const struct ble_msg_flash_ps_key_evt_t *msg);
        void (*ble_evt_attributes_value)(const struct ble_msg_attributes_value_evt_t *msg);
        void (*ble_evt_attributes_user_read_request)(const struct ble_msg_attributes_user_read_request_evt_t *msg);
        void (*ble_evt_attributes_status)(const struct ble_msg_attributes_status_evt_t *msg);
        void (*ble_evt_connection_status)(const struct ble_msg_connection_status_evt_t *msg);
        void (*ble_evt_connection_version_ind)(const struct ble_msg_connection_version_ind_evt_t *msg);
        void (*ble_evt_connection_feature_ind)(const struct ble_msg_connection_feature_ind_evt_t *msg);
        void (*ble_evt_connection_raw_rx)(const struct ble_msg_connection_raw_rx_evt_t *msg);
        void (*ble_evt_connection_disconnected)(const struct ble_msg_connection_disconnected_evt_t *msg);
        void (*ble_evt_attclient_indicated)(const struct ble_msg_attclient_indicated_evt_t *msg);
        void (*ble_evt_attclient_procedure_completed)(const struct ble_msg_attclient_procedure_completed_evt_t *msg);
        void (*ble_evt_attclient_group_found)(const struct ble_msg_attclient_group_found_evt_t *msg);
        void (*ble_evt_attclient_attribute_found)(const struct ble_msg_attclient_attribute_found_evt_t *msg);
        void (*ble_evt_attclient_find_information_found)(const struct ble_msg_attclient_find_information_found_evt_t *msg);
        void (*ble_evt_attclient_attribute_value)(const struct ble_msg_attclient_attribute_value_evt_t *msg);
        void (*ble_evt_attclient_read_multiple_response)(const struct ble_msg_attclient_read_multiple_response_evt_t *msg);
        void (*ble_evt_sm_smp_data)(const struct ble_msg_sm_smp_data_evt_t *msg);
        void (*ble_evt_sm_bonding_fail)(const struct ble_msg_sm_bonding_fail_evt_t *msg);
        void (*ble_evt_sm_passkey_display)(const struct ble_msg_sm_passkey_display_evt_t *msg);
        void (*ble_evt_sm_passkey_request)(const struct ble_msg_sm_passkey_request_evt_t *msg);
        void (*ble_evt_sm_bond_status)(const struct ble_msg_sm_bond_status_evt_t *msg);
        void (*ble_evt_gap_scan_response)(const struct ble_msg_gap_scan_response_evt_t *msg);
        void (*ble_evt_gap_mode_changed)(const struct ble_msg_gap_mode_changed_evt_t *msg);
        void (*ble_evt_hardware_io_port_status)(const struct ble_msg_hardware_io_port_status_evt_t *msg);
        void (*ble_evt_hardware_soft_timer)(const struct ble_msg_hardware_soft_timer_evt_t *msg);
        void (*ble_evt_hardware_adc_result)(const struct ble_msg_hardware_adc_result_evt_t *msg);

    private:
        // incoming packet buffer vars
        uint8_t *bgapiRXBuffer;
        uint8_t bgapiRXBufferSize;
        uint8_t bgapiRXBufferPos;
        uint16_t bgapiRXDataLen;

        // outgoing package buffer vars
        uint8_t *bgapiTXBuffer;
        uint8_t bgapiTXBufferSize;
        uint8_t bgapiTXBufferPos;

        // BGAPI packet structure representation
        const struct ble_msg *packetMessage;
        struct ble_header packetHeader;
        uint8_t *packetData;

        HardwareSerial *uModule; // required UART object with module connection
        HardwareSerial *uOutput; // optional UART object for host/debug connection

        bool busy;
        uint8_t lastCommand[2];
        uint8_t lastResponse[2];
        uint32_t timeoutStart;
        bool lastError;
        bool lastTimeout;

};
#endif
