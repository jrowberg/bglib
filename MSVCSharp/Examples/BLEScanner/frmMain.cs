// Bluegiga BGLib C# interface library
// 2013-01-15 by Jeff Rowberg <jeff@rowberg.net>
// Updates should (hopefully) always be available at https://github.com/jrowberg/bglib

/* ============================================
BGLib C# interface library code is placed under the MIT license
Copyright (c) 2013 Jeff Rowberg

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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BLEScanner
{
    public partial class frmMain : Form
    {

        public Bluegiga.BGLib bglib = new Bluegiga.BGLib();

        public void SystemBootEvent(object sender, Bluegiga.BLE.SystemBootEventArgs e)
        {
            String log = String.Format("ble_evt_system_boot:" + Environment.NewLine + "\tmajor={0}, minor={1}, patch={2}, build={3}, ll_version={4}, protocol_version={5}, hw={6}" + Environment.NewLine,
                e.major,
                e.minor,
                e.patch,
                e.build,
                e.ll_version,
                e.protocol_version,
                e.hw
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
        }

        public void GAPScanResponseEvent(object sender, Bluegiga.BLE.GAPScanResponseEventArgs e)
        {
            String log = String.Format("ble_evt_gap_scan_response:" + Environment.NewLine + "\trssi={0}, packet_type={1}, bd_addr=[ {2}], address_type={3}, bond={4}, data=[ {5}]" + Environment.NewLine,
                (SByte)e.rssi,
                (SByte)e.packet_type,
                ByteArrayToHexString(e.bd_addr),
                (SByte)e.address_type,
                (SByte)e.bond,
                ByteArrayToHexString(e.data)
                );
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
        }

        // Thread-safe operations from event handlers
        // I love StackOverflow: http://stackoverflow.com/q/782274
        public void ThreadSafeDelegate(MethodInvoker method)
        {
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }
        
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            serialAPI.Handshake = System.IO.Ports.Handshake.RequestToSend;
            serialAPI.BaudRate = 115200;
            serialAPI.PortName = "COM13";
            serialAPI.DataBits = 8;
            serialAPI.StopBits = System.IO.Ports.StopBits.One;
            serialAPI.Parity = System.IO.Ports.Parity.None;
            serialAPI.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(DataReceivedHandler);
            serialAPI.Open();
            Console.WriteLine("Port open");

            bglib.SystemBootEvent += new Bluegiga.BLE.SystemBootEventHandler(this.SystemBootEvent);
            bglib.GAPScanResponseEvent += new Bluegiga.BLE.GAPScanResponseEventHandler(this.GAPScanResponseEvent);

            // send system_hello()
            //serialAPI.Write(new Byte[] { 0, 0, 0, 1 }, 0, 4);
        }

        private void DataReceivedHandler(
                                object sender,
                                System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort sp = (System.IO.Ports.SerialPort)sender;
            Byte[] inData = new Byte[sp.BytesToRead];

            // read all available bytes from serial port in one chunk
            sp.Read(inData, 0, sp.BytesToRead);
            
            // DEBUG: display bytes read
            Console.WriteLine("<= RX ({0}) [ {1}]", inData.Length, ByteArrayToHexString(inData));
            
            // parse all bytes read through BGLib parser
            for (int i = 0; i < inData.Length; i++)
            {
                bglib.parse(inData[i]);
            }
        }

        private void btnStartScan_Click(object sender, EventArgs e)
        {
            // send gap_discover(mode: 1)
            serialAPI.Write(new Byte[] { 0, 1, 6, 2, 1 }, 0, 5);
        }

        private void btnStopScan_Click(object sender, EventArgs e)
        {
            // send gap_end_procedure()
            serialAPI.Write(new Byte[] { 0, 0, 6, 4 }, 0, 4);
        }

        public string ByteArrayToHexString(Byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2} ", b);
            return hex.ToString();
        }
    }
}
