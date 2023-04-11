using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARP_Spoofing_Server
{
    public class NetAddress
    {
        string ip;
        string mac;

        public NetAddress(string ip, string mac)
        {
            this.ip = ip;
            this.mac = mac;
        }

        public string IP
        {
            get
            {
                return ip;
            }
            set
            {
                ip = IP;
            }
        }

        public string MAC
        {
            get
            {
                return mac;
            }
            set
            {
                mac = MAC;
            }
        }
    }
}
