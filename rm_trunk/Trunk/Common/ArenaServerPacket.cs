using System;
using System.Collections.Generic;
using ProtoBuf;
using RM.Common;

namespace RM.Net
{
    [ProtoContract]
    public class HEART_BEAT : RMPacket
    {
        public HEART_BEAT() : base((ushort)RMProtocols.HEART_BEAT)
        {
        }
    }

    [ProtoContract]
    public class TEST_COMMAND : RMPacket
    {
        public TEST_COMMAND() : base((ushort)RMProtocols.TEST_COMMAND)
        {
        }

        [ProtoMember(1)]
        public TestCommandType TestCommandType { get; set; }

        [ProtoMember(2)]
        public Guid UID_0 { get; set; }

        [ProtoMember(3)]
        public int Int_0 { get; set; }

        [ProtoMember(4)]
        public int Int_1 { get; set; }

        [ProtoMember(5)]
        public int Int_2 { get; set; }

        [ProtoMember(6)]
        public float Float_0 { get; set; }

        [ProtoMember(7)]
        public float Float_1 { get; set; }

        [ProtoMember(8)]
        public float Float_2 { get; set; }
    }

    [ProtoContract]
    public class ARENA_HELLO_NTF : RMPacket
    {
        public ARENA_HELLO_NTF() : base((ushort)RMProtocols.ARENA_HELLO_NTF)
        {
        }
    }


    [ProtoContract]
    public class ARENA_LOGIN_REQ : RMPacket
    {
        public ARENA_LOGIN_REQ() : base((ushort)RMProtocols.ARENA_LOGIN_REQ)
        {
        }

        [ProtoMember(1)]
        public Guid ArenaKey { get; set; }

        [ProtoMember(2)]
        public Guid PlayerUID { get; set; }

        [ProtoMember(3)]
        public int TestPlayerObjectID { get; set; }
    }

    [ProtoContract]
    public class ARENA_LOGIN_ACK : RMPacket
    {
        public ARENA_LOGIN_ACK() : base((ushort)RMProtocols.ARENA_LOGIN_ACK)
        {
        }

        [ProtoMember(1)]
        public Guid ArenaKey { get; set; }

        [ProtoMember(2)]
        public Guid PlayerUID { get; set; }

        [ProtoMember(3)]
        public PacketResult Result { get; set; }
    }

    [ProtoContract]
    public class ARENA_MOVE_REQ : RMPacket
    {
        public ARENA_MOVE_REQ() : base((ushort)RMProtocols.ARENA_MOVE_REQ)
        {
        }

        [ProtoMember(1)]
        public int ArenaID { get; set; }

        [ProtoMember(2)]
        public Guid playerUID { get; set; }
    }

    [ProtoContract]
    public class ARENA_MOVE_ACK : RMPacket
    {
        public ARENA_MOVE_ACK() : base((ushort)RMProtocols.ARENA_MOVE_ACK)
        {
        }

        [ProtoMember(1)]
        public PacketResult Result { get; set; }

        [ProtoMember(2)]
        public Guid PlayerUID { get; set; }

        //[ProtoMember(3)]
        //ARENA_ARRIVAL Arrival { get; set; }//-서버 분리 될때 사용 됨.
    }

    [ProtoContract]
    public class MATCHED_ARENA_MOVE_REQ : RMPacket
    {
        public MATCHED_ARENA_MOVE_REQ() : base((ushort)RMProtocols.MATCHED_ARENA_MOVE_REQ)
        {
        }

        [ProtoMember(1)]
        public Guid ArenaKey { get; set; }

        [ProtoMember(2)]
        public Guid playerUID { get; set; }
    }

    [ProtoContract]
    public class MATCHED_ARENA_MOVE_ACK : RMPacket
    {
        public MATCHED_ARENA_MOVE_ACK() : base((ushort)RMProtocols.MATCHED_ARENA_MOVE_ACK)
        {
        }

        [ProtoMember(1)]
        public PacketResult Result { get; set; }

        [ProtoMember(2)]
        public Guid PlayerUID { get; set; }
    }


    [ProtoContract]
    public class ARENA_MATCHING_REQ : RMPacket
    {
        public ARENA_MATCHING_REQ() : base((ushort)RMProtocols.ARENA_MATCHING_REQ)
        {
        }

        [ProtoMember(1)]
        public int ArenaID { get; set; }
    }

    [ProtoContract]
    public class ARENA_MATCHING_CANCEL : RMPacket
    {
        public ARENA_MATCHING_CANCEL() : base((ushort)RMProtocols.ARENA_MATCHING_CANCEL)
        {
        }

        [ProtoMember(1)]
        public Guid PlayerUID { get; set; }
    }

    [ProtoContract]
    public class ARENA_MATCHING_ACK : RMPacket
    {
        public ARENA_MATCHING_ACK() : base((ushort)RMProtocols.ARENA_MATCHING_ACK)
        {
        }

        [ProtoMember(1)]
        public PacketResult Result { get; set; }

        [ProtoMember(2)]
        public Guid PlayerUID { get; set; }
    }

