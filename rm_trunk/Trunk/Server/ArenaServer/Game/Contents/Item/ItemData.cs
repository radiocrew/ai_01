using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    //-static data (server/client sync by script)
    public class ItemData
    {
        public ItemData(ResJson_Item resItem, ResJson_ItemStat resItemStat)
        {
            m_item = resItem;
            m_itemStat = resItemStat;
        }

        public bool Initialize()
        {
            return true;
        }

        public int ID 
        {
            get 
            {
                return m_item.ID;
            }
        }

        public ResJson_Item ResJsonItem => m_item;

        public ResJson_ItemStat ResJsonItemStat => m_itemStat;

        readonly ResJson_Item m_item = null;
        readonly ResJson_ItemStat m_itemStat = null;
    }
}
