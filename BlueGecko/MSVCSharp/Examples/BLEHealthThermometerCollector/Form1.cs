//#define SERIAL_DEBUG

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Management;
using System.Windows.Forms;

namespace BLEHealthThermometerCollector
{
    using System.Management;
    using System.Threading;
    public partial class Form1 : Form
    {
        public BlueGecko.BGLib bglib = new BlueGecko.BGLib();
        public Boolean isAttached = false;
        public Dictionary<string, string> portDict = new Dictionary<string, string>();

        /* ================================================================ */
        /*                BEGIN MAIN EVENT-DRIVEN APP LOGIC                 */
        /* ================================================================ */

        public const UInt16 STATE_STANDBY = 0;
        public const UInt16 STATE_SCANNING = 1;
        public const UInt16 STATE_CONNECTING = 2;
        public const UInt16 STATE_FINDING_SERVICES = 3;
        public const UInt16 STATE_FINDING_ATTRIBUTES = 4;
        public const UInt16 STATE_LISTENING_MEASUREMENTS = 5;
        public const UInt16 STATE_SCAN_END_REQUEST = 6;


        public UInt16 app_state = STATE_STANDBY;        // current application state
        public Byte connection_handle = 0;              // connection handle
        public UInt32 thermometer_service_handle = 0;
        public UInt16 thermometer_characteristic_handle = 0;
        public byte[] target_device_address;
        public Byte target_device_address_type;

        // for master/scanner devices, the "gap_scan_response" event is a common entry-like point
        // this filters adv packets to find devices which advertise the Health Thermometer service
        public void GAPScanResponseEvent(object sender, BlueGecko.BLE.Events.LEGAP.ScanResponseEventArgs e)
        {
            Byte[] cmd;

            String log = String.Format("ble_evt_gap_scan_response: rssi={0}, packet_type={1}, address=[ {2}], address_type={3}, bonding={4}, data=[ {5}]" + Environment.NewLine,
                (SByte)e.rssi,
                e.packet_type,
                ByteArrayToHexString(e.address),
                e.address_type,
                e.bonding,
                ByteArrayToHexString(e.data)
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });

            // pull all advertised service info from adv packet
            List<Byte[]> ad_services = new List<Byte[]>();
            Byte[] this_field = {};
            int bytes_left = 0;
            int field_offset = 0;
            for (int i = 0; i < e.data.Length; i++)
            {
                if (bytes_left == 0)
                {
                    bytes_left = e.data[i];
                    this_field = new Byte[e.data[i]];
                    field_offset = i + 1;
                }
                else
                {
                    this_field[i - field_offset] = e.data[i];
                    bytes_left--;
                    if (bytes_left == 0)
                    {
                        if (this_field[0] == 0x02 || this_field[0] == 0x03)
                        {
                            // partial or complete list of 16-bit UUIDs
                            ad_services.Add(this_field.Skip(1).Take(2).Reverse().ToArray());
                        }
                        else if (this_field[0] == 0x04 || this_field[0] == 0x05)
                        {
                            // partial or complete list of 32-bit UUIDs
                            ad_services.Add(this_field.Skip(1).Take(4).Reverse().ToArray());
                        }
                        else if (this_field[0] == 0x06 || this_field[0] == 0x07)
                        {
                            // partial or complete list of 128-bit UUIDs
                            ad_services.Add(this_field.Skip(1).Take(16).Reverse().ToArray());
                        }
                    }
                }
            }

            // check for 0x1809 (official health thermometer service UUID)
            if (ad_services.Any(a => a.SequenceEqual(new Byte[] { 0x18, 0x09 })))
            {

                Console.Write("Found service! Attempting to connect." + Environment.NewLine);

                // Stop scanning
                cmd = bglib.BLECommandLEGAPEndProcedure();
                bglib.SendCommand(serialAPI, cmd);

#if SERIAL_DEBUG
                // DEBUG: display bytes written
                log = String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine;
                Console.Write(log);
                ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
#endif

                /* We are going to connect in the response handler for le_gap_end_procedure because
                 * le_gap_procedure will also cancel the connect request if called too early! */
                target_device_address = e.address;
                target_device_address_type = e.address_type;
                app_state = STATE_SCAN_END_REQUEST;
            }
            
        }

