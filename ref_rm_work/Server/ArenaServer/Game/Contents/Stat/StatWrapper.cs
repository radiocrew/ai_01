using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;

namespace ArenaServer.Game
{
    public class StatWrapper
    {
        public bool Initialize()
        {
            m_stat = new Dictionary<StatType, float>();
            return true;
        }

        public void Destroy()
        {
            m_stat.Clear();
            m_stat = null;
        }




        Dictionary<StatType, float> m_stat = null;
    }
}