    [ProtoContract]
    public class ARENA_MATCHING_COMPLETE_NTF : RMPacket
    {
        public ARENA_MATCHING_COMPLETE_NTF() : base((ushort)RMProtocols.ARENA_MATCHING_COMPLETE_NTF)
        {
        }

        [ProtoMember(1)]
        public ARENA_ARRIVAL Arrival { get; set; }

        [ProtoMember(2)]
        public Guid PlayerUID { get; set; }
    }

    // Arena 접속 후 가장 먼저 받는 패킷
    [ProtoContract]
    public class ARENA_ENTER_PLAYER_NTF : RMPacket
    {
        public ARENA_ENTER_PLAYER_NTF() : base((ushort)RMProtocols.ARENA_ENTER_PLAYER_NTF)
        {
        }

        [ProtoMember(1)]
        public int ArenaID { get; set; }//-arena.json 에서 map id 에 접근 가능함.

        [ProtoMember(2)]
        public int MapID { get; set; }

        [ProtoMember(3)]
        public List<int> ArenaObjectIDs { get; set; } // 그 스테이지에 등장할 ArenaObject(영웅+몬스터) IDs  

        [ProtoMember(4)]
        public HERO_DATA HeroData { get; set; }       // 이 패킷을 받는 주체 영웅

        [ProtoMember(5)]
        public List<ARENA_OBJECT_DATA> ArenaObjectDataList { get; set; }       // 다른 영웅 + 몬스터 정보
    }

    [ProtoContract]
    public class ARENA_ENTER_OBJECT_NTF : RMPacket
    {
        public ARENA_ENTER_OBJECT_NTF() : base((ushort)RMProtocols.ARENA_ENTER_OBJECT_NTF)
        {
        }

        [ProtoMember(1)]
        public ARENA_OBJECT_DATA ArenaObjectData { get; set; }
    }