        // the "connection_opened" event occurs when a new connection is established
        public void ConnectionOpenedEvent(object sender, BlueGecko.BLE.Events.LEConnection.OpenedEventArgs e)
        {
            String log = String.Format("ble_evt_connection_opened: connection={0}, address=[ {1}], address_type={2}, bonding={3}" + Environment.NewLine,
                e.connection,
                ByteArrayToHexString(e.address),
                e.address_type,
                e.bonding
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
       
            // connected, now perform service discovery
            connection_handle = e.connection;
            ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("Connected to {0}", ByteArrayToHexString(e.address)) + Environment.NewLine); });
            Byte[] cmd = bglib.BLECommandGATTDiscoverPrimaryServicesByUUID(e.connection, new Byte[] { 0x09, 0x18 }); // health thermometer service UUID defined by Bluetooth SIG
            bglib.SendCommand(serialAPI, cmd);

#if SERIAL_DEBUG
            // DEBUG: display bytes written
            log = String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine;
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
#endif

            // update state
            app_state = STATE_FINDING_SERVICES;
           
        }

        public void ConnectionClosedEvent(object sender, BlueGecko.BLE.Events.LEConnection.ClosedEventArgs e)
        {
            String log = String.Format("ble_evt_connection_closed: connection={0}, reason=[ {1}]" + Environment.NewLine,
                e.connection,
                e.reason
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });

            // enable "GO" button
            btnGo.BeginInvoke((Action)delegate ()
            {
                btnGo.Enabled = true;
            });
         

            // update app_state
            app_state = STATE_STANDBY;

        }

        public void GATTServiceFoundEvent(object sender, BlueGecko.BLE.Events.GATT.ServiceEventArgs e)
        {
            String log = String.Format("ble_evt_gatt_service: connection={0}, service handle={1}, uuid=[ {2}]" + Environment.NewLine,
                e.connection,
                e.service,
                ByteArrayToHexString(e.uuid)
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
            thermometer_service_handle = e.service;
        }

        public void GATTCharacteristicFoundEvent(object sender, BlueGecko.BLE.Events.GATT.CharacteristicEventArgs e)
        {
            String log = String.Format("ble_evt_gatt_characteristic: connection={0}, characteristic_handle={1}, uuid=[ {2}]" + Environment.NewLine,
                e.connection,
                e.characteristic,
                ByteArrayToHexString(e.uuid)
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
            thermometer_characteristic_handle = e.characteristic;
        }

        public void GATTProcedureCompletedEvent(object sender, BlueGecko.BLE.Events.GATT.ProcedureCompletedEventArgs e)
        {
            String log = String.Format("ble_evt_gatt_procedure_completed: connection={0}, result={1}" + Environment.NewLine,
                e.connection,
                e.result
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });

            // check if we just finished searching for services
            if (app_state == STATE_FINDING_SERVICES)
            {
                if (thermometer_service_handle > 0)
                {
                    //print "Found 'Health Thermometer' service with UUID 0x1809"
                    Console.Write("Found the service handle - now discovering characteristics." + Environment.NewLine);

                    // found the Health Thermometer service, so now search for the attributes inside
                    Byte[] cmd = bglib.BLECommandGATTDiscoverCharacteristicsByUUID(e.connection, thermometer_service_handle, new Byte[] { 0x1c, 0x2a }); //temp measurement characteristic UUID defined by Bluetooth SIG
                    bglib.SendCommand(serialAPI, cmd);

#if SERIAL_DEBUG
                    // DEBUG: display bytes written
                    log = String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine;
                    Console.Write(log);
                    ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
#endif

                    // update state
                    app_state = STATE_FINDING_ATTRIBUTES;
                }
                else
                {
                    ThreadSafeDelegate(delegate { txtLog.AppendText("Could not find 'Health Thermometer' service with UUID 0x1809" + Environment.NewLine); });
                }
            }
            // check if we just finished searching for attributes within the thermometer service
            else if (app_state == STATE_FINDING_ATTRIBUTES)
            {
                if (thermometer_characteristic_handle > 0)
                {
                    //print "Found 'Health Thermometer' measurement attribute with UUID 0x2A1C"
                    Console.Write("Found attribute, enabling indications" + Environment.NewLine);
                    // found the measurement + client characteristic configuration, so enable indications
                    // (this is done by writing 0x0002 to the client characteristic configuration attribute)
                    Byte[] cmd = bglib.BLECommandGATTSetCharacteristicNotification(e.connection, thermometer_characteristic_handle,0x02); //0x02 is indication
                    bglib.SendCommand(serialAPI, cmd);
#if SERIAL_DEBUG
                    // DEBUG: display bytes written
                    log = String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine;
                    Console.Write(log);
                    ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
#endif

                    // update state
                    app_state = STATE_LISTENING_MEASUREMENTS;
                }
                else
                {
                    ThreadSafeDelegate(delegate { txtLog.AppendText("Could not find 'Health Thermometer' measurement attribute with UUID 0x2A1C" + Environment.NewLine); });
                }
            }
        }

        public void GATTCharacteristicValueEvent(object sender, BlueGecko.BLE.Events.GATT.CharacteristicValueEventArgs e)
        {
            String log = String.Format("ble_evt_gatt_characteristic_value: connection={0}, att_opcode={1}, offset={2}, value=[ {3}]" + Environment.NewLine,
                e.connection,
                e.characteristic,
                e.att_opcode,
                e.offset,
                ByteArrayToHexString(e.value)
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });

            // check for a new value from the connected peripheral's temperature measurement attribute
            if (e.connection == connection_handle && e.characteristic == thermometer_characteristic_handle)
            {
                Byte htm_flags = e.value[0];
                int htm_exponent = e.value[4];
                int htm_mantissa = (e.value[3] << 16) | (e.value[2] << 8) | e.value[1];
                if (htm_exponent > 127)
                {
                    // # convert to signed 8-bit int
                    htm_exponent = htm_exponent - 256;
                }
                float htm_measurement = htm_mantissa * (float)Math.Pow(10, htm_exponent);
                char temp_type = 'C';
                if ((htm_flags & 0x01) == 0x01)
                {
                    // value sent is Fahrenheit, not Celsius
                    temp_type = 'F';
                }

                // display actual measurement
                ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("Temperature: {0}\u00B0 {1}", htm_measurement, temp_type) + Environment.NewLine); });

                // Send confirmation for the indication
                Byte[] cmd = bglib.BLECommandGATTSendCharacteristicConfirmation(e.connection); 
                bglib.SendCommand(serialAPI, cmd);

