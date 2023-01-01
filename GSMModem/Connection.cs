using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMModem
{
    public class Connection : IDisposable
    {
        private const int TIMEOUT = 10000;

        public static string[] GetPortName()
        {
            return SerialPort.GetPortNames();
        }        


        public delegate void GSMDataReceivedEventHandler(string receivedData);

        private SerialPort port;
        public event GSMDataReceivedEventHandler DataReceived;

      

        public Connection(string portName)
        {
            port = new SerialPort();
            //port.ReadTimeout = 300;
            //port.WriteTimeout = 300;
            //port.Encoding = Encoding.GetEncoding("utf-8");
            //port.DataReceived += Port_DataReceived;
            //port.DtrEnable = true;
            //port.Handshake = Handshake.None;
            //port.NewLine = "\r\n";
            //port.WriteBufferSize = 1024;

            port.NewLine = "\r\n";
            port.BaudRate = 9600;
            port.Parity = Parity.None;
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.Handshake = Handshake.None;
            port.DtrEnable = true;
            port.WriteBufferSize = 1024;
            port.PortName = portName;
        }

        public void Dispose()
        {
            Close();
            port.DataReceived -= new SerialDataReceivedEventHandler(Port_DataReceived);
            port.Dispose();
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Chars)
            {
                if (DataReceived != null)
                    DataReceived(port.ReadExisting());
            }
        }

        private string readPort(int timeout)
        {
            string result;
            while (!((result = port.ReadExisting()).Contains("OK")) && timeout > 0)
            {
                timeout--;
            }

            return result;
        }

        public void FlushPort()
        {
            port.BaseStream.Flush();
        }

        public void Open()
        {
            try
            {
                if (!this.port.IsOpen)
                {
                    this.port.Open();

                }
            }
            catch (UnauthorizedAccessException un)
            {
                throw new Exception("Port is busy. Please exit other software");
            }
            catch
            {
                throw;
            }
        }

        public void Close()
        {
            try
            {
                if (port.IsOpen)
                    this.port.Close();                
            }
            catch
            {
                throw;
            }
        }


        public string SendCommand(string value)
        {
            if (DataReceived != null)
                DataReceived("Command: " + value);

            this.port.WriteLine(value);
            System.Threading.Thread.Sleep(1000);


            var result = readPort(TIMEOUT);
            if (DataReceived != null)
                DataReceived("Response: " + result);

           
            return result;

        }

    
    }
}
