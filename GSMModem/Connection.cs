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

        public static string[] GetPortName()
        {
            return SerialPort.GetPortNames();
        }




        public delegate void GSMDataReceivedEventHandler(string receivedData);

        private SerialPort port;
        public event GSMDataReceivedEventHandler DataReceived;

      

        public Connection(string portName)
        {
            port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            port.ReadTimeout = 300;
            port.WriteTimeout = 300;
            port.Encoding = Encoding.GetEncoding("utf-8");
            port.DataReceived += Port_DataReceived;
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

        public void Open()
        {
            try
            {
                if (!this.port.IsOpen)
                {
                    this.port.Open();
                    this.port.DtrEnable = true;
                    this.port.RtsEnable = true;
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


        public void SendCommand(string value)
        {
            this.port.WriteLine(value);
        }
    }
}
