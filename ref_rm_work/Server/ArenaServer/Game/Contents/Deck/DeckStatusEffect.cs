using System;
using System.Collections.Generic;

using RM.Net;
using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class DeckStatusEffect : DeckBase
    {
        public DeckStatusEffect(ResJson_Deck jsonDeck)
            :base(jsonDeck)
        {
        }

        public override void Affect(ref STAT_DATA statData)
        {            
        }

        public override void Use()
        {
            m_arenaObject.StatusEffect.Add(m_jsonDeck.StatusEffectID);//, m_point);
        }
    }
}
