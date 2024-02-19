using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
       
using RM.Net;
using RM.Common;

using rm_login.Network;
using rm_login.Tool;
using rm_login.Tool.Contents;
using rm_login.Tool.Script;

namespace rm_login
{
    public partial class Form1 : Form
    {
        public System.Windows.Forms.Timer m_connectTimer = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer m_heartBeatTimer = new System.Windows.Forms.Timer();
        public System.Threading.Timer m_matchingTimer = null;
        public System.Threading.Timer m_initTimer = null;

        private FormInitialize m_form = null;

        public Form1()
        {
            InitializeComponent();

            m_form = new FormInitialize(this);
            m_form.Initialize();
        }

        public void Initialize()
        {
            m_initTimer = new System.Threading.Timer((obj) => {

                Player.Instance.Inventory.Test();   

                m_initTimer.Dispose();
            }, null, 1000, 0);
        }

        public void LogFunction(string log, Color? color = null)
        {
            string text = Environment.NewLine + DateTime.Now.ToString("T") + " : ";
            text += log;

            Action<string, Color> action = (t, c) => {

                rtb_log.SelectionColor = c;
                rtb_log.AppendText(t);
                rtb_log.ScrollToCaret();
            };

            if (true == rtb_log.InvokeRequired)
            {
                rtb_log.Invoke((MethodInvoker)delegate {

                    action(text, color ?? Color.Black);
                });
            }
            else
            {
                action(text, color ?? Color.Black);
            }
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            if (false == ArenaServerSession.Instance.IsConnected)
            {
                LogDelegate.Instance.Log("conneting " + Player.sArenaIP);

                ArenaServerSession.Instance.RemoteEndPoint = Player.sArenaIP;
                ArenaServerSession.Instance.Connect();
            }
        }

        public void heatbeat_Tick(object sender, EventArgs e)
        {
            HEART_BEAT packet = new HEART_BEAT();
            ArenaServerSession.Instance.SendPacket(packet);
        }

        public void matching_Tick(object state)
        {
            if (pgb_matching.Value == pgb_matching.Maximum)
            {
                m_matchingTimer.Change(Timeout.Infinite, Timeout.Infinite);
                pgb_matching.Value = 0;

                Player.Instance.CancelMatching();
                return;
            }

            pgb_matching.Invoke((MethodInvoker)delegate {
                pgb_matching.PerformStep();
            });
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            if (0 < rtb_item_list.Lines.Count())
            {
                int firstcharindex = rtb_item_list.GetFirstCharIndexOfCurrentLine();
                int currentline = rtb_item_list.GetLineFromCharIndex(firstcharindex);
                string currentlinetext = rtb_item_list.Lines[currentline];
                rtb_item_list.Select(firstcharindex, currentlinetext.Length);
                //LogDelegate.Instance.Log(currentlinetext);
            }   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (false == ArenaServerSession.Instance.IsConnected)
            {
                LogDelegate.Instance.Log("not connected.");
            }
            else
            {
                Player.Instance.Login(ToolDefine.TEST_PLAYER_OBJECT_ID);
                LogDelegate.Instance.Log(string.Format("[>] login req"));
            }       
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((0 == tbx_x.Text.Length) || (0 == tbx_z.Text.Length) || (0 == tbx_d.Text.Length))
            {
                LogDelegate.Instance.Log(string.Format("fill a all position box."), Color.Red);
                return;
            }

            decimal x = 0;
            if (false == decimal.TryParse(tbx_x.Text, out x))
            {    
                return;
            }

            decimal z = 0;
            if (false == decimal.TryParse(tbx_z.Text, out z))
            {
                return;
            }

            decimal d = 0;
            if (false == decimal.TryParse(tbx_d.Text, out d))
            {
                return;
            }

            var fx = Convert.ToSingle(x);
            var fy = Convert.ToSingle(0.0f);
            var fz = Convert.ToSingle(z);
            var fd = Convert.ToSingle(d);


            MapManager.Instance.PositionClamp(fx, fz, out fx, out fz);
            
            Player.Instance.MoveNtf(fx, fy, fz, fd);

            LogDelegate.Instance.Log(string.Format("[>] move x[{0}], z[{1}], dir[{2}]", fx, fz, fd));
        }

        private void btn_suicide(object sender, EventArgs e)
        {
            Player.Instance.TestCommand(TestCommandType.PlayerSuicide);
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //-UID
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //-Instance ID
        }

        private void btn_StartAutoAttack_Click(object sender, EventArgs e)
        {
            Player.Instance.StartAttack();
        }

        private void btn_StopAutoAttack_Click(object sender, EventArgs e)
        {
            Player.Instance.StopAttack();
        }

        private void btn_OnceAttack_Click(object sender, EventArgs e)
        {
            Player.Instance.CastSkill();
        }

        private void arena_move_Enter(object sender, EventArgs e)
        {
        }

        private void singleArenaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int areanID = Int32.Parse(singleArenaComboBox.SelectedItem as string);

            Player.Instance.SingleArenaID = areanID;
            LogDelegate.Instance.Log(string.Format("Selected single arena id[{0}]", Player.Instance.SingleArenaID));
        }

        private void arena_go_Click(object sender, EventArgs e)
        {
            if (0 > singleArenaComboBox.SelectedIndex)
            {
                LogDelegate.Instance.Log(string.Format("Should choose arena id"));
                return;
            }

            Player.Instance.ArenaMove();
        }

