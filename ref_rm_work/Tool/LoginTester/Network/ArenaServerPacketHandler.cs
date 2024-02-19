using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Net;
using RM.Common;
using rm_login.Tool;

namespace rm_login.Network
{
    public partial class ArenaServerPacketHandler
    {
        public IPacketHandler this[RMProtocols pID]
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

        public bool InitHandler()
        {
            m_PacketHandlers.Add(RMProtocols.ARENA_HELLO_NTF, new PacketHandler<ARENA_HELLO_NTF>(ArenaHelloNtf));
            m_PacketHandlers.Add(RMProtocols.ARENA_LOGIN_ACK, new PacketHandler<ARENA_LOGIN_ACK>(ArenaLoginAck));
            m_PacketHandlers.Add(RMProtocols.ARENA_MOVE_ACK, new PacketHandler<ARENA_MOVE_ACK>(ArenaMoveAck));

            m_PacketHandlers.Add(RMProtocols.ARENA_MATCHING_ACK, new PacketHandler<ARENA_MATCHING_ACK>(ArenaMatchingAck));
            m_PacketHandlers.Add(RMProtocols.ARENA_MATCHING_CANCEL, new PacketHandler<ARENA_MATCHING_CANCEL>(ArenaMatchingCancel));
            m_PacketHandlers.Add(RMProtocols.ARENA_MATCHING_COMPLETE_NTF, new PacketHandler<ARENA_MATCHING_COMPLETE_NTF>(ArenaMatchingCompleteNtf));

            m_PacketHandlers.Add(RMProtocols.MATCHED_ARENA_MOVE_ACK, new PacketHandler<MATCHED_ARENA_MOVE_ACK>(MatchedArenaMoveAck));

            m_PacketHandlers.Add(RMProtocols.ARENA_ENTER_PLAYER_NTF, new PacketHandler<ARENA_ENTER_PLAYER_NTF>(ArenaEnterPlayerNtf));
            m_PacketHandlers.Add(RMProtocols.ARENA_ENTER_OBJECT_NTF, new PacketHandler<ARENA_ENTER_OBJECT_NTF>(ArenaEnterObjectNtf));
            m_PacketHandlers.Add(RMProtocols.ARENA_LEAVE_OBJECT_NTF, new PacketHandler<ARENA_LEAVE_OBJECT_NTF>(ArenaLeaveObjectNtf));
            m_PacketHandlers.Add(RMProtocols.HERO_MOVEMENT_NTF, new PacketHandler<HERO_MOVEMENT_NTF>(MovementNtf));

            m_PacketHandlers.Add(RMProtocols.CAST_SKILL_NTF, new PacketHandler<CAST_SKILL_NTF>(CastSkillNtf));
            m_PacketHandlers.Add(RMProtocols.CAST_SKILL_PROJECTILE_NTF, new PacketHandler<CAST_SKILL_PROJECTILE_NTF>(CastSkillProjectileNtf));
            m_PacketHandlers.Add(RMProtocols.INVOKE_SKILL_NTF, new PacketHandler<INVOKE_SKILL_NTF>(InvokiSkillNtf));
            m_PacketHandlers.Add(RMProtocols.DAMAGE_NTF, new PacketHandler<DAMAGE_NTF>(DamageNtf));
            m_PacketHandlers.Add(RMProtocols.NPC_MOVEMENT_NTF, new PacketHandler<NPC_MOVEMENT_NTF>(NpcMovementNtf));
            m_PacketHandlers.Add(RMProtocols.STATUS_NTF, new PacketHandler<STATUS_NTF>(StatusNtf));
            m_PacketHandlers.Add(RMProtocols.HERO_REVIVE_ACK, new PacketHandler<HERO_REVIVE_ACK>(HeroReviveAck));
            m_PacketHandlers.Add(RMProtocols.HEALTH_NTF, new PacketHandler<HEALTH_NTF>(HealthNtf));
            m_PacketHandlers.Add(RMProtocols.ACTIVE_LEVEL_NTF, new PacketHandler<ACTIVE_LEVEL_NTF>(ActiveLevelNtf));
            m_PacketHandlers.Add(RMProtocols.STAT_NTF, new PacketHandler<STAT_NTF>(StatNtf));
            m_PacketHandlers.Add(RMProtocols.DECK_NTF, new PacketHandler<DECK_NTF>(DeckNtf));

            m_PacketHandlers.Add(RMProtocols.INVENTORY, new PacketHandler<INVENTORY_ACK>(InventoryAck));
            m_PacketHandlers.Add(RMProtocols.INVENTORY_ITEM_UPDATE_NTF, new PacketHandler<INVENTORY_ITEM_UPDATE>(InventoryItemUpdate));
            m_PacketHandlers.Add(RMProtocols.INVENTORY_ITEM_DELETE, new PacketHandler<INVENTORY_ITEM_DELETE_ACK>(InventoryItemDelete));
            m_PacketHandlers.Add(RMProtocols.ITEM_EQUIP, new PacketHandler<ITEM_EQUIP>(ItemEquip));

            m_PacketHandlers.Add(RMProtocols.PROJECTILE_REMOVE_NTF, new PacketHandler<PROJECTILE_REMOVE_NTF>(ProjectileRemoveNtf));

            return (0 < m_PacketHandlers.Count);
        }

        public void OnConnected(object sender, EventArgs e)
        {
            LogDelegate.Instance.Log("server connected", System.Drawing.Color.Blue);
        }

        public void OnClosed(object sender, EventArgs e)
        {
            LogDelegate.Instance.Log("server disconnected", System.Drawing.Color.Red);

        //-UI 쵸기화,
            ArenaObjectManager.Instance.Clear();
            FormDelegate.Instance.UIClear();

            DrawManager.Instance.Clear();
        }

        public void OnReceived(ushort pID, byte[] receiveBuffer)
        {
            var protocolType = ((RMProtocols)pID);
            var handler = this[protocolType];

            //Debug.Assert(null != handler);

            if (null != handler)
            {
                m_Packets.Enqueue(() =>
                {
                    handler.Invoke(receiveBuffer);
                });
            }
            else
            {
                LogDelegate.Instance.Log(string.Format("Incoming unknown protocol id: {0}", protocolType.ToString()), System.Drawing.Color.Red);
            }
        }

        public void OnError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            LogDelegate.Instance.Log(e.Exception.Message, System.Drawing.Color.Red);
        }

        public void OnException(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            LogDelegate.Instance.Log(e.Exception.Message, System.Drawing.Color.Red);
        }

        public void PacketProcessing()
        {
            // 기회를 잡으면 모두 소모
            while (0 != m_Packets.Count)
            {
                Action processing = null;
                if (true == m_Packets.TryDequeue(out processing))
                {
                    // 패킷 처리
                    processing?.Invoke();
                }
            }
        }

        ConcurrentQueue<Action> m_Packets = new ConcurrentQueue<Action>();
        Dictionary<RMProtocols, IPacketHandler> m_PacketHandlers = new Dictionary<RMProtocols, IPacketHandler>();
    }
}
