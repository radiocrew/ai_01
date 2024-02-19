using System;
using System.Collections.Generic;
using System.Diagnostics;

using Level9.Expedition.Mobile.Logging;

using RM.Common;
using RM.Net;
using RM.Server.Net;
using RM.Server.Common;

using ArenaServer.Game;
using ArenaServer.Resource;
using ArenaServer.Tester;

namespace ArenaServer.Net
{

    public class ArenaPacketHandlers
    {
        public ArenaPacketHandlers()
        {
            InitHandler();
        }

        public void InitHandler()
        {
            m_PacketHandlers.Add(RMProtocols.HEART_BEAT, new PacketHandler<PlayerSession, HEART_BEAT>(HeartBeat));
            m_PacketHandlers.Add(RMProtocols.TEST_COMMAND, new PacketHandler<PlayerSession, TEST_COMMAND>(TestCommand));
            m_PacketHandlers.Add(RMProtocols.ARENA_LOGIN_REQ, new PacketHandler<PlayerSession, ARENA_LOGIN_REQ>(ArenaLoginReq));
            m_PacketHandlers.Add(RMProtocols.ARENA_MOVE_REQ, new PacketHandler<PlayerSession, ARENA_MOVE_REQ>(ArenaMoveReq));
            m_PacketHandlers.Add(RMProtocols.ARENA_MATCHING_REQ, new PacketHandler<PlayerSession, ARENA_MATCHING_REQ>(ArenaMatchingReq));
            m_PacketHandlers.Add(RMProtocols.ARENA_MATCHING_CANCEL, new PacketHandler<PlayerSession, ARENA_MATCHING_CANCEL>(ArenaMathcingCancel));
            m_PacketHandlers.Add(RMProtocols.MATCHED_ARENA_MOVE_REQ, new PacketHandler<PlayerSession, MATCHED_ARENA_MOVE_REQ>(MatchedArenaMoveReq));

            m_PacketHandlers.Add(RMProtocols.HERO_MOVEMENT_NTF, new PacketHandler<PlayerSession, HERO_MOVEMENT_NTF>(Movement));
            m_PacketHandlers.Add(RMProtocols.START_ATTACK, new PacketHandler<PlayerSession, START_ATTACK>(StartAttack));
            m_PacketHandlers.Add(RMProtocols.STOP_ATTACK, new PacketHandler<PlayerSession, STOP_ATTACK>(StopAttack));
            m_PacketHandlers.Add(RMProtocols.CAST_SKILL, new PacketHandler<PlayerSession, CAST_SKILL>(CastSkill));

            m_PacketHandlers.Add(RMProtocols.HERO_REVIVE_REQ, new PacketHandler<PlayerSession, HERO_REVIVE_REQ>(HeroRevive));

            m_PacketHandlers.Add(RMProtocols.DECK_USE_REQ, new PacketHandler<PlayerSession, DECK_USE_REQ>(DeckUse));
            m_PacketHandlers.Add(RMProtocols.DECK_SELECT_REQ, new PacketHandler<PlayerSession, DECK_SELECT_REQ>(DeckSelect));
            m_PacketHandlers.Add(RMProtocols.DECK_GEN_REQ, new PacketHandler<PlayerSession, DECK_GEN_REQ>(DeckGen));

            m_PacketHandlers.Add(RMProtocols.INVENTORY, new PacketHandler<PlayerSession, INVENTORY_REQ>(InventoryReq));
        }

        // Packet Handler
        #region PacketHandler

        public void ArenaLoginReq(PlayerSession session, ARENA_LOGIN_REQ packet)
        {
            Action<Guid, PacketResult, bool> actionAck = (key, result, close) => {
                ARENA_LOGIN_ACK ack = new ARENA_LOGIN_ACK();
                ack.ArenaKey = key;
                ack.Result = result;
                session.SendPacket(ack);

                if ((PacketResult.Success != result) && (true == close))
                {
                    session.Close();
                }
            };

            //-testcode, 재접속해서 이전 아레나에 연결시키는 작업하려면 아래를 수정.
            var arenaMember = ArenaMemberManager.Instance.GetArenaMember(packet.PlayerUID);
            if ((null != arenaMember) && (arenaMember.InArena(packet.ArenaKey)))
            {
                actionAck(packet.ArenaKey, PacketResult.Fail_InArenaAlready, true);
                return;
            }

            //-testcode, login 전에 싱글/레이드 아레나는 이미 생성 되어 있어야 하지.
            {
                //var existArena = ArenaManager.Instance.GetFirst();
                //if (null != existArena)
                //{
                //    packet.ArenaKey = existArena.Key;
                //}
                //else
                {
                    var newArena = ArenaManager.Instance.Create(DEFINE.TEST_FIRST_ARENA_ID);
                    packet.ArenaKey = newArena.Key;
                }
            }

            arenaMember = ArenaMember.Loading(packet.PlayerUID, packet.TestPlayerObjectID);

            if ((null != arenaMember) && arenaMember.EnterArena(packet.ArenaKey, session))
            {
                // @@ Account 일단 PlayerUID로
                session.InitPlayerInfo(packet.PlayerUID, packet.PlayerUID);

                //-jinsub, 이상하다. 아래에서 Add가 실패나면?! 근데, 위에는 이미 enter 성공 했는데???? 말이 안됨. 
                ArenaMemberManager.Instance.Add(packet.PlayerUID, arenaMember);

                actionAck(packet.ArenaKey, PacketResult.Success, false);
                return;
            }

            session.Close();
        }

