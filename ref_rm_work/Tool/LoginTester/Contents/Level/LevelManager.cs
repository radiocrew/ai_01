using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using RM.Common;

using rm_login.Tool.Script;

namespace rm_login.Tool.Contents
{
    public class LevelManager
    {
        static private readonly Lazy<LevelManager> s_lazy = new Lazy<LevelManager>(() => new LevelManager());
        static public LevelManager Instance { get { return s_lazy.Value; } }

        public bool Initialize()
        {
            m_levelArr = Array.CreateInstance(typeof(ResJson_Level), CONST_DEFINE.TEST_MAX_PLAYER_LEVEL);

            for (int lv = 1; lv <= m_levelArr.Length; ++lv)
            {
                var res = ResourceManager.Instance.FindLevel(lv);
                if (null == res)
                {
                    Debug.Assert(false);
                    return false;
                }

                m_levelArr.SetValue(res, lv - 1);
            }

            m_activeLevelArr = Array.CreateInstance(typeof(ResJson_ActiveLevel), CONST_DEFINE.TEST_MAX_PLAYER_ACTIVE_LEVEL);

            for (int lv = 1; lv <= m_activeLevelArr.Length; ++lv)
            {
                var res = ResourceManager.Instance.FindActiveLevel(lv);
                if (null == res)
                {
                    Debug.Assert(false);
                    return false;
                }

                m_activeLevelArr.SetValue(res, lv - 1);
            }

            Console.WriteLine("[{0}] Level manager Initialize Completed", System.DateTime.Now.ToString("hh:mm:ss.fff"));
            return true;
        }

        public float GetExpPercent(float exp, int lv)
        {
            float beforeAccum = 0.0f;

            if (0 <= (lv - 2))
            {
                var before = m_levelArr.GetValue(lv - 2) as ResJson_Level;
                if (null != before)
                {
                    beforeAccum = before.AccumulatedExp;
                }
            }

            exp -= beforeAccum;

            float currentAccum = 0.0f;
            var current = m_levelArr.GetValue(lv - 1) as ResJson_Level;
            if (null != current)
            {
                currentAccum = current.AccumulatedExp;
            }
            
            return (exp / (currentAccum - beforeAccum)) * 100.0f;
        }

        public float GetActiveExpPercent(float exp, int lv)
        {
            float beforeAccum = 0.0f;

            if (0 <= (lv - 2))
            {
                var before = m_activeLevelArr.GetValue(lv - 2) as ResJson_ActiveLevel;
                if (null != before)
                {
                    beforeAccum = before.AccumulatedExp;
                }
            }

            exp -= beforeAccum;

            float currentAccum = 0.0f;
            var current = m_activeLevelArr.GetValue(lv - 1) as ResJson_ActiveLevel;
            if (null != current)
            {
                currentAccum = current.AccumulatedExp;
            }

            return (exp / (currentAccum - beforeAccum)) * 100.0f;
        }

        Array m_levelArr = null;
        Array m_activeLevelArr = null;
    }
}
