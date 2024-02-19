using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;
using RM.Net;

namespace ArenaServer.Game
{
    public class Health
    {
        public Health()
        {

            //스펠 작업 -> 공격력 처리 -> 레벨 당 공격력 올리고 테스트  testcode,
        }

        public bool Initialize(HEALTH_DATA data)
        {
            lock (m_lock)
            {
                m_hp = data.Hp;
                m_hpMax = data.HpMax;
                m_mp = data.Mp;
                m_mpMax = data.MpMax;
                m_level = data.Level;
                m_levelMax = m_level;
                m_exp = data.Exp;
            }

            return true;
        }

        public HEALTH_DATA SerializeData()
        {
            var data = new HEALTH_DATA();

            lock (m_lock)
            {
                data.Hp = m_hp;
                data.HpMax = m_hpMax;
                //data.Mp = m_mp;
                //data.MpMax = m_mpMax;
                data.Level = m_level;
                data.Exp = m_exp;
            }

            return data;
        }

        public void Destory()
        {
            lock (m_lock)
            {
                m_hp = 0;
                m_hpMax = 0;

                //m_mp = 0;
                //m_mpMax = 0;

                m_level = 0;
                m_levelMax = 0;

                m_exp = 0;
            }
        }

        public int HP
        {
            get
            {
                lock (m_lock) { return m_hp; }
            }
        }

        public int HPMax
        {
            get
            {
                lock (m_lock) { return m_hpMax; }
            }
        }

        public int MP
        {
            get
            {
                lock (m_lock) { return m_mp; }
            }
        }

        public int MPMax
        {
            get
            {
                lock (m_lock) { return m_mpMax; }
            }
        }

        public long Exp
        {
            get
            {
                lock (m_lock) { return m_exp; }
            }
        }

        public int Level
        {
            get
            {
                lock (m_lock) { return m_level; }
            }
        }

        public int IncreaseHP(int value)
        {
            lock (m_lock)
            {
                m_hp = RMMath.Clamp(m_hp + value, 0, m_hpMax);
                return m_hp;
            }   
        }

        public void ResetHP(int value)
        {
            lock (m_lock)
            {
                if (0 < value)
                {
                    m_hp = value;
                    m_hpMax = value;
                }
            }
        }

        public bool IncreaseExp(long exp)
        {
            lock (m_lock)
            {
                var before = m_exp;

                m_exp += exp;
                m_exp = Math.Max(0, m_exp);

                if (before != m_exp)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IncreaseLevel(int delta = 1)
        {
            lock (m_lock)
            {
                var before = m_level;
                m_level += delta;

                if (before != m_level)
                {
                    m_level = RMMath.Clamp(m_level, 1, CONST_DEFINE.TEST_MAX_PLAYER_LEVEL);
                    m_levelMax = Math.Max(m_levelMax, m_level);

                    return true;
                }
            }

            return false;
        }


        //-intergrate lock for health data
        object m_lock = new object();

        int m_hp = 0;
        int m_hpMax = 0;

        int m_mp = 0;
        int m_mpMax = 0;

        int m_level = 0;
        int m_levelMax = 0;
        long m_exp = 0;
    }
}
