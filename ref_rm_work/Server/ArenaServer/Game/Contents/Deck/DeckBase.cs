using System;
using System.Threading;

using RM.Net;
using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public abstract class DeckBase
    {
        public virtual bool Initialize()
        {
            return false;
        }

        public virtual void Destroy()
        {
            m_arenaObject = null;
            m_jsonDeck = null;
            m_point = 0;
        }

        public int Point { get; set; }

        public ArenaObject BattleObject { set => m_arenaObject = value; }

        public DeckBase(ResJson_Deck jsonDeck)
        {
            m_jsonDeck = jsonDeck;
        }

        public bool IsPointMax()
        {
            lock (m_lock)
            {
                return m_point >= m_jsonDeck.PointMax;
            }
        }

        public void AddPoint()
        {
            lock (m_lock)
            {
                if (m_point < m_jsonDeck.PointMax)
                {
                    ++m_point;
                }                
            }
        }

        public abstract void Affect(ref STAT_DATA statData);

        public virtual void Use()
        {
        }

        public DECK SerializeData()
        {
            var data = new DECK();
            data.DeckID = m_jsonDeck.DeckID;
            data.DeckType = m_jsonDeck.DeckType;
            data.Point = m_point;
            data.PointMax = m_jsonDeck.PointMax;
            return data;
        }

        protected ArenaObject m_arenaObject;
        protected ResJson_Deck m_jsonDeck;
        private object m_lock = new object();
        protected int m_point = 1;
    }
}
