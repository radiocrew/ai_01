using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using Expedition.NetClient;
using RM.Net;

using rm_login.Network;

namespace rm_login.Network
{
    public class ArenaServerSession : NetClient
    {
        static private readonly Lazy<ArenaServerSession> s_lazy = new Lazy<ArenaServerSession>(() => new ArenaServerSession());
        static public ArenaServerSession Instance { get { return s_lazy.Value; } }

        public override bool Initialize()
        {
            base.Initialize();

            InitHandler();
            m_packetProc = new System.Threading.Timer(PacketProcessing, null, 1000, 100);

            Console.WriteLine("[{0}] Arena server session Initialize completed", System.DateTime.Now.ToString("hh:mm:ss.fff"));
            return true;
        }

        public void SendPacket(RMPacket packet)
        {
            if (false == IsConnected)
            {
                return;
            }
            SendPacket(packet.ToStream());
        }
        private bool InitHandler()
        {
            this.PacketReceived += m_handlers.OnReceived;
            this.Exception += m_handlers.OnException;
            this.Error += m_handlers.OnError;
            this.Connected += m_handlers.OnConnected;
            this.Closed += m_handlers.OnClosed;

            return m_handlers.InitHandler();

        }
        private void PacketProcessing(object state)
        {
            m_handlers.PacketProcessing();
        }

        ArenaServerPacketHandler m_handlers = new ArenaServerPacketHandler();
        System.Threading.Timer m_packetProc = null;
    }
}
