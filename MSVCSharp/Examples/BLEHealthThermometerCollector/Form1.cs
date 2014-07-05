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
    public partial class Form1 : Form
    {
        public Bluegiga.BGLib bglib = new Bluegiga.BGLib();
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
        
        public UInt16 app_state = STATE_STANDBY;        // current application state
        public Byte connection_handle = 0;              // connection handle (will always be 0 if only one connection happens at a time)
        public UInt16 att_handlesearch_start = 0;       // "start" handle holder during search
        public UInt16 att_handlesearch_end = 0;         // "end" handle holder during search
        public UInt16 att_handle_measurement = 0;       // temperature measurement attribute handle
        public UInt16 att_handle_measurement_ccc = 0;   // temperature measurement client characteristic configuration handle (to enable indications)

        // for master/scanner devices, the "gap_scan_response" event is a common entry-like point
        // this filters ad packets to find devices which advertise the Health Thermometer service
        public void GAPScanResponseEvent(object sender, Bluegiga.BLE.Events.GAP.ScanResponseEventArgs e)
        {
            String log = String.Format("ble_evt_gap_scan_response: rssi={0}, packet_type={1}, sender=[ {2}], address_type={3}, bond={4}, data=[ {5}]" + Environment.NewLine,
                (SByte)e.rssi,
                e.packet_type,
                ByteArrayToHexString(e.sender),
                e.address_type,
                e.bond,
                ByteArrayToHexString(e.data)
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });

            // pull all advertised service info from ad packet
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
            if (ad_services.Any(a => a.SequenceEqual(new Byte[] { 0x18, 0x09 }))) {
                // connect to this device
                Byte[] cmd = bglib.BLECommandGAPConnectDirect(e.sender, e.address_type, 0x20, 0x30, 0x100, 0); // 125ms interval, 125ms window, active scanning
                // DEBUG: display bytes written
                ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine); });
                bglib.SendCommand(serialAPI, cmd);
                //while (bglib.IsBusy()) ;

                // update state
                app_state = STATE_CONNECTING;
            }
        }

        // the "connection_status" event occurs when a new connection is established
        public void ConnectionStatusEvent(object sender, Bluegiga.BLE.Events.Connection.StatusEventArgs e)
        {
            String log = String.Format("ble_evt_connection_status: connection={0}, flags={1}, address=[ {2}], address_type={3}, conn_interval={4}, timeout={5}, latency={6}, bonding={7}" + Environment.NewLine,
                e.connection,
                e.flags,
                ByteArrayToHexString(e.address),
                e.address_type,
                e.conn_interval,
                e.timeout,
                e.latency,
                e.bonding
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });

            if ((e.flags & 0x05) == 0x05)
            {
                // connected, now perform service discovery
                connection_handle = e.connection;
                ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("Connected to {0}", ByteArrayToHexString(e.address)) + Environment.NewLine); });
                Byte[] cmd = bglib.BLECommandATTClientReadByGroupType(e.connection, 0x0001, 0xFFFF, new Byte[] { 0x00, 0x28 }); // "service" UUID is 0x2800 (little-endian for UUID uint8array)
                // DEBUG: display bytes written
                ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine); });
                bglib.SendCommand(serialAPI, cmd);
                //while (bglib.IsBusy()) ;
                
                // update state
                app_state = STATE_FINDING_SERVICES;
            }
        }

        public void ATTClientGroupFoundEvent(object sender, Bluegiga.BLE.Events.ATTClient.GroupFoundEventArgs e)
        {
            String log = String.Format("ble_evt_attclient_group_found: connection={0}, start={1}, end={2}, uuid=[ {3}]" + Environment.NewLine,
                e.connection,
                e.start,
                e.end,
                ByteArrayToHexString(e.uuid)
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });

            // found "service" attribute groups (UUID=0x2800), check for thermometer service
            if (e.uuid.SequenceEqual(new Byte[] { 0x09, 0x18 }))
            {
                ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("Found attribute group for service w/UUID=0x1809: start={0}, end=%d", e.start, e.end) + Environment.NewLine); });
                att_handlesearch_start = e.start;
                att_handlesearch_end = e.end;
            }
        }

        public void ATTClientFindInformationFoundEvent(object sender, Bluegiga.BLE.Events.ATTClient.FindInformationFoundEventArgs e)
        {
            String log = String.Format("ble_evt_attclient_find_information_found: connection={0}, chrhandle={1}, uuid=[ {2}]" + Environment.NewLine,
                e.connection,
                e.chrhandle,
                ByteArrayToHexString(e.uuid)
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });

            // check for thermometer measurement characteristic
            if (e.uuid.SequenceEqual(new Byte[] { 0x1C, 0x2A }))
            {
                ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("Found attribute w/UUID=0x2A1C: handle={0}", e.chrhandle) + Environment.NewLine); });
                att_handle_measurement = e.chrhandle;
            }
            // check for subsequent client characteristic configuration
            else if (e.uuid.SequenceEqual(new Byte[]  { 0x02, 0x29 }) && att_handle_measurement > 0)
            {
                ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("Found attribute w/UUID=0x2902: handle={0}", e.chrhandle) + Environment.NewLine); });
                att_handle_measurement_ccc = e.chrhandle;
            }
        }

        public void ATTClientProcedureCompletedEvent(object sender, Bluegiga.BLE.Events.ATTClient.ProcedureCompletedEventArgs e)
        {
            String log = String.Format("ble_evt_attclient_procedure_completed: connection={0}, result={1}, chrhandle={2}" + Environment.NewLine,
                e.connection,
                e.result,
                e.chrhandle
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });

            // check if we just finished searching for services
            if (app_state == STATE_FINDING_SERVICES)
            {
                if (att_handlesearch_end > 0)
                {
                    //print "Found 'Health Thermometer' service with UUID 0x1809"

                    // found the Health Thermometer service, so now search for the attributes inside
                    Byte[] cmd = bglib.BLECommandATTClientFindInformation(e.connection, att_handlesearch_start, att_handlesearch_end);
                    // DEBUG: display bytes written
                    ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine); });
                    bglib.SendCommand(serialAPI, cmd);
                    //while (bglib.IsBusy()) ;

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
                if (att_handle_measurement_ccc > 0)
                {
                    //print "Found 'Health Thermometer' measurement attribute with UUID 0x2A1C"

                    // found the measurement + client characteristic configuration, so enable indications
                    // (this is done by writing 0x0002 to the client characteristic configuration attribute)
                    Byte[] cmd = bglib.BLECommandATTClientAttributeWrite(e.connection, att_handle_measurement_ccc, new Byte[] { 0x02, 0x00 });
                    // DEBUG: display bytes written
                    ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine); });
                    bglib.SendCommand(serialAPI, cmd);
                    //while (bglib.IsBusy()) ;

                    // update state
                    app_state = STATE_LISTENING_MEASUREMENTS;
                }
                else
                {
                    ThreadSafeDelegate(delegate { txtLog.AppendText("Could not find 'Health Thermometer' measurement attribute with UUID 0x2A1C" + Environment.NewLine); });
                }
            }
        }

        public void ATTClientAttributeValueEvent(object sender, Bluegiga.BLE.Events.ATTClient.AttributeValueEventArgs e)
        {
            String log = String.Format("ble_evt_attclient_attribute_value: connection={0}, atthandle={1}, type={2}, value=[ {3}]" + Environment.NewLine,
                e.connection,
                e.atthandle,
                e.type,
                ByteArrayToHexString(e.value)
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });

            // check for a new value from the connected peripheral's temperature measurement attribute
            if (e.connection == connection_handle && e.atthandle == att_handle_measurement)
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

            // DEBUG: display bytes read
            ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("<= RX ({0}) [ {1}]", inData.Length, ByteArrayToHexString(inData)) + Environment.NewLine); });

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

            // initialize serial port with all of the normal values (should work with BLED112 on USB)
            serialAPI.Handshake = System.IO.Ports.Handshake.RequestToSend;
            serialAPI.BaudRate = 115200;
            serialAPI.DataBits = 8;
            serialAPI.StopBits = System.IO.Ports.StopBits.One;
            serialAPI.Parity = System.IO.Ports.Parity.None;
            serialAPI.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(DataReceivedHandler);

            // initialize BGLib events we'll need for this script
            bglib.BLEEventGAPScanResponse += new Bluegiga.BLE.Events.GAP.ScanResponseEventHandler(this.GAPScanResponseEvent);
            bglib.BLEEventConnectionStatus += new Bluegiga.BLE.Events.Connection.StatusEventHandler(this.ConnectionStatusEvent);
            bglib.BLEEventATTClientGroupFound += new Bluegiga.BLE.Events.ATTClient.GroupFoundEventHandler(this.ATTClientGroupFoundEvent);
            bglib.BLEEventATTClientFindInformationFound += new Bluegiga.BLE.Events.ATTClient.FindInformationFoundEventHandler(this.ATTClientFindInformationFoundEvent);
            bglib.BLEEventATTClientProcedureCompleted += new Bluegiga.BLE.Events.ATTClient.ProcedureCompletedEventHandler(this.ATTClientProcedureCompletedEvent);
            bglib.BLEEventATTClientAttributeValue += new Bluegiga.BLE.Events.ATTClient.AttributeValueEventHandler(this.ATTClientAttributeValueEvent);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // get a list of all available ports on the system
            portDict.Clear();
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SerialPort");
                //string[] ports = System.IO.Ports.SerialPort.GetPortNames();
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    portDict.Add(String.Format("{0}", queryObj["DeviceID"]), String.Format("{0} - {1}", queryObj["DeviceID"], queryObj["Caption"]));
                }
            }
            catch (ManagementException ex)
            {
                portDict.Add("0", "Error " + ex.Message);
            }
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

            // set scan parameters
            cmd = bglib.BLECommandGAPSetScanParameters(0xC8, 0xC8, 1); // 125ms interval, 125ms window, active scanning
            // DEBUG: display bytes read
            ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine); });
            bglib.SendCommand(serialAPI, cmd);
            //while (bglib.IsBusy()) ;

            // begin scanning for BLE peripherals
            cmd = bglib.BLECommandGAPDiscover(1); // generic discovery mode
            // DEBUG: display bytes read
            ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine); });
            bglib.SendCommand(serialAPI, cmd);
            //while (bglib.IsBusy()) ;

            // update state
            app_state = STATE_SCANNING;

            // disable "GO" button since we already started, and sending the same commands again sill not work right
            btnGo.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // stop everything we're doing, if possible
            Byte[] cmd;

            // disconnect if connected
            cmd = bglib.BLECommandConnectionDisconnect(0);
            // DEBUG: display bytes read
            ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine); });
            bglib.SendCommand(serialAPI, cmd);
            //while (bglib.IsBusy()) ;

            // stop scanning if scanning
            cmd = bglib.BLECommandGAPEndProcedure();
            // DEBUG: display bytes read
            ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine); });
            bglib.SendCommand(serialAPI, cmd);
            //while (bglib.IsBusy()) ;

            // stop advertising if advertising
            cmd = bglib.BLECommandGAPSetMode(0, 0);
            // DEBUG: display bytes read
            ThreadSafeDelegate(delegate { txtLog.AppendText(String.Format("=> TX ({0}) [ {1}]", cmd.Length, ByteArrayToHexString(cmd)) + Environment.NewLine); });
            bglib.SendCommand(serialAPI, cmd);
            //while (bglib.IsBusy()) ;

            // enable "GO" button to allow them to start again
            btnGo.Enabled = true;

            // update state
            app_state = STATE_STANDBY;
        }

    }
}
