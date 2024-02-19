using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaServer.Game.AI
{
    public class StateChase_Test : AiState
    {
        public override AiStateType StateType => AiStateType.Chase_Test;

        public override void Enter()
        {
        }

        public override void Execute(float dt)
        {
        }

        public override void Exit()
        {
        }

        public StateChase_Test(int arg0, int arg1, int arg2, int arg3)
            : base(arg0, arg1, arg2, arg3)
        {
        }
    }
}
