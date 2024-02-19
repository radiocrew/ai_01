using System.Collections.Generic;

using UnityEngine;

using RM.Common;

namespace rm_login.Tool.Script
{
    public class ResData_Collision
    {
        public CollisionType CollisionType { get; set; }
        public float Arg_0 { get; set; }
        public float Arg_1 { get; set; }
        public float Arg_2 { get; set; }
    }

    public class ResData_Health//-jinsub, db_player.json에서 세팅하는데. db_player_base_stat.json의 hpmax가 덮어 써버리네?! 흠.. Health를 없애야할듯
        //레벨별로 햇갈리고 초기 health라는 명시가 없으므로 햇갈릴듯 결론 -> db_player.json의 health를 읎앤다. 
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
        public int Level { get; set; }              // 레벨
        public float MovementSpeed { get; set; }    // 이동 속도
        public float AttackSpeed { get; set; }      // 공격 속도
    }

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
}
