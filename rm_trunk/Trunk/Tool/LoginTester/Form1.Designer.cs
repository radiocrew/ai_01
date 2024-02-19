
using rm_login.Tool;
using System.Drawing;

namespace rm_login
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_Suicide = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.rtb_test_command = new System.Windows.Forms.RichTextBox();
            this.btn_item_equip = new System.Windows.Forms.Button();
            this.btn_inventory_req = new System.Windows.Forms.Button();
            this.rtb_item_list = new System.Windows.Forms.RichTextBox();
            this.btn_activeExp = new System.Windows.Forms.Button();
            this.tbx_activeExp = new System.Windows.Forms.TextBox();
            this.btn_exp = new System.Windows.Forms.Button();
            this.tbx_exp = new System.Windows.Forms.TextBox();
            this.btn_Revive = new System.Windows.Forms.Button();
            this.btn_OnceAttack = new System.Windows.Forms.Button();
            this.btn_StopAutoAttack = new System.Windows.Forms.Button();
            this.btn_StartAutoAttack = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.playerUidTextBox5 = new System.Windows.Forms.TextBox();
            this.accountUidTextBox4 = new System.Windows.Forms.TextBox();
            this.mapBox = new System.Windows.Forms.PictureBox();
            this.tbx_x = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbx_y = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbx_z = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbx_d = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.arena_go = new System.Windows.Forms.Button();
            this.singleArenaComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.arenaIdtextBox3 = new System.Windows.Forms.TextBox();
            this.tbx_map_info = new System.Windows.Forms.TextBox();
            this.matchingGroupBox4 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.raidArenaComboBox = new System.Windows.Forms.ComboBox();
            this.btn_MatchingCancel = new System.Windows.Forms.Button();
            this.btn_MatchingRequest = new System.Windows.Forms.Button();
            this.gbx_Information = new System.Windows.Forms.GroupBox();
            this.gbx_ExtendStat = new System.Windows.Forms.GroupBox();
            this.rtb_active_deck = new System.Windows.Forms.RichTextBox();
            this.btn_deck_generate = new System.Windows.Forms.Button();
            this.btn_deck_point_up = new System.Windows.Forms.Button();
            this.btn_select_deck3 = new System.Windows.Forms.Button();
            this.btn_select_deck2 = new System.Windows.Forms.Button();
            this.btn_select_deck1 = new System.Windows.Forms.Button();
            this.lbl_deck_point = new System.Windows.Forms.Label();
            this.tbx_deck_point = new System.Windows.Forms.TextBox();
            this.tbx_deck3 = new System.Windows.Forms.TextBox();
            this.tbx_deck2 = new System.Windows.Forms.TextBox();
            this.tbx_deck1 = new System.Windows.Forms.TextBox();
            this.gbx_BseStat = new System.Windows.Forms.GroupBox();
            this.lbl_hpmax_add = new System.Windows.Forms.Label();
            this.tbx_hpmax_add = new System.Windows.Forms.TextBox();
            this.lbl_atk_add = new System.Windows.Forms.Label();
            this.tbx_atk_add = new System.Windows.Forms.TextBox();
            this.lbl_atk_rate = new System.Windows.Forms.Label();
            this.tbx_atk_rate = new System.Windows.Forms.TextBox();
            this.lbl_vampire_rate = new System.Windows.Forms.Label();
            this.tbx_vampire_rate = new System.Windows.Forms.TextBox();
            this.lbl_vigor_recovery_rate = new System.Windows.Forms.Label();
            this.tbx_vigor_recovery_rate = new System.Windows.Forms.TextBox();
            this.lbl_exp_add_rate = new System.Windows.Forms.Label();
            this.tbx_exp_add_rate = new System.Windows.Forms.TextBox();
            this.lbl_atk_spd_rate = new System.Windows.Forms.Label();
            this.tbx_atk_spd_rate = new System.Windows.Forms.TextBox();
            this.lbl_SPD = new System.Windows.Forms.Label();
            this.tbx_spd = new System.Windows.Forms.TextBox();
            this.lbl_ATK = new System.Windows.Forms.Label();
            this.tbx_atk = new System.Windows.Forms.TextBox();
            this.lbl_mpMax = new System.Windows.Forms.Label();
            this.tbx_mpMax = new System.Windows.Forms.TextBox();
            this.lbl_mp = new System.Windows.Forms.Label();
            this.tbx_mp = new System.Windows.Forms.TextBox();
            this.lbl_hpMax = new System.Windows.Forms.Label();
            this.tbx_hpMax = new System.Windows.Forms.TextBox();
            this.lbl_hp = new System.Windows.Forms.Label();
            this.tbx_hp = new System.Windows.Forms.TextBox();
            this.tbx_ActiveLevel = new System.Windows.Forms.TextBox();
            this.lbl_ActiveLevel = new System.Windows.Forms.Label();
            this.lbl_Level = new System.Windows.Forms.Label();
            this.tbx_Level = new System.Windows.Forms.TextBox();
            this.lbl_ActiveExp = new System.Windows.Forms.Label();
            this.lbl_Exp = new System.Windows.Forms.Label();
            this.pgb_ActiveExp = new rm_login.Tool.TextProgressBar();
            this.pgb_Exp = new rm_login.Tool.TextProgressBar();
            this.pgb_matching = new rm_login.Tool.TextProgressBar();
            this.tbx_player_class = new System.Windows.Forms.TextBox();
            this.tbx_player_name = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBox)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.matchingGroupBox4.SuspendLayout();
            this.gbx_Information.SuspendLayout();
            this.gbx_ExtendStat.SuspendLayout();
            this.gbx_BseStat.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(684, 561);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "UID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(684, 589);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(768, 561);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 21);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(768, 589);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(125, 21);
            this.textBox2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(899, 560);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 53);
            this.button1.TabIndex = 4;
            this.button1.Text = "LOGIN";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            // 
            // rtb_log
            // 
            this.rtb_log.BackColor = System.Drawing.SystemColors.Window;
            this.rtb_log.Location = new System.Drawing.Point(12, 268);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.Size = new System.Drawing.Size(388, 252);
            this.rtb_log.TabIndex = 7;
            this.rtb_log.Text = "";
            this.rtb_log.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(46, 119);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "move";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(29, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btn_Suicide
            // 
            this.btn_Suicide.Location = new System.Drawing.Point(14, 53);
            this.btn_Suicide.Name = "btn_Suicide";
            this.btn_Suicide.Size = new System.Drawing.Size(121, 23);
            this.btn_Suicide.TabIndex = 10;
            this.btn_Suicide.Text = "suicide";
            this.btn_Suicide.UseVisualStyleBackColor = true;
            this.btn_Suicide.Click += new System.EventHandler(this.btn_suicide);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.rtb_test_command);
            this.groupBox1.Controls.Add(this.btn_item_equip);
            this.groupBox1.Controls.Add(this.btn_inventory_req);
            this.groupBox1.Controls.Add(this.rtb_item_list);
            this.groupBox1.Controls.Add(this.btn_activeExp);
            this.groupBox1.Controls.Add(this.tbx_activeExp);
            this.groupBox1.Controls.Add(this.btn_exp);
            this.groupBox1.Controls.Add(this.tbx_exp);
            this.groupBox1.Controls.Add(this.btn_Revive);
            this.groupBox1.Controls.Add(this.btn_OnceAttack);
            this.groupBox1.Controls.Add(this.btn_StopAutoAttack);
            this.groupBox1.Controls.Add(this.btn_StartAutoAttack);
            this.groupBox1.Controls.Add(this.btn_Suicide);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox1.Location = new System.Drawing.Point(422, 312);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(562, 232);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Function";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(468, 185);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(86, 23);
            this.button5.TabIndex = 31;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(373, 185);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(89, 23);
            this.button4.TabIndex = 30;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // rtb_test_command
            // 
            this.rtb_test_command.BackColor = System.Drawing.Color.Black;
            this.rtb_test_command.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rtb_test_command.Location = new System.Drawing.Point(14, 169);
            this.rtb_test_command.Name = "rtb_test_command";
            this.rtb_test_command.Size = new System.Drawing.Size(352, 52);
            this.rtb_test_command.TabIndex = 29;
            this.rtb_test_command.Text = "";
            this.rtb_test_command.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtb_test_command_KeyDown);
            // 
            // btn_item_equip
            // 
            this.btn_item_equip.Enabled = false;
            this.btn_item_equip.Location = new System.Drawing.Point(468, 154);
            this.btn_item_equip.Name = "btn_item_equip";
            this.btn_item_equip.Size = new System.Drawing.Size(85, 23);
            this.btn_item_equip.TabIndex = 28;
            this.btn_item_equip.Text = "item equip";
            this.btn_item_equip.UseVisualStyleBackColor = true;
            this.btn_item_equip.Click += new System.EventHandler(this.btn_item_equip_Click);
            // 
            // btn_inventory_req
            // 
            this.btn_inventory_req.Enabled = false;
            this.btn_inventory_req.Location = new System.Drawing.Point(373, 154);
            this.btn_inventory_req.Name = "btn_inventory_req";
            this.btn_inventory_req.Size = new System.Drawing.Size(89, 23);
            this.btn_inventory_req.TabIndex = 27;
            this.btn_inventory_req.Text = "inventory";
            this.btn_inventory_req.UseVisualStyleBackColor = true;
            this.btn_inventory_req.Click += new System.EventHandler(this.btn_inventory_req_Click);
            // 
            // rtb_item_list
            // 
            this.rtb_item_list.BackColor = System.Drawing.SystemColors.Window;
            this.rtb_item_list.Location = new System.Drawing.Point(373, 24);
            this.rtb_item_list.Name = "rtb_item_list";
            this.rtb_item_list.ReadOnly = true;
            this.rtb_item_list.Size = new System.Drawing.Size(180, 124);
            this.rtb_item_list.TabIndex = 26;
            this.rtb_item_list.Text = "";
            this.rtb_item_list.TextChanged += new System.EventHandler(this.rtb_item_list_TextChanged);
            this.rtb_item_list.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            // 
            // btn_activeExp
            // 
            this.btn_activeExp.Location = new System.Drawing.Point(236, 52);
            this.btn_activeExp.Name = "btn_activeExp";
            this.btn_activeExp.Size = new System.Drawing.Size(121, 23);
            this.btn_activeExp.TabIndex = 18;
            this.btn_activeExp.Text = "active exp";
            this.btn_activeExp.UseVisualStyleBackColor = true;
            this.btn_activeExp.Click += new System.EventHandler(this.btn_activeExp_Click);
            // 
            // tbx_activeExp
            // 
            this.tbx_activeExp.Location = new System.Drawing.Point(149, 53);
            this.tbx_activeExp.Name = "tbx_activeExp";
            this.tbx_activeExp.Size = new System.Drawing.Size(83, 21);
            this.tbx_activeExp.TabIndex = 17;
            // 
            // btn_exp
            // 
            this.btn_exp.Enabled = false;
            this.btn_exp.Location = new System.Drawing.Point(236, 24);
            this.btn_exp.Name = "btn_exp";
            this.btn_exp.Size = new System.Drawing.Size(121, 23);
            this.btn_exp.TabIndex = 16;
            this.btn_exp.Text = "exp";
            this.btn_exp.UseVisualStyleBackColor = true;
            this.btn_exp.Click += new System.EventHandler(this.btn_exp_Click);
            // 
            // tbx_exp
            // 
            this.tbx_exp.Enabled = false;
            this.tbx_exp.Location = new System.Drawing.Point(149, 25);
            this.tbx_exp.Name = "tbx_exp";
            this.tbx_exp.Size = new System.Drawing.Size(83, 21);
            this.tbx_exp.TabIndex = 15;
            // 
            // btn_Revive
            // 
            this.btn_Revive.Location = new System.Drawing.Point(14, 24);
            this.btn_Revive.Name = "btn_Revive";
            this.btn_Revive.Size = new System.Drawing.Size(121, 23);
            this.btn_Revive.TabIndex = 14;
            this.btn_Revive.Text = "revive";
            this.btn_Revive.UseVisualStyleBackColor = true;
            this.btn_Revive.Click += new System.EventHandler(this.btn_Revive_Click);
            // 
            // btn_OnceAttack
            // 
            this.btn_OnceAttack.Location = new System.Drawing.Point(14, 140);
            this.btn_OnceAttack.Name = "btn_OnceAttack";
            this.btn_OnceAttack.Size = new System.Drawing.Size(121, 23);
            this.btn_OnceAttack.TabIndex = 13;
            this.btn_OnceAttack.Text = "once attack";
            this.btn_OnceAttack.UseVisualStyleBackColor = true;
            this.btn_OnceAttack.Click += new System.EventHandler(this.btn_OnceAttack_Click);
            // 
            // btn_StopAutoAttack
            // 
            this.btn_StopAutoAttack.Location = new System.Drawing.Point(14, 111);
            this.btn_StopAutoAttack.Name = "btn_StopAutoAttack";
            this.btn_StopAutoAttack.Size = new System.Drawing.Size(121, 23);
            this.btn_StopAutoAttack.TabIndex = 12;
            this.btn_StopAutoAttack.Text = "stop attack";
            this.btn_StopAutoAttack.UseVisualStyleBackColor = true;
            this.btn_StopAutoAttack.Click += new System.EventHandler(this.btn_StopAutoAttack_Click);
            // 
            // btn_StartAutoAttack
            // 
            this.btn_StartAutoAttack.Location = new System.Drawing.Point(14, 82);
            this.btn_StartAutoAttack.Name = "btn_StartAutoAttack";
            this.btn_StartAutoAttack.Size = new System.Drawing.Size(121, 23);
            this.btn_StartAutoAttack.TabIndex = 11;
            this.btn_StartAutoAttack.Text = "start attack";
            this.btn_StartAutoAttack.UseVisualStyleBackColor = true;
            this.btn_StartAutoAttack.Click += new System.EventHandler(this.btn_StartAutoAttack_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.playerUidTextBox5);
            this.groupBox2.Controls.Add(this.accountUidTextBox4);
            this.groupBox2.Location = new System.Drawing.Point(422, 550);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(256, 63);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Player";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "Player UID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "Account UID";
            // 
            // playerUidTextBox5
            // 
            this.playerUidTextBox5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.playerUidTextBox5.Location = new System.Drawing.Point(92, 36);
            this.playerUidTextBox5.Name = "playerUidTextBox5";
            this.playerUidTextBox5.ReadOnly = true;
            this.playerUidTextBox5.Size = new System.Drawing.Size(158, 21);
            this.playerUidTextBox5.TabIndex = 14;
            this.playerUidTextBox5.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // accountUidTextBox4
            // 
            this.accountUidTextBox4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.accountUidTextBox4.Location = new System.Drawing.Point(91, 13);
            this.accountUidTextBox4.Name = "accountUidTextBox4";
            this.accountUidTextBox4.ReadOnly = true;
            this.accountUidTextBox4.Size = new System.Drawing.Size(159, 21);
            this.accountUidTextBox4.TabIndex = 13;
            this.accountUidTextBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // mapBox
            // 
            this.mapBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.mapBox.Location = new System.Drawing.Point(12, 12);
            this.mapBox.Name = "mapBox";
            this.mapBox.Size = new System.Drawing.Size(250, 250);
            this.mapBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mapBox.TabIndex = 13;
            this.mapBox.TabStop = false;
            this.mapBox.Click += new System.EventHandler(this.mapBox_Click);
            this.mapBox.Paint += new System.Windows.Forms.PaintEventHandler(this.mapBox_Paint);
            this.mapBox.Validated += new System.EventHandler(this.timer_Tick);
            // 
            // tbx_x
            // 
            this.tbx_x.Location = new System.Drawing.Point(46, 15);
            this.tbx_x.Name = "tbx_x";
            this.tbx_x.Size = new System.Drawing.Size(64, 21);
            this.tbx_x.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "Y";
            // 
            // tbx_y
            // 
            this.tbx_y.Enabled = false;
            this.tbx_y.Location = new System.Drawing.Point(46, 41);
            this.tbx_y.Name = "tbx_y";
            this.tbx_y.Size = new System.Drawing.Size(64, 21);
            this.tbx_y.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "Z";
            // 
            // tbx_z
            // 
            this.tbx_z.Location = new System.Drawing.Point(46, 67);
            this.tbx_z.Name = "tbx_z";
            this.tbx_z.Size = new System.Drawing.Size(64, 21);
            this.tbx_z.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "D";
            // 
            // tbx_d
            // 
            this.tbx_d.Location = new System.Drawing.Point(46, 93);
            this.tbx_d.Name = "tbx_d";
            this.tbx_d.Size = new System.Drawing.Size(64, 21);
            this.tbx_d.TabIndex = 20;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbx_z);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.tbx_x);
            this.groupBox3.Controls.Add(this.tbx_d);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tbx_y);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(278, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(122, 149);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Position";
            // 
            // arena_go
            // 
            this.arena_go.Location = new System.Drawing.Point(270, 215);
            this.arena_go.Name = "arena_go";
            this.arena_go.Size = new System.Drawing.Size(130, 21);
            this.arena_go.TabIndex = 26;
            this.arena_go.Text = "go";
            this.arena_go.UseVisualStyleBackColor = true;
            this.arena_go.Click += new System.EventHandler(this.arena_go_Click);
            // 
            // singleArenaComboBox
            // 
            this.singleArenaComboBox.DropDownHeight = 120;
            this.singleArenaComboBox.FormattingEnabled = true;
            this.singleArenaComboBox.IntegralHeight = false;
            this.singleArenaComboBox.Location = new System.Drawing.Point(270, 191);
            this.singleArenaComboBox.Name = "singleArenaComboBox";
            this.singleArenaComboBox.Size = new System.Drawing.Size(130, 20);
            this.singleArenaComboBox.TabIndex = 0;
            this.singleArenaComboBox.SelectedIndexChanged += new System.EventHandler(this.singleArenaComboBox_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(268, 170);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 25;
            this.label10.Text = "Arena ID";
            // 
            // arenaIdtextBox3
            // 
            this.arenaIdtextBox3.Location = new System.Drawing.Point(324, 165);
            this.arenaIdtextBox3.Name = "arenaIdtextBox3";
            this.arenaIdtextBox3.Size = new System.Drawing.Size(76, 21);
            this.arenaIdtextBox3.TabIndex = 26;
            // 
            // tbx_map_info
            // 
            this.tbx_map_info.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_map_info.Location = new System.Drawing.Point(270, 239);
            this.tbx_map_info.Name = "tbx_map_info";
            this.tbx_map_info.Size = new System.Drawing.Size(131, 21);
            this.tbx_map_info.TabIndex = 28;
            // 
            // matchingGroupBox4
            // 
            this.matchingGroupBox4.Controls.Add(this.label12);
            this.matchingGroupBox4.Controls.Add(this.raidArenaComboBox);
            this.matchingGroupBox4.Controls.Add(this.pgb_matching);
            this.matchingGroupBox4.Controls.Add(this.btn_MatchingCancel);
            this.matchingGroupBox4.Controls.Add(this.btn_MatchingRequest);
            this.matchingGroupBox4.Location = new System.Drawing.Point(12, 524);
            this.matchingGroupBox4.Name = "matchingGroupBox4";
            this.matchingGroupBox4.Size = new System.Drawing.Size(388, 89);
            this.matchingGroupBox4.TabIndex = 29;
            this.matchingGroupBox4.TabStop = false;
            this.matchingGroupBox4.Text = "RAID Matching";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 27;
            this.label12.Text = "Arena ID";
            // 
            // raidArenaComboBox
            // 
            this.raidArenaComboBox.FormattingEnabled = true;
            this.raidArenaComboBox.Location = new System.Drawing.Point(65, 16);
            this.raidArenaComboBox.Name = "raidArenaComboBox";
            this.raidArenaComboBox.Size = new System.Drawing.Size(86, 20);
            this.raidArenaComboBox.TabIndex = 21;
            this.raidArenaComboBox.SelectedIndexChanged += new System.EventHandler(this.raidArenaComboBox_SelectedIndexChanged);
            // 
            // btn_MatchingCancel
            // 
            this.btn_MatchingCancel.Location = new System.Drawing.Point(257, 51);
            this.btn_MatchingCancel.Name = "btn_MatchingCancel";
            this.btn_MatchingCancel.Size = new System.Drawing.Size(83, 28);
            this.btn_MatchingCancel.TabIndex = 1;
            this.btn_MatchingCancel.Text = "cancel";
            this.btn_MatchingCancel.UseVisualStyleBackColor = true;
            this.btn_MatchingCancel.Click += new System.EventHandler(this.MatchingCancel_Click);
            // 
            // btn_MatchingRequest
            // 
            this.btn_MatchingRequest.Location = new System.Drawing.Point(163, 51);
            this.btn_MatchingRequest.Name = "btn_MatchingRequest";
            this.btn_MatchingRequest.Size = new System.Drawing.Size(88, 28);
            this.btn_MatchingRequest.TabIndex = 0;
            this.btn_MatchingRequest.Text = "request";
            this.btn_MatchingRequest.UseVisualStyleBackColor = true;
            this.btn_MatchingRequest.Click += new System.EventHandler(this.MatchingRequest_Click);
            // 
            // gbx_Information
            // 
            this.gbx_Information.Controls.Add(this.tbx_player_name);
            this.gbx_Information.Controls.Add(this.tbx_player_class);
            this.gbx_Information.Controls.Add(this.gbx_ExtendStat);
            this.gbx_Information.Controls.Add(this.gbx_BseStat);
            this.gbx_Information.Controls.Add(this.tbx_ActiveLevel);
            this.gbx_Information.Controls.Add(this.lbl_ActiveLevel);
            this.gbx_Information.Controls.Add(this.lbl_Level);
            this.gbx_Information.Controls.Add(this.tbx_Level);
            this.gbx_Information.Controls.Add(this.pgb_ActiveExp);
            this.gbx_Information.Controls.Add(this.lbl_ActiveExp);
            this.gbx_Information.Controls.Add(this.lbl_Exp);
            this.gbx_Information.Controls.Add(this.pgb_Exp);
            this.gbx_Information.Location = new System.Drawing.Point(422, 12);
            this.gbx_Information.Name = "gbx_Information";
            this.gbx_Information.Size = new System.Drawing.Size(562, 294);
            this.gbx_Information.TabIndex = 30;
            this.gbx_Information.TabStop = false;
            this.gbx_Information.Text = "Player information";
            // 
            // gbx_ExtendStat
            // 
            this.gbx_ExtendStat.Controls.Add(this.rtb_active_deck);
            this.gbx_ExtendStat.Controls.Add(this.btn_deck_generate);
            this.gbx_ExtendStat.Controls.Add(this.btn_deck_point_up);
            this.gbx_ExtendStat.Controls.Add(this.btn_select_deck3);
            this.gbx_ExtendStat.Controls.Add(this.btn_select_deck2);
            this.gbx_ExtendStat.Controls.Add(this.btn_select_deck1);
            this.gbx_ExtendStat.Controls.Add(this.lbl_deck_point);
            this.gbx_ExtendStat.Controls.Add(this.tbx_deck_point);
            this.gbx_ExtendStat.Controls.Add(this.tbx_deck3);
            this.gbx_ExtendStat.Controls.Add(this.tbx_deck2);
            this.gbx_ExtendStat.Controls.Add(this.tbx_deck1);
            this.gbx_ExtendStat.Location = new System.Drawing.Point(298, 75);
            this.gbx_ExtendStat.Name = "gbx_ExtendStat";
            this.gbx_ExtendStat.Size = new System.Drawing.Size(258, 203);
            this.gbx_ExtendStat.TabIndex = 17;
            this.gbx_ExtendStat.TabStop = false;
            this.gbx_ExtendStat.Text = "Deck";
            // 
            // rtb_active_deck
            // 
            this.rtb_active_deck.Location = new System.Drawing.Point(6, 113);
            this.rtb_active_deck.Name = "rtb_active_deck";
            this.rtb_active_deck.Size = new System.Drawing.Size(201, 84);
            this.rtb_active_deck.TabIndex = 13;
            this.rtb_active_deck.Text = "";
            // 
            // btn_deck_generate
            // 
            this.btn_deck_generate.Location = new System.Drawing.Point(5, 70);
            this.btn_deck_generate.Name = "btn_deck_generate";
            this.btn_deck_generate.Size = new System.Drawing.Size(203, 37);
            this.btn_deck_generate.TabIndex = 12;
            this.btn_deck_generate.Text = "request generate deck";
            this.btn_deck_generate.UseVisualStyleBackColor = true;
            this.btn_deck_generate.Click += new System.EventHandler(this.btn_deck_generate_Click);
            // 
            // btn_deck_point_up
            // 
            this.btn_deck_point_up.Location = new System.Drawing.Point(212, 70);
            this.btn_deck_point_up.Name = "btn_deck_point_up";
            this.btn_deck_point_up.Size = new System.Drawing.Size(43, 37);
            this.btn_deck_point_up.TabIndex = 11;
            this.btn_deck_point_up.Text = "▲";
            this.btn_deck_point_up.UseVisualStyleBackColor = true;
            this.btn_deck_point_up.Click += new System.EventHandler(this.btn_deck_point_up_Click);
            // 
            // btn_select_deck3
            // 
            this.btn_select_deck3.Location = new System.Drawing.Point(144, 43);
            this.btn_select_deck3.Name = "btn_select_deck3";
            this.btn_select_deck3.Size = new System.Drawing.Size(63, 23);
            this.btn_select_deck3.TabIndex = 10;
            this.btn_select_deck3.Text = "select";
            this.btn_select_deck3.UseVisualStyleBackColor = true;
            this.btn_select_deck3.Click += new System.EventHandler(this.btn_select_deck3_Click);
            // 
            // btn_select_deck2
            // 
            this.btn_select_deck2.Location = new System.Drawing.Point(75, 43);
            this.btn_select_deck2.Name = "btn_select_deck2";
            this.btn_select_deck2.Size = new System.Drawing.Size(63, 23);
            this.btn_select_deck2.TabIndex = 9;
            this.btn_select_deck2.Text = "select";
            this.btn_select_deck2.UseVisualStyleBackColor = true;
            this.btn_select_deck2.Click += new System.EventHandler(this.btn_select_deck2_Click);
            // 
            // btn_select_deck1
            // 
            this.btn_select_deck1.Location = new System.Drawing.Point(5, 43);
            this.btn_select_deck1.Name = "btn_select_deck1";
            this.btn_select_deck1.Size = new System.Drawing.Size(63, 23);
            this.btn_select_deck1.TabIndex = 8;
            this.btn_select_deck1.Text = "select";
            this.btn_select_deck1.UseVisualStyleBackColor = true;
            this.btn_select_deck1.Click += new System.EventHandler(this.btn_select_deck1_Click);
            // 
            // lbl_deck_point
            // 
            this.lbl_deck_point.AutoSize = true;
            this.lbl_deck_point.Location = new System.Drawing.Point(214, 50);
            this.lbl_deck_point.Name = "lbl_deck_point";
            this.lbl_deck_point.Size = new System.Drawing.Size(42, 12);
            this.lbl_deck_point.TabIndex = 7;
            this.lbl_deck_point.Text = "POINT";
            // 
            // tbx_deck_point
            // 
            this.tbx_deck_point.Location = new System.Drawing.Point(212, 17);
            this.tbx_deck_point.Name = "tbx_deck_point";
            this.tbx_deck_point.Size = new System.Drawing.Size(43, 21);
            this.tbx_deck_point.TabIndex = 3;
            // 
            // tbx_deck3
            // 
            this.tbx_deck3.Location = new System.Drawing.Point(143, 17);
            this.tbx_deck3.Name = "tbx_deck3";
            this.tbx_deck3.Size = new System.Drawing.Size(63, 21);
            this.tbx_deck3.TabIndex = 2;
            // 
            // tbx_deck2
            // 
            this.tbx_deck2.Location = new System.Drawing.Point(74, 17);
            this.tbx_deck2.Name = "tbx_deck2";
            this.tbx_deck2.Size = new System.Drawing.Size(63, 21);
            this.tbx_deck2.TabIndex = 1;
            // 
            // tbx_deck1
            // 
            this.tbx_deck1.Location = new System.Drawing.Point(5, 17);
            this.tbx_deck1.Name = "tbx_deck1";
            this.tbx_deck1.Size = new System.Drawing.Size(63, 21);
            this.tbx_deck1.TabIndex = 0;
            // 
            // gbx_BseStat
            // 
            this.gbx_BseStat.Controls.Add(this.lbl_hpmax_add);
            this.gbx_BseStat.Controls.Add(this.tbx_hpmax_add);
            this.gbx_BseStat.Controls.Add(this.lbl_atk_add);
            this.gbx_BseStat.Controls.Add(this.tbx_atk_add);
            this.gbx_BseStat.Controls.Add(this.lbl_atk_rate);
            this.gbx_BseStat.Controls.Add(this.tbx_atk_rate);
            this.gbx_BseStat.Controls.Add(this.lbl_vampire_rate);
            this.gbx_BseStat.Controls.Add(this.tbx_vampire_rate);
            this.gbx_BseStat.Controls.Add(this.lbl_vigor_recovery_rate);
            this.gbx_BseStat.Controls.Add(this.tbx_vigor_recovery_rate);
            this.gbx_BseStat.Controls.Add(this.lbl_exp_add_rate);
            this.gbx_BseStat.Controls.Add(this.tbx_exp_add_rate);
            this.gbx_BseStat.Controls.Add(this.lbl_atk_spd_rate);
            this.gbx_BseStat.Controls.Add(this.tbx_atk_spd_rate);
            this.gbx_BseStat.Controls.Add(this.lbl_SPD);
            this.gbx_BseStat.Controls.Add(this.tbx_spd);
            this.gbx_BseStat.Controls.Add(this.lbl_ATK);
            this.gbx_BseStat.Controls.Add(this.tbx_atk);
            this.gbx_BseStat.Controls.Add(this.lbl_mpMax);
            this.gbx_BseStat.Controls.Add(this.tbx_mpMax);
            this.gbx_BseStat.Controls.Add(this.lbl_mp);
            this.gbx_BseStat.Controls.Add(this.tbx_mp);
            this.gbx_BseStat.Controls.Add(this.lbl_hpMax);
            this.gbx_BseStat.Controls.Add(this.tbx_hpMax);
            this.gbx_BseStat.Controls.Add(this.lbl_hp);
            this.gbx_BseStat.Controls.Add(this.tbx_hp);
            this.gbx_BseStat.Location = new System.Drawing.Point(6, 75);
            this.gbx_BseStat.Name = "gbx_BseStat";
            this.gbx_BseStat.Size = new System.Drawing.Size(286, 203);
            this.gbx_BseStat.TabIndex = 16;
            this.gbx_BseStat.TabStop = false;
            this.gbx_BseStat.Text = "Stat";
            // 
            // lbl_hpmax_add
            // 
            this.lbl_hpmax_add.AutoSize = true;
            this.lbl_hpmax_add.Location = new System.Drawing.Point(8, 152);
            this.lbl_hpmax_add.Name = "lbl_hpmax_add";
            this.lbl_hpmax_add.Size = new System.Drawing.Size(62, 12);
            this.lbl_hpmax_add.TabIndex = 33;
            this.lbl_hpmax_add.Text = "HP MAX +";
            // 
            // tbx_hpmax_add
            // 
            this.tbx_hpmax_add.Location = new System.Drawing.Point(76, 147);
            this.tbx_hpmax_add.Name = "tbx_hpmax_add";
            this.tbx_hpmax_add.Size = new System.Drawing.Size(53, 21);
            this.tbx_hpmax_add.TabIndex = 32;
            // 
            // lbl_atk_add
            // 
            this.lbl_atk_add.AutoSize = true;
            this.lbl_atk_add.Location = new System.Drawing.Point(30, 126);
            this.lbl_atk_add.Name = "lbl_atk_add";
            this.lbl_atk_add.Size = new System.Drawing.Size(39, 12);
            this.lbl_atk_add.TabIndex = 31;
            this.lbl_atk_add.Text = "ATK +";
            // 
            // tbx_atk_add
            // 
            this.tbx_atk_add.Location = new System.Drawing.Point(76, 120);
            this.tbx_atk_add.Name = "tbx_atk_add";
            this.tbx_atk_add.Size = new System.Drawing.Size(53, 21);
            this.tbx_atk_add.TabIndex = 30;
            // 
            // lbl_atk_rate
            // 
            this.lbl_atk_rate.AutoSize = true;
            this.lbl_atk_rate.Location = new System.Drawing.Point(170, 180);
            this.lbl_atk_rate.Name = "lbl_atk_rate";
            this.lbl_atk_rate.Size = new System.Drawing.Size(43, 12);
            this.lbl_atk_rate.TabIndex = 29;
            this.lbl_atk_rate.Text = "ATK %";
            // 
            // tbx_atk_rate
            // 
            this.tbx_atk_rate.Location = new System.Drawing.Point(219, 174);
            this.tbx_atk_rate.Name = "tbx_atk_rate";
            this.tbx_atk_rate.Size = new System.Drawing.Size(53, 21);
            this.tbx_atk_rate.TabIndex = 28;
            // 
            // lbl_vampire_rate
            // 
            this.lbl_vampire_rate.AutoSize = true;
            this.lbl_vampire_rate.Location = new System.Drawing.Point(140, 152);
            this.lbl_vampire_rate.Name = "lbl_vampire_rate";
            this.lbl_vampire_rate.Size = new System.Drawing.Size(73, 12);
            this.lbl_vampire_rate.TabIndex = 27;
            this.lbl_vampire_rate.Text = "VAMPIRE %";
            // 
            // tbx_vampire_rate
            // 
            this.tbx_vampire_rate.Location = new System.Drawing.Point(219, 147);
            this.tbx_vampire_rate.Name = "tbx_vampire_rate";
            this.tbx_vampire_rate.Size = new System.Drawing.Size(53, 21);
            this.tbx_vampire_rate.TabIndex = 26;
            // 
            // lbl_vigor_recovery_rate
            // 
            this.lbl_vigor_recovery_rate.AutoSize = true;
            this.lbl_vigor_recovery_rate.Location = new System.Drawing.Point(155, 126);
            this.lbl_vigor_recovery_rate.Name = "lbl_vigor_recovery_rate";
            this.lbl_vigor_recovery_rate.Size = new System.Drawing.Size(56, 12);
            this.lbl_vigor_recovery_rate.TabIndex = 25;
            this.lbl_vigor_recovery_rate.Text = "VIGOR %";
            // 
            // tbx_vigor_recovery_rate
            // 
            this.tbx_vigor_recovery_rate.Location = new System.Drawing.Point(219, 120);
            this.tbx_vigor_recovery_rate.Name = "tbx_vigor_recovery_rate";
            this.tbx_vigor_recovery_rate.Size = new System.Drawing.Size(53, 21);
            this.tbx_vigor_recovery_rate.TabIndex = 24;
            // 
            // lbl_exp_add_rate
            // 
            this.lbl_exp_add_rate.AutoSize = true;
            this.lbl_exp_add_rate.Location = new System.Drawing.Point(142, 99);
            this.lbl_exp_add_rate.Name = "lbl_exp_add_rate";
            this.lbl_exp_add_rate.Size = new System.Drawing.Size(71, 12);
            this.lbl_exp_add_rate.TabIndex = 23;
            this.lbl_exp_add_rate.Text = "EXP ADD %";
            // 
            // tbx_exp_add_rate
            // 
            this.tbx_exp_add_rate.Location = new System.Drawing.Point(219, 93);
            this.tbx_exp_add_rate.Name = "tbx_exp_add_rate";
            this.tbx_exp_add_rate.Size = new System.Drawing.Size(53, 21);
            this.tbx_exp_add_rate.TabIndex = 22;
            // 
            // lbl_atk_spd_rate
            // 
            this.lbl_atk_spd_rate.AutoSize = true;
            this.lbl_atk_spd_rate.Location = new System.Drawing.Point(142, 73);
            this.lbl_atk_spd_rate.Name = "lbl_atk_spd_rate";
            this.lbl_atk_spd_rate.Size = new System.Drawing.Size(71, 12);
            this.lbl_atk_spd_rate.TabIndex = 21;
            this.lbl_atk_spd_rate.Text = "ATK SPD %";
            // 
            // tbx_atk_spd_rate
            // 
            this.tbx_atk_spd_rate.Location = new System.Drawing.Point(219, 66);
            this.tbx_atk_spd_rate.Name = "tbx_atk_spd_rate";
            this.tbx_atk_spd_rate.Size = new System.Drawing.Size(53, 21);
            this.tbx_atk_spd_rate.TabIndex = 20;
            // 
            // lbl_SPD
            // 
            this.lbl_SPD.AutoSize = true;
            this.lbl_SPD.Location = new System.Drawing.Point(184, 46);
            this.lbl_SPD.Name = "lbl_SPD";
            this.lbl_SPD.Size = new System.Drawing.Size(29, 12);
            this.lbl_SPD.TabIndex = 19;
            this.lbl_SPD.Text = "SPD";
            // 
            // tbx_spd
            // 
            this.tbx_spd.Location = new System.Drawing.Point(219, 39);
            this.tbx_spd.Name = "tbx_spd";
            this.tbx_spd.Size = new System.Drawing.Size(53, 21);
            this.tbx_spd.TabIndex = 18;
            // 
            // lbl_ATK
            // 
            this.lbl_ATK.AutoSize = true;
            this.lbl_ATK.Location = new System.Drawing.Point(184, 20);
            this.lbl_ATK.Name = "lbl_ATK";
            this.lbl_ATK.Size = new System.Drawing.Size(29, 12);
            this.lbl_ATK.TabIndex = 17;
            this.lbl_ATK.Text = "ATK";
            // 
            // tbx_atk
            // 
            this.tbx_atk.Location = new System.Drawing.Point(219, 13);
            this.tbx_atk.Name = "tbx_atk";
            this.tbx_atk.Size = new System.Drawing.Size(53, 21);
            this.tbx_atk.TabIndex = 16;
            // 
            // lbl_mpMax
            // 
            this.lbl_mpMax.AutoSize = true;
            this.lbl_mpMax.Location = new System.Drawing.Point(19, 99);
            this.lbl_mpMax.Name = "lbl_mpMax";
            this.lbl_mpMax.Size = new System.Drawing.Size(55, 12);
            this.lbl_mpMax.TabIndex = 15;
            this.lbl_mpMax.Text = "MP MAX";
            // 
            // tbx_mpMax
            // 
            this.tbx_mpMax.Location = new System.Drawing.Point(76, 93);
            this.tbx_mpMax.Name = "tbx_mpMax";
            this.tbx_mpMax.Size = new System.Drawing.Size(53, 21);
            this.tbx_mpMax.TabIndex = 14;
            // 
            // lbl_mp
            // 
            this.lbl_mp.AutoSize = true;
            this.lbl_mp.Location = new System.Drawing.Point(47, 72);
            this.lbl_mp.Name = "lbl_mp";
            this.lbl_mp.Size = new System.Drawing.Size(24, 12);
            this.lbl_mp.TabIndex = 13;
            this.lbl_mp.Text = "MP";
            // 
            // tbx_mp
            // 
            this.tbx_mp.Location = new System.Drawing.Point(76, 66);
            this.tbx_mp.Name = "tbx_mp";
            this.tbx_mp.Size = new System.Drawing.Size(53, 21);
            this.tbx_mp.TabIndex = 12;
            // 
            // lbl_hpMax
            // 
            this.lbl_hpMax.AutoSize = true;
            this.lbl_hpMax.Location = new System.Drawing.Point(19, 45);
            this.lbl_hpMax.Name = "lbl_hpMax";
            this.lbl_hpMax.Size = new System.Drawing.Size(52, 12);
            this.lbl_hpMax.TabIndex = 11;
            this.lbl_hpMax.Text = "HP MAX";
            // 
            // tbx_hpMax
            // 
            this.tbx_hpMax.Location = new System.Drawing.Point(76, 39);
            this.tbx_hpMax.Name = "tbx_hpMax";
            this.tbx_hpMax.Size = new System.Drawing.Size(53, 21);
            this.tbx_hpMax.TabIndex = 10;
            this.tbx_hpMax.TextChanged += new System.EventHandler(this.tbx_hpMax_TextChanged);
            // 
            // lbl_hp
            // 
            this.lbl_hp.AutoSize = true;
            this.lbl_hp.Location = new System.Drawing.Point(49, 19);
            this.lbl_hp.Name = "lbl_hp";
            this.lbl_hp.Size = new System.Drawing.Size(21, 12);
            this.lbl_hp.TabIndex = 9;
            this.lbl_hp.Text = "HP";
            // 
            // tbx_hp
            // 
            this.tbx_hp.Location = new System.Drawing.Point(76, 13);
            this.tbx_hp.Name = "tbx_hp";
            this.tbx_hp.Size = new System.Drawing.Size(53, 21);
            this.tbx_hp.TabIndex = 8;
            // 
            // tbx_ActiveLevel
            // 
            this.tbx_ActiveLevel.Location = new System.Drawing.Point(175, 41);
            this.tbx_ActiveLevel.Name = "tbx_ActiveLevel";
            this.tbx_ActiveLevel.Size = new System.Drawing.Size(82, 21);
            this.tbx_ActiveLevel.TabIndex = 7;
            // 
            // lbl_ActiveLevel
            // 
            this.lbl_ActiveLevel.AutoSize = true;
            this.lbl_ActiveLevel.Location = new System.Drawing.Point(97, 47);
            this.lbl_ActiveLevel.Name = "lbl_ActiveLevel";
            this.lbl_ActiveLevel.Size = new System.Drawing.Size(73, 12);
            this.lbl_ActiveLevel.TabIndex = 6;
            this.lbl_ActiveLevel.Text = "Active Level";
            // 
            // lbl_Level
            // 
            this.lbl_Level.AutoSize = true;
            this.lbl_Level.Location = new System.Drawing.Point(134, 24);
            this.lbl_Level.Name = "lbl_Level";
            this.lbl_Level.Size = new System.Drawing.Size(35, 12);
            this.lbl_Level.TabIndex = 5;
            this.lbl_Level.Text = "Level";
            this.lbl_Level.Click += new System.EventHandler(this.lbl_Level_Click);
            // 
            // tbx_Level
            // 
            this.tbx_Level.Location = new System.Drawing.Point(175, 15);
            this.tbx_Level.Name = "tbx_Level";
            this.tbx_Level.Size = new System.Drawing.Size(82, 21);
            this.tbx_Level.TabIndex = 4;
            // 
            // lbl_ActiveExp
            // 
            this.lbl_ActiveExp.AutoSize = true;
            this.lbl_ActiveExp.Location = new System.Drawing.Point(268, 42);
            this.lbl_ActiveExp.Name = "lbl_ActiveExp";
            this.lbl_ActiveExp.Size = new System.Drawing.Size(65, 12);
            this.lbl_ActiveExp.TabIndex = 2;
            this.lbl_ActiveExp.Text = "Active Exp";
            // 
            // lbl_Exp
            // 
            this.lbl_Exp.AutoSize = true;
            this.lbl_Exp.Location = new System.Drawing.Point(305, 21);
            this.lbl_Exp.Name = "lbl_Exp";
            this.lbl_Exp.Size = new System.Drawing.Size(27, 12);
            this.lbl_Exp.TabIndex = 1;
            this.lbl_Exp.Text = "Exp";
            this.lbl_Exp.Click += new System.EventHandler(this.label13_Click);
            // 
            // pgb_ActiveExp
            // 
            this.pgb_ActiveExp.CustomText = "";
            this.pgb_ActiveExp.Location = new System.Drawing.Point(336, 41);
            this.pgb_ActiveExp.Name = "pgb_ActiveExp";
            this.pgb_ActiveExp.ProgressColor = System.Drawing.Color.LightGreen;
            this.pgb_ActiveExp.Size = new System.Drawing.Size(193, 18);
            this.pgb_ActiveExp.TabIndex = 3;
            this.pgb_ActiveExp.TextColor = System.Drawing.Color.Black;
            this.pgb_ActiveExp.TextFont = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.pgb_ActiveExp.VisualMode = rm_login.Tool.ProgressBarDisplayMode.TextAndPercentage;
            // 
            // pgb_Exp
            // 
            this.pgb_Exp.CustomText = "";
            this.pgb_Exp.Location = new System.Drawing.Point(336, 17);
            this.pgb_Exp.Name = "pgb_Exp";
            this.pgb_Exp.ProgressColor = System.Drawing.Color.LightGreen;
            this.pgb_Exp.Size = new System.Drawing.Size(193, 18);
            this.pgb_Exp.TabIndex = 0;
            this.pgb_Exp.TextColor = System.Drawing.Color.Black;
            this.pgb_Exp.TextFont = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.pgb_Exp.VisualMode = rm_login.Tool.ProgressBarDisplayMode.TextAndPercentage;
            // 
            // pgb_matching
            // 
            this.pgb_matching.CustomText = "";
            this.pgb_matching.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.pgb_matching.Location = new System.Drawing.Point(163, 15);
            this.pgb_matching.Maximum = 3000;
            this.pgb_matching.Name = "pgb_matching";
            this.pgb_matching.ProgressColor = System.Drawing.Color.LightGreen;
            this.pgb_matching.Size = new System.Drawing.Size(177, 28);
            this.pgb_matching.Step = 1;
            this.pgb_matching.TabIndex = 20;
            this.pgb_matching.TextColor = System.Drawing.Color.Black;
            this.pgb_matching.TextFont = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.pgb_matching.VisualMode = rm_login.Tool.ProgressBarDisplayMode.CurrProgress;
            // 
            // tbx_player_class
            // 
            this.tbx_player_class.Font = new System.Drawing.Font("궁서체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbx_player_class.Location = new System.Drawing.Point(7, 15);
            this.tbx_player_class.Name = "tbx_player_class";
            this.tbx_player_class.Size = new System.Drawing.Size(84, 21);
            this.tbx_player_class.TabIndex = 18;
            // 
            // tbx_player_name
            // 
            this.tbx_player_name.Location = new System.Drawing.Point(7, 42);
            this.tbx_player_name.Name = "tbx_player_name";
            this.tbx_player_name.Size = new System.Drawing.Size(84, 21);
            this.tbx_player_name.TabIndex = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(994, 625);
            this.Controls.Add(this.arena_go);
            this.Controls.Add(this.gbx_Information);
            this.Controls.Add(this.matchingGroupBox4);
            this.Controls.Add(this.tbx_map_info);
            this.Controls.Add(this.singleArenaComboBox);
            this.Controls.Add(this.arenaIdtextBox3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.mapBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rtb_log);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "LOGIN TESTER";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBox)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.matchingGroupBox4.ResumeLayout(false);
            this.matchingGroupBox4.PerformLayout();
            this.gbx_Information.ResumeLayout(false);
            this.gbx_Information.PerformLayout();
            this.gbx_ExtendStat.ResumeLayout(false);
            this.gbx_ExtendStat.PerformLayout();
            this.gbx_BseStat.ResumeLayout(false);
            this.gbx_BseStat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelPB;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.RichTextBox rtb_log;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.Button btn_Suicide;
        public System.Windows.Forms.ColorDialog colorDialog1;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button btn_StopAutoAttack;
        public System.Windows.Forms.Button btn_StartAutoAttack;
        public System.Windows.Forms.Button btn_OnceAttack;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox playerUidTextBox5;
        public System.Windows.Forms.TextBox accountUidTextBox4;
        public System.Windows.Forms.PictureBox mapBox;
        public System.Windows.Forms.TextBox tbx_x;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox tbx_y;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox tbx_z;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox tbx_d;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.ComboBox singleArenaComboBox;
        public System.Windows.Forms.Button arena_go;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox arenaIdtextBox3;
        public System.Windows.Forms.TextBox tbx_map_info;
        public System.Windows.Forms.GroupBox matchingGroupBox4;
        public System.Windows.Forms.Button btn_MatchingRequest;

        //public System.Windows.Forms.ProgressBar pgb_matching;
        private TextProgressBar pgb_matching;

        public System.Windows.Forms.Button btn_MatchingCancel;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.ComboBox raidArenaComboBox;
        private System.Windows.Forms.Button btn_Revive;
        private System.Windows.Forms.GroupBox gbx_Information;
        private System.Windows.Forms.Label lbl_Exp;

        //private System.Windows.Forms.ProgressBar pgb_Exp;
        private TextProgressBar pgb_Exp;

        private System.Windows.Forms.Label lbl_ActiveExp;
        private System.Windows.Forms.Label lbl_Level;
        private System.Windows.Forms.TextBox tbx_Level;

        //private System.Windows.Forms.ProgressBar pgb_ActiveExp;
        private TextProgressBar pgb_ActiveExp;

        private System.Windows.Forms.TextBox tbx_ActiveLevel;
        private System.Windows.Forms.Label lbl_ActiveLevel;
        private System.Windows.Forms.Label lbl_mpMax;
        private System.Windows.Forms.TextBox tbx_mpMax;
        private System.Windows.Forms.Label lbl_mp;
        private System.Windows.Forms.TextBox tbx_mp;
        private System.Windows.Forms.Label lbl_hpMax;
        private System.Windows.Forms.TextBox tbx_hpMax;
        private System.Windows.Forms.Label lbl_hp;
        private System.Windows.Forms.TextBox tbx_hp;
        private System.Windows.Forms.Button btn_activeExp;
        private System.Windows.Forms.TextBox tbx_activeExp;
        private System.Windows.Forms.Button btn_exp;
        private System.Windows.Forms.TextBox tbx_exp;
        private System.Windows.Forms.GroupBox gbx_ExtendStat;
        private System.Windows.Forms.GroupBox gbx_BseStat;
        private System.Windows.Forms.Label lbl_ATK;
        private System.Windows.Forms.TextBox tbx_atk;
        private System.Windows.Forms.Label lbl_SPD;
        private System.Windows.Forms.TextBox tbx_spd;
        private System.Windows.Forms.TextBox tbx_deck3;
        private System.Windows.Forms.TextBox tbx_deck2;
        private System.Windows.Forms.TextBox tbx_deck1;
        private System.Windows.Forms.Button btn_deck_point_up;
        private System.Windows.Forms.Button btn_select_deck3;
        private System.Windows.Forms.Button btn_select_deck2;
        private System.Windows.Forms.Button btn_select_deck1;
        private System.Windows.Forms.Label lbl_deck_point;
        private System.Windows.Forms.TextBox tbx_deck_point;
        private System.Windows.Forms.Button btn_deck_generate;
        private System.Windows.Forms.RichTextBox rtb_active_deck;
        private System.Windows.Forms.Label lbl_atk_spd_rate;
        private System.Windows.Forms.TextBox tbx_atk_spd_rate;
        private System.Windows.Forms.Label lbl_exp_add_rate;
        private System.Windows.Forms.TextBox tbx_exp_add_rate;
        private System.Windows.Forms.Label lbl_vampire_rate;
        private System.Windows.Forms.TextBox tbx_vampire_rate;
        private System.Windows.Forms.Label lbl_vigor_recovery_rate;
        private System.Windows.Forms.TextBox tbx_vigor_recovery_rate;
        private System.Windows.Forms.TextBox tbx_atk_rate;
        private System.Windows.Forms.Button btn_item_equip;
        private System.Windows.Forms.Button btn_inventory_req;
        private System.Windows.Forms.RichTextBox rtb_item_list;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RichTextBox rtb_test_command;
        private System.Windows.Forms.Label lbl_atk_rate;
        private System.Windows.Forms.Label lbl_hpmax_add;
        private System.Windows.Forms.TextBox tbx_hpmax_add;
        private System.Windows.Forms.Label lbl_atk_add;
        private System.Windows.Forms.TextBox tbx_atk_add;
        private System.Windows.Forms.TextBox tbx_player_name;
        private System.Windows.Forms.TextBox tbx_player_class;
    }
}

