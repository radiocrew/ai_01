using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Net;

namespace ArenaServer.Game
{
    public class Inventory
    {
        public bool Initialize()
        {
            m_items = new Dictionary<Guid, Item>();
            return false;
        }

        public void Destroy()
        {
            lock (m_lock)
            {
                m_items.Clear();
                m_items = null;
            }
        }

        public bool Load()
        {
            return false;
        }

        public Item FindItem(Guid uid)
        {
            Item item = null;

            lock (m_lock)
            {
                if (true == m_items.TryGetValue(uid, out item))
                {
                    return item;
                }
            }

            return null;
        }

        public bool GetItem(Player player, int itemID, int amount)
        {
            var newItem = ItemManager.Instance.AllocItem(player, itemID, Guid.NewGuid());
            if (null != newItem)
            {
                lock (m_lock)
                {
                    m_items.Add(newItem.UID, newItem);
                }
                
                newItem.OnItemUpdate(player, INVENTORY_ITEM_UPDATE.ItemUpdateType.Created);
                return true;
            }

            return false;
        }

        public void DeleteIem(Player player, Guid uid)
        {
            Item item = null;
            bool ret = false;

            lock (m_lock)
            {
                if (true == m_items.TryGetValue(uid, out item))
                {
                    ret = m_items.Remove(uid);
                }
            }

            if (true == ret)
            {
                item.OnItemUpdate(player, INVENTORY_ITEM_UPDATE.ItemUpdateType.Deleted);
            }
        }

        object m_lock = new object();
        Dictionary<Guid, Item> m_items = null;
    }
}
