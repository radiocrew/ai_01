using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArenaServer.Resource;

namespace ArenaServer.Game.AI
{
    public class AiNpc : AiFSM
    {
        public AiNpc(ArenaObject arenaObject)
        {
            m_blackBoard = new BlackBoard(arenaObject);
        }

        public bool Initialize(int aiBagID, int skillBagID)
        {
            //-here m_blackBoard init...
            //

            var resNpcSkillBag = ResourceManager.Instance.FindNpcSkillBag(skillBagID);
            m_blackBoard.Initialize((null != resNpcSkillBag) ? resNpcSkillBag.NpcSkillList : null);

            return AiFSMFactory.Instance.Build(aiBagID, this, m_blackBoard);
        }

        public BlackBoard BlackBoard { get => m_blackBoard; }

        BlackBoard m_blackBoard = null;
    }
}
