# ================================================================
# Bluegiga BLE API BGLib code generator: MSVCSharp platform
# Jeff Rowberg <jeff.rowberg@bluegiga.com>
# ----------------------------------------------------------------
#
# CHANGELOG:
#   2013-05-?? - Initial release
#
# ================================================================

from xml.dom.minidom import parseString
import string

# open, read, and close the BLEAPI XML data
print("Reading bleapi.xml...")
file = open('bleapi.xml', 'r')
data = file.read()
file.close()

# parse XML into a DOM structure
print("Parsing BLE API definition...")
dom = parseString(data)

# read relevant dom nodes for highlighter generation
ble_datatypes = dom.getElementsByTagName('datatype')
ble_classes = dom.getElementsByTagName('class')

#for ble_datatype in ble_datatypes:
#    print ble_datatype.toxml()

command_method_definitions = []
response_callback_declarations = []
response_callback_structure = []
response_callback_initializations = []
response_callback_parser_conditions = []
event_callback_declarations = []
event_callback_structure = []
event_callback_initializations = []
event_callback_parser_conditions = []
constant_macros = []
struct_definitions = []

for ble_class in ble_classes:
    class_name = ble_class.attributes['name'].value
    print("Gathering command, event, and enum data from main class '" + class_name + "'...")

    if len(response_callback_parser_conditions) > 0:
        response_callback_parser_conditions.append('else if (bgapiRXBuffer[2] == ' + ble_class.attributes['index'].value + ') {')
    else:
        response_callback_parser_conditions.append('if (bgapiRXBuffer[2] == ' + ble_class.attributes['index'].value + ') {')

    num_responses = 0
    command_prefix = ''
    for ble_command in ble_class.getElementsByTagName('command'):
        #print class_name + '_' + ble_command.attributes['name'].value
        command_name_parts = (string.capwords(class_name + ' ' + ble_command.attributes['name'].value.replace('_', ' '))).split(' ')
        command_name = ''
        word_pos = 0
        for word in command_name_parts:
            if word in ['Gap', 'Sm', 'Smp', 'Adc', 'Rx', 'Tx', 'Ps', 'Phy', 'Io', 'Spi', 'I2c', 'Dfu']:
                command_name += word.upper()
            elif word == 'Attclient':
                command_name += 'ATTClient'
            else:
                command_name += word
            if word_pos == 0:
                if num_responses == 0:
                    response_callback_structure.append('namespace ' + command_name + ' {')
                #command_name += '.'
                command_prefix = command_name
                command_name = ''
            word_pos += 1

        # gather parameter info, if present
        ble_params = ble_command.getElementsByTagName('params');
        parameters = []
        payload_length = 0
        payload_additional = ''
        payload_parameters = []
        payload_extra_lines = []
        if len(ble_params) > 0:
            for ble_param in ble_params[0].getElementsByTagName('param'):
                if ble_param.attributes['type'].value == 'uint8':
                    parameters.append('Byte ' + ble_param.attributes['name'].value)
                    payload_parameters.append(ble_param.attributes['name'].value);
                    payload_length += 1
                elif ble_param.attributes['type'].value == 'int8':
                    parameters.append('SByte ' + ble_param.attributes['name'].value)
                    payload_parameters.append('(Byte)' + ble_param.attributes['name'].value);
                    payload_length += 1
                elif ble_param.attributes['type'].value == 'uint16':
                    parameters.append('UInt16 ' + ble_param.attributes['name'].value)
                    payload_parameters.append('(Byte)(' + ble_param.attributes['name'].value + '), (Byte)(' + ble_param.attributes['name'].value + ' >> 8)');
                    payload_length += 2
                elif ble_param.attributes['type'].value == 'int16':
                    parameters.append('Int16 ' + ble_param.attributes['name'].value)
                    payload_parameters.append('(Byte)(' + ble_param.attributes['name'].value + '), (Byte)(' + ble_param.attributes['name'].value + ' >> 8)');
                    payload_length += 2
                elif ble_param.attributes['type'].value == 'uint32':
                    parameters.append('UInt32 ' + ble_param.attributes['name'].value)
                    payload_parameters.append('(Byte)(' + ble_param.attributes['name'].value + '), (Byte)(' + ble_param.attributes['name'].value + ' >> 8), (Byte)(' + ble_param.attributes['name'].value + ' >> 16), (Byte)(' + ble_param.attributes['name'].value + ' >> 24)');
                    payload_length += 4
                elif ble_param.attributes['type'].value == 'int32':
                    parameters.append('Int32 ' + ble_param.attributes['name'].value)
                    payload_parameters.append('(Byte)(' + ble_param.attributes['name'].value + '), (Byte)(' + ble_param.attributes['name'].value + ' >> 8), (Byte)(' + ble_param.attributes['name'].value + ' >> 16), (Byte)(' + ble_param.attributes['name'].value + ' >> 24)');
                    payload_length += 4
                elif ble_param.attributes['type'].value == 'bd_addr':
                    parameters.append('Byte[] ' + ble_param.attributes['name'].value)
                    payload_parameters.append('0, 0, 0, 0, 0, 0')
                    payload_extra_lines.append('Array.Copy(' + ble_param.attributes['name'].value + ', 0, cmd, ' + str(payload_length + 4) + ', 6);');
                    payload_length += 6
                elif ble_param.attributes['type'].value == 'uint8array':
                    parameters.append('Byte[] ' + ble_param.attributes['name'].value)
                    payload_parameters.append('(Byte)' + ble_param.attributes['name'].value + '.Length')
                    payload_length += 1
                    payload_additional += ' + ' + ble_param.attributes['name'].value + '.Length'
                    payload_extra_lines.append('Array.Copy(' + ble_param.attributes['name'].value + ', 0, cmd, ' + str(payload_length + 4) + ', ' + ble_param.attributes['name'].value + '.Length);');

        # gather return value info, if present
        ble_returns = ble_command.getElementsByTagName('returns');
        returns = []
        if len(ble_returns) > 0:
            for ble_return in ble_returns[0].getElementsByTagName('param'):
                returns.append(ble_return.attributes['type'].value + ' ' + ble_return.attributes['name'].value)

        payload_str = ''
        if len(payload_parameters) > 0:
            payload_str = ', ' + ', '.join(payload_parameters)

        command_method_definitions.append('public Byte[] BLECommand' + command_prefix + command_name + '(' + ', '.join(parameters) + ') {')
        if len(payload_extra_lines) == 0:
            command_method_definitions.append('    return new Byte[] { 0, ' + str(payload_length) + ', ' + ble_class.attributes['index'].value + ', ' + ble_command.attributes['index'].value + payload_str + ' };')
        else:
            command_method_definitions.append('    Byte[] cmd = new Byte[' + str(payload_length + 4) + payload_additional + '];')
            command_method_definitions.append('    Array.Copy(new Byte[] { 0, (Byte)(' + str(payload_length) + payload_additional + '), ' + ble_class.attributes['index'].value + ', ' + ble_command.attributes['index'].value + payload_str + ' }, 0, cmd, 0, ' + str(payload_length + 4) + ');')
            for l in payload_extra_lines:
                command_method_definitions.append('    ' + l)
            command_method_definitions.append('    return cmd;')
        command_method_definitions.append('}')

        parameters = []
        param_init = []
        response_args = []
        buf_pos = 4
        if len(ble_returns) > 0:
            for ble_return in ble_returns[0].getElementsByTagName('param'):
                if (ble_return.attributes['type'].value == 'uint8'):
                    parameters.append('Byte ' + ble_return.attributes['name'].value)
                    param_init.append('this.' + ble_return.attributes['name'].value + ' = ' + ble_return.attributes['name'].value + ';')
                    response_args.append('bgapiRXBuffer[' + str(buf_pos) + ']')
                    buf_pos += 1
                elif (ble_return.attributes['type'].value == 'uint16'):
                    parameters.append('UInt16 ' + ble_return.attributes['name'].value)
                    param_init.append('this.' + ble_return.attributes['name'].value + ' = ' + ble_return.attributes['name'].value + ';')
                    response_args.append('(UInt16)(bgapiRXBuffer[' + str(buf_pos) + '] + (bgapiRXBuffer[' + str(buf_pos + 1) + '] << 8))')
                    buf_pos += 2
                elif (ble_return.attributes['type'].value == 'uint32'):
                    parameters.append('UInt32 ' + ble_return.attributes['name'].value)
                    param_init.append('this.' + ble_return.attributes['name'].value + ' = ' + ble_return.attributes['name'].value + ';')
                    response_args.append('(UInt32)(bgapiRXBuffer[' + str(buf_pos) + '] + (bgapiRXBuffer[' + str(buf_pos + 1) + '] << 8) + (bgapiRXBuffer[' + str(buf_pos + 2) + '] << 16) + (bgapiRXBuffer[' + str(buf_pos + 3) + '] << 24))')
                    buf_pos += 4
                elif (ble_return.attributes['type'].value == 'int8'):
                    parameters.append('SByte ' + ble_return.attributes['name'].value)
                    param_init.append('this.' + ble_return.attributes['name'].value + ' = ' + ble_return.attributes['name'].value + ';')
                    response_args.append('(SByte)(bgapiRXBuffer[' + str(buf_pos) + '])')
                    buf_pos += 1
                elif (ble_return.attributes['type'].value == 'int16'):
                    parameters.append('Int16 ' + ble_return.attributes['name'].value)
                    param_init.append('this.' + ble_return.attributes['name'].value + ' = ' + ble_return.attributes['name'].value + ';')
                    response_args.append('(Int16)(bgapiRXBuffer[' + str(buf_pos) + '] + (bgapiRXBuffer[' + str(buf_pos + 1) + '] << 8))')
                    buf_pos += 2
                elif (ble_return.attributes['type'].value == 'int32'):
                    parameters.append('Int32 ' + ble_return.attributes['name'].value)
                    param_init.append('this.' + ble_return.attributes['name'].value + ' = ' + ble_return.attributes['name'].value + ';')
                    response_args.append('(Int32)(bgapiRXBuffer[' + str(buf_pos) + '] + (bgapiRXBuffer[' + str(buf_pos + 1) + '] << 8) + (bgapiRXBuffer[' + str(buf_pos + 2) + '] << 16) + (bgapiRXBuffer[' + str(buf_pos + 3) + '] << 24))')
                    buf_pos += 4
                elif (ble_return.attributes['type'].value == 'bd_addr'):
                    parameters.append('Byte[] ' + ble_return.attributes['name'].value)
                    param_init.append('this.' + ble_return.attributes['name'].value + ' = ' + ble_return.attributes['name'].value + ';')
                    response_args.append('(Byte[])(bgapiRXBuffer.Skip(' + str(buf_pos) + ').Take(6).ToArray())')
                    buf_pos += 6
                elif (ble_return.attributes['type'].value == 'uint8array'):
                    parameters.append('Byte[] ' + ble_return.attributes['name'].value)
                    param_init.append('this.' + ble_return.attributes['name'].value + ' = ' + ble_return.attributes['name'].value + ';')
                    response_args.append('(Byte[])(bgapiRXBuffer.Skip(' + str(buf_pos + 1) + ').Take(bgapiRXBuffer[' + str(buf_pos) + ']).ToArray())')
                    # buf_pos doesn't matter here since uint8arrays are ALWAYS at the end

        cs_code = []
        cs_code.append('if (BLEResponse' + command_prefix + command_name + ' != null) {')
        cs_code.append('    BLEResponse' + command_prefix + command_name + '(this, new Bluegiga.BLE.Responses.' + command_prefix + '.' + command_name + 'EventArgs(')
        if len(response_args) > 0:
            cs_code.append('        ' + ',\n                                        '.join(response_args))
        cs_code.append('    ));')
        cs_code.append('}')

        response_callback_declarations.append('public event Bluegiga.BLE.Responses.' + command_prefix + '.' + command_name + 'EventHandler BLEResponse' + command_prefix + command_name + ';')
        response_callback_structure.append('    public delegate void ' + command_name + 'EventHandler(object sender, Bluegiga.BLE.Responses.' + command_prefix + '.' + command_name + 'EventArgs e);')
        response_callback_structure.append('    public class ' + command_name + 'EventArgs : EventArgs {')
        for parameter in parameters:
            response_callback_structure.append('        public readonly ' + parameter + ';')
        if len(param_init) > 0:
            response_callback_structure.append('        public ' + command_name + 'EventArgs(' + ', '.join(parameters) + ') {')
            response_callback_structure.append('            ' + '\n                        '.join(param_init))
            response_callback_structure.append('        }')
        else:
            response_callback_structure.append('        public ' + command_name + 'EventArgs(' + ', '.join(parameters) + ') { }')
        response_callback_structure.append('    }')
        response_callback_structure.append('')

        if num_responses > 0:
            response_callback_parser_conditions.append('    else if (bgapiRXBuffer[3] == ' + ble_command.attributes['index'].value + ')')
            response_callback_parser_conditions.append('    {')
        else:
            response_callback_parser_conditions.append('    if (bgapiRXBuffer[3] == ' + ble_command.attributes['index'].value + ')')
            response_callback_parser_conditions.append('    {')

        response_callback_parser_conditions.append('        ' + '\n                                '.join(cs_code))
        if ble_class.attributes['index'].value == '0' and ble_command.attributes['index'].value == '0':
            response_callback_parser_conditions.append('        SetBusy(false);')
        response_callback_parser_conditions.append('    }')

        num_responses += 1

    if num_responses > 0:
        response_callback_structure.append('}')
    response_callback_parser_conditions.append('}')

    if len(event_callback_parser_conditions) > 0:
        event_callback_parser_conditions.append('else if (bgapiRXBuffer[2] == ' + ble_class.attributes['index'].value + ') {')
    else:
        event_callback_parser_conditions.append('if (bgapiRXBuffer[2] == ' + ble_class.attributes['index'].value + ') {')

    num_events = 0
    event_prefix = ''
    for ble_event in ble_class.getElementsByTagName('event'):
        #print class_name + '_' + ble_event.attributes['name'].value
        event_name_parts = (string.capwords(class_name + ' ' + ble_event.attributes['name'].value.replace('_', ' '))).split(' ')
        event_name = ''
        word_pos = 0
        for word in event_name_parts:
            if word in ['Gap', 'Sm', 'Smp', 'Adc', 'Rx', 'Tx', 'Ps', 'Phy', 'Io', 'Spi', 'I2c', 'Dfu']:
                event_name += word.upper()
            elif word == 'Attclient':
                event_name += 'ATTClient'
            else:
                event_name += word
            if word_pos == 0:
                if num_events == 0:
                    event_callback_structure.append('namespace ' + event_name + ' {')
                #cnd_name += '.'
                event_prefix = event_name
                event_name = ''
            word_pos += 1

        # gather parameter info, if present
        ble_params = ble_event.getElementsByTagName('params');
        parameters = []
        param_init = []
        event_args = []
        buf_pos = 4
        if len(ble_params) > 0:
            for ble_param in ble_params[0].getElementsByTagName('param'):
                if (ble_param.attributes['type'].value == 'uint8'):
                    parameters.append('Byte ' + ble_param.attributes['name'].value)
                    param_init.append('this.' + ble_param.attributes['name'].value + ' = ' + ble_param.attributes['name'].value + ';')
                    event_args.append('bgapiRXBuffer[' + str(buf_pos) + ']')
                    buf_pos += 1
                elif (ble_param.attributes['type'].value == 'uint16'):
                    parameters.append('UInt16 ' + ble_param.attributes['name'].value)
                    param_init.append('this.' + ble_param.attributes['name'].value + ' = ' + ble_param.attributes['name'].value + ';')
                    event_args.append('(UInt16)(bgapiRXBuffer[' + str(buf_pos) + '] + (bgapiRXBuffer[' + str(buf_pos + 1) + '] << 8))')
                    buf_pos += 2
                elif (ble_param.attributes['type'].value == 'uint32'):
                    parameters.append('UInt32 ' + ble_param.attributes['name'].value)
                    param_init.append('this.' + ble_param.attributes['name'].value + ' = ' + ble_param.attributes['name'].value + ';')
                    event_args.append('(UInt32)(bgapiRXBuffer[' + str(buf_pos) + '] + (bgapiRXBuffer[' + str(buf_pos + 1) + '] << 8) + (bgapiRXBuffer[' + str(buf_pos + 2) + '] << 16) + (bgapiRXBuffer[' + str(buf_pos + 3) + '] << 24))')
                    buf_pos += 4
                elif (ble_param.attributes['type'].value == 'int8'):
                    parameters.append('SByte ' + ble_param.attributes['name'].value)
                    param_init.append('this.' + ble_param.attributes['name'].value + ' = ' + ble_param.attributes['name'].value + ';')
                    event_args.append('(SByte)(bgapiRXBuffer[' + str(buf_pos) + '])')
                    buf_pos += 1
                elif (ble_param.attributes['type'].value == 'int16'):
                    parameters.append('Int16 ' + ble_param.attributes['name'].value)
                    param_init.append('this.' + ble_param.attributes['name'].value + ' = ' + ble_param.attributes['name'].value + ';')
                    event_args.append('(Int16)(bgapiRXBuffer[' + str(buf_pos) + '] + (bgapiRXBuffer[' + str(buf_pos + 1) + '] << 8))')
                    buf_pos += 2
                elif (ble_param.attributes['type'].value == 'int32'):
                    parameters.append('Int32 ' + ble_param.attributes['name'].value)
                    param_init.append('this.' + ble_param.attributes['name'].value + ' = ' + ble_param.attributes['name'].value + ';')
                    event_args.append('(Int32)(bgapiRXBuffer[' + str(buf_pos) + '] + (bgapiRXBuffer[' + str(buf_pos + 1) + '] << 8) + (bgapiRXBuffer[' + str(buf_pos + 2) + '] << 16) + (bgapiRXBuffer[' + str(buf_pos + 3) + '] << 24))')
                    buf_pos += 4
                elif (ble_param.attributes['type'].value == 'bd_addr'):
                    parameters.append('Byte[] ' + ble_param.attributes['name'].value)
                    param_init.append('this.' + ble_param.attributes['name'].value + ' = ' + ble_param.attributes['name'].value + ';')
                    event_args.append('(Byte[])(bgapiRXBuffer.Skip(' + str(buf_pos) + ').Take(6).ToArray())')
                    buf_pos += 6
                elif (ble_param.attributes['type'].value == 'uint8array'):
                    parameters.append('Byte[] ' + ble_param.attributes['name'].value)
                    param_init.append('this.' + ble_param.attributes['name'].value + ' = ' + ble_param.attributes['name'].value + ';')
                    event_args.append('(Byte[])(bgapiRXBuffer.Skip(' + str(buf_pos + 1) + ').Take(bgapiRXBuffer[' + str(buf_pos) + ']).ToArray())')
                    # buf_pos doesn't matter here since uint8arrays are ALWAYS at the end

        cs_code = []
        cs_code.append('if (BLEEvent' + event_prefix + event_name + ' != null) {')
        cs_code.append('    BLEEvent' + event_prefix + event_name + '(this, new Bluegiga.BLE.Events.' + event_prefix + '.' + event_name + 'EventArgs(')
        if len(event_args) > 0:
            cs_code.append('        ' + ',\n                                        '.join(event_args))
        cs_code.append('    ));')
        cs_code.append('}')

        event_callback_declarations.append('public event Bluegiga.BLE.Events.' + event_prefix + '.' + event_name + 'EventHandler BLEEvent' + event_prefix + event_name + ';')
        event_callback_structure.append('    public delegate void ' + event_name + 'EventHandler(object sender, Bluegiga.BLE.Events.' + event_prefix + '.' + event_name + 'EventArgs e);')
        event_callback_structure.append('    public class ' + event_name + 'EventArgs : EventArgs {')
        for parameter in parameters:
            event_callback_structure.append('        public readonly ' + parameter + ';')
        if len(param_init) > 0:
            event_callback_structure.append('        public ' + event_name + 'EventArgs(' + ', '.join(parameters) + ') {')
            event_callback_structure.append('            ' + '\n                        '.join(param_init))
            event_callback_structure.append('        }')
        else:
            event_callback_structure.append('        public ' + event_name + 'EventArgs(' + ', '.join(parameters) + ') { }')
        event_callback_structure.append('    }')
        event_callback_structure.append('')

        if num_events > 0:
            event_callback_parser_conditions.append('    else if (bgapiRXBuffer[3] == ' + ble_event.attributes['index'].value + ')')
            event_callback_parser_conditions.append('    {')
        else:
            event_callback_parser_conditions.append('    if (bgapiRXBuffer[3] == ' + ble_event.attributes['index'].value + ')')
            event_callback_parser_conditions.append('    {')

        event_callback_parser_conditions.append('        ' + '\n                                '.join(cs_code))
        if ble_class.attributes['index'].value == '0' and ble_event.attributes['index'].value == '0':
            event_callback_parser_conditions.append('        SetBusy(false);')
        event_callback_parser_conditions.append('    }')

        num_events += 1

    if num_events > 0:
        event_callback_structure.append('}')
    event_callback_parser_conditions.append('}')

    for ble_enum in ble_class.getElementsByTagName('enum'):
        #print class_name + '_' + ble_enum.attributes['name'].value
        enum_name = class_name + '_' + ble_enum.attributes['name'].value
        constant_macros.append('#define BGLIB_' + (enum_name.upper() + ' ').ljust(54) + ble_enum.attributes['value'].value)

    if constant_macros[len(constant_macros) - 1] != '':
        constant_macros.append('')

