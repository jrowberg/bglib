# ================================================================
# Bluegiga BLE API BGLib code generator
# Jeff Rowberg <jeff.rowberg@bluegiga.com>
# ----------------------------------------------------------------
#
# CHANGELOG:
#   2012-11-?? - Initial release
#
# ================================================================

from xml.dom.minidom import parseString
from sets import Set

# open, read, and close the BLEAPI XML data
print "Reading bleapi.xml..."
file = open('bleapi.xml', 'r')
data = file.read()
file.close()

# parse XML into a DOM structure
print "Parsing BLE API definition..."
dom = parseString(data)

# read relevant dom nodes for highlighter generation
ble_datatypes = dom.getElementsByTagName('datatype')
ble_classes = dom.getElementsByTagName('class')

#for ble_datatype in ble_datatypes:
#    print ble_datatype.toxml()

command_method_declarations = []
command_method_definitions = []
response_callback_declarations = []
response_callback_initializations = []
response_callback_parser_conditions = []
event_callback_declarations = []
event_callback_initializations = []
event_callback_parser_conditions = []
constant_macros = []
struct_definitions = []
macro_enable_general_commands = []
macro_enable_general_events = []
macro_enable_detail = []

for ble_class in ble_classes:
    class_name = ble_class.attributes['name'].value
    print "Gathering command, event, and enum data from main class '" + class_name + "'..."

    if len(response_callback_parser_conditions) > 0:
        response_callback_parser_conditions.append('else if (bgapiRXBuffer[2] == ' + ble_class.attributes['index'].value + ') {')
    else:
        response_callback_parser_conditions.append('if (bgapiRXBuffer[2] == ' + ble_class.attributes['index'].value + ') {')

    # filler condition in case all in this class are disabled
    response_callback_parser_conditions.append('    if (false) { }')

    num_responses = 0
    for ble_command in ble_class.getElementsByTagName('command'):
        if num_responses == 0:
            macro_enable_detail.append('#ifdef BGLIB_ENABLE_COMMAND_CLASS_' + class_name.upper())

        #print class_name + '_' + ble_command.attributes['name'].value
        command_name = class_name + '_' + ble_command.attributes['name'].value

        # gather parameter info, if present
        ble_params = ble_command.getElementsByTagName('params');
        parameters = []
        payload_length = 0
        payload_additional = ''
        payload_parameters = []
        has_uint8array = 0
        name_uint8array = ''
        if len(ble_params) > 0:
            for ble_param in ble_params[0].getElementsByTagName('param'):
                if ble_param.attributes['type'].value == 'uint8array':
                    parameters.append('uint8 ' + ble_param.attributes['name'].value + '_len')
                    parameters.append('const uint8 *' + ble_param.attributes['name'].value + '_data')
                    payload_parameters.append(ble_param.attributes['name'].value + '_len = ' + ble_param.attributes['name'].value + '_len')
                    payload_parameters.append(ble_param.attributes['name'].value + '_data = ' + ble_param.attributes['name'].value + '_data')
                else:
                    parameters.append(ble_param.attributes['type'].value + ' ' + ble_param.attributes['name'].value)
                    payload_parameters.append(ble_param.attributes['name'].value + ' = ' + ble_param.attributes['name'].value)

                if ble_param.attributes['type'].value == 'uint8':
                    payload_length += 1
                elif ble_param.attributes['type'].value == 'int8':
                    payload_length += 1
                elif ble_param.attributes['type'].value == 'uint16':
                    payload_length += 2
                elif ble_param.attributes['type'].value == 'int16':
                    payload_length += 2
                elif ble_param.attributes['type'].value == 'uint32':
                    payload_length += 4
                elif ble_param.attributes['type'].value == 'bd_addr':
                    payload_length += 6
                elif ble_param.attributes['type'].value == 'uint8array':
                    payload_length += 1
                    payload_additional += ' + ' + ble_param.attributes['name'].value + '_len'
                    has_uint8array = 1
                    name_uint8array = ble_param.attributes['name'].value

        # gather return value info, if present
        ble_returns = ble_command.getElementsByTagName('returns');
        returns = []
        if len(ble_returns) > 0:
            for ble_return in ble_returns[0].getElementsByTagName('param'):
                returns.append(ble_return.attributes['type'].value + ' ' + ble_return.attributes['name'].value)

        command_method_declarations.append('#ifdef BGLIB_ENABLE_COMMAND_' + command_name.upper())
        command_method_declarations.append('    uint8_t ble_cmd_' + command_name + '(' + ', '.join(parameters) + ');')
        command_method_declarations.append('#endif')
        command_method_definitions.append('#ifdef BGLIB_ENABLE_COMMAND_' + command_name.upper())
        if len(parameters) > 0:
            if has_uint8array == 4:
                command_method_definitions.append('uint8_t BGLib::ble_cmd_' + command_name + '(' + ', '.join(parameters) + ') {\n\
    ble_msg_' + command_name + '_cmd_t payload;\n\
    payload.' + ';\n    payload.'.join(payload_parameters) + ';\n\
    return sendCommand(' + str(payload_length) + payload_additional + ', ' + ble_class.attributes['index'].value + ', ' + ble_command.attributes['index'].value + ', &payload);\n\
}')
            else:
                # recreate payload parameters to use memcpy instead of direct assignment
                payload_parameters = []
                payload_parameters.append('uint8_t d[' + str(payload_length) + payload_additional + ']')
                payload_length = 0
                for ble_param in ble_params[0].getElementsByTagName('param'):
                    if ble_param.attributes['type'].value == 'uint8array':
                        payload_parameters.append('memcpy(d + ' + str(payload_length) + ', &' + ble_param.attributes['name'].value + '_len, sizeof(uint8))')
                        payload_parameters.append('memcpy(d + ' + str(payload_length + 1) + ', ' + ble_param.attributes['name'].value + '_data, ' + ble_param.attributes['name'].value + '_len)')
                    else:
                        payload_parameters.append('memcpy(d + ' + str(payload_length) + ', &' + ble_param.attributes['name'].value + ', sizeof(' + ble_param.attributes['type'].value + '))')
                    if ble_param.attributes['type'].value == 'uint8':
                        payload_length += 1
                    elif ble_param.attributes['type'].value == 'int8':
                        payload_length += 1
                    elif ble_param.attributes['type'].value == 'uint16':
                        payload_length += 2
                    elif ble_param.attributes['type'].value == 'int16':
                        payload_length += 2
                    elif ble_param.attributes['type'].value == 'uint32':
                        payload_length += 4
                    elif ble_param.attributes['type'].value == 'bd_addr':
                        payload_length += 6
                    elif ble_param.attributes['type'].value == 'uint8array':
                        payload_length += 1
                        payload_additional += ' + ' + ble_param.attributes['name'].value + '_len'
                command_method_definitions.append('uint8_t BGLib::ble_cmd_' + command_name + '(' + ', '.join(parameters) + ') {\n\
    ' + ';\n    '.join(payload_parameters) + ';\n\
    return sendCommand(' + str(payload_length) + payload_additional + ', ' + ble_class.attributes['index'].value + ', ' + ble_command.attributes['index'].value + ', d);\n\
}')

        else:
            command_method_definitions.append('uint8_t BGLib::ble_cmd_' + command_name + '(' + ', '.join(parameters) + ') {\n\
    return sendCommand(0, ' + ble_class.attributes['index'].value + ', ' + ble_command.attributes['index'].value + ');\n\
}')
        command_method_definitions.append('#endif\n')

        response_callback_initializations.append('#ifdef BGLIB_ENABLE_COMMAND_' + command_name.upper())
        response_callback_initializations.append('    ble_rsp_' + command_name + ' = 0;')
        response_callback_initializations.append('#endif')
        response_callback_declarations.append('#ifdef BGLIB_ENABLE_COMMAND_' + command_name.upper())
        response_callback_declarations.append('    void (*ble_rsp_' + command_name + ')(const struct ble_msg_' + command_name + '_rsp_t *msg);')
        response_callback_declarations.append('#endif')
        response_callback_parser_conditions.append('    #ifdef BGLIB_ENABLE_COMMAND_' + command_name.upper())
        response_callback_parser_conditions.append('        else if (bgapiRXBuffer[3] == ' + ble_command.attributes['index'].value + ') { if (ble_rsp_' + command_name +') ble_rsp_' + command_name + '((const struct ble_msg_' + command_name + '_rsp_t *)(bgapiRXBuffer + 4)); }')
        response_callback_parser_conditions.append('    #endif')
        if len(parameters) > 0:
            struct_definitions.append('#ifdef BGLIB_ENABLE_COMMAND_' + command_name.upper())
            struct_definitions.append('struct ble_msg_' + command_name + '_cmd_t {\n    ' + ';\n    '.join(parameters) + ';\n} PACKED;')
            if len(returns) < 1: struct_definitions.append('#endif')
        if len(returns) > 0:
            if len(parameters) < 1: struct_definitions.append('#ifdef BGLIB_ENABLE_COMMAND_' + command_name.upper())
            struct_definitions.append('struct ble_msg_' + command_name + '_rsp_t {\n    ' + ';\n    '.join(returns) + ';\n} PACKED;')
            struct_definitions.append('#endif')
        num_responses += 1
        macro_enable_detail.append('    #define BGLIB_ENABLE_COMMAND_' + command_name.upper())

    if num_responses > 0:
        macro_enable_detail.append('#endif')
        macro_enable_detail.append('')

    response_callback_parser_conditions.append('}')

    if len(event_callback_parser_conditions) > 0:
        event_callback_parser_conditions.append('else if (bgapiRXBuffer[2] == ' + ble_class.attributes['index'].value + ') {')
    else:
        event_callback_parser_conditions.append('if (bgapiRXBuffer[2] == ' + ble_class.attributes['index'].value + ') {')

    # filler condition in case all in this class are disabled
    event_callback_parser_conditions.append('    if (false) { }')

    num_events = 0
    for ble_event in ble_class.getElementsByTagName('event'):
        if num_events == 0:
            macro_enable_detail.append('#ifdef BGLIB_ENABLE_EVENT_CLASS_' + class_name.upper())

        #print class_name + '_' + ble_event.attributes['name'].value
        event_name = class_name + '_' + ble_event.attributes['name'].value

        # gather parameter info, if present
        ble_params = ble_event.getElementsByTagName('params');
        parameters = []
        if len(ble_params) > 0:
            for ble_param in ble_params[0].getElementsByTagName('param'):
                if ble_param.attributes['type'].value == 'uint8array':
                    parameters.append('uint8array ' + ble_param.attributes['name'].value)
                    #parameters.append('uint8 ' + ble_param.attributes['name'].value + '_len')
                    #parameters.append('const uint8 *' + ble_param.attributes['name'].value + '_data')
                else:
                    parameters.append(ble_param.attributes['type'].value + ' ' + ble_param.attributes['name'].value)

        event_callback_initializations.append('#ifdef BGLIB_ENABLE_EVENT_' + event_name.upper())
        event_callback_initializations.append('    ble_evt_' + event_name + ' = 0;')
        event_callback_initializations.append('#endif')
        event_callback_declarations.append('#ifdef BGLIB_ENABLE_EVENT_' + event_name.upper())
        event_callback_declarations.append('    void (*ble_evt_' + event_name + ')(const struct ble_msg_' + event_name + '_evt_t *msg);')
        event_callback_declarations.append('#endif')
        event_callback_parser_conditions.append('    #ifdef BGLIB_ENABLE_EVENT_' + event_name.upper())
        if ble_class.attributes['index'].value == '0' and ble_event.attributes['index'].value == '0':
            event_callback_parser_conditions.append('        else if (bgapiRXBuffer[3] == ' + ble_event.attributes['index'].value + ') { if (ble_evt_' + event_name + ') { ble_evt_' + event_name + '((const struct ble_msg_' + event_name + '_evt_t *)(bgapiRXBuffer + 4)); } setBusy(false); }')
        else:
            event_callback_parser_conditions.append('        else if (bgapiRXBuffer[3] == ' + ble_event.attributes['index'].value + ') { if (ble_evt_' + event_name + ') ble_evt_' + event_name + '((const struct ble_msg_' + event_name + '_evt_t *)(bgapiRXBuffer + 4)); }')
        event_callback_parser_conditions.append('    #endif')
        if len(parameters) > 0:
            struct_definitions.append('#ifdef BGLIB_ENABLE_EVENT_' + event_name.upper())
            struct_definitions.append('struct ble_msg_' + event_name + '_evt_t {\n    ' + ';\n    '.join(parameters) + ';\n} PACKED;')
            struct_definitions.append('#endif')
        num_events += 1
        macro_enable_detail.append('    #define BGLIB_ENABLE_EVENT_' + event_name.upper())

    if num_events > 0:
        macro_enable_detail.append('#endif')
        macro_enable_detail.append('')

    event_callback_parser_conditions.append('}')

    for ble_enum in ble_class.getElementsByTagName('enum'):
        #print class_name + '_' + ble_enum.attributes['name'].value
        enum_name = class_name + '_' + ble_enum.attributes['name'].value
        constant_macros.append('#define BGLIB_' + (enum_name.upper() + ' ').ljust(54) + ble_enum.attributes['value'].value)

    if constant_macros[len(constant_macros) - 1] != '':
        constant_macros.append('')

    macro_enable_general_commands.append('#define BGLIB_ENABLE_COMMAND_CLASS_' + class_name.upper())
    macro_enable_general_events.append('#define BGLIB_ENABLE_EVENT_CLASS_' + class_name.upper())

