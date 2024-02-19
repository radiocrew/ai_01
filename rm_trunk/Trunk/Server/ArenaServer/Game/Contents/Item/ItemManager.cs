using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level9.Expedition.Mobile.Util;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class ItemManager : Singleton<ItemManager>
    {
        public bool Initialize()
        {
            m_items = new ConcurrentDictionary<int, ItemData>();
            
            foreach (var jsonItem in ResourceManager.Instance.JsonItem.Values.ToList())
            {
                var resItemStat = ResourceManager.Instance.FindItemStat(jsonItem.StatID);
                if ((null == resItemStat) && (0 != jsonItem.StatID))
                {
                    return false;
                }

                ItemData itemData = new ItemData(jsonItem, resItemStat);
                if (false == itemData.Initialize())
                {
                    return false;
                }

                if (false == m_items.TryAdd(itemData.ID, itemData))
                {
                    return false;
                }
            }
            
            return true;
        }

        public ItemData FindItem(int itemID)
        {
            ItemData itemData = null;
            m_items.TryGetValue(itemID, out itemData);

            return itemData;
        }

        public Item AllocItem(Player player, int itemID, Guid uid)
        {
            ItemData itemData = null;
            if (true == m_items.TryGetValue(itemID, out itemData))
            {
                Item newItem = new Item(uid, itemData);
                newItem.Owner(player.UID);

                return newItem;
            }

            return null;
        }

        private ItemManager()
        {
        }

        ConcurrentDictionary<int, ItemData> m_items = null;
    }
}
