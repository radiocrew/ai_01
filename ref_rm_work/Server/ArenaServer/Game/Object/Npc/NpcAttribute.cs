using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class NpcAttribute
    {
        public int Level { get; set; }
        public int AiBagID { get; set; }
        public int SkillBagID { get; set; }
        public int Sight { get; set; }
        public NpcTendency Tendency { get; set; }
        public long Exp { get; set; }
        public float MovementSpeed { get; set; }
        public float AttackSpeed { get; set; }

        public void CopyFrom(ResJson_Npc resNpc)
        {
            Level = resNpc.Level;
            AiBagID = resNpc.AiBagID;
            SkillBagID = resNpc.SkillBagID;
            Sight = resNpc.Sight;
            Tendency = resNpc.Tendency;
            Exp = resNpc.Exp;
            MovementSpeed = resNpc.MovementSpeed;
            AttackSpeed = resNpc.AttackSpeed;
        }
    }
}
