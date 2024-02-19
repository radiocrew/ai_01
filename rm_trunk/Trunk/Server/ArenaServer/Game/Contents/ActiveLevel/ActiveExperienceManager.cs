using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level9.Expedition.Mobile.Util;

using RM.Common;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class ActiveLevelExperienceManager : Singleton<ActiveLevelExperienceManager>
    {
        public bool Initialize()
        {
            return InitializeActiveLevel();
        }

        public bool Give(Player player, long exp)
        {
            if (0 < exp)
            {
                if (true == player.ActiveLevel.IncreaseExp(exp))
                {
                    player.ActiveLevel.OnIncreaseExp(player);
                    CheckAndLevelUp(player);
                    return true;
                }
            }

            return false;
        }

        private void CheckAndLevelUp(Player player)
        {
            var currentActiveLevel = player.ActiveLevel.Level;
            var currentActiveExp = player.ActiveLevel.Exp;

            var requiredActiveExp = AccumulatedActiveExp(currentActiveLevel);
            if (0 > requiredActiveExp)
            {
                return;
            }

            while (requiredActiveExp < currentActiveExp)
            {
                var nextActiveLevel = currentActiveLevel + 1;
                requiredActiveExp = AccumulatedActiveExp(nextActiveLevel);
                if (0 > requiredActiveExp)
                {
                    break;
                }

                if (true == player.ActiveLevel.IncreaseLevel())
                {
                    player.ActiveLevel.OnIncreaseLevel(player);
                }

                currentActiveLevel = player.ActiveLevel.Level;
                currentActiveExp = player.ActiveLevel.Exp;
            }
        }

        public long AccumulatedActiveExp(int level)
        {
            if ((0 < level) && (level <= m_activeLevelArr.Length))
            {
                var res = m_activeLevelArr.GetValue(level - 1) as ResJson_ActiveLevel;
                if (null != res)
                {
                    return res.AccumulatedExp;
                }
            }

            return -1;
        }

        private bool InitializeActiveLevel()
        {
            m_activeLevelArr = Array.CreateInstance(typeof(ResJson_ActiveLevel), CONST_DEFINE.TEST_MAX_PLAYER_ACTIVE_LEVEL);

            for (int lv = 1; lv <= m_activeLevelArr.Length; ++lv)
            {
                var res = ResourceManager.Instance.FindActiveLevel(lv);
                if (null == res)
                {
                    return false;
                }

                m_activeLevelArr.SetValue(res, lv - 1);
            }

            return true;
        }

        private ActiveLevelExperienceManager()
        {
        }

        Array m_activeLevelArr = null;
    }
}
