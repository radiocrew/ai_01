using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace rm_login.Tool
{
    public class FormInitialize
    {
        public FormInitialize(Form1 form)
        {
            m_form = form;
        }

        public bool Initialize()
        {
            m_form.m_connectTimer.Tick += m_form.timer_Tick;
            m_form.m_connectTimer.Interval = 1500;
            m_form.m_connectTimer.Start();

            m_form.m_heartBeatTimer.Tick += m_form.heatbeat_Tick;
            m_form.m_heartBeatTimer.Interval = 1000;
            m_form.m_heartBeatTimer.Start();

            m_form.m_matchingTimer = new System.Threading.Timer(new TimerCallback(m_form.matching_Tick));

            m_form.accountUidTextBox4.Text = Player.Instance.UID.ToString();

            InitArenaList();
            SystemGreeting();

            return true;
        }

        void InitArenaList()
        {
            m_form.singleArenaComboBox.Text = "--- select ---";
            m_form.singleArenaComboBox.Items.Add("1");
            m_form.singleArenaComboBox.Items.Add("2");
            m_form.singleArenaComboBox.Items.Add("666");

            m_form.raidArenaComboBox.Items.Add("3");
            m_form.raidArenaComboBox.Items.Add("4");
            m_form.raidArenaComboBox.SelectedIndex = 0;
        }


        void SystemGreeting()
        {
            m_form.rtb_log.SelectionColor = Color.Green;
            m_form.rtb_log.SelectionFont = new Font(Control.DefaultFont, FontStyle.Bold);

            m_form.rtb_log.AppendText(Environment.OSVersion.Platform.ToString() + ", " + Environment.OSVersion.Version.ToString());
            m_form.rtb_log.AppendText(Environment.NewLine + "System Ready.");

            m_form.rtb_log.SelectionFont = new Font(Control.DefaultFont, FontStyle.Regular);
        }

        Form1 m_form = null;
    }
}
