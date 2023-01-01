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
using static GSMModem.Connection;

namespace GsmTest
{
    public partial class TestForm : Form
    {

        public TestForm()
        {
            InitializeComponent();
        }


        public static bool connected = false;
        public static string connectedport = "Disconnect";
        public static string connectedportname = "Disconnect";
        SerialPort sp = new SerialPort();
        const int TIMEOUT = 10000; //t: response msg

        private void initiatePort()
        {
            string portname = (string)cmbPort.SelectedItem;
           
            sp.NewLine = "\r\n";
            sp.BaudRate = 9600;
            sp.Parity = Parity.None;
            sp.DataBits = 8;
            sp.StopBits = StopBits.One;
            sp.Handshake = Handshake.None;
            sp.DtrEnable = true;
            sp.WriteBufferSize = 1024;

            //MessageBox.Show("1." + portname);

            sp.PortName = (string)cmbPort.SelectedItem;
            int v = sp.PortName.IndexOf("COM", 0, sp.PortName.Length);
            sp.PortName = sp.PortName.Substring(v, 5);
            string port = sp.PortName;

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

            sp.PortName = port;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            
            try
            {
                /*using (var conn = new GSMModem.Connection(cmbPort.SelectedItem))
                {
                    conn.DataReceived += dataReceiver;

                    GSMModem.SMS sms = new GSMModem.SMS(conn);
                    sms.SendResponseReceived += smsSendEvent;

                    if (rdPDU.Checked)
                        sms.SendPDU(txtbxMobile.Text, txtbxMessage.Text);
                    else
                        sms.SendText(txtbxMobile.Text, txtbxMessage.Text);
                }*/


                //MessageBox.Show("3." + port);
                // sp.WriteLine(string.Format("AT+CMGS=\"{0}\"", txtbxMobile.Text));
                //sp.WriteLine(SMS.ConvertStringToHEX(txtbxMessage.Text) + '\x1A');

                string msg = txtbxMessage.Text;
                if (rdPDU.Checked)
                    msg = SMS.ConvertStringToHEX(txtbxMessage.Text);

                sendCommand(
                    string.Format("AT+CMGS=\"{0}\"", txtbxMobile.Text),
                    msg + '\x1A'
                );
                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.Source);
            }
        }

      /*  public static string convertToPDU(string mobile, string message)
        {
            encoded = "31000B91" + mobile.SwapChar2By2() + "0008AA" + (message.Length / 2).ToString("X") + message;
            sendPDUMessage(encoded);
        }*/

        private void smsSendEvent(int sendId)
        {
            txtResult.Text += "sendId: " + sendId + "\r\n";
        }

        private void dataReceiver(string receivedData)
        {
            txtResult.Text += "receivedData: " + receivedData + "\r\n";
        }

        private void btnPort_Click(object sender, EventArgs e)
        {
            getAllComPort(cmbPort);
        }

        private void sendCommand(string command, string command2 = "")
        {
            //int timeout = TIMEOUT;
            string result = "";
            sp.BaseStream.Flush();
            /*sp.WriteLine("AT");           //Get the modem's attention
            sp.WriteLine("ATI");          // Get All Manufacturer Info
            sp.WriteLine("AT+CGMM");      // Get USB Model
            sp.WriteLine("AT+CGMI");      // Manufacturer
            sp.WriteLine("AT+CIMI");      // Get SIM IMSI number
            sp.WriteLine("AT+CGSN");      // Get modem IMEI
            sp.WriteLine("AT+CGMR");      // Print firmware version of the modem*/
            //sp.WriteLine("AT+CMGF=0"); // PDU mode
            //MessageBox.Show("text mode");
            //sp.WriteLine("AT+CMGF=1"); // text mode
            //sp.WriteLine("AT+CSCS=\"UCS2\"");
            //sp.WriteLine("AT+CSMP=49,167,0,8");

            /*while (!((t = sp.ReadExisting()).Contains("OK")) && timeout > 0)
            {
                timeout--;
            }

            txtResult.Text += t;
*/
            MessageBox.Show(command);
            // sp.WriteLine(string.Format("AT+CMGS=\"{0}\"", txtbxMobile.Text));
            //sp.WriteLine(SMS.ConvertStringToHEX(txtbxMessage.Text) + '\x1A');
            //sp.WriteLine("AT");
            sp.WriteLine(command);
            System.Threading.Thread.Sleep(2000);
            result = readPort(TIMEOUT);
            txtResult.Text += result;

            if (command2 != "")
            {
                MessageBox.Show(command2);
                sp.WriteLine(command2);
                System.Threading.Thread.Sleep(3000);
                result = readPort(TIMEOUT);
                txtResult.Text += result;
            }
            
            //result = readPort(TIMEOUT);

            
            txtResult.ScrollToCaret();

            sp.BaseStream.Flush();
        }

        private string readPort(int timeout)
        {
            string result;
            while (!((result = sp.ReadExisting()).Contains("OK")) && timeout > 0)
            {
                timeout--;
            }

            return result;
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
            initiatePort();

            if (!sp.IsOpen)
            {
                sp.Open();

                if (sp.IsOpen)
                {
                    MessageBox.Show("Connected to Port" + cmbPort.SelectedItem);
                    connected = true;
                    connectedport = sp.PortName;
                    connectedportname = cmbPort.SelectedItem.ToStringExt();
                }
            }
        }

        private void btnSendCommand_Click(object sender, EventArgs e)
        {
            sendCommand(txtCommand.Text);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (sp.IsOpen)
                sp.Close();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            txtResult.Text += SMS.ConvertStringToHEX(txtbxMessage.Text);
        }
    }
}
