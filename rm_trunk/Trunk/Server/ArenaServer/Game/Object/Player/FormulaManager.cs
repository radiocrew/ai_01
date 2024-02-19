using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;
using RM.Net;
using RM.Server.Common;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class FormulaManager
    {
        static public int CalcDamage(ArenaObject attacker, ResData_Skill jsonSkill)
        {
            //-여기에서, 다마게 공식

            int finalAtk = 0;

            int skillAtk = DamageMinMax(jsonSkill.Damage);
            finalAtk += skillAtk;

            float statAtk = .0f;
            if (true == attacker.Stat.GetValue(StatType.Attack, out statAtk))
            {
                finalAtk += (int)statAtk;
            }

            float statAtkAdd = .0f;
            if (true == attacker.Stat.GetValue(StatType.AttackAdd, out statAtkAdd))
            {
                finalAtk += (int)statAtkAdd;
            }

            float statAtkRate = .0f;
            if (true == attacker.Stat.GetValue(StatType.AttackRate, out statAtkRate))
            {
                //https://kdsoft-zeros.tistory.com/95
                var add = (int)Math.Round((double)(finalAtk * (statAtkRate / 100.0f)), MidpointRounding.AwayFromZero);
                finalAtk += add;
            }
                
            return finalAtk;
        }

        static public long CalcExp(ArenaObject arenaObject, long baseExp)
        {
            float expAddrate = 0.0f;
            if (true == arenaObject.Stat.GetValue(StatType.ExpAddRate, out expAddrate))
            {
                baseExp = (long)(baseExp * expAddrate);
            }

            return baseExp;
        }

        static public ulong CalcAttackSpeed(ArenaObject arenaObject)
        {
            ulong atkSpdMs = DEFINE.TEST_TIME_AUTO_ATTACK_INTERVAL_MS;
            
            float rate = 0.0f;
            if (true == arenaObject.Stat.GetValue(StatType.AttackSpeedRate, out rate))
            {
                rate *= 0.01f;
                atkSpdMs = (ulong)(atkSpdMs * (1.0f - rate));
            }

            //-attack interval 을 ms 단위로 반환 
            return atkSpdMs;
        }

        static int DamageMinMax(ResData_Damage damage)
        {
            return new Random((int)DateTime.Now.Ticks).Next(damage.DamageMin, damage.DamageMax + 1);
        }
    }
}