#if SERIAL_DEBUG
                // DEBUG: display bytes written
                log = String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine;
                Console.Write(log);
                ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
#endif
            }
        }

        public void SystemBootEvent(object sender, BlueGecko.BLE.Events.System.BootEventArgs e)
        {
            /* Print boot information to the console */
            String log = String.Format("ble_evt_system_boot: version {0}.{1}.{2}.{3}" + Environment.NewLine,
                e.major,
                e.minor,
                e.patch,
                e.build
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
        }

        public void LEGAPEndProcedureEvent(object sender, BlueGecko.BLE.Responses.LEGAP.EndProcedureEventArgs e)
        {
            /* We need to wait on the le_gap_end_procedure event before we send le_gap_connect, or else the 
             * connection procedure will be ended too! */
#if SERIAL_DEBUG
            String log = String.Format("rsp_le_gap_end_procedure: result {0}" + Environment.NewLine,
               e.result
               );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
#endif
            if (app_state == STATE_SCAN_END_REQUEST)
            {
                // scan successfully stopped! connect to the target device
                Byte[] cmd = bglib.BLECommandLEGAPConnect(target_device_address, target_device_address_type, 0x01); // 0x01 = 1Mbit PHY
                bglib.SendCommand(serialAPI, cmd);

#if SERIAL_DEBUG
                // DEBUG: display bytes written
                log = String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine;
                Console.Write(log);
                ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
#endif

                // update state
                app_state = STATE_CONNECTING;
            }
    }

        /* ================================================================ */
        /*                 END MAIN EVENT-DRIVEN APP LOGIC                  */
        /* ================================================================ */



        // Thread-safe operations from event handlers
        // I love StackOverflow: http://stackoverflow.com/q/782274
        public void ThreadSafeDelegate(MethodInvoker method)
        {
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        // Convert byte array to "00 11 22 33 44 55 " string
        public string ByteArrayToHexString(Byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2} ", b);
            return hex.ToString();
        }

        // Serial port event handler for a nice event-driven architecture
        private void DataReceivedHandler(
                                object sender,
                                System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort sp = (System.IO.Ports.SerialPort)sender;
            Byte[] inData = new Byte[sp.BytesToRead];

            // read all available bytes from serial port in one chunk
            sp.Read(inData, 0, sp.BytesToRead);
#if SERIAL_DEBUG
            // DEBUG: display bytes read
            String log = String.Format("<= RX ({0}) [ {1}]", inData.Length, ByteArrayToHexString(inData)) + Environment.NewLine;
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
#endif
            // parse all bytes read through BGLib parser
            for (int i = 0; i < inData.Length; i++)
            {
                bglib.Parse(inData[i]);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // initialize list of ports
            btnRefresh_Click(sender, e);

            // initialize COM port combobox with list of ports
            comboPorts.DataSource = new BindingSource(portDict, null);
            comboPorts.DisplayMember = "Value";
            comboPorts.ValueMember = "Key";

            // initialize serial port with all of the normal values (works with WSTK NCP or Darwin Tech Dongle)
            serialAPI.Handshake = System.IO.Ports.Handshake.RequestToSend;
            serialAPI.BaudRate = 115200;
            serialAPI.DataBits = 8;
            serialAPI.StopBits = System.IO.Ports.StopBits.One;
            serialAPI.Parity = System.IO.Ports.Parity.None;
            serialAPI.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(DataReceivedHandler);

            // initialize BGLib events we'll need for this program
            bglib.BLEEventLEGAPScanResponse += new BlueGecko.BLE.Events.LEGAP.ScanResponseEventHandler(this.GAPScanResponseEvent);
            bglib.BLEEventLEConnectionOpened += new BlueGecko.BLE.Events.LEConnection.OpenedEventHandler(this.ConnectionOpenedEvent);
            bglib.BLEEventLEConnectionClosed += new BlueGecko.BLE.Events.LEConnection.ClosedEventHandler(this.ConnectionClosedEvent);
            bglib.BLEEventGATTService += new BlueGecko.BLE.Events.GATT.ServiceEventHandler(this.GATTServiceFoundEvent);
            bglib.BLEEventGATTCharacteristic += new BlueGecko.BLE.Events.GATT.CharacteristicEventHandler(this.GATTCharacteristicFoundEvent);
            bglib.BLEEventGATTProcedureCompleted += new BlueGecko.BLE.Events.GATT.ProcedureCompletedEventHandler(this.GATTProcedureCompletedEvent);
            bglib.BLEEventGATTCharacteristicValue += new BlueGecko.BLE.Events.GATT.CharacteristicValueEventHandler(this.GATTCharacteristicValueEvent);
            bglib.BLEEventSystemBoot += new BlueGecko.BLE.Events.System.BootEventHandler(this.SystemBootEvent);
            bglib.BLEResponseLEGAPEndProcedure += new BlueGecko.BLE.Responses.LEGAP.EndProcedureEventHandler(this.LEGAPEndProcedureEvent);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // get a list of all available ports on the system
            portDict.Clear();
            try
            {
                //ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SerialPort");
                //string[] ports = System.IO.Ports.SerialPort.GetPortNames();
                foreach (COMPortInfo comPort in COMPortInfo.GetCOMPortsInfo())
                {
                    portDict.Add(String.Format("{0}", comPort.Name), String.Format("{0} - {1}", comPort.Name,comPort.Description));
                }
            }
            catch (ManagementException ex)
            {
                portDict.Add("0", "Error " + ex.Message);
            }

            /* Refresh combo with new data */
            comboPorts.DataSource = new BindingSource(portDict, null);
      
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            if (!isAttached)
            {
                txtLog.AppendText("Opening serial port '" + comboPorts.SelectedValue.ToString() + "'..." + Environment.NewLine);
                serialAPI.PortName = comboPorts.SelectedValue.ToString();
                serialAPI.Open();
                txtLog.AppendText("Port opened" + Environment.NewLine);
                isAttached = true;
                btnAttach.Text = "Detach";
                btnGo.Enabled = true;
                btnReset.Enabled = true;

                // Reset the NCP
                Byte[] cmd;

                String log = "Sending NCP reset command" + Environment.NewLine;
                Console.Write(log);
                ThreadSafeDelegate(delegate { txtLog.AppendText(log); });


                cmd = bglib.BLECommandSystemReset(0);
                bglib.SendCommand(serialAPI, cmd);

#if SERIAL_DEBUG
            // DEBUG: display bytes written
            log = String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine;
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
#endif

            }
            else
            {
                txtLog.AppendText("Closing serial port..." + Environment.NewLine);
                serialAPI.Close();
                txtLog.AppendText("Port closed" + Environment.NewLine);
                isAttached = false;
                btnAttach.Text = "Attach";
                btnGo.Enabled = false;
                btnReset.Enabled = false;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            // start the scan/connect process now
            Byte[] cmd;

            // set discovery timing parameters
            cmd = bglib.BLECommandLEGAPSetDiscoveryTiming(1, 0xC8, 0xC8); // 1Mbit PHY, 125ms interval, 125ms window
            bglib.SendCommand(serialAPI, cmd);

            Thread.Sleep(5); // sleep for 5ms to allow space in between commands

#if SERIAL_DEBUG
            // DEBUG: display bytes written
            String log = String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine;
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
#endif

            // set discovery type parameters
            cmd = bglib.BLECommandLEGAPSetDiscoveryType(1,1); // 1Mbit PHY, active scanning
            bglib.SendCommand(serialAPI, cmd);

            Thread.Sleep(5); // sleep for 5ms to allow space in between commands

#if SERIAL_DEBUG
            // DEBUG: display bytes written
            log = String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine;
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
#endif

            // begin scanning for BLE peripherals
            cmd = bglib.BLECommandLEGAPStartDiscovery(1,1); // 1Mbit PHY, generic discovery mode
            bglib.SendCommand(serialAPI, cmd);

#if SERIAL_DEBUG
            // DEBUG: display bytes written
            log = String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine;
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
#endif

            // update state
            app_state = STATE_SCANNING;

            // disable "GO" button since we already started, and sending the same commands again sill not work right
            btnGo.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset the NCP
            Byte[] cmd;

            String log = "Sending NCP reset command" + Environment.NewLine;
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });


            cmd = bglib.BLECommandSystemReset(0);
            bglib.SendCommand(serialAPI, cmd);

#if SERIAL_DEBUG
            // DEBUG: display bytes written
            log = String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine;
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
#endif

            // enable "GO" button to allow them to start again
            btnGo.Enabled = true;

            // update state
            app_state = STATE_STANDBY;
        }

    }

    /* Serial port code from https://dariosantarelli.wordpress.com/2010/10/18/c-how-to-programmatically-find-a-com-port-by-friendly-name/ */
