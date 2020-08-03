// Blue Gecko v2.x BGLib C# interface library
// 2013-01-15 by Jeff Rowberg <jeff@rowberg.net
// 2020-08-03 Ported to Blue Gecko API v2.x by Kris Young <kris.young@silabs.com>
// Updates should (hopefully) always be available at https://github.com/jrowberg/bglib

/* ============================================
BGLib C# interface library code is placed under the MIT license
Original work Copyright (c) 2013 Jeff Rowberg
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
Generated on 2020-Aug-03 11:58:44
=============================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueGecko
{

    namespace BLE
    {

        namespace Responses
        {
            namespace DFU
            {
                public delegate void ResetEventHandler(object sender, BlueGecko.BLE.Responses.DFU.ResetEventArgs e);
                public class ResetEventArgs : EventArgs
                {
                    public ResetEventArgs() { }
                }

                public delegate void FlashSetAddressEventHandler(object sender, BlueGecko.BLE.Responses.DFU.FlashSetAddressEventArgs e);
                public class FlashSetAddressEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public FlashSetAddressEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void FlashUploadEventHandler(object sender, BlueGecko.BLE.Responses.DFU.FlashUploadEventArgs e);
                public class FlashUploadEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public FlashUploadEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void FlashUploadFinishEventHandler(object sender, BlueGecko.BLE.Responses.DFU.FlashUploadFinishEventArgs e);
                public class FlashUploadFinishEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public FlashUploadFinishEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace System
            {
                public delegate void HelloEventHandler(object sender, BlueGecko.BLE.Responses.System.HelloEventArgs e);
                public class HelloEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public HelloEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ResetEventHandler(object sender, BlueGecko.BLE.Responses.System.ResetEventArgs e);
                public class ResetEventArgs : EventArgs
                {
                    public ResetEventArgs() { }
                }

                public delegate void GetBtAddressEventHandler(object sender, BlueGecko.BLE.Responses.System.GetBtAddressEventArgs e);
                public class GetBtAddressEventArgs : EventArgs
                {
                    public readonly Byte[] address;
                    public GetBtAddressEventArgs(Byte[] address)
                    {
                        this.address = address;
                    }
                }

                public delegate void SetBtAddressEventHandler(object sender, BlueGecko.BLE.Responses.System.SetBtAddressEventArgs e);
                public class SetBtAddressEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetBtAddressEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetTXPowerEventHandler(object sender, BlueGecko.BLE.Responses.System.SetTXPowerEventArgs e);
                public class SetTXPowerEventArgs : EventArgs
                {
                    public readonly Int16 set_power;
                    public SetTXPowerEventArgs(Int16 set_power)
                    {
                        this.set_power = set_power;
                    }
                }

                public delegate void GetRandomDataEventHandler(object sender, BlueGecko.BLE.Responses.System.GetRandomDataEventArgs e);
                public class GetRandomDataEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte[] data;
                    public GetRandomDataEventArgs(UInt16 result, Byte[] data)
                    {
                        this.result = result;
                        this.data = data;
                    }
                }

                public delegate void HaltEventHandler(object sender, BlueGecko.BLE.Responses.System.HaltEventArgs e);
                public class HaltEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public HaltEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetDeviceNameEventHandler(object sender, BlueGecko.BLE.Responses.System.SetDeviceNameEventArgs e);
                public class SetDeviceNameEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetDeviceNameEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void LinklayerConfigureEventHandler(object sender, BlueGecko.BLE.Responses.System.LinklayerConfigureEventArgs e);
                public class LinklayerConfigureEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public LinklayerConfigureEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void GetCountersEventHandler(object sender, BlueGecko.BLE.Responses.System.GetCountersEventArgs e);
                public class GetCountersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt16 tx_packets;
                    public readonly UInt16 rx_packets;
                    public readonly UInt16 crc_errors;
                    public readonly UInt16 failures;
                    public GetCountersEventArgs(UInt16 result, UInt16 tx_packets, UInt16 rx_packets, UInt16 crc_errors, UInt16 failures)
                    {
                        this.result = result;
                        this.tx_packets = tx_packets;
                        this.rx_packets = rx_packets;
                        this.crc_errors = crc_errors;
                        this.failures = failures;
                    }
                }

                public delegate void DataBufferWriteEventHandler(object sender, BlueGecko.BLE.Responses.System.DataBufferWriteEventArgs e);
                public class DataBufferWriteEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DataBufferWriteEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetIdentityAddressEventHandler(object sender, BlueGecko.BLE.Responses.System.SetIdentityAddressEventArgs e);
                public class SetIdentityAddressEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetIdentityAddressEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DataBufferClearEventHandler(object sender, BlueGecko.BLE.Responses.System.DataBufferClearEventArgs e);
                public class DataBufferClearEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DataBufferClearEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace LEGAP
            {
                public delegate void OpenEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.OpenEventArgs e);
                public class OpenEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte connection;
                    public OpenEventArgs(UInt16 result, Byte connection)
                    {
                        this.result = result;
                        this.connection = connection;
                    }
                }

                public delegate void SetModeEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetModeEventArgs e);
                public class SetModeEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetModeEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DiscoverEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.DiscoverEventArgs e);
                public class DiscoverEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DiscoverEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void EndProcedureEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.EndProcedureEventArgs e);
                public class EndProcedureEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public EndProcedureEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetAdvParametersEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetAdvParametersEventArgs e);
                public class SetAdvParametersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetAdvParametersEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetConnParametersEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetConnParametersEventArgs e);
                public class SetConnParametersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetConnParametersEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetScanParametersEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetScanParametersEventArgs e);
                public class SetScanParametersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetScanParametersEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetAdvDataEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetAdvDataEventArgs e);
                public class SetAdvDataEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetAdvDataEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetAdvTimeoutEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetAdvTimeoutEventArgs e);
                public class SetAdvTimeoutEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetAdvTimeoutEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetConnPHYEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetConnPHYEventArgs e);
                public class SetConnPHYEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetConnPHYEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void Bt5SetModeEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.Bt5SetModeEventArgs e);
                public class Bt5SetModeEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public Bt5SetModeEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void Bt5SetAdvParametersEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.Bt5SetAdvParametersEventArgs e);
                public class Bt5SetAdvParametersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public Bt5SetAdvParametersEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void Bt5SetAdvDataEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.Bt5SetAdvDataEventArgs e);
                public class Bt5SetAdvDataEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public Bt5SetAdvDataEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetPrivacyModeEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetPrivacyModeEventArgs e);
                public class SetPrivacyModeEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetPrivacyModeEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetAdvertiseTimingEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetAdvertiseTimingEventArgs e);
                public class SetAdvertiseTimingEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetAdvertiseTimingEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetAdvertiseChannelMapEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetAdvertiseChannelMapEventArgs e);
                public class SetAdvertiseChannelMapEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetAdvertiseChannelMapEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetAdvertiseReportScanRequestEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetAdvertiseReportScanRequestEventArgs e);
                public class SetAdvertiseReportScanRequestEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetAdvertiseReportScanRequestEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetAdvertisePHYEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetAdvertisePHYEventArgs e);
                public class SetAdvertisePHYEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetAdvertisePHYEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetAdvertiseConfigurationEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetAdvertiseConfigurationEventArgs e);
                public class SetAdvertiseConfigurationEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetAdvertiseConfigurationEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ClearAdvertiseConfigurationEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.ClearAdvertiseConfigurationEventArgs e);
                public class ClearAdvertiseConfigurationEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ClearAdvertiseConfigurationEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void StartAdvertisingEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.StartAdvertisingEventArgs e);
                public class StartAdvertisingEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public StartAdvertisingEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void StopAdvertisingEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.StopAdvertisingEventArgs e);
                public class StopAdvertisingEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public StopAdvertisingEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetDiscoveryTimingEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetDiscoveryTimingEventArgs e);
                public class SetDiscoveryTimingEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetDiscoveryTimingEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetDiscoveryTypeEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetDiscoveryTypeEventArgs e);
                public class SetDiscoveryTypeEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetDiscoveryTypeEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void StartDiscoveryEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.StartDiscoveryEventArgs e);
                public class StartDiscoveryEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public StartDiscoveryEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetDataChannelClassificationEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetDataChannelClassificationEventArgs e);
                public class SetDataChannelClassificationEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetDataChannelClassificationEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ConnectEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.ConnectEventArgs e);
                public class ConnectEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte connection;
                    public ConnectEventArgs(UInt16 result, Byte connection)
                    {
                        this.result = result;
                        this.connection = connection;
                    }
                }

                public delegate void SetAdvertiseTXPowerEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetAdvertiseTXPowerEventArgs e);
                public class SetAdvertiseTXPowerEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Int16 set_power;
                    public SetAdvertiseTXPowerEventArgs(UInt16 result, Int16 set_power)
                    {
                        this.result = result;
                        this.set_power = set_power;
                    }
                }

                public delegate void SetDiscoveryExtendedScanResponseEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetDiscoveryExtendedScanResponseEventArgs e);
                public class SetDiscoveryExtendedScanResponseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetDiscoveryExtendedScanResponseEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void StartPeriodicAdvertisingEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.StartPeriodicAdvertisingEventArgs e);
                public class StartPeriodicAdvertisingEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public StartPeriodicAdvertisingEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void StopPeriodicAdvertisingEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.StopPeriodicAdvertisingEventArgs e);
                public class StopPeriodicAdvertisingEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public StopPeriodicAdvertisingEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetLongAdvertisingDataEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetLongAdvertisingDataEventArgs e);
                public class SetLongAdvertisingDataEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetLongAdvertisingDataEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void EnableWhitelistingEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.EnableWhitelistingEventArgs e);
                public class EnableWhitelistingEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public EnableWhitelistingEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetConnTimingParametersEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetConnTimingParametersEventArgs e);
                public class SetConnTimingParametersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetConnTimingParametersEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetAdvertiseRandomAddressEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.SetAdvertiseRandomAddressEventArgs e);
                public class SetAdvertiseRandomAddressEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte[] address_out;
                    public SetAdvertiseRandomAddressEventArgs(UInt16 result, Byte[] address_out)
                    {
                        this.result = result;
                        this.address_out = address_out;
                    }
                }

                public delegate void ClearAdvertiseRandomAddressEventHandler(object sender, BlueGecko.BLE.Responses.LEGAP.ClearAdvertiseRandomAddressEventArgs e);
                public class ClearAdvertiseRandomAddressEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ClearAdvertiseRandomAddressEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace Sync
            {
                public delegate void OpenEventHandler(object sender, BlueGecko.BLE.Responses.Sync.OpenEventArgs e);
                public class OpenEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte sync;
                    public OpenEventArgs(UInt16 result, Byte sync)
                    {
                        this.result = result;
                        this.sync = sync;
                    }
                }

                public delegate void CloseEventHandler(object sender, BlueGecko.BLE.Responses.Sync.CloseEventArgs e);
                public class CloseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public CloseEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace LEConnection
            {
                public delegate void SetParametersEventHandler(object sender, BlueGecko.BLE.Responses.LEConnection.SetParametersEventArgs e);
                public class SetParametersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetParametersEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void GetRssiEventHandler(object sender, BlueGecko.BLE.Responses.LEConnection.GetRssiEventArgs e);
                public class GetRssiEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public GetRssiEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DisableSlaveLatencyEventHandler(object sender, BlueGecko.BLE.Responses.LEConnection.DisableSlaveLatencyEventArgs e);
                public class DisableSlaveLatencyEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DisableSlaveLatencyEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetPHYEventHandler(object sender, BlueGecko.BLE.Responses.LEConnection.SetPHYEventArgs e);
                public class SetPHYEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetPHYEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void CloseEventHandler(object sender, BlueGecko.BLE.Responses.LEConnection.CloseEventArgs e);
                public class CloseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public CloseEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetTimingParametersEventHandler(object sender, BlueGecko.BLE.Responses.LEConnection.SetTimingParametersEventArgs e);
                public class SetTimingParametersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetTimingParametersEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ReadChannelMapEventHandler(object sender, BlueGecko.BLE.Responses.LEConnection.ReadChannelMapEventArgs e);
                public class ReadChannelMapEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte[] channel_map;
                    public ReadChannelMapEventArgs(UInt16 result, Byte[] channel_map)
                    {
                        this.result = result;
                        this.channel_map = channel_map;
                    }
                }

                public delegate void SetPreferredPHYEventHandler(object sender, BlueGecko.BLE.Responses.LEConnection.SetPreferredPHYEventArgs e);
                public class SetPreferredPHYEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetPreferredPHYEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace GATT
            {
                public delegate void SetMaxMtuEventHandler(object sender, BlueGecko.BLE.Responses.GATT.SetMaxMtuEventArgs e);
                public class SetMaxMtuEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt16 max_mtu;
                    public SetMaxMtuEventArgs(UInt16 result, UInt16 max_mtu)
                    {
                        this.result = result;
                        this.max_mtu = max_mtu;
                    }
                }

                public delegate void DiscoverPrimaryServicesEventHandler(object sender, BlueGecko.BLE.Responses.GATT.DiscoverPrimaryServicesEventArgs e);
                public class DiscoverPrimaryServicesEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DiscoverPrimaryServicesEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DiscoverPrimaryServicesByUUIDEventHandler(object sender, BlueGecko.BLE.Responses.GATT.DiscoverPrimaryServicesByUUIDEventArgs e);
                public class DiscoverPrimaryServicesByUUIDEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DiscoverPrimaryServicesByUUIDEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DiscoverCharacteristicsEventHandler(object sender, BlueGecko.BLE.Responses.GATT.DiscoverCharacteristicsEventArgs e);
                public class DiscoverCharacteristicsEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DiscoverCharacteristicsEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DiscoverCharacteristicsByUUIDEventHandler(object sender, BlueGecko.BLE.Responses.GATT.DiscoverCharacteristicsByUUIDEventArgs e);
                public class DiscoverCharacteristicsByUUIDEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DiscoverCharacteristicsByUUIDEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetCharacteristicNotificationEventHandler(object sender, BlueGecko.BLE.Responses.GATT.SetCharacteristicNotificationEventArgs e);
                public class SetCharacteristicNotificationEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetCharacteristicNotificationEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DiscoverDescriptorsEventHandler(object sender, BlueGecko.BLE.Responses.GATT.DiscoverDescriptorsEventArgs e);
                public class DiscoverDescriptorsEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DiscoverDescriptorsEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ReadCharacteristicValueEventHandler(object sender, BlueGecko.BLE.Responses.GATT.ReadCharacteristicValueEventArgs e);
                public class ReadCharacteristicValueEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ReadCharacteristicValueEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ReadCharacteristicValueByUUIDEventHandler(object sender, BlueGecko.BLE.Responses.GATT.ReadCharacteristicValueByUUIDEventArgs e);
                public class ReadCharacteristicValueByUUIDEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ReadCharacteristicValueByUUIDEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void WriteCharacteristicValueEventHandler(object sender, BlueGecko.BLE.Responses.GATT.WriteCharacteristicValueEventArgs e);
                public class WriteCharacteristicValueEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public WriteCharacteristicValueEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void WriteCharacteristicValueWithoutResponseEventHandler(object sender, BlueGecko.BLE.Responses.GATT.WriteCharacteristicValueWithoutResponseEventArgs e);
                public class WriteCharacteristicValueWithoutResponseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt16 sent_len;
                    public WriteCharacteristicValueWithoutResponseEventArgs(UInt16 result, UInt16 sent_len)
                    {
                        this.result = result;
                        this.sent_len = sent_len;
                    }
                }

                public delegate void PrepareCharacteristicValueWriteEventHandler(object sender, BlueGecko.BLE.Responses.GATT.PrepareCharacteristicValueWriteEventArgs e);
                public class PrepareCharacteristicValueWriteEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt16 sent_len;
                    public PrepareCharacteristicValueWriteEventArgs(UInt16 result, UInt16 sent_len)
                    {
                        this.result = result;
                        this.sent_len = sent_len;
                    }
                }

                public delegate void ExecuteCharacteristicValueWriteEventHandler(object sender, BlueGecko.BLE.Responses.GATT.ExecuteCharacteristicValueWriteEventArgs e);
                public class ExecuteCharacteristicValueWriteEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ExecuteCharacteristicValueWriteEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SendCharacteristicConfirmationEventHandler(object sender, BlueGecko.BLE.Responses.GATT.SendCharacteristicConfirmationEventArgs e);
                public class SendCharacteristicConfirmationEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SendCharacteristicConfirmationEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ReadDescriptorValueEventHandler(object sender, BlueGecko.BLE.Responses.GATT.ReadDescriptorValueEventArgs e);
                public class ReadDescriptorValueEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ReadDescriptorValueEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void WriteDescriptorValueEventHandler(object sender, BlueGecko.BLE.Responses.GATT.WriteDescriptorValueEventArgs e);
                public class WriteDescriptorValueEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public WriteDescriptorValueEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void FindIncludedServicesEventHandler(object sender, BlueGecko.BLE.Responses.GATT.FindIncludedServicesEventArgs e);
                public class FindIncludedServicesEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public FindIncludedServicesEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ReadMultipleCharacteristicValuesEventHandler(object sender, BlueGecko.BLE.Responses.GATT.ReadMultipleCharacteristicValuesEventArgs e);
                public class ReadMultipleCharacteristicValuesEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ReadMultipleCharacteristicValuesEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ReadCharacteristicValueFromOffsetEventHandler(object sender, BlueGecko.BLE.Responses.GATT.ReadCharacteristicValueFromOffsetEventArgs e);
                public class ReadCharacteristicValueFromOffsetEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ReadCharacteristicValueFromOffsetEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void PrepareCharacteristicValueReliableWriteEventHandler(object sender, BlueGecko.BLE.Responses.GATT.PrepareCharacteristicValueReliableWriteEventArgs e);
                public class PrepareCharacteristicValueReliableWriteEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt16 sent_len;
                    public PrepareCharacteristicValueReliableWriteEventArgs(UInt16 result, UInt16 sent_len)
                    {
                        this.result = result;
                        this.sent_len = sent_len;
                    }
                }

            }
            namespace GATTServer
            {
                public delegate void ReadAttributeValueEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.ReadAttributeValueEventArgs e);
                public class ReadAttributeValueEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte[] value;
                    public ReadAttributeValueEventArgs(UInt16 result, Byte[] value)
                    {
                        this.result = result;
                        this.value = value;
                    }
                }

                public delegate void ReadAttributeTypeEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.ReadAttributeTypeEventArgs e);
                public class ReadAttributeTypeEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte[] type;
                    public ReadAttributeTypeEventArgs(UInt16 result, Byte[] type)
                    {
                        this.result = result;
                        this.type = type;
                    }
                }

                public delegate void WriteAttributeValueEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.WriteAttributeValueEventArgs e);
                public class WriteAttributeValueEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public WriteAttributeValueEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SendUserReadResponseEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.SendUserReadResponseEventArgs e);
                public class SendUserReadResponseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt16 sent_len;
                    public SendUserReadResponseEventArgs(UInt16 result, UInt16 sent_len)
                    {
                        this.result = result;
                        this.sent_len = sent_len;
                    }
                }

                public delegate void SendUserWriteResponseEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.SendUserWriteResponseEventArgs e);
                public class SendUserWriteResponseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SendUserWriteResponseEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SendCharacteristicNotificationEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.SendCharacteristicNotificationEventArgs e);
                public class SendCharacteristicNotificationEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt16 sent_len;
                    public SendCharacteristicNotificationEventArgs(UInt16 result, UInt16 sent_len)
                    {
                        this.result = result;
                        this.sent_len = sent_len;
                    }
                }

                public delegate void FindAttributeEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.FindAttributeEventArgs e);
                public class FindAttributeEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt16 attribute;
                    public FindAttributeEventArgs(UInt16 result, UInt16 attribute)
                    {
                        this.result = result;
                        this.attribute = attribute;
                    }
                }

                public delegate void SetCapabilitiesEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.SetCapabilitiesEventArgs e);
                public class SetCapabilitiesEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetCapabilitiesEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void FindPrimaryServiceEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.FindPrimaryServiceEventArgs e);
                public class FindPrimaryServiceEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt16 start;
                    public readonly UInt16 end;
                    public FindPrimaryServiceEventArgs(UInt16 result, UInt16 start, UInt16 end)
                    {
                        this.result = result;
                        this.start = start;
                        this.end = end;
                    }
                }

                public delegate void SetMaxMtuEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.SetMaxMtuEventArgs e);
                public class SetMaxMtuEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt16 max_mtu;
                    public SetMaxMtuEventArgs(UInt16 result, UInt16 max_mtu)
                    {
                        this.result = result;
                        this.max_mtu = max_mtu;
                    }
                }

                public delegate void GetMtuEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.GetMtuEventArgs e);
                public class GetMtuEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt16 mtu;
                    public GetMtuEventArgs(UInt16 result, UInt16 mtu)
                    {
                        this.result = result;
                        this.mtu = mtu;
                    }
                }

                public delegate void EnableCapabilitiesEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.EnableCapabilitiesEventArgs e);
                public class EnableCapabilitiesEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public EnableCapabilitiesEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DisableCapabilitiesEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.DisableCapabilitiesEventArgs e);
                public class DisableCapabilitiesEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DisableCapabilitiesEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void GetEnabledCapabilitiesEventHandler(object sender, BlueGecko.BLE.Responses.GATTServer.GetEnabledCapabilitiesEventArgs e);
                public class GetEnabledCapabilitiesEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt32 caps;
                    public GetEnabledCapabilitiesEventArgs(UInt16 result, UInt32 caps)
                    {
                        this.result = result;
                        this.caps = caps;
                    }
                }

            }
            namespace Hardware
            {
                public delegate void SetSoftTimerEventHandler(object sender, BlueGecko.BLE.Responses.Hardware.SetSoftTimerEventArgs e);
                public class SetSoftTimerEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetSoftTimerEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void GetTimeEventHandler(object sender, BlueGecko.BLE.Responses.Hardware.GetTimeEventArgs e);
                public class GetTimeEventArgs : EventArgs
                {
                    public readonly UInt32 seconds;
                    public readonly UInt16 ticks;
                    public GetTimeEventArgs(UInt32 seconds, UInt16 ticks)
                    {
                        this.seconds = seconds;
                        this.ticks = ticks;
                    }
                }

                public delegate void SetLazySoftTimerEventHandler(object sender, BlueGecko.BLE.Responses.Hardware.SetLazySoftTimerEventArgs e);
                public class SetLazySoftTimerEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetLazySoftTimerEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace Flash
            {
                public delegate void PSEraseAllEventHandler(object sender, BlueGecko.BLE.Responses.Flash.PSEraseAllEventArgs e);
                public class PSEraseAllEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public PSEraseAllEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void PSSaveEventHandler(object sender, BlueGecko.BLE.Responses.Flash.PSSaveEventArgs e);
                public class PSSaveEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public PSSaveEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void PSLoadEventHandler(object sender, BlueGecko.BLE.Responses.Flash.PSLoadEventArgs e);
                public class PSLoadEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte[] value;
                    public PSLoadEventArgs(UInt16 result, Byte[] value)
                    {
                        this.result = result;
                        this.value = value;
                    }
                }

                public delegate void PSEraseEventHandler(object sender, BlueGecko.BLE.Responses.Flash.PSEraseEventArgs e);
                public class PSEraseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public PSEraseEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace Test
            {
                public delegate void DTMTXEventHandler(object sender, BlueGecko.BLE.Responses.Test.DTMTXEventArgs e);
                public class DTMTXEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DTMTXEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DTMRXEventHandler(object sender, BlueGecko.BLE.Responses.Test.DTMRXEventArgs e);
                public class DTMRXEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DTMRXEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DTMEndEventHandler(object sender, BlueGecko.BLE.Responses.Test.DTMEndEventArgs e);
                public class DTMEndEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DTMEndEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DebugCommandEventHandler(object sender, BlueGecko.BLE.Responses.Test.DebugCommandEventArgs e);
                public class DebugCommandEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte id;
                    public readonly Byte[] debugdata;
                    public DebugCommandEventArgs(UInt16 result, Byte id, Byte[] debugdata)
                    {
                        this.result = result;
                        this.id = id;
                        this.debugdata = debugdata;
                    }
                }

                public delegate void DebugCounterEventHandler(object sender, BlueGecko.BLE.Responses.Test.DebugCounterEventArgs e);
                public class DebugCounterEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt32 value;
                    public DebugCounterEventArgs(UInt16 result, UInt32 value)
                    {
                        this.result = result;
                        this.value = value;
                    }
                }

            }
            namespace SM
            {
                public delegate void SetBondableModeEventHandler(object sender, BlueGecko.BLE.Responses.SM.SetBondableModeEventArgs e);
                public class SetBondableModeEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetBondableModeEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ConfigureEventHandler(object sender, BlueGecko.BLE.Responses.SM.ConfigureEventArgs e);
                public class ConfigureEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ConfigureEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void StoreBondingConfigurationEventHandler(object sender, BlueGecko.BLE.Responses.SM.StoreBondingConfigurationEventArgs e);
                public class StoreBondingConfigurationEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public StoreBondingConfigurationEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void IncreaseSecurityEventHandler(object sender, BlueGecko.BLE.Responses.SM.IncreaseSecurityEventArgs e);
                public class IncreaseSecurityEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public IncreaseSecurityEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DeleteBondingEventHandler(object sender, BlueGecko.BLE.Responses.SM.DeleteBondingEventArgs e);
                public class DeleteBondingEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DeleteBondingEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DeleteBondingsEventHandler(object sender, BlueGecko.BLE.Responses.SM.DeleteBondingsEventArgs e);
                public class DeleteBondingsEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DeleteBondingsEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void EnterPasskeyEventHandler(object sender, BlueGecko.BLE.Responses.SM.EnterPasskeyEventArgs e);
                public class EnterPasskeyEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public EnterPasskeyEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void PasskeyConfirmEventHandler(object sender, BlueGecko.BLE.Responses.SM.PasskeyConfirmEventArgs e);
                public class PasskeyConfirmEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public PasskeyConfirmEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetOobDataEventHandler(object sender, BlueGecko.BLE.Responses.SM.SetOobDataEventArgs e);
                public class SetOobDataEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetOobDataEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ListAllBondingsEventHandler(object sender, BlueGecko.BLE.Responses.SM.ListAllBondingsEventArgs e);
                public class ListAllBondingsEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ListAllBondingsEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void BondingConfirmEventHandler(object sender, BlueGecko.BLE.Responses.SM.BondingConfirmEventArgs e);
                public class BondingConfirmEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public BondingConfirmEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetDebugModeEventHandler(object sender, BlueGecko.BLE.Responses.SM.SetDebugModeEventArgs e);
                public class SetDebugModeEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetDebugModeEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetPasskeyEventHandler(object sender, BlueGecko.BLE.Responses.SM.SetPasskeyEventArgs e);
                public class SetPasskeyEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetPasskeyEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void UseScOobEventHandler(object sender, BlueGecko.BLE.Responses.SM.UseScOobEventArgs e);
                public class UseScOobEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte[] oob_data;
                    public UseScOobEventArgs(UInt16 result, Byte[] oob_data)
                    {
                        this.result = result;
                        this.oob_data = oob_data;
                    }
                }

                public delegate void SetScRemoteOobDataEventHandler(object sender, BlueGecko.BLE.Responses.SM.SetScRemoteOobDataEventArgs e);
                public class SetScRemoteOobDataEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetScRemoteOobDataEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void AddToWhitelistEventHandler(object sender, BlueGecko.BLE.Responses.SM.AddToWhitelistEventArgs e);
                public class AddToWhitelistEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public AddToWhitelistEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetMinimumKeySizeEventHandler(object sender, BlueGecko.BLE.Responses.SM.SetMinimumKeySizeEventArgs e);
                public class SetMinimumKeySizeEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetMinimumKeySizeEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace Homekit
            {
                public delegate void ConfigureEventHandler(object sender, BlueGecko.BLE.Responses.Homekit.ConfigureEventArgs e);
                public class ConfigureEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ConfigureEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void AdvertiseEventHandler(object sender, BlueGecko.BLE.Responses.Homekit.AdvertiseEventArgs e);
                public class AdvertiseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public AdvertiseEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DeletePairingsEventHandler(object sender, BlueGecko.BLE.Responses.Homekit.DeletePairingsEventArgs e);
                public class DeletePairingsEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DeletePairingsEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void CheckAuthcpEventHandler(object sender, BlueGecko.BLE.Responses.Homekit.CheckAuthcpEventArgs e);
                public class CheckAuthcpEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public CheckAuthcpEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void GetPairingIdEventHandler(object sender, BlueGecko.BLE.Responses.Homekit.GetPairingIdEventArgs e);
                public class GetPairingIdEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte[] pairing_id;
                    public GetPairingIdEventArgs(UInt16 result, Byte[] pairing_id)
                    {
                        this.result = result;
                        this.pairing_id = pairing_id;
                    }
                }

                public delegate void SendWriteResponseEventHandler(object sender, BlueGecko.BLE.Responses.Homekit.SendWriteResponseEventArgs e);
                public class SendWriteResponseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SendWriteResponseEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SendReadResponseEventHandler(object sender, BlueGecko.BLE.Responses.Homekit.SendReadResponseEventArgs e);
                public class SendReadResponseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SendReadResponseEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void GsnActionEventHandler(object sender, BlueGecko.BLE.Responses.Homekit.GsnActionEventArgs e);
                public class GsnActionEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public GsnActionEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void EventNotificationEventHandler(object sender, BlueGecko.BLE.Responses.Homekit.EventNotificationEventArgs e);
                public class EventNotificationEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public EventNotificationEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void BroadcastActionEventHandler(object sender, BlueGecko.BLE.Responses.Homekit.BroadcastActionEventArgs e);
                public class BroadcastActionEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public BroadcastActionEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ConfigureProductDataEventHandler(object sender, BlueGecko.BLE.Responses.Homekit.ConfigureProductDataEventArgs e);
                public class ConfigureProductDataEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ConfigureProductDataEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace Coex
            {
                public delegate void SetOptionsEventHandler(object sender, BlueGecko.BLE.Responses.Coex.SetOptionsEventArgs e);
                public class SetOptionsEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetOptionsEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void GetCountersEventHandler(object sender, BlueGecko.BLE.Responses.Coex.GetCountersEventArgs e);
                public class GetCountersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte[] counters;
                    public GetCountersEventArgs(UInt16 result, Byte[] counters)
                    {
                        this.result = result;
                        this.counters = counters;
                    }
                }

                public delegate void SetParametersEventHandler(object sender, BlueGecko.BLE.Responses.Coex.SetParametersEventArgs e);
                public class SetParametersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetParametersEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetDirectionalPriorityPulseEventHandler(object sender, BlueGecko.BLE.Responses.Coex.SetDirectionalPriorityPulseEventArgs e);
                public class SetDirectionalPriorityPulseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetDirectionalPriorityPulseEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace L2CAP
            {
                public delegate void CocSendConnectionRequestEventHandler(object sender, BlueGecko.BLE.Responses.L2CAP.CocSendConnectionRequestEventArgs e);
                public class CocSendConnectionRequestEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public CocSendConnectionRequestEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void CocSendConnectionResponseEventHandler(object sender, BlueGecko.BLE.Responses.L2CAP.CocSendConnectionResponseEventArgs e);
                public class CocSendConnectionResponseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public CocSendConnectionResponseEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void CocSendLEFlowControlCreditEventHandler(object sender, BlueGecko.BLE.Responses.L2CAP.CocSendLEFlowControlCreditEventArgs e);
                public class CocSendLEFlowControlCreditEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public CocSendLEFlowControlCreditEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void CocSendDisconnectionRequestEventHandler(object sender, BlueGecko.BLE.Responses.L2CAP.CocSendDisconnectionRequestEventArgs e);
                public class CocSendDisconnectionRequestEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public CocSendDisconnectionRequestEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void CocSendDataEventHandler(object sender, BlueGecko.BLE.Responses.L2CAP.CocSendDataEventArgs e);
                public class CocSendDataEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public CocSendDataEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace CTETransmitter
            {
                public delegate void EnableCTEResponseEventHandler(object sender, BlueGecko.BLE.Responses.CTETransmitter.EnableCTEResponseEventArgs e);
                public class EnableCTEResponseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public EnableCTEResponseEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void DisableCTEResponseEventHandler(object sender, BlueGecko.BLE.Responses.CTETransmitter.DisableCTEResponseEventArgs e);
                public class DisableCTEResponseEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public DisableCTEResponseEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void StartConnectionlessCTEEventHandler(object sender, BlueGecko.BLE.Responses.CTETransmitter.StartConnectionlessCTEEventArgs e);
                public class StartConnectionlessCTEEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public StartConnectionlessCTEEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void StopConnectionlessCTEEventHandler(object sender, BlueGecko.BLE.Responses.CTETransmitter.StopConnectionlessCTEEventArgs e);
                public class StopConnectionlessCTEEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public StopConnectionlessCTEEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetDTMParametersEventHandler(object sender, BlueGecko.BLE.Responses.CTETransmitter.SetDTMParametersEventArgs e);
                public class SetDTMParametersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetDTMParametersEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ClearDTMParametersEventHandler(object sender, BlueGecko.BLE.Responses.CTETransmitter.ClearDTMParametersEventArgs e);
                public class ClearDTMParametersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ClearDTMParametersEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace CTEReceiver
            {
                public delegate void ConfigureEventHandler(object sender, BlueGecko.BLE.Responses.CTEReceiver.ConfigureEventArgs e);
                public class ConfigureEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ConfigureEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void StartIqSamplingEventHandler(object sender, BlueGecko.BLE.Responses.CTEReceiver.StartIqSamplingEventArgs e);
                public class StartIqSamplingEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public StartIqSamplingEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void StopIqSamplingEventHandler(object sender, BlueGecko.BLE.Responses.CTEReceiver.StopIqSamplingEventArgs e);
                public class StopIqSamplingEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public StopIqSamplingEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void StartConnectionlessIqSamplingEventHandler(object sender, BlueGecko.BLE.Responses.CTEReceiver.StartConnectionlessIqSamplingEventArgs e);
                public class StartConnectionlessIqSamplingEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public StartConnectionlessIqSamplingEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void StopConnectionlessIqSamplingEventHandler(object sender, BlueGecko.BLE.Responses.CTEReceiver.StopConnectionlessIqSamplingEventArgs e);
                public class StopConnectionlessIqSamplingEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public StopConnectionlessIqSamplingEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void SetDTMParametersEventHandler(object sender, BlueGecko.BLE.Responses.CTEReceiver.SetDTMParametersEventArgs e);
                public class SetDTMParametersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public SetDTMParametersEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

                public delegate void ClearDTMParametersEventHandler(object sender, BlueGecko.BLE.Responses.CTEReceiver.ClearDTMParametersEventArgs e);
                public class ClearDTMParametersEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ClearDTMParametersEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace Qualtester
            {
                public delegate void ConfigureEventHandler(object sender, BlueGecko.BLE.Responses.Qualtester.ConfigureEventArgs e);
                public class ConfigureEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public ConfigureEventArgs(UInt16 result)
                    {
                        this.result = result;
                    }
                }

            }
            namespace User
            {
                public delegate void MessageToTargetEventHandler(object sender, BlueGecko.BLE.Responses.User.MessageToTargetEventArgs e);
                public class MessageToTargetEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly Byte[] data;
                    public MessageToTargetEventArgs(UInt16 result, Byte[] data)
                    {
                        this.result = result;
                        this.data = data;
                    }
                }

            }
        }

        namespace Events
        {
            namespace DFU
            {
                public delegate void BootEventHandler(object sender, BlueGecko.BLE.Events.DFU.BootEventArgs e);
                public class BootEventArgs : EventArgs
                {
                    public readonly UInt32 version;
                    public BootEventArgs(UInt32 version)
                    {
                        this.version = version;
                    }
                }

                public delegate void BootFailureEventHandler(object sender, BlueGecko.BLE.Events.DFU.BootFailureEventArgs e);
                public class BootFailureEventArgs : EventArgs
                {
                    public readonly UInt16 reason;
                    public BootFailureEventArgs(UInt16 reason)
                    {
                        this.reason = reason;
                    }
                }

            }
            namespace System
            {
                public delegate void BootEventHandler(object sender, BlueGecko.BLE.Events.System.BootEventArgs e);
                public class BootEventArgs : EventArgs
                {
                    public readonly UInt16 major;
                    public readonly UInt16 minor;
                    public readonly UInt16 patch;
                    public readonly UInt16 build;
                    public readonly UInt32 bootloader;
                    public readonly UInt16 hw;
                    public readonly UInt32 hash;
                    public BootEventArgs(UInt16 major, UInt16 minor, UInt16 patch, UInt16 build, UInt32 bootloader, UInt16 hw, UInt32 hash)
                    {
                        this.major = major;
                        this.minor = minor;
                        this.patch = patch;
                        this.build = build;
                        this.bootloader = bootloader;
                        this.hw = hw;
                        this.hash = hash;
                    }
                }

                public delegate void ExternalSignalEventHandler(object sender, BlueGecko.BLE.Events.System.ExternalSignalEventArgs e);
                public class ExternalSignalEventArgs : EventArgs
                {
                    public readonly UInt32 extsignals;
                    public ExternalSignalEventArgs(UInt32 extsignals)
                    {
                        this.extsignals = extsignals;
                    }
                }

                public delegate void AwakeEventHandler(object sender, BlueGecko.BLE.Events.System.AwakeEventArgs e);
                public class AwakeEventArgs : EventArgs
                {
                    public AwakeEventArgs() { }
                }

                public delegate void HardwareErrorEventHandler(object sender, BlueGecko.BLE.Events.System.HardwareErrorEventArgs e);
                public class HardwareErrorEventArgs : EventArgs
                {
                    public readonly UInt16 status;
                    public HardwareErrorEventArgs(UInt16 status)
                    {
                        this.status = status;
                    }
                }

                public delegate void ErrorEventHandler(object sender, BlueGecko.BLE.Events.System.ErrorEventArgs e);
                public class ErrorEventArgs : EventArgs
                {
                    public readonly UInt16 reason;
                    public readonly Byte[] data;
                    public ErrorEventArgs(UInt16 reason, Byte[] data)
                    {
                        this.reason = reason;
                        this.data = data;
                    }
                }

            }
            namespace LEGAP
            {
                public delegate void ScanResponseEventHandler(object sender, BlueGecko.BLE.Events.LEGAP.ScanResponseEventArgs e);
                public class ScanResponseEventArgs : EventArgs
                {
                    public readonly SByte rssi;
                    public readonly Byte packet_type;
                    public readonly Byte[] address;
                    public readonly Byte address_type;
                    public readonly Byte bonding;
                    public readonly Byte[] data;
                    public ScanResponseEventArgs(SByte rssi, Byte packet_type, Byte[] address, Byte address_type, Byte bonding, Byte[] data)
                    {
                        this.rssi = rssi;
                        this.packet_type = packet_type;
                        this.address = address;
                        this.address_type = address_type;
                        this.bonding = bonding;
                        this.data = data;
                    }
                }

                public delegate void AdvTimeoutEventHandler(object sender, BlueGecko.BLE.Events.LEGAP.AdvTimeoutEventArgs e);
                public class AdvTimeoutEventArgs : EventArgs
                {
                    public readonly Byte handle;
                    public AdvTimeoutEventArgs(Byte handle)
                    {
                        this.handle = handle;
                    }
                }

                public delegate void ScanRequestEventHandler(object sender, BlueGecko.BLE.Events.LEGAP.ScanRequestEventArgs e);
                public class ScanRequestEventArgs : EventArgs
                {
                    public readonly Byte handle;
                    public readonly Byte[] address;
                    public readonly Byte address_type;
                    public readonly Byte bonding;
                    public ScanRequestEventArgs(Byte handle, Byte[] address, Byte address_type, Byte bonding)
                    {
                        this.handle = handle;
                        this.address = address;
                        this.address_type = address_type;
                        this.bonding = bonding;
                    }
                }

                public delegate void ExtendedScanResponseEventHandler(object sender, BlueGecko.BLE.Events.LEGAP.ExtendedScanResponseEventArgs e);
                public class ExtendedScanResponseEventArgs : EventArgs
                {
                    public readonly Byte packet_type;
                    public readonly Byte[] address;
                    public readonly Byte address_type;
                    public readonly Byte bonding;
                    public readonly Byte primary_phy;
                    public readonly Byte secondary_phy;
                    public readonly Byte adv_sid;
                    public readonly SByte tx_power;
                    public readonly SByte rssi;
                    public readonly Byte channel;
                    public readonly UInt16 periodic_interval;
                    public readonly Byte[] data;
                    public ExtendedScanResponseEventArgs(Byte packet_type, Byte[] address, Byte address_type, Byte bonding, Byte primary_phy, Byte secondary_phy, Byte adv_sid, SByte tx_power, SByte rssi, Byte channel, UInt16 periodic_interval, Byte[] data)
                    {
                        this.packet_type = packet_type;
                        this.address = address;
                        this.address_type = address_type;
                        this.bonding = bonding;
                        this.primary_phy = primary_phy;
                        this.secondary_phy = secondary_phy;
                        this.adv_sid = adv_sid;
                        this.tx_power = tx_power;
                        this.rssi = rssi;
                        this.channel = channel;
                        this.periodic_interval = periodic_interval;
                        this.data = data;
                    }
                }

                public delegate void PeriodicAdvertisingStatusEventHandler(object sender, BlueGecko.BLE.Events.LEGAP.PeriodicAdvertisingStatusEventArgs e);
                public class PeriodicAdvertisingStatusEventArgs : EventArgs
                {
                    public readonly Byte sid;
                    public readonly UInt32 status;
                    public PeriodicAdvertisingStatusEventArgs(Byte sid, UInt32 status)
                    {
                        this.sid = sid;
                        this.status = status;
                    }
                }

            }
            namespace Sync
            {
                public delegate void OpenedEventHandler(object sender, BlueGecko.BLE.Events.Sync.OpenedEventArgs e);
                public class OpenedEventArgs : EventArgs
                {
                    public readonly Byte sync;
                    public readonly Byte adv_sid;
                    public readonly Byte[] address;
                    public readonly Byte address_type;
                    public readonly Byte adv_phy;
                    public readonly UInt16 adv_interval;
                    public readonly UInt16 clock_accuracy;
                    public OpenedEventArgs(Byte sync, Byte adv_sid, Byte[] address, Byte address_type, Byte adv_phy, UInt16 adv_interval, UInt16 clock_accuracy)
                    {
                        this.sync = sync;
                        this.adv_sid = adv_sid;
                        this.address = address;
                        this.address_type = address_type;
                        this.adv_phy = adv_phy;
                        this.adv_interval = adv_interval;
                        this.clock_accuracy = clock_accuracy;
                    }
                }

                public delegate void ClosedEventHandler(object sender, BlueGecko.BLE.Events.Sync.ClosedEventArgs e);
                public class ClosedEventArgs : EventArgs
                {
                    public readonly UInt16 reason;
                    public readonly Byte sync;
                    public ClosedEventArgs(UInt16 reason, Byte sync)
                    {
                        this.reason = reason;
                        this.sync = sync;
                    }
                }

                public delegate void DataEventHandler(object sender, BlueGecko.BLE.Events.Sync.DataEventArgs e);
                public class DataEventArgs : EventArgs
                {
                    public readonly Byte sync;
                    public readonly SByte tx_power;
                    public readonly SByte rssi;
                    public readonly Byte data_status;
                    public readonly Byte[] data;
                    public DataEventArgs(Byte sync, SByte tx_power, SByte rssi, Byte data_status, Byte[] data)
                    {
                        this.sync = sync;
                        this.tx_power = tx_power;
                        this.rssi = rssi;
                        this.data_status = data_status;
                        this.data = data;
                    }
                }

            }
            namespace LEConnection
            {
                public delegate void OpenedEventHandler(object sender, BlueGecko.BLE.Events.LEConnection.OpenedEventArgs e);
                public class OpenedEventArgs : EventArgs
                {
                    public readonly Byte[] address;
                    public readonly Byte address_type;
                    public readonly Byte master;
                    public readonly Byte connection;
                    public readonly Byte bonding;
                    public readonly Byte advertiser;
                    public OpenedEventArgs(Byte[] address, Byte address_type, Byte master, Byte connection, Byte bonding, Byte advertiser)
                    {
                        this.address = address;
                        this.address_type = address_type;
                        this.master = master;
                        this.connection = connection;
                        this.bonding = bonding;
                        this.advertiser = advertiser;
                    }
                }

                public delegate void ClosedEventHandler(object sender, BlueGecko.BLE.Events.LEConnection.ClosedEventArgs e);
                public class ClosedEventArgs : EventArgs
                {
                    public readonly UInt16 reason;
                    public readonly Byte connection;
                    public ClosedEventArgs(UInt16 reason, Byte connection)
                    {
                        this.reason = reason;
                        this.connection = connection;
                    }
                }

                public delegate void ParametersEventHandler(object sender, BlueGecko.BLE.Events.LEConnection.ParametersEventArgs e);
                public class ParametersEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 interval;
                    public readonly UInt16 latency;
                    public readonly UInt16 timeout;
                    public readonly Byte security_mode;
                    public readonly UInt16 txsize;
                    public ParametersEventArgs(Byte connection, UInt16 interval, UInt16 latency, UInt16 timeout, Byte security_mode, UInt16 txsize)
                    {
                        this.connection = connection;
                        this.interval = interval;
                        this.latency = latency;
                        this.timeout = timeout;
                        this.security_mode = security_mode;
                        this.txsize = txsize;
                    }
                }

                public delegate void RssiEventHandler(object sender, BlueGecko.BLE.Events.LEConnection.RssiEventArgs e);
                public class RssiEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly Byte status;
                    public readonly SByte rssi;
                    public RssiEventArgs(Byte connection, Byte status, SByte rssi)
                    {
                        this.connection = connection;
                        this.status = status;
                        this.rssi = rssi;
                    }
                }

                public delegate void PHYStatusEventHandler(object sender, BlueGecko.BLE.Events.LEConnection.PHYStatusEventArgs e);
                public class PHYStatusEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly Byte phy;
                    public PHYStatusEventArgs(Byte connection, Byte phy)
                    {
                        this.connection = connection;
                        this.phy = phy;
                    }
                }

            }
            namespace GATT
            {
                public delegate void MtuExchangedEventHandler(object sender, BlueGecko.BLE.Events.GATT.MtuExchangedEventArgs e);
                public class MtuExchangedEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 mtu;
                    public MtuExchangedEventArgs(Byte connection, UInt16 mtu)
                    {
                        this.connection = connection;
                        this.mtu = mtu;
                    }
                }

                public delegate void ServiceEventHandler(object sender, BlueGecko.BLE.Events.GATT.ServiceEventArgs e);
                public class ServiceEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt32 service;
                    public readonly Byte[] uuid;
                    public ServiceEventArgs(Byte connection, UInt32 service, Byte[] uuid)
                    {
                        this.connection = connection;
                        this.service = service;
                        this.uuid = uuid;
                    }
                }

                public delegate void CharacteristicEventHandler(object sender, BlueGecko.BLE.Events.GATT.CharacteristicEventArgs e);
                public class CharacteristicEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 characteristic;
                    public readonly Byte properties;
                    public readonly Byte[] uuid;
                    public CharacteristicEventArgs(Byte connection, UInt16 characteristic, Byte properties, Byte[] uuid)
                    {
                        this.connection = connection;
                        this.characteristic = characteristic;
                        this.properties = properties;
                        this.uuid = uuid;
                    }
                }

                public delegate void DescriptorEventHandler(object sender, BlueGecko.BLE.Events.GATT.DescriptorEventArgs e);
                public class DescriptorEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 descriptor;
                    public readonly Byte[] uuid;
                    public DescriptorEventArgs(Byte connection, UInt16 descriptor, Byte[] uuid)
                    {
                        this.connection = connection;
                        this.descriptor = descriptor;
                        this.uuid = uuid;
                    }
                }

                public delegate void CharacteristicValueEventHandler(object sender, BlueGecko.BLE.Events.GATT.CharacteristicValueEventArgs e);
                public class CharacteristicValueEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 characteristic;
                    public readonly Byte att_opcode;
                    public readonly UInt16 offset;
                    public readonly Byte[] value;
                    public CharacteristicValueEventArgs(Byte connection, UInt16 characteristic, Byte att_opcode, UInt16 offset, Byte[] value)
                    {
                        this.connection = connection;
                        this.characteristic = characteristic;
                        this.att_opcode = att_opcode;
                        this.offset = offset;
                        this.value = value;
                    }
                }

                public delegate void DescriptorValueEventHandler(object sender, BlueGecko.BLE.Events.GATT.DescriptorValueEventArgs e);
                public class DescriptorValueEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 descriptor;
                    public readonly UInt16 offset;
                    public readonly Byte[] value;
                    public DescriptorValueEventArgs(Byte connection, UInt16 descriptor, UInt16 offset, Byte[] value)
                    {
                        this.connection = connection;
                        this.descriptor = descriptor;
                        this.offset = offset;
                        this.value = value;
                    }
                }

                public delegate void ProcedureCompletedEventHandler(object sender, BlueGecko.BLE.Events.GATT.ProcedureCompletedEventArgs e);
                public class ProcedureCompletedEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public ProcedureCompletedEventArgs(Byte connection, UInt16 result)
                    {
                        this.connection = connection;
                        this.result = result;
                    }
                }

            }
            namespace GATTServer
            {
                public delegate void AttributeValueEventHandler(object sender, BlueGecko.BLE.Events.GATTServer.AttributeValueEventArgs e);
                public class AttributeValueEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 attribute;
                    public readonly Byte att_opcode;
                    public readonly UInt16 offset;
                    public readonly Byte[] value;
                    public AttributeValueEventArgs(Byte connection, UInt16 attribute, Byte att_opcode, UInt16 offset, Byte[] value)
                    {
                        this.connection = connection;
                        this.attribute = attribute;
                        this.att_opcode = att_opcode;
                        this.offset = offset;
                        this.value = value;
                    }
                }

                public delegate void UserReadRequestEventHandler(object sender, BlueGecko.BLE.Events.GATTServer.UserReadRequestEventArgs e);
                public class UserReadRequestEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 characteristic;
                    public readonly Byte att_opcode;
                    public readonly UInt16 offset;
                    public UserReadRequestEventArgs(Byte connection, UInt16 characteristic, Byte att_opcode, UInt16 offset)
                    {
                        this.connection = connection;
                        this.characteristic = characteristic;
                        this.att_opcode = att_opcode;
                        this.offset = offset;
                    }
                }

                public delegate void UserWriteRequestEventHandler(object sender, BlueGecko.BLE.Events.GATTServer.UserWriteRequestEventArgs e);
                public class UserWriteRequestEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 characteristic;
                    public readonly Byte att_opcode;
                    public readonly UInt16 offset;
                    public readonly Byte[] value;
                    public UserWriteRequestEventArgs(Byte connection, UInt16 characteristic, Byte att_opcode, UInt16 offset, Byte[] value)
                    {
                        this.connection = connection;
                        this.characteristic = characteristic;
                        this.att_opcode = att_opcode;
                        this.offset = offset;
                        this.value = value;
                    }
                }

                public delegate void CharacteristicStatusEventHandler(object sender, BlueGecko.BLE.Events.GATTServer.CharacteristicStatusEventArgs e);
                public class CharacteristicStatusEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 characteristic;
                    public readonly Byte status_flags;
                    public readonly UInt16 client_config_flags;
                    public CharacteristicStatusEventArgs(Byte connection, UInt16 characteristic, Byte status_flags, UInt16 client_config_flags)
                    {
                        this.connection = connection;
                        this.characteristic = characteristic;
                        this.status_flags = status_flags;
                        this.client_config_flags = client_config_flags;
                    }
                }

                public delegate void ExecuteWriteCompletedEventHandler(object sender, BlueGecko.BLE.Events.GATTServer.ExecuteWriteCompletedEventArgs e);
                public class ExecuteWriteCompletedEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 result;
                    public ExecuteWriteCompletedEventArgs(Byte connection, UInt16 result)
                    {
                        this.connection = connection;
                        this.result = result;
                    }
                }

            }
            namespace Hardware
            {
                public delegate void SoftTimerEventHandler(object sender, BlueGecko.BLE.Events.Hardware.SoftTimerEventArgs e);
                public class SoftTimerEventArgs : EventArgs
                {
                    public readonly Byte handle;
                    public SoftTimerEventArgs(Byte handle)
                    {
                        this.handle = handle;
                    }
                }

            }
            namespace Test
            {
                public delegate void DTMCompletedEventHandler(object sender, BlueGecko.BLE.Events.Test.DTMCompletedEventArgs e);
                public class DTMCompletedEventArgs : EventArgs
                {
                    public readonly UInt16 result;
                    public readonly UInt16 number_of_packets;
                    public DTMCompletedEventArgs(UInt16 result, UInt16 number_of_packets)
                    {
                        this.result = result;
                        this.number_of_packets = number_of_packets;
                    }
                }

            }
            namespace SM
            {
                public delegate void PasskeyDisplayEventHandler(object sender, BlueGecko.BLE.Events.SM.PasskeyDisplayEventArgs e);
                public class PasskeyDisplayEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt32 passkey;
                    public PasskeyDisplayEventArgs(Byte connection, UInt32 passkey)
                    {
                        this.connection = connection;
                        this.passkey = passkey;
                    }
                }

                public delegate void PasskeyRequestEventHandler(object sender, BlueGecko.BLE.Events.SM.PasskeyRequestEventArgs e);
                public class PasskeyRequestEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public PasskeyRequestEventArgs(Byte connection)
                    {
                        this.connection = connection;
                    }
                }

                public delegate void ConfirmPasskeyEventHandler(object sender, BlueGecko.BLE.Events.SM.ConfirmPasskeyEventArgs e);
                public class ConfirmPasskeyEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt32 passkey;
                    public ConfirmPasskeyEventArgs(Byte connection, UInt32 passkey)
                    {
                        this.connection = connection;
                        this.passkey = passkey;
                    }
                }

                public delegate void BondedEventHandler(object sender, BlueGecko.BLE.Events.SM.BondedEventArgs e);
                public class BondedEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly Byte bonding;
                    public BondedEventArgs(Byte connection, Byte bonding)
                    {
                        this.connection = connection;
                        this.bonding = bonding;
                    }
                }

                public delegate void BondingFailedEventHandler(object sender, BlueGecko.BLE.Events.SM.BondingFailedEventArgs e);
                public class BondingFailedEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 reason;
                    public BondingFailedEventArgs(Byte connection, UInt16 reason)
                    {
                        this.connection = connection;
                        this.reason = reason;
                    }
                }

                public delegate void ListBondingEntryEventHandler(object sender, BlueGecko.BLE.Events.SM.ListBondingEntryEventArgs e);
                public class ListBondingEntryEventArgs : EventArgs
                {
                    public readonly Byte bonding;
                    public readonly Byte[] address;
                    public readonly Byte address_type;
                    public ListBondingEntryEventArgs(Byte bonding, Byte[] address, Byte address_type)
                    {
                        this.bonding = bonding;
                        this.address = address;
                        this.address_type = address_type;
                    }
                }

                public delegate void ListAllBondingsCompleteEventHandler(object sender, BlueGecko.BLE.Events.SM.ListAllBondingsCompleteEventArgs e);
                public class ListAllBondingsCompleteEventArgs : EventArgs
                {
                    public ListAllBondingsCompleteEventArgs() { }
                }

                public delegate void ConfirmBondingEventHandler(object sender, BlueGecko.BLE.Events.SM.ConfirmBondingEventArgs e);
                public class ConfirmBondingEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly SByte bonding_handle;
                    public ConfirmBondingEventArgs(Byte connection, SByte bonding_handle)
                    {
                        this.connection = connection;
                        this.bonding_handle = bonding_handle;
                    }
                }

            }
            namespace Homekit
            {
                public delegate void SetupcodeDisplayEventHandler(object sender, BlueGecko.BLE.Events.Homekit.SetupcodeDisplayEventArgs e);
                public class SetupcodeDisplayEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly Byte[] setupcode;
                    public SetupcodeDisplayEventArgs(Byte connection, Byte[] setupcode)
                    {
                        this.connection = connection;
                        this.setupcode = setupcode;
                    }
                }

                public delegate void PairedEventHandler(object sender, BlueGecko.BLE.Events.Homekit.PairedEventArgs e);
                public class PairedEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 reason;
                    public PairedEventArgs(Byte connection, UInt16 reason)
                    {
                        this.connection = connection;
                        this.reason = reason;
                    }
                }

                public delegate void PairVerifiedEventHandler(object sender, BlueGecko.BLE.Events.Homekit.PairVerifiedEventArgs e);
                public class PairVerifiedEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 reason;
                    public PairVerifiedEventArgs(Byte connection, UInt16 reason)
                    {
                        this.connection = connection;
                        this.reason = reason;
                    }
                }

                public delegate void ConnectionOpenedEventHandler(object sender, BlueGecko.BLE.Events.Homekit.ConnectionOpenedEventArgs e);
                public class ConnectionOpenedEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public ConnectionOpenedEventArgs(Byte connection)
                    {
                        this.connection = connection;
                    }
                }

                public delegate void ConnectionClosedEventHandler(object sender, BlueGecko.BLE.Events.Homekit.ConnectionClosedEventArgs e);
                public class ConnectionClosedEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 reason;
                    public ConnectionClosedEventArgs(Byte connection, UInt16 reason)
                    {
                        this.connection = connection;
                        this.reason = reason;
                    }
                }

                public delegate void IdentifyEventHandler(object sender, BlueGecko.BLE.Events.Homekit.IdentifyEventArgs e);
                public class IdentifyEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public IdentifyEventArgs(Byte connection)
                    {
                        this.connection = connection;
                    }
                }

                public delegate void WriteRequestEventHandler(object sender, BlueGecko.BLE.Events.Homekit.WriteRequestEventArgs e);
                public class WriteRequestEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 characteristic;
                    public readonly UInt16 chr_value_size;
                    public readonly UInt16 authorization_size;
                    public readonly UInt16 value_offset;
                    public readonly Byte[] value;
                    public WriteRequestEventArgs(Byte connection, UInt16 characteristic, UInt16 chr_value_size, UInt16 authorization_size, UInt16 value_offset, Byte[] value)
                    {
                        this.connection = connection;
                        this.characteristic = characteristic;
                        this.chr_value_size = chr_value_size;
                        this.authorization_size = authorization_size;
                        this.value_offset = value_offset;
                        this.value = value;
                    }
                }

                public delegate void ReadRequestEventHandler(object sender, BlueGecko.BLE.Events.Homekit.ReadRequestEventArgs e);
                public class ReadRequestEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 characteristic;
                    public readonly UInt16 offset;
                    public ReadRequestEventArgs(Byte connection, UInt16 characteristic, UInt16 offset)
                    {
                        this.connection = connection;
                        this.characteristic = characteristic;
                        this.offset = offset;
                    }
                }

                public delegate void DisconnectionRequiredEventHandler(object sender, BlueGecko.BLE.Events.Homekit.DisconnectionRequiredEventArgs e);
                public class DisconnectionRequiredEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 reason;
                    public DisconnectionRequiredEventArgs(Byte connection, UInt16 reason)
                    {
                        this.connection = connection;
                        this.reason = reason;
                    }
                }

                public delegate void PairingRemovedEventHandler(object sender, BlueGecko.BLE.Events.Homekit.PairingRemovedEventArgs e);
                public class PairingRemovedEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 remaining_pairings;
                    public readonly Byte[] pairing_id;
                    public PairingRemovedEventArgs(Byte connection, UInt16 remaining_pairings, Byte[] pairing_id)
                    {
                        this.connection = connection;
                        this.remaining_pairings = remaining_pairings;
                        this.pairing_id = pairing_id;
                    }
                }

                public delegate void SetuppayloadDisplayEventHandler(object sender, BlueGecko.BLE.Events.Homekit.SetuppayloadDisplayEventArgs e);
                public class SetuppayloadDisplayEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly Byte[] setuppayload;
                    public SetuppayloadDisplayEventArgs(Byte connection, Byte[] setuppayload)
                    {
                        this.connection = connection;
                        this.setuppayload = setuppayload;
                    }
                }

            }
            namespace L2CAP
            {
                public delegate void CocConnectionRequestEventHandler(object sender, BlueGecko.BLE.Events.L2CAP.CocConnectionRequestEventArgs e);
                public class CocConnectionRequestEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 le_psm;
                    public readonly UInt16 source_cid;
                    public readonly UInt16 mtu;
                    public readonly UInt16 mps;
                    public readonly UInt16 initial_credit;
                    public readonly Byte flags;
                    public readonly Byte encryption_key_size;
                    public CocConnectionRequestEventArgs(Byte connection, UInt16 le_psm, UInt16 source_cid, UInt16 mtu, UInt16 mps, UInt16 initial_credit, Byte flags, Byte encryption_key_size)
                    {
                        this.connection = connection;
                        this.le_psm = le_psm;
                        this.source_cid = source_cid;
                        this.mtu = mtu;
                        this.mps = mps;
                        this.initial_credit = initial_credit;
                        this.flags = flags;
                        this.encryption_key_size = encryption_key_size;
                    }
                }

                public delegate void CocConnectionResponseEventHandler(object sender, BlueGecko.BLE.Events.L2CAP.CocConnectionResponseEventArgs e);
                public class CocConnectionResponseEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 destination_cid;
                    public readonly UInt16 mtu;
                    public readonly UInt16 mps;
                    public readonly UInt16 initial_credit;
                    public readonly UInt16 result;
                    public CocConnectionResponseEventArgs(Byte connection, UInt16 destination_cid, UInt16 mtu, UInt16 mps, UInt16 initial_credit, UInt16 result)
                    {
                        this.connection = connection;
                        this.destination_cid = destination_cid;
                        this.mtu = mtu;
                        this.mps = mps;
                        this.initial_credit = initial_credit;
                        this.result = result;
                    }
                }

                public delegate void CocLEFlowControlCreditEventHandler(object sender, BlueGecko.BLE.Events.L2CAP.CocLEFlowControlCreditEventArgs e);
                public class CocLEFlowControlCreditEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 cid;
                    public readonly UInt16 credits;
                    public CocLEFlowControlCreditEventArgs(Byte connection, UInt16 cid, UInt16 credits)
                    {
                        this.connection = connection;
                        this.cid = cid;
                        this.credits = credits;
                    }
                }

                public delegate void CocChannelDisconnectedEventHandler(object sender, BlueGecko.BLE.Events.L2CAP.CocChannelDisconnectedEventArgs e);
                public class CocChannelDisconnectedEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 cid;
                    public readonly UInt16 reason;
                    public CocChannelDisconnectedEventArgs(Byte connection, UInt16 cid, UInt16 reason)
                    {
                        this.connection = connection;
                        this.cid = cid;
                        this.reason = reason;
                    }
                }

                public delegate void CocDataEventHandler(object sender, BlueGecko.BLE.Events.L2CAP.CocDataEventArgs e);
                public class CocDataEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly UInt16 cid;
                    public readonly Byte[] data;
                    public CocDataEventArgs(Byte connection, UInt16 cid, Byte[] data)
                    {
                        this.connection = connection;
                        this.cid = cid;
                        this.data = data;
                    }
                }

                public delegate void CommandRejectedEventHandler(object sender, BlueGecko.BLE.Events.L2CAP.CommandRejectedEventArgs e);
                public class CommandRejectedEventArgs : EventArgs
                {
                    public readonly Byte connection;
                    public readonly Byte code;
                    public readonly UInt16 reason;
                    public CommandRejectedEventArgs(Byte connection, Byte code, UInt16 reason)
                    {
                        this.connection = connection;
                        this.code = code;
                        this.reason = reason;
                    }
                }

            }
            namespace CTEReceiver
            {
                public delegate void IqReportEventHandler(object sender, BlueGecko.BLE.Events.CTEReceiver.IqReportEventArgs e);
                public class IqReportEventArgs : EventArgs
                {
                    public readonly UInt16 status;
                    public readonly Byte packet_type;
                    public readonly Byte handle;
                    public readonly Byte phy;
                    public readonly Byte channel;
                    public readonly SByte rssi;
                    public readonly Byte rssi_antenna_id;
                    public readonly Byte cte_type;
                    public readonly Byte slot_durations;
                    public readonly UInt16 event_counter;
                    public readonly Byte completeness;
                    public readonly Byte[] samples;
                    public IqReportEventArgs(UInt16 status, Byte packet_type, Byte handle, Byte phy, Byte channel, SByte rssi, Byte rssi_antenna_id, Byte cte_type, Byte slot_durations, UInt16 event_counter, Byte completeness, Byte[] samples)
                    {
                        this.status = status;
                        this.packet_type = packet_type;
                        this.handle = handle;
                        this.phy = phy;
                        this.channel = channel;
                        this.rssi = rssi;
                        this.rssi_antenna_id = rssi_antenna_id;
                        this.cte_type = cte_type;
                        this.slot_durations = slot_durations;
                        this.event_counter = event_counter;
                        this.completeness = completeness;
                        this.samples = samples;
                    }
                }

            }
            namespace Qualtester
            {
                public delegate void StateChangedEventHandler(object sender, BlueGecko.BLE.Events.Qualtester.StateChangedEventArgs e);
                public class StateChangedEventArgs : EventArgs
                {
                    public readonly UInt32 group;
                    public readonly UInt32 id;
                    public readonly UInt32 value;
                    public readonly Byte[] data;
                    public StateChangedEventArgs(UInt32 group, UInt32 id, UInt32 value, Byte[] data)
                    {
                        this.group = group;
                        this.id = id;
                        this.value = value;
                        this.data = data;
                    }
                }

            }
            namespace User
            {
                public delegate void MessageToHostEventHandler(object sender, BlueGecko.BLE.Events.User.MessageToHostEventArgs e);
                public class MessageToHostEventArgs : EventArgs
                {
                    public readonly Byte[] data;
                    public MessageToHostEventArgs(Byte[] data)
                    {
                        this.data = data;
                    }
                }

            }
        }

    }

    public class BGLib
    {

        public Byte[] BLECommandDFUReset(Byte dfu)
        {
            return new Byte[] { 0x20, 1, 0, 0, dfu };
        }
        public Byte[] BLECommandDFUFlashSetAddress(UInt32 address)
        {
            return new Byte[] { 0x20, 4, 0, 1, (Byte)(address), (Byte)(address >> 8), (Byte)(address >> 16), (Byte)(address >> 24) };
        }
        public Byte[] BLECommandDFUFlashUpload(Byte[] data)
        {
            Byte[] cmd = new Byte[5 + data.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(1 + data.Length), 0, 2, (Byte)data.Length }, 0, cmd, 0, 5);
            Array.Copy(data, 0, cmd, 5, data.Length);
            return cmd;
        }
        public Byte[] BLECommandDFUFlashUploadFinish()
        {
            return new Byte[] { 0x20, 0, 0, 3 };
        }
        public Byte[] BLECommandSystemHello()
        {
            return new Byte[] { 0x20, 0, 1, 0 };
        }
        public Byte[] BLECommandSystemReset(Byte dfu)
        {
            return new Byte[] { 0x20, 1, 1, 1, dfu };
        }
        public Byte[] BLECommandSystemGetBtAddress()
        {
            return new Byte[] { 0x20, 0, 1, 3 };
        }
        public Byte[] BLECommandSystemSetBtAddress(Byte[] address)
        {
            Byte[] cmd = new Byte[10];
            Array.Copy(new Byte[] { 0x20, (Byte)(6), 1, 4, 0, 0, 0, 0, 0, 0 }, 0, cmd, 0, 10);
            Array.Copy(address, 0, cmd, 4, 6);
            return cmd;
        }
        public Byte[] BLECommandSystemSetTXPower(Int16 power)
        {
            return new Byte[] { 0x20, 2, 1, 10, (Byte)(power), (Byte)(power >> 8) };
        }
        public Byte[] BLECommandSystemGetRandomData(Byte length)
        {
            return new Byte[] { 0x20, 1, 1, 11, length };
        }
        public Byte[] BLECommandSystemHalt(Byte halt)
        {
            return new Byte[] { 0x20, 1, 1, 12, halt };
        }
        public Byte[] BLECommandSystemSetDeviceName(Byte type, Byte[] name)
        {
            Byte[] cmd = new Byte[6 + name.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(2 + name.Length), 1, 13, type, (Byte)name.Length }, 0, cmd, 0, 6);
            Array.Copy(name, 0, cmd, 6, name.Length);
            return cmd;
        }
        public Byte[] BLECommandSystemLinklayerConfigure(Byte key, Byte[] data)
        {
            Byte[] cmd = new Byte[6 + data.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(2 + data.Length), 1, 14, key, (Byte)data.Length }, 0, cmd, 0, 6);
            Array.Copy(data, 0, cmd, 6, data.Length);
            return cmd;
        }
        public Byte[] BLECommandSystemGetCounters(Byte reset)
        {
            return new Byte[] { 0x20, 1, 1, 15, reset };
        }
        public Byte[] BLECommandSystemDataBufferWrite(Byte[] data)
        {
            Byte[] cmd = new Byte[5 + data.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(1 + data.Length), 1, 18, (Byte)data.Length }, 0, cmd, 0, 5);
            Array.Copy(data, 0, cmd, 5, data.Length);
            return cmd;
        }
        public Byte[] BLECommandSystemSetIdentityAddress(Byte[] address, Byte type)
        {
            Byte[] cmd = new Byte[11];
            Array.Copy(new Byte[] { 0x20, (Byte)(7), 1, 19, 0, 0, 0, 0, 0, 0, type }, 0, cmd, 0, 11);
            Array.Copy(address, 0, cmd, 4, 6);
            return cmd;
        }
        public Byte[] BLECommandSystemDataBufferClear()
        {
            return new Byte[] { 0x20, 0, 1, 20 };
        }
        public Byte[] BLECommandLEGAPOpen(Byte[] address, Byte address_type)
        {
            Byte[] cmd = new Byte[11];
            Array.Copy(new Byte[] { 0x20, (Byte)(7), 3, 0, 0, 0, 0, 0, 0, 0, address_type }, 0, cmd, 0, 11);
            Array.Copy(address, 0, cmd, 4, 6);
            return cmd;
        }
        public Byte[] BLECommandLEGAPSetMode(Byte discover, Byte connect)
        {
            return new Byte[] { 0x20, 2, 3, 1, discover, connect };
        }
        public Byte[] BLECommandLEGAPDiscover(Byte mode)
        {
            return new Byte[] { 0x20, 1, 3, 2, mode };
        }
        public Byte[] BLECommandLEGAPEndProcedure()
        {
            return new Byte[] { 0x20, 0, 3, 3 };
        }
        public Byte[] BLECommandLEGAPSetAdvParameters(UInt16 interval_min, UInt16 interval_max, Byte channel_map)
        {
            return new Byte[] { 0x20, 5, 3, 4, (Byte)(interval_min), (Byte)(interval_min >> 8), (Byte)(interval_max), (Byte)(interval_max >> 8), channel_map };
        }
        public Byte[] BLECommandLEGAPSetConnParameters(UInt16 min_interval, UInt16 max_interval, UInt16 latency, UInt16 timeout)
        {
            return new Byte[] { 0x20, 8, 3, 5, (Byte)(min_interval), (Byte)(min_interval >> 8), (Byte)(max_interval), (Byte)(max_interval >> 8), (Byte)(latency), (Byte)(latency >> 8), (Byte)(timeout), (Byte)(timeout >> 8) };
        }
        public Byte[] BLECommandLEGAPSetScanParameters(UInt16 scan_interval, UInt16 scan_window, Byte active)
        {
            return new Byte[] { 0x20, 5, 3, 6, (Byte)(scan_interval), (Byte)(scan_interval >> 8), (Byte)(scan_window), (Byte)(scan_window >> 8), active };
        }
        public Byte[] BLECommandLEGAPSetAdvData(Byte scan_rsp, Byte[] adv_data)
        {
            Byte[] cmd = new Byte[6 + adv_data.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(2 + adv_data.Length), 3, 7, scan_rsp, (Byte)adv_data.Length }, 0, cmd, 0, 6);
            Array.Copy(adv_data, 0, cmd, 6, adv_data.Length);
            return cmd;
        }
        public Byte[] BLECommandLEGAPSetAdvTimeout(Byte maxevents)
        {
            return new Byte[] { 0x20, 1, 3, 8, maxevents };
        }
        public Byte[] BLECommandLEGAPSetConnPHY(Byte preferred_phy, Byte accepted_phy)
        {
            return new Byte[] { 0x20, 2, 3, 9, preferred_phy, accepted_phy };
        }
        public Byte[] BLECommandLEGAPBt5SetMode(Byte handle, Byte discover, Byte connect, UInt16 maxevents, Byte address_type)
        {
            return new Byte[] { 0x20, 6, 3, 10, handle, discover, connect, (Byte)(maxevents), (Byte)(maxevents >> 8), address_type };
        }
        public Byte[] BLECommandLEGAPBt5SetAdvParameters(Byte handle, UInt16 interval_min, UInt16 interval_max, Byte channel_map, Byte report_scan)
        {
            return new Byte[] { 0x20, 7, 3, 11, handle, (Byte)(interval_min), (Byte)(interval_min >> 8), (Byte)(interval_max), (Byte)(interval_max >> 8), channel_map, report_scan };
        }
        public Byte[] BLECommandLEGAPBt5SetAdvData(Byte handle, Byte scan_rsp, Byte[] adv_data)
        {
            Byte[] cmd = new Byte[7 + adv_data.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(3 + adv_data.Length), 3, 12, handle, scan_rsp, (Byte)adv_data.Length }, 0, cmd, 0, 7);
            Array.Copy(adv_data, 0, cmd, 7, adv_data.Length);
            return cmd;
        }
        public Byte[] BLECommandLEGAPSetPrivacyMode(Byte privacy, Byte interval)
        {
            return new Byte[] { 0x20, 2, 3, 13, privacy, interval };
        }
        public Byte[] BLECommandLEGAPSetAdvertiseTiming(Byte handle, UInt32 interval_min, UInt32 interval_max, UInt16 duration, Byte maxevents)
        {
            return new Byte[] { 0x20, 12, 3, 14, handle, (Byte)(interval_min), (Byte)(interval_min >> 8), (Byte)(interval_min >> 16), (Byte)(interval_min >> 24), (Byte)(interval_max), (Byte)(interval_max >> 8), (Byte)(interval_max >> 16), (Byte)(interval_max >> 24), (Byte)(duration), (Byte)(duration >> 8), maxevents };
        }
        public Byte[] BLECommandLEGAPSetAdvertiseChannelMap(Byte handle, Byte channel_map)
        {
            return new Byte[] { 0x20, 2, 3, 15, handle, channel_map };
        }
        public Byte[] BLECommandLEGAPSetAdvertiseReportScanRequest(Byte handle, Byte report_scan_req)
        {
            return new Byte[] { 0x20, 2, 3, 16, handle, report_scan_req };
        }
        public Byte[] BLECommandLEGAPSetAdvertisePHY(Byte handle, Byte primary_phy, Byte secondary_phy)
        {
            return new Byte[] { 0x20, 3, 3, 17, handle, primary_phy, secondary_phy };
        }
        public Byte[] BLECommandLEGAPSetAdvertiseConfiguration(Byte handle, UInt32 configurations)
        {
            return new Byte[] { 0x20, 5, 3, 18, handle, (Byte)(configurations), (Byte)(configurations >> 8), (Byte)(configurations >> 16), (Byte)(configurations >> 24) };
        }
        public Byte[] BLECommandLEGAPClearAdvertiseConfiguration(Byte handle, UInt32 configurations)
        {
            return new Byte[] { 0x20, 5, 3, 19, handle, (Byte)(configurations), (Byte)(configurations >> 8), (Byte)(configurations >> 16), (Byte)(configurations >> 24) };
        }
        public Byte[] BLECommandLEGAPStartAdvertising(Byte handle, Byte discover, Byte connect)
        {
            return new Byte[] { 0x20, 3, 3, 20, handle, discover, connect };
        }
        public Byte[] BLECommandLEGAPStopAdvertising(Byte handle)
        {
            return new Byte[] { 0x20, 1, 3, 21, handle };
        }
        public Byte[] BLECommandLEGAPSetDiscoveryTiming(Byte phys, UInt16 scan_interval, UInt16 scan_window)
        {
            return new Byte[] { 0x20, 5, 3, 22, phys, (Byte)(scan_interval), (Byte)(scan_interval >> 8), (Byte)(scan_window), (Byte)(scan_window >> 8) };
        }
        public Byte[] BLECommandLEGAPSetDiscoveryType(Byte phys, Byte scan_type)
        {
            return new Byte[] { 0x20, 2, 3, 23, phys, scan_type };
        }
        public Byte[] BLECommandLEGAPStartDiscovery(Byte scanning_phy, Byte mode)
        {
            return new Byte[] { 0x20, 2, 3, 24, scanning_phy, mode };
        }
        public Byte[] BLECommandLEGAPSetDataChannelClassification(Byte[] channel_map)
        {
            Byte[] cmd = new Byte[5 + channel_map.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(1 + channel_map.Length), 3, 25, (Byte)channel_map.Length }, 0, cmd, 0, 5);
            Array.Copy(channel_map, 0, cmd, 5, channel_map.Length);
            return cmd;
        }
        public Byte[] BLECommandLEGAPConnect(Byte[] address, Byte address_type, Byte initiating_phy)
        {
            Byte[] cmd = new Byte[12];
            Array.Copy(new Byte[] { 0x20, (Byte)(8), 3, 26, 0, 0, 0, 0, 0, 0, address_type, initiating_phy }, 0, cmd, 0, 12);
            Array.Copy(address, 0, cmd, 4, 6);
            return cmd;
        }
        public Byte[] BLECommandLEGAPSetAdvertiseTXPower(Byte handle, Int16 power)
        {
            return new Byte[] { 0x20, 3, 3, 27, handle, (Byte)(power), (Byte)(power >> 8) };
        }
        public Byte[] BLECommandLEGAPSetDiscoveryExtendedScanResponse(Byte enable)
        {
            return new Byte[] { 0x20, 1, 3, 28, enable };
        }
        public Byte[] BLECommandLEGAPStartPeriodicAdvertising(Byte handle, UInt16 interval_min, UInt16 interval_max, UInt32 flags)
        {
            return new Byte[] { 0x20, 9, 3, 29, handle, (Byte)(interval_min), (Byte)(interval_min >> 8), (Byte)(interval_max), (Byte)(interval_max >> 8), (Byte)(flags), (Byte)(flags >> 8), (Byte)(flags >> 16), (Byte)(flags >> 24) };
        }
        public Byte[] BLECommandLEGAPStopPeriodicAdvertising(Byte handle)
        {
            return new Byte[] { 0x20, 1, 3, 31, handle };
        }
        public Byte[] BLECommandLEGAPSetLongAdvertisingData(Byte handle, Byte packet_type)
        {
            return new Byte[] { 0x20, 2, 3, 32, handle, packet_type };
        }
        public Byte[] BLECommandLEGAPEnableWhitelisting(Byte enable)
        {
            return new Byte[] { 0x20, 1, 3, 33, enable };
        }
        public Byte[] BLECommandLEGAPSetConnTimingParameters(UInt16 min_interval, UInt16 max_interval, UInt16 latency, UInt16 timeout, UInt16 min_ce_length, UInt16 max_ce_length)
        {
            return new Byte[] { 0x20, 12, 3, 34, (Byte)(min_interval), (Byte)(min_interval >> 8), (Byte)(max_interval), (Byte)(max_interval >> 8), (Byte)(latency), (Byte)(latency >> 8), (Byte)(timeout), (Byte)(timeout >> 8), (Byte)(min_ce_length), (Byte)(min_ce_length >> 8), (Byte)(max_ce_length), (Byte)(max_ce_length >> 8) };
        }
        public Byte[] BLECommandLEGAPSetAdvertiseRandomAddress(Byte handle, Byte addr_type, Byte[] address)
        {
            Byte[] cmd = new Byte[12];
            Array.Copy(new Byte[] { 0x20, (Byte)(8), 3, 37, handle, addr_type, 0, 0, 0, 0, 0, 0 }, 0, cmd, 0, 12);
            Array.Copy(address, 0, cmd, 6, 6);
            return cmd;
        }
        public Byte[] BLECommandLEGAPClearAdvertiseRandomAddress(Byte handle)
        {
            return new Byte[] { 0x20, 1, 3, 38, handle };
        }
        public Byte[] BLECommandSyncOpen(Byte adv_sid, UInt16 skip, UInt16 timeout, Byte[] address, Byte address_type)
        {
            Byte[] cmd = new Byte[16];
            Array.Copy(new Byte[] { 0x20, (Byte)(12), 66, 0, adv_sid, (Byte)(skip), (Byte)(skip >> 8), (Byte)(timeout), (Byte)(timeout >> 8), 0, 0, 0, 0, 0, 0, address_type }, 0, cmd, 0, 16);
            Array.Copy(address, 0, cmd, 9, 6);
            return cmd;
        }
        public Byte[] BLECommandSyncClose(Byte sync)
        {
            return new Byte[] { 0x20, 1, 66, 1, sync };
        }
        public Byte[] BLECommandLEConnectionSetParameters(Byte connection, UInt16 min_interval, UInt16 max_interval, UInt16 latency, UInt16 timeout)
        {
            return new Byte[] { 0x20, 9, 8, 0, connection, (Byte)(min_interval), (Byte)(min_interval >> 8), (Byte)(max_interval), (Byte)(max_interval >> 8), (Byte)(latency), (Byte)(latency >> 8), (Byte)(timeout), (Byte)(timeout >> 8) };
        }
        public Byte[] BLECommandLEConnectionGetRssi(Byte connection)
        {
            return new Byte[] { 0x20, 1, 8, 1, connection };
        }
        public Byte[] BLECommandLEConnectionDisableSlaveLatency(Byte connection, Byte disable)
        {
            return new Byte[] { 0x20, 2, 8, 2, connection, disable };
        }
        public Byte[] BLECommandLEConnectionSetPHY(Byte connection, Byte phy)
        {
            return new Byte[] { 0x20, 2, 8, 3, connection, phy };
        }
        public Byte[] BLECommandLEConnectionClose(Byte connection)
        {
            return new Byte[] { 0x20, 1, 8, 4, connection };
        }
        public Byte[] BLECommandLEConnectionSetTimingParameters(Byte connection, UInt16 min_interval, UInt16 max_interval, UInt16 latency, UInt16 timeout, UInt16 min_ce_length, UInt16 max_ce_length)
        {
            return new Byte[] { 0x20, 13, 8, 5, connection, (Byte)(min_interval), (Byte)(min_interval >> 8), (Byte)(max_interval), (Byte)(max_interval >> 8), (Byte)(latency), (Byte)(latency >> 8), (Byte)(timeout), (Byte)(timeout >> 8), (Byte)(min_ce_length), (Byte)(min_ce_length >> 8), (Byte)(max_ce_length), (Byte)(max_ce_length >> 8) };
        }
        public Byte[] BLECommandLEConnectionReadChannelMap(Byte connection)
        {
            return new Byte[] { 0x20, 1, 8, 6, connection };
        }
        public Byte[] BLECommandLEConnectionSetPreferredPHY(Byte connection, Byte preferred_phy, Byte accepted_phy)
        {
            return new Byte[] { 0x20, 3, 8, 7, connection, preferred_phy, accepted_phy };
        }
        public Byte[] BLECommandGATTSetMaxMtu(UInt16 max_mtu)
        {
            return new Byte[] { 0x20, 2, 9, 0, (Byte)(max_mtu), (Byte)(max_mtu >> 8) };
        }
        public Byte[] BLECommandGATTDiscoverPrimaryServices(Byte connection)
        {
            return new Byte[] { 0x20, 1, 9, 1, connection };
        }
        public Byte[] BLECommandGATTDiscoverPrimaryServicesByUUID(Byte connection, Byte[] uuid)
        {
            Byte[] cmd = new Byte[6 + uuid.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(2 + uuid.Length), 9, 2, connection, (Byte)uuid.Length }, 0, cmd, 0, 6);
            Array.Copy(uuid, 0, cmd, 6, uuid.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTDiscoverCharacteristics(Byte connection, UInt32 service)
        {
            return new Byte[] { 0x20, 5, 9, 3, connection, (Byte)(service), (Byte)(service >> 8), (Byte)(service >> 16), (Byte)(service >> 24) };
        }
        public Byte[] BLECommandGATTDiscoverCharacteristicsByUUID(Byte connection, UInt32 service, Byte[] uuid)
        {
            Byte[] cmd = new Byte[10 + uuid.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(6 + uuid.Length), 9, 4, connection, (Byte)(service), (Byte)(service >> 8), (Byte)(service >> 16), (Byte)(service >> 24), (Byte)uuid.Length }, 0, cmd, 0, 10);
            Array.Copy(uuid, 0, cmd, 10, uuid.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTSetCharacteristicNotification(Byte connection, UInt16 characteristic, Byte flags)
        {
            return new Byte[] { 0x20, 4, 9, 5, connection, (Byte)(characteristic), (Byte)(characteristic >> 8), flags };
        }
        public Byte[] BLECommandGATTDiscoverDescriptors(Byte connection, UInt16 characteristic)
        {
            return new Byte[] { 0x20, 3, 9, 6, connection, (Byte)(characteristic), (Byte)(characteristic >> 8) };
        }
        public Byte[] BLECommandGATTReadCharacteristicValue(Byte connection, UInt16 characteristic)
        {
            return new Byte[] { 0x20, 3, 9, 7, connection, (Byte)(characteristic), (Byte)(characteristic >> 8) };
        }
        public Byte[] BLECommandGATTReadCharacteristicValueByUUID(Byte connection, UInt32 service, Byte[] uuid)
        {
            Byte[] cmd = new Byte[10 + uuid.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(6 + uuid.Length), 9, 8, connection, (Byte)(service), (Byte)(service >> 8), (Byte)(service >> 16), (Byte)(service >> 24), (Byte)uuid.Length }, 0, cmd, 0, 10);
            Array.Copy(uuid, 0, cmd, 10, uuid.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTWriteCharacteristicValue(Byte connection, UInt16 characteristic, Byte[] value)
        {
            Byte[] cmd = new Byte[8 + value.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(4 + value.Length), 9, 9, connection, (Byte)(characteristic), (Byte)(characteristic >> 8), (Byte)value.Length }, 0, cmd, 0, 8);
            Array.Copy(value, 0, cmd, 8, value.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTWriteCharacteristicValueWithoutResponse(Byte connection, UInt16 characteristic, Byte[] value)
        {
            Byte[] cmd = new Byte[8 + value.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(4 + value.Length), 9, 10, connection, (Byte)(characteristic), (Byte)(characteristic >> 8), (Byte)value.Length }, 0, cmd, 0, 8);
            Array.Copy(value, 0, cmd, 8, value.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTPrepareCharacteristicValueWrite(Byte connection, UInt16 characteristic, UInt16 offset, Byte[] value)
        {
            Byte[] cmd = new Byte[10 + value.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(6 + value.Length), 9, 11, connection, (Byte)(characteristic), (Byte)(characteristic >> 8), (Byte)(offset), (Byte)(offset >> 8), (Byte)value.Length }, 0, cmd, 0, 10);
            Array.Copy(value, 0, cmd, 10, value.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTExecuteCharacteristicValueWrite(Byte connection, Byte flags)
        {
            return new Byte[] { 0x20, 2, 9, 12, connection, flags };
        }
        public Byte[] BLECommandGATTSendCharacteristicConfirmation(Byte connection)
        {
            return new Byte[] { 0x20, 1, 9, 13, connection };
        }
        public Byte[] BLECommandGATTReadDescriptorValue(Byte connection, UInt16 descriptor)
        {
            return new Byte[] { 0x20, 3, 9, 14, connection, (Byte)(descriptor), (Byte)(descriptor >> 8) };
        }
        public Byte[] BLECommandGATTWriteDescriptorValue(Byte connection, UInt16 descriptor, Byte[] value)
        {
            Byte[] cmd = new Byte[8 + value.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(4 + value.Length), 9, 15, connection, (Byte)(descriptor), (Byte)(descriptor >> 8), (Byte)value.Length }, 0, cmd, 0, 8);
            Array.Copy(value, 0, cmd, 8, value.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTFindIncludedServices(Byte connection, UInt32 service)
        {
            return new Byte[] { 0x20, 5, 9, 16, connection, (Byte)(service), (Byte)(service >> 8), (Byte)(service >> 16), (Byte)(service >> 24) };
        }
        public Byte[] BLECommandGATTReadMultipleCharacteristicValues(Byte connection, Byte[] characteristic_list)
        {
            Byte[] cmd = new Byte[6 + characteristic_list.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(2 + characteristic_list.Length), 9, 17, connection, (Byte)characteristic_list.Length }, 0, cmd, 0, 6);
            Array.Copy(characteristic_list, 0, cmd, 6, characteristic_list.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTReadCharacteristicValueFromOffset(Byte connection, UInt16 characteristic, UInt16 offset, UInt16 maxlen)
        {
            return new Byte[] { 0x20, 7, 9, 18, connection, (Byte)(characteristic), (Byte)(characteristic >> 8), (Byte)(offset), (Byte)(offset >> 8), (Byte)(maxlen), (Byte)(maxlen >> 8) };
        }
        public Byte[] BLECommandGATTPrepareCharacteristicValueReliableWrite(Byte connection, UInt16 characteristic, UInt16 offset, Byte[] value)
        {
            Byte[] cmd = new Byte[10 + value.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(6 + value.Length), 9, 19, connection, (Byte)(characteristic), (Byte)(characteristic >> 8), (Byte)(offset), (Byte)(offset >> 8), (Byte)value.Length }, 0, cmd, 0, 10);
            Array.Copy(value, 0, cmd, 10, value.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTServerReadAttributeValue(UInt16 attribute, UInt16 offset)
        {
            return new Byte[] { 0x20, 4, 10, 0, (Byte)(attribute), (Byte)(attribute >> 8), (Byte)(offset), (Byte)(offset >> 8) };
        }
        public Byte[] BLECommandGATTServerReadAttributeType(UInt16 attribute)
        {
            return new Byte[] { 0x20, 2, 10, 1, (Byte)(attribute), (Byte)(attribute >> 8) };
        }
        public Byte[] BLECommandGATTServerWriteAttributeValue(UInt16 attribute, UInt16 offset, Byte[] value)
        {
            Byte[] cmd = new Byte[9 + value.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(5 + value.Length), 10, 2, (Byte)(attribute), (Byte)(attribute >> 8), (Byte)(offset), (Byte)(offset >> 8), (Byte)value.Length }, 0, cmd, 0, 9);
            Array.Copy(value, 0, cmd, 9, value.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTServerSendUserReadResponse(Byte connection, UInt16 characteristic, Byte att_errorcode, Byte[] value)
        {
            Byte[] cmd = new Byte[9 + value.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(5 + value.Length), 10, 3, connection, (Byte)(characteristic), (Byte)(characteristic >> 8), att_errorcode, (Byte)value.Length }, 0, cmd, 0, 9);
            Array.Copy(value, 0, cmd, 9, value.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTServerSendUserWriteResponse(Byte connection, UInt16 characteristic, Byte att_errorcode)
        {
            return new Byte[] { 0x20, 4, 10, 4, connection, (Byte)(characteristic), (Byte)(characteristic >> 8), att_errorcode };
        }
        public Byte[] BLECommandGATTServerSendCharacteristicNotification(Byte connection, UInt16 characteristic, Byte[] value)
        {
            Byte[] cmd = new Byte[8 + value.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(4 + value.Length), 10, 5, connection, (Byte)(characteristic), (Byte)(characteristic >> 8), (Byte)value.Length }, 0, cmd, 0, 8);
            Array.Copy(value, 0, cmd, 8, value.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTServerFindAttribute(UInt16 start, Byte[] type)
        {
            Byte[] cmd = new Byte[7 + type.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(3 + type.Length), 10, 6, (Byte)(start), (Byte)(start >> 8), (Byte)type.Length }, 0, cmd, 0, 7);
            Array.Copy(type, 0, cmd, 7, type.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTServerSetCapabilities(UInt32 caps, UInt32 reserved)
        {
            return new Byte[] { 0x20, 8, 10, 8, (Byte)(caps), (Byte)(caps >> 8), (Byte)(caps >> 16), (Byte)(caps >> 24), (Byte)(reserved), (Byte)(reserved >> 8), (Byte)(reserved >> 16), (Byte)(reserved >> 24) };
        }
        public Byte[] BLECommandGATTServerFindPrimaryService(UInt16 start, Byte[] uuid)
        {
            Byte[] cmd = new Byte[7 + uuid.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(3 + uuid.Length), 10, 9, (Byte)(start), (Byte)(start >> 8), (Byte)uuid.Length }, 0, cmd, 0, 7);
            Array.Copy(uuid, 0, cmd, 7, uuid.Length);
            return cmd;
        }
        public Byte[] BLECommandGATTServerSetMaxMtu(UInt16 max_mtu)
        {
            return new Byte[] { 0x20, 2, 10, 10, (Byte)(max_mtu), (Byte)(max_mtu >> 8) };
        }
        public Byte[] BLECommandGATTServerGetMtu(Byte connection)
        {
            return new Byte[] { 0x20, 1, 10, 11, connection };
        }
        public Byte[] BLECommandGATTServerEnableCapabilities(UInt32 caps)
        {
            return new Byte[] { 0x20, 4, 10, 12, (Byte)(caps), (Byte)(caps >> 8), (Byte)(caps >> 16), (Byte)(caps >> 24) };
        }
        public Byte[] BLECommandGATTServerDisableCapabilities(UInt32 caps)
        {
            return new Byte[] { 0x20, 4, 10, 13, (Byte)(caps), (Byte)(caps >> 8), (Byte)(caps >> 16), (Byte)(caps >> 24) };
        }
        public Byte[] BLECommandGATTServerGetEnabledCapabilities()
        {
            return new Byte[] { 0x20, 0, 10, 14 };
        }
        public Byte[] BLECommandHardwareSetSoftTimer(UInt32 time, Byte handle, Byte single_shot)
        {
            return new Byte[] { 0x20, 6, 12, 0, (Byte)(time), (Byte)(time >> 8), (Byte)(time >> 16), (Byte)(time >> 24), handle, single_shot };
        }
        public Byte[] BLECommandHardwareGetTime()
        {
            return new Byte[] { 0x20, 0, 12, 11 };
        }
        public Byte[] BLECommandHardwareSetLazySoftTimer(UInt32 time, UInt32 slack, Byte handle, Byte single_shot)
        {
            return new Byte[] { 0x20, 10, 12, 12, (Byte)(time), (Byte)(time >> 8), (Byte)(time >> 16), (Byte)(time >> 24), (Byte)(slack), (Byte)(slack >> 8), (Byte)(slack >> 16), (Byte)(slack >> 24), handle, single_shot };
        }
        public Byte[] BLECommandFlashPSEraseAll()
        {
            return new Byte[] { 0x20, 0, 13, 1 };
        }
        public Byte[] BLECommandFlashPSSave(UInt16 key, Byte[] value)
        {
            Byte[] cmd = new Byte[7 + value.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(3 + value.Length), 13, 2, (Byte)(key), (Byte)(key >> 8), (Byte)value.Length }, 0, cmd, 0, 7);
            Array.Copy(value, 0, cmd, 7, value.Length);
            return cmd;
        }
        public Byte[] BLECommandFlashPSLoad(UInt16 key)
        {
            return new Byte[] { 0x20, 2, 13, 3, (Byte)(key), (Byte)(key >> 8) };
        }
        public Byte[] BLECommandFlashPSErase(UInt16 key)
        {
            return new Byte[] { 0x20, 2, 13, 4, (Byte)(key), (Byte)(key >> 8) };
        }
        public Byte[] BLECommandTestDTMTX(Byte packet_type, Byte length, Byte channel, Byte phy)
        {
            return new Byte[] { 0x20, 4, 14, 0, packet_type, length, channel, phy };
        }
        public Byte[] BLECommandTestDTMRX(Byte channel, Byte phy)
        {
            return new Byte[] { 0x20, 2, 14, 1, channel, phy };
        }
        public Byte[] BLECommandTestDTMEnd()
        {
            return new Byte[] { 0x20, 0, 14, 2 };
        }
        public Byte[] BLECommandTestDebugCommand(Byte id, Byte[] debugdata)
        {
            Byte[] cmd = new Byte[6 + debugdata.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(2 + debugdata.Length), 14, 7, id, (Byte)debugdata.Length }, 0, cmd, 0, 6);
            Array.Copy(debugdata, 0, cmd, 6, debugdata.Length);
            return cmd;
        }
        public Byte[] BLECommandTestDebugCounter(UInt32 id)
        {
            return new Byte[] { 0x20, 4, 14, 12, (Byte)(id), (Byte)(id >> 8), (Byte)(id >> 16), (Byte)(id >> 24) };
        }
        public Byte[] BLECommandSMSetBondableMode(Byte bondable)
        {
            return new Byte[] { 0x20, 1, 15, 0, bondable };
        }
        public Byte[] BLECommandSMConfigure(Byte flags, Byte io_capabilities)
        {
            return new Byte[] { 0x20, 2, 15, 1, flags, io_capabilities };
        }
        public Byte[] BLECommandSMStoreBondingConfiguration(Byte max_bonding_count, Byte policy_flags)
        {
            return new Byte[] { 0x20, 2, 15, 2, max_bonding_count, policy_flags };
        }
        public Byte[] BLECommandSMIncreaseSecurity(Byte connection)
        {
            return new Byte[] { 0x20, 1, 15, 4, connection };
        }
        public Byte[] BLECommandSMDeleteBonding(Byte bonding)
        {
            return new Byte[] { 0x20, 1, 15, 6, bonding };
        }
        public Byte[] BLECommandSMDeleteBondings()
        {
            return new Byte[] { 0x20, 0, 15, 7 };
        }
        public Byte[] BLECommandSMEnterPasskey(Byte connection, Int32 passkey)
        {
            return new Byte[] { 0x20, 5, 15, 8, connection, (Byte)(passkey), (Byte)(passkey >> 8), (Byte)(passkey >> 16), (Byte)(passkey >> 24) };
        }
        public Byte[] BLECommandSMPasskeyConfirm(Byte connection, Byte confirm)
        {
            return new Byte[] { 0x20, 2, 15, 9, connection, confirm };
        }
        public Byte[] BLECommandSMSetOobData(Byte[] oob_data)
        {
            Byte[] cmd = new Byte[5 + oob_data.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(1 + oob_data.Length), 15, 10, (Byte)oob_data.Length }, 0, cmd, 0, 5);
            Array.Copy(oob_data, 0, cmd, 5, oob_data.Length);
            return cmd;
        }
        public Byte[] BLECommandSMListAllBondings()
        {
            return new Byte[] { 0x20, 0, 15, 11 };
        }
        public Byte[] BLECommandSMBondingConfirm(Byte connection, Byte confirm)
        {
            return new Byte[] { 0x20, 2, 15, 14, connection, confirm };
        }
        public Byte[] BLECommandSMSetDebugMode()
        {
            return new Byte[] { 0x20, 0, 15, 15 };
        }
        public Byte[] BLECommandSMSetPasskey(Int32 passkey)
        {
            return new Byte[] { 0x20, 4, 15, 16, (Byte)(passkey), (Byte)(passkey >> 8), (Byte)(passkey >> 16), (Byte)(passkey >> 24) };
        }
        public Byte[] BLECommandSMUseScOob(Byte enable)
        {
            return new Byte[] { 0x20, 1, 15, 17, enable };
        }
        public Byte[] BLECommandSMSetScRemoteOobData(Byte[] oob_data)
        {
            Byte[] cmd = new Byte[5 + oob_data.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(1 + oob_data.Length), 15, 18, (Byte)oob_data.Length }, 0, cmd, 0, 5);
            Array.Copy(oob_data, 0, cmd, 5, oob_data.Length);
            return cmd;
        }
        public Byte[] BLECommandSMAddToWhitelist(Byte[] address, Byte address_type)
        {
            Byte[] cmd = new Byte[11];
            Array.Copy(new Byte[] { 0x20, (Byte)(7), 15, 19, 0, 0, 0, 0, 0, 0, address_type }, 0, cmd, 0, 11);
            Array.Copy(address, 0, cmd, 4, 6);
            return cmd;
        }
        public Byte[] BLECommandSMSetMinimumKeySize(Byte minimum_key_size)
        {
            return new Byte[] { 0x20, 1, 15, 20, minimum_key_size };
        }
        public Byte[] BLECommandHomekitConfigure(Byte i2c_address, Byte support_display, Byte hap_attribute_features, UInt16 category, Byte configuration_number, UInt16 fast_advert_interval, UInt16 fast_advert_timeout, UInt32 flag, UInt16 broadcast_advert_timeout, Byte[] model_name)
        {
            Byte[] cmd = new Byte[21 + model_name.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(17 + model_name.Length), 19, 0, i2c_address, support_display, hap_attribute_features, (Byte)(category), (Byte)(category >> 8), configuration_number, (Byte)(fast_advert_interval), (Byte)(fast_advert_interval >> 8), (Byte)(fast_advert_timeout), (Byte)(fast_advert_timeout >> 8), (Byte)(flag), (Byte)(flag >> 8), (Byte)(flag >> 16), (Byte)(flag >> 24), (Byte)(broadcast_advert_timeout), (Byte)(broadcast_advert_timeout >> 8), (Byte)model_name.Length }, 0, cmd, 0, 21);
            Array.Copy(model_name, 0, cmd, 21, model_name.Length);
            return cmd;
        }
        public Byte[] BLECommandHomekitAdvertise(Byte enable, UInt16 interval_min, UInt16 interval_max, Byte channel_map)
        {
            return new Byte[] { 0x20, 6, 19, 1, enable, (Byte)(interval_min), (Byte)(interval_min >> 8), (Byte)(interval_max), (Byte)(interval_max >> 8), channel_map };
        }
        public Byte[] BLECommandHomekitDeletePairings()
        {
            return new Byte[] { 0x20, 0, 19, 2 };
        }
        public Byte[] BLECommandHomekitCheckAuthcp()
        {
            return new Byte[] { 0x20, 0, 19, 3 };
        }
        public Byte[] BLECommandHomekitGetPairingId(Byte connection)
        {
            return new Byte[] { 0x20, 1, 19, 4, connection };
        }
        public Byte[] BLECommandHomekitSendWriteResponse(Byte connection, UInt16 characteristic, Byte status_code)
        {
            return new Byte[] { 0x20, 4, 19, 5, connection, (Byte)(characteristic), (Byte)(characteristic >> 8), status_code };
        }
        public Byte[] BLECommandHomekitSendReadResponse(Byte connection, UInt16 characteristic, Byte status_code, UInt16 attribute_size, Byte[] value)
        {
            Byte[] cmd = new Byte[11 + value.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(7 + value.Length), 19, 6, connection, (Byte)(characteristic), (Byte)(characteristic >> 8), status_code, (Byte)(attribute_size), (Byte)(attribute_size >> 8), (Byte)value.Length }, 0, cmd, 0, 11);
            Array.Copy(value, 0, cmd, 11, value.Length);
            return cmd;
        }
        public Byte[] BLECommandHomekitGsnAction(Byte action)
        {
            return new Byte[] { 0x20, 1, 19, 7, action };
        }
        public Byte[] BLECommandHomekitEventNotification(Byte connection, UInt16 characteristic, Byte change_originator, Byte[] value)
        {
            Byte[] cmd = new Byte[9 + value.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(5 + value.Length), 19, 8, connection, (Byte)(characteristic), (Byte)(characteristic >> 8), change_originator, (Byte)value.Length }, 0, cmd, 0, 9);
            Array.Copy(value, 0, cmd, 9, value.Length);
            return cmd;
        }
        public Byte[] BLECommandHomekitBroadcastAction(Byte action, Byte[] parameters)
        {
            Byte[] cmd = new Byte[6 + parameters.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(2 + parameters.Length), 19, 9, action, (Byte)parameters.Length }, 0, cmd, 0, 6);
            Array.Copy(parameters, 0, cmd, 6, parameters.Length);
            return cmd;
        }
        public Byte[] BLECommandHomekitConfigureProductData(Byte[] product_data)
        {
            Byte[] cmd = new Byte[5 + product_data.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(1 + product_data.Length), 19, 10, (Byte)product_data.Length }, 0, cmd, 0, 5);
            Array.Copy(product_data, 0, cmd, 5, product_data.Length);
            return cmd;
        }
        public Byte[] BLECommandCoexSetOptions(UInt32 mask, UInt32 options)
        {
            return new Byte[] { 0x20, 8, 32, 0, (Byte)(mask), (Byte)(mask >> 8), (Byte)(mask >> 16), (Byte)(mask >> 24), (Byte)(options), (Byte)(options >> 8), (Byte)(options >> 16), (Byte)(options >> 24) };
        }
        public Byte[] BLECommandCoexGetCounters(Byte reset)
        {
            return new Byte[] { 0x20, 1, 32, 1, reset };
        }
        public Byte[] BLECommandCoexSetParameters(Byte priority, Byte request, Byte pwm_period, Byte pwm_dutycycle)
        {
            return new Byte[] { 0x20, 4, 32, 2, priority, request, pwm_period, pwm_dutycycle };
        }
        public Byte[] BLECommandCoexSetDirectionalPriorityPulse(Byte pulse)
        {
            return new Byte[] { 0x20, 1, 32, 3, pulse };
        }
        public Byte[] BLECommandL2CAPCocSendConnectionRequest(Byte connection, UInt16 le_psm, UInt16 mtu, UInt16 mps, UInt16 initial_credit)
        {
            return new Byte[] { 0x20, 9, 67, 1, connection, (Byte)(le_psm), (Byte)(le_psm >> 8), (Byte)(mtu), (Byte)(mtu >> 8), (Byte)(mps), (Byte)(mps >> 8), (Byte)(initial_credit), (Byte)(initial_credit >> 8) };
        }
        public Byte[] BLECommandL2CAPCocSendConnectionResponse(Byte connection, UInt16 cid, UInt16 mtu, UInt16 mps, UInt16 initial_credit, UInt16 result)
        {
            return new Byte[] { 0x20, 11, 67, 2, connection, (Byte)(cid), (Byte)(cid >> 8), (Byte)(mtu), (Byte)(mtu >> 8), (Byte)(mps), (Byte)(mps >> 8), (Byte)(initial_credit), (Byte)(initial_credit >> 8), (Byte)(result), (Byte)(result >> 8) };
        }
        public Byte[] BLECommandL2CAPCocSendLEFlowControlCredit(Byte connection, UInt16 cid, UInt16 credits)
        {
            return new Byte[] { 0x20, 5, 67, 3, connection, (Byte)(cid), (Byte)(cid >> 8), (Byte)(credits), (Byte)(credits >> 8) };
        }
        public Byte[] BLECommandL2CAPCocSendDisconnectionRequest(Byte connection, UInt16 cid)
        {
            return new Byte[] { 0x20, 3, 67, 4, connection, (Byte)(cid), (Byte)(cid >> 8) };
        }
        public Byte[] BLECommandL2CAPCocSendData(Byte connection, UInt16 cid, Byte[] data)
        {
            Byte[] cmd = new Byte[8 + data.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(4 + data.Length), 67, 5, connection, (Byte)(cid), (Byte)(cid >> 8), (Byte)data.Length }, 0, cmd, 0, 8);
            Array.Copy(data, 0, cmd, 8, data.Length);
            return cmd;
        }
        public Byte[] BLECommandCTETransmitterEnableCTEResponse(Byte connection, Byte cte_types, Byte[] switching_pattern)
        {
            Byte[] cmd = new Byte[7 + switching_pattern.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(3 + switching_pattern.Length), 68, 0, connection, cte_types, (Byte)switching_pattern.Length }, 0, cmd, 0, 7);
            Array.Copy(switching_pattern, 0, cmd, 7, switching_pattern.Length);
            return cmd;
        }
        public Byte[] BLECommandCTETransmitterDisableCTEResponse(Byte connection)
        {
            return new Byte[] { 0x20, 1, 68, 1, connection };
        }
        public Byte[] BLECommandCTETransmitterStartConnectionlessCTE(Byte adv, Byte cte_length, Byte cte_type, Byte cte_count, Byte[] switching_pattern)
        {
            Byte[] cmd = new Byte[9 + switching_pattern.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(5 + switching_pattern.Length), 68, 2, adv, cte_length, cte_type, cte_count, (Byte)switching_pattern.Length }, 0, cmd, 0, 9);
            Array.Copy(switching_pattern, 0, cmd, 9, switching_pattern.Length);
            return cmd;
        }
        public Byte[] BLECommandCTETransmitterStopConnectionlessCTE(Byte adv)
        {
            return new Byte[] { 0x20, 1, 68, 3, adv };
        }
        public Byte[] BLECommandCTETransmitterSetDTMParameters(Byte cte_length, Byte cte_type, Byte[] switching_pattern)
        {
            Byte[] cmd = new Byte[7 + switching_pattern.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(3 + switching_pattern.Length), 68, 4, cte_length, cte_type, (Byte)switching_pattern.Length }, 0, cmd, 0, 7);
            Array.Copy(switching_pattern, 0, cmd, 7, switching_pattern.Length);
            return cmd;
        }
        public Byte[] BLECommandCTETransmitterClearDTMParameters()
        {
            return new Byte[] { 0x20, 0, 68, 5 };
        }
        public Byte[] BLECommandCTEReceiverConfigure(Byte flags)
        {
            return new Byte[] { 0x20, 1, 69, 0, flags };
        }
        public Byte[] BLECommandCTEReceiverStartIqSampling(Byte connection, UInt16 interval, Byte cte_length, Byte cte_type, Byte slot_durations, Byte[] switching_pattern)
        {
            Byte[] cmd = new Byte[11 + switching_pattern.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(7 + switching_pattern.Length), 69, 1, connection, (Byte)(interval), (Byte)(interval >> 8), cte_length, cte_type, slot_durations, (Byte)switching_pattern.Length }, 0, cmd, 0, 11);
            Array.Copy(switching_pattern, 0, cmd, 11, switching_pattern.Length);
            return cmd;
        }
        public Byte[] BLECommandCTEReceiverStopIqSampling(Byte connection)
        {
            return new Byte[] { 0x20, 1, 69, 2, connection };
        }
        public Byte[] BLECommandCTEReceiverStartConnectionlessIqSampling(Byte sync, Byte slot_durations, Byte cte_count, Byte[] switching_pattern)
        {
            Byte[] cmd = new Byte[8 + switching_pattern.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(4 + switching_pattern.Length), 69, 3, sync, slot_durations, cte_count, (Byte)switching_pattern.Length }, 0, cmd, 0, 8);
            Array.Copy(switching_pattern, 0, cmd, 8, switching_pattern.Length);
            return cmd;
        }
        public Byte[] BLECommandCTEReceiverStopConnectionlessIqSampling(Byte sync)
        {
            return new Byte[] { 0x20, 1, 69, 4, sync };
        }
        public Byte[] BLECommandCTEReceiverSetDTMParameters(Byte cte_length, Byte cte_type, Byte slot_durations, Byte[] switching_pattern)
        {
            Byte[] cmd = new Byte[8 + switching_pattern.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(4 + switching_pattern.Length), 69, 5, cte_length, cte_type, slot_durations, (Byte)switching_pattern.Length }, 0, cmd, 0, 8);
            Array.Copy(switching_pattern, 0, cmd, 8, switching_pattern.Length);
            return cmd;
        }
        public Byte[] BLECommandCTEReceiverClearDTMParameters()
        {
            return new Byte[] { 0x20, 0, 69, 6 };
        }
        public Byte[] BLECommandQualtesterConfigure(UInt32 group, UInt32 id, UInt32 value, Byte[] data)
        {
            Byte[] cmd = new Byte[17 + data.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(13 + data.Length), 254, 0, (Byte)(group), (Byte)(group >> 8), (Byte)(group >> 16), (Byte)(group >> 24), (Byte)(id), (Byte)(id >> 8), (Byte)(id >> 16), (Byte)(id >> 24), (Byte)(value), (Byte)(value >> 8), (Byte)(value >> 16), (Byte)(value >> 24), (Byte)data.Length }, 0, cmd, 0, 17);
            Array.Copy(data, 0, cmd, 17, data.Length);
            return cmd;
        }
        public Byte[] BLECommandUserMessageToTarget(Byte[] data)
        {
            Byte[] cmd = new Byte[5 + data.Length];
            Array.Copy(new Byte[] { 0x20, (Byte)(1 + data.Length), 255, 0, (Byte)data.Length }, 0, cmd, 0, 5);
            Array.Copy(data, 0, cmd, 5, data.Length);
            return cmd;
        }

        public event BlueGecko.BLE.Responses.DFU.ResetEventHandler BLEResponseDFUReset;
        public event BlueGecko.BLE.Responses.DFU.FlashSetAddressEventHandler BLEResponseDFUFlashSetAddress;
        public event BlueGecko.BLE.Responses.DFU.FlashUploadEventHandler BLEResponseDFUFlashUpload;
        public event BlueGecko.BLE.Responses.DFU.FlashUploadFinishEventHandler BLEResponseDFUFlashUploadFinish;
        public event BlueGecko.BLE.Responses.System.HelloEventHandler BLEResponseSystemHello;
        public event BlueGecko.BLE.Responses.System.ResetEventHandler BLEResponseSystemReset;
        public event BlueGecko.BLE.Responses.System.GetBtAddressEventHandler BLEResponseSystemGetBtAddress;
        public event BlueGecko.BLE.Responses.System.SetBtAddressEventHandler BLEResponseSystemSetBtAddress;
        public event BlueGecko.BLE.Responses.System.SetTXPowerEventHandler BLEResponseSystemSetTXPower;
        public event BlueGecko.BLE.Responses.System.GetRandomDataEventHandler BLEResponseSystemGetRandomData;
        public event BlueGecko.BLE.Responses.System.HaltEventHandler BLEResponseSystemHalt;
        public event BlueGecko.BLE.Responses.System.SetDeviceNameEventHandler BLEResponseSystemSetDeviceName;
        public event BlueGecko.BLE.Responses.System.LinklayerConfigureEventHandler BLEResponseSystemLinklayerConfigure;
        public event BlueGecko.BLE.Responses.System.GetCountersEventHandler BLEResponseSystemGetCounters;
        public event BlueGecko.BLE.Responses.System.DataBufferWriteEventHandler BLEResponseSystemDataBufferWrite;
        public event BlueGecko.BLE.Responses.System.SetIdentityAddressEventHandler BLEResponseSystemSetIdentityAddress;
        public event BlueGecko.BLE.Responses.System.DataBufferClearEventHandler BLEResponseSystemDataBufferClear;
        public event BlueGecko.BLE.Responses.LEGAP.OpenEventHandler BLEResponseLEGAPOpen;
        public event BlueGecko.BLE.Responses.LEGAP.SetModeEventHandler BLEResponseLEGAPSetMode;
        public event BlueGecko.BLE.Responses.LEGAP.DiscoverEventHandler BLEResponseLEGAPDiscover;
        public event BlueGecko.BLE.Responses.LEGAP.EndProcedureEventHandler BLEResponseLEGAPEndProcedure;
        public event BlueGecko.BLE.Responses.LEGAP.SetAdvParametersEventHandler BLEResponseLEGAPSetAdvParameters;
        public event BlueGecko.BLE.Responses.LEGAP.SetConnParametersEventHandler BLEResponseLEGAPSetConnParameters;
        public event BlueGecko.BLE.Responses.LEGAP.SetScanParametersEventHandler BLEResponseLEGAPSetScanParameters;
        public event BlueGecko.BLE.Responses.LEGAP.SetAdvDataEventHandler BLEResponseLEGAPSetAdvData;
        public event BlueGecko.BLE.Responses.LEGAP.SetAdvTimeoutEventHandler BLEResponseLEGAPSetAdvTimeout;
        public event BlueGecko.BLE.Responses.LEGAP.SetConnPHYEventHandler BLEResponseLEGAPSetConnPHY;
        public event BlueGecko.BLE.Responses.LEGAP.Bt5SetModeEventHandler BLEResponseLEGAPBt5SetMode;
        public event BlueGecko.BLE.Responses.LEGAP.Bt5SetAdvParametersEventHandler BLEResponseLEGAPBt5SetAdvParameters;
        public event BlueGecko.BLE.Responses.LEGAP.Bt5SetAdvDataEventHandler BLEResponseLEGAPBt5SetAdvData;
        public event BlueGecko.BLE.Responses.LEGAP.SetPrivacyModeEventHandler BLEResponseLEGAPSetPrivacyMode;
        public event BlueGecko.BLE.Responses.LEGAP.SetAdvertiseTimingEventHandler BLEResponseLEGAPSetAdvertiseTiming;
        public event BlueGecko.BLE.Responses.LEGAP.SetAdvertiseChannelMapEventHandler BLEResponseLEGAPSetAdvertiseChannelMap;
        public event BlueGecko.BLE.Responses.LEGAP.SetAdvertiseReportScanRequestEventHandler BLEResponseLEGAPSetAdvertiseReportScanRequest;
        public event BlueGecko.BLE.Responses.LEGAP.SetAdvertisePHYEventHandler BLEResponseLEGAPSetAdvertisePHY;
        public event BlueGecko.BLE.Responses.LEGAP.SetAdvertiseConfigurationEventHandler BLEResponseLEGAPSetAdvertiseConfiguration;
        public event BlueGecko.BLE.Responses.LEGAP.ClearAdvertiseConfigurationEventHandler BLEResponseLEGAPClearAdvertiseConfiguration;
        public event BlueGecko.BLE.Responses.LEGAP.StartAdvertisingEventHandler BLEResponseLEGAPStartAdvertising;
        public event BlueGecko.BLE.Responses.LEGAP.StopAdvertisingEventHandler BLEResponseLEGAPStopAdvertising;
        public event BlueGecko.BLE.Responses.LEGAP.SetDiscoveryTimingEventHandler BLEResponseLEGAPSetDiscoveryTiming;
        public event BlueGecko.BLE.Responses.LEGAP.SetDiscoveryTypeEventHandler BLEResponseLEGAPSetDiscoveryType;
        public event BlueGecko.BLE.Responses.LEGAP.StartDiscoveryEventHandler BLEResponseLEGAPStartDiscovery;
        public event BlueGecko.BLE.Responses.LEGAP.SetDataChannelClassificationEventHandler BLEResponseLEGAPSetDataChannelClassification;
        public event BlueGecko.BLE.Responses.LEGAP.ConnectEventHandler BLEResponseLEGAPConnect;
        public event BlueGecko.BLE.Responses.LEGAP.SetAdvertiseTXPowerEventHandler BLEResponseLEGAPSetAdvertiseTXPower;
        public event BlueGecko.BLE.Responses.LEGAP.SetDiscoveryExtendedScanResponseEventHandler BLEResponseLEGAPSetDiscoveryExtendedScanResponse;
        public event BlueGecko.BLE.Responses.LEGAP.StartPeriodicAdvertisingEventHandler BLEResponseLEGAPStartPeriodicAdvertising;
        public event BlueGecko.BLE.Responses.LEGAP.StopPeriodicAdvertisingEventHandler BLEResponseLEGAPStopPeriodicAdvertising;
        public event BlueGecko.BLE.Responses.LEGAP.SetLongAdvertisingDataEventHandler BLEResponseLEGAPSetLongAdvertisingData;
        public event BlueGecko.BLE.Responses.LEGAP.EnableWhitelistingEventHandler BLEResponseLEGAPEnableWhitelisting;
        public event BlueGecko.BLE.Responses.LEGAP.SetConnTimingParametersEventHandler BLEResponseLEGAPSetConnTimingParameters;
        public event BlueGecko.BLE.Responses.LEGAP.SetAdvertiseRandomAddressEventHandler BLEResponseLEGAPSetAdvertiseRandomAddress;
        public event BlueGecko.BLE.Responses.LEGAP.ClearAdvertiseRandomAddressEventHandler BLEResponseLEGAPClearAdvertiseRandomAddress;
        public event BlueGecko.BLE.Responses.Sync.OpenEventHandler BLEResponseSyncOpen;
        public event BlueGecko.BLE.Responses.Sync.CloseEventHandler BLEResponseSyncClose;
        public event BlueGecko.BLE.Responses.LEConnection.SetParametersEventHandler BLEResponseLEConnectionSetParameters;
        public event BlueGecko.BLE.Responses.LEConnection.GetRssiEventHandler BLEResponseLEConnectionGetRssi;
        public event BlueGecko.BLE.Responses.LEConnection.DisableSlaveLatencyEventHandler BLEResponseLEConnectionDisableSlaveLatency;
        public event BlueGecko.BLE.Responses.LEConnection.SetPHYEventHandler BLEResponseLEConnectionSetPHY;
        public event BlueGecko.BLE.Responses.LEConnection.CloseEventHandler BLEResponseLEConnectionClose;
        public event BlueGecko.BLE.Responses.LEConnection.SetTimingParametersEventHandler BLEResponseLEConnectionSetTimingParameters;
        public event BlueGecko.BLE.Responses.LEConnection.ReadChannelMapEventHandler BLEResponseLEConnectionReadChannelMap;
        public event BlueGecko.BLE.Responses.LEConnection.SetPreferredPHYEventHandler BLEResponseLEConnectionSetPreferredPHY;
        public event BlueGecko.BLE.Responses.GATT.SetMaxMtuEventHandler BLEResponseGATTSetMaxMtu;
        public event BlueGecko.BLE.Responses.GATT.DiscoverPrimaryServicesEventHandler BLEResponseGATTDiscoverPrimaryServices;
        public event BlueGecko.BLE.Responses.GATT.DiscoverPrimaryServicesByUUIDEventHandler BLEResponseGATTDiscoverPrimaryServicesByUUID;
        public event BlueGecko.BLE.Responses.GATT.DiscoverCharacteristicsEventHandler BLEResponseGATTDiscoverCharacteristics;
        public event BlueGecko.BLE.Responses.GATT.DiscoverCharacteristicsByUUIDEventHandler BLEResponseGATTDiscoverCharacteristicsByUUID;
        public event BlueGecko.BLE.Responses.GATT.SetCharacteristicNotificationEventHandler BLEResponseGATTSetCharacteristicNotification;
        public event BlueGecko.BLE.Responses.GATT.DiscoverDescriptorsEventHandler BLEResponseGATTDiscoverDescriptors;
        public event BlueGecko.BLE.Responses.GATT.ReadCharacteristicValueEventHandler BLEResponseGATTReadCharacteristicValue;
        public event BlueGecko.BLE.Responses.GATT.ReadCharacteristicValueByUUIDEventHandler BLEResponseGATTReadCharacteristicValueByUUID;
        public event BlueGecko.BLE.Responses.GATT.WriteCharacteristicValueEventHandler BLEResponseGATTWriteCharacteristicValue;
        public event BlueGecko.BLE.Responses.GATT.WriteCharacteristicValueWithoutResponseEventHandler BLEResponseGATTWriteCharacteristicValueWithoutResponse;
        public event BlueGecko.BLE.Responses.GATT.PrepareCharacteristicValueWriteEventHandler BLEResponseGATTPrepareCharacteristicValueWrite;
        public event BlueGecko.BLE.Responses.GATT.ExecuteCharacteristicValueWriteEventHandler BLEResponseGATTExecuteCharacteristicValueWrite;
        public event BlueGecko.BLE.Responses.GATT.SendCharacteristicConfirmationEventHandler BLEResponseGATTSendCharacteristicConfirmation;
        public event BlueGecko.BLE.Responses.GATT.ReadDescriptorValueEventHandler BLEResponseGATTReadDescriptorValue;
        public event BlueGecko.BLE.Responses.GATT.WriteDescriptorValueEventHandler BLEResponseGATTWriteDescriptorValue;
        public event BlueGecko.BLE.Responses.GATT.FindIncludedServicesEventHandler BLEResponseGATTFindIncludedServices;
        public event BlueGecko.BLE.Responses.GATT.ReadMultipleCharacteristicValuesEventHandler BLEResponseGATTReadMultipleCharacteristicValues;
        public event BlueGecko.BLE.Responses.GATT.ReadCharacteristicValueFromOffsetEventHandler BLEResponseGATTReadCharacteristicValueFromOffset;
        public event BlueGecko.BLE.Responses.GATT.PrepareCharacteristicValueReliableWriteEventHandler BLEResponseGATTPrepareCharacteristicValueReliableWrite;
        public event BlueGecko.BLE.Responses.GATTServer.ReadAttributeValueEventHandler BLEResponseGATTServerReadAttributeValue;
        public event BlueGecko.BLE.Responses.GATTServer.ReadAttributeTypeEventHandler BLEResponseGATTServerReadAttributeType;
        public event BlueGecko.BLE.Responses.GATTServer.WriteAttributeValueEventHandler BLEResponseGATTServerWriteAttributeValue;
        public event BlueGecko.BLE.Responses.GATTServer.SendUserReadResponseEventHandler BLEResponseGATTServerSendUserReadResponse;
        public event BlueGecko.BLE.Responses.GATTServer.SendUserWriteResponseEventHandler BLEResponseGATTServerSendUserWriteResponse;
        public event BlueGecko.BLE.Responses.GATTServer.SendCharacteristicNotificationEventHandler BLEResponseGATTServerSendCharacteristicNotification;
        public event BlueGecko.BLE.Responses.GATTServer.FindAttributeEventHandler BLEResponseGATTServerFindAttribute;
        public event BlueGecko.BLE.Responses.GATTServer.SetCapabilitiesEventHandler BLEResponseGATTServerSetCapabilities;
        public event BlueGecko.BLE.Responses.GATTServer.FindPrimaryServiceEventHandler BLEResponseGATTServerFindPrimaryService;
        public event BlueGecko.BLE.Responses.GATTServer.SetMaxMtuEventHandler BLEResponseGATTServerSetMaxMtu;
        public event BlueGecko.BLE.Responses.GATTServer.GetMtuEventHandler BLEResponseGATTServerGetMtu;
        public event BlueGecko.BLE.Responses.GATTServer.EnableCapabilitiesEventHandler BLEResponseGATTServerEnableCapabilities;
        public event BlueGecko.BLE.Responses.GATTServer.DisableCapabilitiesEventHandler BLEResponseGATTServerDisableCapabilities;
        public event BlueGecko.BLE.Responses.GATTServer.GetEnabledCapabilitiesEventHandler BLEResponseGATTServerGetEnabledCapabilities;
        public event BlueGecko.BLE.Responses.Hardware.SetSoftTimerEventHandler BLEResponseHardwareSetSoftTimer;
        public event BlueGecko.BLE.Responses.Hardware.GetTimeEventHandler BLEResponseHardwareGetTime;
        public event BlueGecko.BLE.Responses.Hardware.SetLazySoftTimerEventHandler BLEResponseHardwareSetLazySoftTimer;
        public event BlueGecko.BLE.Responses.Flash.PSEraseAllEventHandler BLEResponseFlashPSEraseAll;
        public event BlueGecko.BLE.Responses.Flash.PSSaveEventHandler BLEResponseFlashPSSave;
        public event BlueGecko.BLE.Responses.Flash.PSLoadEventHandler BLEResponseFlashPSLoad;
        public event BlueGecko.BLE.Responses.Flash.PSEraseEventHandler BLEResponseFlashPSErase;
        public event BlueGecko.BLE.Responses.Test.DTMTXEventHandler BLEResponseTestDTMTX;
        public event BlueGecko.BLE.Responses.Test.DTMRXEventHandler BLEResponseTestDTMRX;
        public event BlueGecko.BLE.Responses.Test.DTMEndEventHandler BLEResponseTestDTMEnd;
        public event BlueGecko.BLE.Responses.Test.DebugCommandEventHandler BLEResponseTestDebugCommand;
        public event BlueGecko.BLE.Responses.Test.DebugCounterEventHandler BLEResponseTestDebugCounter;
        public event BlueGecko.BLE.Responses.SM.SetBondableModeEventHandler BLEResponseSMSetBondableMode;
        public event BlueGecko.BLE.Responses.SM.ConfigureEventHandler BLEResponseSMConfigure;
        public event BlueGecko.BLE.Responses.SM.StoreBondingConfigurationEventHandler BLEResponseSMStoreBondingConfiguration;
        public event BlueGecko.BLE.Responses.SM.IncreaseSecurityEventHandler BLEResponseSMIncreaseSecurity;
        public event BlueGecko.BLE.Responses.SM.DeleteBondingEventHandler BLEResponseSMDeleteBonding;
        public event BlueGecko.BLE.Responses.SM.DeleteBondingsEventHandler BLEResponseSMDeleteBondings;
        public event BlueGecko.BLE.Responses.SM.EnterPasskeyEventHandler BLEResponseSMEnterPasskey;
        public event BlueGecko.BLE.Responses.SM.PasskeyConfirmEventHandler BLEResponseSMPasskeyConfirm;
        public event BlueGecko.BLE.Responses.SM.SetOobDataEventHandler BLEResponseSMSetOobData;
        public event BlueGecko.BLE.Responses.SM.ListAllBondingsEventHandler BLEResponseSMListAllBondings;
        public event BlueGecko.BLE.Responses.SM.BondingConfirmEventHandler BLEResponseSMBondingConfirm;
        public event BlueGecko.BLE.Responses.SM.SetDebugModeEventHandler BLEResponseSMSetDebugMode;
        public event BlueGecko.BLE.Responses.SM.SetPasskeyEventHandler BLEResponseSMSetPasskey;
        public event BlueGecko.BLE.Responses.SM.UseScOobEventHandler BLEResponseSMUseScOob;
        public event BlueGecko.BLE.Responses.SM.SetScRemoteOobDataEventHandler BLEResponseSMSetScRemoteOobData;
        public event BlueGecko.BLE.Responses.SM.AddToWhitelistEventHandler BLEResponseSMAddToWhitelist;
        public event BlueGecko.BLE.Responses.SM.SetMinimumKeySizeEventHandler BLEResponseSMSetMinimumKeySize;
        public event BlueGecko.BLE.Responses.Homekit.ConfigureEventHandler BLEResponseHomekitConfigure;
        public event BlueGecko.BLE.Responses.Homekit.AdvertiseEventHandler BLEResponseHomekitAdvertise;
        public event BlueGecko.BLE.Responses.Homekit.DeletePairingsEventHandler BLEResponseHomekitDeletePairings;
        public event BlueGecko.BLE.Responses.Homekit.CheckAuthcpEventHandler BLEResponseHomekitCheckAuthcp;
        public event BlueGecko.BLE.Responses.Homekit.GetPairingIdEventHandler BLEResponseHomekitGetPairingId;
        public event BlueGecko.BLE.Responses.Homekit.SendWriteResponseEventHandler BLEResponseHomekitSendWriteResponse;
        public event BlueGecko.BLE.Responses.Homekit.SendReadResponseEventHandler BLEResponseHomekitSendReadResponse;
        public event BlueGecko.BLE.Responses.Homekit.GsnActionEventHandler BLEResponseHomekitGsnAction;
        public event BlueGecko.BLE.Responses.Homekit.EventNotificationEventHandler BLEResponseHomekitEventNotification;
        public event BlueGecko.BLE.Responses.Homekit.BroadcastActionEventHandler BLEResponseHomekitBroadcastAction;
        public event BlueGecko.BLE.Responses.Homekit.ConfigureProductDataEventHandler BLEResponseHomekitConfigureProductData;
        public event BlueGecko.BLE.Responses.Coex.SetOptionsEventHandler BLEResponseCoexSetOptions;
        public event BlueGecko.BLE.Responses.Coex.GetCountersEventHandler BLEResponseCoexGetCounters;
        public event BlueGecko.BLE.Responses.Coex.SetParametersEventHandler BLEResponseCoexSetParameters;
        public event BlueGecko.BLE.Responses.Coex.SetDirectionalPriorityPulseEventHandler BLEResponseCoexSetDirectionalPriorityPulse;
        public event BlueGecko.BLE.Responses.L2CAP.CocSendConnectionRequestEventHandler BLEResponseL2CAPCocSendConnectionRequest;
        public event BlueGecko.BLE.Responses.L2CAP.CocSendConnectionResponseEventHandler BLEResponseL2CAPCocSendConnectionResponse;
        public event BlueGecko.BLE.Responses.L2CAP.CocSendLEFlowControlCreditEventHandler BLEResponseL2CAPCocSendLEFlowControlCredit;
        public event BlueGecko.BLE.Responses.L2CAP.CocSendDisconnectionRequestEventHandler BLEResponseL2CAPCocSendDisconnectionRequest;
        public event BlueGecko.BLE.Responses.L2CAP.CocSendDataEventHandler BLEResponseL2CAPCocSendData;
        public event BlueGecko.BLE.Responses.CTETransmitter.EnableCTEResponseEventHandler BLEResponseCTETransmitterEnableCTEResponse;
        public event BlueGecko.BLE.Responses.CTETransmitter.DisableCTEResponseEventHandler BLEResponseCTETransmitterDisableCTEResponse;
        public event BlueGecko.BLE.Responses.CTETransmitter.StartConnectionlessCTEEventHandler BLEResponseCTETransmitterStartConnectionlessCTE;
        public event BlueGecko.BLE.Responses.CTETransmitter.StopConnectionlessCTEEventHandler BLEResponseCTETransmitterStopConnectionlessCTE;
        public event BlueGecko.BLE.Responses.CTETransmitter.SetDTMParametersEventHandler BLEResponseCTETransmitterSetDTMParameters;
        public event BlueGecko.BLE.Responses.CTETransmitter.ClearDTMParametersEventHandler BLEResponseCTETransmitterClearDTMParameters;
        public event BlueGecko.BLE.Responses.CTEReceiver.ConfigureEventHandler BLEResponseCTEReceiverConfigure;
        public event BlueGecko.BLE.Responses.CTEReceiver.StartIqSamplingEventHandler BLEResponseCTEReceiverStartIqSampling;
        public event BlueGecko.BLE.Responses.CTEReceiver.StopIqSamplingEventHandler BLEResponseCTEReceiverStopIqSampling;
        public event BlueGecko.BLE.Responses.CTEReceiver.StartConnectionlessIqSamplingEventHandler BLEResponseCTEReceiverStartConnectionlessIqSampling;
        public event BlueGecko.BLE.Responses.CTEReceiver.StopConnectionlessIqSamplingEventHandler BLEResponseCTEReceiverStopConnectionlessIqSampling;
        public event BlueGecko.BLE.Responses.CTEReceiver.SetDTMParametersEventHandler BLEResponseCTEReceiverSetDTMParameters;
        public event BlueGecko.BLE.Responses.CTEReceiver.ClearDTMParametersEventHandler BLEResponseCTEReceiverClearDTMParameters;
        public event BlueGecko.BLE.Responses.Qualtester.ConfigureEventHandler BLEResponseQualtesterConfigure;
        public event BlueGecko.BLE.Responses.User.MessageToTargetEventHandler BLEResponseUserMessageToTarget;

        public event BlueGecko.BLE.Events.DFU.BootEventHandler BLEEventDFUBoot;
        public event BlueGecko.BLE.Events.DFU.BootFailureEventHandler BLEEventDFUBootFailure;
        public event BlueGecko.BLE.Events.System.BootEventHandler BLEEventSystemBoot;
        public event BlueGecko.BLE.Events.System.ExternalSignalEventHandler BLEEventSystemExternalSignal;
        public event BlueGecko.BLE.Events.System.AwakeEventHandler BLEEventSystemAwake;
        public event BlueGecko.BLE.Events.System.HardwareErrorEventHandler BLEEventSystemHardwareError;
        public event BlueGecko.BLE.Events.System.ErrorEventHandler BLEEventSystemError;
        public event BlueGecko.BLE.Events.LEGAP.ScanResponseEventHandler BLEEventLEGAPScanResponse;
        public event BlueGecko.BLE.Events.LEGAP.AdvTimeoutEventHandler BLEEventLEGAPAdvTimeout;
        public event BlueGecko.BLE.Events.LEGAP.ScanRequestEventHandler BLEEventLEGAPScanRequest;
        public event BlueGecko.BLE.Events.LEGAP.ExtendedScanResponseEventHandler BLEEventLEGAPExtendedScanResponse;
        public event BlueGecko.BLE.Events.LEGAP.PeriodicAdvertisingStatusEventHandler BLEEventLEGAPPeriodicAdvertisingStatus;
        public event BlueGecko.BLE.Events.Sync.OpenedEventHandler BLEEventSyncOpened;
        public event BlueGecko.BLE.Events.Sync.ClosedEventHandler BLEEventSyncClosed;
        public event BlueGecko.BLE.Events.Sync.DataEventHandler BLEEventSyncData;
        public event BlueGecko.BLE.Events.LEConnection.OpenedEventHandler BLEEventLEConnectionOpened;
        public event BlueGecko.BLE.Events.LEConnection.ClosedEventHandler BLEEventLEConnectionClosed;
        public event BlueGecko.BLE.Events.LEConnection.ParametersEventHandler BLEEventLEConnectionParameters;
        public event BlueGecko.BLE.Events.LEConnection.RssiEventHandler BLEEventLEConnectionRssi;
        public event BlueGecko.BLE.Events.LEConnection.PHYStatusEventHandler BLEEventLEConnectionPHYStatus;
        public event BlueGecko.BLE.Events.GATT.MtuExchangedEventHandler BLEEventGATTMtuExchanged;
        public event BlueGecko.BLE.Events.GATT.ServiceEventHandler BLEEventGATTService;
        public event BlueGecko.BLE.Events.GATT.CharacteristicEventHandler BLEEventGATTCharacteristic;
        public event BlueGecko.BLE.Events.GATT.DescriptorEventHandler BLEEventGATTDescriptor;
        public event BlueGecko.BLE.Events.GATT.CharacteristicValueEventHandler BLEEventGATTCharacteristicValue;
        public event BlueGecko.BLE.Events.GATT.DescriptorValueEventHandler BLEEventGATTDescriptorValue;
        public event BlueGecko.BLE.Events.GATT.ProcedureCompletedEventHandler BLEEventGATTProcedureCompleted;
        public event BlueGecko.BLE.Events.GATTServer.AttributeValueEventHandler BLEEventGATTServerAttributeValue;
        public event BlueGecko.BLE.Events.GATTServer.UserReadRequestEventHandler BLEEventGATTServerUserReadRequest;
        public event BlueGecko.BLE.Events.GATTServer.UserWriteRequestEventHandler BLEEventGATTServerUserWriteRequest;
        public event BlueGecko.BLE.Events.GATTServer.CharacteristicStatusEventHandler BLEEventGATTServerCharacteristicStatus;
        public event BlueGecko.BLE.Events.GATTServer.ExecuteWriteCompletedEventHandler BLEEventGATTServerExecuteWriteCompleted;
        public event BlueGecko.BLE.Events.Hardware.SoftTimerEventHandler BLEEventHardwareSoftTimer;
        public event BlueGecko.BLE.Events.Test.DTMCompletedEventHandler BLEEventTestDTMCompleted;
        public event BlueGecko.BLE.Events.SM.PasskeyDisplayEventHandler BLEEventSMPasskeyDisplay;
        public event BlueGecko.BLE.Events.SM.PasskeyRequestEventHandler BLEEventSMPasskeyRequest;
        public event BlueGecko.BLE.Events.SM.ConfirmPasskeyEventHandler BLEEventSMConfirmPasskey;
        public event BlueGecko.BLE.Events.SM.BondedEventHandler BLEEventSMBonded;
        public event BlueGecko.BLE.Events.SM.BondingFailedEventHandler BLEEventSMBondingFailed;
        public event BlueGecko.BLE.Events.SM.ListBondingEntryEventHandler BLEEventSMListBondingEntry;
        public event BlueGecko.BLE.Events.SM.ListAllBondingsCompleteEventHandler BLEEventSMListAllBondingsComplete;
        public event BlueGecko.BLE.Events.SM.ConfirmBondingEventHandler BLEEventSMConfirmBonding;
        public event BlueGecko.BLE.Events.Homekit.SetupcodeDisplayEventHandler BLEEventHomekitSetupcodeDisplay;
        public event BlueGecko.BLE.Events.Homekit.PairedEventHandler BLEEventHomekitPaired;
        public event BlueGecko.BLE.Events.Homekit.PairVerifiedEventHandler BLEEventHomekitPairVerified;
        public event BlueGecko.BLE.Events.Homekit.ConnectionOpenedEventHandler BLEEventHomekitConnectionOpened;
        public event BlueGecko.BLE.Events.Homekit.ConnectionClosedEventHandler BLEEventHomekitConnectionClosed;
        public event BlueGecko.BLE.Events.Homekit.IdentifyEventHandler BLEEventHomekitIdentify;
        public event BlueGecko.BLE.Events.Homekit.WriteRequestEventHandler BLEEventHomekitWriteRequest;
        public event BlueGecko.BLE.Events.Homekit.ReadRequestEventHandler BLEEventHomekitReadRequest;
        public event BlueGecko.BLE.Events.Homekit.DisconnectionRequiredEventHandler BLEEventHomekitDisconnectionRequired;
        public event BlueGecko.BLE.Events.Homekit.PairingRemovedEventHandler BLEEventHomekitPairingRemoved;
        public event BlueGecko.BLE.Events.Homekit.SetuppayloadDisplayEventHandler BLEEventHomekitSetuppayloadDisplay;
        public event BlueGecko.BLE.Events.L2CAP.CocConnectionRequestEventHandler BLEEventL2CAPCocConnectionRequest;
        public event BlueGecko.BLE.Events.L2CAP.CocConnectionResponseEventHandler BLEEventL2CAPCocConnectionResponse;
        public event BlueGecko.BLE.Events.L2CAP.CocLEFlowControlCreditEventHandler BLEEventL2CAPCocLEFlowControlCredit;
        public event BlueGecko.BLE.Events.L2CAP.CocChannelDisconnectedEventHandler BLEEventL2CAPCocChannelDisconnected;
        public event BlueGecko.BLE.Events.L2CAP.CocDataEventHandler BLEEventL2CAPCocData;
        public event BlueGecko.BLE.Events.L2CAP.CommandRejectedEventHandler BLEEventL2CAPCommandRejected;
        public event BlueGecko.BLE.Events.CTEReceiver.IqReportEventHandler BLEEventCTEReceiverIqReport;
        public event BlueGecko.BLE.Events.Qualtester.StateChangedEventHandler BLEEventQualtesterStateChanged;
        public event BlueGecko.BLE.Events.User.MessageToHostEventHandler BLEEventUserMessageToHost;

        private Byte[] bgapiRXBuffer = new Byte[65];
        private int bgapiRXBufferPos = 0;
        private int bgapiRXDataLen = 0;

        private Boolean parserBusy = false;

        public void SetBusy(Boolean isBusy)
        {
            this.parserBusy = isBusy;
        }

        public Boolean IsBusy()
        {
            return parserBusy;
        }

        public UInt16 Parse(Byte ch)
        {
            /*#ifdef DEBUG
                // DEBUG: output hex value of incoming character
                if (ch < 16) Serial.write(0x30);    // leading '0'
                Serial.print(ch, HEX);              // actual hex value
                Serial.write(0x20);                 // trailing ' '
            #endif*/

            /*
            BGAPI packet structure (as of 2020-06-12):
                Byte 0:
                      [7] - 1 bit, Message Type (MT)         0 = Command/Response, 1 = Event
                    [6:3] - 4 bits, Technology Type (TT)     0100b/0x04 - Blue Gecko
                    [2:0] - 3 bits, Length High (LH)         Payload length (high bits)
                Byte 1:     8 bits, Length Low (LL)          Payload length (low bits)
                Byte 2:     8 bits, Class ID (CID)           Command class ID
                Byte 3:     8 bits, Command ID (CMD)         Command ID
                Bytes 4-n:  0 - 2048 Bytes, Payload (PL)     Up to 2048 bytes of payload
            */

            // check packet position
            if (bgapiRXBufferPos == 0)
            {
                // beginning of packet, check for correct framing/expected byte(s)
                // BGAPI packet for Blue Gecko must be either Command/Response (0x20) or Event (0xa0)
                // Verify four bit technology type == 0x02 shifted into TT field
                if ((ch & 0x78) == (0x04) << 3)
                {
                    // store new character in RX buffer
                    bgapiRXBuffer[bgapiRXBufferPos++] = ch;
                }
                else
                {
                    /*#ifdef DEBUG
                        Serial.print("*** Packet frame sync error! Expected .0000... binary, got 0x");
                        Serial.println(ch, HEX);
                    #endif*/
                    return 1; // packet format error
                }
            }
            else
            {
                // middle of packet, assume we're okay
                bgapiRXBuffer[bgapiRXBufferPos++] = ch;
                if (bgapiRXBufferPos == 2)
                {
                    // just received "Length Low" byte, so store expected packet length
                    bgapiRXDataLen = ch + ((bgapiRXBuffer[0] & 0x07) << 8);
                }
                else if (bgapiRXBufferPos == bgapiRXDataLen + 4)
                {
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
                    if ((bgapiRXBuffer[0] & 0x80) == 0)
                    {
                        // 0x00 = Response packet
                        if (bgapiRXBuffer[2] == 0)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseDFUReset != null)
                                {
                                    BLEResponseDFUReset(this, new BlueGecko.BLE.Responses.DFU.ResetEventArgs(
                                    ));
                                }
                                SetBusy(false);
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseDFUFlashSetAddress != null)
                                {
                                    BLEResponseDFUFlashSetAddress(this, new BlueGecko.BLE.Responses.DFU.FlashSetAddressEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseDFUFlashUpload != null)
                                {
                                    BLEResponseDFUFlashUpload(this, new BlueGecko.BLE.Responses.DFU.FlashUploadEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseDFUFlashUploadFinish != null)
                                {
                                    BLEResponseDFUFlashUploadFinish(this, new BlueGecko.BLE.Responses.DFU.FlashUploadFinishEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 1)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseSystemHello != null)
                                {
                                    BLEResponseSystemHello(this, new BlueGecko.BLE.Responses.System.HelloEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseSystemReset != null)
                                {
                                    BLEResponseSystemReset(this, new BlueGecko.BLE.Responses.System.ResetEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseSystemGetBtAddress != null)
                                {
                                    BLEResponseSystemGetBtAddress(this, new BlueGecko.BLE.Responses.System.GetBtAddressEventArgs(
                                        (Byte[])(bgapiRXBuffer.Skip(4).Take(6).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseSystemSetBtAddress != null)
                                {
                                    BLEResponseSystemSetBtAddress(this, new BlueGecko.BLE.Responses.System.SetBtAddressEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 10)
                            {
                                if (BLEResponseSystemSetTXPower != null)
                                {
                                    BLEResponseSystemSetTXPower(this, new BlueGecko.BLE.Responses.System.SetTXPowerEventArgs(
                                        (Int16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 11)
                            {
                                if (BLEResponseSystemGetRandomData != null)
                                {
                                    BLEResponseSystemGetRandomData(this, new BlueGecko.BLE.Responses.System.GetRandomDataEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 12)
                            {
                                if (BLEResponseSystemHalt != null)
                                {
                                    BLEResponseSystemHalt(this, new BlueGecko.BLE.Responses.System.HaltEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 13)
                            {
                                if (BLEResponseSystemSetDeviceName != null)
                                {
                                    BLEResponseSystemSetDeviceName(this, new BlueGecko.BLE.Responses.System.SetDeviceNameEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 14)
                            {
                                if (BLEResponseSystemLinklayerConfigure != null)
                                {
                                    BLEResponseSystemLinklayerConfigure(this, new BlueGecko.BLE.Responses.System.LinklayerConfigureEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 15)
                            {
                                if (BLEResponseSystemGetCounters != null)
                                {
                                    BLEResponseSystemGetCounters(this, new BlueGecko.BLE.Responses.System.GetCountersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8)),
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8)),
                                        (UInt16)(bgapiRXBuffer[10] + (bgapiRXBuffer[11] << 8)),
                                        (UInt16)(bgapiRXBuffer[12] + (bgapiRXBuffer[13] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 18)
                            {
                                if (BLEResponseSystemDataBufferWrite != null)
                                {
                                    BLEResponseSystemDataBufferWrite(this, new BlueGecko.BLE.Responses.System.DataBufferWriteEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 19)
                            {
                                if (BLEResponseSystemSetIdentityAddress != null)
                                {
                                    BLEResponseSystemSetIdentityAddress(this, new BlueGecko.BLE.Responses.System.SetIdentityAddressEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 20)
                            {
                                if (BLEResponseSystemDataBufferClear != null)
                                {
                                    BLEResponseSystemDataBufferClear(this, new BlueGecko.BLE.Responses.System.DataBufferClearEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 3)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseLEGAPOpen != null)
                                {
                                    BLEResponseLEGAPOpen(this, new BlueGecko.BLE.Responses.LEGAP.OpenEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseLEGAPSetMode != null)
                                {
                                    BLEResponseLEGAPSetMode(this, new BlueGecko.BLE.Responses.LEGAP.SetModeEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseLEGAPDiscover != null)
                                {
                                    BLEResponseLEGAPDiscover(this, new BlueGecko.BLE.Responses.LEGAP.DiscoverEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseLEGAPEndProcedure != null)
                                {
                                    BLEResponseLEGAPEndProcedure(this, new BlueGecko.BLE.Responses.LEGAP.EndProcedureEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseLEGAPSetAdvParameters != null)
                                {
                                    BLEResponseLEGAPSetAdvParameters(this, new BlueGecko.BLE.Responses.LEGAP.SetAdvParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseLEGAPSetConnParameters != null)
                                {
                                    BLEResponseLEGAPSetConnParameters(this, new BlueGecko.BLE.Responses.LEGAP.SetConnParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseLEGAPSetScanParameters != null)
                                {
                                    BLEResponseLEGAPSetScanParameters(this, new BlueGecko.BLE.Responses.LEGAP.SetScanParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEResponseLEGAPSetAdvData != null)
                                {
                                    BLEResponseLEGAPSetAdvData(this, new BlueGecko.BLE.Responses.LEGAP.SetAdvDataEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 8)
                            {
                                if (BLEResponseLEGAPSetAdvTimeout != null)
                                {
                                    BLEResponseLEGAPSetAdvTimeout(this, new BlueGecko.BLE.Responses.LEGAP.SetAdvTimeoutEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 9)
                            {
                                if (BLEResponseLEGAPSetConnPHY != null)
                                {
                                    BLEResponseLEGAPSetConnPHY(this, new BlueGecko.BLE.Responses.LEGAP.SetConnPHYEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 10)
                            {
                                if (BLEResponseLEGAPBt5SetMode != null)
                                {
                                    BLEResponseLEGAPBt5SetMode(this, new BlueGecko.BLE.Responses.LEGAP.Bt5SetModeEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 11)
                            {
                                if (BLEResponseLEGAPBt5SetAdvParameters != null)
                                {
                                    BLEResponseLEGAPBt5SetAdvParameters(this, new BlueGecko.BLE.Responses.LEGAP.Bt5SetAdvParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 12)
                            {
                                if (BLEResponseLEGAPBt5SetAdvData != null)
                                {
                                    BLEResponseLEGAPBt5SetAdvData(this, new BlueGecko.BLE.Responses.LEGAP.Bt5SetAdvDataEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 13)
                            {
                                if (BLEResponseLEGAPSetPrivacyMode != null)
                                {
                                    BLEResponseLEGAPSetPrivacyMode(this, new BlueGecko.BLE.Responses.LEGAP.SetPrivacyModeEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 14)
                            {
                                if (BLEResponseLEGAPSetAdvertiseTiming != null)
                                {
                                    BLEResponseLEGAPSetAdvertiseTiming(this, new BlueGecko.BLE.Responses.LEGAP.SetAdvertiseTimingEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 15)
                            {
                                if (BLEResponseLEGAPSetAdvertiseChannelMap != null)
                                {
                                    BLEResponseLEGAPSetAdvertiseChannelMap(this, new BlueGecko.BLE.Responses.LEGAP.SetAdvertiseChannelMapEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 16)
                            {
                                if (BLEResponseLEGAPSetAdvertiseReportScanRequest != null)
                                {
                                    BLEResponseLEGAPSetAdvertiseReportScanRequest(this, new BlueGecko.BLE.Responses.LEGAP.SetAdvertiseReportScanRequestEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 17)
                            {
                                if (BLEResponseLEGAPSetAdvertisePHY != null)
                                {
                                    BLEResponseLEGAPSetAdvertisePHY(this, new BlueGecko.BLE.Responses.LEGAP.SetAdvertisePHYEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 18)
                            {
                                if (BLEResponseLEGAPSetAdvertiseConfiguration != null)
                                {
                                    BLEResponseLEGAPSetAdvertiseConfiguration(this, new BlueGecko.BLE.Responses.LEGAP.SetAdvertiseConfigurationEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 19)
                            {
                                if (BLEResponseLEGAPClearAdvertiseConfiguration != null)
                                {
                                    BLEResponseLEGAPClearAdvertiseConfiguration(this, new BlueGecko.BLE.Responses.LEGAP.ClearAdvertiseConfigurationEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 20)
                            {
                                if (BLEResponseLEGAPStartAdvertising != null)
                                {
                                    BLEResponseLEGAPStartAdvertising(this, new BlueGecko.BLE.Responses.LEGAP.StartAdvertisingEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 21)
                            {
                                if (BLEResponseLEGAPStopAdvertising != null)
                                {
                                    BLEResponseLEGAPStopAdvertising(this, new BlueGecko.BLE.Responses.LEGAP.StopAdvertisingEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 22)
                            {
                                if (BLEResponseLEGAPSetDiscoveryTiming != null)
                                {
                                    BLEResponseLEGAPSetDiscoveryTiming(this, new BlueGecko.BLE.Responses.LEGAP.SetDiscoveryTimingEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 23)
                            {
                                if (BLEResponseLEGAPSetDiscoveryType != null)
                                {
                                    BLEResponseLEGAPSetDiscoveryType(this, new BlueGecko.BLE.Responses.LEGAP.SetDiscoveryTypeEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 24)
                            {
                                if (BLEResponseLEGAPStartDiscovery != null)
                                {
                                    BLEResponseLEGAPStartDiscovery(this, new BlueGecko.BLE.Responses.LEGAP.StartDiscoveryEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 25)
                            {
                                if (BLEResponseLEGAPSetDataChannelClassification != null)
                                {
                                    BLEResponseLEGAPSetDataChannelClassification(this, new BlueGecko.BLE.Responses.LEGAP.SetDataChannelClassificationEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 26)
                            {
                                if (BLEResponseLEGAPConnect != null)
                                {
                                    BLEResponseLEGAPConnect(this, new BlueGecko.BLE.Responses.LEGAP.ConnectEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 27)
                            {
                                if (BLEResponseLEGAPSetAdvertiseTXPower != null)
                                {
                                    BLEResponseLEGAPSetAdvertiseTXPower(this, new BlueGecko.BLE.Responses.LEGAP.SetAdvertiseTXPowerEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Int16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 28)
                            {
                                if (BLEResponseLEGAPSetDiscoveryExtendedScanResponse != null)
                                {
                                    BLEResponseLEGAPSetDiscoveryExtendedScanResponse(this, new BlueGecko.BLE.Responses.LEGAP.SetDiscoveryExtendedScanResponseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 29)
                            {
                                if (BLEResponseLEGAPStartPeriodicAdvertising != null)
                                {
                                    BLEResponseLEGAPStartPeriodicAdvertising(this, new BlueGecko.BLE.Responses.LEGAP.StartPeriodicAdvertisingEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 31)
                            {
                                if (BLEResponseLEGAPStopPeriodicAdvertising != null)
                                {
                                    BLEResponseLEGAPStopPeriodicAdvertising(this, new BlueGecko.BLE.Responses.LEGAP.StopPeriodicAdvertisingEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 32)
                            {
                                if (BLEResponseLEGAPSetLongAdvertisingData != null)
                                {
                                    BLEResponseLEGAPSetLongAdvertisingData(this, new BlueGecko.BLE.Responses.LEGAP.SetLongAdvertisingDataEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 33)
                            {
                                if (BLEResponseLEGAPEnableWhitelisting != null)
                                {
                                    BLEResponseLEGAPEnableWhitelisting(this, new BlueGecko.BLE.Responses.LEGAP.EnableWhitelistingEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 34)
                            {
                                if (BLEResponseLEGAPSetConnTimingParameters != null)
                                {
                                    BLEResponseLEGAPSetConnTimingParameters(this, new BlueGecko.BLE.Responses.LEGAP.SetConnTimingParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 37)
                            {
                                if (BLEResponseLEGAPSetAdvertiseRandomAddress != null)
                                {
                                    BLEResponseLEGAPSetAdvertiseRandomAddress(this, new BlueGecko.BLE.Responses.LEGAP.SetAdvertiseRandomAddressEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(6).Take(6).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 38)
                            {
                                if (BLEResponseLEGAPClearAdvertiseRandomAddress != null)
                                {
                                    BLEResponseLEGAPClearAdvertiseRandomAddress(this, new BlueGecko.BLE.Responses.LEGAP.ClearAdvertiseRandomAddressEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 66)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseSyncOpen != null)
                                {
                                    BLEResponseSyncOpen(this, new BlueGecko.BLE.Responses.Sync.OpenEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseSyncClose != null)
                                {
                                    BLEResponseSyncClose(this, new BlueGecko.BLE.Responses.Sync.CloseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 8)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseLEConnectionSetParameters != null)
                                {
                                    BLEResponseLEConnectionSetParameters(this, new BlueGecko.BLE.Responses.LEConnection.SetParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseLEConnectionGetRssi != null)
                                {
                                    BLEResponseLEConnectionGetRssi(this, new BlueGecko.BLE.Responses.LEConnection.GetRssiEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseLEConnectionDisableSlaveLatency != null)
                                {
                                    BLEResponseLEConnectionDisableSlaveLatency(this, new BlueGecko.BLE.Responses.LEConnection.DisableSlaveLatencyEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseLEConnectionSetPHY != null)
                                {
                                    BLEResponseLEConnectionSetPHY(this, new BlueGecko.BLE.Responses.LEConnection.SetPHYEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseLEConnectionClose != null)
                                {
                                    BLEResponseLEConnectionClose(this, new BlueGecko.BLE.Responses.LEConnection.CloseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseLEConnectionSetTimingParameters != null)
                                {
                                    BLEResponseLEConnectionSetTimingParameters(this, new BlueGecko.BLE.Responses.LEConnection.SetTimingParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseLEConnectionReadChannelMap != null)
                                {
                                    BLEResponseLEConnectionReadChannelMap(this, new BlueGecko.BLE.Responses.LEConnection.ReadChannelMapEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEResponseLEConnectionSetPreferredPHY != null)
                                {
                                    BLEResponseLEConnectionSetPreferredPHY(this, new BlueGecko.BLE.Responses.LEConnection.SetPreferredPHYEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 9)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseGATTSetMaxMtu != null)
                                {
                                    BLEResponseGATTSetMaxMtu(this, new BlueGecko.BLE.Responses.GATT.SetMaxMtuEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseGATTDiscoverPrimaryServices != null)
                                {
                                    BLEResponseGATTDiscoverPrimaryServices(this, new BlueGecko.BLE.Responses.GATT.DiscoverPrimaryServicesEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseGATTDiscoverPrimaryServicesByUUID != null)
                                {
                                    BLEResponseGATTDiscoverPrimaryServicesByUUID(this, new BlueGecko.BLE.Responses.GATT.DiscoverPrimaryServicesByUUIDEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseGATTDiscoverCharacteristics != null)
                                {
                                    BLEResponseGATTDiscoverCharacteristics(this, new BlueGecko.BLE.Responses.GATT.DiscoverCharacteristicsEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseGATTDiscoverCharacteristicsByUUID != null)
                                {
                                    BLEResponseGATTDiscoverCharacteristicsByUUID(this, new BlueGecko.BLE.Responses.GATT.DiscoverCharacteristicsByUUIDEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseGATTSetCharacteristicNotification != null)
                                {
                                    BLEResponseGATTSetCharacteristicNotification(this, new BlueGecko.BLE.Responses.GATT.SetCharacteristicNotificationEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseGATTDiscoverDescriptors != null)
                                {
                                    BLEResponseGATTDiscoverDescriptors(this, new BlueGecko.BLE.Responses.GATT.DiscoverDescriptorsEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEResponseGATTReadCharacteristicValue != null)
                                {
                                    BLEResponseGATTReadCharacteristicValue(this, new BlueGecko.BLE.Responses.GATT.ReadCharacteristicValueEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 8)
                            {
                                if (BLEResponseGATTReadCharacteristicValueByUUID != null)
                                {
                                    BLEResponseGATTReadCharacteristicValueByUUID(this, new BlueGecko.BLE.Responses.GATT.ReadCharacteristicValueByUUIDEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 9)
                            {
                                if (BLEResponseGATTWriteCharacteristicValue != null)
                                {
                                    BLEResponseGATTWriteCharacteristicValue(this, new BlueGecko.BLE.Responses.GATT.WriteCharacteristicValueEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 10)
                            {
                                if (BLEResponseGATTWriteCharacteristicValueWithoutResponse != null)
                                {
                                    BLEResponseGATTWriteCharacteristicValueWithoutResponse(this, new BlueGecko.BLE.Responses.GATT.WriteCharacteristicValueWithoutResponseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 11)
                            {
                                if (BLEResponseGATTPrepareCharacteristicValueWrite != null)
                                {
                                    BLEResponseGATTPrepareCharacteristicValueWrite(this, new BlueGecko.BLE.Responses.GATT.PrepareCharacteristicValueWriteEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 12)
                            {
                                if (BLEResponseGATTExecuteCharacteristicValueWrite != null)
                                {
                                    BLEResponseGATTExecuteCharacteristicValueWrite(this, new BlueGecko.BLE.Responses.GATT.ExecuteCharacteristicValueWriteEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 13)
                            {
                                if (BLEResponseGATTSendCharacteristicConfirmation != null)
                                {
                                    BLEResponseGATTSendCharacteristicConfirmation(this, new BlueGecko.BLE.Responses.GATT.SendCharacteristicConfirmationEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 14)
                            {
                                if (BLEResponseGATTReadDescriptorValue != null)
                                {
                                    BLEResponseGATTReadDescriptorValue(this, new BlueGecko.BLE.Responses.GATT.ReadDescriptorValueEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 15)
                            {
                                if (BLEResponseGATTWriteDescriptorValue != null)
                                {
                                    BLEResponseGATTWriteDescriptorValue(this, new BlueGecko.BLE.Responses.GATT.WriteDescriptorValueEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 16)
                            {
                                if (BLEResponseGATTFindIncludedServices != null)
                                {
                                    BLEResponseGATTFindIncludedServices(this, new BlueGecko.BLE.Responses.GATT.FindIncludedServicesEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 17)
                            {
                                if (BLEResponseGATTReadMultipleCharacteristicValues != null)
                                {
                                    BLEResponseGATTReadMultipleCharacteristicValues(this, new BlueGecko.BLE.Responses.GATT.ReadMultipleCharacteristicValuesEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 18)
                            {
                                if (BLEResponseGATTReadCharacteristicValueFromOffset != null)
                                {
                                    BLEResponseGATTReadCharacteristicValueFromOffset(this, new BlueGecko.BLE.Responses.GATT.ReadCharacteristicValueFromOffsetEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 19)
                            {
                                if (BLEResponseGATTPrepareCharacteristicValueReliableWrite != null)
                                {
                                    BLEResponseGATTPrepareCharacteristicValueReliableWrite(this, new BlueGecko.BLE.Responses.GATT.PrepareCharacteristicValueReliableWriteEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 10)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseGATTServerReadAttributeValue != null)
                                {
                                    BLEResponseGATTServerReadAttributeValue(this, new BlueGecko.BLE.Responses.GATTServer.ReadAttributeValueEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseGATTServerReadAttributeType != null)
                                {
                                    BLEResponseGATTServerReadAttributeType(this, new BlueGecko.BLE.Responses.GATTServer.ReadAttributeTypeEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseGATTServerWriteAttributeValue != null)
                                {
                                    BLEResponseGATTServerWriteAttributeValue(this, new BlueGecko.BLE.Responses.GATTServer.WriteAttributeValueEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseGATTServerSendUserReadResponse != null)
                                {
                                    BLEResponseGATTServerSendUserReadResponse(this, new BlueGecko.BLE.Responses.GATTServer.SendUserReadResponseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseGATTServerSendUserWriteResponse != null)
                                {
                                    BLEResponseGATTServerSendUserWriteResponse(this, new BlueGecko.BLE.Responses.GATTServer.SendUserWriteResponseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseGATTServerSendCharacteristicNotification != null)
                                {
                                    BLEResponseGATTServerSendCharacteristicNotification(this, new BlueGecko.BLE.Responses.GATTServer.SendCharacteristicNotificationEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseGATTServerFindAttribute != null)
                                {
                                    BLEResponseGATTServerFindAttribute(this, new BlueGecko.BLE.Responses.GATTServer.FindAttributeEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 8)
                            {
                                if (BLEResponseGATTServerSetCapabilities != null)
                                {
                                    BLEResponseGATTServerSetCapabilities(this, new BlueGecko.BLE.Responses.GATTServer.SetCapabilitiesEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 9)
                            {
                                if (BLEResponseGATTServerFindPrimaryService != null)
                                {
                                    BLEResponseGATTServerFindPrimaryService(this, new BlueGecko.BLE.Responses.GATTServer.FindPrimaryServiceEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8)),
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 10)
                            {
                                if (BLEResponseGATTServerSetMaxMtu != null)
                                {
                                    BLEResponseGATTServerSetMaxMtu(this, new BlueGecko.BLE.Responses.GATTServer.SetMaxMtuEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 11)
                            {
                                if (BLEResponseGATTServerGetMtu != null)
                                {
                                    BLEResponseGATTServerGetMtu(this, new BlueGecko.BLE.Responses.GATTServer.GetMtuEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 12)
                            {
                                if (BLEResponseGATTServerEnableCapabilities != null)
                                {
                                    BLEResponseGATTServerEnableCapabilities(this, new BlueGecko.BLE.Responses.GATTServer.EnableCapabilitiesEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 13)
                            {
                                if (BLEResponseGATTServerDisableCapabilities != null)
                                {
                                    BLEResponseGATTServerDisableCapabilities(this, new BlueGecko.BLE.Responses.GATTServer.DisableCapabilitiesEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 14)
                            {
                                if (BLEResponseGATTServerGetEnabledCapabilities != null)
                                {
                                    BLEResponseGATTServerGetEnabledCapabilities(this, new BlueGecko.BLE.Responses.GATTServer.GetEnabledCapabilitiesEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt32)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8) + (bgapiRXBuffer[8] << 16) + (bgapiRXBuffer[9] << 24))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 12)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseHardwareSetSoftTimer != null)
                                {
                                    BLEResponseHardwareSetSoftTimer(this, new BlueGecko.BLE.Responses.Hardware.SetSoftTimerEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 11)
                            {
                                if (BLEResponseHardwareGetTime != null)
                                {
                                    BLEResponseHardwareGetTime(this, new BlueGecko.BLE.Responses.Hardware.GetTimeEventArgs(
                                        (UInt32)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8) + (bgapiRXBuffer[6] << 16) + (bgapiRXBuffer[7] << 24)),
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 12)
                            {
                                if (BLEResponseHardwareSetLazySoftTimer != null)
                                {
                                    BLEResponseHardwareSetLazySoftTimer(this, new BlueGecko.BLE.Responses.Hardware.SetLazySoftTimerEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 13)
                        {
                            if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseFlashPSEraseAll != null)
                                {
                                    BLEResponseFlashPSEraseAll(this, new BlueGecko.BLE.Responses.Flash.PSEraseAllEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseFlashPSSave != null)
                                {
                                    BLEResponseFlashPSSave(this, new BlueGecko.BLE.Responses.Flash.PSSaveEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseFlashPSLoad != null)
                                {
                                    BLEResponseFlashPSLoad(this, new BlueGecko.BLE.Responses.Flash.PSLoadEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseFlashPSErase != null)
                                {
                                    BLEResponseFlashPSErase(this, new BlueGecko.BLE.Responses.Flash.PSEraseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 14)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseTestDTMTX != null)
                                {
                                    BLEResponseTestDTMTX(this, new BlueGecko.BLE.Responses.Test.DTMTXEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseTestDTMRX != null)
                                {
                                    BLEResponseTestDTMRX(this, new BlueGecko.BLE.Responses.Test.DTMRXEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseTestDTMEnd != null)
                                {
                                    BLEResponseTestDTMEnd(this, new BlueGecko.BLE.Responses.Test.DTMEndEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEResponseTestDebugCommand != null)
                                {
                                    BLEResponseTestDebugCommand(this, new BlueGecko.BLE.Responses.Test.DebugCommandEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6],
                                        (Byte[])(bgapiRXBuffer.Skip(8).Take(bgapiRXBuffer[7]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 12)
                            {
                                if (BLEResponseTestDebugCounter != null)
                                {
                                    BLEResponseTestDebugCounter(this, new BlueGecko.BLE.Responses.Test.DebugCounterEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt32)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8) + (bgapiRXBuffer[8] << 16) + (bgapiRXBuffer[9] << 24))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 15)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseSMSetBondableMode != null)
                                {
                                    BLEResponseSMSetBondableMode(this, new BlueGecko.BLE.Responses.SM.SetBondableModeEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseSMConfigure != null)
                                {
                                    BLEResponseSMConfigure(this, new BlueGecko.BLE.Responses.SM.ConfigureEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseSMStoreBondingConfiguration != null)
                                {
                                    BLEResponseSMStoreBondingConfiguration(this, new BlueGecko.BLE.Responses.SM.StoreBondingConfigurationEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseSMIncreaseSecurity != null)
                                {
                                    BLEResponseSMIncreaseSecurity(this, new BlueGecko.BLE.Responses.SM.IncreaseSecurityEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseSMDeleteBonding != null)
                                {
                                    BLEResponseSMDeleteBonding(this, new BlueGecko.BLE.Responses.SM.DeleteBondingEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEResponseSMDeleteBondings != null)
                                {
                                    BLEResponseSMDeleteBondings(this, new BlueGecko.BLE.Responses.SM.DeleteBondingsEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 8)
                            {
                                if (BLEResponseSMEnterPasskey != null)
                                {
                                    BLEResponseSMEnterPasskey(this, new BlueGecko.BLE.Responses.SM.EnterPasskeyEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 9)
                            {
                                if (BLEResponseSMPasskeyConfirm != null)
                                {
                                    BLEResponseSMPasskeyConfirm(this, new BlueGecko.BLE.Responses.SM.PasskeyConfirmEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 10)
                            {
                                if (BLEResponseSMSetOobData != null)
                                {
                                    BLEResponseSMSetOobData(this, new BlueGecko.BLE.Responses.SM.SetOobDataEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 11)
                            {
                                if (BLEResponseSMListAllBondings != null)
                                {
                                    BLEResponseSMListAllBondings(this, new BlueGecko.BLE.Responses.SM.ListAllBondingsEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 14)
                            {
                                if (BLEResponseSMBondingConfirm != null)
                                {
                                    BLEResponseSMBondingConfirm(this, new BlueGecko.BLE.Responses.SM.BondingConfirmEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 15)
                            {
                                if (BLEResponseSMSetDebugMode != null)
                                {
                                    BLEResponseSMSetDebugMode(this, new BlueGecko.BLE.Responses.SM.SetDebugModeEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 16)
                            {
                                if (BLEResponseSMSetPasskey != null)
                                {
                                    BLEResponseSMSetPasskey(this, new BlueGecko.BLE.Responses.SM.SetPasskeyEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 17)
                            {
                                if (BLEResponseSMUseScOob != null)
                                {
                                    BLEResponseSMUseScOob(this, new BlueGecko.BLE.Responses.SM.UseScOobEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 18)
                            {
                                if (BLEResponseSMSetScRemoteOobData != null)
                                {
                                    BLEResponseSMSetScRemoteOobData(this, new BlueGecko.BLE.Responses.SM.SetScRemoteOobDataEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 19)
                            {
                                if (BLEResponseSMAddToWhitelist != null)
                                {
                                    BLEResponseSMAddToWhitelist(this, new BlueGecko.BLE.Responses.SM.AddToWhitelistEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 20)
                            {
                                if (BLEResponseSMSetMinimumKeySize != null)
                                {
                                    BLEResponseSMSetMinimumKeySize(this, new BlueGecko.BLE.Responses.SM.SetMinimumKeySizeEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 19)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseHomekitConfigure != null)
                                {
                                    BLEResponseHomekitConfigure(this, new BlueGecko.BLE.Responses.Homekit.ConfigureEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseHomekitAdvertise != null)
                                {
                                    BLEResponseHomekitAdvertise(this, new BlueGecko.BLE.Responses.Homekit.AdvertiseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseHomekitDeletePairings != null)
                                {
                                    BLEResponseHomekitDeletePairings(this, new BlueGecko.BLE.Responses.Homekit.DeletePairingsEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseHomekitCheckAuthcp != null)
                                {
                                    BLEResponseHomekitCheckAuthcp(this, new BlueGecko.BLE.Responses.Homekit.CheckAuthcpEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseHomekitGetPairingId != null)
                                {
                                    BLEResponseHomekitGetPairingId(this, new BlueGecko.BLE.Responses.Homekit.GetPairingIdEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseHomekitSendWriteResponse != null)
                                {
                                    BLEResponseHomekitSendWriteResponse(this, new BlueGecko.BLE.Responses.Homekit.SendWriteResponseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseHomekitSendReadResponse != null)
                                {
                                    BLEResponseHomekitSendReadResponse(this, new BlueGecko.BLE.Responses.Homekit.SendReadResponseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEResponseHomekitGsnAction != null)
                                {
                                    BLEResponseHomekitGsnAction(this, new BlueGecko.BLE.Responses.Homekit.GsnActionEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 8)
                            {
                                if (BLEResponseHomekitEventNotification != null)
                                {
                                    BLEResponseHomekitEventNotification(this, new BlueGecko.BLE.Responses.Homekit.EventNotificationEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 9)
                            {
                                if (BLEResponseHomekitBroadcastAction != null)
                                {
                                    BLEResponseHomekitBroadcastAction(this, new BlueGecko.BLE.Responses.Homekit.BroadcastActionEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 10)
                            {
                                if (BLEResponseHomekitConfigureProductData != null)
                                {
                                    BLEResponseHomekitConfigureProductData(this, new BlueGecko.BLE.Responses.Homekit.ConfigureProductDataEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 32)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseCoexSetOptions != null)
                                {
                                    BLEResponseCoexSetOptions(this, new BlueGecko.BLE.Responses.Coex.SetOptionsEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseCoexGetCounters != null)
                                {
                                    BLEResponseCoexGetCounters(this, new BlueGecko.BLE.Responses.Coex.GetCountersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseCoexSetParameters != null)
                                {
                                    BLEResponseCoexSetParameters(this, new BlueGecko.BLE.Responses.Coex.SetParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseCoexSetDirectionalPriorityPulse != null)
                                {
                                    BLEResponseCoexSetDirectionalPriorityPulse(this, new BlueGecko.BLE.Responses.Coex.SetDirectionalPriorityPulseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 67)
                        {
                            if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseL2CAPCocSendConnectionRequest != null)
                                {
                                    BLEResponseL2CAPCocSendConnectionRequest(this, new BlueGecko.BLE.Responses.L2CAP.CocSendConnectionRequestEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseL2CAPCocSendConnectionResponse != null)
                                {
                                    BLEResponseL2CAPCocSendConnectionResponse(this, new BlueGecko.BLE.Responses.L2CAP.CocSendConnectionResponseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseL2CAPCocSendLEFlowControlCredit != null)
                                {
                                    BLEResponseL2CAPCocSendLEFlowControlCredit(this, new BlueGecko.BLE.Responses.L2CAP.CocSendLEFlowControlCreditEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseL2CAPCocSendDisconnectionRequest != null)
                                {
                                    BLEResponseL2CAPCocSendDisconnectionRequest(this, new BlueGecko.BLE.Responses.L2CAP.CocSendDisconnectionRequestEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseL2CAPCocSendData != null)
                                {
                                    BLEResponseL2CAPCocSendData(this, new BlueGecko.BLE.Responses.L2CAP.CocSendDataEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 68)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseCTETransmitterEnableCTEResponse != null)
                                {
                                    BLEResponseCTETransmitterEnableCTEResponse(this, new BlueGecko.BLE.Responses.CTETransmitter.EnableCTEResponseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseCTETransmitterDisableCTEResponse != null)
                                {
                                    BLEResponseCTETransmitterDisableCTEResponse(this, new BlueGecko.BLE.Responses.CTETransmitter.DisableCTEResponseEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseCTETransmitterStartConnectionlessCTE != null)
                                {
                                    BLEResponseCTETransmitterStartConnectionlessCTE(this, new BlueGecko.BLE.Responses.CTETransmitter.StartConnectionlessCTEEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseCTETransmitterStopConnectionlessCTE != null)
                                {
                                    BLEResponseCTETransmitterStopConnectionlessCTE(this, new BlueGecko.BLE.Responses.CTETransmitter.StopConnectionlessCTEEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseCTETransmitterSetDTMParameters != null)
                                {
                                    BLEResponseCTETransmitterSetDTMParameters(this, new BlueGecko.BLE.Responses.CTETransmitter.SetDTMParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseCTETransmitterClearDTMParameters != null)
                                {
                                    BLEResponseCTETransmitterClearDTMParameters(this, new BlueGecko.BLE.Responses.CTETransmitter.ClearDTMParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 69)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseCTEReceiverConfigure != null)
                                {
                                    BLEResponseCTEReceiverConfigure(this, new BlueGecko.BLE.Responses.CTEReceiver.ConfigureEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEResponseCTEReceiverStartIqSampling != null)
                                {
                                    BLEResponseCTEReceiverStartIqSampling(this, new BlueGecko.BLE.Responses.CTEReceiver.StartIqSamplingEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEResponseCTEReceiverStopIqSampling != null)
                                {
                                    BLEResponseCTEReceiverStopIqSampling(this, new BlueGecko.BLE.Responses.CTEReceiver.StopIqSamplingEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEResponseCTEReceiverStartConnectionlessIqSampling != null)
                                {
                                    BLEResponseCTEReceiverStartConnectionlessIqSampling(this, new BlueGecko.BLE.Responses.CTEReceiver.StartConnectionlessIqSamplingEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEResponseCTEReceiverStopConnectionlessIqSampling != null)
                                {
                                    BLEResponseCTEReceiverStopConnectionlessIqSampling(this, new BlueGecko.BLE.Responses.CTEReceiver.StopConnectionlessIqSamplingEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEResponseCTEReceiverSetDTMParameters != null)
                                {
                                    BLEResponseCTEReceiverSetDTMParameters(this, new BlueGecko.BLE.Responses.CTEReceiver.SetDTMParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEResponseCTEReceiverClearDTMParameters != null)
                                {
                                    BLEResponseCTEReceiverClearDTMParameters(this, new BlueGecko.BLE.Responses.CTEReceiver.ClearDTMParametersEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 254)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseQualtesterConfigure != null)
                                {
                                    BLEResponseQualtesterConfigure(this, new BlueGecko.BLE.Responses.Qualtester.ConfigureEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 255)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEResponseUserMessageToTarget != null)
                                {
                                    BLEResponseUserMessageToTarget(this, new BlueGecko.BLE.Responses.User.MessageToTargetEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                        }
                        SetBusy(false);
                    }
                    else
                    {
                        // 0x80 = Event packet
                        if (bgapiRXBuffer[2] == 0)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventDFUBoot != null)
                                {
                                    BLEEventDFUBoot(this, new BlueGecko.BLE.Events.DFU.BootEventArgs(
                                        (UInt32)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8) + (bgapiRXBuffer[6] << 16) + (bgapiRXBuffer[7] << 24))
                                    ));
                                }
                                SetBusy(false);
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventDFUBootFailure != null)
                                {
                                    BLEEventDFUBootFailure(this, new BlueGecko.BLE.Events.DFU.BootFailureEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 1)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventSystemBoot != null)
                                {
                                    BLEEventSystemBoot(this, new BlueGecko.BLE.Events.System.BootEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8)),
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8)),
                                        (UInt16)(bgapiRXBuffer[10] + (bgapiRXBuffer[11] << 8)),
                                        (UInt32)(bgapiRXBuffer[12] + (bgapiRXBuffer[13] << 8) + (bgapiRXBuffer[14] << 16) + (bgapiRXBuffer[15] << 24)),
                                        (UInt16)(bgapiRXBuffer[16] + (bgapiRXBuffer[17] << 8)),
                                        (UInt32)(bgapiRXBuffer[18] + (bgapiRXBuffer[19] << 8) + (bgapiRXBuffer[20] << 16) + (bgapiRXBuffer[21] << 24))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEEventSystemExternalSignal != null)
                                {
                                    BLEEventSystemExternalSignal(this, new BlueGecko.BLE.Events.System.ExternalSignalEventArgs(
                                        (UInt32)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8) + (bgapiRXBuffer[6] << 16) + (bgapiRXBuffer[7] << 24))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEEventSystemAwake != null)
                                {
                                    BLEEventSystemAwake(this, new BlueGecko.BLE.Events.System.AwakeEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEEventSystemHardwareError != null)
                                {
                                    BLEEventSystemHardwareError(this, new BlueGecko.BLE.Events.System.HardwareErrorEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEEventSystemError != null)
                                {
                                    BLEEventSystemError(this, new BlueGecko.BLE.Events.System.ErrorEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(7).Take(bgapiRXBuffer[6]).ToArray())
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 3)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventLEGAPScanResponse != null)
                                {
                                    BLEEventLEGAPScanResponse(this, new BlueGecko.BLE.Events.LEGAP.ScanResponseEventArgs(
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
                                if (BLEEventLEGAPAdvTimeout != null)
                                {
                                    BLEEventLEGAPAdvTimeout(this, new BlueGecko.BLE.Events.LEGAP.AdvTimeoutEventArgs(
                                        bgapiRXBuffer[4]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventLEGAPScanRequest != null)
                                {
                                    BLEEventLEGAPScanRequest(this, new BlueGecko.BLE.Events.LEGAP.ScanRequestEventArgs(
                                        bgapiRXBuffer[4],
                                        (Byte[])(bgapiRXBuffer.Skip(5).Take(6).ToArray()),
                                        bgapiRXBuffer[11],
                                        bgapiRXBuffer[12]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEEventLEGAPExtendedScanResponse != null)
                                {
                                    BLEEventLEGAPExtendedScanResponse(this, new BlueGecko.BLE.Events.LEGAP.ExtendedScanResponseEventArgs(
                                        bgapiRXBuffer[4],
                                        (Byte[])(bgapiRXBuffer.Skip(5).Take(6).ToArray()),
                                        bgapiRXBuffer[11],
                                        bgapiRXBuffer[12],
                                        bgapiRXBuffer[13],
                                        bgapiRXBuffer[14],
                                        bgapiRXBuffer[15],
                                        (SByte)(bgapiRXBuffer[16]),
                                        (SByte)(bgapiRXBuffer[17]),
                                        bgapiRXBuffer[18],
                                        (UInt16)(bgapiRXBuffer[19] + (bgapiRXBuffer[20] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(22).Take(bgapiRXBuffer[21]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEEventLEGAPPeriodicAdvertisingStatus != null)
                                {
                                    BLEEventLEGAPPeriodicAdvertisingStatus(this, new BlueGecko.BLE.Events.LEGAP.PeriodicAdvertisingStatusEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt32)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8) + (bgapiRXBuffer[7] << 16) + (bgapiRXBuffer[8] << 24))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 66)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventSyncOpened != null)
                                {
                                    BLEEventSyncOpened(this, new BlueGecko.BLE.Events.Sync.OpenedEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5],
                                        (Byte[])(bgapiRXBuffer.Skip(6).Take(6).ToArray()),
                                        bgapiRXBuffer[12],
                                        bgapiRXBuffer[13],
                                        (UInt16)(bgapiRXBuffer[14] + (bgapiRXBuffer[15] << 8)),
                                        (UInt16)(bgapiRXBuffer[16] + (bgapiRXBuffer[17] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventSyncClosed != null)
                                {
                                    BLEEventSyncClosed(this, new BlueGecko.BLE.Events.Sync.ClosedEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventSyncData != null)
                                {
                                    BLEEventSyncData(this, new BlueGecko.BLE.Events.Sync.DataEventArgs(
                                        bgapiRXBuffer[4],
                                        (SByte)(bgapiRXBuffer[5]),
                                        (SByte)(bgapiRXBuffer[6]),
                                        bgapiRXBuffer[7],
                                        (Byte[])(bgapiRXBuffer.Skip(9).Take(bgapiRXBuffer[8]).ToArray())
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 8)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventLEConnectionOpened != null)
                                {
                                    BLEEventLEConnectionOpened(this, new BlueGecko.BLE.Events.LEConnection.OpenedEventArgs(
                                        (Byte[])(bgapiRXBuffer.Skip(4).Take(6).ToArray()),
                                        bgapiRXBuffer[10],
                                        bgapiRXBuffer[11],
                                        bgapiRXBuffer[12],
                                        bgapiRXBuffer[13],
                                        bgapiRXBuffer[14]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventLEConnectionClosed != null)
                                {
                                    BLEEventLEConnectionClosed(this, new BlueGecko.BLE.Events.LEConnection.ClosedEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventLEConnectionParameters != null)
                                {
                                    BLEEventLEConnectionParameters(this, new BlueGecko.BLE.Events.LEConnection.ParametersEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (UInt16)(bgapiRXBuffer[7] + (bgapiRXBuffer[8] << 8)),
                                        (UInt16)(bgapiRXBuffer[9] + (bgapiRXBuffer[10] << 8)),
                                        bgapiRXBuffer[11],
                                        (UInt16)(bgapiRXBuffer[12] + (bgapiRXBuffer[13] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEEventLEConnectionRssi != null)
                                {
                                    BLEEventLEConnectionRssi(this, new BlueGecko.BLE.Events.LEConnection.RssiEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5],
                                        (SByte)(bgapiRXBuffer[6])
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEEventLEConnectionPHYStatus != null)
                                {
                                    BLEEventLEConnectionPHYStatus(this, new BlueGecko.BLE.Events.LEConnection.PHYStatusEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5]
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 9)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventGATTMtuExchanged != null)
                                {
                                    BLEEventGATTMtuExchanged(this, new BlueGecko.BLE.Events.GATT.MtuExchangedEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventGATTService != null)
                                {
                                    BLEEventGATTService(this, new BlueGecko.BLE.Events.GATT.ServiceEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt32)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8) + (bgapiRXBuffer[7] << 16) + (bgapiRXBuffer[8] << 24)),
                                        (Byte[])(bgapiRXBuffer.Skip(10).Take(bgapiRXBuffer[9]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventGATTCharacteristic != null)
                                {
                                    BLEEventGATTCharacteristic(this, new BlueGecko.BLE.Events.GATT.CharacteristicEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        bgapiRXBuffer[7],
                                        (Byte[])(bgapiRXBuffer.Skip(9).Take(bgapiRXBuffer[8]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEEventGATTDescriptor != null)
                                {
                                    BLEEventGATTDescriptor(this, new BlueGecko.BLE.Events.GATT.DescriptorEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(8).Take(bgapiRXBuffer[7]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEEventGATTCharacteristicValue != null)
                                {
                                    BLEEventGATTCharacteristicValue(this, new BlueGecko.BLE.Events.GATT.CharacteristicValueEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        bgapiRXBuffer[7],
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(11).Take(bgapiRXBuffer[10]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEEventGATTDescriptorValue != null)
                                {
                                    BLEEventGATTDescriptorValue(this, new BlueGecko.BLE.Events.GATT.DescriptorValueEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (UInt16)(bgapiRXBuffer[7] + (bgapiRXBuffer[8] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(10).Take(bgapiRXBuffer[9]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEEventGATTProcedureCompleted != null)
                                {
                                    BLEEventGATTProcedureCompleted(this, new BlueGecko.BLE.Events.GATT.ProcedureCompletedEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 10)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventGATTServerAttributeValue != null)
                                {
                                    BLEEventGATTServerAttributeValue(this, new BlueGecko.BLE.Events.GATTServer.AttributeValueEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        bgapiRXBuffer[7],
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(11).Take(bgapiRXBuffer[10]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventGATTServerUserReadRequest != null)
                                {
                                    BLEEventGATTServerUserReadRequest(this, new BlueGecko.BLE.Events.GATTServer.UserReadRequestEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        bgapiRXBuffer[7],
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventGATTServerUserWriteRequest != null)
                                {
                                    BLEEventGATTServerUserWriteRequest(this, new BlueGecko.BLE.Events.GATTServer.UserWriteRequestEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        bgapiRXBuffer[7],
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(11).Take(bgapiRXBuffer[10]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEEventGATTServerCharacteristicStatus != null)
                                {
                                    BLEEventGATTServerCharacteristicStatus(this, new BlueGecko.BLE.Events.GATTServer.CharacteristicStatusEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        bgapiRXBuffer[7],
                                        (UInt16)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEEventGATTServerExecuteWriteCompleted != null)
                                {
                                    BLEEventGATTServerExecuteWriteCompleted(this, new BlueGecko.BLE.Events.GATTServer.ExecuteWriteCompletedEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 12)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventHardwareSoftTimer != null)
                                {
                                    BLEEventHardwareSoftTimer(this, new BlueGecko.BLE.Events.Hardware.SoftTimerEventArgs(
                                        bgapiRXBuffer[4]
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 13)
                        {
                        }
                        else if (bgapiRXBuffer[2] == 14)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventTestDTMCompleted != null)
                                {
                                    BLEEventTestDTMCompleted(this, new BlueGecko.BLE.Events.Test.DTMCompletedEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 15)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventSMPasskeyDisplay != null)
                                {
                                    BLEEventSMPasskeyDisplay(this, new BlueGecko.BLE.Events.SM.PasskeyDisplayEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt32)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8) + (bgapiRXBuffer[7] << 16) + (bgapiRXBuffer[8] << 24))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventSMPasskeyRequest != null)
                                {
                                    BLEEventSMPasskeyRequest(this, new BlueGecko.BLE.Events.SM.PasskeyRequestEventArgs(
                                        bgapiRXBuffer[4]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventSMConfirmPasskey != null)
                                {
                                    BLEEventSMConfirmPasskey(this, new BlueGecko.BLE.Events.SM.ConfirmPasskeyEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt32)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8) + (bgapiRXBuffer[7] << 16) + (bgapiRXBuffer[8] << 24))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEEventSMBonded != null)
                                {
                                    BLEEventSMBonded(this, new BlueGecko.BLE.Events.SM.BondedEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEEventSMBondingFailed != null)
                                {
                                    BLEEventSMBondingFailed(this, new BlueGecko.BLE.Events.SM.BondingFailedEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEEventSMListBondingEntry != null)
                                {
                                    BLEEventSMListBondingEntry(this, new BlueGecko.BLE.Events.SM.ListBondingEntryEventArgs(
                                        bgapiRXBuffer[4],
                                        (Byte[])(bgapiRXBuffer.Skip(5).Take(6).ToArray()),
                                        bgapiRXBuffer[11]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEEventSMListAllBondingsComplete != null)
                                {
                                    BLEEventSMListAllBondingsComplete(this, new BlueGecko.BLE.Events.SM.ListAllBondingsCompleteEventArgs(
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 9)
                            {
                                if (BLEEventSMConfirmBonding != null)
                                {
                                    BLEEventSMConfirmBonding(this, new BlueGecko.BLE.Events.SM.ConfirmBondingEventArgs(
                                        bgapiRXBuffer[4],
                                        (SByte)(bgapiRXBuffer[5])
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 19)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventHomekitSetupcodeDisplay != null)
                                {
                                    BLEEventHomekitSetupcodeDisplay(this, new BlueGecko.BLE.Events.Homekit.SetupcodeDisplayEventArgs(
                                        bgapiRXBuffer[4],
                                        (Byte[])(bgapiRXBuffer.Skip(6).Take(bgapiRXBuffer[5]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventHomekitPaired != null)
                                {
                                    BLEEventHomekitPaired(this, new BlueGecko.BLE.Events.Homekit.PairedEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventHomekitPairVerified != null)
                                {
                                    BLEEventHomekitPairVerified(this, new BlueGecko.BLE.Events.Homekit.PairVerifiedEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEEventHomekitConnectionOpened != null)
                                {
                                    BLEEventHomekitConnectionOpened(this, new BlueGecko.BLE.Events.Homekit.ConnectionOpenedEventArgs(
                                        bgapiRXBuffer[4]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEEventHomekitConnectionClosed != null)
                                {
                                    BLEEventHomekitConnectionClosed(this, new BlueGecko.BLE.Events.Homekit.ConnectionClosedEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEEventHomekitIdentify != null)
                                {
                                    BLEEventHomekitIdentify(this, new BlueGecko.BLE.Events.Homekit.IdentifyEventArgs(
                                        bgapiRXBuffer[4]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEEventHomekitWriteRequest != null)
                                {
                                    BLEEventHomekitWriteRequest(this, new BlueGecko.BLE.Events.Homekit.WriteRequestEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (UInt16)(bgapiRXBuffer[7] + (bgapiRXBuffer[8] << 8)),
                                        (UInt16)(bgapiRXBuffer[9] + (bgapiRXBuffer[10] << 8)),
                                        (UInt16)(bgapiRXBuffer[11] + (bgapiRXBuffer[12] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(14).Take(bgapiRXBuffer[13]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 7)
                            {
                                if (BLEEventHomekitReadRequest != null)
                                {
                                    BLEEventHomekitReadRequest(this, new BlueGecko.BLE.Events.Homekit.ReadRequestEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (UInt16)(bgapiRXBuffer[7] + (bgapiRXBuffer[8] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 8)
                            {
                                if (BLEEventHomekitDisconnectionRequired != null)
                                {
                                    BLEEventHomekitDisconnectionRequired(this, new BlueGecko.BLE.Events.Homekit.DisconnectionRequiredEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 9)
                            {
                                if (BLEEventHomekitPairingRemoved != null)
                                {
                                    BLEEventHomekitPairingRemoved(this, new BlueGecko.BLE.Events.Homekit.PairingRemovedEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(8).Take(bgapiRXBuffer[7]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 10)
                            {
                                if (BLEEventHomekitSetuppayloadDisplay != null)
                                {
                                    BLEEventHomekitSetuppayloadDisplay(this, new BlueGecko.BLE.Events.Homekit.SetuppayloadDisplayEventArgs(
                                        bgapiRXBuffer[4],
                                        (Byte[])(bgapiRXBuffer.Skip(6).Take(bgapiRXBuffer[5]).ToArray())
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 32)
                        {
                        }
                        else if (bgapiRXBuffer[2] == 67)
                        {
                            if (bgapiRXBuffer[3] == 1)
                            {
                                if (BLEEventL2CAPCocConnectionRequest != null)
                                {
                                    BLEEventL2CAPCocConnectionRequest(this, new BlueGecko.BLE.Events.L2CAP.CocConnectionRequestEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (UInt16)(bgapiRXBuffer[7] + (bgapiRXBuffer[8] << 8)),
                                        (UInt16)(bgapiRXBuffer[9] + (bgapiRXBuffer[10] << 8)),
                                        (UInt16)(bgapiRXBuffer[11] + (bgapiRXBuffer[12] << 8)),
                                        (UInt16)(bgapiRXBuffer[13] + (bgapiRXBuffer[14] << 8)),
                                        bgapiRXBuffer[15],
                                        bgapiRXBuffer[16]
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 2)
                            {
                                if (BLEEventL2CAPCocConnectionResponse != null)
                                {
                                    BLEEventL2CAPCocConnectionResponse(this, new BlueGecko.BLE.Events.L2CAP.CocConnectionResponseEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (UInt16)(bgapiRXBuffer[7] + (bgapiRXBuffer[8] << 8)),
                                        (UInt16)(bgapiRXBuffer[9] + (bgapiRXBuffer[10] << 8)),
                                        (UInt16)(bgapiRXBuffer[11] + (bgapiRXBuffer[12] << 8)),
                                        (UInt16)(bgapiRXBuffer[13] + (bgapiRXBuffer[14] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 3)
                            {
                                if (BLEEventL2CAPCocLEFlowControlCredit != null)
                                {
                                    BLEEventL2CAPCocLEFlowControlCredit(this, new BlueGecko.BLE.Events.L2CAP.CocLEFlowControlCreditEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (UInt16)(bgapiRXBuffer[7] + (bgapiRXBuffer[8] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 4)
                            {
                                if (BLEEventL2CAPCocChannelDisconnected != null)
                                {
                                    BLEEventL2CAPCocChannelDisconnected(this, new BlueGecko.BLE.Events.L2CAP.CocChannelDisconnectedEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (UInt16)(bgapiRXBuffer[7] + (bgapiRXBuffer[8] << 8))
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 5)
                            {
                                if (BLEEventL2CAPCocData != null)
                                {
                                    BLEEventL2CAPCocData(this, new BlueGecko.BLE.Events.L2CAP.CocDataEventArgs(
                                        bgapiRXBuffer[4],
                                        (UInt16)(bgapiRXBuffer[5] + (bgapiRXBuffer[6] << 8)),
                                        (Byte[])(bgapiRXBuffer.Skip(8).Take(bgapiRXBuffer[7]).ToArray())
                                    ));
                                }
                            }
                            else if (bgapiRXBuffer[3] == 6)
                            {
                                if (BLEEventL2CAPCommandRejected != null)
                                {
                                    BLEEventL2CAPCommandRejected(this, new BlueGecko.BLE.Events.L2CAP.CommandRejectedEventArgs(
                                        bgapiRXBuffer[4],
                                        bgapiRXBuffer[5],
                                        (UInt16)(bgapiRXBuffer[6] + (bgapiRXBuffer[7] << 8))
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 68)
                        {
                        }
                        else if (bgapiRXBuffer[2] == 69)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventCTEReceiverIqReport != null)
                                {
                                    BLEEventCTEReceiverIqReport(this, new BlueGecko.BLE.Events.CTEReceiver.IqReportEventArgs(
                                        (UInt16)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8)),
                                        bgapiRXBuffer[6],
                                        bgapiRXBuffer[7],
                                        bgapiRXBuffer[8],
                                        bgapiRXBuffer[9],
                                        (SByte)(bgapiRXBuffer[10]),
                                        bgapiRXBuffer[11],
                                        bgapiRXBuffer[12],
                                        bgapiRXBuffer[13],
                                        (UInt16)(bgapiRXBuffer[14] + (bgapiRXBuffer[15] << 8)),
                                        bgapiRXBuffer[16],
                                        (Byte[])(bgapiRXBuffer.Skip(18).Take(bgapiRXBuffer[17]).ToArray())
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 254)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventQualtesterStateChanged != null)
                                {
                                    BLEEventQualtesterStateChanged(this, new BlueGecko.BLE.Events.Qualtester.StateChangedEventArgs(
                                        (UInt32)(bgapiRXBuffer[4] + (bgapiRXBuffer[5] << 8) + (bgapiRXBuffer[6] << 16) + (bgapiRXBuffer[7] << 24)),
                                        (UInt32)(bgapiRXBuffer[8] + (bgapiRXBuffer[9] << 8) + (bgapiRXBuffer[10] << 16) + (bgapiRXBuffer[11] << 24)),
                                        (UInt32)(bgapiRXBuffer[12] + (bgapiRXBuffer[13] << 8) + (bgapiRXBuffer[14] << 16) + (bgapiRXBuffer[15] << 24)),
                                        (Byte[])(bgapiRXBuffer.Skip(17).Take(bgapiRXBuffer[16]).ToArray())
                                    ));
                                }
                            }
                        }
                        else if (bgapiRXBuffer[2] == 255)
                        {
                            if (bgapiRXBuffer[3] == 0)
                            {
                                if (BLEEventUserMessageToHost != null)
                                {
                                    BLEEventUserMessageToHost(this, new BlueGecko.BLE.Events.User.MessageToHostEventArgs(
                                        (Byte[])(bgapiRXBuffer.Skip(5).Take(bgapiRXBuffer[4]).ToArray())
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

        public UInt16 SendCommand(System.IO.Ports.SerialPort port, Byte[] cmd)
        {
            SetBusy(true);
            port.Write(cmd, 0, cmd.Length);
            return 0; // no error handling yet
        }

    }

}
