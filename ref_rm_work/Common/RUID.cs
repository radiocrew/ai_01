using System;

//using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization;

using ProtoBuf;

namespace RM.Common
{
    [ProtoContract]
    public sealed class RUID : IEquatable<RUID>//, ISerializable
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

        public bool Generate()
        {
            //-이미 만들어져 있다면 재생성 안됨.
            if (m_guid == Guid.Empty)
            {
                m_guid = Guid.NewGuid();
                return true;
            }

            return false;
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
            return m_guid.GetHashCode();
        }

        static public RUID NewRuid() => new RUID(Guid.NewGuid());

        static public RUID StringToRUID(string uid)
        {
            Guid result;
            if (true == Guid.TryParse(uid, out result))
            {
                return new RUID(result);
            }

            return null;
        }

        public static bool operator ==(RUID lhs, RUID rhs)
        {
            if ((false == object.ReferenceEquals(lhs, null)) && (false == object.ReferenceEquals(rhs, null)))
            {
                return lhs.Equals(rhs);
            }

            return false;
        }

        public static bool operator !=(RUID lhs,  RUID rhs)
        {
            if ((false == object.ReferenceEquals(lhs, null)) && (false == object.ReferenceEquals(rhs, null)))
            {
                return (false == lhs.Equals(rhs));
            }

            return true;
        }

        public static explicit operator RUID(Guid guid) => new RUID(guid);
        public static explicit operator Guid(RUID uid) => uid.m_guid;

        [ProtoMember(1)]
        private Guid m_guid;
    }
}
