using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaServer.Game
{
    public partial class Npc
    {
        public void GiveExp()
        {
            var attacker = m_arena.ObjectList.FindObject(m_attackerList.GetLast());
            if (null != attacker)
            {
                LevelExperienceManager.Instance.Give(attacker, NpcAttribute.Exp);
            }
        }

    }
}
