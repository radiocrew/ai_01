using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;

namespace ArenaServer.Resource
{
    public class ResourceValid
    {
        public bool Initialize()
        {
            foreach (StatType type in (StatType[])Enum.GetValues(typeof(StatType)))
            {
                m_statTypes.Add(type);
            }

            return false;
        }

        public bool IsStat(StatType type)
        {
            return m_statTypes.Contains(type);
        }

        public void Destroy()
        {
        }


        HashSet<StatType> m_statTypes = new HashSet<StatType>();
    }
}
