using System.Collections.Generic;

using UnityEngine;

using RM.Common;
using RM.Server.Common;
using ArenaServer.Net;

namespace ArenaServer.Resource
{
    public class ResData_Collision
    {
        public CollisionType CollisionType { get; set; }
        public float Arg_0 { get; set; }
        public float Arg_1 { get; set; }
        public float Arg_2 { get; set; }
    }

    public class ResData_Health
    {
        public int Hp { get; set; }
        public int HpMax { get; set; }
    }

    public class ResData_ArenaObject
    {
        public int ArenaObjectID { get; set; }

        public int CollisionID { get; set; }

        public ResData_Health Health { get; set; }
    }

    public class ResData_Stat
    {
        public StatType StatType { get; set; }
        public float Value { get; set; }
    }


    //public class ResData_StatList
    //{
    //    public List<ResData_Stat> 
    //    //public int Level { get; set; }              // 레벨
    //    //public float MovementSpeed { get; set; }    // 이동 속도
    //    //public float AttackSpeed { get; set; }      // 공격 속도
    //}

    public class ResData_Damage
    {
        public int DamageMin { get; set; }
        public int DamageMax { get; set; }
    }

    public class ResData_Size
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class ResData_Grid
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class ResData_DeployArenaObject
    {
        public int ID { get; set; }
        public Vector2 Position { get; set; }
        public float Direction { get; set; }
    }

    public class ResData_BaseStat
    {
        public int Level { get; set; }
        public List<int> StatIDList { get; set; }
    }

    public class ResData_Skill
    {
        public int SkillID { get; set; }
        public SkillType SkillType { get; set; }
        public int PenaltyStatusEffectID { get; set; }      // 스킬 발동 속도 패널티
        public float SkillCastPenaltyTime { get; set; }     // 스킬 발동 속도 패널티        
        public ulong InvokeTimeMS { get; set; }               // 실제 발동되는 딜레이
        public bool CastLookAtTarget { get; set; }          // 스킬 스전시 대상 바라봄
        public bool InvokeLookAtTarget { get; set; }        // 스킬 발동시 대상 바라봄
        public ResData_Damage Damage { get; set; }
    }

    public class ResData_NpcSkill
    {
        public int SkillID { get; set; }
        public float CoolTime { get; set; }
    }

    public class ResData_Projectile
    { 
        public int ProjectileID { get; set; }
        public ProjectileType ProjectileType { get; set; }

        public ResData_Projectile(int id, ProjectileType type)
        {
            ProjectileID = id;
            ProjectileType = type;
        }
    }




    //─────────────────────────────────────────────────────────────────────────────────────────────────────────
    //
    //          이하 미만 잡..


    //public class ResData_Stat
    //{
    //    //public int Level { get; set; }              // 레벨
    //    public float MovementSpeed { get; set; }    // 이동 속도
    //    public float AttackSpeed { get; set; }      // 공격 속도
    //}

    /*


    public class ResData_BKBoardMinion
    {
        public float PathBoundary { get; set; } // Radius로 사용
        public int SkillID { get; set; }
        public float AttackSpeed { get; set; }
        public float AttackRange { get; set; }
        public int AttackRangeRandom { get; set; } // 0~9  (1=10% 짧아짐)
        public float ChaseTimeout { get; set; }
    }

    public class ResData_BKBoardMonster
    {
        public List<ResData_MonsterSkill> MonsterSkill { get; set; }
    }

    public class ResData_BKBoardDefenseTower
    {
        //public List<CCPoint> PathList { get; set; }

        public float AttackSpeed { get; set; }
        public float Interval { get; set; }
        public float Sight { get; set; }
        public int SkillID { get; set; }
    }

    public class ResData_BKBoardDuck
    {
        //public List<CCPoint> PathList { get; set; }

        public float AttackSpeed { get; set; }
        public float Interval { get; set; }
        public float Sight { get; set; }
        public int SkillID { get; set; }
    }

    public class ResData_MonsterSkill
    {
        public int ID { get; set; }
        public float CoolTime { get; set; }

        public float AttackRange { get; set; }
    }


    //public class ResData_Exp
    //{
    //    public ExpRewardType ExpRewardType { get; set; }
    //    public int ExpValue { get; set; }

    //}

    public class ResData_RewardData
    { 
        public RewardWho RewardWho { get; set; }
        public RewardWhat RewardWhat { get; set; }
        public int RewardValue { get; set; }
        public RewardHow RewardHow { get; set; }
    }
*/
}
