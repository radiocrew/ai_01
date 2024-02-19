using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaServer.Game
{
    public partial class Arena
    {
        public virtual void OnUpdate(int id, double accumT, double dT)
        {
            //Console.WriteLine("Arena called : [{0}] accumt[{1}] dt[{2}]", System.DateTime.Now.ToString("hh:mm:ss.fff"), accumT, dT);
            m_map.OnUpdate((float)dT);
        }
    }
}