        public void ArenaMoveReq(PlayerSession session, ARENA_MOVE_REQ packet)
        {
            var player = GetPlayer(session);
            if (null == player)
            {
                return;
            }

            if (false == ArenaFactory.Instance.IsExist(packet.ArenaID))
            {
                ARENA_MOVE_ACK ack = new ARENA_MOVE_ACK();
                ack.Result = PacketResult.Fail_InvaildArenaID;
                session.SendPacket(ack);
                return;
            }

            MatchingTask task = new MatchingTask(() =>
            {
                packet.playerUID = player.UID;
                VirtualMatchingServerPacketHandler.ArenaMove(packet);
            });
            task.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
        }

        public void ArenaMatchingReq(PlayerSession session, ARENA_MATCHING_REQ packet)
        {
            var player = GetPlayer(session);
            if (null == player)
            {
                Console.WriteLine("Unknown player");
                return;
            }

            if (false == ArenaFactory.Instance.IsExist(packet.ArenaID))
            {
                ARENA_MATCHING_ACK ack = new ARENA_MATCHING_ACK();
                ack.Result = PacketResult.Fail_InvaildArenaID;
                session.SendPacket(ack);

                return;
            }

            S_ARENA_MATCHING_REQ req = new S_ARENA_MATCHING_REQ();
            req.Depature = new ARENA_DEPATURE();
            req.Depature.IP = "1.1.1.1";
            req.Depature.PlayerUID = player.UID;
            req.ArenaID = packet.ArenaID;
            

            MatchingTask mt = new MatchingTask(() => {

                VirtualMatchingServerPacketHandler.ArenaMatchingReq(req);
            });

            mt.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
        }

        public void ArenaMathcingCancel(PlayerSession session, ARENA_MATCHING_CANCEL packet)
        {
            var player = GetPlayer(session);
            if (null == player)
            {
                return;
            }

            MatchingTask mt = new MatchingTask(() => {

                S_ARENA_MATCHING_CANCEL cancel = new S_ARENA_MATCHING_CANCEL();
                cancel.PlayerUID = player.UID;
                VirtualMatchingServerPacketHandler.ArenaMatchingCancel(cancel);
            });

            mt.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
        }

        public void MatchedArenaMoveReq(PlayerSession session, MATCHED_ARENA_MOVE_REQ packet)
        {
            var player = GetPlayer(session);
            if (null == player)
            {
                Console.WriteLine("Unknown player");
                return;
            }

            MatchingTask task = new MatchingTask(() =>
            {
                packet.playerUID = player.UID;
                VirtualMatchingServerPacketHandler.MatchedArenaMove(packet);
            });
            task.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
        }

        public void HeartBeat(PlayerSession session, HEART_BEAT packet)
        {
            var player = GetPlayer(session);
            if (null != player)
            {
            //-jinsub, 로컬 아레나 이동에서 기존 아레나를 벗어날때 player를 destory 시키고, 
            //  player.heartbeat 를 null 로 만든다. 그 순간에 클라에서는 하트비트를 보낼수 있고 그때 널참조가 발생할 수 있음. 
                player.HeartBeat?.Revive();
            }
        }

