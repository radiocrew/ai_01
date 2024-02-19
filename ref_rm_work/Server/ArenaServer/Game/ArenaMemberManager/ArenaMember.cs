using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;

using RM.Server.Net;
using RM.Server.Common;

using ArenaServer.Net;
using ArenaServer.Tester;

namespace ArenaServer.Game
{
    public class ArenaMember
    {
        static public ArenaMember Loading(Guid playerUID, int playerArenaObjectID)
        {
            var player = ArenaMemberManager.Instance.LoadFromDB(playerUID, playerArenaObjectID);
            if (null != player)
            {
                ArenaMember arenaMember = new ArenaMember();
                arenaMember.Initialize();
                arenaMember.Player = player;

                return arenaMember;
            }

            return null;
        }

        public bool Initialize()
        {
            return false;
        }

        public void Destroy()
        {
            Player = null;
        }

        public void Reset(int arenaObjectID)
        {
            Player.Reset(arenaObjectID);
        }

        public bool EnterArena(Guid arenaKey, PlayerSession session)
        {
            var arena = ArenaManager.Instance.Get(arenaKey);
            if (null != arena)
            {
                if ((null != session) && (false == Player.IsConnected()))
                {
                    Player.BindSession(session);
                }

                //Player.Reset();
                if (true == Player.Enter(arena))
                {
                    this.ArenaKey = arenaKey;
                    this.ArenaID = arena.ID;
                    return true;
                }
            }

            return false;
        }

        public bool InArena(Guid key)
        {
            var arena = ArenaManager.Instance.Get(key);
            if (null != arena)
            {
                return arena.ExistPlayer(Player.UID);
            }

            return false;
        }

        public void MoveArena(Guid arenaKey)
        {
            if (null != Player)
            {
                Player.MoveArena(arenaKey);
            }
        }

        public void LeaveArena()
        {
            if (null != Player)
            {
                //-testcode,
                {
                    var playerUID = Player.UID;

                    MatchingTask mt = new MatchingTask(() => {

                        S_ARENA_MATCHING_CANCEL cancel = new S_ARENA_MATCHING_CANCEL();
                        cancel.PlayerUID = playerUID;
                        VirtualMatchingServerPacketHandler.ArenaMatchingCancel(cancel);
                    });
                    mt.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
                }

                Player.Leave();
            }
        }


        public string AccountUID
        {
            get; set;
        }

        public Player Player
        {
            get; 
            private set;
        }

        //>>
        //-testcode, 하나의 프로세스 안에서는 변경될 일이 있지만, 한 스레드에서 변경이 된다. arena leave & enter
        //              한 블럭에서 변경이 안되는 경우가 있다면 locking...
        public Guid ArenaKey
        {
            get; set;
        }

        public int ArenaID
        {
            get; set;
        }
        //<<    
    }
}
