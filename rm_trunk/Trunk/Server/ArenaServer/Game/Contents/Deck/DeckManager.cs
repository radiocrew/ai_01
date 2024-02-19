using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using RM.Common;
using RM.Net;
using RM.Server.Common;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class DeckManager
    {
        public DeckManager(ArenaObject owner)
        {
            //m_owner = owner;
        }

        public bool Initialize()
        {
            return false;
        }

        public void Destroy()
        {
            //m_owner = null;
            m_deckGenPoint = 0;
            m_selectableDeck.Clear();
            m_selectableDeck = null;


            m_activeDeck.All(pair => {
                pair.Value.Destroy();
                return true;
            });
            m_activeDeck = null;
        }

        public int BonusStatIndex { get => 1; } // stat Index 설계가 필요함

        public STAT_DATA BonusStat
        {
            get
            {
                var statData = new STAT_DATA();
                statData.Stat = new Dictionary<StatType, float>();

                lock (m_lock)
                {
                    foreach (var pair in m_activeDeck)
                    {
                        pair.Value.Affect(ref statData);
                    }
                }
                return statData;
            }
        }
        public void Use(int deckID)
        {
            lock (m_lock)
            {
                DeckBase deck = null;
                if (true == m_activeDeck.TryGetValue(deckID, out deck))
                {
                    deck.Use();
                }
            }
        }

        public bool Select(int deckID)
        {
            var jsonDeck = ResourceManager.Instance.FindDeck(deckID);
            if (null == jsonDeck)
            {
                Debug.Assert(false);
                return false;
            }

            lock (m_lock)
            {
                if (false == m_selectableDeck.Contains(deckID))
                {
                    return false;
                }

                m_selectableDeck.Clear();

                // Add Or Update
                AddOrUpdate(jsonDeck);
            }
            return true;
        }

        public bool HasSelectableDeck()
        {
            lock (m_lock)
            {
                return 0 < m_selectableDeck.Count;
            }
        }

        public bool GenRandomDeck(int deckCount)
        {
            if ((0 >= deckCount) || (DEFINE.MAX_GENERATE_DECK_COUNT < deckCount))
            {
                return false;
            }

            var jsonDeck = ResourceManager.Instance.JsonDeck.ToDictionary(key => key.Key, value => value.Value);
            if (0 == jsonDeck.Count)
            {
                return false;
            }

            lock (m_lock)
            {
                if ((0 < m_activeDeck.Count) && (true == m_activeDeck.All(pair =>
                {
                    if (true == pair.Value.IsPointMax())
                    {
                        jsonDeck.Remove(pair.Key);
                        return true;
                    }
                    return false;
                })))
                {
                    //-activeDeck 에서 max point 를 제외. jsonDeck 이 empty 라면 return false,
                    return false;
                }

                var jsonDeckList = jsonDeck.ToList();

                m_selectableDeck.Clear();

                while (m_selectableDeck.Count < deckCount)
                {
                    var rnd = RMMath.Random(jsonDeckList.Count);

                    m_selectableDeck.Add(jsonDeckList[rnd].Value.DeckID);

                    if (1 < jsonDeckList.Count)
                    {
                        jsonDeckList.RemoveAt(rnd);//-중쀍방지..
                    }
                }

                return true;
            }
        }

        public int IncreaseDeckPoint(int delta)
        {
            lock (m_lock)
            {
                m_deckGenPoint = RMMath.Clamp(m_deckGenPoint + delta, 0, CONST_DEFINE.DeckPointMax);
            }
            return m_deckGenPoint;
        }

        public void DeckNtf(ArenaObject arenaObject)
        {
            DECK_NTF ntf = new DECK_NTF();
            ntf.UID = arenaObject.UID;
            ntf.DeckData = SerializeData();
            arenaObject.SendPacket(ntf);
        }

        private void AddOrUpdate(ResJson_Deck jsonDeck)
        {
            lock (m_lock)
            {
                DeckBase deck = null;
                if (true == m_activeDeck.TryGetValue(jsonDeck.DeckID, out deck))
                {
                    // 있다면 Point++
                    deck.AddPoint();
                    return;
                }

                // 새로 생성
                deck = CreateDeck(jsonDeck);
                deck.BattleObject = null;// m_owner;

                if (null == deck)
                {
                    Debug.Assert(false);
                    return;
                }

                m_activeDeck.Add(jsonDeck.DeckID, deck);
            }
        }

        private DeckBase CreateDeck(ResJson_Deck jsonDeck)
        {
            switch (jsonDeck.DeckType)
            {
                case DeckType.StatEffect:
                    {
                        return new DeckStatEffect(jsonDeck);
                    }
                case DeckType.StatusEffect:
                    {
                        return new DeckStatusEffect(jsonDeck);
                    }
                case DeckType.Skill:
                    {
                        return new DeckSkillOpen(jsonDeck);
                    }
            }

            Debug.Assert(false);
            return null;
        }

        private DECK_DATA SerializeData()
        {
            var data = new DECK_DATA();
            lock (m_lock)
            {
                data.DeckGenPoint = m_deckGenPoint;
                data.SelectableDeck = m_selectableDeck.ToList();
                data.ActiveDeck = new Dictionary<int, DECK>();

                foreach (var pair in m_activeDeck)
                {
                    data.ActiveDeck.Add(pair.Key, pair.Value.SerializeData());
                }
            }
            return data;
        }

        //private ArenaObject m_owner = null;

        private object m_lock = new object();
        private int m_deckGenPoint = 0;
        private List<int> m_selectableDeck = new List<int>(); // Player가 선택할 DeckSkillID
        private Dictionary<int, DeckBase> m_activeDeck = new Dictionary<int, DeckBase>(); // 활성화된 DeckSkill
    }
}
