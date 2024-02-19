using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Server.Common;

namespace ArenaServer.Game.AI
{
    public class StateWander : AiState
    {
        public override AiStateType StateType => AiStateType.Wander;

        public override void Enter()
        {
        }

        public override void Execute(float dt)
        {
            if (false == Interval(dt, DEFINE.TEST_SLIME_WANDER_INTERVAL_S))
            {
                return;
            }

            var owner = m_blackBoard.Owner;
            var arena = owner.Arena;
            var targetPos = m_blackBoard.TargetPosition;

            Vector2 curPos = owner.Position;

            if (((0 == targetPos.x) && (0 == targetPos.y))
            || (true == Arrival(curPos, targetPos))
            || (false == owner.Move(m_blackBoard.TargetPosition)))
            {
                m_blackBoard.TargetPosition = NewTargetPosition(arena.Map.Width, arena.Map.Height, DEFINE.TEST_NPC_WANDER_BOUDARY, curPos);
            }

            if (true == HasTarget())
            {
                owner.ChangeAiState(AiStateType.Chase);
                m_blackBoard.TargetPositionClear();
            }
        }

        public override void Exit()
        {
        }

        public StateWander(int arg0, int arg1, int arg2, int arg3)
            : base(arg0, arg1, arg2, arg3)
        {
        }

        private bool Arrival(Vector2 cur, Vector2 end)
        {
            if (((end.x + 1) > cur.x) && (cur.x > (end.x - 1)))
            {
                return true;
            }

            return false;
        }

        private Vector2 NewTargetPosition(int width, int height, int boundary, Vector2 curPos)
        {
            Vector2 ret = new Vector2(curPos.x, curPos.y);

            System.Random rand = new System.Random(Guid.NewGuid().GetHashCode());
            ret.x += rand.Next(-boundary, boundary);
            ret.y += rand.Next(-boundary, boundary);

            if (0 > ret.x)
            {
                ret.x = 0;
            }
            if (width <= ret.x)
            {
                ret.x = width - 1;
            }
            if (0 > ret.y)
            {
                ret.y = 0;
            }
            if (height <=ret.y)
            {
                ret.y = height - 1;
            }

            return ret;
        }

        private bool HasTarget()
        {
            var targetObject = m_blackBoard.TargetObject;
            if ((null != targetObject) && (false == targetObject.IsDead()))
            {
                return true;
            }

            return false;
        }
    }
}
