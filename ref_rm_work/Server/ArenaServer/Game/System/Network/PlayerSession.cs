using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;

using SuperSocket.SocketBase;
using Newtonsoft.Json;

using Level9.Expedition.Mobile.Logging;
using Level9.Expedition.Mobile.Net.SuperSocket.Server.TCP;

using RM.Net;
using RM.Common;

using ArenaServer.Game;

namespace ArenaServer.Net
{
    public sealed partial class PlayerSession : SSSession<PlayerSession>
    {
        public void ForceSendPacket(RMPacket packet)
        {
            SendPacket(packet.ToStream());
        }

        public void SendPacket(RMPacket packet)
        {
            if (true == m_bPending)
            {
                m_PendingQueue.Enqueue(packet);
            }
            else
            {
                //-jinsub testcode?!
                RMPacket prePacket;
                while (m_PendingQueue.TryDequeue(out prePacket))
                {
                    SendPacket(packet);
                }

                SendPacket(packet.ToStream());
            }
        }

        public void Pending()
        {
            m_bPending = true;
        }

        public void ReleasePending()
        {
            RMPacket packet;
            while (m_PendingQueue.TryDequeue(out packet))
            {
                SendPacket(packet);
            }

            m_bPending = false;
        }

        public override void OnReceive(ushort pID, byte[] receiveBuffer)
        {
            var protocolType = ((RMProtocols)pID);
            var handler = s_packetHandlers[protocolType];

            if (null != handler)
            {
                handler.Invoke(this, receiveBuffer);
            }
        }

        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();
            if (null == this.SocketSession.Client)
            {
                Debug.Assert(false);
                this.Close();
                return;
            }
            this.SocketSession.Client.NoDelay = true;

#if DEBUG
            Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Info, "[OnSessionStarted] session({0}))", base.SessionID);
#endif

            ARENA_HELLO_NTF packet = new ARENA_HELLO_NTF();
            SendPacket(packet);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);

#if DEBUG
            var log = string.Format("[OnSessionClosed] Reason : {0}, \r\nSessionID : {1}, \r\nConnected : {2}, \r\nRemoteEndPoint : {3}", reason, SessionID, Connected, RemoteEndPoint);
            Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Info, log);
#endif

            this.DestoryPlayerInfo();
        }

        protected override void HandleException(Exception e)
        {
            var log = string.Format("[HandleException] Exception : {0}, \r\n InnerException : {1} \r\nSessionID : {2}, \r\nConnected : {3}, \r\nRemoteEndPoint : {4}", e.Message, (null == e.InnerException) ? "" : e.InnerException.Message, SessionID, Connected, RemoteEndPoint);
            Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Error, log);

            base.HandleException(e);
        }

        protected override void HandleUnknownRequest(SSProtocol requestInfo)
        {
            var log = string.Format("[HandleUnknownRequest] RequestInfo : {0}, \r\nSessionID : {1}, \r\nConnected : {2}, \r\nRemoteEndPoint : {3}", requestInfo.ToString(), SessionID, Connected, RemoteEndPoint);
            Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Error, log);

            base.HandleUnknownRequest(requestInfo);
        }

        volatile bool m_bPending = false;
        ConcurrentQueue<RMPacket> m_PendingQueue = new ConcurrentQueue<RMPacket>();

        public static ArenaPacketHandlers s_packetHandlers = new ArenaPacketHandlers();
    }
}