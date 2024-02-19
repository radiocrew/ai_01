using System;

namespace RM.Server.Common
{
    public sealed class RUID : IEquatable<RUID>
    {
        public static readonly RUID Empty;

        public RUID()
        {
        }

        public RUID(Guid uid)
        {
            m_guid = uid;
        }

        public RUID(string uid)
        {
            Guid result;
            if (true == Guid.TryParse(uid, out result))
            {
                m_guid = result;
            }
        }

        public bool IsEmpty()
        {
            return (m_guid == Guid.Empty);
        }

        public void Generate()
        {
            m_guid = Guid.NewGuid();
        }

        public override string ToString()
        {
            return m_guid.ToString();
        }
        
        public override bool Equals(object rhs)
        {
            if (true == this.Equals(rhs as RUID))
            {
                return true;
            }

            return base.Equals(rhs);
        }

        public bool Equals(RUID obj)
        {
            var ret = m_guid.Equals(obj.m_guid);
            return ret;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        static public RUID NewRuid() => new RUID(Guid.NewGuid());

        static public RUID ToRUID(string uid)
        {
            Guid result;
            if (true == Guid.TryParse(uid, out result))
            {
                return new RUID(result);
            }

            return null;
        }

        public static bool operator == (RUID lhs, RUID rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(RUID lhs, RUID rhs)
        {
            return (false == (lhs == rhs));
        }

        public static explicit operator RUID(Guid guid) => new RUID(guid);
        public static explicit operator Guid(RUID uid) => uid.m_guid;

        private Guid m_guid;
    }
}
