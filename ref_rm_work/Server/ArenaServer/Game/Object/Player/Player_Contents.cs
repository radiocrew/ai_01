using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;
using RM.Server.Common;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public partial class Player : ITimerHeartbeat
    {
        public override void OnUpdate(int id, double accumT, double dT)
        {
            base.OnUpdate(id, accumT, dT);

            //-todo. what you think, what you need and what you have to do
        }

        public override void OnDie()
        {
        }

        public override bool IncreaseExp(long exp)
        {
            if (true == Health.IncreaseExp(exp))
            {
                OnIncreaseExp();
                return true;
            }

            return false;
        }

        public override bool IncreaseLevel(int delta = 1)
        {
            if (true == Health.IncreaseLevel(delta))
            {
                OnIncreaseLevel();  
                return true;
            }

            return false;
        }

        private void OnIncreaseExp()
        {
            HealthNtf();
        }

        private void OnIncreaseLevel()
        {
            Stat.Calculate(this, true);
            //CalculateStat(true);   
        }
        /*
        public override void CalculateStat(bool ntf = false)
        {
            //-level 기준으로 한 base stat 
            var resBaseStats = BaseStatManager.Instance.GetStats(PlayerClassType, Health.Level);
            if (null == resBaseStats)
            {
                return;
            }

            float levelStatValue = 0;

            if (true == resBaseStats.TryGetValue(StatType.HPMax, out levelStatValue))
            {
                Stat.AddOrUpdate(StatType.HPMax, levelStatValue);

                float hpMax = 0.0f;
                if (true == Stat.GetValue(StatType.HPMax, out hpMax))
                {
                    HPResetSync((int)hpMax);
                }
            }

            levelStatValue = 0.0f;
            if (true == resBaseStats.TryGetValue(StatType.Attack, out levelStatValue))
            {
                Stat.AddOrUpdate(StatType.Attack, levelStatValue);
            }

            //-
            // some do set

            //-
            // some do set

            //-
            // some do set

            
            if (true == ntf)
            {
                HealthNtf();
                StatNtf();
            }
        }
        */
    }
}
