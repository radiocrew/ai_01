using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaServer.Game.AI
{
    public class StateIdle : AiState
    {
        public override AiStateType StateType => AiStateType.Idle;

        public override void Enter()
        {
        }

        public override void Execute(float dt)
        {
            //m_testTime += (float)dt;
            //if (1 < m_testTime)
            //{
            //    Console.WriteLine("ai idling : {0}, T{1}", System.DateTime.Now.ToString("hh:mm:ss.fff"), Environment.TickCount);
            //    m_testTime = 0.0f;
            //}
        }

        public override void Exit()
        {
        }

        public StateIdle(int arg0, int arg1, int arg2, int arg3)
            :   base(arg0, arg1, arg2, arg3)
        {
        }

        //volatile float m_testTime = 0.0f;//-testcode,
    }
}
