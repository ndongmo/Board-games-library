namespace GCM.view
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxConnection = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.groupBoxGameBoard = new System.Windows.Forms.GroupBox();
            this.buttonQuitGame = new System.Windows.Forms.Button();
            this.dataGridViewGame = new System.Windows.Forms.DataGridView();
            this.Player = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Points = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Matches = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxListofPlayers = new System.Windows.Forms.GroupBox();
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxReceivedR = new System.Windows.Forms.GroupBox();
            this.dataGridViewRequests = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBoxSentR = new System.Windows.Forms.GroupBox();
            this.buttonAbortSent = new System.Windows.Forms.Button();
            this.labelSentTo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBoxComputer = new System.Windows.Forms.GroupBox();
            this.buttonComputer = new System.Windows.Forms.Button();
            this.radioButtonHard = new System.Windows.Forms.RadioButton();
            this.radioButtonNormal = new System.Windows.Forms.RadioButton();
            this.radioButtonEasy = new System.Windows.Forms.RadioButton();
            this.labelLog = new System.Windows.Forms.Label();
            this.groupBoxTchat = new System.Windows.Forms.GroupBox();
            this.richTextBoxMsg = new System.Windows.Forms.RichTextBox();
            this.buttonMsgReset = new System.Windows.Forms.Button();
            this.buttonMsgSend = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxMsg = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.groupBoxConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBoxGameBoard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGame)).BeginInit();
            this.groupBoxListofPlayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.groupBoxReceivedR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).BeginInit();
            this.groupBoxSentR.SuspendLayout();
            this.groupBoxComputer.SuspendLayout();
            this.groupBoxTchat.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 535);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(712, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 17);
            this.toolStripStatusLabel1.Text = "...";
            // 
            // groupBoxConnection
            // 
            this.groupBoxConnection.Controls.Add(this.checkBox1);
            this.groupBoxConnection.Controls.Add(this.numericUpDown1);
            this.groupBoxConnection.Controls.Add(this.buttonRegister);
            this.groupBoxConnection.Controls.Add(this.buttonLogin);
            this.groupBoxConnection.Controls.Add(this.textBoxPassword);
            this.groupBoxConnection.Controls.Add(this.label4);
            this.groupBoxConnection.Controls.Add(this.textBoxLogin);
            this.groupBoxConnection.Controls.Add(this.label3);
            this.groupBoxConnection.Controls.Add(this.label2);
            this.groupBoxConnection.Controls.Add(this.label1);
            this.groupBoxConnection.Controls.Add(this.buttonDisconnect);
            this.groupBoxConnection.Controls.Add(this.textBoxAddress);
            this.groupBoxConnection.Controls.Add(this.buttonConnect);
            this.groupBoxConnection.Location = new System.Drawing.Point(0, 449);
            this.groupBoxConnection.Name = "groupBoxConnection";
            this.groupBoxConnection.Size = new System.Drawing.Size(709, 65);
            this.groupBoxConnection.TabIndex = 1;
            this.groupBoxConnection.TabStop = false;
            this.groupBoxConnection.Text = "Server connection";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(389, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(94, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Remember me";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(278, 39);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(101, 20);
            this.numericUpDown1.TabIndex = 12;
            this.numericUpDown1.Value = new decimal(new int[] {
            4445,
            0,
            0,
            0});
            // 
            // buttonRegister
            // 
            this.buttonRegister.Location = new System.Drawing.Point(631, 15);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(71, 23);
            this.buttonRegister.TabIndex = 11;
            this.buttonRegister.Text = "Register";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(553, 15);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(72, 23);
            this.buttonLogin.TabIndex = 10;
            this.buttonLogin.Text = "Log in";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(278, 15);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(101, 20);
            this.textBoxPassword.TabIndex = 9;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Password :";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(58, 17);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(122, 20);
            this.textBoxLogin.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Login :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Address :";
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(631, 39);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(71, 23);
            this.buttonDisconnect.TabIndex = 3;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(58, 38);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(122, 20);
            this.textBoxAddress.TabIndex = 1;
            this.textBoxAddress.Text = "127.0.0.1";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(553, 39);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(71, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // groupBoxGameBoard
            // 
            this.groupBoxGameBoard.Controls.Add(this.buttonQuitGame);
            this.groupBoxGameBoard.Controls.Add(this.dataGridViewGame);
            this.groupBoxGameBoard.Location = new System.Drawing.Point(0, 0);
            this.groupBoxGameBoard.Name = "groupBoxGameBoard";
            this.groupBoxGameBoard.Size = new System.Drawing.Size(416, 443);
            this.groupBoxGameBoard.TabIndex = 2;
            this.groupBoxGameBoard.TabStop = false;
            this.groupBoxGameBoard.Text = "Game board";
            // 
            // buttonQuitGame
            // 
            this.buttonQuitGame.Location = new System.Drawing.Point(215, 351);
            this.buttonQuitGame.Name = "buttonQuitGame";
            this.buttonQuitGame.Size = new System.Drawing.Size(195, 23);
            this.buttonQuitGame.TabIndex = 2;
            this.buttonQuitGame.Text = "Quit Game";
            this.buttonQuitGame.UseVisualStyleBackColor = true;
            this.buttonQuitGame.Click += new System.EventHandler(this.buttonQuitGame_Click);
            // 
            // dataGridViewGame
            // 
            this.dataGridViewGame.AllowUserToAddRows = false;
            this.dataGridViewGame.AllowUserToDeleteRows = false;
            this.dataGridViewGame.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGame.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Player,
            this.Points,
            this.Matches});
            this.dataGridViewGame.Enabled = false;
            this.dataGridViewGame.Location = new System.Drawing.Point(9, 375);
            this.dataGridViewGame.MultiSelect = false;
            this.dataGridViewGame.Name = "dataGridViewGame";
            this.dataGridViewGame.ReadOnly = true;
            this.dataGridViewGame.RowHeadersVisible = false;
            this.dataGridViewGame.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewGame.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewGame.Size = new System.Drawing.Size(401, 62);
            this.dataGridViewGame.TabIndex = 1;
            // 
            // Player
            // 
            this.Player.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Player.DividerWidth = 1;
            this.Player.HeaderText = "Player";
            this.Player.Name = "Player";
            this.Player.ReadOnly = true;
            // 
            // Points
            // 
            this.Points.DividerWidth = 1;
            this.Points.HeaderText = "Points";
            this.Points.Name = "Points";
            this.Points.ReadOnly = true;
            // 
            // Matches
            // 
            this.Matches.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Matches.HeaderText = "Nb of matches";
            this.Matches.Name = "Matches";
            this.Matches.ReadOnly = true;
            this.Matches.Width = 105;
            // 
            // groupBoxListofPlayers
            // 
            this.groupBoxListofPlayers.Controls.Add(this.dataGridViewList);
            this.groupBoxListofPlayers.Location = new System.Drawing.Point(423, 0);
            this.groupBoxListofPlayers.Name = "groupBoxListofPlayers";
            this.groupBoxListofPlayers.Size = new System.Drawing.Size(286, 168);
            this.groupBoxListofPlayers.TabIndex = 3;
            this.groupBoxListofPlayers.TabStop = false;
            this.groupBoxListofPlayers.Text = "List of connected  players";
            // 
            // dataGridViewList
            // 
            this.dataGridViewList.AllowUserToAddRows = false;
            this.dataGridViewList.AllowUserToDeleteRows = false;
            this.dataGridViewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridViewList.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.ReadOnly = true;
            this.dataGridViewList.RowHeadersVisible = false;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(274, 143);
            this.dataGridViewList.TabIndex = 2;
            this.dataGridViewList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DividerWidth = 1;
            this.dataGridViewTextBoxColumn1.HeaderText = "Player";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DividerWidth = 1;
            this.dataGridViewTextBoxColumn2.HeaderText = "Points";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn3.HeaderText = "Nb of matches";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 101;
            // 
            // groupBoxReceivedR
            // 
            this.groupBoxReceivedR.Controls.Add(this.dataGridViewRequests);
            this.groupBoxReceivedR.Location = new System.Drawing.Point(423, 174);
            this.groupBoxReceivedR.Name = "groupBoxReceivedR";
            this.groupBoxReceivedR.Size = new System.Drawing.Size(286, 156);
            this.groupBoxReceivedR.TabIndex = 4;
            this.groupBoxReceivedR.TabStop = false;
            this.groupBoxReceivedR.Text = "Received requests";
            // 
            // dataGridViewRequests
            // 
            this.dataGridViewRequests.AllowUserToAddRows = false;
            this.dataGridViewRequests.AllowUserToDeleteRows = false;
            this.dataGridViewRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRequests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dataGridViewRequests.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewRequests.Name = "dataGridViewRequests";
            this.dataGridViewRequests.ReadOnly = true;
            this.dataGridViewRequests.RowHeadersVisible = false;
            this.dataGridViewRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewRequests.Size = new System.Drawing.Size(274, 131);
            this.dataGridViewRequests.TabIndex = 3;
            this.dataGridViewRequests.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRequests_CellContentClick);
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DividerWidth = 1;
            this.dataGridViewTextBoxColumn4.HeaderText = "Player";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn5.DividerWidth = 1;
            this.dataGridViewTextBoxColumn5.HeaderText = "Accept";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn5.Width = 67;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn6.HeaderText = "Abort";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn6.Width = 57;
            // 
            // groupBoxSentR
            // 
            this.groupBoxSentR.Controls.Add(this.buttonAbortSent);
            this.groupBoxSentR.Controls.Add(this.labelSentTo);
            this.groupBoxSentR.Controls.Add(this.label5);
            this.groupBoxSentR.Location = new System.Drawing.Point(423, 336);
            this.groupBoxSentR.Name = "groupBoxSentR";
            this.groupBoxSentR.Size = new System.Drawing.Size(286, 51);
            this.groupBoxSentR.TabIndex = 5;
            this.groupBoxSentR.TabStop = false;
            this.groupBoxSentR.Text = "Send request state";
            // 
            // buttonAbortSent
            // 
            this.buttonAbortSent.Location = new System.Drawing.Point(233, 15);
            this.buttonAbortSent.Name = "buttonAbortSent";
            this.buttonAbortSent.Size = new System.Drawing.Size(47, 23);
            this.buttonAbortSent.TabIndex = 2;
            this.buttonAbortSent.Text = "Abort";
            this.buttonAbortSent.UseVisualStyleBackColor = true;
            this.buttonAbortSent.Click += new System.EventHandler(this.buttonAbortSent_Click);
            // 
            // labelSentTo
            // 
            this.labelSentTo.AutoSize = true;
            this.labelSentTo.Location = new System.Drawing.Point(39, 19);
            this.labelSentTo.Name = "labelSentTo";
            this.labelSentTo.Size = new System.Drawing.Size(16, 13);
            this.labelSentTo.TabIndex = 1;
            this.labelSentTo.Text = "...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "To :";
            // 
            // groupBoxComputer
            // 
            this.groupBoxComputer.Controls.Add(this.buttonComputer);
            this.groupBoxComputer.Controls.Add(this.radioButtonHard);
            this.groupBoxComputer.Controls.Add(this.radioButtonNormal);
            this.groupBoxComputer.Controls.Add(this.radioButtonEasy);
            this.groupBoxComputer.Location = new System.Drawing.Point(423, 393);
            this.groupBoxComputer.Name = "groupBoxComputer";
            this.groupBoxComputer.Size = new System.Drawing.Size(286, 50);
            this.groupBoxComputer.TabIndex = 6;
            this.groupBoxComputer.TabStop = false;
            this.groupBoxComputer.Text = "Play with computer";
            // 
            // buttonComputer
            // 
            this.buttonComputer.Location = new System.Drawing.Point(229, 17);
            this.buttonComputer.Name = "buttonComputer";
            this.buttonComputer.Size = new System.Drawing.Size(51, 23);
            this.buttonComputer.TabIndex = 3;
            this.buttonComputer.Text = "Start";
            this.buttonComputer.UseVisualStyleBackColor = true;
            this.buttonComputer.Click += new System.EventHandler(this.buttonComputer_Click);
            // 
            // radioButtonHard
            // 
            this.radioButtonHard.AutoSize = true;
            this.radioButtonHard.Location = new System.Drawing.Point(130, 20);
            this.radioButtonHard.Name = "radioButtonHard";
            this.radioButtonHard.Size = new System.Drawing.Size(48, 17);
            this.radioButtonHard.TabIndex = 2;
            this.radioButtonHard.Text = "Hard";
            this.radioButtonHard.UseVisualStyleBackColor = true;
            // 
            // radioButtonNormal
            // 
            this.radioButtonNormal.AutoSize = true;
            this.radioButtonNormal.Checked = true;
            this.radioButtonNormal.Location = new System.Drawing.Point(66, 20);
            this.radioButtonNormal.Name = "radioButtonNormal";
            this.radioButtonNormal.Size = new System.Drawing.Size(58, 17);
            this.radioButtonNormal.TabIndex = 1;
            this.radioButtonNormal.TabStop = true;
            this.radioButtonNormal.Text = "Normal";
            this.radioButtonNormal.UseVisualStyleBackColor = true;
            // 
            // radioButtonEasy
            // 
            this.radioButtonEasy.AutoSize = true;
            this.radioButtonEasy.Location = new System.Drawing.Point(12, 19);
            this.radioButtonEasy.Name = "radioButtonEasy";
            this.radioButtonEasy.Size = new System.Drawing.Size(48, 17);
            this.radioButtonEasy.TabIndex = 0;
            this.radioButtonEasy.Text = "Easy";
            this.radioButtonEasy.UseVisualStyleBackColor = true;
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(6, 517);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(0, 13);
            this.labelLog.TabIndex = 7;
            // 
            // groupBoxTchat
            // 
            this.groupBoxTchat.Controls.Add(this.richTextBoxMsg);
            this.groupBoxTchat.Controls.Add(this.buttonMsgReset);
            this.groupBoxTchat.Controls.Add(this.buttonMsgSend);
            this.groupBoxTchat.Controls.Add(this.label6);
            this.groupBoxTchat.Controls.Add(this.textBoxMsg);
            this.groupBoxTchat.Location = new System.Drawing.Point(422, 0);
            this.groupBoxTchat.Name = "groupBoxTchat";
            this.groupBoxTchat.Size = new System.Drawing.Size(286, 330);
            this.groupBoxTchat.TabIndex = 8;
            this.groupBoxTchat.TabStop = false;
            this.groupBoxTchat.Text = "Tchat";
            // 
            // richTextBoxMsg
            // 
            this.richTextBoxMsg.Enabled = false;
            this.richTextBoxMsg.Location = new System.Drawing.Point(7, 19);
            this.richTextBoxMsg.Name = "richTextBoxMsg";
            this.richTextBoxMsg.Size = new System.Drawing.Size(273, 220);
            this.richTextBoxMsg.TabIndex = 0;
            this.richTextBoxMsg.Text = "";
            // 
            // buttonMsgReset
            // 
            this.buttonMsgReset.Location = new System.Drawing.Point(60, 301);
            this.buttonMsgReset.Name = "buttonMsgReset";
            this.buttonMsgReset.Size = new System.Drawing.Size(72, 23);
            this.buttonMsgReset.TabIndex = 4;
            this.buttonMsgReset.Text = "Clear tchat";
            this.buttonMsgReset.UseVisualStyleBackColor = true;
            this.buttonMsgReset.Click += new System.EventHandler(this.buttonMsgReset_Click);
            // 
            // buttonMsgSend
            // 
            this.buttonMsgSend.Location = new System.Drawing.Point(7, 301);
            this.buttonMsgSend.Name = "buttonMsgSend";
            this.buttonMsgSend.Size = new System.Drawing.Size(47, 23);
            this.buttonMsgSend.TabIndex = 3;
            this.buttonMsgSend.Text = "Send";
            this.buttonMsgSend.UseVisualStyleBackColor = true;
            this.buttonMsgSend.Click += new System.EventHandler(this.buttonMsgSend_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(176, 306);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Max 100 characters";
            // 
            // textBoxMsg
            // 
            this.textBoxMsg.Location = new System.Drawing.Point(6, 245);
            this.textBoxMsg.MaxLength = 100;
            this.textBoxMsg.Multiline = true;
            this.textBoxMsg.Name = "textBoxMsg";
            this.textBoxMsg.Size = new System.Drawing.Size(274, 52);
            this.textBoxMsg.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 557);
            this.Controls.Add(this.groupBoxTchat);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.groupBoxComputer);
            this.Controls.Add(this.groupBoxSentR);
            this.Controls.Add(this.groupBoxListofPlayers);
            this.Controls.Add(this.groupBoxGameBoard);
            this.Controls.Add(this.groupBoxConnection);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxReceivedR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ClientForm";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxConnection.ResumeLayout(false);
            this.groupBoxConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBoxGameBoard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGame)).EndInit();
            this.groupBoxListofPlayers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.groupBoxReceivedR.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).EndInit();
            this.groupBoxSentR.ResumeLayout(false);
            this.groupBoxSentR.PerformLayout();
            this.groupBoxComputer.ResumeLayout(false);
            this.groupBoxComputer.PerformLayout();
            this.groupBoxTchat.ResumeLayout(false);
            this.groupBoxTchat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBoxConnection;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.GroupBox groupBoxGameBoard;
        private System.Windows.Forms.Button buttonQuitGame;
        private System.Windows.Forms.DataGridView dataGridViewGame;
        private System.Windows.Forms.GroupBox groupBoxListofPlayers;
        private System.Windows.Forms.DataGridView dataGridViewList;
        private System.Windows.Forms.GroupBox groupBoxReceivedR;
        private System.Windows.Forms.GroupBox groupBoxSentR;
        private System.Windows.Forms.Button buttonAbortSent;
        private System.Windows.Forms.Label labelSentTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBoxComputer;
        private System.Windows.Forms.Button buttonComputer;
        private System.Windows.Forms.RadioButton radioButtonHard;
        private System.Windows.Forms.RadioButton radioButtonNormal;
        private System.Windows.Forms.RadioButton radioButtonEasy;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.DataGridView dataGridViewRequests;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Player;
        private System.Windows.Forms.DataGridViewTextBoxColumn Points;
        private System.Windows.Forms.DataGridViewTextBoxColumn Matches;
        private System.Windows.Forms.GroupBox groupBoxTchat;
        private System.Windows.Forms.Button buttonMsgReset;
        private System.Windows.Forms.Button buttonMsgSend;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxMsg;
        private System.Windows.Forms.RichTextBox richTextBoxMsg;
        private System.Windows.Forms.Timer timer1;
    }
}