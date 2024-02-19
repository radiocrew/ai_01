using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;

namespace ArenaServer.Game
{
    public class ItemEquipSlot
    {
        public bool Initialize(ItemEquipSlotType slotType)
        {
            m_slotType = slotType;
            return false;
        }

        public void Destory()
        {
            ResetSlot();
        }

        public bool SetSlot(Guid uid, int id)
        {
            m_itemUID = uid;
            m_itemID = id;
            return false;
        }

        public void ResetSlot()
        {
            m_itemUID = Guid.Empty;
            m_itemID = 0;
        }

        public bool IsEmpty()
        {
            return (m_itemUID == Guid.Empty);
        }

        public Guid ItemUID => m_itemUID;
        public int ItemID => m_itemID;
        

        ItemEquipSlotType m_slotType = ItemEquipSlotType.None;
        Guid m_itemUID;
        int m_itemID = 0;
    }
}
