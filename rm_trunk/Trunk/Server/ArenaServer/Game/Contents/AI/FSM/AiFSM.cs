using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ArenaServer.Game.AI
{
    public class AiFSM
    {
        public float IdleTime { get; set; }

        public AiFSM()
        {
        }

        public void AddState(AiStateType type, AiState state)
        {
            if (false == m_aiStates.ContainsKey(type))
            {
                m_aiStates.Add(type, state);
                return;
            }

            Debug.Assert(false);
        }

        public void ChangeState(AiStateType stateType)
        {
            lock (m_lock)
            {
                AiState changeState = null;
                if (false == m_aiStates.TryGetValue(stateType, out changeState))
                {
                    Debug.Assert(false);
                    return;
                }

                //-first step
                if (null == m_curState)
                {
                    m_curState = changeState;
                    m_curState.Enter();
                    return;
                }

                if (m_curState == changeState)
                {
                    return;
                }

                m_curState.Exit();
                m_prvState = m_curState;

                m_curState = changeState;
                m_curState.Enter();
            }
        }

        public virtual void UpdateStateMachine(float dt)
        {
            lock (m_lock)
            {
                if (null != m_curState)
                {
                    m_curState.Execute(dt);
                }
            }
        }


        object m_lock = new object();

        protected Dictionary<AiStateType, AiState> m_aiStates = new Dictionary<AiStateType, AiState>();

        protected AiState m_curState;
        protected AiState m_prvState;
    }
}
