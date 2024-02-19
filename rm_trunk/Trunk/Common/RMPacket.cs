using System;
using System.Diagnostics;
using System.IO;
using ProtoBuf;

namespace RM.Net
{
    // Google ProtoBuf를 사용한 abstract Packet

    [ProtoContract]
    public abstract class RMPacket
    {
        static public bool s_IsCrypro = false;

        public RMPacket()
        {
            ProtocolBytes = BitConverter.GetBytes(((ushort)0));
        }

        public RMPacket(ushort protocolType)
        {
            ProtocolType = protocolType;
            ProtocolBytes = BitConverter.GetBytes(((ushort)ProtocolType));
        }

        [ProtoMember(1)]
        public ushort ProtocolType { get; private set; }

        [ProtoMember(2)]
        public byte[] ProtocolBytes { get; private set; }

        // 실제 패킷이 호출
        public virtual byte[] ToStream()
        {
            return RMPacket.ToStream(this);
        }

        //public virtual byte[] ToStream(bool isCrypro) { throw new NotImplementedException(); }
        //public virtual byte[] ToStream()
        //{
        //    throw new NotImplementedException();
        //}


        // 패킷의 Full Byte 를 반환
        // [BodySize + ProtocolType + Body]
        public static byte[] ToStream<T>(T packet) where T : RMPacket
        {
            MemoryStream serialize = new MemoryStream();
            ProtoBuf.Serializer.Serialize<T>(serialize, packet);
            var bytes = serialize.ToArray();

            if (s_IsCrypro)
            {
                //var cryptoBuffer = Expedition.Mobile.Crypto.Crypto.RC4XORKey(NetSecurityKey.CRYPTO_KEY, bytes);
                //bytes = null;
                //bytes = cryptoBuffer;
            }

            var serializeLength = BitConverter.GetBytes(((ushort)bytes.Length));

            // Total Size = 2 + 2 + Body
            var toStreamBytes = new byte[2 + 2 + bytes.Length];

            // bodyLength
            int dstOffset = 0;
            Buffer.BlockCopy(serializeLength, 0, toStreamBytes, dstOffset, 2 /*serializeLength.Length*/);

            // PID
            dstOffset += serializeLength.Length;
            Buffer.BlockCopy(packet.ProtocolBytes, 0, toStreamBytes, dstOffset, 2 /*packet.ProtocolBytes.Length*/);

            // body
            dstOffset += packet.ProtocolBytes.Length;
            Buffer.BlockCopy(bytes, 0, toStreamBytes, dstOffset, bytes.Length);

            // toStream
            return toStreamBytes;
        }

        public static T FromStream<T>(byte[] bytes) where T : RMPacket
        {
            if (s_IsCrypro)
            {
                //var cryptoBuffer = Level9.Expedition.Mobile.Crypto.Crypto.RC4XORKey(NetSecurityKey.CRYPTO_KEY, bytes);
                //bytes = null;
                //bytes = cryptoBuffer;
            }

            MemoryStream deserialize = new MemoryStream(bytes);
            try
            {
                T packet = ProtoBuf.Serializer.Deserialize<T>(deserialize);
                return packet;
            }
            catch
            {
                Debug.Assert(false);
                return null;
            }
        }


    }
}
