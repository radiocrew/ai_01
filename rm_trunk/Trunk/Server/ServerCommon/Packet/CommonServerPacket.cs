using System;
using System.Collections.Generic;

using ProtoBuf;


using RM.Net;
using RM.Common;
using RM.Server.Common;

namespace RM.Server.Net
{
    [ProtoContract]
    public class S_ARENA_MATCHING_REQ : RMPacket
    {
        public S_ARENA_MATCHING_REQ() : base((ushort)ServerProtocols.S_ARENA_MATCHING_REQ)
        {
        }

        [ProtoMember(1)]
        public ARENA_DEPATURE Depature { get; set; }

        [ProtoMember(2)]
        public int ArenaID { get; set; }
    }

    [ProtoContract]
    public class S_ARENA_MATCHING_ACK : RMPacket
    {

        public S_ARENA_MATCHING_ACK() : base((ushort)ServerProtocols.S_ARENA_MATCHING_ACK)
        {
        }

        [ProtoMember(1)]
        public PacketResult Result { get; set; }

        [ProtoMember(2)]
        public Guid PlayerUID { get; set; }
    }

    [ProtoContract]
    public class S_ARENA_MATCHING_CANCEL : RMPacket
    {
        public S_ARENA_MATCHING_CANCEL() : base((ushort)ServerProtocols.S_ARENA_MATCHING_CANCEL)
        {
        }

        [ProtoMember(1)]
        public Guid PlayerUID { get; set; }
    }

    [ProtoContract]
    public class S_ARENA_CREATE_REQ : RMPacket
    {
        public S_ARENA_CREATE_REQ() : base((ushort)ServerProtocols.S_ARENA_CREATE_REQ)
        {
        }

        [ProtoMember(1)]
        public RequestArenaType RequestType { get; set; }

        [ProtoMember(2)]
        public int ArenaID { get; set; }

        [ProtoMember(3)]
        public List<ARENA_DEPATURE> MatchedPlayers { get; set; }
    }

    [ProtoContract]
    public class S_ARENA_CREATE_ACK : RMPacket
    {    
        public S_ARENA_CREATE_ACK() : base((ushort)ServerProtocols.S_ARENA_CREATE_ACK)
        {
        }

        [ProtoMember(1)]
        public RequestArenaType RequestType { get; set; }

        [ProtoMember(2)]
        public Guid ArenaKey { get; set; }

        [ProtoMember(3)]
        public List<ARENA_DEPATURE> MatchedPlayers { get; set; }

        [ProtoMember(4)]
        public PacketResult Result { get; set; }
    }

    [ProtoContract]
    public class S_ARENA_MATCHING_COMPLETE_NTF : RMPacket
    {
        public S_ARENA_MATCHING_COMPLETE_NTF() : base((ushort)ServerProtocols.S_ARENA_MATCHING_COMPLETE_NTF)
        {
        }

        [ProtoMember(1)]
        public ARENA_ARRIVAL Arrival { get; set; }

        [ProtoMember(2)]
        public Guid PlayerUID { get; set; }

        [ProtoMember(3)]
        public PacketResult Result { get; set; }
    }

}


