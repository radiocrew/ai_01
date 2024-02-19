using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level9.Expedition.Mobile.Util;

using RM.Common;
using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class BaseStatManager : Singleton<BaseStatManager>
    {
        public bool Initialize()
        {
            var playerList = ResourceManager.Instance.JsonPlayer.ToList();
            if (null == playerList)
            {
                return false;
            }

            return playerList.All(element =>
            {
                return Intialize(element.PlayerClassType);
            });
        }

        public Dictionary<StatType, float> GetStats(PlayerClassType playerClassType, int level)
        {
            var levelStats = GetClass(playerClassType);
            if (null == levelStats)
            {
                return null;
            }

            Dictionary<StatType, float> ret = null;
            if (false == levelStats.TryGetValue(level, out ret))
            {
                return null;
            }

            return ret;
        }

        private bool Intialize(PlayerClassType classType)
        {
            var playerClass = GetClass(classType);

            var res = ResourceManager.Instance.FindPlayerStatLevel(classType);
            return res.BaseStat.All(element => {

                Dictionary<StatType, float> stats = new Dictionary<StatType, float>();

                element.StatIDList.All(id => {

                    var stat = ResourceManager.Instance.FindPlayerStat(id);
                    if (null != stat)
                    {
                        stats.Add(stat.StatType, stat.Value);
                        return true;
                    }

                    return false;
                });

                if (false == playerClass.ContainsKey(element.Level))
                {
                    playerClass.Add(element.Level, stats);
                    return true;
                }

                return false;
            });
        }

        private Dictionary<int, Dictionary<StatType, float>> GetClass(PlayerClassType playerClassType)
        {
            switch (playerClassType)
            {
                case PlayerClassType.Rogue:
                    {
                        if (null == m_rogue)
                        {
                            m_rogue = new Dictionary<int, Dictionary<StatType, float>>();
                        }
                        return m_rogue;
                    }
                case PlayerClassType.Warrior:
                    {
                        if (null == m_warrior)
                        {
                            m_warrior = new Dictionary<int, Dictionary<StatType, float>>();
                        }
                        return m_warrior;
                    }
                case PlayerClassType.Mage:
                    {
                        if (null == m_mage)
                        {
                            m_mage = new Dictionary<int, Dictionary<StatType, float>>();
                        }
                        return m_mage;
                    }
                case PlayerClassType.Archer:
                    {
                        if (null == m_archer)
                        {
                            m_archer = new Dictionary<int, Dictionary<StatType, float>>();
                        }
                        return m_archer;
                    }
            }
        
            return null;
        }

        //public void Destory()
        //{
        //    m_rogue.Clear();
        //    m_rogue = null;

        //    m_warrior.Clear();
        //    m_warrior = null;

        //    m_mage.Clear();
        //    m_mage = null;

        //    m_archer.Clear();
        //    m_archer = null;
        //}

        private BaseStatManager()
        {
        }

        Dictionary<int/*level*/, Dictionary<StatType, float>> m_rogue = null;
        Dictionary<int/*level*/, Dictionary<StatType, float>> m_warrior = null;
        Dictionary<int/*level*/, Dictionary<StatType, float>> m_mage = null;
        Dictionary<int/*level*/, Dictionary<StatType, float>> m_archer = null;
    }
}
