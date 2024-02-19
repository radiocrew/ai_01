using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.ComTypes;

using RM.Common;
using RM.Net;
using RM.Server.Common;

using ArenaServer.Net;
using ArenaServer.Game.AI;

namespace ArenaServer.Resource
{
    public class ResJson_Arena
    {
        public int ArenaID { get; set; }
        public int ArenaType { get; set; }
        public int MapID { get; set; }
    }

    //public class ResJson_ArenaObject
    //{
    //    public int ArenaObjectID { get; set; }
    //}

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

    public class ResJson_NpcSkillBag
    {
        public int ID { get; set; }

        public List<ResData_NpcSkill> NpcSkillList { get; set; }
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

    public class ResJson_SkillAttack : ResData_Skill
    {
        public TargetFilterType TargetFilterType { get; set; }
        public ResData_Collision Collision { get; set; }
    }

    public class ResJson_SkillProjectile : ResData_Skill
    {
        public int ProjectileID { get; set; }
        //public float Range { get; set; }
    }

    public class ResJson_NpcAiState
    {
        public AiStateType StateType { get; set; }
        public int Arg_0 { get; set; }
        public int Arg_1 { get; set; }
        public int Arg_2 { get; set; }
        public int Arg_3 { get; set; }
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

    public class ResJson_PlayerBaseStat
    {
        public PlayerClassType ClassType { get; set; }

        public List<ResData_BaseStat> BaseStat { get; set; }
    }

    public class ResJson_PlayerStat
    {
        public int ID { get; set; }
        public StatType StatType { get; set; }
        public float Value { get; set; }
    }

    public class ResJson_Deck
    {
        public int DeckID { get; set; }
        public DeckType DeckType { get; set; }
        public int PointMax { get; set; }
        public int StatID { get; set; }
        public int StatusEffectID { get; set; }
    }

    public class ResJson_DeckStat
    {
        public int ID { get; set; }
        public Dictionary<int, ResData_Stat> Stats { get; set; }
    }

    public class ResJson_Item
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ItemEquipSlotType EquipSlotType { get; set; }

        public ItemGradeType GradeType { get; set; }

        public int StatID { get; set; }
    }

    public class ResJson_ItemStat
    {
        public int ID { get; set; }
        public List<ResData_Stat> Stats { get; set; }
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

    public class ResJson_ProjectileTarget
    {
        public int ProjectileID { get; set; }
        public ProjectileType ProjectileType { get; set; }
        public TargetFilterType TargetFilterType { get; set; }
        public float Speed { get; set; }
    }








    //─────────────────────────────────────────────────────────────────────────────────────────────────────────
    //
    //          이하 미만 잡..

    /*




    // Player 시작 위치
    public class ResJson_StartingPoint
    {
        public int Index { get; set; }
        //public CCPoint Position { get; set; }
        public float Direction { get; set; }
    }

    // 성장 설정
    public class ResJson_Growth
    {
        public int Exp { get; set; }
        public int Level { get; set; }
        public int RewardDeckSkill { get; set; } // 레벨업 DeckSkill 보상

        public float WaitRespawnTime { get; set; }
    }

    // Path
    public class ResJson_Path
    {
        public int PathID { get; set; }
        public int PathGroupID { get; set; }
        //public List<CCPoint> PathList { get; set; }
    }

    public class ResJson_MinionMap
    {
        //public List<CCPointI> Walls { get; set; }
    }

    // Champ Attribute
    // 레벨 따른 Stat, Health, Movement 설정
    public class ResJson_ChampAttribute
    {
        public int ArenaObjectID { get; set; }
        public int Level { get; set; }
        public ResData_Health Health { get; set; }
        //public STAT_DATA Stat { get; set; }
        public float HPReward { get; set; } // 레벨업 HP보상     
    }

    


    public class ResJson_ExpBox : ResJson_ArenaObject
    {
    }

    public class ResJson_DefenseTower : ResJson_ArenaObject
    {
        public int Interval;
        public float Sight;
        public int SkillID;
        public ResData_BKBoardDefenseTower Blackboard { get; set; }
    }

    public class ResJson_HPTower : ResJson_ArenaObject
    {
        public float Interval;
        public float Sight;
        public float HpRecoveryRate;
    }




    public class ResJson_BonusStat
    {      
        public int BonusStatID { get; set; }
        //public Dictionary<int, STAT_DATA> BonusStats { get; set; }
    }

    #region Skill




    public class ResJson_SkillElement : ResJson_Skill
    {
        public int ElementID { get; set; }
        public float Range { get; set; }
    }

    public class ResJson_SkillTeleport : ResJson_Skill
    {
        public TargetFilterType TargetFilterType { get; set; }
        public ResData_Collision Collision { get; set; }     
    }

    public class ResJson_SkillStatusEffect : ResJson_Skill
    {
        public int DeckID { get; set; }     // 현재 덱으로만 발동
        public int StatusEffectID { get; set; }
    }

    public class ResJson_SkillSpawn : ResJson_Skill
    {
        public int ArenaObjectID { get; set; }     // 현재 덱으로만 발동
    }

    public class ResJson_Deck
    {
        public int DeckID { get; set; }
        //public DeckType DeckType { get; set; }
        public int PointMax { get; set; }
        public int BonusStatID { get; set; }
        public int StatusEffectID { get; set; }

    }

    #endregion Skill


    #region Element
    public class ResJson_Element
    {
        public int ElementID { get; set; }
        //public ElementType ElementType { get; set; }
        //public int DerivedProjectileID { get; set; }
    }

    public class ResJson_ElementAccel : ResJson_Element
    {
        public TargetFilterType TargetFilterType { get; set; }
        public float LifeTime { get; set; }
        public float Factor { get; set; }
        public float Speed { get; set; }
        public ResData_Collision Collision { get; set; }
    }

    public class ResJson_ElementConstant : ResJson_Element
    {
        public TargetFilterType TargetFilterType { get; set; }
        public float LifeTime { get; set; }
        public float Speed { get; set; }
        public ResData_Collision Collision { get; set; }
    }

    public class ResJson_ElementTarget : ResJson_Element
    {
        public float Speed { get; set; }
    }

    public class ResJson_ElementParabola : ResJson_Element
    {
        public TargetFilterType TargetFilterType { get; set; }
        public float Speed { get; set; }
        public ResData_Collision Collision { get; set; } // 대미지 범위
    }
    #endregion Element


    #region Status Effect
    public class ResJson_StatusEffect
    {
        public int StatusEffectID { get; set; }
        public StatusEffectType StatusEffectType { get; set; }
        public float ActionTime { get; set; }
        public TimeStreamLifeType LifeType { get; set; }

        public List<int> ChildList { get; set; }
        //public int BonusStatID { get; set; }
    }

    public class ResJson_StatusEffect_Stat : ResJson_StatusEffect
    {
        public int BonusStatID { get; set; }
    }

    public class ResJson_StatusEffect_Status : ResJson_StatusEffect
    {
        //public List<KeyValuePair<StatusType, float>> StatusList { get; set; }
    }



    #endregion

    public class ResJson_Reward
    {
        public int RewardID { get; set; }
        public List<ResData_RewardData> RewardData { get; set; }
    }

    public class ResJson_RewardFromObject
    { 
        public int ArenaObjectID { get; set; }
        public int RewardID { get; set; }
    }

    public class ResJson_BodyGuard
    { 
        public int OwnDeployID { get; set; }
        public List<int> BodyGuardDepoyID { get; set; }

        public bool NeedAll { get; set; }
    }

    */
}
