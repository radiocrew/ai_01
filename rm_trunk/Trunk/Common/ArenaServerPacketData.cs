using System;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using RM.Common;

namespace RM.Net
{
    [ProtoContract]
    public struct NVECTOR3
    {
        [ProtoMember(1)]
        public float X { get; set; }

        [ProtoMember(2)]
        public float Y { get; set; }

        [ProtoMember(3)]
        public float Z { get; set; }

        public NVECTOR3(Vector3 vec)
        {
            X = vec.x;
            Y = vec.y;
            Z = vec.z;
        }

        public NVECTOR3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        //public void Normalize()
        //{
        //    var ret = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
        //    X /= (float)ret;
        //    Y /= (float)ret;
        //    Z /= (float)ret;
        //}

        //public static float Dot(NVECTOR3 v1, NVECTOR3 v2)
        //{
        //    float theta = 0.0f;
        //    float degree = 0.0f;

        //    theta = (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z + v2.Z);
        //    theta = (float)Math.Acos(theta);

        //    degree = theta * (180.0f / (float)Math.PI);

        //    return degree;
        //}

        // implicit 암시적 (위험)
        // explicit 명시적
        public static explicit operator Vector3(NVECTOR3 vec) => new Vector3(vec.X, vec.Y, vec.Z);
        public static explicit operator NVECTOR3(Vector3 vec) => new NVECTOR3(vec);

        //-for server
        public static explicit operator NVECTOR3(Vector2 vec) => new NVECTOR3(vec.x, 0, vec.y);
    }

    [ProtoContract]
    public class MOVEMENT_DATA
    {
        [ProtoMember(1)]
        public NVECTOR3 Position { get; set; }

        [ProtoMember(2)]
        public float Direction { get; set; }    // degree
    }

    [ProtoContract]
    public class HEALTH_DATA
    {
        [ProtoMember(1)]
        public int Hp { get; set; }

        [ProtoMember(2)]
        public int HpMax { get; set; }

        [ProtoMember(3)]
        public int Mp { get; set; }

        [ProtoMember(4)]
        public int MpMax { get; set; }

        [ProtoMember(5)]
        public int Level { get; set; }

        [ProtoMember(6)]
        public long Exp { get; set; }
    }

    [ProtoContract]
    public class ACTIVE_LEVEL
    {
        [ProtoMember(1)]
        public int Level { get; set; }

        [ProtoMember(2)]
        public long Exp { get; set; }
    }


    [ProtoContract]
    public class STATUS_DATA
    {
        [ProtoMember(1)]
        public Dictionary<StatusType, float> Status { get; set; }
    }

    [ProtoContract]
    public class STAT_DATA
    {
        [ProtoMember(1)]
        public Dictionary<StatType, float> Stat { get; set; }

        /*
        [ProtoMember(2)]
        public int ALevel { get; set; }         // A공격 강화

        [ProtoMember(3)]
        public int WLevel { get; set; }         // W스킬 강화

        [ProtoMember(4)]
        public int RLevel { get; set; }         // R스킬 강화

        [ProtoMember(5)]
        public float MovementSpeed { get; set; }  // 이속ㅠ

        [ProtoMember(6)]
        public float CoolTime { get; set; }       // 재사용시간 대기시간 (WR에만 적용, 0.1f = 10% 빠르게)

        [ProtoMember(7)]
        public float VampireRate { get; set; }    // 흡혈 (1.0f => 대미지 100% 흡수)

        [ProtoMember(8)]
        public float HPRecover { get; set; }      // HP 자연회복 (0.1f 체력Max의 10%)

        [ProtoMember(9)]
        public float BonusExpRate { get; set; }   // 보너스 추가 경험치 (1.0f 100% 추가)
        */
    }

