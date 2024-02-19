using System;

namespace RM.Common
{
    // 클라 서버 공용 상수
    public class CONST_DEFINE
    {
        public const int TEST_MAX_PLAYER_LEVEL = 5;
        public const int TEST_MAX_PLAYER_ACTIVE_LEVEL = 5;

        public const int DeckPointMax = 20;

        // Stat Max
        public const int ALevel = 20;
        public const int WLevel = 20;
        public const int RLevel = 20;
        public const float MoveSpeedMax = 500.0f;
        public const float CoolTime = 1.0f;    // 0.1f = 10% 빠르게
        public const float VampireRate = 2.0f; // 대미지의 두배
        public const float HPRecover = 0.5f;   // 체력Max의 10%
        public const float BonusExpRate = 1.0f; // 100% 추가
    }

    public enum TestCommandType : ushort
    {
        None = 0,
        ActiveExp,
        DeckPointUp,
        DeckGenerate,
        PlayerSuicide,

        //-이하, db_test_command.json 참조
        GetItem = 50,
        DelItem = 51,
        EquipItem = 52,
        CreateNpc = 53,
        Login = 54,
        Logout = 55,
        ShutDown = 56,
        AddExp = 57,
        CastSkill = 58,
        Move = 59,
    }

    // Script Class에 따라 분류
    public enum ArenaObjectType : byte
    {
        None = 0,
        Player,
        Npc,
        Item,

        //기믹 타입
        //루팅한다면 아이템
    }

    public enum PlayerClassType : byte
    {
        None = 0,
        Rogue = 1,              // 영웅:도적
        Warrior = 2,            // 영웅:전사
        Mage = 3,               // 영웅:마법사
        Archer = 4,             // 영웅:궁수
    }

    public enum StatType : int
    {
        None = 0,

        //-overwrite
        //  고정값, 덮어 쓴다.
        HPMax = 2010,
        Attack = 2020,


        //-add
        HPMaxAdd = 2200,//-test
        AttackAdd = 2201,//-test


        //-rate
        //  rate 는 값을 퍼센테이지를 기준으로 누적으로 해야 합산했을 때, 계산이 편하다.
        //  ex) 신발에서 이속 4%(4) + 귀거리에서 이속 1%(1) -> 1 + 4 = 5(5%)
        AttackSpeedRate = 3020,     //-공속율 (1.0f => 기본, 2.0f => 2배 속도 증가)
        AttackRate = 3021,           //-test
        VampireRate = 3030,         //-흡혈 (1.0f => 기본, 1.2f => 120% 흡수)
        VigorRecoveryRate = 3040,   //-기력회복율 (1.0f => 기본, 1.2f => 120% 회복 증가)
        ExpAddRate = 3050,          //-경험치 획들율 (1.0f => 기본, 1.2f => 120% 획득 증가)
    }

    public enum StatMaxValue
    {

    }

    public enum StatBonusType
    {
        None = 0,
        Deck = 1,
        Item = 2,
    }

    public enum CollisionType : byte
    {
        None = 0,
        Circle = 1,
        Arc = 2,
        Rect = 3,
        Line = 4
    }

    public enum TargetFilterType : byte
    {
        NearSingle = 1,     // 가장 가까운 첫번째 단일
        Multi = 2,          // 모든
    }

    public enum SkillType : byte
    {
        None = 0,
        SkillAttack = 1,           // melee attack, (default)
        SkillProjectile = 2,
    }

    // 클라이언트 Hero Ani 상태 동기화
    public enum ObjectState : byte
    {
        Idle = 0,
        Run = 1,
        Die = 2,
    }

    public enum StatusType : byte
    {
        None = 0,
        Dead = 1,
        //InBush = 2,
        //Invincible = 3,
        //Invisible = 4,
        //CantUseSkill = 5,
        //Stiffness = 6,
        //SkillCastPenalty = 7, // 스킬Cast 후 Speed 패널티(스킬x)
        //Gathering = 8,

    }

    public enum NpcTendency
    {
        None = 0,
        Passive,
        Aggressive,
    }

    public enum NpcAttribute : byte
    {
        None = 0,
    }

    public enum DeckType
    {
        None = 0,
        StatEffect = 1,      // 스탯 효과
        StatusEffect = 2,    // 상태 효과
        Skill = 3,           // 스킬 사용가능
    }

    public enum ItemGradeType
    {
        None = 0,
        Normal = 1,
        High = 2,
        Rare = 3,
        Hero = 4,
        Legend = 5,
    }

    public enum ItemEquipSlotType
    {
        None = 0,
        Weapon = 1,
        Armor = 2,
        Shoe = 3,
        Ring = 4,
    }

    public enum ProjectileType
    {
        None = 0,
        Constant = 1,   // 등속 운동
        Target = 2,     // 고정 타겟
        Accel = 3,      // 가속 성질
        Parabola = 4,   // 포물선
    }

}
