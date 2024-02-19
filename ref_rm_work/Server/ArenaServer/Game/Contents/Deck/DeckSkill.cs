using System;
using System.Collections.Generic;

using RM.Net;
using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public  class DeckSkillOpen : DeckBase
    {
        public DeckSkillOpen(ResJson_Deck jsonDeckSkill)
            :base(jsonDeckSkill)
        {
        }

        public override void Affect(ref STAT_DATA statData)
        {
            // do nothing
        }
    }
}
