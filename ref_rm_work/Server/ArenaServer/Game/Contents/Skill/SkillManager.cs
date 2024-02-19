using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level9.Expedition.Mobile.Util;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class SkillManager : Singleton<SkillManager>
    {
        public bool Initialize()
        {
            m_skills = new Dictionary<int, Skill>();

            var resSkillAttacks = ResourceManager.Instance.JsonSkillAttack.Values.ToList();
            foreach (var resSkillAttack in resSkillAttacks)
            {
                if (true == m_skills.ContainsKey(resSkillAttack.SkillID))
                {
                    return false;    
                }

                m_skills.Add(resSkillAttack.SkillID, new SkillAttack(resSkillAttack));
            }

            var resSkillProjectiles = ResourceManager.Instance.JsonSkillProjectile.Values.ToList();
            foreach (var resSkillProjectile in resSkillProjectiles)
            {
                if (true == m_skills.ContainsKey(resSkillProjectile.SkillID))
                {
                    return false;
                }

                m_skills.Add(resSkillProjectile.SkillID, new SkillProjectile(resSkillProjectile));
            }

            return true;
        }

        public void Destroy()
        {
        }

        public Skill FindSkill(int skillID)
        {
            Skill skill = null;
            if (true == m_skills.TryGetValue(skillID, out skill))
            {
                return skill;
            }

            return null;
        }

        private SkillManager()
        {
        }

        Dictionary<int, Skill> m_skills = null;
    }
}