# create C# library files
print("Writing C# source library files in 'BGLib\\MSVCSharp'...")
source = open('BGLib.cs', 'w')
source.write('// Bluegiga BGLib C# interface library\n\
// 2013-01-15 by Jeff Rowberg <jeff@rowberg.net\n\
// Updates should (hopefully) always be available at https://github.com/jrowberg/bglib\n\
\n\
/* ============================================\n\
BGLib C# interface library code is placed under the MIT license\n\
Copyright (c) 2013 Jeff Rowberg\n\
\n\
Permission is hereby granted, free of charge, to any person obtaining a copy\n\
of this software and associated documentation files (the "Software"), to deal\n\
in the Software without restriction, including without limitation the rights\n\
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell\n\
copies of the Software, and to permit persons to whom the Software is\n\
furnished to do so, subject to the following conditions:\n\
\n\
The above copyright notice and this permission notice shall be included in\n\
all copies or substantial portions of the Software.\n\
\n\
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR\n\
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,\n\
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE\n\
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER\n\
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,\n\
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN\n\
THE SOFTWARE.\n\
===============================================\n\
*/\n\
\n\
using System;\n\
using System.Collections.Generic;\n\
using System.Linq;\n\
using System.Text;\n\
\n\
namespace Bluegiga {\n\
\n\
    namespace BLE {\n\
\n\
        namespace Responses {\n\
            ' + ('\n            '.join(response_callback_structure)) + '\n\
        }\n\
\n\
        namespace Events {\n\
            ' + ('\n            '.join(event_callback_structure)) + '\n\
        }\n\
\n\
    }\n\
\n\
    public class BGLib\n\
    {\n\
\n\
        ' + ('\n        '.join(command_method_definitions)) + '\n\n\
        ' + ('\n        '.join(response_callback_declarations)) + '\n\n\
        ' + ('\n        '.join(event_callback_declarations)) + '\n\
\n\
        private Byte[] bgapiRXBuffer = new Byte[65];\n\
        private int bgapiRXBufferPos = 0;\n\
        private int bgapiRXDataLen = 0;\n\
\n\
        private Boolean bgapiPacketMode = false;\n\
\n\
        private Boolean parserBusy = false;\n\
\n\
        public void SetBusy(Boolean isBusy) {\n\
            this.parserBusy = isBusy;\n\
        }\n\
\n\
        public Boolean IsBusy() {\n\
            return parserBusy;\n\
        }\n\
\n\
        public void SetPacketMode(Boolean packetMode) {\n\
            this.bgapiPacketMode = packetMode;\n\
        }\n\
\n\
        public UInt16 Parse(Byte ch) {\n\
            /*#ifdef DEBUG\n\
                // DEBUG: output hex value of incoming character\n\
                if (ch < 16) Serial.write(0x30);    // leading \'0\'\n\
                Serial.print(ch, HEX);              // actual hex value\n\
                Serial.write(0x20);                 // trailing \' \'\n\
            #endif*/\n\
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
                    /*#ifdef DEBUG\n\
                        Serial.print("*** Packet frame sync error! Expected .0000... binary, got 0x");\n\
                        Serial.println(ch, HEX);\n\
                    #endif*/\n\
                    return 1; // packet format error\n\
                }\n\
            } else {\n\
                // middle of packet, assume we\'re okay\n\
                bgapiRXBuffer[bgapiRXBufferPos++] = ch;\n\
                if (bgapiRXBufferPos == 2) {\n\
                    // just received "Length Low" byte, so store expected packet length\n\
                    bgapiRXDataLen = ch + ((bgapiRXBuffer[0] & 0x07) << 8);\n\
                } else if (bgapiRXBufferPos == bgapiRXDataLen + 4) {\n\
                    // just received last expected byte\n\
                    /*#ifdef DEBUG\n\
                        Serial.print("\\n<- RX [ ");\n\
                        for (uint8_t i = 0; i < bgapiRXBufferPos; i++) {\n\
                            if (bgapiRXBuffer[i] < 16) Serial.write(0x30);\n\
                            Serial.print(bgapiRXBuffer[i], HEX);\n\
                            Serial.write(0x20);\n\
                        }\n\
                        Serial.println("]");\n\
                    #endif*/\n\
\n\
                    // check packet type\n\
                    if ((bgapiRXBuffer[0] & 0x80) == 0) {\n\
                        // 0x00 = Response packet\n\
                        ' + ('\n                        '.join(response_callback_parser_conditions)) + '\n\
                        SetBusy(false);\n\
                    } else {\n\
                        // 0x80 = Event packet\n\
                        ' + ('\n                        '.join(event_callback_parser_conditions)) + '\n\
                    }\n\
\n\
                    // reset RX packet buffer position to be ready for new packet\n\
                    bgapiRXBufferPos = 0;\n\
                }\n\
            }\n\
\n\
            return 0; // parsed successfully\n\
        }\n\
\n\
        public UInt16 SendCommand(System.IO.Ports.SerialPort port, Byte[] cmd) {\n\
            SetBusy(true);\n\
            if (bgapiPacketMode) port.Write(new Byte[] { (Byte)cmd.Length }, 0, 1);\n\
            port.Write(cmd, 0, cmd.Length);\n\
            return 0; // no error handling yet\n\
        }\n\
\n\
    }\n\
\n\
}\n\
')
source.close()

print("Finished!\n")

print("C# Installation Instructions:")
print("====================================")
print("1. Add BGLib.cs to your project")
print("2. Import Bluegiga.* in your source file(s)")
print("3. Add event handlers for desired BGLib response and event packets\n")
