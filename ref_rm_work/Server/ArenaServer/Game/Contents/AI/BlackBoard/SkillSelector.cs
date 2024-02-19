using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;
using ArenaServer.Resource;

namespace ArenaServer.Game.AI
{
    public class SkillSelector
    {
        public SkillSelector()
        {
        }

        public bool Initialize(List<ResData_NpcSkill> list)
        {
            lock (m_lock)
            {
                m_resNpcSkillList = new List<ResData_NpcSkill>();
                if (null != list)
                {
                    m_resNpcSkillList = list.ToList();
                    Select();
                }

                return true;
            }
        }

        public void Destory()
        {
            lock (m_lock)
            {
                m_resNpcSkillList.Clear();
            }
        }

        public int GetSkillID()
        {
            int skillID = 0;
            lock (m_lock)
            {
                skillID = m_skillID;
                Select();
            }
            return skillID;
        }

        public float GetCoolTime()
        {
            float coolTime = 1.0f;
            lock (m_lock)
            {
                coolTime = m_coolTime;
            }
            return coolTime;
        }

        private void Select()
        {
            var count = m_resNpcSkillList.Count();
            if (0 < count)
            {
                var rand = RMMath.Random(count);

                m_skillID = m_resNpcSkillList.ElementAt(rand).SkillID;
                m_coolTime = m_resNpcSkillList.ElementAt(rand).CoolTime;
            }
        }

        object m_lock = new object();
        int m_skillID = 0;
        float m_coolTime = 0.0f;
        List<ResData_NpcSkill> m_resNpcSkillList = null;
    }
}
