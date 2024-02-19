using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class ProjectileParam
    {
        public ResJson_SkillProjectile JsonSkill { get; set; }
        public Vector2 StartPos { get; set; }
        public Vector2 TargetPos { get; set; }
        public float Direction { get; set; }
        public float JogRate { get; set; }
        public ArenaObject Target { get; set; }

    }
}