        public void TestCommand(PlayerSession session, TEST_COMMAND packet)
        {
            var player = GetPlayer(session);
            if ((null == player) 
            && (TestCommandType.Login != packet.TestCommandType)
            && (TestCommandType.Logout != packet.TestCommandType)
            )
            {
                return;
            }

            Guid uid;

            switch (packet.TestCommandType)
            {
                case TestCommandType.AddExp:
                    LevelExperienceManager.Instance.Give(player, packet.Int_0);
                    break;
                case TestCommandType.ActiveExp:
                    ActiveLevelExperienceManager.Instance.Give(player, packet.Int_0);
                    break;
                case TestCommandType.DeckPointUp:
                    player.AddDeckPoint(1);
                    break;
                case TestCommandType.DeckGenerate:
                    player.DeckGen(true);
                    break;
                case TestCommandType.CreateNpc:

                    var arenaObjectID = packet.Int_0;
                    var dir = packet.Int_1;

                    var x = packet.Float_0;
                    var y = packet.Float_1;

                    if (false == player.Arena.ForceDeploy(arenaObjectID, x, y, dir))
                    {
                        Console.WriteLine("can't make it.");
                    }
                    break;
                case TestCommandType.PlayerSuicide:
                    player.Suicide();
                    break;
                case TestCommandType.GetItem:
                    int itemID = packet.Int_0;
                    int amount = packet.Int_1;

                    player.Inventory.GetItem(player, itemID, amount);
                    break;
                case TestCommandType.DelItem:
                    uid = packet.UID_0;
                    player.Inventory.DeleteIem(player, uid);
                    break;
                case TestCommandType.EquipItem:
                    ITEM_EQUIP command = new ITEM_EQUIP();
                    command.UID = packet.UID_0;
                    command.IsEquipOrNot = (0 == packet.Int_0) ? (false) : (true);
                    player.EquipItem(command);
                    break;
                case TestCommandType.Login:
                    ARENA_LOGIN_REQ req = new ARENA_LOGIN_REQ();
                    req.PlayerUID = packet.UID_0;
                    req.TestPlayerObjectID = packet.Int_0;
                    ArenaLoginReq(session, req);
                    break;
                case TestCommandType.Logout:
                    session.Close();
                    break;
                case TestCommandType.CastSkill:
                    SkillParam skillParam = new SkillParam();
                    skillParam.CastPosition = player.Position;
                    skillParam.Direction = packet.Int_1;
                    player.CastSkill(packet.Int_0, skillParam);
                    break;
                case TestCommandType.Move:
                    x = (float)packet.Int_0;
                    y = (float)packet.Int_1;
                    var d = (float)packet.Int_2;

                    if (true == player.Arena.Map.IsValid(x, y))
                    {
                        HERO_MOVEMENT_NTF ntf = new HERO_MOVEMENT_NTF();
                        ntf.UID = player.UID;
                        ntf.Position = new NVECTOR3(x, 0.0f, y);
                        ntf.Direction = d;
                        player.MoveNtf(ntf);
                    }
                    break;
            }
        }

        public void Movement(PlayerSession session, HERO_MOVEMENT_NTF packet)
        {
            if (false == ContentsValidation(session))
            {
                return;
            }

            var player = GetPlayer(session);
            if (null == player)
            {
                return;
            }

            if (false == player.Arena.Map.IsValid(packet.Position.X, packet.Position.Z))
            {
                return;
            }

            player.MoveNtf(packet);            
        }

        public void StartAttack(PlayerSession session, START_ATTACK packet)
        {
            GetPlayer(session)?.AutoSkill.Switch(ESwitch.On);
        }

        public void StopAttack(PlayerSession session, STOP_ATTACK packet)
        {
            GetPlayer(session)?.AutoSkill.Switch(ESwitch.Off);
        }

        public void CastSkill(PlayerSession session, CAST_SKILL packet)
        {
            if (false == ContentsValidation(session))
            {
                return;
            }

            var player = GetPlayer(session);
            if (null != player)
            {
                player.CastSkillFromNet(packet);
                return;
            }
        }

        public void HeroRevive(PlayerSession session, HERO_REVIVE_REQ packet)
        {
            var player = GetPlayer(session);
            if (null != player)
            {
                player.ReviveReq(packet);
            }
        }

        public void DeckUse(PlayerSession session, DECK_USE_REQ packet)
        {
            var player = GetPlayer(session);
            if (null != player)
            {
                player.DeckUse(packet);
            }
        }

        public void DeckSelect(PlayerSession session, DECK_SELECT_REQ packet)
        {
            var player = GetPlayer(session);
            if (null != player)
            {
                player.DeckSelect(packet);
            }
        }

        public void DeckGen(PlayerSession session, DECK_GEN_REQ packet)
        {
            var player = GetPlayer(session);
            if (null != player)
            {
                player.DeckGen();
            }
        }

        public void InventoryReq(PlayerSession session, INVENTORY_REQ packet)
        {
            var player = GetPlayer(session);
            if (null != player)
            {
            }
        }

        public void ItemEquipReq(PlayerSession session, ITEM_EQUIP packet)
        {
            var player = GetPlayer(session);
            if (null != player)
            {
                player.EquipItem(packet);
            }
        }


        #endregion


        #region 

        private Player GetPlayer(PlayerSession session)
        {
            return session.Player;//-jinsub, Player를 가져올 때, 스레드 세이프 한가?!  (locking -> stack copy -> unlock -> return stack 하는가?)
        }


        private bool ContentsValidation(PlayerSession session)
        {
            //if (null == session.PlayerInfo)
            //{
            //    return false;
            //}

            return true;
        }

        #endregion


        public IPacketHandler<PlayerSession> this[RMProtocols pID]
        {
            get
            {
                if (m_PacketHandlers.ContainsKey(pID))
                {
                    return m_PacketHandlers[pID];
                }
                return null;
            }
        }

        Dictionary<RMProtocols, IPacketHandler<PlayerSession>> m_PacketHandlers = new Dictionary<RMProtocols, IPacketHandler<PlayerSession>>();
    }




}
