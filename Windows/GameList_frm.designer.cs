using DevProLauncher.Windows.Components;
namespace DevProLauncher.Windows
{
    sealed partial class GameListFrm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.GameServerSelect = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.UnrankedList = new DevProLauncher.Windows.Components.DoubleBufferedListBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.QuickBtn = new System.Windows.Forms.Button();
            this.HostBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.RankedQuickBtn = new System.Windows.Forms.Button();
            this.RankedHostBtn = new System.Windows.Forms.Button();
            this.RankedList = new DevProLauncher.Windows.Components.DoubleBufferedListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.DeckBtn = new System.Windows.Forms.Button();
            this.ReplaysBtn = new System.Windows.Forms.Button();
            this.ProfileBtn = new System.Windows.Forms.Button();
            this.OptionsBtn = new System.Windows.Forms.Button();
            this.OfflineBtn = new System.Windows.Forms.Button();
            this.LogoutBtn = new System.Windows.Forms.Button();
            this.FilterTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.NumberOfPlayers = new System.Windows.Forms.Label();
            this.NumberOfOpenRooms = new System.Windows.Forms.Label();
            this.NumberofRanked = new System.Windows.Forms.Label();
            this.NumberOfUnranked = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NumberofRooms = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.FilterActive = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.DeckSelect = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.ServerList = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.GameServerSelect.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel1.Controls.Add(this.GameServerSelect, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(909, 383);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // GameServerSelect
            // 
            this.GameServerSelect.ColumnCount = 2;
            this.GameServerSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GameServerSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GameServerSelect.Controls.Add(this.groupBox2, 0, 0);
            this.GameServerSelect.Controls.Add(this.groupBox3, 1, 0);
            this.GameServerSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameServerSelect.Location = new System.Drawing.Point(3, 3);
            this.GameServerSelect.Name = "GameServerSelect";
            this.GameServerSelect.RowCount = 1;
            this.GameServerSelect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GameServerSelect.Size = new System.Drawing.Size(728, 377);
            this.GameServerSelect.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel8);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(358, 371);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unranked";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.UnrankedList, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.flowLayoutPanel2, 0, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(352, 352);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // UnrankedList
            // 
            this.UnrankedList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UnrankedList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.UnrankedList.FormattingEnabled = true;
            this.UnrankedList.IntegralHeight = false;
            this.UnrankedList.ItemHeight = 50;
            this.UnrankedList.Location = new System.Drawing.Point(3, 3);
            this.UnrankedList.Name = "UnrankedList";
            this.UnrankedList.Size = new System.Drawing.Size(346, 306);
            this.UnrankedList.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.QuickBtn);
            this.flowLayoutPanel2.Controls.Add(this.HostBtn);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 315);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(346, 34);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // QuickBtn
            // 
            this.QuickBtn.Location = new System.Drawing.Point(277, 3);
            this.QuickBtn.Name = "QuickBtn";
            this.QuickBtn.Size = new System.Drawing.Size(66, 23);
            this.QuickBtn.TabIndex = 1;
            this.QuickBtn.Text = "Quick";
            this.QuickBtn.UseVisualStyleBackColor = true;
            this.QuickBtn.Click += new System.EventHandler(this.QuickBtn_Click);
            // 
            // HostBtn
            // 
            this.HostBtn.Location = new System.Drawing.Point(205, 3);
            this.HostBtn.Name = "HostBtn";
            this.HostBtn.Size = new System.Drawing.Size(66, 23);
            this.HostBtn.TabIndex = 0;
            this.HostBtn.Text = "Host";
            this.HostBtn.UseVisualStyleBackColor = true;
            this.HostBtn.Click += new System.EventHandler(this.HostBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel9);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(367, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(358, 371);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ranked";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.flowLayoutPanel3, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.RankedList, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(352, 352);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.RankedQuickBtn);
            this.flowLayoutPanel3.Controls.Add(this.RankedHostBtn);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 315);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(346, 34);
            this.flowLayoutPanel3.TabIndex = 4;
            // 
            // RankedQuickBtn
            // 
            this.RankedQuickBtn.Location = new System.Drawing.Point(277, 3);
            this.RankedQuickBtn.Name = "RankedQuickBtn";
            this.RankedQuickBtn.Size = new System.Drawing.Size(66, 23);
            this.RankedQuickBtn.TabIndex = 1;
            this.RankedQuickBtn.Text = "Quick";
            this.RankedQuickBtn.UseVisualStyleBackColor = true;
            this.RankedQuickBtn.Click += new System.EventHandler(this.QuickBtn_Click);
            // 
            // RankedHostBtn
            // 
            this.RankedHostBtn.Location = new System.Drawing.Point(205, 3);
            this.RankedHostBtn.Name = "RankedHostBtn";
            this.RankedHostBtn.Size = new System.Drawing.Size(66, 23);
            this.RankedHostBtn.TabIndex = 0;
            this.RankedHostBtn.Text = "Host";
            this.RankedHostBtn.UseVisualStyleBackColor = true;
            this.RankedHostBtn.Click += new System.EventHandler(this.HostBtn_Click);
            // 
            // RankedList
            // 
            this.RankedList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RankedList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RankedList.FormattingEnabled = true;
            this.RankedList.IntegralHeight = false;
            this.RankedList.ItemHeight = 50;
            this.RankedList.Location = new System.Drawing.Point(3, 3);
            this.RankedList.Name = "RankedList";
            this.RankedList.Size = new System.Drawing.Size(346, 306);
            this.RankedList.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.FilterTextBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.FilterActive, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(737, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(169, 377);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.907976F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95.09203F));
            this.tableLayoutPanel6.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 273);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(163, 101);
            this.tableLayoutPanel6.TabIndex = 11;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.DeckBtn);
            this.flowLayoutPanel1.Controls.Add(this.ReplaysBtn);
            this.flowLayoutPanel1.Controls.Add(this.ProfileBtn);
            this.flowLayoutPanel1.Controls.Add(this.OptionsBtn);
            this.flowLayoutPanel1.Controls.Add(this.OfflineBtn);
            this.flowLayoutPanel1.Controls.Add(this.LogoutBtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(11, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(149, 95);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // DeckBtn
            // 
            this.DeckBtn.Location = new System.Drawing.Point(3, 3);
            this.DeckBtn.Name = "DeckBtn";
            this.DeckBtn.Size = new System.Drawing.Size(66, 23);
            this.DeckBtn.TabIndex = 2;
            this.DeckBtn.Text = "Deck Edit";
            this.DeckBtn.UseVisualStyleBackColor = true;
            this.DeckBtn.Click += new System.EventHandler(this.DeckBtn_Click);
            // 
            // ReplaysBtn
            // 
            this.ReplaysBtn.Location = new System.Drawing.Point(3, 32);
            this.ReplaysBtn.Name = "ReplaysBtn";
            this.ReplaysBtn.Size = new System.Drawing.Size(66, 23);
            this.ReplaysBtn.TabIndex = 3;
            this.ReplaysBtn.Text = "Replays";
            this.ReplaysBtn.UseVisualStyleBackColor = true;
            this.ReplaysBtn.Click += new System.EventHandler(this.ReplaysBtn_Click);
            // 
            // ProfileBtn
            // 
            this.ProfileBtn.Location = new System.Drawing.Point(3, 61);
            this.ProfileBtn.Name = "ProfileBtn";
            this.ProfileBtn.Size = new System.Drawing.Size(66, 23);
            this.ProfileBtn.TabIndex = 8;
            this.ProfileBtn.Text = "Profile";
            this.ProfileBtn.UseVisualStyleBackColor = true;
            this.ProfileBtn.Click += new System.EventHandler(this.ProfileBtn_Click);
            // 
            // OptionsBtn
            // 
            this.OptionsBtn.Location = new System.Drawing.Point(75, 3);
            this.OptionsBtn.Name = "OptionsBtn";
            this.OptionsBtn.Size = new System.Drawing.Size(66, 23);
            this.OptionsBtn.TabIndex = 9;
            this.OptionsBtn.Text = "Options";
            this.OptionsBtn.UseVisualStyleBackColor = true;
            this.OptionsBtn.Click += new System.EventHandler(this.OptionsBtn_Click);
            // 
            // OfflineBtn
            // 
            this.OfflineBtn.Location = new System.Drawing.Point(75, 32);
            this.OfflineBtn.Name = "OfflineBtn";
            this.OfflineBtn.Size = new System.Drawing.Size(66, 23);
            this.OfflineBtn.TabIndex = 10;
            this.OfflineBtn.Text = "Offline";
            this.OfflineBtn.UseVisualStyleBackColor = true;
            this.OfflineBtn.Click += new System.EventHandler(this.OfflineBtn_Click);
            // 
            // LogoutBtn
            // 
            this.LogoutBtn.Location = new System.Drawing.Point(75, 61);
            this.LogoutBtn.Name = "LogoutBtn";
            this.LogoutBtn.Size = new System.Drawing.Size(66, 23);
            this.LogoutBtn.TabIndex = 11;
            this.LogoutBtn.Text = "Logout";
            this.LogoutBtn.UseVisualStyleBackColor = true;
            this.LogoutBtn.Click += new System.EventHandler(this.LogoutBtn_Click);
            // 
            // FilterTextBox
            // 
            this.FilterTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterTextBox.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.FilterTextBox.Location = new System.Drawing.Point(3, 3);
            this.FilterTextBox.Name = "FilterTextBox";
            this.FilterTextBox.Size = new System.Drawing.Size(163, 20);
            this.FilterTextBox.TabIndex = 0;
            this.FilterTextBox.Text = "Search";
            this.FilterTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FilterTextBox.Enter += new System.EventHandler(this.FilterTextBox_Enter);
            this.FilterTextBox.Leave += new System.EventHandler(this.FilterTextBox_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 129);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Details";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.30986F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.69014F));
            this.tableLayoutPanel3.Controls.Add(this.NumberOfPlayers, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.NumberOfOpenRooms, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.NumberofRanked, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.NumberOfUnranked, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.NumberofRooms, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(157, 110);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // NumberOfPlayers
            // 
            this.NumberOfPlayers.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NumberOfPlayers.AutoSize = true;
            this.NumberOfPlayers.Location = new System.Drawing.Point(125, 92);
            this.NumberOfPlayers.Name = "NumberOfPlayers";
            this.NumberOfPlayers.Size = new System.Drawing.Size(13, 13);
            this.NumberOfPlayers.TabIndex = 9;
            this.NumberOfPlayers.Text = "0";
            // 
            // NumberOfOpenRooms
            // 
            this.NumberOfOpenRooms.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NumberOfOpenRooms.AutoSize = true;
            this.NumberOfOpenRooms.Location = new System.Drawing.Point(125, 70);
            this.NumberOfOpenRooms.Name = "NumberOfOpenRooms";
            this.NumberOfOpenRooms.Size = new System.Drawing.Size(13, 13);
            this.NumberOfOpenRooms.TabIndex = 8;
            this.NumberOfOpenRooms.Text = "0";
            // 
            // NumberofRanked
            // 
            this.NumberofRanked.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NumberofRanked.AutoSize = true;
            this.NumberofRanked.Location = new System.Drawing.Point(125, 48);
            this.NumberofRanked.Name = "NumberofRanked";
            this.NumberofRanked.Size = new System.Drawing.Size(13, 13);
            this.NumberofRanked.TabIndex = 7;
            this.NumberofRanked.Text = "0";
            // 
            // NumberOfUnranked
            // 
            this.NumberOfUnranked.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NumberOfUnranked.AutoSize = true;
            this.NumberOfUnranked.Location = new System.Drawing.Point(125, 26);
            this.NumberOfUnranked.Name = "NumberOfUnranked";
            this.NumberOfUnranked.Size = new System.Drawing.Size(13, 13);
            this.NumberOfUnranked.TabIndex = 6;
            this.NumberOfUnranked.Text = "0";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "# of Rooms";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "# of Unranked";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "# of Players";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NumberofRooms
            // 
            this.NumberofRooms.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NumberofRooms.AutoSize = true;
            this.NumberofRooms.Location = new System.Drawing.Point(125, 4);
            this.NumberofRooms.Name = "NumberofRooms";
            this.NumberofRooms.Size = new System.Drawing.Size(13, 13);
            this.NumberofRooms.TabIndex = 5;
            this.NumberofRooms.Text = "0";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "# of Ranked";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 22);
            this.label5.TabIndex = 10;
            this.label5.Text = "# of Open Rooms";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FilterActive
            // 
            this.FilterActive.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.FilterActive.AutoSize = true;
            this.FilterActive.Checked = true;
            this.FilterActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FilterActive.Location = new System.Drawing.Point(26, 219);
            this.FilterActive.Name = "FilterActive";
            this.FilterActive.Size = new System.Drawing.Size(117, 15);
            this.FilterActive.TabIndex = 3;
            this.FilterActive.Text = "Filter Active Games";
            this.FilterActive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FilterActive.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.94702F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 92.05298F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel4.Controls.Add(this.DeckSelect, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 240);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(163, 27);
            this.tableLayoutPanel4.TabIndex = 12;
            // 
            // DeckSelect
            // 
            this.DeckSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeckSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeckSelect.FormattingEnabled = true;
            this.DeckSelect.Location = new System.Drawing.Point(11, 3);
            this.DeckSelect.Name = "DeckSelect";
            this.DeckSelect.Size = new System.Drawing.Size(88, 21);
            this.DeckSelect.TabIndex = 14;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.ServerList, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 167);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(163, 46);
            this.tableLayoutPanel5.TabIndex = 13;
            // 
            // ServerList
            // 
            this.ServerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ServerList.FormattingEnabled = true;
            this.ServerList.Items.AddRange(new object[] {
            "Random"});
            this.ServerList.Location = new System.Drawing.Point(3, 19);
            this.ServerList.Name = "ServerList";
            this.ServerList.Size = new System.Drawing.Size(157, 21);
            this.ServerList.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Server List";
            // 
            // GameListFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(909, 383);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GameListFrm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.GameServerSelect.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox FilterTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label NumberOfPlayers;
        private System.Windows.Forms.Label NumberOfOpenRooms;
        private System.Windows.Forms.Label NumberofRanked;
        private System.Windows.Forms.Label NumberOfUnranked;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label NumberofRooms;
        private System.Windows.Forms.CheckBox FilterActive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button DeckBtn;
        private System.Windows.Forms.Button ReplaysBtn;
        private System.Windows.Forms.Button ProfileBtn;
        private System.Windows.Forms.Button OptionsBtn;
        private System.Windows.Forms.Button OfflineBtn;
        private System.Windows.Forms.Button LogoutBtn;
        private System.Windows.Forms.TableLayoutPanel GameServerSelect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private DoubleBufferedListBox UnrankedList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button QuickBtn;
        private System.Windows.Forms.Button HostBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button RankedQuickBtn;
        private System.Windows.Forms.Button RankedHostBtn;
        private DoubleBufferedListBox RankedList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ComboBox DeckSelect;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ComboBox ServerList;
        private System.Windows.Forms.Label label6;
    }
}