using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.ComTypes;

using RM.Common;
using RM.Net;

namespace rm_login.Tool.Script
{
    public class ResJson_Arena
    {
        public int ArenaID { get; set; }
        public int ArenaType { get; set; }
        public int MapID { get; set; }
    }

    public class ResJson_ArenaObject
    {
        public int ArenaObjectID { get; set; }
    }

    public class ResJson_Player : ResData_ArenaObject
    {
        public PlayerClassType PlayerClassType { get; set; }
    }

    public class ResJson_Npc : ResData_ArenaObject
    {
        public int Level { get; set; }
        public int AiBagID { get; set; }
        public int SkillBagID { get; set; }
        public int Sight { get; set; }
        public NpcTendency Tendency { get; set; }
        public long Exp { get; set; }
        public float MovementSpeed { get; set; }
        public float AttackSpeed { get; set; }
    }

    public class ResJson_NpcSkill
    {
        public int ArenaObjectID { get; set; }
    }

    public class ResJson_Collision
    {
        public int ID { get; set; }
        public ResData_Collision Collision { get; set; }
    }

    public class ResJson_Map
    {
        public int MapID { get; set; }
        public ResData_Size Size { get; set; }
        public ResData_Grid Grid { get; set; }
        public List<int> DeployList { get; set; }

        public int MaxDeployNpcCount { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }

    public class ResJson_Deploy
    {
        public int DeployID { get; set; }
        public ResData_DeployArenaObject ArenaObject { get; set; }
        public ulong StartDelayTime { get; set; }
        public int ArenaObjectCount { get; set; }
        public ulong ArenaObjectCountIntervalTime { get; set; }
        public bool UseInterval { get; set; }
        public ulong IntervalTime { get; set; }
    }

    public class ResJson_Skill
    {
        public int SkillID { get; set; }
        public SkillType SkillType { get; set; }
        public int PenaltyStatusEffectID { get; set; }      // 스킬 발동 속도 패널티
        public float SkillCastPenaltyTime { get; set; }     // 스킬 발동 속도 패널티        
        public float InvokeTime { get; set; }               // 실제 발동되는 딜레이
        public bool CastLookAtTarget { get; set; }          // 스킬 스전시 대상 바라봄
        public bool InvokeLookAtTarget { get; set; }        // 스킬 발동시 대상 바라봄
        public ResData_Damage Damage { get; set; }
    }

    public class ResJson_SkillAttack : ResJson_Skill
    {
        public TargetFilterType TargetFilterType { get; set; }
        public ResData_Collision Collision { get; set; }
    }

    public class ResJson_NpcAiBag
    {
        public int BagID { get; set; }
        public int FirstStateType { get; set; }
        public List<int> StateTypeList { get; set; }
    }

    public class ResJson_Level
    {
        public int Level { get; set; }
        public long AccumulatedExp { get; set; }
        public long LossExpOnDie { get; set; }
        public int ReviveCost { get; set; }
        public int FreeReviveCount { get; set; }
    }

    public class ResJson_ActiveLevel
    {
        public int Level { get; set; }
        public long AccumulatedExp { get; set; }
        public long LossExpOnDie { get; set; }
        public int ReviveCost { get; set; }
        public int FreeReviveCount { get; set; }
    }

    public class ResJson_TestCommand
    {
        public string Text { get; set; }
        public TestCommandType TestCommandType { get; set; }
    }

    public class ResJson_ProjectileConstant
    {
        public int ProjectileID { get; set; }
        public ProjectileType ProjectileType { get; set; }
        public TargetFilterType TargetFilterType { get; set; }
        public float LifeTime { get; set; }
        public float Speed { get; set; }
        public ResData_Collision Collision { get; set; }
    }
}
