using GSMModem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GsmTest
{
    public partial class TestFormGsmModemModule : Form
    {

        Connection conn;
        GSMModem.SMS sms;

        public TestFormGsmModemModule()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            try
            {

                //SMS sms = new SMS();

                if (rdPDU.Checked)
                    sms.SendPDU(txtbxMobile.Text, txtbxMessage.Text);
                else
                    sms.SendText(txtbxMobile.Text, txtbxMessage.Text);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.Source);
            }
        }

        private void smsSendEvent(int sendId)
        {
            txtResult.Text += "sendId: " + sendId + "\r\n";
        }

        private void dataReceiver(string receivedData)
        {
            if (txtResult.InvokeRequired)
            {
                // Call this same method 
                Action safeWrite = delegate { dataReceiver("receivedData: " + receivedData + "\r\n"); };
                txtResult.Invoke(safeWrite);
            }
            else
                txtResult.Text += "receivedData: " + receivedData + "\r\n";        
        }

        private void btnPort_Click(object sender, EventArgs e)
        {
            getAllComPort(cmbPort);
        }

        private string getPort()
        {
            string portName = (string)cmbPort.SelectedItem;
            int v = portName.IndexOf("COM", 0, portName.Length);
            portName = portName.Substring(v, 5);
            string port = portName;

            //MessageBox.Show("2." + port);

            //Removing unwanted Leading and Trailing Character's.
            char[] port1 = new char[1];
            port1[0] = ')';

            if (port.Contains(")"))
            {
                port = port.TrimEnd(port1);
            }

            if (port.Length >= 5)
            {
                port = port.Trim();
            }

            return port;
        }       


        private static void getAllComPort(ComboBox cmb)
        {

            // getting a list of all available com port devices and their friendly names     
            // must add System.Management DLL resource to solution before using this     
            // Project -> Add Reference -> .Net tab, choose System.Management 

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnpEntity ");
            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_POTSModem ");
            try
            {
                foreach (ManagementObject queryObj in searcher.Get())
                {

                    string name = queryObj.GetPropertyValue("Name").ToStringExt();
                    if (name.Contains("(COM"))
                    {
                        cmb.Items.Add(name);

                    }
                }

                foreach (ManagementObject queryObj1 in searcher1.Get())
                {
                    if ((string)queryObj1["Status"] == "OK")
                    {
                        cmb.Items.Add(queryObj1["AttachedTo"] + " - " + System.Convert.ToString(queryObj1["Description"]));

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            conn = new GSMModem.Connection(getPort());
            sms = new GSMModem.SMS(conn);

            conn.DataReceived += dataReceiver;
            sms.SendResponseReceived += smsSendEvent;
        }

        private void btnSendCommand_Click(object sender, EventArgs e)
        {
            conn.SendCommand(txtCommand.Text);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            conn.Close();                
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            txtResult.Text += SMS.ConvertStringToHEX(txtbxMessage.Text);
        }

        private void btnForm1_Click(object sender, EventArgs e)
        {
            TestForm form1 = new TestForm();
            form1.ShowDialog();
        }
    }
}
