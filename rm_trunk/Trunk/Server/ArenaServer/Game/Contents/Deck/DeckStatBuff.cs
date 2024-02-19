using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;

using RM.Common;
using RM.Net;
using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class DeckStatEffect : DeckBase
    {
        //public int Point { get; set; }

        public DeckStatEffect(ResJson_Deck jsonDeck)
            :base(jsonDeck)
        {
            m_jsonDeckStat = ResourceManager.Instance.FindDeckStat(jsonDeck.StatID);
            if (null == m_jsonDeckStat)
            {
                Debug.Assert(false);
            }

            if (null == m_jsonDeckStat.Stats)
            {
                Debug.Assert(false);
            }
        }

        public override void Affect(ref STAT_DATA ret)
        {
            Dictionary<StatType, float> temp = new Dictionary<StatType, float>(ret.Stat);
            ResData_Stat resStat = null;

            if (true == m_jsonDeckStat.Stats.TryGetValue(m_point, out resStat))
            {
                //-그냥.. deck point 에 맞는 stat 을 ret에 복사한 것일 뿐.
                StatManager.ApplyStat(ref temp, resStat.StatType, resStat.Value);

                ret.Stat.Clear();
                ret.Stat = temp;
            }
        }

        ResJson_DeckStat m_jsonDeckStat;
    }
}
