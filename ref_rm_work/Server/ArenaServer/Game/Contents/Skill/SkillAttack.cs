using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class SkillAttack : Skill
    {
        public SkillAttack(ResJson_SkillAttack jsonSkillAttack)
            :base(jsonSkillAttack)
        {
            m_jsonSkillAttack = jsonSkillAttack;
            m_collision = CollisionBase.CreateCollision(jsonSkillAttack.Collision);
        }

        public override CollisionBase GetCollision()
        {
            return m_collision;
        }

        public override void DoAction(Arena arena, ArenaObject owner, SkillParam skillParam)
        {
            base.DoAction(arena, owner, skillParam);

            m_collision.Position = owner.Position;
            m_collision.Direction = owner.Direction;
            
            var arenaObjects = arena.ObjectList.GetObjects();

            var targets = TargetFilter.GetTargets(arena, owner.Position, m_jsonSkillAttack.TargetFilterType,

                (targetObject) => {
                    if (true == owner.IsAttackable(targetObject))
                    {
                        return CollisionBase.Collision(this, targetObject);
                    }

                    return false;
                });

            if (null != targets)
            {
                foreach (var target in targets)
                {
                    //FormulaManager.ApplyDamage(owner, target, m_jsonSkill);
                    var dmg = FormulaManager.CalcDamage(owner, m_jsonSkill);
                    target.TakeDamage(dmg, owner, m_jsonSkill.SkillID);
                }
            }
        }

        public override float SkillRange()
        {
            return SkillRange(m_jsonSkillAttack.Collision);
        }

        ResJson_SkillAttack m_jsonSkillAttack = null;
        CollisionBase m_collision = null;
    }
}
