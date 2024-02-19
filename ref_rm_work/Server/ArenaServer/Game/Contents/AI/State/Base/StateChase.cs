using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Server.Common;

namespace ArenaServer.Game.AI
{
    public class StateChase : AiState
    {
        public override AiStateType StateType => AiStateType.Chase;

        public override void Enter()
        {
        }

        public override void Execute(float dt)
        {
            if (false == Interval(dt, DEFINE.TEST_SLIME_CHASE_INTERVAL_S))
            {
                return;
            }

            var owner = m_blackBoard.Owner;
            var targetObject = m_blackBoard.TargetObject;

            if (null == targetObject)
            {
                Reset(owner);
                return;
            }

            var ownerSight = owner.Sight;

            var d2 = Vector2.Distance(owner.Position, targetObject.Position);
            d2 -= targetObject.ObjectRadius;
            if (ownerSight < d2)
            {
                Reset(owner);
                return;
            }

            var skill = SkillManager.Instance.FindSkill(m_blackBoard.SkillSelector.GetSkillID());
            if (null == skill)
            {
                Reset(owner);
                return;
            }

            var d1 = skill.SkillRange();
            d1 -= DEFINE.SKILL_SPARE_DISTANCE;

            if ((Math.Truncate(d1 * 100) / 100) >= (Math.Truncate(d2 * 100) / 100))
            {
                owner.ChangeAiState(AiStateType.Attack);
                return;
            }

            Vector2 attackPos = AttackPosition(owner, targetObject, d1);
            if (false == owner.Move(attackPos))
            {
                Console.WriteLine("Can't move when attack. : {0} UID{1}", System.DateTime.Now.ToString("hh:mm:ss.fff"), owner.UID.ToString());
            }
        }

        public override void Exit()
        {
        }

        private void Reset(ArenaObject owner)
        {
            owner.ChangeAiState(AiStateType.Wander);
            m_blackBoard.TargetObject = null;
            m_blackBoard.TargetPosition = owner.Position;
        }

        private Vector2 AttackPosition(ArenaObject owner, ArenaObject target, float attackDistance)
        {
            if (attackDistance < Vector2.Distance(owner.Position, target.Position))
            {
                Vector2 v = target.Position - owner.Position;
                v.Normalize();
                v *= (attackDistance + target.ObjectRadius);

                return target.Position - v;
            }

            return owner.Position;
        }

        public StateChase(int arg0, int arg1, int arg2, int arg3)
            : base(arg0, arg1, arg2, arg3)
        {
        }
    }
}
