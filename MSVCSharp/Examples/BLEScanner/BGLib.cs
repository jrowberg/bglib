// Bluegiga BGLib C# interface library
// 2015-03-19 by Jeff Rowberg <jeff@rowberg.net
// Updates should (hopefully) always be available at https://github.com/jrowberg/bglib

/* ============================================
BGLib C# interface library code is placed under the MIT license
Copyright (c) 2015 Jeff Rowberg

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

        namespace Responses {
            namespace System {
                public delegate void ResetEventHandler(object sender, Bluegiga.BLE.Responses.System.ResetEventArgs e);
                public class ResetEventArgs : EventArgs {
                    public ResetEventArgs() { }
                }
            
                public delegate void HelloEventHandler(object sender, Bluegiga.BLE.Responses.System.HelloEventArgs e);
                public class HelloEventArgs : EventArgs {
                    public HelloEventArgs() { }
                }
            
                public delegate void AddressGetEventHandler(object sender, Bluegiga.BLE.Responses.System.AddressGetEventArgs e);
                public class AddressGetEventArgs : EventArgs {
                    public readonly Byte[] address;
                    public AddressGetEventArgs(Byte[] address) {
                        this.address = address;
                    }
                }
            
                public delegate void RegWriteEventHandler(object sender, Bluegiga.BLE.Responses.System.RegWriteEventArgs e);
                public class RegWriteEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public RegWriteEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void RegReadEventHandler(object sender, Bluegiga.BLE.Responses.System.RegReadEventArgs e);
                public class RegReadEventArgs : EventArgs {
                    public readonly UInt16 address;
                    public readonly Byte value;
                    public RegReadEventArgs(UInt16 address, Byte value) {
                        this.address = address;
                        this.value = value;
                    }
                }
            
                public delegate void GetCountersEventHandler(object sender, Bluegiga.BLE.Responses.System.GetCountersEventArgs e);
                public class GetCountersEventArgs : EventArgs {
                    public readonly Byte txok;
                    public readonly Byte txretry;
                    public readonly Byte rxok;
                    public readonly Byte rxfail;
                    public readonly Byte mbuf;
                    public GetCountersEventArgs(Byte txok, Byte txretry, Byte rxok, Byte rxfail, Byte mbuf) {
                        this.txok = txok;
                        this.txretry = txretry;
                        this.rxok = rxok;
                        this.rxfail = rxfail;
                        this.mbuf = mbuf;
                    }
                }
            
                public delegate void GetConnectionsEventHandler(object sender, Bluegiga.BLE.Responses.System.GetConnectionsEventArgs e);
                public class GetConnectionsEventArgs : EventArgs {
                    public readonly Byte maxconn;
                    public GetConnectionsEventArgs(Byte maxconn) {
                        this.maxconn = maxconn;
                    }
                }
            
                public delegate void ReadMemoryEventHandler(object sender, Bluegiga.BLE.Responses.System.ReadMemoryEventArgs e);
                public class ReadMemoryEventArgs : EventArgs {
                    public readonly UInt32 address;
                    public readonly Byte[] data;
                    public ReadMemoryEventArgs(UInt32 address, Byte[] data) {
                        this.address = address;
                        this.data = data;
                    }
                }
            
                public delegate void GetInfoEventHandler(object sender, Bluegiga.BLE.Responses.System.GetInfoEventArgs e);
                public class GetInfoEventArgs : EventArgs {
                    public readonly UInt16 major;
                    public readonly UInt16 minor;
                    public readonly UInt16 patch;
                    public readonly UInt16 build;
                    public readonly UInt16 ll_version;
                    public readonly Byte protocol_version;
                    public readonly Byte hw;
                    public GetInfoEventArgs(UInt16 major, UInt16 minor, UInt16 patch, UInt16 build, UInt16 ll_version, Byte protocol_version, Byte hw) {
                        this.major = major;
                        this.minor = minor;
                        this.patch = patch;
                        this.build = build;
                        this.ll_version = ll_version;
                        this.protocol_version = protocol_version;
                        this.hw = hw;
                    }
                }
            
                public delegate void EndpointTXEventHandler(object sender, Bluegiga.BLE.Responses.System.EndpointTXEventArgs e);
                public class EndpointTXEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public EndpointTXEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void WhitelistAppendEventHandler(object sender, Bluegiga.BLE.Responses.System.WhitelistAppendEventArgs e);
                public class WhitelistAppendEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public WhitelistAppendEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void WhitelistRemoveEventHandler(object sender, Bluegiga.BLE.Responses.System.WhitelistRemoveEventArgs e);
                public class WhitelistRemoveEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public WhitelistRemoveEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void WhitelistClearEventHandler(object sender, Bluegiga.BLE.Responses.System.WhitelistClearEventArgs e);
                public class WhitelistClearEventArgs : EventArgs {
                    public WhitelistClearEventArgs() { }
                }
            
                public delegate void EndpointRXEventHandler(object sender, Bluegiga.BLE.Responses.System.EndpointRXEventArgs e);
                public class EndpointRXEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public readonly Byte[] data;
                    public EndpointRXEventArgs(UInt16 result, Byte[] data) {
                        this.result = result;
                        this.data = data;
                    }
                }
            
                public delegate void EndpointSetWatermarksEventHandler(object sender, Bluegiga.BLE.Responses.System.EndpointSetWatermarksEventArgs e);
                public class EndpointSetWatermarksEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public EndpointSetWatermarksEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void AesSetkeyEventHandler(object sender, Bluegiga.BLE.Responses.System.AesSetkeyEventArgs e);
                public class AesSetkeyEventArgs : EventArgs {
                    public AesSetkeyEventArgs() { }
                }
            
                public delegate void AesEncryptEventHandler(object sender, Bluegiga.BLE.Responses.System.AesEncryptEventArgs e);
                public class AesEncryptEventArgs : EventArgs {
                    public readonly Byte[] data;
                    public AesEncryptEventArgs(Byte[] data) {
                        this.data = data;
                    }
                }
            
                public delegate void AesDecryptEventHandler(object sender, Bluegiga.BLE.Responses.System.AesDecryptEventArgs e);
                public class AesDecryptEventArgs : EventArgs {
                    public readonly Byte[] data;
                    public AesDecryptEventArgs(Byte[] data) {
                        this.data = data;
                    }
                }
            
            }
            namespace Flash {
                public delegate void PSDefragEventHandler(object sender, Bluegiga.BLE.Responses.Flash.PSDefragEventArgs e);
                public class PSDefragEventArgs : EventArgs {
                    public PSDefragEventArgs() { }
                }
            
                public delegate void PSDumpEventHandler(object sender, Bluegiga.BLE.Responses.Flash.PSDumpEventArgs e);
                public class PSDumpEventArgs : EventArgs {
                    public PSDumpEventArgs() { }
                }
            
                public delegate void PSEraseAllEventHandler(object sender, Bluegiga.BLE.Responses.Flash.PSEraseAllEventArgs e);
                public class PSEraseAllEventArgs : EventArgs {
                    public PSEraseAllEventArgs() { }
                }
            
                public delegate void PSSaveEventHandler(object sender, Bluegiga.BLE.Responses.Flash.PSSaveEventArgs e);
                public class PSSaveEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public PSSaveEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void PSLoadEventHandler(object sender, Bluegiga.BLE.Responses.Flash.PSLoadEventArgs e);
                public class PSLoadEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public readonly Byte[] value;
                    public PSLoadEventArgs(UInt16 result, Byte[] value) {
                        this.result = result;
                        this.value = value;
                    }
                }
            
                public delegate void PSEraseEventHandler(object sender, Bluegiga.BLE.Responses.Flash.PSEraseEventArgs e);
                public class PSEraseEventArgs : EventArgs {
                    public PSEraseEventArgs() { }
                }
            
                public delegate void ErasePageEventHandler(object sender, Bluegiga.BLE.Responses.Flash.ErasePageEventArgs e);
                public class ErasePageEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public ErasePageEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void WriteDataEventHandler(object sender, Bluegiga.BLE.Responses.Flash.WriteDataEventArgs e);
                public class WriteDataEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public WriteDataEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void ReadDataEventHandler(object sender, Bluegiga.BLE.Responses.Flash.ReadDataEventArgs e);
                public class ReadDataEventArgs : EventArgs {
                    public readonly Byte[] data;
                    public ReadDataEventArgs(Byte[] data) {
                        this.data = data;
                    }
                }
            
            }
            namespace Attributes {
                public delegate void WriteEventHandler(object sender, Bluegiga.BLE.Responses.Attributes.WriteEventArgs e);
                public class WriteEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public WriteEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void ReadEventHandler(object sender, Bluegiga.BLE.Responses.Attributes.ReadEventArgs e);
                public class ReadEventArgs : EventArgs {
                    public readonly UInt16 handle;
                    public readonly UInt16 offset;
                    public readonly UInt16 result;
                    public readonly Byte[] value;
                    public ReadEventArgs(UInt16 handle, UInt16 offset, UInt16 result, Byte[] value) {
                        this.handle = handle;
                        this.offset = offset;
                        this.result = result;
                        this.value = value;
                    }
                }
            
                public delegate void ReadTypeEventHandler(object sender, Bluegiga.BLE.Responses.Attributes.ReadTypeEventArgs e);
                public class ReadTypeEventArgs : EventArgs {
                    public readonly UInt16 handle;
                    public readonly UInt16 result;
                    public readonly Byte[] value;
                    public ReadTypeEventArgs(UInt16 handle, UInt16 result, Byte[] value) {
                        this.handle = handle;
                        this.result = result;
                        this.value = value;
                    }
                }
            
                public delegate void UserReadResponseEventHandler(object sender, Bluegiga.BLE.Responses.Attributes.UserReadResponseEventArgs e);
                public class UserReadResponseEventArgs : EventArgs {
                    public UserReadResponseEventArgs() { }
                }
            
                public delegate void UserWriteResponseEventHandler(object sender, Bluegiga.BLE.Responses.Attributes.UserWriteResponseEventArgs e);
                public class UserWriteResponseEventArgs : EventArgs {
                    public UserWriteResponseEventArgs() { }
                }
            
                public delegate void SendEventHandler(object sender, Bluegiga.BLE.Responses.Attributes.SendEventArgs e);
                public class SendEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public SendEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
            }
            namespace Connection {
                public delegate void DisconnectEventHandler(object sender, Bluegiga.BLE.Responses.Connection.DisconnectEventArgs e);
                public class DisconnectEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public DisconnectEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void GetRssiEventHandler(object sender, Bluegiga.BLE.Responses.Connection.GetRssiEventArgs e);
                public class GetRssiEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly SByte rssi;
                    public GetRssiEventArgs(Byte connection, SByte rssi) {
                        this.connection = connection;
                        this.rssi = rssi;
                    }
                }
            
                public delegate void UpdateEventHandler(object sender, Bluegiga.BLE.Responses.Connection.UpdateEventArgs e);
                public class UpdateEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public UpdateEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void VersionUpdateEventHandler(object sender, Bluegiga.BLE.Responses.Connection.VersionUpdateEventArgs e);
                public class VersionUpdateEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public VersionUpdateEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void ChannelMapGetEventHandler(object sender, Bluegiga.BLE.Responses.Connection.ChannelMapGetEventArgs e);
                public class ChannelMapGetEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly Byte[] map;
                    public ChannelMapGetEventArgs(Byte connection, Byte[] map) {
                        this.connection = connection;
                        this.map = map;
                    }
                }
            
                public delegate void ChannelMapSetEventHandler(object sender, Bluegiga.BLE.Responses.Connection.ChannelMapSetEventArgs e);
                public class ChannelMapSetEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public ChannelMapSetEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void FeaturesGetEventHandler(object sender, Bluegiga.BLE.Responses.Connection.FeaturesGetEventArgs e);
                public class FeaturesGetEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public FeaturesGetEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void GetStatusEventHandler(object sender, Bluegiga.BLE.Responses.Connection.GetStatusEventArgs e);
                public class GetStatusEventArgs : EventArgs {
                    public readonly Byte connection;
                    public GetStatusEventArgs(Byte connection) {
                        this.connection = connection;
                    }
                }
            
                public delegate void RawTXEventHandler(object sender, Bluegiga.BLE.Responses.Connection.RawTXEventArgs e);
                public class RawTXEventArgs : EventArgs {
                    public readonly Byte connection;
                    public RawTXEventArgs(Byte connection) {
                        this.connection = connection;
                    }
                }
            
            }
            namespace ATTClient {
                public delegate void FindByTypeValueEventHandler(object sender, Bluegiga.BLE.Responses.ATTClient.FindByTypeValueEventArgs e);
                public class FindByTypeValueEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public FindByTypeValueEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void ReadByGroupTypeEventHandler(object sender, Bluegiga.BLE.Responses.ATTClient.ReadByGroupTypeEventArgs e);
                public class ReadByGroupTypeEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public ReadByGroupTypeEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void ReadByTypeEventHandler(object sender, Bluegiga.BLE.Responses.ATTClient.ReadByTypeEventArgs e);
                public class ReadByTypeEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public ReadByTypeEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void FindInformationEventHandler(object sender, Bluegiga.BLE.Responses.ATTClient.FindInformationEventArgs e);
                public class FindInformationEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public FindInformationEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void ReadByHandleEventHandler(object sender, Bluegiga.BLE.Responses.ATTClient.ReadByHandleEventArgs e);
                public class ReadByHandleEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public ReadByHandleEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void AttributeWriteEventHandler(object sender, Bluegiga.BLE.Responses.ATTClient.AttributeWriteEventArgs e);
                public class AttributeWriteEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public AttributeWriteEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void WriteCommandEventHandler(object sender, Bluegiga.BLE.Responses.ATTClient.WriteCommandEventArgs e);
                public class WriteCommandEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public WriteCommandEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void IndicateConfirmEventHandler(object sender, Bluegiga.BLE.Responses.ATTClient.IndicateConfirmEventArgs e);
                public class IndicateConfirmEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public IndicateConfirmEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void ReadLongEventHandler(object sender, Bluegiga.BLE.Responses.ATTClient.ReadLongEventArgs e);
                public class ReadLongEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public ReadLongEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void PrepareWriteEventHandler(object sender, Bluegiga.BLE.Responses.ATTClient.PrepareWriteEventArgs e);
                public class PrepareWriteEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public PrepareWriteEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void ExecuteWriteEventHandler(object sender, Bluegiga.BLE.Responses.ATTClient.ExecuteWriteEventArgs e);
                public class ExecuteWriteEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public ExecuteWriteEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
                public delegate void ReadMultipleEventHandler(object sender, Bluegiga.BLE.Responses.ATTClient.ReadMultipleEventArgs e);
                public class ReadMultipleEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public ReadMultipleEventArgs(Byte connection, UInt16 result) {
                        this.connection = connection;
                        this.result = result;
                    }
                }
            
            }
            namespace SM {
                public delegate void EncryptStartEventHandler(object sender, Bluegiga.BLE.Responses.SM.EncryptStartEventArgs e);
                public class EncryptStartEventArgs : EventArgs {
                    public readonly Byte handle;
                    public readonly UInt16 result;
                    public EncryptStartEventArgs(Byte handle, UInt16 result) {
                        this.handle = handle;
                        this.result = result;
                    }
                }
            
                public delegate void SetBondableModeEventHandler(object sender, Bluegiga.BLE.Responses.SM.SetBondableModeEventArgs e);
                public class SetBondableModeEventArgs : EventArgs {
                    public SetBondableModeEventArgs() { }
                }
            
                public delegate void DeleteBondingEventHandler(object sender, Bluegiga.BLE.Responses.SM.DeleteBondingEventArgs e);
                public class DeleteBondingEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public DeleteBondingEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void SetParametersEventHandler(object sender, Bluegiga.BLE.Responses.SM.SetParametersEventArgs e);
                public class SetParametersEventArgs : EventArgs {
                    public SetParametersEventArgs() { }
                }
            
                public delegate void PasskeyEntryEventHandler(object sender, Bluegiga.BLE.Responses.SM.PasskeyEntryEventArgs e);
                public class PasskeyEntryEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public PasskeyEntryEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void GetBondsEventHandler(object sender, Bluegiga.BLE.Responses.SM.GetBondsEventArgs e);
                public class GetBondsEventArgs : EventArgs {
                    public readonly Byte bonds;
                    public GetBondsEventArgs(Byte bonds) {
                        this.bonds = bonds;
                    }
                }
            
                public delegate void SetOobDataEventHandler(object sender, Bluegiga.BLE.Responses.SM.SetOobDataEventArgs e);
                public class SetOobDataEventArgs : EventArgs {
                    public SetOobDataEventArgs() { }
                }
            
                public delegate void WhitelistBondsEventHandler(object sender, Bluegiga.BLE.Responses.SM.WhitelistBondsEventArgs e);
                public class WhitelistBondsEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public readonly Byte count;
                    public WhitelistBondsEventArgs(UInt16 result, Byte count) {
                        this.result = result;
                        this.count = count;
                    }
                }
            
            }
            namespace GAP {
                public delegate void SetPrivacyFlagsEventHandler(object sender, Bluegiga.BLE.Responses.GAP.SetPrivacyFlagsEventArgs e);
                public class SetPrivacyFlagsEventArgs : EventArgs {
                    public SetPrivacyFlagsEventArgs() { }
                }
            
                public delegate void SetModeEventHandler(object sender, Bluegiga.BLE.Responses.GAP.SetModeEventArgs e);
                public class SetModeEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public SetModeEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void DiscoverEventHandler(object sender, Bluegiga.BLE.Responses.GAP.DiscoverEventArgs e);
                public class DiscoverEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public DiscoverEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void ConnectDirectEventHandler(object sender, Bluegiga.BLE.Responses.GAP.ConnectDirectEventArgs e);
                public class ConnectDirectEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public readonly Byte connection_handle;
                    public ConnectDirectEventArgs(UInt16 result, Byte connection_handle) {
                        this.result = result;
                        this.connection_handle = connection_handle;
                    }
                }
            
                public delegate void EndProcedureEventHandler(object sender, Bluegiga.BLE.Responses.GAP.EndProcedureEventArgs e);
                public class EndProcedureEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public EndProcedureEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void ConnectSelectiveEventHandler(object sender, Bluegiga.BLE.Responses.GAP.ConnectSelectiveEventArgs e);
                public class ConnectSelectiveEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public readonly Byte connection_handle;
                    public ConnectSelectiveEventArgs(UInt16 result, Byte connection_handle) {
                        this.result = result;
                        this.connection_handle = connection_handle;
                    }
                }
            
                public delegate void SetFilteringEventHandler(object sender, Bluegiga.BLE.Responses.GAP.SetFilteringEventArgs e);
                public class SetFilteringEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public SetFilteringEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void SetScanParametersEventHandler(object sender, Bluegiga.BLE.Responses.GAP.SetScanParametersEventArgs e);
                public class SetScanParametersEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public SetScanParametersEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void SetAdvParametersEventHandler(object sender, Bluegiga.BLE.Responses.GAP.SetAdvParametersEventArgs e);
                public class SetAdvParametersEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public SetAdvParametersEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void SetAdvDataEventHandler(object sender, Bluegiga.BLE.Responses.GAP.SetAdvDataEventArgs e);
                public class SetAdvDataEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public SetAdvDataEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void SetDirectedConnectableModeEventHandler(object sender, Bluegiga.BLE.Responses.GAP.SetDirectedConnectableModeEventArgs e);
                public class SetDirectedConnectableModeEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public SetDirectedConnectableModeEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
            }
            namespace Hardware {
                public delegate void IOPortConfigIrqEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.IOPortConfigIrqEventArgs e);
                public class IOPortConfigIrqEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public IOPortConfigIrqEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void SetSoftTimerEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.SetSoftTimerEventArgs e);
                public class SetSoftTimerEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public SetSoftTimerEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void ADCReadEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.ADCReadEventArgs e);
                public class ADCReadEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public ADCReadEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void IOPortConfigDirectionEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.IOPortConfigDirectionEventArgs e);
                public class IOPortConfigDirectionEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public IOPortConfigDirectionEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void IOPortConfigFunctionEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.IOPortConfigFunctionEventArgs e);
                public class IOPortConfigFunctionEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public IOPortConfigFunctionEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void IOPortConfigPullEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.IOPortConfigPullEventArgs e);
                public class IOPortConfigPullEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public IOPortConfigPullEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void IOPortWriteEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.IOPortWriteEventArgs e);
                public class IOPortWriteEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public IOPortWriteEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void IOPortReadEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.IOPortReadEventArgs e);
                public class IOPortReadEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public readonly Byte port;
                    public readonly Byte data;
                    public IOPortReadEventArgs(UInt16 result, Byte port, Byte data) {
                        this.result = result;
                        this.port = port;
                        this.data = data;
                    }
                }
            
                public delegate void SPIConfigEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.SPIConfigEventArgs e);
                public class SPIConfigEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public SPIConfigEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void SPITransferEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.SPITransferEventArgs e);
                public class SPITransferEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public readonly Byte channel;
                    public readonly Byte[] data;
                    public SPITransferEventArgs(UInt16 result, Byte channel, Byte[] data) {
                        this.result = result;
                        this.channel = channel;
                        this.data = data;
                    }
                }
            
                public delegate void I2CReadEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.I2CReadEventArgs e);
                public class I2CReadEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public readonly Byte[] data;
                    public I2CReadEventArgs(UInt16 result, Byte[] data) {
                        this.result = result;
                        this.data = data;
                    }
                }
            
                public delegate void I2CWriteEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.I2CWriteEventArgs e);
                public class I2CWriteEventArgs : EventArgs {
                    public readonly Byte written;
                    public I2CWriteEventArgs(Byte written) {
                        this.written = written;
                    }
                }
            
                public delegate void SetTxpowerEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.SetTxpowerEventArgs e);
                public class SetTxpowerEventArgs : EventArgs {
                    public SetTxpowerEventArgs() { }
                }
            
                public delegate void TimerComparatorEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.TimerComparatorEventArgs e);
                public class TimerComparatorEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public TimerComparatorEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void IOPortIrqEnableEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.IOPortIrqEnableEventArgs e);
                public class IOPortIrqEnableEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public IOPortIrqEnableEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void IOPortIrqDirectionEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.IOPortIrqDirectionEventArgs e);
                public class IOPortIrqDirectionEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public IOPortIrqDirectionEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void AnalogComparatorEnableEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.AnalogComparatorEnableEventArgs e);
                public class AnalogComparatorEnableEventArgs : EventArgs {
                    public AnalogComparatorEnableEventArgs() { }
                }
            
                public delegate void AnalogComparatorReadEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.AnalogComparatorReadEventArgs e);
                public class AnalogComparatorReadEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public readonly Byte output;
                    public AnalogComparatorReadEventArgs(UInt16 result, Byte output) {
                        this.result = result;
                        this.output = output;
                    }
                }
            
                public delegate void AnalogComparatorConfigIrqEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.AnalogComparatorConfigIrqEventArgs e);
                public class AnalogComparatorConfigIrqEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public AnalogComparatorConfigIrqEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void SetRxgainEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.SetRxgainEventArgs e);
                public class SetRxgainEventArgs : EventArgs {
                    public SetRxgainEventArgs() { }
                }
            
                public delegate void UsbEnableEventHandler(object sender, Bluegiga.BLE.Responses.Hardware.UsbEnableEventArgs e);
                public class UsbEnableEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public UsbEnableEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
            }
            namespace Test {
                public delegate void PHYTXEventHandler(object sender, Bluegiga.BLE.Responses.Test.PHYTXEventArgs e);
                public class PHYTXEventArgs : EventArgs {
                    public PHYTXEventArgs() { }
                }
            
                public delegate void PHYRXEventHandler(object sender, Bluegiga.BLE.Responses.Test.PHYRXEventArgs e);
                public class PHYRXEventArgs : EventArgs {
                    public PHYRXEventArgs() { }
                }
            
                public delegate void PHYEndEventHandler(object sender, Bluegiga.BLE.Responses.Test.PHYEndEventArgs e);
                public class PHYEndEventArgs : EventArgs {
                    public readonly UInt16 counter;
                    public PHYEndEventArgs(UInt16 counter) {
                        this.counter = counter;
                    }
                }
            
                public delegate void PHYResetEventHandler(object sender, Bluegiga.BLE.Responses.Test.PHYResetEventArgs e);
                public class PHYResetEventArgs : EventArgs {
                    public PHYResetEventArgs() { }
                }
            
                public delegate void GetChannelMapEventHandler(object sender, Bluegiga.BLE.Responses.Test.GetChannelMapEventArgs e);
                public class GetChannelMapEventArgs : EventArgs {
                    public readonly Byte[] channel_map;
                    public GetChannelMapEventArgs(Byte[] channel_map) {
                        this.channel_map = channel_map;
                    }
                }
            
                public delegate void DebugEventHandler(object sender, Bluegiga.BLE.Responses.Test.DebugEventArgs e);
                public class DebugEventArgs : EventArgs {
                    public readonly Byte[] output;
                    public DebugEventArgs(Byte[] output) {
                        this.output = output;
                    }
                }
            
                public delegate void ChannelModeEventHandler(object sender, Bluegiga.BLE.Responses.Test.ChannelModeEventArgs e);
                public class ChannelModeEventArgs : EventArgs {
                    public ChannelModeEventArgs() { }
                }
            
            }
            namespace DFU {
                public delegate void ResetEventHandler(object sender, Bluegiga.BLE.Responses.DFU.ResetEventArgs e);
                public class ResetEventArgs : EventArgs {
                    public ResetEventArgs() { }
                }
            
                public delegate void FlashSetAddressEventHandler(object sender, Bluegiga.BLE.Responses.DFU.FlashSetAddressEventArgs e);
                public class FlashSetAddressEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public FlashSetAddressEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void FlashUploadEventHandler(object sender, Bluegiga.BLE.Responses.DFU.FlashUploadEventArgs e);
                public class FlashUploadEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public FlashUploadEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
                public delegate void FlashUploadFinishEventHandler(object sender, Bluegiga.BLE.Responses.DFU.FlashUploadFinishEventArgs e);
                public class FlashUploadFinishEventArgs : EventArgs {
                    public readonly UInt16 result;
                    public FlashUploadFinishEventArgs(UInt16 result) {
                        this.result = result;
                    }
                }
            
            }
        }

        namespace Events {
            namespace System {
                public delegate void BootEventHandler(object sender, Bluegiga.BLE.Events.System.BootEventArgs e);
                public class BootEventArgs : EventArgs {
                    public readonly UInt16 major;
                    public readonly UInt16 minor;
                    public readonly UInt16 patch;
                    public readonly UInt16 build;
                    public readonly UInt16 ll_version;
                    public readonly Byte protocol_version;
                    public readonly Byte hw;
                    public BootEventArgs(UInt16 major, UInt16 minor, UInt16 patch, UInt16 build, UInt16 ll_version, Byte protocol_version, Byte hw) {
                        this.major = major;
                        this.minor = minor;
                        this.patch = patch;
                        this.build = build;
                        this.ll_version = ll_version;
                        this.protocol_version = protocol_version;
                        this.hw = hw;
                    }
                }
            
                public delegate void DebugEventHandler(object sender, Bluegiga.BLE.Events.System.DebugEventArgs e);
                public class DebugEventArgs : EventArgs {
                    public readonly Byte[] data;
                    public DebugEventArgs(Byte[] data) {
                        this.data = data;
                    }
                }
            
                public delegate void EndpointWatermarkRXEventHandler(object sender, Bluegiga.BLE.Events.System.EndpointWatermarkRXEventArgs e);
                public class EndpointWatermarkRXEventArgs : EventArgs {
                    public readonly Byte endpoint;
                    public readonly Byte data;
                    public EndpointWatermarkRXEventArgs(Byte endpoint, Byte data) {
                        this.endpoint = endpoint;
                        this.data = data;
                    }
                }
            
                public delegate void EndpointWatermarkTXEventHandler(object sender, Bluegiga.BLE.Events.System.EndpointWatermarkTXEventArgs e);
                public class EndpointWatermarkTXEventArgs : EventArgs {
                    public readonly Byte endpoint;
                    public readonly Byte data;
                    public EndpointWatermarkTXEventArgs(Byte endpoint, Byte data) {
                        this.endpoint = endpoint;
                        this.data = data;
                    }
                }
            
                public delegate void ScriptFailureEventHandler(object sender, Bluegiga.BLE.Events.System.ScriptFailureEventArgs e);
                public class ScriptFailureEventArgs : EventArgs {
                    public readonly UInt16 address;
                    public readonly UInt16 reason;
                    public ScriptFailureEventArgs(UInt16 address, UInt16 reason) {
                        this.address = address;
                        this.reason = reason;
                    }
                }
            
                public delegate void NoLicenseKeyEventHandler(object sender, Bluegiga.BLE.Events.System.NoLicenseKeyEventArgs e);
                public class NoLicenseKeyEventArgs : EventArgs {
                    public NoLicenseKeyEventArgs() { }
                }
            
                public delegate void ProtocolErrorEventHandler(object sender, Bluegiga.BLE.Events.System.ProtocolErrorEventArgs e);
                public class ProtocolErrorEventArgs : EventArgs {
                    public readonly UInt16 reason;
                    public ProtocolErrorEventArgs(UInt16 reason) {
                        this.reason = reason;
                    }
                }
            
            }
            namespace Flash {
                public delegate void PSKeyEventHandler(object sender, Bluegiga.BLE.Events.Flash.PSKeyEventArgs e);
                public class PSKeyEventArgs : EventArgs {
                    public readonly UInt16 key;
                    public readonly Byte[] value;
                    public PSKeyEventArgs(UInt16 key, Byte[] value) {
                        this.key = key;
                        this.value = value;
                    }
                }
            
            }
            namespace Attributes {
                public delegate void ValueEventHandler(object sender, Bluegiga.BLE.Events.Attributes.ValueEventArgs e);
                public class ValueEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly Byte reason;
                    public readonly UInt16 handle;
                    public readonly UInt16 offset;
                    public readonly Byte[] value;
                    public ValueEventArgs(Byte connection, Byte reason, UInt16 handle, UInt16 offset, Byte[] value) {
                        this.connection = connection;
                        this.reason = reason;
                        this.handle = handle;
                        this.offset = offset;
                        this.value = value;
                    }
                }
            
                public delegate void UserReadRequestEventHandler(object sender, Bluegiga.BLE.Events.Attributes.UserReadRequestEventArgs e);
                public class UserReadRequestEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 handle;
                    public readonly UInt16 offset;
                    public readonly Byte maxsize;
                    public UserReadRequestEventArgs(Byte connection, UInt16 handle, UInt16 offset, Byte maxsize) {
                        this.connection = connection;
                        this.handle = handle;
                        this.offset = offset;
                        this.maxsize = maxsize;
                    }
                }
            
                public delegate void StatusEventHandler(object sender, Bluegiga.BLE.Events.Attributes.StatusEventArgs e);
                public class StatusEventArgs : EventArgs {
                    public readonly UInt16 handle;
                    public readonly Byte flags;
                    public StatusEventArgs(UInt16 handle, Byte flags) {
                        this.handle = handle;
                        this.flags = flags;
                    }
                }
            
            }
            namespace Connection {
                public delegate void StatusEventHandler(object sender, Bluegiga.BLE.Events.Connection.StatusEventArgs e);
                public class StatusEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly Byte flags;
                    public readonly Byte[] address;
                    public readonly Byte address_type;
                    public readonly UInt16 conn_interval;
                    public readonly UInt16 timeout;
                    public readonly UInt16 latency;
                    public readonly Byte bonding;
                    public StatusEventArgs(Byte connection, Byte flags, Byte[] address, Byte address_type, UInt16 conn_interval, UInt16 timeout, UInt16 latency, Byte bonding) {
                        this.connection = connection;
                        this.flags = flags;
                        this.address = address;
                        this.address_type = address_type;
                        this.conn_interval = conn_interval;
                        this.timeout = timeout;
                        this.latency = latency;
                        this.bonding = bonding;
                    }
                }
            
                public delegate void VersionIndEventHandler(object sender, Bluegiga.BLE.Events.Connection.VersionIndEventArgs e);
                public class VersionIndEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly Byte vers_nr;
                    public readonly UInt16 comp_id;
                    public readonly UInt16 sub_vers_nr;
                    public VersionIndEventArgs(Byte connection, Byte vers_nr, UInt16 comp_id, UInt16 sub_vers_nr) {
                        this.connection = connection;
                        this.vers_nr = vers_nr;
                        this.comp_id = comp_id;
                        this.sub_vers_nr = sub_vers_nr;
                    }
                }
            
                public delegate void FeatureIndEventHandler(object sender, Bluegiga.BLE.Events.Connection.FeatureIndEventArgs e);
                public class FeatureIndEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly Byte[] features;
                    public FeatureIndEventArgs(Byte connection, Byte[] features) {
                        this.connection = connection;
                        this.features = features;
                    }
                }
            
                public delegate void RawRXEventHandler(object sender, Bluegiga.BLE.Events.Connection.RawRXEventArgs e);
                public class RawRXEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly Byte[] data;
                    public RawRXEventArgs(Byte connection, Byte[] data) {
                        this.connection = connection;
                        this.data = data;
                    }
                }
            
                public delegate void DisconnectedEventHandler(object sender, Bluegiga.BLE.Events.Connection.DisconnectedEventArgs e);
                public class DisconnectedEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 reason;
                    public DisconnectedEventArgs(Byte connection, UInt16 reason) {
                        this.connection = connection;
                        this.reason = reason;
                    }
                }
            
            }
            namespace ATTClient {
                public delegate void IndicatedEventHandler(object sender, Bluegiga.BLE.Events.ATTClient.IndicatedEventArgs e);
                public class IndicatedEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 attrhandle;
                    public IndicatedEventArgs(Byte connection, UInt16 attrhandle) {
                        this.connection = connection;
                        this.attrhandle = attrhandle;
                    }
                }
            
                public delegate void ProcedureCompletedEventHandler(object sender, Bluegiga.BLE.Events.ATTClient.ProcedureCompletedEventArgs e);
                public class ProcedureCompletedEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public readonly UInt16 chrhandle;
                    public ProcedureCompletedEventArgs(Byte connection, UInt16 result, UInt16 chrhandle) {
                        this.connection = connection;
                        this.result = result;
                        this.chrhandle = chrhandle;
                    }
                }
            
                public delegate void GroupFoundEventHandler(object sender, Bluegiga.BLE.Events.ATTClient.GroupFoundEventArgs e);
                public class GroupFoundEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 start;
                    public readonly UInt16 end;
                    public readonly Byte[] uuid;
                    public GroupFoundEventArgs(Byte connection, UInt16 start, UInt16 end, Byte[] uuid) {
                        this.connection = connection;
                        this.start = start;
                        this.end = end;
                        this.uuid = uuid;
                    }
                }
            
                public delegate void AttributeFoundEventHandler(object sender, Bluegiga.BLE.Events.ATTClient.AttributeFoundEventArgs e);
                public class AttributeFoundEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 chrdecl;
                    public readonly UInt16 value;
                    public readonly Byte properties;
                    public readonly Byte[] uuid;
                    public AttributeFoundEventArgs(Byte connection, UInt16 chrdecl, UInt16 value, Byte properties, Byte[] uuid) {
                        this.connection = connection;
                        this.chrdecl = chrdecl;
                        this.value = value;
                        this.properties = properties;
                        this.uuid = uuid;
                    }
                }
            
                public delegate void FindInformationFoundEventHandler(object sender, Bluegiga.BLE.Events.ATTClient.FindInformationFoundEventArgs e);
                public class FindInformationFoundEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 chrhandle;
                    public readonly Byte[] uuid;
                    public FindInformationFoundEventArgs(Byte connection, UInt16 chrhandle, Byte[] uuid) {
                        this.connection = connection;
                        this.chrhandle = chrhandle;
                        this.uuid = uuid;
                    }
                }
            
                public delegate void AttributeValueEventHandler(object sender, Bluegiga.BLE.Events.ATTClient.AttributeValueEventArgs e);
                public class AttributeValueEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly UInt16 atthandle;
                    public readonly Byte type;
                    public readonly Byte[] value;
                    public AttributeValueEventArgs(Byte connection, UInt16 atthandle, Byte type, Byte[] value) {
                        this.connection = connection;
                        this.atthandle = atthandle;
                        this.type = type;
                        this.value = value;
                    }
                }
            
                public delegate void ReadMultipleResponseEventHandler(object sender, Bluegiga.BLE.Events.ATTClient.ReadMultipleResponseEventArgs e);
                public class ReadMultipleResponseEventArgs : EventArgs {
                    public readonly Byte connection;
                    public readonly Byte[] handles;
                    public ReadMultipleResponseEventArgs(Byte connection, Byte[] handles) {
                        this.connection = connection;
                        this.handles = handles;
                    }
                }
            
            }
            namespace SM {
                public delegate void SMPDataEventHandler(object sender, Bluegiga.BLE.Events.SM.SMPDataEventArgs e);
                public class SMPDataEventArgs : EventArgs {
                    public readonly Byte handle;
                    public readonly Byte packet;
                    public readonly Byte[] data;
                    public SMPDataEventArgs(Byte handle, Byte packet, Byte[] data) {
                        this.handle = handle;
                        this.packet = packet;
                        this.data = data;
                    }
                }
            
                public delegate void BondingFailEventHandler(object sender, Bluegiga.BLE.Events.SM.BondingFailEventArgs e);
                public class BondingFailEventArgs : EventArgs {
                    public readonly Byte handle;
                    public readonly UInt16 result;
                    public BondingFailEventArgs(Byte handle, UInt16 result) {
                        this.handle = handle;
                        this.result = result;
                    }
                }
            
                public delegate void PasskeyDisplayEventHandler(object sender, Bluegiga.BLE.Events.SM.PasskeyDisplayEventArgs e);
                public class PasskeyDisplayEventArgs : EventArgs {
                    public readonly Byte handle;
                    public readonly UInt32 passkey;
                    public PasskeyDisplayEventArgs(Byte handle, UInt32 passkey) {
                        this.handle = handle;
                        this.passkey = passkey;
                    }
                }
            
                public delegate void PasskeyRequestEventHandler(object sender, Bluegiga.BLE.Events.SM.PasskeyRequestEventArgs e);
                public class PasskeyRequestEventArgs : EventArgs {
                    public readonly Byte handle;
                    public PasskeyRequestEventArgs(Byte handle) {
                        this.handle = handle;
                    }
                }
            
                public delegate void BondStatusEventHandler(object sender, Bluegiga.BLE.Events.SM.BondStatusEventArgs e);
                public class BondStatusEventArgs : EventArgs {
                    public readonly Byte bond;
                    public readonly Byte keysize;
                    public readonly Byte mitm;
                    public readonly Byte keys;
                    public BondStatusEventArgs(Byte bond, Byte keysize, Byte mitm, Byte keys) {
                        this.bond = bond;
                        this.keysize = keysize;
                        this.mitm = mitm;
                        this.keys = keys;
                    }
                }
            
            }
            namespace GAP {
                public delegate void ScanResponseEventHandler(object sender, Bluegiga.BLE.Events.GAP.ScanResponseEventArgs e);
                public class ScanResponseEventArgs : EventArgs {
                    public readonly SByte rssi;
                    public readonly Byte packet_type;
                    public readonly Byte[] sender;
                    public readonly Byte address_type;
                    public readonly Byte bond;
                    public readonly Byte[] data;
                    public ScanResponseEventArgs(SByte rssi, Byte packet_type, Byte[] sender, Byte address_type, Byte bond, Byte[] data) {
                        this.rssi = rssi;
                        this.packet_type = packet_type;
                        this.sender = sender;
                        this.address_type = address_type;
                        this.bond = bond;
                        this.data = data;
                    }
                }
            
                public delegate void ModeChangedEventHandler(object sender, Bluegiga.BLE.Events.GAP.ModeChangedEventArgs e);
                public class ModeChangedEventArgs : EventArgs {
                    public readonly Byte discover;
                    public readonly Byte connect;
                    public ModeChangedEventArgs(Byte discover, Byte connect) {
                        this.discover = discover;
                        this.connect = connect;
                    }
                }
            
            }
            namespace Hardware {
                public delegate void IOPortStatusEventHandler(object sender, Bluegiga.BLE.Events.Hardware.IOPortStatusEventArgs e);
                public class IOPortStatusEventArgs : EventArgs {
                    public readonly UInt32 timestamp;
                    public readonly Byte port;
                    public readonly Byte irq;
                    public readonly Byte state;
                    public IOPortStatusEventArgs(UInt32 timestamp, Byte port, Byte irq, Byte state) {
                        this.timestamp = timestamp;
                        this.port = port;
                        this.irq = irq;
                        this.state = state;
                    }
                }
            
                public delegate void SoftTimerEventHandler(object sender, Bluegiga.BLE.Events.Hardware.SoftTimerEventArgs e);
                public class SoftTimerEventArgs : EventArgs {
                    public readonly Byte handle;
                    public SoftTimerEventArgs(Byte handle) {
                        this.handle = handle;
                    }
                }
            
                public delegate void ADCResultEventHandler(object sender, Bluegiga.BLE.Events.Hardware.ADCResultEventArgs e);
                public class ADCResultEventArgs : EventArgs {
                    public readonly Byte input;
                    public readonly Int16 value;
                    public ADCResultEventArgs(Byte input, Int16 value) {
                        this.input = input;
                        this.value = value;
                    }
                }
            
                public delegate void AnalogComparatorStatusEventHandler(object sender, Bluegiga.BLE.Events.Hardware.AnalogComparatorStatusEventArgs e);
                public class AnalogComparatorStatusEventArgs : EventArgs {
                    public readonly UInt32 timestamp;
                    public readonly Byte output;
                    public AnalogComparatorStatusEventArgs(UInt32 timestamp, Byte output) {
                        this.timestamp = timestamp;
                        this.output = output;
                    }
                }
            
            }
            namespace DFU {
                public delegate void BootEventHandler(object sender, Bluegiga.BLE.Events.DFU.BootEventArgs e);
                public class BootEventArgs : EventArgs {
                    public readonly UInt32 version;
                    public BootEventArgs(UInt32 version) {
                        this.version = version;
                    }
                }
            
            }
        }

    }

    public class BGLib
    {

        public Byte[] BLECommandSystemReset(Byte boot_in_dfu) {
            return new Byte[] { 0, 1, 0, 0, boot_in_dfu };
        }
        public Byte[] BLECommandSystemHello() {
            return new Byte[] { 0, 0, 0, 1 };
        }
        public Byte[] BLECommandSystemAddressGet() {
            return new Byte[] { 0, 0, 0, 2 };
        }
        public Byte[] BLECommandSystemRegWrite(UInt16 address, Byte value) {
            return new Byte[] { 0, 3, 0, 3, (Byte)(address), (Byte)(address >> 8), value };
        }
        public Byte[] BLECommandSystemRegRead(UInt16 address) {
            return new Byte[] { 0, 2, 0, 4, (Byte)(address), (Byte)(address >> 8) };
        }
        public Byte[] BLECommandSystemGetCounters() {
            return new Byte[] { 0, 0, 0, 5 };
        }
        public Byte[] BLECommandSystemGetConnections() {
            return new Byte[] { 0, 0, 0, 6 };
        }
        public Byte[] BLECommandSystemReadMemory(UInt32 address, Byte length) {
            return new Byte[] { 0, 5, 0, 7, (Byte)(address), (Byte)(address >> 8), (Byte)(address >> 16), (Byte)(address >> 24), length };
        }
        public Byte[] BLECommandSystemGetInfo() {
            return new Byte[] { 0, 0, 0, 8 };
        }
        public Byte[] BLECommandSystemEndpointTX(Byte endpoint, Byte[] data) {
            Byte[] cmd = new Byte[6 + data.Length];
            Array.Copy(new Byte[] { 0, (Byte)(2 + data.Length), 0, 9, endpoint, (Byte)data.Length }, 0, cmd, 0, 6);
            Array.Copy(data, 0, cmd, 6, data.Length);
            return cmd;
        }
        public Byte[] BLECommandSystemWhitelistAppend(Byte[] address, Byte address_type) {
            Byte[] cmd = new Byte[11];
            Array.Copy(new Byte[] { 0, (Byte)(7), 0, 10, 0, 0, 0, 0, 0, 0, address_type }, 0, cmd, 0, 11);
            Array.Copy(address, 0, cmd, 4, 6);
            return cmd;
        }
        public Byte[] BLECommandSystemWhitelistRemove(Byte[] address, Byte address_type) {
            Byte[] cmd = new Byte[11];
            Array.Copy(new Byte[] { 0, (Byte)(7), 0, 11, 0, 0, 0, 0, 0, 0, address_type }, 0, cmd, 0, 11);
            Array.Copy(address, 0, cmd, 4, 6);
            return cmd;
        }
        public Byte[] BLECommandSystemWhitelistClear() {
            return new Byte[] { 0, 0, 0, 12 };
        }
        public Byte[] BLECommandSystemEndpointRX(Byte endpoint, Byte size) {
            return new Byte[] { 0, 2, 0, 13, endpoint, size };
        }
        public Byte[] BLECommandSystemEndpointSetWatermarks(Byte endpoint, Byte rx, Byte tx) {
            return new Byte[] { 0, 3, 0, 14, endpoint, rx, tx };
        }
        public Byte[] BLECommandSystemAesSetkey(Byte[] key) {
            Byte[] cmd = new Byte[5 + key.Length];
            Array.Copy(new Byte[] { 0, (Byte)(1 + key.Length), 0, 15, (Byte)key.Length }, 0, cmd, 0, 5);
            Array.Copy(key, 0, cmd, 5, key.Length);
            return cmd;
        }
        public Byte[] BLECommandSystemAesEncrypt(Byte[] data) {
            Byte[] cmd = new Byte[5 + data.Length];
            Array.Copy(new Byte[] { 0, (Byte)(1 + data.Length), 0, 16, (Byte)data.Length }, 0, cmd, 0, 5);
            Array.Copy(data, 0, cmd, 5, data.Length);
            return cmd;
        }
        public Byte[] BLECommandSystemAesDecrypt(Byte[] data) {
            Byte[] cmd = new Byte[5 + data.Length];
            Array.Copy(new Byte[] { 0, (Byte)(1 + data.Length), 0, 17, (Byte)data.Length }, 0, cmd, 0, 5);
            Array.Copy(data, 0, cmd, 5, data.Length);
            return cmd;
        }
        public Byte[] BLECommandFlashPSDefrag() {
            return new Byte[] { 0, 0, 1, 0 };
        }
        public Byte[] BLECommandFlashPSDump() {
            return new Byte[] { 0, 0, 1, 1 };
        }
        public Byte[] BLECommandFlashPSEraseAll() {
            return new Byte[] { 0, 0, 1, 2 };
        }
        public Byte[] BLECommandFlashPSSave(UInt16 key, Byte[] value) {
            Byte[] cmd = new Byte[7 + value.Length];
            Array.Copy(new Byte[] { 0, (Byte)(3 + value.Length), 1, 3, (Byte)(key), (Byte)(key >> 8), (Byte)value.Length }, 0, cmd, 0, 7);
            Array.Copy(value, 0, cmd, 7, value.Length);
            return cmd;
        }
        public Byte[] BLECommandFlashPSLoad(UInt16 key) {
            return new Byte[] { 0, 2, 1, 4, (Byte)(key), (Byte)(key >> 8) };
        }
        public Byte[] BLECommandFlashPSErase(UInt16 key) {
            return new Byte[] { 0, 2, 1, 5, (Byte)(key), (Byte)(key >> 8) };
        }
        public Byte[] BLECommandFlashErasePage(Byte page) {
            return new Byte[] { 0, 1, 1, 6, page };
        }
        public Byte[] BLECommandFlashWriteData(UInt32 address, Byte[] data) {
            Byte[] cmd = new Byte[9 + data.Length];
            Array.Copy(new Byte[] { 0, (Byte)(5 + data.Length), 1, 7, (Byte)(address), (Byte)(address >> 8), (Byte)(address >> 16), (Byte)(address >> 24), (Byte)data.Length }, 0, cmd, 0, 9);
            Array.Copy(data, 0, cmd, 9, data.Length);
            return cmd;
        }
        public Byte[] BLECommandFlashReadData(UInt32 address, Byte length) {
            return new Byte[] { 0, 5, 1, 8, (Byte)(address), (Byte)(address >> 8), (Byte)(address >> 16), (Byte)(address >> 24), length };
        }
        public Byte[] BLECommandAttributesWrite(UInt16 handle, Byte offset, Byte[] value) {
            Byte[] cmd = new Byte[8 + value.Length];
            Array.Copy(new Byte[] { 0, (Byte)(4 + value.Length), 2, 0, (Byte)(handle), (Byte)(handle >> 8), offset, (Byte)value.Length }, 0, cmd, 0, 8);
            Array.Copy(value, 0, cmd, 8, value.Length);
            return cmd;
        }
        public Byte[] BLECommandAttributesRead(UInt16 handle, UInt16 offset) {
            return new Byte[] { 0, 4, 2, 1, (Byte)(handle), (Byte)(handle >> 8), (Byte)(offset), (Byte)(offset >> 8) };
        }
        public Byte[] BLECommandAttributesReadType(UInt16 handle) {
            return new Byte[] { 0, 2, 2, 2, (Byte)(handle), (Byte)(handle >> 8) };
        }
        public Byte[] BLECommandAttributesUserReadResponse(Byte connection, Byte att_error, Byte[] value) {
            Byte[] cmd = new Byte[7 + value.Length];
            Array.Copy(new Byte[] { 0, (Byte)(3 + value.Length), 2, 3, connection, att_error, (Byte)value.Length }, 0, cmd, 0, 7);
            Array.Copy(value, 0, cmd, 7, value.Length);
            return cmd;
        }
        public Byte[] BLECommandAttributesUserWriteResponse(Byte connection, Byte att_error) {
            return new Byte[] { 0, 2, 2, 4, connection, att_error };
        }
        public Byte[] BLECommandAttributesSend(Byte connection, UInt16 handle, Byte[] value) {
            Byte[] cmd = new Byte[8 + value.Length];
            Array.Copy(new Byte[] { 0, (Byte)(4 + value.Length), 2, 5, connection, (Byte)(handle), (Byte)(handle >> 8), (Byte)value.Length }, 0, cmd, 0, 8);
            Array.Copy(value, 0, cmd, 8, value.Length);
            return cmd;
        }
        public Byte[] BLECommandConnectionDisconnect(Byte connection) {
            return new Byte[] { 0, 1, 3, 0, connection };
        }
        public Byte[] BLECommandConnectionGetRssi(Byte connection) {
            return new Byte[] { 0, 1, 3, 1, connection };
        }
        public Byte[] BLECommandConnectionUpdate(Byte connection, UInt16 interval_min, UInt16 interval_max, UInt16 latency, UInt16 timeout) {
            return new Byte[] { 0, 9, 3, 2, connection, (Byte)(interval_min), (Byte)(interval_min >> 8), (Byte)(interval_max), (Byte)(interval_max >> 8), (Byte)(latency), (Byte)(latency >> 8), (Byte)(timeout), (Byte)(timeout >> 8) };
        }
        public Byte[] BLECommandConnectionVersionUpdate(Byte connection) {
            return new Byte[] { 0, 1, 3, 3, connection };
        }
        public Byte[] BLECommandConnectionChannelMapGet(Byte connection) {
            return new Byte[] { 0, 1, 3, 4, connection };
        }
        public Byte[] BLECommandConnectionChannelMapSet(Byte connection, Byte[] map) {
            Byte[] cmd = new Byte[6 + map.Length];
            Array.Copy(new Byte[] { 0, (Byte)(2 + map.Length), 3, 5, connection, (Byte)map.Length }, 0, cmd, 0, 6);
            Array.Copy(map, 0, cmd, 6, map.Length);
            return cmd;
        }
        public Byte[] BLECommandConnectionFeaturesGet(Byte connection) {
            return new Byte[] { 0, 1, 3, 6, connection };
        }
        public Byte[] BLECommandConnectionGetStatus(Byte connection) {
            return new Byte[] { 0, 1, 3, 7, connection };
        }
        public Byte[] BLECommandConnectionRawTX(Byte connection, Byte[] data) {
            Byte[] cmd = new Byte[6 + data.Length];
            Array.Copy(new Byte[] { 0, (Byte)(2 + data.Length), 3, 8, connection, (Byte)data.Length }, 0, cmd, 0, 6);
            Array.Copy(data, 0, cmd, 6, data.Length);
            return cmd;
        }
        public Byte[] BLECommandATTClientFindByTypeValue(Byte connection, UInt16 start, UInt16 end, UInt16 uuid, Byte[] value) {
            Byte[] cmd = new Byte[12 + value.Length];
            Array.Copy(new Byte[] { 0, (Byte)(8 + value.Length), 4, 0, connection, (Byte)(start), (Byte)(start >> 8), (Byte)(end), (Byte)(end >> 8), (Byte)(uuid), (Byte)(uuid >> 8), (Byte)value.Length }, 0, cmd, 0, 12);
            Array.Copy(value, 0, cmd, 12, value.Length);
            return cmd;
        }
        public Byte[] BLECommandATTClientReadByGroupType(Byte connection, UInt16 start, UInt16 end, Byte[] uuid) {
            Byte[] cmd = new Byte[10 + uuid.Length];
            Array.Copy(new Byte[] { 0, (Byte)(6 + uuid.Length), 4, 1, connection, (Byte)(start), (Byte)(start >> 8), (Byte)(end), (Byte)(end >> 8), (Byte)uuid.Length }, 0, cmd, 0, 10);
            Array.Copy(uuid, 0, cmd, 10, uuid.Length);
            return cmd;
        }
        public Byte[] BLECommandATTClientReadByType(Byte connection, UInt16 start, UInt16 end, Byte[] uuid) {
            Byte[] cmd = new Byte[10 + uuid.Length];
            Array.Copy(new Byte[] { 0, (Byte)(6 + uuid.Length), 4, 2, connection, (Byte)(start), (Byte)(start >> 8), (Byte)(end), (Byte)(end >> 8), (Byte)uuid.Length }, 0, cmd, 0, 10);
            Array.Copy(uuid, 0, cmd, 10, uuid.Length);
            return cmd;
        }
        public Byte[] BLECommandATTClientFindInformation(Byte connection, UInt16 start, UInt16 end) {
            return new Byte[] { 0, 5, 4, 3, connection, (Byte)(start), (Byte)(start >> 8), (Byte)(end), (Byte)(end >> 8) };
        }
        public Byte[] BLECommandATTClientReadByHandle(Byte connection, UInt16 chrhandle) {
            return new Byte[] { 0, 3, 4, 4, connection, (Byte)(chrhandle), (Byte)(chrhandle >> 8) };
        }
        public Byte[] BLECommandATTClientAttributeWrite(Byte connection, UInt16 atthandle, Byte[] data) {
            Byte[] cmd = new Byte[8 + data.Length];
            Array.Copy(new Byte[] { 0, (Byte)(4 + data.Length), 4, 5, connection, (Byte)(atthandle), (Byte)(atthandle >> 8), (Byte)data.Length }, 0, cmd, 0, 8);
            Array.Copy(data, 0, cmd, 8, data.Length);
            return cmd;
        }
        public Byte[] BLECommandATTClientWriteCommand(Byte connection, UInt16 atthandle, Byte[] data) {
            Byte[] cmd = new Byte[8 + data.Length];
            Array.Copy(new Byte[] { 0, (Byte)(4 + data.Length), 4, 6, connection, (Byte)(atthandle), (Byte)(atthandle >> 8), (Byte)data.Length }, 0, cmd, 0, 8);
            Array.Copy(data, 0, cmd, 8, data.Length);
            return cmd;
        }
        public Byte[] BLECommandATTClientIndicateConfirm(Byte connection) {
            return new Byte[] { 0, 1, 4, 7, connection };
        }
        public Byte[] BLECommandATTClientReadLong(Byte connection, UInt16 chrhandle) {
            return new Byte[] { 0, 3, 4, 8, connection, (Byte)(chrhandle), (Byte)(chrhandle >> 8) };
        }
        public Byte[] BLECommandATTClientPrepareWrite(Byte connection, UInt16 atthandle, UInt16 offset, Byte[] data) {
            Byte[] cmd = new Byte[10 + data.Length];
            Array.Copy(new Byte[] { 0, (Byte)(6 + data.Length), 4, 9, connection, (Byte)(atthandle), (Byte)(atthandle >> 8), (Byte)(offset), (Byte)(offset >> 8), (Byte)data.Length }, 0, cmd, 0, 10);
            Array.Copy(data, 0, cmd, 10, data.Length);
            return cmd;
        }
        public Byte[] BLECommandATTClientExecuteWrite(Byte connection, Byte commit) {
            return new Byte[] { 0, 2, 4, 10, connection, commit };
        }
        public Byte[] BLECommandATTClientReadMultiple(Byte connection, Byte[] handles) {
            Byte[] cmd = new Byte[6 + handles.Length];
            Array.Copy(new Byte[] { 0, (Byte)(2 + handles.Length), 4, 11, connection, (Byte)handles.Length }, 0, cmd, 0, 6);
            Array.Copy(handles, 0, cmd, 6, handles.Length);
            return cmd;
        }
        public Byte[] BLECommandSMEncryptStart(Byte handle, Byte bonding) {
            return new Byte[] { 0, 2, 5, 0, handle, bonding };
        }
        public Byte[] BLECommandSMSetBondableMode(Byte bondable) {
            return new Byte[] { 0, 1, 5, 1, bondable };
        }
        public Byte[] BLECommandSMDeleteBonding(Byte handle) {
            return new Byte[] { 0, 1, 5, 2, handle };
        }
        public Byte[] BLECommandSMSetParameters(Byte mitm, Byte min_key_size, Byte io_capabilities) {
            return new Byte[] { 0, 3, 5, 3, mitm, min_key_size, io_capabilities };
        }
        public Byte[] BLECommandSMPasskeyEntry(Byte handle, UInt32 passkey) {
            return new Byte[] { 0, 5, 5, 4, handle, (Byte)(passkey), (Byte)(passkey >> 8), (Byte)(passkey >> 16), (Byte)(passkey >> 24) };
        }
        public Byte[] BLECommandSMGetBonds() {
            return new Byte[] { 0, 0, 5, 5 };
        }
        public Byte[] BLECommandSMSetOobData(Byte[] oob) {
            Byte[] cmd = new Byte[5 + oob.Length];
            Array.Copy(new Byte[] { 0, (Byte)(1 + oob.Length), 5, 6, (Byte)oob.Length }, 0, cmd, 0, 5);
            Array.Copy(oob, 0, cmd, 5, oob.Length);
            return cmd;
        }
        public Byte[] BLECommandSMWhitelistBonds() {
            return new Byte[] { 0, 0, 5, 7 };
        }
        public Byte[] BLECommandGAPSetPrivacyFlags(Byte peripheral_privacy, Byte central_privacy) {
            return new Byte[] { 0, 2, 6, 0, peripheral_privacy, central_privacy };
        }
        public Byte[] BLECommandGAPSetMode(Byte discover, Byte connect) {
            return new Byte[] { 0, 2, 6, 1, discover, connect };
        }
        public Byte[] BLECommandGAPDiscover(Byte mode) {
            return new Byte[] { 0, 1, 6, 2, mode };
        }
        public Byte[] BLECommandGAPConnectDirect(Byte[] address, Byte addr_type, UInt16 conn_interval_min, UInt16 conn_interval_max, UInt16 timeout, UInt16 latency) {
            Byte[] cmd = new Byte[19];
            Array.Copy(new Byte[] { 0, (Byte)(15), 6, 3, 0, 0, 0, 0, 0, 0, addr_type, (Byte)(conn_interval_min), (Byte)(conn_interval_min >> 8), (Byte)(conn_interval_max), (Byte)(conn_interval_max >> 8), (Byte)(timeout), (Byte)(timeout >> 8), (Byte)(latency), (Byte)(latency >> 8) }, 0, cmd, 0, 19);
            Array.Copy(address, 0, cmd, 4, 6);
            return cmd;
        }
        public Byte[] BLECommandGAPEndProcedure() {
            return new Byte[] { 0, 0, 6, 4 };
        }
        public Byte[] BLECommandGAPConnectSelective(UInt16 conn_interval_min, UInt16 conn_interval_max, UInt16 timeout, UInt16 latency) {
            return new Byte[] { 0, 8, 6, 5, (Byte)(conn_interval_min), (Byte)(conn_interval_min >> 8), (Byte)(conn_interval_max), (Byte)(conn_interval_max >> 8), (Byte)(timeout), (Byte)(timeout >> 8), (Byte)(latency), (Byte)(latency >> 8) };
        }
        public Byte[] BLECommandGAPSetFiltering(Byte scan_policy, Byte adv_policy, Byte scan_duplicate_filtering) {
            return new Byte[] { 0, 3, 6, 6, scan_policy, adv_policy, scan_duplicate_filtering };
        }
        public Byte[] BLECommandGAPSetScanParameters(UInt16 scan_interval, UInt16 scan_window, Byte active) {
            return new Byte[] { 0, 5, 6, 7, (Byte)(scan_interval), (Byte)(scan_interval >> 8), (Byte)(scan_window), (Byte)(scan_window >> 8), active };
        }
        public Byte[] BLECommandGAPSetAdvParameters(UInt16 adv_interval_min, UInt16 adv_interval_max, Byte adv_channels) {
            return new Byte[] { 0, 5, 6, 8, (Byte)(adv_interval_min), (Byte)(adv_interval_min >> 8), (Byte)(adv_interval_max), (Byte)(adv_interval_max >> 8), adv_channels };
        }
        public Byte[] BLECommandGAPSetAdvData(Byte set_scanrsp, Byte[] adv_data) {
            Byte[] cmd = new Byte[6 + adv_data.Length];
            Array.Copy(new Byte[] { 0, (Byte)(2 + adv_data.Length), 6, 9, set_scanrsp, (Byte)adv_data.Length }, 0, cmd, 0, 6);
            Array.Copy(adv_data, 0, cmd, 6, adv_data.Length);
            return cmd;
        }
        public Byte[] BLECommandGAPSetDirectedConnectableMode(Byte[] address, Byte addr_type) {
            Byte[] cmd = new Byte[11];
            Array.Copy(new Byte[] { 0, (Byte)(7), 6, 10, 0, 0, 0, 0, 0, 0, addr_type }, 0, cmd, 0, 11);
            Array.Copy(address, 0, cmd, 4, 6);
            return cmd;
        }
        public Byte[] BLECommandHardwareIOPortConfigIrq(Byte port, Byte enable_bits, Byte falling_edge) {
            return new Byte[] { 0, 3, 7, 0, port, enable_bits, falling_edge };
        }
        public Byte[] BLECommandHardwareSetSoftTimer(UInt32 time, Byte handle, Byte single_shot) {
            return new Byte[] { 0, 6, 7, 1, (Byte)(time), (Byte)(time >> 8), (Byte)(time >> 16), (Byte)(time >> 24), handle, single_shot };
        }
        public Byte[] BLECommandHardwareADCRead(Byte input, Byte decimation, Byte reference_selection) {
            return new Byte[] { 0, 3, 7, 2, input, decimation, reference_selection };
        }
        public Byte[] BLECommandHardwareIOPortConfigDirection(Byte port, Byte direction) {
            return new Byte[] { 0, 2, 7, 3, port, direction };
        }
        public Byte[] BLECommandHardwareIOPortConfigFunction(Byte port, Byte function) {
            return new Byte[] { 0, 2, 7, 4, port, function };
        }
        public Byte[] BLECommandHardwareIOPortConfigPull(Byte port, Byte tristate_mask, Byte pull_up) {
            return new Byte[] { 0, 3, 7, 5, port, tristate_mask, pull_up };
        }
        public Byte[] BLECommandHardwareIOPortWrite(Byte port, Byte mask, Byte data) {
            return new Byte[] { 0, 3, 7, 6, port, mask, data };
        }
        public Byte[] BLECommandHardwareIOPortRead(Byte port, Byte mask) {
            return new Byte[] { 0, 2, 7, 7, port, mask };
        }
        public Byte[] BLECommandHardwareSPIConfig(Byte channel, Byte polarity, Byte phase, Byte bit_order, Byte baud_e, Byte baud_m) {
            return new Byte[] { 0, 6, 7, 8, channel, polarity, phase, bit_order, baud_e, baud_m };
        }
        public Byte[] BLECommandHardwareSPITransfer(Byte channel, Byte[] data) {
            Byte[] cmd = new Byte[6 + data.Length];
            Array.Copy(new Byte[] { 0, (Byte)(2 + data.Length), 7, 9, channel, (Byte)data.Length }, 0, cmd, 0, 6);
            Array.Copy(data, 0, cmd, 6, data.Length);
            return cmd;
        }
        public Byte[] BLECommandHardwareI2CRead(Byte address, Byte stop, Byte length) {
            return new Byte[] { 0, 3, 7, 10, address, stop, length };
        }
        public Byte[] BLECommandHardwareI2CWrite(Byte address, Byte stop, Byte[] data) {
            Byte[] cmd = new Byte[7 + data.Length];
            Array.Copy(new Byte[] { 0, (Byte)(3 + data.Length), 7, 11, address, stop, (Byte)data.Length }, 0, cmd, 0, 7);
            Array.Copy(data, 0, cmd, 7, data.Length);
            return cmd;
        }
        public Byte[] BLECommandHardwareSetTxpower(Byte power) {
            return new Byte[] { 0, 1, 7, 12, power };
        }
        public Byte[] BLECommandHardwareTimerComparator(Byte timer, Byte channel, Byte mode, UInt16 comparator_value) {
            return new Byte[] { 0, 5, 7, 13, timer, channel, mode, (Byte)(comparator_value), (Byte)(comparator_value >> 8) };
        }
        public Byte[] BLECommandHardwareIOPortIrqEnable(Byte port, Byte enable_bits) {
            return new Byte[] { 0, 2, 7, 14, port, enable_bits };
        }
        public Byte[] BLECommandHardwareIOPortIrqDirection(Byte port, Byte falling_edge) {
            return new Byte[] { 0, 2, 7, 15, port, falling_edge };
        }
        public Byte[] BLECommandHardwareAnalogComparatorEnable(Byte enable) {
            return new Byte[] { 0, 1, 7, 16, enable };
        }
        public Byte[] BLECommandHardwareAnalogComparatorRead() {
            return new Byte[] { 0, 0, 7, 17 };
        }
        public Byte[] BLECommandHardwareAnalogComparatorConfigIrq(Byte enabled) {
            return new Byte[] { 0, 1, 7, 18, enabled };
        }
        public Byte[] BLECommandHardwareSetRxgain(Byte gain) {
            return new Byte[] { 0, 1, 7, 19, gain };
        }
        public Byte[] BLECommandHardwareUsbEnable(Byte enable) {
            return new Byte[] { 0, 1, 7, 20, enable };
        }
        public Byte[] BLECommandTestPHYTX(Byte channel, Byte length, Byte type) {
            return new Byte[] { 0, 3, 8, 0, channel, length, type };
        }
        public Byte[] BLECommandTestPHYRX(Byte channel) {
            return new Byte[] { 0, 1, 8, 1, channel };
        }
        public Byte[] BLECommandTestPHYEnd() {
            return new Byte[] { 0, 0, 8, 2 };
        }
        public Byte[] BLECommandTestPHYReset() {
            return new Byte[] { 0, 0, 8, 3 };
        }
        public Byte[] BLECommandTestGetChannelMap() {
            return new Byte[] { 0, 0, 8, 4 };
        }
        public Byte[] BLECommandTestDebug(Byte[] input) {
            Byte[] cmd = new Byte[5 + input.Length];
            Array.Copy(new Byte[] { 0, (Byte)(1 + input.Length), 8, 5, (Byte)input.Length }, 0, cmd, 0, 5);
            Array.Copy(input, 0, cmd, 5, input.Length);
            return cmd;
        }
        public Byte[] BLECommandTestChannelMode(Byte mode) {
            return new Byte[] { 0, 1, 8, 6, mode };
        }
        public Byte[] BLECommandDFUReset(Byte dfu) {
            return new Byte[] { 0, 1, 9, 0, dfu };
        }
        public Byte[] BLECommandDFUFlashSetAddress(UInt32 address) {
            return new Byte[] { 0, 4, 9, 1, (Byte)(address), (Byte)(address >> 8), (Byte)(address >> 16), (Byte)(address >> 24) };
        }
        public Byte[] BLECommandDFUFlashUpload(Byte[] data) {
            Byte[] cmd = new Byte[5 + data.Length];
            Array.Copy(new Byte[] { 0, (Byte)(1 + data.Length), 9, 2, (Byte)data.Length }, 0, cmd, 0, 5);
            Array.Copy(data, 0, cmd, 5, data.Length);
            return cmd;
        }
        public Byte[] BLECommandDFUFlashUploadFinish() {
            return new Byte[] { 0, 0, 9, 3 };
        }

        public event Bluegiga.BLE.Responses.System.ResetEventHandler BLEResponseSystemReset;
        public event Bluegiga.BLE.Responses.System.HelloEventHandler BLEResponseSystemHello;
        public event Bluegiga.BLE.Responses.System.AddressGetEventHandler BLEResponseSystemAddressGet;
        public event Bluegiga.BLE.Responses.System.RegWriteEventHandler BLEResponseSystemRegWrite;
        public event Bluegiga.BLE.Responses.System.RegReadEventHandler BLEResponseSystemRegRead;
        public event Bluegiga.BLE.Responses.System.GetCountersEventHandler BLEResponseSystemGetCounters;
        public event Bluegiga.BLE.Responses.System.GetConnectionsEventHandler BLEResponseSystemGetConnections;
        public event Bluegiga.BLE.Responses.System.ReadMemoryEventHandler BLEResponseSystemReadMemory;
        public event Bluegiga.BLE.Responses.System.GetInfoEventHandler BLEResponseSystemGetInfo;
        public event Bluegiga.BLE.Responses.System.EndpointTXEventHandler BLEResponseSystemEndpointTX;
        public event Bluegiga.BLE.Responses.System.WhitelistAppendEventHandler BLEResponseSystemWhitelistAppend;
        public event Bluegiga.BLE.Responses.System.WhitelistRemoveEventHandler BLEResponseSystemWhitelistRemove;
        public event Bluegiga.BLE.Responses.System.WhitelistClearEventHandler BLEResponseSystemWhitelistClear;
        public event Bluegiga.BLE.Responses.System.EndpointRXEventHandler BLEResponseSystemEndpointRX;
        public event Bluegiga.BLE.Responses.System.EndpointSetWatermarksEventHandler BLEResponseSystemEndpointSetWatermarks;
        public event Bluegiga.BLE.Responses.System.AesSetkeyEventHandler BLEResponseSystemAesSetkey;
        public event Bluegiga.BLE.Responses.System.AesEncryptEventHandler BLEResponseSystemAesEncrypt;
        public event Bluegiga.BLE.Responses.System.AesDecryptEventHandler BLEResponseSystemAesDecrypt;
        public event Bluegiga.BLE.Responses.Flash.PSDefragEventHandler BLEResponseFlashPSDefrag;
        public event Bluegiga.BLE.Responses.Flash.PSDumpEventHandler BLEResponseFlashPSDump;
        public event Bluegiga.BLE.Responses.Flash.PSEraseAllEventHandler BLEResponseFlashPSEraseAll;
        public event Bluegiga.BLE.Responses.Flash.PSSaveEventHandler BLEResponseFlashPSSave;
        public event Bluegiga.BLE.Responses.Flash.PSLoadEventHandler BLEResponseFlashPSLoad;
        public event Bluegiga.BLE.Responses.Flash.PSEraseEventHandler BLEResponseFlashPSErase;
        public event Bluegiga.BLE.Responses.Flash.ErasePageEventHandler BLEResponseFlashErasePage;
        public event Bluegiga.BLE.Responses.Flash.WriteDataEventHandler BLEResponseFlashWriteData;
        public event Bluegiga.BLE.Responses.Flash.ReadDataEventHandler BLEResponseFlashReadData;
        public event Bluegiga.BLE.Responses.Attributes.WriteEventHandler BLEResponseAttributesWrite;
        public event Bluegiga.BLE.Responses.Attributes.ReadEventHandler BLEResponseAttributesRead;
        public event Bluegiga.BLE.Responses.Attributes.ReadTypeEventHandler BLEResponseAttributesReadType;
        public event Bluegiga.BLE.Responses.Attributes.UserReadResponseEventHandler BLEResponseAttributesUserReadResponse;
        public event Bluegiga.BLE.Responses.Attributes.UserWriteResponseEventHandler BLEResponseAttributesUserWriteResponse;
        public event Bluegiga.BLE.Responses.Attributes.SendEventHandler BLEResponseAttributesSend;
        public event Bluegiga.BLE.Responses.Connection.DisconnectEventHandler BLEResponseConnectionDisconnect;
        public event Bluegiga.BLE.Responses.Connection.GetRssiEventHandler BLEResponseConnectionGetRssi;
        public event Bluegiga.BLE.Responses.Connection.UpdateEventHandler BLEResponseConnectionUpdate;
        public event Bluegiga.BLE.Responses.Connection.VersionUpdateEventHandler BLEResponseConnectionVersionUpdate;
        public event Bluegiga.BLE.Responses.Connection.ChannelMapGetEventHandler BLEResponseConnectionChannelMapGet;
        public event Bluegiga.BLE.Responses.Connection.ChannelMapSetEventHandler BLEResponseConnectionChannelMapSet;
        public event Bluegiga.BLE.Responses.Connection.FeaturesGetEventHandler BLEResponseConnectionFeaturesGet;
        public event Bluegiga.BLE.Responses.Connection.GetStatusEventHandler BLEResponseConnectionGetStatus;
        public event Bluegiga.BLE.Responses.Connection.RawTXEventHandler BLEResponseConnectionRawTX;
        public event Bluegiga.BLE.Responses.ATTClient.FindByTypeValueEventHandler BLEResponseATTClientFindByTypeValue;
        public event Bluegiga.BLE.Responses.ATTClient.ReadByGroupTypeEventHandler BLEResponseATTClientReadByGroupType;
        public event Bluegiga.BLE.Responses.ATTClient.ReadByTypeEventHandler BLEResponseATTClientReadByType;
        public event Bluegiga.BLE.Responses.ATTClient.FindInformationEventHandler BLEResponseATTClientFindInformation;
        public event Bluegiga.BLE.Responses.ATTClient.ReadByHandleEventHandler BLEResponseATTClientReadByHandle;
        public event Bluegiga.BLE.Responses.ATTClient.AttributeWriteEventHandler BLEResponseATTClientAttributeWrite;
        public event Bluegiga.BLE.Responses.ATTClient.WriteCommandEventHandler BLEResponseATTClientWriteCommand;
        public event Bluegiga.BLE.Responses.ATTClient.IndicateConfirmEventHandler BLEResponseATTClientIndicateConfirm;
        public event Bluegiga.BLE.Responses.ATTClient.ReadLongEventHandler BLEResponseATTClientReadLong;
        public event Bluegiga.BLE.Responses.ATTClient.PrepareWriteEventHandler BLEResponseATTClientPrepareWrite;
        public event Bluegiga.BLE.Responses.ATTClient.ExecuteWriteEventHandler BLEResponseATTClientExecuteWrite;
        public event Bluegiga.BLE.Responses.ATTClient.ReadMultipleEventHandler BLEResponseATTClientReadMultiple;
        public event Bluegiga.BLE.Responses.SM.EncryptStartEventHandler BLEResponseSMEncryptStart;
        public event Bluegiga.BLE.Responses.SM.SetBondableModeEventHandler BLEResponseSMSetBondableMode;
        public event Bluegiga.BLE.Responses.SM.DeleteBondingEventHandler BLEResponseSMDeleteBonding;
        public event Bluegiga.BLE.Responses.SM.SetParametersEventHandler BLEResponseSMSetParameters;
        public event Bluegiga.BLE.Responses.SM.PasskeyEntryEventHandler BLEResponseSMPasskeyEntry;
        public event Bluegiga.BLE.Responses.SM.GetBondsEventHandler BLEResponseSMGetBonds;
        public event Bluegiga.BLE.Responses.SM.SetOobDataEventHandler BLEResponseSMSetOobData;
        public event Bluegiga.BLE.Responses.SM.WhitelistBondsEventHandler BLEResponseSMWhitelistBonds;
        public event Bluegiga.BLE.Responses.GAP.SetPrivacyFlagsEventHandler BLEResponseGAPSetPrivacyFlags;
        public event Bluegiga.BLE.Responses.GAP.SetModeEventHandler BLEResponseGAPSetMode;
        public event Bluegiga.BLE.Responses.GAP.DiscoverEventHandler BLEResponseGAPDiscover;
        public event Bluegiga.BLE.Responses.GAP.ConnectDirectEventHandler BLEResponseGAPConnectDirect;
        public event Bluegiga.BLE.Responses.GAP.EndProcedureEventHandler BLEResponseGAPEndProcedure;
        public event Bluegiga.BLE.Responses.GAP.ConnectSelectiveEventHandler BLEResponseGAPConnectSelective;
        public event Bluegiga.BLE.Responses.GAP.SetFilteringEventHandler BLEResponseGAPSetFiltering;
        public event Bluegiga.BLE.Responses.GAP.SetScanParametersEventHandler BLEResponseGAPSetScanParameters;
        public event Bluegiga.BLE.Responses.GAP.SetAdvParametersEventHandler BLEResponseGAPSetAdvParameters;
        public event Bluegiga.BLE.Responses.GAP.SetAdvDataEventHandler BLEResponseGAPSetAdvData;
        public event Bluegiga.BLE.Responses.GAP.SetDirectedConnectableModeEventHandler BLEResponseGAPSetDirectedConnectableMode;
        public event Bluegiga.BLE.Responses.Hardware.IOPortConfigIrqEventHandler BLEResponseHardwareIOPortConfigIrq;
        public event Bluegiga.BLE.Responses.Hardware.SetSoftTimerEventHandler BLEResponseHardwareSetSoftTimer;
        public event Bluegiga.BLE.Responses.Hardware.ADCReadEventHandler BLEResponseHardwareADCRead;
        public event Bluegiga.BLE.Responses.Hardware.IOPortConfigDirectionEventHandler BLEResponseHardwareIOPortConfigDirection;
        public event Bluegiga.BLE.Responses.Hardware.IOPortConfigFunctionEventHandler BLEResponseHardwareIOPortConfigFunction;
        public event Bluegiga.BLE.Responses.Hardware.IOPortConfigPullEventHandler BLEResponseHardwareIOPortConfigPull;
        public event Bluegiga.BLE.Responses.Hardware.IOPortWriteEventHandler BLEResponseHardwareIOPortWrite;
        public event Bluegiga.BLE.Responses.Hardware.IOPortReadEventHandler BLEResponseHardwareIOPortRead;
        public event Bluegiga.BLE.Responses.Hardware.SPIConfigEventHandler BLEResponseHardwareSPIConfig;
        public event Bluegiga.BLE.Responses.Hardware.SPITransferEventHandler BLEResponseHardwareSPITransfer;
        public event Bluegiga.BLE.Responses.Hardware.I2CReadEventHandler BLEResponseHardwareI2CRead;
        public event Bluegiga.BLE.Responses.Hardware.I2CWriteEventHandler BLEResponseHardwareI2CWrite;
        public event Bluegiga.BLE.Responses.Hardware.SetTxpowerEventHandler BLEResponseHardwareSetTxpower;
        public event Bluegiga.BLE.Responses.Hardware.TimerComparatorEventHandler BLEResponseHardwareTimerComparator;
        public event Bluegiga.BLE.Responses.Hardware.IOPortIrqEnableEventHandler BLEResponseHardwareIOPortIrqEnable;
        public event Bluegiga.BLE.Responses.Hardware.IOPortIrqDirectionEventHandler BLEResponseHardwareIOPortIrqDirection;
        public event Bluegiga.BLE.Responses.Hardware.AnalogComparatorEnableEventHandler BLEResponseHardwareAnalogComparatorEnable;
        public event Bluegiga.BLE.Responses.Hardware.AnalogComparatorReadEventHandler BLEResponseHardwareAnalogComparatorRead;
        public event Bluegiga.BLE.Responses.Hardware.AnalogComparatorConfigIrqEventHandler BLEResponseHardwareAnalogComparatorConfigIrq;
        public event Bluegiga.BLE.Responses.Hardware.SetRxgainEventHandler BLEResponseHardwareSetRxgain;
        public event Bluegiga.BLE.Responses.Hardware.UsbEnableEventHandler BLEResponseHardwareUsbEnable;
        public event Bluegiga.BLE.Responses.Test.PHYTXEventHandler BLEResponseTestPHYTX;
        public event Bluegiga.BLE.Responses.Test.PHYRXEventHandler BLEResponseTestPHYRX;
        public event Bluegiga.BLE.Responses.Test.PHYEndEventHandler BLEResponseTestPHYEnd;
        public event Bluegiga.BLE.Responses.Test.PHYResetEventHandler BLEResponseTestPHYReset;
        public event Bluegiga.BLE.Responses.Test.GetChannelMapEventHandler BLEResponseTestGetChannelMap;
        public event Bluegiga.BLE.Responses.Test.DebugEventHandler BLEResponseTestDebug;
        public event Bluegiga.BLE.Responses.Test.ChannelModeEventHandler BLEResponseTestChannelMode;
        public event Bluegiga.BLE.Responses.DFU.ResetEventHandler BLEResponseDFUReset;
        public event Bluegiga.BLE.Responses.DFU.FlashSetAddressEventHandler BLEResponseDFUFlashSetAddress;
        public event Bluegiga.BLE.Responses.DFU.FlashUploadEventHandler BLEResponseDFUFlashUpload;
        public event Bluegiga.BLE.Responses.DFU.FlashUploadFinishEventHandler BLEResponseDFUFlashUploadFinish;

        public event Bluegiga.BLE.Events.System.BootEventHandler BLEEventSystemBoot;
        public event Bluegiga.BLE.Events.System.DebugEventHandler BLEEventSystemDebug;
        public event Bluegiga.BLE.Events.System.EndpointWatermarkRXEventHandler BLEEventSystemEndpointWatermarkRX;
        public event Bluegiga.BLE.Events.System.EndpointWatermarkTXEventHandler BLEEventSystemEndpointWatermarkTX;
        public event Bluegiga.BLE.Events.System.ScriptFailureEventHandler BLEEventSystemScriptFailure;
        public event Bluegiga.BLE.Events.System.NoLicenseKeyEventHandler BLEEventSystemNoLicenseKey;
        public event Bluegiga.BLE.Events.System.ProtocolErrorEventHandler BLEEventSystemProtocolError;
        public event Bluegiga.BLE.Events.Flash.PSKeyEventHandler BLEEventFlashPSKey;
        public event Bluegiga.BLE.Events.Attributes.ValueEventHandler BLEEventAttributesValue;
        public event Bluegiga.BLE.Events.Attributes.UserReadRequestEventHandler BLEEventAttributesUserReadRequest;
        public event Bluegiga.BLE.Events.Attributes.StatusEventHandler BLEEventAttributesStatus;
        public event Bluegiga.BLE.Events.Connection.StatusEventHandler BLEEventConnectionStatus;
        public event Bluegiga.BLE.Events.Connection.VersionIndEventHandler BLEEventConnectionVersionInd;
        public event Bluegiga.BLE.Events.Connection.FeatureIndEventHandler BLEEventConnectionFeatureInd;
        public event Bluegiga.BLE.Events.Connection.RawRXEventHandler BLEEventConnectionRawRX;
        public event Bluegiga.BLE.Events.Connection.DisconnectedEventHandler BLEEventConnectionDisconnected;
        public event Bluegiga.BLE.Events.ATTClient.IndicatedEventHandler BLEEventATTClientIndicated;
        public event Bluegiga.BLE.Events.ATTClient.ProcedureCompletedEventHandler BLEEventATTClientProcedureCompleted;
        public event Bluegiga.BLE.Events.ATTClient.GroupFoundEventHandler BLEEventATTClientGroupFound;
        public event Bluegiga.BLE.Events.ATTClient.AttributeFoundEventHandler BLEEventATTClientAttributeFound;
        public event Bluegiga.BLE.Events.ATTClient.FindInformationFoundEventHandler BLEEventATTClientFindInformationFound;
        public event Bluegiga.BLE.Events.ATTClient.AttributeValueEventHandler BLEEventATTClientAttributeValue;
        public event Bluegiga.BLE.Events.ATTClient.ReadMultipleResponseEventHandler BLEEventATTClientReadMultipleResponse;
        public event Bluegiga.BLE.Events.SM.SMPDataEventHandler BLEEventSMSMPData;
        public event Bluegiga.BLE.Events.SM.BondingFailEventHandler BLEEventSMBondingFail;
        public event Bluegiga.BLE.Events.SM.PasskeyDisplayEventHandler BLEEventSMPasskeyDisplay;
        public event Bluegiga.BLE.Events.SM.PasskeyRequestEventHandler BLEEventSMPasskeyRequest;
        public event Bluegiga.BLE.Events.SM.BondStatusEventHandler BLEEventSMBondStatus;
        public event Bluegiga.BLE.Events.GAP.ScanResponseEventHandler BLEEventGAPScanResponse;
        public event Bluegiga.BLE.Events.GAP.ModeChangedEventHandler BLEEventGAPModeChanged;
        public event Bluegiga.BLE.Events.Hardware.IOPortStatusEventHandler BLEEventHardwareIOPortStatus;
        public event Bluegiga.BLE.Events.Hardware.SoftTimerEventHandler BLEEventHardwareSoftTimer;
        public event Bluegiga.BLE.Events.Hardware.ADCResultEventHandler BLEEventHardwareADCResult;
        public event Bluegiga.BLE.Events.Hardware.AnalogComparatorStatusEventHandler BLEEventHardwareAnalogComparatorStatus;
        public event Bluegiga.BLE.Events.DFU.BootEventHandler BLEEventDFUBoot;

        private Byte[] bgapiRXBuffer = new Byte[65];
        private int bgapiRXBufferPos = 0;
        private int bgapiRXDataLen = 0;

        private Boolean bgapiPacketMode = false;

        private Boolean parserBusy = false;

        public void SetBusy(Boolean isBusy) {
            this.parserBusy = isBusy;
        }

        public Boolean IsBusy() {
            return parserBusy;
        }

        public void SetPacketMode(Boolean packetMode) {
            this.bgapiPacketMode = packetMode;
        }

        public UInt16 Parse(Byte ch) {
            /*#ifdef DEBUG
                // DEBUG: output hex value of incoming character
                if (ch < 16) Serial.write(0x30);    // leading '0'
                Serial.print(ch, HEX);              // actual hex value
                Serial.write(0x20);                 // trailing ' '
            #endif*/

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
                        if (bgapiRXBuffer[2] == 0) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseSystemReset != null) {
                                    BLEResponseSystemReset(this, new Bluegiga.BLE.Responses.System.ResetEventArgs(
                                    ));
                                }
                                SetBusy(false);
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseSystemHello != null) {
                                    BLEResponseSystemHello(this, new Bluegiga.BLE.Responses.System.HelloEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseSystemAddressGet != null) {
                                    BLEResponseSystemAddressGet(this, new Bluegiga.BLE.Responses.System.AddressGetEventArgs(
                                        (Byte[])(bgapiRXBuffer.Skip(4).Take(6).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseSystemRegWrite != null) {
                                    BLEResponseSystemRegWrite(this, new Bluegiga.BLE.Responses.System.RegWriteEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseSystemRegRead != null) {
                                    BLEResponseSystemRegRead(this, new Bluegiga.BLE.Responses.System.RegReadEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseSystemGetCounters != null) {
                                    BLEResponseSystemGetCounters(this, new Bluegiga.BLE.Responses.System.GetCountersEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5],
                                        bgapiRXBuffer[6],
                                        bgapiRXBuffer[7],
                                        bgapiRXBuffer[8]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseSystemGetConnections != null) {
                                    BLEResponseSystemGetConnections(this, new Bluegiga.BLE.Responses.System.GetConnectionsEventArgs(
                                        bgapiRXBuffer[4]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEResponseSystemReadMemory != null) {
                                    BLEResponseSystemReadMemory(this, new Bluegiga.BLE.Responses.System.ReadMemoryEventArgs(
                                        (UInt32)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8) + (bgapiRXBuffer[5] << 16) + (bgapiRXBuffer[5] << 24)),
                                        (Byte[])(bgapiRXBuffer.Skip(9).Take(bgapiRXBuffer[8]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 8)
                            {
                                if (BLEResponseSystemGetInfo != null) {
                                    BLEResponseSystemGetInfo(this, new Bluegiga.BLE.Responses.System.GetInfoEventArgs(
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
                            else if (bgapiRXBuffer[3] == 9)
                            {
                                if (BLEResponseSystemEndpointTX != null) {
                                    BLEResponseSystemEndpointTX(this, new Bluegiga.BLE.Responses.System.EndpointTXEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 10)
                            {
                                if (BLEResponseSystemWhitelistAppend != null) {
                                    BLEResponseSystemWhitelistAppend(this, new Bluegiga.BLE.Responses.System.WhitelistAppendEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 11)
                            {
                                if (BLEResponseSystemWhitelistRemove != null) {
                                    BLEResponseSystemWhitelistRemove(this, new Bluegiga.BLE.Responses.System.WhitelistRemoveEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 12)
                            {
                                if (BLEResponseSystemWhitelistClear != null) {
                                    BLEResponseSystemWhitelistClear(this, new Bluegiga.BLE.Responses.System.WhitelistClearEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 13)
                            {
                                if (BLEResponseSystemEndpointRX != null) {
                                    BLEResponseSystemEndpointRX(this, new Bluegiga.BLE.Responses.System.EndpointRXEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 14)
                            {
                                if (BLEResponseSystemEndpointSetWatermarks != null) {
                                    BLEResponseSystemEndpointSetWatermarks(this, new Bluegiga.BLE.Responses.System.EndpointSetWatermarksEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 15)
                            {
                                if (BLEResponseSystemAesSetkey != null) {
                                    BLEResponseSystemAesSetkey(this, new Bluegiga.BLE.Responses.System.AesSetkeyEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 16)
                            {
                                if (BLEResponseSystemAesEncrypt != null) {
                                    BLEResponseSystemAesEncrypt(this, new Bluegiga.BLE.Responses.System.AesEncryptEventArgs(
                                        (Byte[])(bgapiRXBuffer.Skip(5).Take(bgapiRXBuffer[4]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 17)
                            {
                                if (BLEResponseSystemAesDecrypt != null) {
                                    BLEResponseSystemAesDecrypt(this, new Bluegiga.BLE.Responses.System.AesDecryptEventArgs(
                                        (Byte[])(bgapiRXBuffer.Skip(5).Take(bgapiRXBuffer[4]).ToArray())
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 1) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseFlashPSDefrag != null) {
                                    BLEResponseFlashPSDefrag(this, new Bluegiga.BLE.Responses.Flash.PSDefragEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseFlashPSDump != null) {
                                    BLEResponseFlashPSDump(this, new Bluegiga.BLE.Responses.Flash.PSDumpEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseFlashPSEraseAll != null) {
                                    BLEResponseFlashPSEraseAll(this, new Bluegiga.BLE.Responses.Flash.PSEraseAllEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseFlashPSSave != null) {
                                    BLEResponseFlashPSSave(this, new Bluegiga.BLE.Responses.Flash.PSSaveEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseFlashPSLoad != null) {
                                    BLEResponseFlashPSLoad(this, new Bluegiga.BLE.Responses.Flash.PSLoadEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseFlashPSErase != null) {
                                    BLEResponseFlashPSErase(this, new Bluegiga.BLE.Responses.Flash.PSEraseEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseFlashErasePage != null) {
                                    BLEResponseFlashErasePage(this, new Bluegiga.BLE.Responses.Flash.ErasePageEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEResponseFlashWriteData != null) {
                                    BLEResponseFlashWriteData(this, new Bluegiga.BLE.Responses.Flash.WriteDataEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 8)
                            {
                                if (BLEResponseFlashReadData != null) {
                                    BLEResponseFlashReadData(this, new Bluegiga.BLE.Responses.Flash.ReadDataEventArgs(
                                        (Byte[])(bgapiRXBuffer.Skip(5).Take(bgapiRXBuffer[4]).ToArray())
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 2) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseAttributesWrite != null) {
                                    BLEResponseAttributesWrite(this, new Bluegiga.BLE.Responses.Attributes.WriteEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseAttributesRead != null) {
                                    BLEResponseAttributesRead(this, new Bluegiga.BLE.Responses.Attributes.ReadEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8)),
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(11).Take(bgapiRXBuffer[10]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseAttributesReadType != null) {
                                    BLEResponseAttributesReadType(this, new Bluegiga.BLE.Responses.Attributes.ReadTypeEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(9).Take(bgapiRXBuffer[8]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseAttributesUserReadResponse != null) {
                                    BLEResponseAttributesUserReadResponse(this, new Bluegiga.BLE.Responses.Attributes.UserReadResponseEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseAttributesUserWriteResponse != null) {
                                    BLEResponseAttributesUserWriteResponse(this, new Bluegiga.BLE.Responses.Attributes.UserWriteResponseEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseAttributesSend != null) {
                                    BLEResponseAttributesSend(this, new Bluegiga.BLE.Responses.Attributes.SendEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 3) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseConnectionDisconnect != null) {
                                    BLEResponseConnectionDisconnect(this, new Bluegiga.BLE.Responses.Connection.DisconnectEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseConnectionGetRssi != null) {
                                    BLEResponseConnectionGetRssi(this, new Bluegiga.BLE.Responses.Connection.GetRssiEventArgs(
                                        bgapiRXBuffer[4],
                                        (SByte)(bgapiRXBuffer[5])
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseConnectionUpdate != null) {
                                    BLEResponseConnectionUpdate(this, new Bluegiga.BLE.Responses.Connection.UpdateEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseConnectionVersionUpdate != null) {
                                    BLEResponseConnectionVersionUpdate(this, new Bluegiga.BLE.Responses.Connection.VersionUpdateEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseConnectionChannelMapGet != null) {
                                    BLEResponseConnectionChannelMapGet(this, new Bluegiga.BLE.Responses.Connection.ChannelMapGetEventArgs(
                                        bgapiRXBuffer[4],
                                        (Byte[])(bgapiRXBuffer.Skip(6).Take(bgapiRXBuffer[5]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseConnectionChannelMapSet != null) {
                                    BLEResponseConnectionChannelMapSet(this, new Bluegiga.BLE.Responses.Connection.ChannelMapSetEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseConnectionFeaturesGet != null) {
                                    BLEResponseConnectionFeaturesGet(this, new Bluegiga.BLE.Responses.Connection.FeaturesGetEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEResponseConnectionGetStatus != null) {
                                    BLEResponseConnectionGetStatus(this, new Bluegiga.BLE.Responses.Connection.GetStatusEventArgs(
                                        bgapiRXBuffer[4]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 8)
                            {
                                if (BLEResponseConnectionRawTX != null) {
                                    BLEResponseConnectionRawTX(this, new Bluegiga.BLE.Responses.Connection.RawTXEventArgs(
                                        bgapiRXBuffer[4]
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 4) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseATTClientFindByTypeValue != null) {
                                    BLEResponseATTClientFindByTypeValue(this, new Bluegiga.BLE.Responses.ATTClient.FindByTypeValueEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseATTClientReadByGroupType != null) {
                                    BLEResponseATTClientReadByGroupType(this, new Bluegiga.BLE.Responses.ATTClient.ReadByGroupTypeEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseATTClientReadByType != null) {
                                    BLEResponseATTClientReadByType(this, new Bluegiga.BLE.Responses.ATTClient.ReadByTypeEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseATTClientFindInformation != null) {
                                    BLEResponseATTClientFindInformation(this, new Bluegiga.BLE.Responses.ATTClient.FindInformationEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseATTClientReadByHandle != null) {
                                    BLEResponseATTClientReadByHandle(this, new Bluegiga.BLE.Responses.ATTClient.ReadByHandleEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseATTClientAttributeWrite != null) {
                                    BLEResponseATTClientAttributeWrite(this, new Bluegiga.BLE.Responses.ATTClient.AttributeWriteEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseATTClientWriteCommand != null) {
                                    BLEResponseATTClientWriteCommand(this, new Bluegiga.BLE.Responses.ATTClient.WriteCommandEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEResponseATTClientIndicateConfirm != null) {
                                    BLEResponseATTClientIndicateConfirm(this, new Bluegiga.BLE.Responses.ATTClient.IndicateConfirmEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 8)
                            {
                                if (BLEResponseATTClientReadLong != null) {
                                    BLEResponseATTClientReadLong(this, new Bluegiga.BLE.Responses.ATTClient.ReadLongEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 9)
                            {
                                if (BLEResponseATTClientPrepareWrite != null) {
                                    BLEResponseATTClientPrepareWrite(this, new Bluegiga.BLE.Responses.ATTClient.PrepareWriteEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 10)
                            {
                                if (BLEResponseATTClientExecuteWrite != null) {
                                    BLEResponseATTClientExecuteWrite(this, new Bluegiga.BLE.Responses.ATTClient.ExecuteWriteEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 11)
                            {
                                if (BLEResponseATTClientReadMultiple != null) {
                                    BLEResponseATTClientReadMultiple(this, new Bluegiga.BLE.Responses.ATTClient.ReadMultipleEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 5) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseSMEncryptStart != null) {
                                    BLEResponseSMEncryptStart(this, new Bluegiga.BLE.Responses.SM.EncryptStartEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseSMSetBondableMode != null) {
                                    BLEResponseSMSetBondableMode(this, new Bluegiga.BLE.Responses.SM.SetBondableModeEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseSMDeleteBonding != null) {
                                    BLEResponseSMDeleteBonding(this, new Bluegiga.BLE.Responses.SM.DeleteBondingEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseSMSetParameters != null) {
                                    BLEResponseSMSetParameters(this, new Bluegiga.BLE.Responses.SM.SetParametersEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseSMPasskeyEntry != null) {
                                    BLEResponseSMPasskeyEntry(this, new Bluegiga.BLE.Responses.SM.PasskeyEntryEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseSMGetBonds != null) {
                                    BLEResponseSMGetBonds(this, new Bluegiga.BLE.Responses.SM.GetBondsEventArgs(
                                        bgapiRXBuffer[4]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseSMSetOobData != null) {
                                    BLEResponseSMSetOobData(this, new Bluegiga.BLE.Responses.SM.SetOobDataEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEResponseSMWhitelistBonds != null) {
                                    BLEResponseSMWhitelistBonds(this, new Bluegiga.BLE.Responses.SM.WhitelistBondsEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6]
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 6) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseGAPSetPrivacyFlags != null) {
                                    BLEResponseGAPSetPrivacyFlags(this, new Bluegiga.BLE.Responses.GAP.SetPrivacyFlagsEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseGAPSetMode != null) {
                                    BLEResponseGAPSetMode(this, new Bluegiga.BLE.Responses.GAP.SetModeEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseGAPDiscover != null) {
                                    BLEResponseGAPDiscover(this, new Bluegiga.BLE.Responses.GAP.DiscoverEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseGAPConnectDirect != null) {
                                    BLEResponseGAPConnectDirect(this, new Bluegiga.BLE.Responses.GAP.ConnectDirectEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseGAPEndProcedure != null) {
                                    BLEResponseGAPEndProcedure(this, new Bluegiga.BLE.Responses.GAP.EndProcedureEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseGAPConnectSelective != null) {
                                    BLEResponseGAPConnectSelective(this, new Bluegiga.BLE.Responses.GAP.ConnectSelectiveEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseGAPSetFiltering != null) {
                                    BLEResponseGAPSetFiltering(this, new Bluegiga.BLE.Responses.GAP.SetFilteringEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEResponseGAPSetScanParameters != null) {
                                    BLEResponseGAPSetScanParameters(this, new Bluegiga.BLE.Responses.GAP.SetScanParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 8)
                            {
                                if (BLEResponseGAPSetAdvParameters != null) {
                                    BLEResponseGAPSetAdvParameters(this, new Bluegiga.BLE.Responses.GAP.SetAdvParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 9)
                            {
                                if (BLEResponseGAPSetAdvData != null) {
                                    BLEResponseGAPSetAdvData(this, new Bluegiga.BLE.Responses.GAP.SetAdvDataEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 10)
                            {
                                if (BLEResponseGAPSetDirectedConnectableMode != null) {
                                    BLEResponseGAPSetDirectedConnectableMode(this, new Bluegiga.BLE.Responses.GAP.SetDirectedConnectableModeEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 7) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseHardwareIOPortConfigIrq != null) {
                                    BLEResponseHardwareIOPortConfigIrq(this, new Bluegiga.BLE.Responses.Hardware.IOPortConfigIrqEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseHardwareSetSoftTimer != null) {
                                    BLEResponseHardwareSetSoftTimer(this, new Bluegiga.BLE.Responses.Hardware.SetSoftTimerEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseHardwareADCRead != null) {
                                    BLEResponseHardwareADCRead(this, new Bluegiga.BLE.Responses.Hardware.ADCReadEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseHardwareIOPortConfigDirection != null) {
                                    BLEResponseHardwareIOPortConfigDirection(this, new Bluegiga.BLE.Responses.Hardware.IOPortConfigDirectionEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseHardwareIOPortConfigFunction != null) {
                                    BLEResponseHardwareIOPortConfigFunction(this, new Bluegiga.BLE.Responses.Hardware.IOPortConfigFunctionEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseHardwareIOPortConfigPull != null) {
                                    BLEResponseHardwareIOPortConfigPull(this, new Bluegiga.BLE.Responses.Hardware.IOPortConfigPullEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseHardwareIOPortWrite != null) {
                                    BLEResponseHardwareIOPortWrite(this, new Bluegiga.BLE.Responses.Hardware.IOPortWriteEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEResponseHardwareIOPortRead != null) {
                                    BLEResponseHardwareIOPortRead(this, new Bluegiga.BLE.Responses.Hardware.IOPortReadEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6],
                                        bgapiRXBuffer[7]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 8)
                            {
                                if (BLEResponseHardwareSPIConfig != null) {
                                    BLEResponseHardwareSPIConfig(this, new Bluegiga.BLE.Responses.Hardware.SPIConfigEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 9)
                            {
                                if (BLEResponseHardwareSPITransfer != null) {
                                    BLEResponseHardwareSPITransfer(this, new Bluegiga.BLE.Responses.Hardware.SPITransferEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6],
                                        (Byte[])(bgapiRXBuffer.Skip(8).Take(bgapiRXBuffer[7]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 10)
                            {
                                if (BLEResponseHardwareI2CRead != null) {
                                    BLEResponseHardwareI2CRead(this, new Bluegiga.BLE.Responses.Hardware.I2CReadEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 11)
                            {
                                if (BLEResponseHardwareI2CWrite != null) {
                                    BLEResponseHardwareI2CWrite(this, new Bluegiga.BLE.Responses.Hardware.I2CWriteEventArgs(
                                        bgapiRXBuffer[4]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 12)
                            {
                                if (BLEResponseHardwareSetTxpower != null) {
                                    BLEResponseHardwareSetTxpower(this, new Bluegiga.BLE.Responses.Hardware.SetTxpowerEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 13)
                            {
                                if (BLEResponseHardwareTimerComparator != null) {
                                    BLEResponseHardwareTimerComparator(this, new Bluegiga.BLE.Responses.Hardware.TimerComparatorEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 14)
                            {
                                if (BLEResponseHardwareIOPortIrqEnable != null) {
                                    BLEResponseHardwareIOPortIrqEnable(this, new Bluegiga.BLE.Responses.Hardware.IOPortIrqEnableEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 15)
                            {
                                if (BLEResponseHardwareIOPortIrqDirection != null) {
                                    BLEResponseHardwareIOPortIrqDirection(this, new Bluegiga.BLE.Responses.Hardware.IOPortIrqDirectionEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 16)
                            {
                                if (BLEResponseHardwareAnalogComparatorEnable != null) {
                                    BLEResponseHardwareAnalogComparatorEnable(this, new Bluegiga.BLE.Responses.Hardware.AnalogComparatorEnableEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 17)
                            {
                                if (BLEResponseHardwareAnalogComparatorRead != null) {
                                    BLEResponseHardwareAnalogComparatorRead(this, new Bluegiga.BLE.Responses.Hardware.AnalogComparatorReadEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 18)
                            {
                                if (BLEResponseHardwareAnalogComparatorConfigIrq != null) {
                                    BLEResponseHardwareAnalogComparatorConfigIrq(this, new Bluegiga.BLE.Responses.Hardware.AnalogComparatorConfigIrqEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 19)
                            {
                                if (BLEResponseHardwareSetRxgain != null) {
                                    BLEResponseHardwareSetRxgain(this, new Bluegiga.BLE.Responses.Hardware.SetRxgainEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 20)
                            {
                                if (BLEResponseHardwareUsbEnable != null) {
                                    BLEResponseHardwareUsbEnable(this, new Bluegiga.BLE.Responses.Hardware.UsbEnableEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 8) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseTestPHYTX != null) {
                                    BLEResponseTestPHYTX(this, new Bluegiga.BLE.Responses.Test.PHYTXEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseTestPHYRX != null) {
                                    BLEResponseTestPHYRX(this, new Bluegiga.BLE.Responses.Test.PHYRXEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseTestPHYEnd != null) {
                                    BLEResponseTestPHYEnd(this, new Bluegiga.BLE.Responses.Test.PHYEndEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseTestPHYReset != null) {
                                    BLEResponseTestPHYReset(this, new Bluegiga.BLE.Responses.Test.PHYResetEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseTestGetChannelMap != null) {
                                    BLEResponseTestGetChannelMap(this, new Bluegiga.BLE.Responses.Test.GetChannelMapEventArgs(
                                        (Byte[])(bgapiRXBuffer.Skip(5).Take(bgapiRXBuffer[4]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseTestDebug != null) {
                                    BLEResponseTestDebug(this, new Bluegiga.BLE.Responses.Test.DebugEventArgs(
                                        (Byte[])(bgapiRXBuffer.Skip(5).Take(bgapiRXBuffer[4]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseTestChannelMode != null) {
                                    BLEResponseTestChannelMode(this, new Bluegiga.BLE.Responses.Test.ChannelModeEventArgs(
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 9) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseDFUReset != null) {
                                    BLEResponseDFUReset(this, new Bluegiga.BLE.Responses.DFU.ResetEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseDFUFlashSetAddress != null) {
                                    BLEResponseDFUFlashSetAddress(this, new Bluegiga.BLE.Responses.DFU.FlashSetAddressEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseDFUFlashUpload != null) {
                                    BLEResponseDFUFlashUpload(this, new Bluegiga.BLE.Responses.DFU.FlashUploadEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseDFUFlashUploadFinish != null) {
                                    BLEResponseDFUFlashUploadFinish(this, new Bluegiga.BLE.Responses.DFU.FlashUploadFinishEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        SetBusy(false);
                    } else {
                        // 0x80 = Event packet
                        if (bgapiRXBuffer[2] == 0) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventSystemBoot != null) {
                                    BLEEventSystemBoot(this, new Bluegiga.BLE.Events.System.BootEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8)),
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8)),
                                        (UInt16)(bgapiRXBuffer[10] + (bgapiRXBuffer[11] << 8)),
                                        (UInt16)(bgapiRXBuffer[12] + (bgapiRXBuffer[13] << 8)),
                                        bgapiRXBuffer[14],
                                        bgapiRXBuffer[15]
                                    ));
                                }
                                SetBusy(false);
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventSystemDebug != null) {
                                    BLEEventSystemDebug(this, new Bluegiga.BLE.Events.System.DebugEventArgs(
                                        (Byte[])(bgapiRXBuffer.Skip(5).Take(bgapiRXBuffer[4]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventSystemEndpointWatermarkRX != null) {
                                    BLEEventSystemEndpointWatermarkRX(this, new Bluegiga.BLE.Events.System.EndpointWatermarkRXEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEEventSystemEndpointWatermarkTX != null) {
                                    BLEEventSystemEndpointWatermarkTX(this, new Bluegiga.BLE.Events.System.EndpointWatermarkTXEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEEventSystemScriptFailure != null) {
                                    BLEEventSystemScriptFailure(this, new Bluegiga.BLE.Events.System.ScriptFailureEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEEventSystemNoLicenseKey != null) {
                                    BLEEventSystemNoLicenseKey(this, new Bluegiga.BLE.Events.System.NoLicenseKeyEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEEventSystemProtocolError != null) {
                                    BLEEventSystemProtocolError(this, new Bluegiga.BLE.Events.System.ProtocolErrorEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 1) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventFlashPSKey != null) {
                                    BLEEventFlashPSKey(this, new Bluegiga.BLE.Events.Flash.PSKeyEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 2) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventAttributesValue != null) {
                                    BLEEventAttributesValue(this, new Bluegiga.BLE.Events.Attributes.ValueEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5],
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8)),
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(11).Take(bgapiRXBuffer[10]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventAttributesUserReadRequest != null) {
                                    BLEEventAttributesUserReadRequest(this, new Bluegiga.BLE.Events.Attributes.UserReadRequestEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (UInt16)(bgapiRXBuffer[7] + (bgapiRXBuffer[8] << 8)),
                                        bgapiRXBuffer[9]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventAttributesStatus != null) {
                                    BLEEventAttributesStatus(this, new Bluegiga.BLE.Events.Attributes.StatusEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6]
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 3) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventConnectionStatus != null) {
                                    BLEEventConnectionStatus(this, new Bluegiga.BLE.Events.Connection.StatusEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5],
                                        (Byte[])(bgapiRXBuffer.Skip(6).Take(6).ToArray()),
                                        bgapiRXBuffer[12],
                                        (UInt16)(bgapiRXBuffer[13] + (bgapiRXBuffer[14] << 8)),
                                        (UInt16)(bgapiRXBuffer[15] + (bgapiRXBuffer[16] << 8)),
                                        (UInt16)(bgapiRXBuffer[17] + (bgapiRXBuffer[18] << 8)),
                                        bgapiRXBuffer[19]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventConnectionVersionInd != null) {
                                    BLEEventConnectionVersionInd(this, new Bluegiga.BLE.Events.Connection.VersionIndEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5],
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8)),
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventConnectionFeatureInd != null) {
                                    BLEEventConnectionFeatureInd(this, new Bluegiga.BLE.Events.Connection.FeatureIndEventArgs(
                                        bgapiRXBuffer[4],
                                        (Byte[])(bgapiRXBuffer.Skip(6).Take(bgapiRXBuffer[5]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEEventConnectionRawRX != null) {
                                    BLEEventConnectionRawRX(this, new Bluegiga.BLE.Events.Connection.RawRXEventArgs(
                                        bgapiRXBuffer[4],
                                        (Byte[])(bgapiRXBuffer.Skip(6).Take(bgapiRXBuffer[5]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEEventConnectionDisconnected != null) {
                                    BLEEventConnectionDisconnected(this, new Bluegiga.BLE.Events.Connection.DisconnectedEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 4) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventATTClientIndicated != null) {
                                    BLEEventATTClientIndicated(this, new Bluegiga.BLE.Events.ATTClient.IndicatedEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventATTClientProcedureCompleted != null) {
                                    BLEEventATTClientProcedureCompleted(this, new Bluegiga.BLE.Events.ATTClient.ProcedureCompletedEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (UInt16)(bgapiRXBuffer[7] + (bgapiRXBuffer[8] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventATTClientGroupFound != null) {
                                    BLEEventATTClientGroupFound(this, new Bluegiga.BLE.Events.ATTClient.GroupFoundEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (UInt16)(bgapiRXBuffer[7] + (bgapiRXBuffer[8] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(10).Take(bgapiRXBuffer[9]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEEventATTClientAttributeFound != null) {
                                    BLEEventATTClientAttributeFound(this, new Bluegiga.BLE.Events.ATTClient.AttributeFoundEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (UInt16)(bgapiRXBuffer[7] + (bgapiRXBuffer[8] << 8)),
                                        bgapiRXBuffer[9],
                                        (Byte[])(bgapiRXBuffer.Skip(11).Take(bgapiRXBuffer[10]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEEventATTClientFindInformationFound != null) {
                                    BLEEventATTClientFindInformationFound(this, new Bluegiga.BLE.Events.ATTClient.FindInformationFoundEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(8).Take(bgapiRXBuffer[7]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEEventATTClientAttributeValue != null) {
                                    BLEEventATTClientAttributeValue(this, new Bluegiga.BLE.Events.ATTClient.AttributeValueEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        bgapiRXBuffer[7],
                                        (Byte[])(bgapiRXBuffer.Skip(9).Take(bgapiRXBuffer[8]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEEventATTClientReadMultipleResponse != null) {
                                    BLEEventATTClientReadMultipleResponse(this, new Bluegiga.BLE.Events.ATTClient.ReadMultipleResponseEventArgs(
                                        bgapiRXBuffer[4],
                                        (Byte[])(bgapiRXBuffer.Skip(6).Take(bgapiRXBuffer[5]).ToArray())
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 5) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventSMSMPData != null) {
                                    BLEEventSMSMPData(this, new Bluegiga.BLE.Events.SM.SMPDataEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5],
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventSMBondingFail != null) {
                                    BLEEventSMBondingFail(this, new Bluegiga.BLE.Events.SM.BondingFailEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventSMPasskeyDisplay != null) {
                                    BLEEventSMPasskeyDisplay(this, new Bluegiga.BLE.Events.SM.PasskeyDisplayEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8) + (bgapiRXBuffer[6] << 16) + (bgapiRXBuffer[6] << 24))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEEventSMPasskeyRequest != null) {
                                    BLEEventSMPasskeyRequest(this, new Bluegiga.BLE.Events.SM.PasskeyRequestEventArgs(
                                        bgapiRXBuffer[4]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEEventSMBondStatus != null) {
                                    BLEEventSMBondStatus(this, new Bluegiga.BLE.Events.SM.BondStatusEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5],
                                        bgapiRXBuffer[6],
                                        bgapiRXBuffer[7]
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 6) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventGAPScanResponse != null) {
                                    BLEEventGAPScanResponse(this, new Bluegiga.BLE.Events.GAP.ScanResponseEventArgs(
                                        (SByte)(bgapiRXBuffer[4]),
                                        bgapiRXBuffer[5],
                                        (Byte[])(bgapiRXBuffer.Skip(6).Take(6).ToArray()),
                                        bgapiRXBuffer[12],
                                        bgapiRXBuffer[13],
                                        (Byte[])(bgapiRXBuffer.Skip(15).Take(bgapiRXBuffer[14]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventGAPModeChanged != null) {
                                    BLEEventGAPModeChanged(this, new Bluegiga.BLE.Events.GAP.ModeChangedEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5]
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 7) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventHardwareIOPortStatus != null) {
                                    BLEEventHardwareIOPortStatus(this, new Bluegiga.BLE.Events.Hardware.IOPortStatusEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8) + (bgapiRXBuffer[5] << 16) + (bgapiRXBuffer[5] << 24)),
                                        bgapiRXBuffer[8],
                                        bgapiRXBuffer[9],
                                        bgapiRXBuffer[10]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventHardwareSoftTimer != null) {
                                    BLEEventHardwareSoftTimer(this, new Bluegiga.BLE.Events.Hardware.SoftTimerEventArgs(
                                        bgapiRXBuffer[4]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventHardwareADCResult != null) {
                                    BLEEventHardwareADCResult(this, new Bluegiga.BLE.Events.Hardware.ADCResultEventArgs(
                                        bgapiRXBuffer[4],
                                        (Int16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEEventHardwareAnalogComparatorStatus != null) {
                                    BLEEventHardwareAnalogComparatorStatus(this, new Bluegiga.BLE.Events.Hardware.AnalogComparatorStatusEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8) + (bgapiRXBuffer[5] << 16) + (bgapiRXBuffer[5] << 24)),
                                        bgapiRXBuffer[8]
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 8) {
                        }
                        else if (bgapiRXBuffer[2] == 9) {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventDFUBoot != null) {
                                    BLEEventDFUBoot(this, new Bluegiga.BLE.Events.DFU.BootEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8) + (bgapiRXBuffer[5] << 16) + (bgapiRXBuffer[5] << 24))
                                    ));
                                }
                            }
                        }
                    }

                    // reset RX packet buffer position to be ready for new packet
                    bgapiRXBufferPos = 0;
                }
            }

            return 0; // parsed successfully
        }

        public UInt16 SendCommand(System.IO.Ports.SerialPort port, Byte[] cmd) {
            SetBusy(true);
            if (bgapiPacketMode) port.Write(new Byte[] { (Byte)cmd.Length }, 0, 1);
            port.Write(cmd, 0, cmd.Length);
            return 0; // no error handling yet
        }

    }

}
