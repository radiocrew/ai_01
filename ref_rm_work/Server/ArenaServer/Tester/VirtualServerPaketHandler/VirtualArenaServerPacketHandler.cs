using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Net;
using RM.Server.Net;
using RM.Server.Common;
using ArenaServer.Game;

namespace ArenaServer.Tester
{
    /*
     


        ★ here is VIRTUAL ARENA SERVER. thanks. 



     */
    class VirtualArenaServerPacketHandler
    {
        static public void MatchingAck(S_ARENA_MATCHING_ACK packet)
        {
            //-matching server → arena server

            var playerUID = packet.PlayerUID;
            var result = packet.Result;

            MatchingTask mt = new MatchingTask(() => {
                var player = ArenaMemberManager.Instance.GetPlayer(playerUID);

                ARENA_MATCHING_ACK ack = new ARENA_MATCHING_ACK();
                ack.Result = result;
                ack.PlayerUID = playerUID;
                player.SendPacket(ack);
            });

            mt.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
        }

        static public void CreateArenaReq(S_ARENA_CREATE_REQ packet)
        {
            RequestArenaType reqType = packet.RequestType;
            int arenaID = 0;
            Guid arenaKey = Guid.Empty;
            PacketResult result = PacketResult.Fail_CantCreateArena;

            /*
             * 
             * 여기에서 새로운 아레나를 생성.
             * 
             */
            var arena = ArenaManager.Instance.Create(packet.ArenaID);
            if (null != arena)
            {
                arenaID = arena.ID;
                arenaKey = arena.Key;
                result = PacketResult.Success;
            }

            MatchingTask mt = new MatchingTask(() => {

                S_ARENA_CREATE_ACK ack = new S_ARENA_CREATE_ACK();
                ack.RequestType = reqType;
                ack.ArenaKey = arenaKey;
                ack.MatchedPlayers = new List<ARENA_DEPATURE>();
                ack.Result = result;

                packet.MatchedPlayers.All(player => {
                    ack.MatchedPlayers.Add(player);
                    return true;
                });

                VirtualMatchingServerPacketHandler.CreateArenaAck(ack);
            });

            mt.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
        }

        static public void CreateArenaAck(S_ARENA_CREATE_ACK packet)
        {
            /*
             

             -서버 분할이라면 여기에서 remote 정보를 유저에게 보낸다.
             -testcode, 코드 안정화는 일부로 안 한다. 죽어야 알아 챈다.


             */
            ARENA_DEPATURE depature = packet.MatchedPlayers.First();
            Guid playerUID = depature.PlayerUID;
            Guid arenaKey = packet.ArenaKey;

            MatchingTask mt = new MatchingTask(() =>
            {
                var arenaMember = ArenaMemberManager.Instance.GetArenaMember(playerUID);
                if (null != arenaMember)
                {
                    //-(1)매칭서버가 만들어 놓은 아뤠놔...
                    var newArena = ArenaManager.Instance.Get(packet.ArenaKey);

                    ARENA_MOVE_ACK ack = new ARENA_MOVE_ACK();
                    ack.Result = (null != newArena) ? packet.Result : PacketResult.Fail_CantFindArena;
                    ack.PlayerUID = playerUID;
                    arenaMember.Player.SendPacket(ack);
                    
                    //-(2)이사 작업. 
                    if (null != newArena)
                    {
                        arenaMember.MoveArena(arenaKey);
                    }
                }
            });
            mt.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
        }

        static public void MatchingCompleteNtf(S_ARENA_MATCHING_COMPLETE_NTF packet)
        {
            var player = ArenaMemberManager.Instance.GetPlayer(packet.PlayerUID);
            if (null != player)
            {
                ARENA_MATCHING_COMPLETE_NTF ntf = new ARENA_MATCHING_COMPLETE_NTF();
                ntf.Arrival = packet.Arrival;
                ntf.PlayerUID = packet.PlayerUID;
                player.SendPacket(ntf);
                return;
            }

            Console.WriteLine("Can't notify to empty player[{0}]", packet.PlayerUID.ToString());
        }

        static public void MatchingCancel(S_ARENA_MATCHING_CANCEL packet)
        {
            var playerUID = packet.PlayerUID;

            MatchingTask mt = new MatchingTask(() => {
                var player = ArenaMemberManager.Instance.GetPlayer(playerUID);

                ARENA_MATCHING_CANCEL cancel = new ARENA_MATCHING_CANCEL();
                cancel.PlayerUID = playerUID;

                if (null != player)
                {
                    player.SendPacket(cancel);//jinsub, null 처리 해야함. 매칭도중에 한명이 나갈수있음. 
                    return;
                }

                Console.WriteLine("can't notify matching cancel to user who was disconnected");
            });

            mt.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
        }
    }
}
