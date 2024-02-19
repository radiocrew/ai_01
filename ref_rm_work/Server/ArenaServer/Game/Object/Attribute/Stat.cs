using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;
using RM.Net;

namespace ArenaServer.Game
{
    public class Stat
    {
        public bool Initialize()
        {
            m_finalStat = new Dictionary<StatType, float>();
            m_baseStat = new Dictionary<StatType, float>();
            m_bonusStat = new Dictionary<StatBonusType, Dictionary<StatType, float>>();

            foreach (var e in Enum.GetValues(typeof(StatBonusType)).Cast<StatBonusType>())
            {
                if (StatBonusType.None != e)
                {
                    m_bonusStat.Add(e, new Dictionary<StatType, float>());
                }
            }

            return true;
        }

        public void Destroy()
        {
            lock (m_lock)
            {
                m_finalStat.Clear();
                m_finalStat = null;

                m_baseStat.Clear();
                m_baseStat = null;

                m_bonusStat.All(pair => {
                    pair.Value.Clear();
                    return true;
                });

                m_bonusStat.Clear();
                m_bonusStat = null;
            }
        }

        public void AddOrUpdate(StatType type, float value, StatBonusType bonusType = StatBonusType.None)
        {
            lock (m_lock)
            {
                var stats = Find(bonusType);
                if (null != stats)
                {
                    float old = 0.0f;
                    stats.TryGetValue(type, out old);
                    stats[type] = value;

                    //Build();
                    return;
                }
            }
        }

        public bool GetValue(StatType type, out float value)
        {
            value = 0.0f;

            lock (m_lock)
            {
                return m_finalStat.TryGetValue(type, out value);
            }
        }

        public void Clear(StatBonusType bonusType = StatBonusType.None)
        {
            lock (m_lock)
            {
                var stats = Find(bonusType);
                if (null != stats)
                {
                    stats.Clear();
                }
            }
        }

        public void Calculate(Player player, bool ntf = false)
        {
            StatManager.CalculateStat(player, ntf);
        }

        public void Build()
        {
            lock (m_lock)
            {
                m_finalStat.Clear();

                StatManager.ApplyStat(ref m_finalStat, m_baseStat);

                m_bonusStat.All(pair =>
                {
                    StatManager.ApplyStat(ref m_finalStat, pair.Value);
                    return true;
                });
            }
        }

        public STAT_DATA SerializeData()
        {
            STAT_DATA data = new STAT_DATA();

            lock (m_lock)
            {
                data.Stat = new Dictionary<StatType, float>(m_finalStat);
            }

            return data;
        }

        private Dictionary<StatType, float> Find(StatBonusType type)
        {
            if (type == StatBonusType.None)
            {
                return m_baseStat;
            }
            else
            {
                Dictionary<StatType, float> exist = null;
                if (true == m_bonusStat.TryGetValue(type, out exist))
                {
                    return exist;
                }

                return null;
            }
        }

        object m_lock = new object();

        Dictionary<StatType, float> m_finalStat = null;
        Dictionary<StatType, float> m_baseStat = null;//-character level 에 따라는 변하지 않는 스탯
        Dictionary<StatBonusType, Dictionary<StatType, float>> m_bonusStat = null;
    }
}
