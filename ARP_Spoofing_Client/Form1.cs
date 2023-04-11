using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Timers;
using SharpPcap;
using SharpPcap.LibPcap;
using SharpPcap.WinDivert;
using PacketDotNet;
using System.Net;
using ARP_Spoofing_Client.ObjectSerializer;
using System.Net.Sockets;

namespace ARP_Spoofing_Client
{

    public partial class ARP_Spoofing_Client : Form
    {
        System.Timers.Timer timer = new System.Timers.Timer();

        private List<NetAddress> existData;
        private IObjectSerializable serializer;
        private DeviceData deviceData;
        private Thread sendMsg;
        private Thread ReceiveMsg;
        private static Socket socket;


        public ARP_Spoofing_Client()
        {
            InitializeComponent();
            Initialize();
            sendMsg = new Thread(SendMsgToServer);
            ReceiveMsg = new Thread(ReceiveMsgFromServer);
        }

        #region Source Initialize
        private void Initialize()
        {
            Thread.CurrentThread.Name = "Main Thread";


            /* get ip config */
            try
            {
                // Retrieve the device list
                var devices = LibPcapLiveDeviceList.Instance;
                string myIp = "";
                string mac = "";
                string netClass = "";
                string mask = "";
                LibPcapLiveDevice device;

                existData = new List<NetAddress>();
                serializer = new JsonObjectSerializer();

                // If no devices were found print an error
                if (devices.Count < 1)
                {
                    WriteToLogFile("No devices were found on this machine");
                    return;
                }
                device = devices[0]; // Default0: ether Mac

                /* Get net class and subnet mask */
                var hostName = Dns.GetHostName();
                foreach (var ip in Dns.GetHostEntry(hostName).AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        myIp = ip.ToString();
                    }
                }
                netClass = GetIPClass(myIp).ToString();
                mask = string.Join("", device.Addresses.Select(x => x.Netmask));

                /* Hex form: xx.xx.xx.xx.xx.xx */
                mac = ToHexMacString(device.Interface.MacAddress);

                /* Write ipconfig to log file */
                WriteToLogFile($"My IP is {netClass} Class: {myIp}");
                WriteToLogFile($"MacAddress: {mac}");
                WriteToLogFile($"Subnet mask: {mask}");

                /* Initialize device data */
                deviceData = new DeviceData()
                {
                    Addr = new NetAddress(myIp, mac),
                    Device = device,
                    Mask = mask
                };

                /* GUI Initialize */
                this.textBox_Source_Ip.Text = deviceData.Addr.IP;
                this.textBox_Source_Mac.Text = deviceData.Addr.MAC;
                this.textBox_Source_Mask.Text = deviceData.Mask;
            }
            catch (Exception e)
            {
                WriteToLogFile($"Initialize Exception: {e.ToString()}");
            }
        }
        #endregion

