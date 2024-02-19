using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ArenaServer.Net;
using ArenaServer.Resource;

using RM.Common;
using RM.Server.Common;

namespace ArenaServer.Game
{
    public class AutoSkill
    {
        public void Initialize(Player player)
        {
            m_player = player;
        }

        public void Switch(ESwitch onoff)
        {
            if ((true == m_switch.Switch(onoff)) && (ESwitch.On == onoff))
            {
                m_skillID = DEFINE.TEST_AUTO_SKILL_ID;
                DoAction();
            }
        }

        private void DoAction()
        {
            if (true == m_switch.Is(ESwitch.Off))
            {
                return;
            }

            SkillParam skillParam = new SkillParam();

            //-testcode, cast 시간과 invoke 시간을 동시에 rating 해야 한다. 
            ulong atkSpd = FormulaManager.CalcAttackSpeed(m_player);
           
            m_player.CastSkill(m_skillID, skillParam);
            DelayedTask dt = new DelayedTask(DoAction);
            dt.Submit(atkSpd, (int)TimerDispatcherIDType.PlayerAutoSkill);

            Console.WriteLine(string.Format("cast skill!! in arena [{0}]", m_player.ArenaID)); 
        }

        public void Destory()
        {
            m_switch.Switch(ESwitch.Off);
            //m_player = null; -jinsub, Destory 시켰지만, 타이머큐에 있던 N시간 이후에 스킬이 발동되면, m_player 가 null 참조될 수 있음.
        }

        public int ID
        {
            get
            {
                return m_skillID;   
            }
            set
            {
                m_skillID = value;   
            }
        }
        
        volatile int m_skillID = 0;

        AutoSwitch m_switch = new AutoSwitch();
        Player m_player = null;
    }
}
