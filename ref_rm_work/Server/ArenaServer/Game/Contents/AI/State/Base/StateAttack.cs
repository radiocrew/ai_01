using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Server.Common;

namespace ArenaServer.Game.AI
{
    public class StateAttack : AiState
    {
        public override AiStateType StateType => AiStateType.Attack;

        public override void Enter()
        {
        }

        public override void Execute(float dt)
        {
            if (false == Interval(dt, DEFINE.TEST_SLIME_ATTACK_SPEED_S))
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

            if (true == targetObject.IsDead())
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

            if ((Math.Truncate(d1 * 100) / 100) < (Math.Truncate(d2 * 100) / 100))
            {
                owner.ChangeAiState(AiStateType.Chase);
                return;
            }

            owner.Look(targetObject.Position);

            int skillID = m_blackBoard.SkillSelector.GetSkillID();
            //Console.WriteLine("cast skill[{0}]", skillID);
            owner.CastSkill(skillID, targetObject);
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

        public StateAttack(int arg0, int arg1, int arg2, int arg3)
            : base(arg0, arg1, arg2, arg3)
        {
        }
    }
}
