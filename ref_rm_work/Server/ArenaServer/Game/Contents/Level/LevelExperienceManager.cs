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
    public class LevelExperienceManager : Singleton<LevelExperienceManager>
    {
        public bool Initialize()
        {
            return InitializeLevel();
        }

        public bool Give(ArenaObject arenaObject, long exp)
        {
            exp = FormulaManager.CalcExp(arenaObject, exp);

            if (true == arenaObject.IncreaseExp(exp))
            {
                AdjustLevel(arenaObject);
                return true;
            }

            return false;
        }

        private void AdjustLevel(ArenaObject arenaObject)
        {
            var level = arenaObject.Health.Level;

            var requiredExp = AccumulatedExp(level);
            if (0 > requiredExp)
            {
                return;
            }

            var currentExp = arenaObject.Health.Exp;
            if (requiredExp < currentExp)
            {
                LevelUp(arenaObject);
                return;
            }

            var pastExp = AccumulatedExp(level - 1);
            if (currentExp <= pastExp)
            {
                LevelDown(arenaObject);
                return;
            }
        }

        private void LevelUp(ArenaObject arenaObject)
        {
            var currentLevel = arenaObject.Health.Level;
            var currentExp = arenaObject.Health.Exp;
            var requiredExp = AccumulatedExp(currentLevel);
            if (0 > requiredExp)
            {
                return;
            }

            while (requiredExp < currentExp)
            {
                var nextLevel = currentLevel + 1;
                requiredExp = AccumulatedExp(nextLevel);
                if (0 > requiredExp)
                {
                    break;
                }

                arenaObject.IncreaseLevel();

                currentLevel = arenaObject.Health.Level;
                currentExp = arenaObject.Health.Exp;
            }
        }

        private void LevelDown(ArenaObject arenaObject)
        {
            var currentLevel = arenaObject.Health.Level;
            var currentExp = arenaObject.Health.Exp;
            var requiredExp = AccumulatedExp(currentLevel);
            if (0 > requiredExp)
            {
                return;
            }

            while ((requiredExp > currentExp) && (1 < currentLevel))
            {
                arenaObject.IncreaseLevel(-1);

                currentLevel = arenaObject.Health.Level;
                requiredExp = AccumulatedExp(currentLevel - 1);
                if (0 > requiredExp)
                {
                    break;
                }

                currentExp = arenaObject.Health.Exp;
            }
        }
        private long AccumulatedExp(int level)
        {
            if ((0 < level) && (level <= m_levelArr.Length))
            {
                var res = m_levelArr.GetValue(level - 1) as ResJson_Level;
                if (null != res)
                {
                    return res.AccumulatedExp;
                }
            }

            return -1;
        }

        private bool InitializeLevel()
        {
            m_levelArr = Array.CreateInstance(typeof(ResJson_Level), CONST_DEFINE.TEST_MAX_PLAYER_LEVEL);

            for (int lv = 1; lv <= m_levelArr.Length; ++lv)
            {
                var res = ResourceManager.Instance.FindLevel(lv);
                if (null == res)
                {
                    return false;
                }

                m_levelArr.SetValue(res, lv - 1);
            }

            return true;
        }

        private LevelExperienceManager()
        {
        }


        Array m_levelArr = null;
    }
}
