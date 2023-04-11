using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ARP_Spoofing_Server
{
    class HandleClient
    {
        /// <summary>
        /// private attribute of HandleClient class
        /// </summary>
        private TcpClient mTcpClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_tmpTcpClient">傳入TcpClient參數</param>
        public HandleClient(TcpClient _tmpTcpClient)
        {
            this.mTcpClient = _tmpTcpClient;
        }

        /// <summary>
        /// Communicate
        /// </summary>
        public void Communicate()
        {
            try
            {
                CommunicationBase cb = new CommunicationBase();
                string msg = cb.ReceiveMsg(this.mTcpClient);
                Console.WriteLine(msg + "\n");
                cb.SendMsg("主機回傳測試", this.mTcpClient);
            }
            catch
            {
                Console.WriteLine("客戶端強制關閉連線!");
                this.mTcpClient.Close();
                Console.Read();
            }
        } // end HandleClient()
    } // end Class
}