internal class ProcessConnection
    {
        public static ConnectionOptions ProcessConnectionOptions()

        {
            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.Authentication = AuthenticationLevel.Default;
            options.EnablePrivileges = true;
            return options;
        }

        public static ManagementScope ConnectionScope(string machineName, ConnectionOptions options, string path)

        {
            ManagementScope connectScope = new ManagementScope();
            connectScope.Path = new ManagementPath(@"\\" + machineName + path);
            connectScope.Options = options;
            connectScope.Connect();
            return connectScope;
        }
    }



    public class COMPortInfo

    {
        public string Name { get; set; }
        public string Description { get; set; }

        public COMPortInfo() { }

        public static List<COMPortInfo> GetCOMPortsInfo()
        { 

            List<COMPortInfo> comPortInfoList = new List<COMPortInfo>();

            ConnectionOptions options = ProcessConnection.ProcessConnectionOptions();
            ManagementScope connectionScope = ProcessConnection.ConnectionScope(Environment.MachineName, options, @"\root\CIMV2");

            ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");
            ManagementObjectSearcher comPortSearcher = new ManagementObjectSearcher(connectionScope, objectQuery);

            using (comPortSearcher)
            {
                string caption = null;
                foreach (ManagementObject obj in comPortSearcher.Get())
                {
                    if (obj != null)
                    {

                        object captionObj = obj["Caption"];

                        if (captionObj != null)
                        {
                            caption = captionObj.ToString();
                            if (caption.Contains("(COM"))
                            {
                                COMPortInfo comPortInfo = new COMPortInfo();
                                comPortInfo.Name = caption.Substring(caption.LastIndexOf("(COM")).Replace("(", string.Empty).Replace(")",
                                                                     string.Empty);

                                comPortInfo.Description = caption;
                                comPortInfoList.Add(comPortInfo);
                            }
                        }
                    }
                }
            }
            return comPortInfoList;
        }
    }
}
