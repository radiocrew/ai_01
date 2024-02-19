
namespace RM.Server.Common
{
    public class DEFINE
    {
        //-commercial
        public const int    True = 1;
        public const int    False = 0;

        public const int    MAX_TIMER_QUEUE_SIZE = 1000000;//sim que overflow 대응은?
        public const int    MAX_TIMER_WORKER_THREAD_COUNT = 4;

        public const uint   TIME_ARENAOBJECT_UPDATE_INTERVAL_MS = 100;
        public const uint   TIME_ARENA_UPDATE_INTERVAL_MS = 1000;
        public const uint   TIME_ARENA_MANAGER_UPDATE_INTERVAL_MS = 1000;
        public const uint   TIME_PROJECTILE_UPDATE_INTERVAL_MS = 100;

        public const int    TIME_PlAYER_HEARTBEAT_INTERVAL_MS = 1000;
        public const int    TIME_PLAYER_HEARTBEAT_WAIT_S = 10 * 1000;
        public const int    TIME_WAIT_EMPTY_ARENA_S = 5000;

        public const int    MAX_GENERATE_DECK_COUNT = 3;

        public const float  SKILL_SPARE_DISTANCE = 0.2f;



        //-for test
        public const int    TEST_FIRST_ARENA_ID = 1;
        public const int    TEST_PLAYER_LEVEL = 1;
        public const long   TEST_PLAYER_EXP = 1;
        public const int    TEST_RAID_PLAYER_COUNT = 2;
        public const int    TEST_AUTO_SKILL_ID = 10001;
        public const int    TEST_TIME_AUTO_ATTACK_INTERVAL_MS = 1000;

        public const double TEST_SLIME_SEARCHING_INTERVAL_MS = 1000;
        public const float  TEST_SLIME_ATTACK_SPEED_S = 1f;
        public const float  TEST_SLIME_WANDER_INTERVAL_S = 0.2f;
        public const float  TEST_SLIME_CHASE_INTERVAL_S = 0.2f;

        public const int    TEST_NPC_WANDER_BOUDARY = 15;
    }
}


//namespace ArenaServer.Resource
//{
//    public class ConstValue
//    {
//        public const int DefaultLevel = 1;
//        public const int DeckSkillPointMax = 20;

//        // Stat Max
//        public const float AttackSpeedMax = 2.5f;
//        public const float MoveSpeedMax = 500.0f;
//        public const float CoolTime = 2.5f;
//        public const float VampireRate = 2.5f;
//        public const float HPRecover = 2.5f;


//        // db_growth.json
//        #region Growth
//        static public int ExpMax { get; set; }        
//        static public int LevelMax { get; set; }
//        #endregion Growth
//    }
//}
