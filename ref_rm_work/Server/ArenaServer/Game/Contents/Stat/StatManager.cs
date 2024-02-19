using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;

namespace ArenaServer.Game
{
    public class StatManager
    {
        static public void ApplyStat(ref Dictionary<StatType, float> ret, Dictionary<StatType, float> add)
        {
            foreach (var pair in add)
            {
                StatType addStatType = pair.Key;
                float addStatValue = pair.Value;

                ApplyStat(ref ret, addStatType, addStatValue);
            }
        }

        static public void ApplyStat(ref Dictionary<StatType, float> ret, StatType addStatType, float addStatValue)
        {
            float retValue = 0.0f;
            ret.TryGetValue(addStatType, out retValue);

            ret[addStatType] = retValue + addStatValue;//-jinsub, generic해진 stat 구조의 max value는 어떻게 뽀바낼겉인가...
        }

        static public void CalculateStat(Player player, bool ntf = false)
        {
            var baseStats = BaseStatManager.Instance.GetStats(player.PlayerClassType, player.Health.Level);
            if (null == baseStats)
            {
                return;
            }

            //#1 base stat

            player.Stat.Clear();

            baseStats.All(pair => {
                player.Stat.AddOrUpdate(pair.Key, pair.Value);
                return true;
            });

            //#2 item stat

            player.Stat.Clear(StatBonusType.Item);

            player.ItemEquipManager.Foreach((itemEquipSlot) => {

                //-caution!!! under lock (m_equipLock) here.

                var itemData = ItemManager.Instance.FindItem(itemEquipSlot.ItemID);
                if (null != itemData)
                {
                    itemData.ResJsonItemStat.Stats.All(stat => {
                        player.Stat.AddOrUpdate(stat.StatType, stat.Value, StatBonusType.Item);

                        return true;
                    });
                }
            });

            //#3 build (merge)

            player.Stat.Build();

            //-↓↓↓ 이하 별도 처리를 해야하는 것들, ↓↓↓

            float hpMaxAdd = 0.0f;
            player.Stat.GetValue(StatType.HPMaxAdd, out hpMaxAdd);
            
            float hpMax = 0.0f;
            if (true == player.Stat.GetValue(StatType.HPMax, out hpMax))
            {
                player.HPResetSync((int)(hpMax + hpMaxAdd));
            }

            if (true == ntf)
            {
                player.HealthNtf();
                player.StatNtf();
            }
        }
    }
}
