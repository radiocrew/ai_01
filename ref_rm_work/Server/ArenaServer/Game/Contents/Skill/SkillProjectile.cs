using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using RM.Net;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class SkillProjectile : Skill
    {
        public SkillProjectile(ResJson_SkillProjectile jsonSkill)
            :base(jsonSkill)
        {
            m_jsonSkillProjecile = jsonSkill;
        }
        public override CollisionBase GetCollision()
        {
            return null;
        }

        public override void DoAction(Arena arena, ArenaObject owner, SkillParam skillParam)
        {
            base.DoAction(arena, owner, skillParam);

            ProjectileParam projectileParam = new ProjectileParam();

            projectileParam.JsonSkill = m_jsonSkillProjecile;
            projectileParam.StartPos = owner.Position;
            projectileParam.TargetPos = skillParam.TargetPosition;
            projectileParam.Direction = skillParam.Direction;
            projectileParam.JogRate = skillParam.JogRate;
            projectileParam.Target = skillParam.Target;

            var projectile = arena.AddProjectile(m_jsonSkillProjecile.ProjectileID, owner, projectileParam);
            if (null != projectile)
            {
                CAST_SKILL_PROJECTILE_NTF packet = new CAST_SKILL_PROJECTILE_NTF();
                packet.OwnerUID = owner.UID;
                packet.SkillID = m_jsonSkill.SkillID;
                packet.MovementData = owner.Movement.SerializeData();
                packet.ProjectileData = projectile.SerializedData();

                arena.BroadCasting(packet);
            }
        }

        public override float SkillRange()
        {
            //return SkillRange(m_jsonSkillAttack.Collision);
            return 0.0f;
        }

        readonly ResJson_SkillProjectile m_jsonSkillProjecile;
    }
}