        private void MatchingRequest_Click(object sender, EventArgs e)
        {
            if (false == ArenaServerSession.Instance.IsConnected)
            {
                LogDelegate.Instance.Log("not connected.");
                return;
            }

            Player.Instance.RequetMatching();
        }

        private void MatchingCancel_Click(object sender, EventArgs e)
        {
            m_matchingTimer.Change(Timeout.Infinite, Timeout.Infinite);
            pgb_matching.Value = 0;
            Player.Instance.CancelMatching();
        }

        private void raidArenaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int areanID = Int32.Parse(raidArenaComboBox.SelectedItem as string);

            Player.Instance.RaidArenaID = areanID;
            LogDelegate.Instance.Log(string.Format("Selected raid arena id[{0}]", Player.Instance.RaidArenaID));
        }

        private void btn_Revive_Click(object sender, EventArgs e)
        {
            Player.Instance.RequestRevive();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Level_Click(object sender, EventArgs e)
        {

        }

        private void btn_exp_Click(object sender, EventArgs e)
        {
            if (0 == tbx_exp.Text.Length)
            {
                LogDelegate.Instance.Log(string.Format("fill a exp box."), Color.Red);
                return;
            }

            Player.Instance.TestCommand(TestCommandType.AddExp, Int32.Parse(tbx_exp.Text));
        }

        private void btn_activeExp_Click(object sender, EventArgs e)
        {

            //프로그레스바에 누적 값 표기  + 액티브 테스트 하기 -> 맵에 드갔다 나갓다. 

            if (0 == tbx_activeExp.Text.Length)
            {
                LogDelegate.Instance.Log(string.Format("fill a active exp box."), Color.Red);
                return;
            }

            Player.Instance.TestCommand(TestCommandType.ActiveExp, Int32.Parse(tbx_activeExp.Text));
        }

        private void btn_deck_point_up_Click(object sender, EventArgs e)
        {
            Player.Instance.TestCommand(TestCommandType.DeckPointUp, 1);
        }

        private void btn_deck_generate_Click(object sender, EventArgs e)
        {
            Player.Instance.RequestDeckGen();
            //Player.Instance.TestCommand(TestCommandType.DeckGenerate, 1);
        }

        private void btn_select_deck1_Click(object sender, EventArgs e)
        {
            Player.Instance.SelectDeck(0);
            SelectableDeckClear();
        }

        private void btn_select_deck2_Click(object sender, EventArgs e)
        {
            Player.Instance.SelectDeck(1);
            SelectableDeckClear();
        }

        private void btn_select_deck3_Click(object sender, EventArgs e)
        {
            Player.Instance.SelectDeck(2);
            SelectableDeckClear();
        }

        private void SelectableDeckClear()
        {
            tbx_deck1.Clear();
            tbx_deck2.Clear();
            tbx_deck3.Clear();
        }

        private void mapBox_Click(object sender, EventArgs e)
        {

        }

        private void mapBox_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            return;
        }

        //private void btn_npc_g_Click(object sender, EventArgs e)
        //{
        //    decimal x = 0;
        //    decimal z = 0;
        //    decimal arenaObjectID = 0;

        //    if ((true == decimal.TryParse(tbx_npc_x.Text, out x))
        //    &&  (true == decimal.TryParse(tbx_npc_z.Text, out z))
        //    &&  (true == decimal.TryParse(tbx_npc_oid.Text, out arenaObjectID)))
        //    {
        //        Player.Instance.TestCommand(TestCommandType.CreateNpc, (int)arenaObjectID, 0, (float)x, (float)z);
        //        return;
        //    }

        //    LogDelegate.Instance.Log("fill the fucking box. right now.", Color.Red);
        //}

        private void btn_inventory_req_Click(object sender, EventArgs e)
        {

        }

        private void btn_item_equip_Click(object sender, EventArgs e)
        {

        }

        private void rtb_item_list_TextChanged(object sender, EventArgs e)
        {

        }

        private void rtb_test_command_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keys.Enter == e.KeyCode) && (0 != rtb_test_command.Lines.Count()))
            {
                int index = rtb_test_command.SelectionStart;
                int line = rtb_test_command.GetLineFromCharIndex(index);

                if (rtb_test_command.Lines.Count() <= line)
                {
                    return;
                }

                var fullcommand = rtb_test_command.Lines[line];

                TEST_COMMAND packet = TestCommandManager.Instance.Parsing(fullcommand);
                if (null != packet)
                {
                    ArenaServerSession.Instance.SendPacket(packet);
                }

                //e.Handled = true;
                return;
            }

            if ((Keys.Up == e.KeyCode) && (0 != rtb_test_command.Lines.Count()))
            {
                int index = rtb_test_command.SelectionStart;
                int line = rtb_test_command.GetLineFromCharIndex(index) - 1;//-이 전 라인
                var fullcommand = rtb_test_command.Lines[line];

                rtb_test_command.AppendText(fullcommand);
                e.Handled = true;

                //-jinsub, up arrow 의 index를 기억했다가 위에 쳤던 명령어를.. 음.. 명령어를 다 기억하고 있어야?! 음.. 
            }
        }

        private void tbx_hpMax_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
