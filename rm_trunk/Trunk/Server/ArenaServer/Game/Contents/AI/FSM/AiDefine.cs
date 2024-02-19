using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaServer.Game.AI
{
    public enum AiStateType
    {
        None = 0,

        Idle,
        Wander,
        Chase,
        Attack,

        Chase_Test,
    };
}
