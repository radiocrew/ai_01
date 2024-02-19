using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using RM.Common;

namespace rm_login.Tool.Contents
{
    public class Stat
    {
        public bool Initialize()
        {
            m_stats = new Dictionary<StatType, float>();
            return false;
        }

        public void AddOrUpdate(Dictionary<StatType, float> stats)
        {
            if (null == stats)
            {
                return;
            }

            lock (m_lock)
            {
                ClearStat();
                
                stats.All(pair => {
                    m_stats.Add(pair.Key, pair.Value);
                    return true;
                });

                UpdateStat();
            }
        }

        private void ClearStat()
        {
            var keyList = m_stats.Keys.ToList();
            foreach (var key in keyList)
            {
                m_stats[key] = 0.0f;
            }

            UpdateStat();
            m_stats.Clear();
        }

        private void UpdateStat()
        {
            foreach (var stat in m_stats)
            {
                float value = stat.Value;

                switch (stat.Key)
                {
                    case StatType.HPMax:
                        Player.Instance.HPMax = (int)value;
                        //FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerHpMax, Player.Instance.HPMax.ToString());
                        break;
                    case StatType.Attack:
                        Player.Instance.Atk = (int)value;
                        FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerAtk, Player.Instance.Atk.ToString());
                        break;
                    case StatType.AttackSpeedRate:
                        Player.Instance.AtkSpdRate = (float)value;
                        FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerAtkSpdRate, Player.Instance.AtkSpdRate.ToString());
                        break;
                    case StatType.VampireRate:
                        Player.Instance.VampireRate = (float)value;
                        FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerVampireRate, Player.Instance.VampireRate.ToString());
                        break;
                    case StatType.VigorRecoveryRate:
                        Player.Instance.VigorRecoveryRate = (float)value;
                        FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerVigorRecoveryRate, Player.Instance.VigorRecoveryRate.ToString());
                        break;
                    case StatType.ExpAddRate:
                        Player.Instance.ExpAddRate = (float)value;
                        FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerExpAddRate, Player.Instance.ExpAddRate.ToString());
                        break;
                    case StatType.HPMaxAdd:
                        Player.Instance.HPMaxAdd = (int)value;
                        FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerHpMaxAdd, Player.Instance.HPMaxAdd.ToString());
                        break;
                    case StatType.AttackAdd:
                        Player.Instance.AtkAdd = (int)value;
                        FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerAtkAdd, Player.Instance.AtkAdd.ToString());
                        break;
                    case StatType.AttackRate:
                        Player.Instance.AtkRate = (float)value;
                        FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerAtkRate, Player.Instance.AtkRate.ToString());
                        break;
                    default:
                        //make ui. now
                        Debug.Assert(false);
                        break;
                }
            }
        }

        private bool GetValue(StatType type, out float value)
        {
            lock (m_lock)
            {
                return m_stats.TryGetValue(type, out value);
            }
        }

        object m_lock = new object();
        Dictionary<StatType, float> m_stats;
    }
}
