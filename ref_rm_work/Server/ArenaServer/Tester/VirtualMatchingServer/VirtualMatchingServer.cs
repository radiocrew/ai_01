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

using ArenaServer.Game;

namespace ArenaServer.Tester
{
    public class VirtualArena//-made 된거 정보
    {
        public Guid UID { get; set; }
        public int ServerID { get; set; }//-server machine
        public string IP { get; set; }
        public int Port { get; set; }
        public int MaxPlayerCount { get; set; }
    }

    public class VirtualMatchingServer : Singleton<VirtualMatchingServer>
    {
        public bool Initialize()
        {
            m_arenas = new ConcurrentDictionary<Guid, VirtualArena>();
            return true;
        }

        //-use for Arena L/B
        public bool Regist(VirtualArena arena)
        {
            if (true == m_arenas.TryAdd(arena.UID, arena))
            {
                return true;
            }

            return false;
        }

        public void Unregist(Guid uid)
        {
            VirtualArena arena = null;
            m_arenas.TryRemove(uid, out arena);
        }

        public void Move(ARENA_MOVE_REQ packet)
        {
            int arenaID = packet.ArenaID;

            ARENA_DEPATURE depature = new ARENA_DEPATURE();
            depature.PlayerUID = packet.playerUID;
            var waiterList = new List<Waiter>();
            waiterList.Add(new Waiter(depature));

            VirtualMatchingManager.Instance.RequestCreateArena(RequestArenaType.Single, arenaID, waiterList);
        }

        public void MatchedMove(MATCHED_ARENA_MOVE_REQ packet)
        {
            MatchingTask mt = new MatchingTask(() => {

                Guid playerUID = packet.playerUID;

                var arenaMember = ArenaMemberManager.Instance.GetArenaMember(playerUID);
                if (null != arenaMember)
                {
                    //-(1)매칭서버가 만들어 놓은 아뤠놔...
                    var newArena = ArenaManager.Instance.Get(packet.ArenaKey);

                    MATCHED_ARENA_MOVE_ACK ack = new MATCHED_ARENA_MOVE_ACK();
                    ack.Result = (null != newArena) ? PacketResult.Success : PacketResult.Fail_CantFindArena;
                    ack.PlayerUID = playerUID;
                    arenaMember.Player.SendPacket(ack);

                    //-(2)이사 작업. 
                    if (null != newArena)
                    {
                        arenaMember.MoveArena(newArena.Key);
                    }
                }
            });
            mt.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
        }

        private VirtualMatchingServer()
        {
        }

        ConcurrentDictionary<Guid, VirtualArena> m_arenas = null;
    }
}
