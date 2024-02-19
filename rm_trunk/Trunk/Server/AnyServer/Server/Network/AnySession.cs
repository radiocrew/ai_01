using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.SocketBase;

using Level9.Expedition.Mobile.Logging;
using Level9.Expedition.Mobile.Net.SuperSocket.Server.TCP;

using RM.Common;
using RM.Net;

namespace AnyServer.Net
{
    public sealed class AnySession : SSSession<AnySession>
    {
        public void SendPacket(RMPacket packet)
        {
            SendPacket(packet.ToStream());
        }

        public override void OnReceive(ushort pID, byte[] receiveBuffer)
        {
            var protocolType = ((RMProtocols)pID);
            var handler = s_packetHandler[protocolType];

            if (null != handler)
            {
                handler.Invoke(this, receiveBuffer);
            }
        }
        
        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();
            if (null != this.SocketSession.Client)
            {


                this.SocketSession.Client.NoDelay = true;
                return;
            }

            Close();
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }

        protected override void HandleException(Exception e)
        {
            base.HandleException(e);
        }

        protected override void HandleUnknownRequest(SSProtocol requestInfo)
        {
            base.HandleUnknownRequest(requestInfo);
        }

        public static AnyServerPacketHandler s_packetHandler = new AnyServerPacketHandler();
    }
}
