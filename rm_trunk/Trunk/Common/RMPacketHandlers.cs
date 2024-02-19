using System;

namespace RM.Net
{

    public interface IPacketHandler<TySession>
    {
        void Invoke(TySession session, byte[] receiveBuffer);
    }

    public interface IPacketHandler
    {
        void Invoke(byte[] receiveBuffer);
    }

    public class PacketHandler<TySession, TyPacket> : IPacketHandler<TySession> where TyPacket : RMPacket
    {
        public Action<TySession, TyPacket> Handler;

        public PacketHandler(Action<TySession, TyPacket> handler)
        {
            this.Handler = handler;
        }
        public void Invoke(TySession session, byte[] receiveBuffer)
        {
            var packet = RMPacket.FromStream<TyPacket>(receiveBuffer);
            Handler(session, packet);
        }
    }

    public class PacketHandler<TyPacket> : IPacketHandler where TyPacket : RMPacket
    {
        public Action<TyPacket> Handler;

        public PacketHandler(Action<TyPacket> handler)
        {
            this.Handler = handler;
        }
        public void Invoke(byte[] receiveBuffer)
        {
            var packet = RMPacket.FromStream<TyPacket>(receiveBuffer);
            Handler(packet);
        }
    }

}
