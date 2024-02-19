using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using RM.Net;

namespace ArenaServer.Game
{
    public class ActiveLevel
    {
        public bool Initialize()
        {
            Reset();
            return true;
        }

        public void Destroy()
        {
            //-DB처리 안함.
            //
        }

        public void Reset()
        {
            Interlocked.Exchange(ref m_level, 1);
            Interlocked.Exchange(ref m_exp, 0);
        }

        public ACTIVE_LEVEL SerializeData()
        {
            ACTIVE_LEVEL data = new ACTIVE_LEVEL();
            data.Exp = Exp;
            data.Level = Level;

            return data;
        }

        public bool IncreaseLevel(int delta = 1)
        {
            if (0 < delta)
            {
                Interlocked.Add(ref m_level, delta);
                return true;
            }

            return false;
        }

        public void OnIncreaseLevel(Player player)
        {
            //-여기에서 active level에 대한 스탯 계산/알림

            ActiveLevelNtf(player);
        }

        public bool IncreaseExp(long exp)
        {
            if (0 < exp)
            {
                Interlocked.Add(ref m_exp, exp);
                return true;
            }

            return false;
        }

        public void OnIncreaseExp(Player player)
        {
            //-여기에서 active level에 대한 스탯 계산/알림

            ActiveLevelNtf(player);
        }

        private void ActiveLevelNtf(Player player)
        {
            ACTIVE_LEVEL_NTF ntf = new ACTIVE_LEVEL_NTF();
            ntf.UID = player.UID;
            ntf.ActiveLevelData = SerializeData();

            var arena = player.Arena;
            if (null != arena)
            {
                arena.BroadCasting(ntf);
            }
        }

        public int Level => Interlocked.CompareExchange(ref m_level, 0, 0);

        public long Exp => Interlocked.CompareExchange(ref m_exp, 0, 0);
 

        //-properties,
        int m_level = 0;
        long m_exp = 0;
    }
}
