using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;
using RM.Net;

namespace ArenaServer.Game
{
    public class Item : ArenaObject
    {
        public Item(Guid uid, ItemData data)
            : base(uid)
        {
            m_itemData = data;
        }

        public void Owner(Guid uid)
        {
            m_onwerUID = uid;
        }

        public ITEM_DATA SerializeData()
        {
            ITEM_DATA itemData = new ITEM_DATA();
            itemData.UID = UID;
            itemData.OwnerUID = m_onwerUID;
            itemData.ItemID = m_itemData.ID;
            itemData.ItemLevel = 0;

            return itemData;
        }

        public void OnItemConsume()
        {
        }

        public void OnItemEquip(Player player, bool result, Action equipNtf)
        {
            if (true == result)
            {
                player.Stat.Calculate(player, true);
            }

            equipNtf();
        }

        public void OnItemUpdate(Player player, INVENTORY_ITEM_UPDATE.ItemUpdateType updateType)
        {
            switch (updateType)
            {
                case INVENTORY_ITEM_UPDATE.ItemUpdateType.Created:
                    break;
                case INVENTORY_ITEM_UPDATE.ItemUpdateType.Updated:
                    break;
                case INVENTORY_ITEM_UPDATE.ItemUpdateType.Deleted:
                    player.ItemEquipManager.UnEquip(this);
                    player.Stat.Calculate(player, true);
                    break;
            }

            INVENTORY_ITEM_UPDATE packet = new INVENTORY_ITEM_UPDATE();
            packet.UpdateType = updateType;
            packet.ItemData = SerializeData();

            player.SendPacket(packet);
        }

        public ItemData Data => m_itemData;

        Guid m_onwerUID;
        readonly ItemData m_itemData = null;
    }
}
