using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace ArenaServer.Game
{
    public enum ESwitch : int
    { 
        Off = 0,
        On = 1,
    }

    public class AutoSwitch
    {
        public bool Switch(ESwitch onoff)
        {
            int old = m_switch;
            Interlocked.CompareExchange(ref m_switch, (int)onoff, (int)((onoff == ESwitch.On) ? (ESwitch.Off) : (ESwitch.On)));

            var ret = (old != (int)onoff);
            return ret;
        }

        public bool Is(ESwitch onoff)
        {
            return (m_switch == (int)onoff);
        }

        volatile int m_switch = 0;// 0 for false, 1 for true
    }
}
