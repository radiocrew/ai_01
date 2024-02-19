using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace rm_login.Tool
{
    public class Inventory
    {
        public bool Initialize()
        {
            m_items = new Dictionary<Guid, Item>();
            //m_equipItem = new Dictionary<Guid, ItemSlot>();
            return false;
        }

        public void Destroy()
        {
        }

        public Item Add(int itemID, Guid itemUID)
        {
            lock (m_itemLock)
            {
                if (true == m_items.ContainsKey(itemUID))
                {
                    Debug.Assert(false);
                    return null;
                }

                Item item = new Item(itemID, itemUID, SequenceID);
                m_items.Add(itemUID, item);

                return item;
            }
        }

        public Item FindItem(int uiID)
        {
            Item item = null;

            lock (m_itemLock)
            {
                foreach (var pair in m_items.ToList())
                {
                    if (uiID == pair.Value.UIID)
                    {
                        item = pair.Value;
                    }
                }
            }

            return item;
        }

        public Item FindItem(Guid uid)
        {
            Item item = null;

            lock (m_itemLock)
            {
                if (true == m_items.TryGetValue(uid, out item))
                {
                    return item;
                }
            }

            return null;
        }

        public bool Remove(Guid itemUID)
        {
            lock (m_itemLock)
            {
                return m_items.Remove(itemUID);
            }
        }

        public int SelectedID
        {
            set
            {
                lock (m_idLock)
                {
                    m_selectedID = value;
                }
            }
            get
            {
                lock (m_idLock)
                {
                    return m_selectedID;
                }
            }
        }

        private int SequenceID
        {
            get
            {
                int ret = 0;
                lock (m_idLock)
                {
                    ret = ++m_sequenceID;
                }

                return ret;
            }
        }

        public void Test()
        {
            //FormDelegate.Instance.ToUI(ToolDefine.UIType.Inventory, "1");
            //FormDelegate.Instance.ToUI(ToolDefine.UIType.Inventory, "2");
            //FormDelegate.Instance.ToUI(ToolDefine.UIType.Inventory, "3");
            //FormDelegate.Instance.ToUI(ToolDefine.UIType.Inventory, "4");
        }

        object m_itemLock = new object();
        Dictionary<Guid, Item> m_items;
        //Dictionary<Guid, ItemSlot> m_equipItem;

        object m_idLock = new object();
        int m_sequenceID = 0;
        int m_selectedID = 0;
    }
}
