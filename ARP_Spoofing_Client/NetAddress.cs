using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARP_Spoofing_Client
{
    public class NetAddress
    {
        readonly string ip;
        readonly string mac;

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
        }

        public string MAC
        {
            get
            {
                return mac;
            }
        }


    }

}