    [ProtoContract]
    public class ARENA_LEAVE_OBJECT_NTF : RMPacket
    {
        public ARENA_LEAVE_OBJECT_NTF() : base((ushort)RMProtocols.ARENA_LEAVE_OBJECT_NTF)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }
    }

    [ProtoContract]
    public class HERO_MOVEMENT_NTF : RMPacket
    {
        public HERO_MOVEMENT_NTF() : base((ushort)RMProtocols.HERO_MOVEMENT_NTF)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public float Direction { get; set; }

        [ProtoMember(3)]
        public NVECTOR3 Position { get; set; }

        [ProtoMember(4)]
        public NVECTOR3 Force { get; set; }

        [ProtoMember(5)]
        public ObjectState ObjectState { get; set; }

    }

    [ProtoContract]
    public class START_ATTACK : RMPacket
    {
        public START_ATTACK() : base((ushort)RMProtocols.START_ATTACK)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }
    }

    [ProtoContract]
    public class STOP_ATTACK : RMPacket
    {
        public STOP_ATTACK() : base((ushort)RMProtocols.STOP_ATTACK)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }
    }

    [ProtoContract]
    public class CAST_SKILL : RMPacket
    {
        public CAST_SKILL() : base((ushort)RMProtocols.CAST_SKILL)
        {
        }

        [ProtoMember(1)]
        public int SkillID { get; set; }

        [ProtoMember(2)]
        public MOVEMENT_DATA MovementData { get; set; }

        [ProtoMember(3)]
        public SKILL_PARAM_DATA SkillParamData { get; set; }
    }

    [ProtoContract]
    public class CAST_SKILL_NTF : RMPacket
    {
        public CAST_SKILL_NTF() : base((ushort)RMProtocols.CAST_SKILL_NTF)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public int SkillID { get; set; }

        [ProtoMember(3)]
        public MOVEMENT_DATA MovementData { get; set; }

        [ProtoMember(4)]
        public float CastDirection { get; set; }
    }

    [ProtoContract]
    public class CAST_SKILL_PROJECTILE_NTF : RMPacket
    {
        public CAST_SKILL_PROJECTILE_NTF() : base((ushort)RMProtocols.CAST_SKILL_PROJECTILE_NTF)
        {
        }

        [ProtoMember(1)]
        public Guid OwnerUID { get; set; }

        [ProtoMember(2)]
        public int SkillID { get; set; }

        [ProtoMember(3)]
        public MOVEMENT_DATA MovementData { get; set; }

        [ProtoMember(4)]
        public PROJECTILE_DATA ProjectileData { get; set; }
    }

    [ProtoContract]
    public class PROJECTILE_REMOVE_NTF : RMPacket
    {
        public PROJECTILE_REMOVE_NTF() : base((ushort)RMProtocols.PROJECTILE_REMOVE_NTF)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public bool Hit { get; set; }

        [ProtoMember(3)]
        public NVECTOR3 HitPosition { get; set; }
    }

    [ProtoContract]
    public class INVOKE_SKILL_NTF : RMPacket
    {
        public INVOKE_SKILL_NTF() : base((ushort)RMProtocols.INVOKE_SKILL_NTF)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public int SkillID { get; set; }

        [ProtoMember(3)]
        public MOVEMENT_DATA MovementData { get; set; }

        [ProtoMember(4)]
        public SKILL_PARAM_DATA SkillParamData { get; set; }
    }

    [ProtoContract]
    public class DAMAGE_NTF : RMPacket
    {
        public DAMAGE_NTF() : base((ushort)RMProtocols.DAMAGE_NTF)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public int SkillID { get; set; }

        [ProtoMember(3)]
        public HEALTH_DATA HealthData { get; set; }

        [ProtoMember(4)]
        public ArenaObjectType AttackerType { get; set; }

        [ProtoMember(5)]
        public int Damage { get; set; }
    }

    [ProtoContract]
    public class STATUS_NTF : RMPacket
    {
        public STATUS_NTF() : base((ushort)RMProtocols.STATUS_NTF)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public STATUS_DATA StatusData { get; set; }
    }

    [ProtoContract]
    public class STAT_NTF : RMPacket
    {
        public STAT_NTF() : base((ushort)RMProtocols.STAT_NTF)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public STAT_DATA StatData { get; set; }
    }

    [ProtoContract]
    public class NPC_MOVEMENT_NTF : RMPacket
    {
        public NPC_MOVEMENT_NTF() : base((ushort)RMProtocols.NPC_MOVEMENT_NTF)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public NVECTOR3 StartPos { get; set; }

        [ProtoMember(3)]
        public NVECTOR3 EndPos { get; set; }

        [ProtoMember(4)]
        public float Direction { get; set; }

    }

    [ProtoContract]
    public class HEALTH_NTF : RMPacket
    {
        public HEALTH_NTF() : base((ushort)RMProtocols.HEALTH_NTF)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public HEALTH_DATA HealthData { get; set; }
    }


    [ProtoContract]
    public class ACTIVE_LEVEL_NTF : RMPacket
    {
        public ACTIVE_LEVEL_NTF() : base((ushort)RMProtocols.ACTIVE_LEVEL_NTF)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public ACTIVE_LEVEL ActiveLevelData { get; set; }
    }

    [ProtoContract]
    public class HERO_REVIVE_REQ : RMPacket
    {
        public HERO_REVIVE_REQ() : base((ushort)RMProtocols.HERO_REVIVE_REQ)
        {
        }
    }

    [ProtoContract]
    public class HERO_REVIVE_ACK : RMPacket
    {
        public HERO_REVIVE_ACK() : base((ushort)RMProtocols.HERO_REVIVE_ACK)
        {
        }
    }

    [ProtoContract]
    public class DECK_USE_REQ : RMPacket
    {
        public DECK_USE_REQ() : base((ushort)RMProtocols.DECK_USE_REQ)
        {
        }

        [ProtoMember(1)]
        public int DeckID { get; set; }
    }

    [ProtoContract]
    public class DECK_SELECT_REQ : RMPacket
    {
        public DECK_SELECT_REQ() : base((ushort)RMProtocols.DECK_SELECT_REQ)
        {
        }

        [ProtoMember(1)]
        public int DeckID { get; set; }
    }

    [ProtoContract]
    public class DECK_GEN_REQ : RMPacket
    {
        public DECK_GEN_REQ() : base((ushort)RMProtocols.DECK_GEN_REQ)
        {
        }
    }

    [ProtoContract]
    public class DECK_NTF : RMPacket
    {
        public DECK_NTF() : base((ushort)RMProtocols.DECK_NTF)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public DECK_DATA DeckData { get; set; }
    }

    [ProtoContract]
    public class INVENTORY_REQ : RMPacket
    {
        public INVENTORY_REQ() : base((ushort)RMProtocols.INVENTORY)
        {
        }

    }

    [ProtoContract]
    public class INVENTORY_ACK : RMPacket
    {
        public INVENTORY_ACK() : base((ushort)RMProtocols.INVENTORY)
        {
        }
    }

    [ProtoContract]
    public class INVENTORY_ITEM_UPDATE : RMPacket
    {
        public enum ItemUpdateType
        {
            None = 0,
            Created,
            Updated,
            Deleted,
        }

        public INVENTORY_ITEM_UPDATE() : base((ushort)RMProtocols.INVENTORY_ITEM_UPDATE_NTF)
        {
        }

        [ProtoMember(1)]
        public ItemUpdateType UpdateType { get; set; }

        [ProtoMember(2)]
        public ITEM_DATA ItemData { get; set; }
    }

    [ProtoContract]
    public class INVENTORY_ITEM_DELETE_REQ : RMPacket
    {
        public INVENTORY_ITEM_DELETE_REQ() : base((ushort)RMProtocols.INVENTORY_ITEM_DELETE)
        {
        }
    }

    [ProtoContract]
    public class INVENTORY_ITEM_DELETE_ACK : RMPacket
    {
        public INVENTORY_ITEM_DELETE_ACK() : base((ushort)RMProtocols.INVENTORY_ITEM_DELETE)
        {
        }
    }

    [ProtoContract]
    public class ITEM_EQUIP : RMPacket
    {
        public ITEM_EQUIP() : base((ushort)RMProtocols.ITEM_EQUIP)
        {
        }

        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public bool IsEquipOrNot { get; set; }

        [ProtoMember(3)]
        public bool Result { get; set; }
    }
}