#command_method_declarations.append('uint8_t ble_cmd_system_reset(uint8 boot_in_dfu);')
#response_callback_declarations.append('void (*ble_rsp_system_reset)(const void* nul);')
#event_callback_declarations.append('void (*ble_evt_system_boot)(const struct ble_msg_system_boot_evt_t *msg);')

# create Arduino library files
print "Writing Arduino header and source library files in 'BGLib\\Arduino'..."
header = open('BGLib\\Arduino\\BGLib.h', 'w')
header.write('// Arduino BGLib code library header file\n\
\n\
#ifndef __BGLIB_H__\n\
#define __BGLIB_H__\n\
\n\
#include <Arduino.h>\n\
#include "BGLibConfig.h"\n\
\n\
// uncomment this line for Serial.println() debug output\n\
//#define DEBUG\n\
\n\
' + ('\n'.join(constant_macros)) + '\
\n\
#define PACKED __attribute__((packed))\n\
#define ALIGNED __attribute__((aligned(0x4)))\n\
\n\
typedef uint8_t    uint8;\n\
typedef uint16_t   uint16;\n\
typedef int16_t    int16;\n\
typedef uint32_t   uint32;\n\
typedef int8_t     int8;\n\
\n\
typedef struct bd_addr_t {\n\
    uint8 addr[6];\n\
} bd_addr;\n\
\n\
typedef bd_addr hwaddr;\n\
typedef struct {\n\
    uint8 len;\n\
    uint8 data[];\n\
} uint8array;\n\
\n\
typedef struct {\n\
    uint8 len;\n\
    int8 data[];\n\
} string;\n\
\n\
struct ble_header {\n\
    uint8  type_hilen;\n\
    uint8  lolen;\n\
    uint8  cls;\n\
    uint8  command;\n\
};\n\
\n\
' + ('\n'.join(struct_definitions)) + '\n\
\n\
class BGLib {\n\
    public:\n\
        BGLib(HardwareSerial *module=0, HardwareSerial *output=0, uint8_t pMode=0);\n\
        uint8_t checkActivity(uint16_t timeout=0);\n\
        uint8_t checkError();\n\
        uint8_t checkTimeout();\n\
        void setBusy(bool busyEnabled);\n\
\n\
        uint8_t *getLastCommand();\n\
        uint8_t *getLastResponse();\n\
        uint8_t *getLastEvent();\n\
        void *getLastRXPayload();\n\
\n\
        // set/update UART port objects\n\
        void setModuleUART(HardwareSerial *module);\n\
        void setOutputUART(HardwareSerial *debug);\n\
\n\
        uint8_t parse(uint8_t ch, uint8_t packetMode=0);\n\
        uint8_t sendCommand(uint16_t len, uint8_t commandClass, uint8_t commandId, void *payload=0);\n\
\n\
        void (*onBusy)();               // special function to run when entering a "busy" state (e.g. mid-packet)\n\
        void (*onIdle)();               // special function to run when returning to idle mode\n\
        void (*onTimeout)();            // special function to run when the parser times out waiting for expected data\n\
        void (*onBeforeTXCommand)();    // special function to run immediately before sending a command\n\
        void (*onTXCommandComplete)();  // special function to run immediately after command transmission is complete\n\
\n\
        ' + ('\n        '.join(command_method_declarations)) + '\n\n\
        ' + ('\n        '.join(response_callback_declarations)) + '\n\n\
        ' + ('\n        '.join(event_callback_declarations)) + '\n\
\n\
    private:\n\
        // incoming packet buffer vars\n\
        uint8_t *bgapiRXBuffer;\n\
        uint8_t bgapiRXBufferSize;\n\
        uint8_t bgapiRXBufferPos;\n\
        uint16_t bgapiRXDataLen;\n\
\n\
        // outgoing package buffer vars\n\
        uint8_t *bgapiTXBuffer;\n\
        uint8_t bgapiTXBufferSize;\n\
        uint8_t bgapiTXBufferPos;\n\
\n\
        // BGAPI packet structure representation\n\
        const struct ble_msg *packetMessage;\n\
        struct ble_header packetHeader;\n\
        uint8_t *packetData;\n\
\n\
        HardwareSerial *uModule; // required UART object with module connection\n\
        HardwareSerial *uOutput; // optional UART object for host/debug connection\n\
\n\
        bool busy;\n\
        uint8_t packetMode;\n\
        uint8_t lastCommand[2];\n\
        uint8_t lastResponse[2];\n\
        uint8_t lastEvent[2];\n\
        uint32_t timeoutStart;\n\
        bool lastError;\n\
        bool lastTimeout;\n\
\n\
};\n')

