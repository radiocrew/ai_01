using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Timers;
using System.Linq;

using Level9.Expedition.Mobile.Logging;
using Level9.Expedition.Mobile.Util;

using RM.Common;
using RM.Server.Common;

namespace ArenaServer.Game
{
    public class ArenaManager : Singleton<ArenaManager>, ITimerUpdate
    {
        public bool Initialize()
        {
            m_updateTimer = new TimerUpdater<ArenaManager>(DEFINE.TIME_ARENA_MANAGER_UPDATE_INTERVAL_MS, this);
            m_updateTimer.Submit(m_updateTimer.GetTimerInterval, (int)TimerDispatcherIDType.ArenaManagerUpdate);

            return true;
        }

        public Arena Create(int arenaID)
        {
            var newArena = ArenaFactory.Instance.Create(arenaID);
            if (null != newArena)
            {
                if (true == m_contArena.TryAdd(newArena.Key, newArena))
                {
                    return newArena;
                }
            }

            return null;
        }

        public Arena GetFirst()//-use for test
        {
            if (0 < m_contArena.Count())
            {
                return m_contArena.First().Value;
            }

            return null;   
        }

        public void Destory(ref Guid key)
        {
            Arena arena = null;
            if (true == m_contArena.TryRemove(key, out arena))
            {
                var arenaID = arena.ID;
                arena.Destory();
                Console.WriteLine("★ caution!!!, empty arena[{0}] was destoryed...", arenaID);
            }
        }

        public Arena Get(Guid key)
        {
            Arena arena = null;
            if (true == m_contArena.TryGetValue(key, out arena))
            {
                return arena;
            }

            return null;
        }

        public bool IsExist(Guid key)
        {
            Arena arena = null;
            var ret = m_contArena.TryGetValue(key, out arena);
            return ret;
        }
        public virtual void OnUpdate(int id, double accumT, double dT)
        {
            //Console.WriteLine("Manager called : [{0}] accumt[{1}] dt[{2}]", System.DateTime.Now.ToString("hh:mm:ss.fff"), accumT, dT);



            FindEmptyArena();
        }

        private ArenaManager()
        {
        }

        private void FindEmptyArena()
        {
            var now = System.DateTime.UtcNow;
            List<Guid> expiredArenaKeys = new List<Guid>();

            foreach (var arena in m_contArena.Select(pair => pair.Value))
            {
                if (true == arena.IsEmpty(now))
                {
                    expiredArenaKeys.Add(arena.Key);
                }
            }

            expiredArenaKeys.ForEach((key) => {
                Destory(ref key);
            });
        }

        TimerUpdater<ArenaManager> m_updateTimer = null;
        ConcurrentDictionary<Guid, Arena> m_contArena = new ConcurrentDictionary<Guid, Arena>();
    }
}


