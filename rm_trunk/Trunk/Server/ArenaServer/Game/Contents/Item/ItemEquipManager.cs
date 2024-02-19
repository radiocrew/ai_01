using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;

namespace ArenaServer.Game
{
    public class ItemEquipManager
    {
        public bool Initialize(List<ItemEquipSlotType> slotTypeList = null)
        {
            m_equipSlots = new Dictionary<ItemEquipSlotType, ItemEquipSlot>();

            foreach (var e in Enum.GetValues(typeof(ItemEquipSlotType)).Cast<ItemEquipSlotType>())
            {
                var itemEquipSlot = new ItemEquipSlot();
                itemEquipSlot.Initialize(e);

                m_equipSlots.Add(e, itemEquipSlot);
            }
            
            return false;
        }

        public void Destroy()
        {
            m_equipSlots.Clear();
            m_equipSlots = null;
        }

        public bool Equip(Item item)
        {
            lock (m_equipLock)
            {
                var equipSlotType = item.Data.ResJsonItem.EquipSlotType;
                var itemID = item.Data.ID;
                var itemUID = item.UID;

                if (false == CanEquip(equipSlotType, itemID, itemUID))
                {
                    return false;
                }

                if (true == IsEquipped(itemUID))
                {
                    return false;
                }

                ItemEquipSlot itemEquipSlot = null;
                if (false == m_equipSlots.TryGetValue(equipSlotType, out itemEquipSlot))
                {
                    return false;
                }

                itemEquipSlot.SetSlot(itemUID, itemID);
                return true;
            }
        }

        public bool UnEquip(Item item)
        {
            lock (m_equipLock)
            {
                var equipSlotType = item.Data.ResJsonItem.EquipSlotType;

                ItemEquipSlot itemEquipSlot = null;
                if (true == m_equipSlots.TryGetValue(equipSlotType, out itemEquipSlot))
                {
                    if (itemEquipSlot.ItemUID == item.UID)
                    {
                        itemEquipSlot.ResetSlot();
                        return true;
                    }
                }
                
                return false;
            }
        }

        public void Foreach(Action<ItemEquipSlot> action)
        {
            lock (m_equipLock)
            {
                foreach (var slot in m_equipSlots)
                {
                    action(slot.Value);
                }
            }
        }

        private bool IsEquipped(Guid uid)
        {
            return m_equipSlots.Any(pair => {

                if ((false == pair.Value.IsEmpty()) && (pair.Value.ItemUID == uid))
                {
                    return true;
                }

                return false;
            });   
        }

        private bool CanEquip(ItemEquipSlotType slotType, int itemID, Guid uid)
        {
            ItemEquipSlot slot = null;
            if (false == m_equipSlots.TryGetValue(slotType, out slot))
            {
                return false;
            }

            if (false == slot.IsEmpty())
            {
                return false;
            }

            return true;
        }

        object m_equipLock = new object();
        Dictionary<ItemEquipSlotType, ItemEquipSlot> m_equipSlots = null;
    }
}