        # region Action
        private void textBox1_TextChanged(object sender, EventArgs e) // Source IP textbox
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) // Source Mac textbox
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e) // Source Mask textbox
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e) // Target IP textbox
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e) // Tartget Mac textbox
        {

        }

        private void ARP_Start_Click(object sender, EventArgs e) // ARP Start button click
        {
            ARP_Start.Enabled = false;
            ARP_Stop.Enabled = true;
            
            Thread sendMsg = new Thread(SendMsgToServer);
            Thread ReceiveMsg = new Thread(ReceiveMsgFromServer);
            ReceiveMsg.IsBackground = true;

            ReceiveMsg.Start();
            sendMsg.Start();
        }

        private void ARP_Stop_Click(object sender, EventArgs e) // ARP Stop button click
        {
            ARP_Start.Enabled = true;
            ARP_Stop.Enabled = false;
            this.Close();
            //sendMsg.Abort();
        }

        private void ARP_Spoofing_Client_Load(object sender, EventArgs e)
        {
            WriteToLogFile($"Connect To Server at {DateTime.Now}");
        }

        private void Server_Ip_Click(object sender, EventArgs e) // Server IP label
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e) // Server IP textbox
        {

        }


        private void Connect_Click(object sender, EventArgs e) // Connect button click
        {
            ClientSocket(textBox_Server_Ip.Text, 3000);
            //ClientSocket("127.0.0.1", 3000);
        }
        #endregion

        /// <summary>
        /// Client socket
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        private void ClientSocket(string ip, int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            IPAddress myIp = IPAddress.Parse(ip);
            IPEndPoint point = new IPEndPoint(myIp, port);
            try
            {
                socket.Connect(point);
                WriteToLogFile("Connect Succese! " + socket.RemoteEndPoint.ToString());
                State.Text = "已連線";
            }
            catch (SocketException e)
            {
                WriteToLogFile($"ClientSocket SocketException...{e.ToString()}");
            }
                     
        }


        private void ReceiveMsgFromServer()
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                int rec = socket.Receive(buffer);
                if (rec == 0)
                {
                    WriteToLogFile("Server Loss!");
                    break;
                }
                string receText = System.Text.Encoding.UTF8.GetString(buffer, 0, rec);
                WriteToLogFile("Server :" + receText);
            }
        }

        private void SendMsgToServer()
        {
            NetAddress na = new NetAddress(textBox_Target_Ip.Text, textBox_Target_Mac.Text);
            //while (true)
            {
                string inputText = serializer.Serialize<NetAddress>(na);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(inputText);
                socket.Send(buffer);
            }
        }

        #region Common Used
        /// <summary>
        /// Reformat from PhysicalAddress into Hex String: xx.xx.xx.xx.xx.xx
        /// </summary>
        /// <param name="physicalAddress"></param>
        /// <returns>xx.xx.xx.xx.xx.xx</returns>
        public static string ToHexMacString(System.Net.NetworkInformation.PhysicalAddress physicalAddress)
        {
            var mac = "";
            byte[] addr = physicalAddress.GetAddressBytes();

            foreach (var value in addr)
            {
                if (value != addr.Last())
                {
                    mac += Convert.ToString(value, 16);
                    mac += ".";
                }
                else
                {
                    mac += Convert.ToString(value, 16);
                }
            }
            return mac;
        }

        /// <summary>
        /// Get IP class
        /// </summary>
        /// <param name="IP"></param>
        /// <returns>A, B, C... Class</returns>
        public static IPClass GetIPClass(string IP)
        {
            if (!string.IsNullOrEmpty(IP) && IP.Split('.').Length == 4 &&
                !string.IsNullOrEmpty(IP.Split('.').Last()))
            {
                string ipclassstr = IP.Split('.').First();
                int ipclasssnum = int.Parse(ipclassstr);
                if (0 <= ipclasssnum && ipclasssnum <= 126)
                {
                    return IPClass.A;
                }
                if (128 <= ipclasssnum && ipclasssnum <= 191)
                {
                    return IPClass.B;
                }
                if (192 <= ipclasssnum && ipclasssnum <= 223)
                {
                    return IPClass.C;
                }
                if (224 <= ipclasssnum && ipclasssnum <= 239)
                {
                    return IPClass.D;
                }
                if (240 <= ipclasssnum && ipclasssnum <= 255)
                {
                    return IPClass.E;
                }
            }
            else return IPClass.notDetected;
            return IPClass.notDetected;
        }


        /// <summary>
        /// Write exist data to xml file
        /// </summary>
        /// <param name="Message"></param>
        public static void WriteToLogFile(string Message)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
                if (!File.Exists(filepath))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
            }
            catch { }
        }

        public static void WriteToJsonFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ExistData_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".json";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
        #endregion
    }

    public enum IPClass { A, B, C, D, E, notDetected }
    public class DeviceData
    {
        public NetAddress Addr;
        public LibPcapLiveDevice Device;
        public string Mask;
    }
}
