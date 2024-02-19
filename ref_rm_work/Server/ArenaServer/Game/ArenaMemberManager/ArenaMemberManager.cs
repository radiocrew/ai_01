using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using Level9.Expedition.Mobile.Util;

using RM.Common;
using RM.Net;
using RM.Server.Common;
using ArenaServer.Net;

namespace ArenaServer.Game
{
    public class ArenaMemberManager : Singleton<ArenaMemberManager>
    {
        public bool Initialize()
        {
            return true;
        }

        public bool Add(Guid playerUID, ArenaMember arenaMember)
        {
            if (true == m_contArenaMember.TryAdd(playerUID, arenaMember))
            {
                return true;
            }

            Console.WriteLine("what the,, member add failed...");
            return false;
        }

        public void Remove(Guid playerUID)
        {
            ArenaMember arenaPlayer;
            if (true == m_contArenaMember.TryRemove(playerUID, out arenaPlayer))
            {
                arenaPlayer.Destroy();

                Console.WriteLine("member in arena[{0}], remove ok", arenaPlayer.ArenaID);
                return;
            }

            Console.WriteLine("member in arena[{[0}], remove failed...", arenaPlayer.ArenaID);
        }

        public ArenaMember GetArenaMember(Guid playerUID)
        {
            ArenaMember arenaPlayer;
            if ((true == m_contArenaMember.TryGetValue(playerUID, out arenaPlayer)) && (null != arenaPlayer))
            {
                return arenaPlayer;
            }

            return null;
        }

        public Player GetPlayer(Guid playerUID)
        {
            ArenaMember arenaPlayer;
            if ((true == m_contArenaMember.TryGetValue(playerUID, out arenaPlayer)) && (null != arenaPlayer))
            {
                return arenaPlayer.Player;
            }

            return null;
        }

        public Player LoadFromDB(Guid playerUID, int playerArenaObjectID)
        {
            /*
              
                DB데이터를 로딩해서 factory 에 전달 해야...
              
             */


            int test_db_data = playerArenaObjectID;

            var player = PlayerFactory.Instance.Create(playerUID, test_db_data);
            return player;
        }

        private ArenaMemberManager()
        {
        }

        //-key : member uid,
        ConcurrentDictionary<Guid, ArenaMember> m_contArenaMember = new ConcurrentDictionary<Guid, ArenaMember>();
    }
}
