using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rm_login.Tool
{
    public class ToolDefine
    {
        public const int TEST_CAST_ATTACK_SKILL_ID = 10001;
        public const int TEST_CAST_PROJECTILE_SKILL_ID = 40001;

        public const float TEST_OBJECT_RADIUS = 0.5f;           //-testcode, db_collision.json 을 참조해야해.
        public const float TEST_OBJECT_ATTACK_RANGE = 1.0f;     //skill 의 range를 참조해야함. 
        public const int TEST_PLAYER_OBJECT_ID = 11;//-db_player.json 도적

        public enum ObjectType : int
        {
            None = 0,
            Hero = 1,
            Player = 2,
            Npc = 3,
            Projectile = 4,
        }

        public enum UIType : int
        { 
            ArenaIDTextBox = 3,
            MapInfoTextBox = 6,

            MatchingAck = 10,
            MatchingCancel = 11,
            MatchingTimerStop = 12,

            AccountUIDTextBox = 30,
            PlayerUIDTextBox = 31,

            PlayerX = 50,
            PlayerY = 51,
            PlayerZ = 52,

            PlayerHp = 100,
            PlayerHpMax,
            PlayerHpMaxAdd,
            PlayerMp,
            PlayerMpMax,
            PlayerLevel,
            PlayerExp,
            PlayerActiveLevel,
            PlayerActiveExp,
           
            PlayerAtk = 200,
            PlayerAtkSpdRate = 201,
            PlayerAtkRate,
            PlayerAtkAdd,
            PlayerVampireRate,
            PlayerVigorRecoveryRate,
            PlayerExpAddRate,

            DeckPoint = 500,
            Deck1 = 501,
            Deck2 = 502,
            Deck3 = 503,
            ActiveDeckList = 504,

            Inventory = 550,
            InventoryItemRemove = 551,
            InventoryItemEquip = 552,
            InventoryItemUnEquip = 553,

            TestCommand = 570,

            PlayerClass = 580,
            PlayerName = 581,
        }
    }
}