    [ProtoContract]
    [ProtoInclude(10, typeof(HERO_DATA))]
    public class ARENA_OBJECT_DATA
    {
        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)] // Json ID
        public int ArenaObjectID { get; set; }

        [ProtoMember(3)]
        public MOVEMENT_DATA Movement { get; set; }

        [ProtoMember(4)]
        public HEALTH_DATA Health { get; set; }

        [ProtoMember(5)]
        public STATUS_DATA Status { get; set; }

        [ProtoMember(6)]
        public STAT_DATA Stat { get; set; }
    }

    [ProtoContract]
    public class HERO_DATA : ARENA_OBJECT_DATA
    {
        [ProtoMember(1)]
        public ACTIVE_LEVEL ActiveLevel { get; set; }
    }

    [ProtoContract]
    public class SKILL_PARAM_DATA
    {
        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public NVECTOR3 CastPosition { get; set; }

        [ProtoMember(3)]
        public NVECTOR3 TargetPosition { get; set; }

        [ProtoMember(4)]
        public float Direction { get; set; }

        [ProtoMember(5)]
        public float JogRate { get; set; }
    }

    [ProtoContract]
    public class ARENA_DEPATURE//-아레나 이동시, 사용자가 출발하는 출발지의 정보,
    {
        [ProtoMember(1)]
        public string IP { get; set; }

        [ProtoMember(2)]
        public int Port { get; set; }

        [ProtoMember(3)]
        public int ServerID { get; set; }

        [ProtoMember(4)]
        public Guid PlayerUID { get; set; }
    }

    [ProtoContract]
    public class ARENA_ARRIVAL//-아레나 이동시, 사용자가 도작하는 도착지의 정보,
    {
        [ProtoMember(1)]
        public string IP { get; set; }

        [ProtoMember(2)]
        public int Port { get; set; }

        [ProtoMember(3)]
        public int ServerID { get; set; }

        [ProtoMember(4)]
        public Guid ArenaKey { get; set; }
    }

    [ProtoContract]
    public class DECK
    {
        [ProtoMember(1)]
        public int DeckID { get; set; }

        [ProtoMember(2)]
        public DeckType DeckType { get; set; }

        [ProtoMember(3)]
        public int Point { get; set; }

        [ProtoMember(4)]
        public int PointMax { get; set; }

    }

    [ProtoContract]
    public class DECK_DATA
    {
        [ProtoMember(1)]
        public int DeckGenPoint { get; set; }  // Deck Gen Point

        [ProtoMember(2)]
        public List<int> SelectableDeck { get; set; }

        [ProtoMember(3)]
        public Dictionary<int, DECK> ActiveDeck { get; set; }
    }

    [ProtoContract]
    public class ITEM_DATA
    {
        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public int ItemID { get; set; }

        [ProtoMember(3)]
        public Guid OwnerUID { get; set; }

        [ProtoMember(4)]
        public int ItemLevel { get; set; }

        [ProtoMember(5)]
        public ulong CreateDate { get; set; }

        /*
         * message ItemData
{
	optional LUID			luid							=	1;	// Item GUID
	optional LUID			owner_luid						=	2;	// account / blood pledge / character / ...(InventoryType 에 따라)
	optional InventoryType	inventory						=	3;	//
	optional int32			inventory_order					=	4[default=0];	// ascending or descending ?

	optional int32			classid							=	5;	// type id
	optional int64			amount							=	6;	// bigint
	optional int64			create_date						=	7;	// time64_t
	optional int64			delete_date						=	8;	// time64_t

	optional int32			enchant							=	9;	// tinyint
	optional bool			eroded							=	10;	// bit
	optional int32			bless							=	11;	// tinyint

	optional bool			is_identified					=	12;
	optional bool			is_wished						=	13;
	optional bool			is_sealed						=	14;

	optional int64			transfer_date					=	15;
	optional int64			restoration_limit_date			=	33;

	optional bool			is_trade_locked					=	16[default=true] ;

	optional ItemExtendData		extend_data					=	31;
	optional ItemDynamicOption	dynamic_option				=	32;

	// Npc 드롭 아이템 전용 설정
	optional int32			required_quest					=	17; // 퀘스트 가진 경우에만 주는 아이템
	optional int32			to_all							=	18;	// 모든 공격자에 주는 아이템

	optional int64			enable_date						=	22;

	optional int32			remote_drop_range				=   23;
	optional bool           ignore_looting_rights			=   24;
	optional int32			no_looting_in_milliseconds		=	25;

    optional SocketStoneIDList    socket_stone_ids          =   26;
}
          */
    }

    [ProtoContract]
    public class PROJECTILE_DATA
    {
        [ProtoMember(1)]
        public Guid UID { get; set; }

        [ProtoMember(2)]
        public int ProjectileID { get; set; }

        [ProtoMember(3)]
        public NVECTOR3 StartPos { get; set; }

        [ProtoMember(4)]
        public NVECTOR3 TargetPos { get; set; }

        [ProtoMember(5)]
        public float Direction { get; set; }

        [ProtoMember(6)]
        public Guid TargetUID { get; set; }
    }
}


