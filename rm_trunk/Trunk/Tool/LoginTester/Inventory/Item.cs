using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rm_login.Tool
{
    public class Item
    {
        public Item(int id, Guid uid, int uiID)
        {
            m_id = id;
            m_uid = uid;
            m_uiID = uiID;
        }

        public int ID 
        {
            get
            {
                return m_id;
            }
        }

        public Guid UID 
        {
            get
            {
                return m_uid;
            }
        }

        public int UIID
        {
            get 
            {
                return m_uiID;
            }
        }

        public bool Equipped 
        {
            set 
            {
                m_equipped = value;
            }
            get 
            {
                return m_equipped;
            }
        }

        int m_id = 0;
        Guid m_uid;
        int m_uiID = 0;
        bool m_equipped = false;
    }
}
