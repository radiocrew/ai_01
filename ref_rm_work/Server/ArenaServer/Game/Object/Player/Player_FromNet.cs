using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using SuperSocket.SocketBase;

using RM.Net;
using RM.Server.Common;

using ArenaServer.Net;

namespace ArenaServer.Game
{
    public partial class Player
    {

        public bool IsConnected()
        {
            lock (m_sessionLock)
            {
                if ((null != m_session) && (m_session.Connected))
                {
                    return true;
                }
            }

            return false;
        }

        public void Disconnect()
        {
            lock (m_sessionLock)
            {
                if (null != m_session)
                {
                    m_session.Close(CloseReason.TimeOut);
                }
            }
        }

        public override void SendPacket(RMPacket packet)
        {
            PlayerSession session = null;

            lock (m_sessionLock)
            {
                session = m_session;
            }

            session?.SendPacket(packet);
        }

        public override void ForceSendPacket(RMPacket packet)
        {
            PlayerSession session = null;

            lock (m_sessionLock)
            {
                session = m_session;
            }

            session?.ForceSendPacket(packet);
        }
        public override void MoveNtf(HERO_MOVEMENT_NTF movement)
        {
            base.MoveNtf(movement);
        }

        public void MoveArena(Guid arenaKey)
        {
            HeartBeat.Stop = true;

            DelayedTask dt = new DelayedTask(() => {

                var playerUID = UID;
                var arenaMember = ArenaMemberManager.Instance.GetArenaMember(playerUID);
                if (null != arenaMember)
                {
                    int lastArenaObjectID = arenaMember.Player.ArenaObjectID;//-testcode,

                    //-enter 는 leave 이후에 오도록 해야한다. (arenamember 안의 player를 초기화 하고 재사용하기 때문이지.)
                    arenaMember.LeaveArena();
                    arenaMember.Reset(lastArenaObjectID);
                    arenaMember.EnterArena(arenaKey, null);
                }
            });
            dt.SubmitImmediately((int)TimerDispatcherIDType.PlayerMoveArena);
        }

        public void CastSkillFromNet(CAST_SKILL packet)
        {
            SkillParam skillParam = new SkillParam();
            if (true == skillParam.Initialize(Arena, packet.SkillParamData))
            {
                CastSkill(packet.SkillID, skillParam);
            }
        }

        public void ReviveReq(HERO_REVIVE_REQ packet)
        {
            HPResetSync(Health.HPMax);
            HealthNtf();

            Status.Remove(RM.Common.StatusType.Dead);
            StatusNtf();
        }

        public void AddDeckPoint(int deckPoint)
        {
            if (0 >= deckPoint)
            {
                return;
            }

            Deck.IncreaseDeckPoint(deckPoint);

            if (false == Deck.HasSelectableDeck())
            {
                Deck.GenRandomDeck(DEFINE.MAX_GENERATE_DECK_COUNT);
            }

            Deck.DeckNtf(this);
        }

        public void DeckUse(DECK_USE_REQ packet)
        {
            Deck.Use(packet.DeckID);
        }

        public void DeckSelect(DECK_SELECT_REQ packet)
        {
            if (false == Deck.Select(packet.DeckID))
            {
                return;
            }

            Deck.IncreaseDeckPoint(-1);

            Deck.BonusStat.Stat.All(stat => {
                Stat.AddOrUpdate(stat.Key, stat.Value, RM.Common.StatBonusType.Deck);
                return true;
            });

            Stat.Calculate(this, true);
            Deck.DeckNtf(this);
        }

        public void DeckGen(bool isTest = false)
        {
            if ((false == isTest) && (true == Deck.HasSelectableDeck()))
            {
                return;
            }

            if (true == Deck.GenRandomDeck(DEFINE.MAX_GENERATE_DECK_COUNT))
            {
                Deck.DeckNtf(this);
            }
        }

        public void Suicide()
        {
            int hp = 0;
            HPSync(-Health.HPMax, out hp);
            if (0 >= hp)
            {
                OnDie();
            }
        }

        public void EquipItem(ITEM_EQUIP packet)
        {
            Item item = m_inventory.FindItem(packet.UID);
            if (null != item)
            {
                bool ret = (true == packet.IsEquipOrNot) ? (ItemEquipManager.Equip(item)) : (ItemEquipManager.UnEquip(item));

                item.OnItemEquip(this, ret, () => {
                    ITEM_EQUIP ntf = new ITEM_EQUIP();
                    ntf.UID = item.UID;
                    ntf.IsEquipOrNot = packet.IsEquipOrNot;
                    ntf.Result = ret;
                    SendPacket(ntf);
                });
            }
        }

        public void DeleteItem(Guid uid)
        {
            //-1. 인벤토리에서 삭제
            //-2. 장착 해제,
            //-3. 효과 읎애기
        }
    }
}
