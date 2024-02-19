using System;
using System.Net;

using SuperSocket.ClientEngine;

using Newtonsoft.Json;

using Level9.Expedition.Mobile.Logging;
using Level9.Expedition.Mobile.Net.SuperSocket.Client.TCP;
using Level9.Expedition.Mobile.Net.SuperSocket.Common;
using Level9.Expedition.Mobile.Net.Util;
//using Level9.Glinda.Common;
//using Level9.Glinda.PVPServer.Game.ReservationManager;

using RM.Net;
using RM.Common;

namespace ArenaServer.Net
{
    public class NetPVPManagerSendQueue : SendQueue<RMPacket>
    {
        public RMInternalClient NetPVPManager { get; set; }

        public override void Add(RMPacket addItem)
        {
            base.Add(addItem);
        }

        public override void SendPacket()
        {
            foreach (var item in WorkerQ.GetConsumingEnumerable())
            {
                if (null == NetPVPManager) continue;

                if (!NetPVPManager.IsConnected) continue;

                var sendPacket = item.ToStream();

                NetPVPManager.SendPacket(sendPacket);
            }
        }
    }

    // server accepter
    public sealed class RMInternalClient : NetClient<RMPacket>
    {
        public RMInternalClient(EndPoint remoteEndPoint, int receiveBufferSize = 8192) : base(remoteEndPoint, receiveBufferSize) 
        {
            if (null == SendQueue)
                SendQueue = new NetPVPManagerSendQueue { NetPVPManager = this, };
        }

        public override string ToString()
        {
            var toString = string.Format("[NetPVPManager] ServerUID : {0}, IsConnected : {1}, RemoteEndPoint : {2}", 
                ServerUID, IsConnected, RemoteEndPoint);

            return toString;
        }

        public override bool Initialize(int retryConnectTime = 0)
        {
            if (!base.Initialize(retryConnectTime))
            {
                Logger.Log(LogLevel.Info, "[NetPVPManager : Initialize] Failed to base Initialize");
                return false;
            }

            this.Exception += OnError;
            return true;
        }

        public override void UnInitialize()
        {
            base.UnInitialize();
        }

        public void SendPacket(RMPacket packet)
        {
            SendPacket(packet.ToStream());
        }

        protected override void OnConnected(object sender, EventArgs e)
        {
            base.OnConnected(sender, e);

            var log = string.Format("[NetPVPManager : OnConnected] Succeed!! sender : {0}", sender.ToString());
            Logger.Log(LogLevel.Info, log);

            //SendQueue.Add(new SERVER_INFO_NTF
            //{
            //    ServerUID = ArenaServer.ServerUID
            //});
        }

        protected override void OnClosed(object sender, EventArgs e)
        {
            base.OnClosed(sender, e);

            var log = string.Format("[NetPVPManager : OnClosed] Closed!! sender : {0}", sender.ToString());
            Logger.Log(LogLevel.Info, log);
        }

        protected override void OnError(object sender, ErrorEventArgs e)
        {
            base.OnError(sender, e);

            var log = string.Format("[OnError] UID : {0}, Message : {1}, InnerMessage : {2}, IsConnected : {3}, Sender : {4} ",
                ServerUID, (null == e.Exception ? "" : e.Exception.Message), (null == e.Exception.InnerException ? "" : e.Exception.InnerException.Message), IsConnected, sender.ToString());

            Logger.Log(LogLevel.Error, log);
        }

        protected override void OnReceive(SuperSocket.ProtoBase.IPackageInfo package, ushort pID, byte[] receiveBuffer)
        {
            var protocolType = ((RMProtocols)pID);
            var handler = s_packetHandlers[protocolType];

            if (null != handler)
            {
                handler.Invoke(this, receiveBuffer);
            }
        }
              
        public static MatchingPacketHandlers s_packetHandlers = new MatchingPacketHandlers();
    }
}

