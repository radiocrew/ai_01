using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RND
{
    public class UID
    {
        public static implicit operator Guid(UID uid) => uid.m_guid;
        public static implicit operator UID(Guid guid) => new UID(guid);

        public UID()
        {
        }

        public UID(Guid guid)
        {
            m_guid = guid;
        }
        
        public void Generate()
        {
            m_guid = Guid.NewGuid();
        }

        private Guid m_guid = new Guid();
    }

}