header.write('#endif\n')
header.close()

source = open('BGLib\\Arduino\\BGLib.cpp', 'w')
source.write('// Arduino BGLib code library source file\n\
\n\
#include "BGLib.h"\n\
\n\
BGLib::BGLib(HardwareSerial *module, HardwareSerial *output, uint8_t pMode) {\n\
    uModule = module;\n\
    uOutput = output;\n\
    packetMode = pMode;\n\
\n\
    // initialize packet buffers\n\
    bgapiRXBuffer = (uint8_t *)malloc(bgapiRXBufferSize = 32);\n\
    bgapiTXBuffer = (uint8_t *)malloc(bgapiTXBufferSize = 32);\n\
    bgapiRXBufferPos = bgapiTXBufferPos = 0;\n\
\n\
    onBusy = 0;\n\
    onIdle = 0;\n\
    onTimeout = 0;\n\
    onBeforeTXCommand = 0;\n\
    onTXCommandComplete = 0;\n\
\n\
    ' + ('\n    '.join(response_callback_initializations)) + '\n\
\n\
    ' + ('\n    '.join(event_callback_initializations)) + '\n\
}\n\
\n\
uint8_t BGLib::checkActivity(uint16_t timeout) {\n\
    uint16_t ch;\n\
    while ((ch = uModule -> read()) < 256 && (timeout == 0 || millis() - timeoutStart < timeout)) {\n\
        parse(ch);\n\
        if (timeout > 0) timeoutStart = millis();\n\
    }\n\
    if (timeout > 0 && busy && millis() - timeoutStart >= timeout) {\n\
        lastTimeout = true;\n\
        if (onTimeout != 0) onTimeout();\n\
        setBusy(false);\n\
    }\n\
    return busy;\n\
}\n\
\n\
uint8_t BGLib::checkError() {\n\
    return lastError;\n\
}\n\
uint8_t BGLib::checkTimeout() {\n\
    return lastTimeout;\n\
}\n\
\n\
uint8_t *BGLib::getLastCommand() {\n\
    return lastCommand;\n\
}\n\
uint8_t *BGLib::getLastResponse() {\n\
    return lastResponse;\n\
}\n\
uint8_t *BGLib::getLastEvent() {\n\
    return lastEvent;\n\
}\n\
void *BGLib::getLastRXPayload() {\n\
    return bgapiRXBuffer + 4;\n\
}\n\
\n\
void BGLib::setBusy(bool busyEnabled) {\n\
    busy = busyEnabled;\n\
    if (busy) {\n\
        timeoutStart = millis();\n\
        lastTimeout = false;\n\
        if (onBusy) onBusy();\n\
    } else {\n\
        lastError = false;\n\
        if (onIdle) onIdle();\n\
    }\n\
}\n\
\n\
// set/update UART port objects\n\
void BGLib::setModuleUART(HardwareSerial *module) {\n\
    uModule = module;\n\
}\n\
\n\
void BGLib::setOutputUART(HardwareSerial *output) {\n\
    uOutput = output;\n\
}\n\
\n\
uint8_t BGLib::parse(uint8_t ch, uint8_t packetMode) {\n\
    #ifdef DEBUG\n\
        // DEBUG: output hex value of incoming character\n\
        if (ch < 16) Serial.write(0x30);    // leading \'0\'\n\
        Serial.print(ch, HEX);              // actual hex value\n\
        Serial.write(0x20);                 // trailing \' \'\n\
    #endif\n\
\n\
    if (bgapiRXBufferPos == bgapiRXBufferSize) {\n\
        // expand RX buffer to prevent overflow\n\
        bgapiRXBuffer = (uint8_t *)realloc(bgapiRXBuffer, bgapiRXBufferSize += 16);\n\
    }\n\
\n\
    /*\n\
    BGAPI packet structure (as of 2012-11-07):\n\
        Byte 0:\n\
              [7] - 1 bit, Message Type (MT)         0 = Command/Response, 1 = Event\n\
            [6:3] - 4 bits, Technology Type (TT)     0000 = Bluetooth 4.0 single mode, 0001 = Wi-Fi\n\
            [2:0] - 3 bits, Length High (LH)         Payload length (high bits)\n\
        Byte 1:     8 bits, Length Low (LL)          Payload length (low bits)\n\
        Byte 2:     8 bits, Class ID (CID)           Command class ID\n\
        Byte 3:     8 bits, Command ID (CMD)         Command ID\n\
        Bytes 4-n:  0 - 2048 Bytes, Payload (PL)     Up to 2048 bytes of payload\n\
    */\n\
\n\
    // check packet position\n\
    if (bgapiRXBufferPos == 0) {\n\
        // beginning of packet, check for correct framing/expected byte(s)\n\
        // BGAPI packet for Bluetooth Smart Single Mode must be either Command/Response (0x00) or Event (0x80)\n\
        if ((ch & 0x78) == 0x00) {\n\
            // store new character in RX buffer\n\
            bgapiRXBuffer[bgapiRXBufferPos++] = ch;\n\
        } else {\n\
            #ifdef DEBUG\n\
                Serial.print("*** Packet frame sync error! Expected .0000... binary, got 0x");\n\
                Serial.println(ch, HEX);\n\
            #endif\n\
            return 1; // packet format error\n\
        }\n\
    } else {\n\
        // middle of packet, assume we\'re okay\n\
        bgapiRXBuffer[bgapiRXBufferPos++] = ch;\n\
        if (bgapiRXBufferPos == 2) {\n\
            // just received "Length Low" byte, so store expected packet length\n\
            bgapiRXDataLen = ch + ((bgapiRXBuffer[0] & 0x03) << 8);\n\
        } else if (bgapiRXBufferPos == bgapiRXDataLen + 4) {\n\
            // just received last expected byte\n\
            #ifdef DEBUG\n\
                Serial.print("\\n<=[ ");\n\
                for (uint8_t i = 0; i < bgapiRXBufferPos; i++) {\n\
                    if (bgapiRXBuffer[i] < 16) Serial.write(0x30);\n\
                    Serial.print(bgapiRXBuffer[i], HEX);\n\
                    Serial.write(0x20);\n\
                }\n\
                Serial.println("]");\n\
            #endif\n\
\n\
            // reset RX packet buffer position to be ready for new packet\n\
            bgapiRXBufferPos = 0;\n\
\n\
            // check packet type\n\
            if ((bgapiRXBuffer[0] & 0x80) == 0) {\n\
                // 0x00 = Response packet\n\
\n\
                // capture last response class/command bytes\n\
                lastResponse[0] = bgapiRXBuffer[2];\n\
                lastResponse[1] = bgapiRXBuffer[3];\n\
\n\
                ' + ('\n                '.join(response_callback_parser_conditions)) + '\n\
\n\
                setBusy(false);\n\
            } else {\n\
                // 0x80 = Event packet\n\
\n\
                // capture last event class/command bytes\n\
                lastEvent[0] = bgapiRXBuffer[2];\n\
                lastEvent[1] = bgapiRXBuffer[3];\n\
\n\
                ' + ('\n                '.join(event_callback_parser_conditions)) + '\n\
            }\n\
        }\n\
    }\n\
\n\
    return 0; // parsed successfully\n\
}\n\
\n\
uint8_t BGLib::sendCommand(uint16_t len, uint8_t commandClass, uint8_t commandId, void *payload) {\n\
    bgapiTXBuffer = (uint8_t *)malloc(len + 4);\n\
    bgapiTXBuffer[0] = 0x00;\n\
    bgapiTXBuffer[1] = (len & 0xFF);\n\
    bgapiTXBuffer[2] = commandClass;\n\
    bgapiTXBuffer[3] = commandId;\n\
    lastCommand[0] = commandClass;\n\
    lastCommand[1] = commandId;\n\
    if (len > 0) memcpy(bgapiTXBuffer + 4, payload, len);\n\
    #ifdef DEBUG\n\
        Serial.print("\\n=>[ ");\n\
        if (packetMode) {\n\
            if (len + 4 < 16) Serial.write(0x30);\n\
            Serial.print(len + 4, HEX);\n\
            Serial.write(0x20);\n\
        }\n\
        for (uint8_t i = 0; i < len + 4; i++) {\n\
            if (bgapiTXBuffer[i] < 16) Serial.write(0x30);\n\
            Serial.print(bgapiTXBuffer[i], HEX);\n\
            Serial.write(0x20);\n\
        }\n\
        Serial.println("]");\n\
    #endif\n\
    if (onBeforeTXCommand) onBeforeTXCommand();\n\
    setBusy(true);\n\
    if (packetMode) uModule -> write(len + 4); // outgoing packet length byte first\n\
    uModule -> write(bgapiTXBuffer, len + 4);\n\
\n\
    if (onTXCommandComplete) onTXCommandComplete();\n\
    free(bgapiTXBuffer);\n\
    return 0;\n\
}\n\
\n\
' + ('\n'.join(command_method_definitions)) + '\
')
source.close()

config = open('BGLib\\Arduino\\BGLibConfig.h', 'w')
config.write('// Arduino BGLib code library config file\n\
\n\
#ifndef __BGLIB_CONFIG_H__\n\
#define __BGLIB_CONFIG_H__\n\
\n\
' + ('\n'.join(macro_enable_general_commands)) + '\n\
\n\
' + ('\n'.join(macro_enable_general_events)) + '\n\
\n\
' + ('\n'.join(macro_enable_detail)) + '\
\n\
#endif /* __BGLIB_CONFIG_H__ */\n\
\n\
')
config.close()

print "Finished!\n"

print "Arduino Installation Instructions:"
print "===================================="
print "1. Copy 'BGLib\\Arduino\\*' files to '<Arduino-Library-Dir>\\BGLib'"
print "2. Restart Arduino IDE, if it's running\n"
print "- 'libraries' folder is typically 'C:\\Users\\Name\\Documents\\Arduino\\libraries'\n"
