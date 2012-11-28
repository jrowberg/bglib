// Bluegiga BGLib Arduino interface library demonstration sketch
// 2012-11-14 by Jeff Rowberg <jeff@rowberg.net>
// Updates should (hopefully) always be available at https://github.com/jrowberg/bglib

/* ============================================
BGLib Arduino interface library code is placed under the MIT license
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

#include <SoftwareSerial.h>
#include "BGLib.h"

// DKBLE112 development kit connections:
// - DK P0_2 -> GND (CTS tied to ground to ignore flow control)
// - DK P0_4 -> Arduino Digital Pin 2 (BLE TX -> Arduino RX)
// - DK P0_5 -> Arduino Digital Pin 3 (BLE RX -> Arduino TX)

// NOTE: this demo REQUIRES the BLE112 be programmed with the UART connected
// to the "api" endpoint in hardware.xml, and be configured for 38400 baud,
// 8,n,1. This may change in the future, but be aware. The BLE SDK archive
// contains an /examples/uartdemo project which is a good starting point for
// this communication. The BGLib repository also includes a project you can
// use for this in /BGScriptProjects/Arduino_BGLib_scanner.

SoftwareSerial bleSerialPort(2, 3); // RX, TX
BGLib ble112((HardwareSerial *)&bleSerialPort);

uint8_t isScanActive = 0;

#define LED_PIN 13 // Arduino Uno

void setup() {
    // initialize status LED
    pinMode(LED_PIN, OUTPUT);
    digitalWrite(LED_PIN, LOW);

    // open Arduino USB serial (and wait, if we're using Leonardo)
    Serial.begin(115200);
    while (!Serial);

    // welcome!
    Serial.println("BLE112 BGAPI Scanner Demo");

    // set up internal status handlers
    // (these are technically optional)
    ble112.onBusy = onBusy;
    ble112.onIdle = onIdle;
    ble112.onTimeout = onTimeout;

    // set up BGLib response handlers (called almost immediately after sending commands)
    // (these are also technicaly optional)
    ble112.ble_rsp_system_hello = my_rsp_system_hello;
    ble112.ble_rsp_gap_set_scan_parameters = my_rsp_gap_set_scan_parameters;
    ble112.ble_rsp_gap_discover = my_rsp_gap_discover;
    ble112.ble_rsp_gap_end_procedure = my_rsp_gap_end_procedure;

    // set up BGLib event handlers (called at unknown times)
    ble112.ble_evt_system_boot = my_evt_system_boot;
    ble112.ble_evt_gap_scan_response = my_evt_gap_scan_response;

    // set the data rate for the SoftwareSerial port
    bleSerialPort.begin(38400);
}

void loop() {
    Serial.println("Operations Menu:");
    Serial.println("0) Reset BLE112 module");
    Serial.println("1) Say hello to the BLE112 and wait for response");
    Serial.println("2) Toggle scanning for advertising BLE devices");
    Serial.println("Command?");
    while (1) {
        // keep polling for new data from BLE
        ble112.checkActivity();

        // check for input from the user
        if (Serial.available()) {
            uint8_t ch = Serial.read();
            uint8_t status;
            if (ch == '0') {
                // Reset BLE112 module
                Serial.println("-->\tsystem_reset: { boot_in_dfu: 0 }");
                ble112.ble_cmd_system_reset(0);
                while ((status = ble112.checkActivity(1000)));
                // system_reset doesn't have a response, but this BGLib
                // implementation allows the system_boot event specially to
                // set the "busy" flag to false for this particular case
            }
            if (ch == '1') {
                // Say hello to the BLE112 and wait for response
                Serial.println("-->\tsystem_hello");
                ble112.ble_cmd_system_hello();
                while ((status = ble112.checkActivity(1000)));
                // response should come back within milliseconds
            }
            else if (ch == '2') {
                // Toggle scanning for advertising BLE devices
                if (isScanActive) {
                    isScanActive = 0;
                    Serial.println("-->\tgap_end_procedure");
                    ble112.ble_cmd_gap_end_procedure();
                    while ((status = ble112.checkActivity(1000)));
                    // response should come back within milliseconds
                } else {
                    isScanActive = 1;
                    Serial.println("-->\tgap_set_scan_parameters: { scan_interval: 0xC8, scan_window: 0xC8, active: 1 }");
                    ble112.ble_cmd_gap_set_scan_parameters(0xC8, 0xC8, 1);
                    while ((status = ble112.checkActivity(1000)));
                    // response should come back within milliseconds
                    Serial.println("-->\tgap_discover: { mode: 2 (GENERIC) }");
                    ble112.ble_cmd_gap_discover(BGLIB_GAP_DISCOVER_GENERIC);
                    while ((status = ble112.checkActivity(1000)));
                    // response should come back within milliseconds
                    // scan response events may happen at any time after this
                }
            }
        }
    }
}

// ================================================================
// INTERNAL BGLIB CLASS CALLBACK FUNCTIONS
// ================================================================

void onBusy() {
    // turn LED on when we're busy
    digitalWrite(LED_PIN, HIGH);
}

void onIdle() {
    // turn LED off when we're no longer busy
    digitalWrite(LED_PIN, LOW);
}

void onTimeout() {
    Serial.println("!!!\tTimeout occurred!");
}

// ================================================================
// USER-DEFINED BGLIB RESPONSE CALLBACKS
// ================================================================

void my_rsp_system_hello(const ble_msg_system_hello_rsp_t *msg) {
    Serial.println("<--\tsystem_hello");
}

void my_rsp_gap_set_scan_parameters(const ble_msg_gap_set_scan_parameters_rsp_t *msg) {
    Serial.print("<--\tgap_set_scan_parameters: { ");
    Serial.print("result: "); Serial.print((uint16_t)msg -> result, HEX);
    Serial.println(" }");
}

void my_rsp_gap_discover(const ble_msg_gap_discover_rsp_t *msg) {
    Serial.print("<--\tgap_discover: { ");
    Serial.print("result: "); Serial.print((uint16_t)msg -> result, HEX);
    Serial.println(" }");
}

void my_rsp_gap_end_procedure(const ble_msg_gap_end_procedure_rsp_t *msg) {
    Serial.print("<--\tgap_end_procedure: { ");
    Serial.print("result: "); Serial.print((uint16_t)msg -> result, HEX);
    Serial.println(" }");
}

// ================================================================
// USER-DEFINED BGLIB EVENT CALLBACKS
// ================================================================

void my_evt_system_boot(const ble_msg_system_boot_evt_t *msg) {
    Serial.print("###\tsystem_boot: { ");
    Serial.print("major: "); Serial.print(msg -> major, HEX);
    Serial.print(", minor: "); Serial.print(msg -> minor, HEX);
    Serial.print(", patch: "); Serial.print(msg -> patch, HEX);
    Serial.print(", build: "); Serial.print(msg -> build, HEX);
    Serial.print(", ll_version: "); Serial.print(msg -> ll_version, HEX);
    Serial.print(", protocol_version: "); Serial.print(msg -> protocol_version, HEX);
    Serial.print(", hw: "); Serial.print(msg -> hw, HEX);
    Serial.println(" }");
}

void my_evt_gap_scan_response(const ble_msg_gap_scan_response_evt_t *msg) {
    Serial.print("###\tgap_scan_response: { ");
    Serial.print("rssi: "); Serial.print(msg -> rssi);
    Serial.print(", packet_type: "); Serial.print((uint8_t)msg -> packet_type, HEX);
    Serial.print(", sender: ");
    // this is a "bd_addr" data type, which is a 6-byte uint8_t array
    for (uint8_t i = 0; i < 6; i++) {
        if (msg -> sender.addr[i] < 16) Serial.write('0');
        Serial.print(msg -> sender.addr[i], HEX);
    }
    Serial.print(", address_type: "); Serial.print(msg -> address_type, HEX);
    Serial.print(", bond: "); Serial.print(msg -> bond, HEX);
    Serial.print(", data: ");
    // this is a "uint8array" data type, which is a length byte and a uint8_t* pointer
    for (uint8_t i = 0; i < msg -> data.len; i++) {
        if (msg -> data.data[i] < 16) Serial.write('0');
        Serial.print(msg -> data.data[i], HEX);
    }
    Serial.println(" }");
}