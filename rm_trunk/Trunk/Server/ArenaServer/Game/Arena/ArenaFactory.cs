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
    public class ArenaFactory : Singleton<ArenaFactory>
    {
        public bool Initialize()
        {
            m_contArenaMap = new Dictionary<int, int>();

            foreach (var resArena in ResourceManager.Instance.JsonArena.Values.ToList())
            {
                var jsonMap = ResourceManager.Instance.FindMap(resArena.MapID);
                if (null == jsonMap)
                {
                    return false;
                }

                m_contArenaMap.Add(resArena.ArenaID, resArena.MapID);
            }

            return (0 < m_contArenaMap.Count);
        }

        public Arena Create(int arenaID)
        {
            int mapID = 0;

            if (true == m_contArenaMap.TryGetValue(arenaID, out mapID))
            {
                Arena arena = new Arena(arenaID, Guid.NewGuid());
                if (true == arena.Initialize(mapID))
                {
                    return arena;
                }
            }

            return null;
        }

        public bool IsExist(int arenaID)
        {
            return m_contArenaMap.ContainsKey(arenaID);
        }

        private ArenaFactory()
        {
        }

        Dictionary<int, int> m_contArenaMap = null;
    }
}
