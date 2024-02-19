using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Net;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class ProjectileTarget : ProjectileBase, RMCollision
    {
        public ProjectileTarget(Arena arena, ArenaObject owner, ResData_Skill jsonSkill, ResJson_ProjectileTarget jsonProjectile)
            : base(arena, owner, jsonSkill)
        {
            m_jsonProjectile = jsonProjectile;
        }

        public override PROJECTILE_DATA SerializedData()
        {
            return null;
        }

        public CollisionBase GetCollision()
        {
            return null;
        }

        public override void OnUpdate(int id, double accumT, double dT)
        {
            base.OnUpdate(id, accumT, dT);
        }

        readonly ResJson_ProjectileTarget m_jsonProjectile;
    }
}
