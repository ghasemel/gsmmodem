using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Threading;

namespace GSMModem
{
    public class SMS
    {
        private Connection mCon;

        public delegate void SmsSendEventHandler(int sendId);
        public delegate void SmsDeliveryEventHandler(int sendId, string deliveryDate);

        public event SmsSendEventHandler SendResponseReceived;
        public event SmsDeliveryEventHandler DeliveryReceived;

        public SMS(Connection connection)
        {
            mCon = connection;
            mCon.Open();                       
            mCon.DataReceived += MCon_DataReceived;
            mCon.SendCommand("AT");
            
        }

        public SMS()
        {

        }


        private void EnableTextMode()
        {
            /*
AT
AT+CSCS="UCS2"
AT+CSMP=49,167,0,8
AT+CMGF=1
AT+CNMI=2,2,0,1,0
AT+CMGS="09111006700"
> text (ctrl+z)
    */
            EnableDelivery();
            SetCharsetToUTF8();
            mCon.SendCommand("AT+CMGF=1");
        }

        private void EnablePDUMode()
        {
            //EnableDelivery();
            mCon.SendCommand("AT+CMGF=0");
        }


        private void SetCharsetToUTF8()
        {
            //+CSCS: ("IRA", "GSM", "HEX", "PCCP437", "8859-1", "UCS2", "UCS2_0X81")
            // AT+CSCS=?
            mCon.SendCommand("AT+CSCS=\"UCS2\"");
            mCon.SendCommand("AT+CSMP=49,167,0,8");
        }

        private void EnableDelivery()
        {
            mCon.SendCommand("AT+CNMI=2,2,0,1,0");
        }

       

        public void SendText(string mobile, string message)
        {
            EnableTextMode();

            mCon.SendCommand(string.Format("AT+CMGS=\"{0}\"", mobile));
            mCon.SendCommand(ConvertStringToHEX(message) + '\x1A');
            Thread.Sleep(5000);
        }


        private const int MAX_SINGLE_LEN = 268;

        public void SendPDU(string mobile, string message, string prefix)
        {
            mCon.FlushPort();
            EnablePDUMode();

            string encoded;

            message = ConvertStringToHEX(message);

            // single sms
            if (message.Length <= MAX_SINGLE_LEN)
            {
                encoded = "31000" + GetMobileFormat(mobile, prefix) + mobile.SwapChar2By2() + "0008AA" + (message.Length / 2).ToString("X") + message;
                sendPDUMessage(encoded);                
            }
            else
            {
                // multi-part sms
                int part_len = 0;

                int part_count = message.Length / MAX_SINGLE_LEN;
                if (message.Length % MAX_SINGLE_LEN > 0)
                    part_count++;

                int part_index = 1;

                Random random = new Random();
                string uniq_send = "00"; // random.Next(0, 255).ToString("X").PadLeft(2, '0');

                do
                {
                    part_len = message.Length > MAX_SINGLE_LEN ? MAX_SINGLE_LEN : message.Length;

                    // get part
                    string part = message.Substring(0, part_len);

                    // remove part from message
                    message = message.Remove(0, part_len);

                    
                    string UDH = "0003" + uniq_send + part_count.ToString("X").PadLeft(2, '0') + part_index.ToString("X").PadLeft(2, '0');
                    int UDHL = UDH.Length / 2;

                    int UDL = 1 + UDHL + part.Length / 2;

                    string pdu_part = UDL.ToString("X").PadLeft(2, '0') + UDHL.ToString("X").PadLeft(2, '0') + UDH + part;
                    encoded = "51000" + GetMobileFormat(mobile, prefix) + mobile.SwapChar2By2() + "00080B" + pdu_part;

                    // send msg
                    sendPDUMessage(encoded);
                    

                    part_index++;
                } while (part_index <= part_count);
            }
            
        }

        private string GetMobileFormat(string mobile, string prefix)
        {
            // 8: local format
            // 9: internatioanl format         
            
            return mobile.Length.ToString("X") + (mobile.StartsWith(prefix) ? "9" : "8") + "1"; 
            
        }

        private void sendPDUMessage(string encoded)
        {
            mCon.SendCommand("AT+CMGS=" + (encoded.Length / 2));

            // add 00, it's a fixed prefix
            encoded = "00" + encoded;
            Debug.WriteLine(encoded);

            mCon.SendCommand(encoded + '\x1A');
            Thread.Sleep(5000);
        }






        private void onSendIdReceived(int sendId)
        {
            if (SendResponseReceived != null)
                SendResponseReceived(sendId);
        }

        private void onDeliveryIdReceived(int sendId, string deliveryDate)
        {
            if (DeliveryReceived != null)
                DeliveryReceived(sendId, deliveryDate);
        }

        private void MCon_DataReceived(string receivedData)
        {
            Debug.WriteLine("Sms Data Received: " + receivedData);
            receivedData = receivedData.Trim().Replace("OK", "").Replace("\r\n", "");
            if (receivedData.Contains("+CMGS"))
            {
                Regex reg = new Regex("[0-9]+");

                string s = receivedData.Remove(0, receivedData.IndexOf("+CMGS"));
                if (reg.IsMatch(s))
                {
                    string sendId = reg.Match(s).Value;
                    onSendIdReceived(int.Parse(sendId));
                }
            }

            if (receivedData.Contains("+CDS"))
            {
                Regex reg = new Regex("[0-9]+");

                string s = receivedData.Remove(0, receivedData.IndexOf("+CDS"));

                // for text mode
                if (s.Contains("+CDS: 6"))
                {
                    s = s.Remove(0, s.IndexOf(","));
                    if (reg.IsMatch(s))
                    {
                        string sentId = reg.Match(s).Value;

                        reg = new Regex("[0-9]{4}/[0-9]{2}/[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}");
                        if (reg.IsMatch(s))
                        {
                            var m = reg.Matches(s);
                            if (m.Count == 2)
                            {
                                onDeliveryIdReceived(int.Parse(sentId), m[1].Value);
                            }
                        }


                    }
                }

                // for PDU mode
                if (s.Contains("+CDS: 25"))
                {
                    // sample report(remove space to get real response): 07 91 89 29 00 00 90 02 06 94 0C91891 911007600 712090 023044 41 712090 023084 4100

                    s = s.Replace("+CDS: 25", "");

                    if (s.Length > 18)
                        s = s.Remove(0, 18);

                    if (s.Length > 2)
                    {
                        int sentId = int.Parse(s.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);


                        if (s.Length > 18)
                            s = s.Remove(0, 18);

                        s = s.SwapChar2By2();

                        if (s.Length > 14)
                            s = s.Remove(0, 14);

                        if (s.Length > 12)
                            s = s.Substring(0, 12);

                        if (s.Length == 12)
                        {
                            s = "20" + s.Insert(2, "/").Insert(5, "/").Insert(8, " ").Insert(11, ":").Insert(14, ":");
                            onDeliveryIdReceived(sentId, s);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// This function only supports unicode characters, for ASCII it generates a wrong message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ConvertStringToHEX(string message)
        {            
            UnicodeEncoding uni = new UnicodeEncoding();
            Byte[] encodedBytes = uni.GetBytes(message);
            string text = "";
            for (int i = 0; i < encodedBytes.Length; i += 2)
            {
                text += string.Format("{0:X2}", encodedBytes[i + 1]) + string.Format("{0:X2}", encodedBytes[i]);
            }
            return text;
        }
    }
}
