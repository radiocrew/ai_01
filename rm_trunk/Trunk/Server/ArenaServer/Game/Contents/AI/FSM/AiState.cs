using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaServer.Game.AI
{
    public abstract class AiState
    {
        public abstract AiStateType StateType { get; }

        public void Initialize(BlackBoard blackBoard)
        {
            m_blackBoard = blackBoard;
        }

        public bool Interval(float dt, float interval_s)
        {
            m_elapsedTime += dt;

            if (m_elapsedTime < interval_s)
            {
                return false;
            }

            m_elapsedTime = 0.0f;
            return true;
        }

        public abstract void Enter();

        public abstract void Execute(float dt);

        public abstract void Exit();

        public AiState(int arg0, int arg1, int arg2, int arg3)
        {
            arg_0 = arg0;
            arg_1 = arg1;
            arg_2 = arg2;
            arg_3 = arg3;
        }

        protected int arg_0;
        protected int arg_1;
        protected int arg_2;
        protected int arg_3;

        protected BlackBoard m_blackBoard = null;

        float m_elapsedTime = 0.0f;
    }
}