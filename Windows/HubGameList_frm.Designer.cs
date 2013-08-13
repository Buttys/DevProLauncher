namespace DevProLauncher.Windows
{
    sealed partial class HubGameList_frm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.SearchRequest_Btn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Host_btn = new System.Windows.Forms.Button();
            this.Quick_Btn = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.UserFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.ActiveGames = new System.Windows.Forms.CheckBox();
            this.IlligalGames = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Format = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GameType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BanList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TimeLimit = new System.Windows.Forms.ComboBox();
            this.UpdateLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.DeckSelect = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SearchReset = new System.Windows.Forms.Timer(this.components);
            this.GameListUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.RankedList = new DevProLauncher.Windows.Components.DoubleBufferedListBox();
            this.UnrankedList = new DevProLauncher.Windows.Components.DoubleBufferedListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(915, 485);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.UpdateLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(668, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 285F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(244, 479);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 197);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 279);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(232, 260);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.83186F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.16814F));
            this.tableLayoutPanel5.Controls.Add(this.SearchRequest_Btn, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 193);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(226, 64);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // SearchRequest_Btn
            // 
            this.SearchRequest_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchRequest_Btn.Location = new System.Drawing.Point(3, 3);
            this.SearchRequest_Btn.Name = "SearchRequest_Btn";
            this.SearchRequest_Btn.Size = new System.Drawing.Size(136, 58);
            this.SearchRequest_Btn.TabIndex = 1;
            this.SearchRequest_Btn.Text = "Search";
            this.SearchRequest_Btn.UseVisualStyleBackColor = true;
            this.SearchRequest_Btn.Click += new System.EventHandler(this.SearchRequest_Btn_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.Host_btn);
            this.flowLayoutPanel1.Controls.Add(this.Quick_Btn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(145, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(78, 58);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // Host_btn
            // 
            this.Host_btn.Location = new System.Drawing.Point(3, 3);
            this.Host_btn.Name = "Host_btn";
            this.Host_btn.Size = new System.Drawing.Size(75, 22);
            this.Host_btn.TabIndex = 0;
            this.Host_btn.Text = "Host";
            this.Host_btn.UseVisualStyleBackColor = true;
            this.Host_btn.Click += new System.EventHandler(this.Host_btn_Click);
            // 
            // Quick_Btn
            // 
            this.Quick_Btn.Location = new System.Drawing.Point(3, 31);
            this.Quick_Btn.Name = "Quick_Btn";
            this.Quick_Btn.Size = new System.Drawing.Size(75, 22);
            this.Quick_Btn.TabIndex = 1;
            this.Quick_Btn.Text = "Quick";
            this.Quick_Btn.UseVisualStyleBackColor = true;
            this.Quick_Btn.Click += new System.EventHandler(this.Quick_Btn_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.UserFilter, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(226, 184);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // UserFilter
            // 
            this.UserFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserFilter.Location = new System.Drawing.Point(3, 162);
            this.UserFilter.Name = "UserFilter";
            this.UserFilter.Size = new System.Drawing.Size(220, 20);
            this.UserFilter.TabIndex = 0;
            this.UserFilter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "User Filter";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.ActiveGames, 0, 5);
            this.tableLayoutPanel8.Controls.Add(this.IlligalGames, 1, 5);
            this.tableLayoutPanel8.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.Format, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.GameType, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.BanList, 1, 3);
            this.tableLayoutPanel8.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel8.Controls.Add(this.TimeLimit, 1, 4);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 6;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(220, 128);
            this.tableLayoutPanel8.TabIndex = 2;
            // 
            // ActiveGames
            // 
            this.ActiveGames.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ActiveGames.AutoSize = true;
            this.ActiveGames.Location = new System.Drawing.Point(9, 107);
            this.ActiveGames.Name = "ActiveGames";
            this.ActiveGames.Size = new System.Drawing.Size(92, 17);
            this.ActiveGames.TabIndex = 0;
            this.ActiveGames.Text = "Active Games";
            this.ActiveGames.UseVisualStyleBackColor = true;
            // 
            // IlligalGames
            // 
            this.IlligalGames.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IlligalGames.AutoSize = true;
            this.IlligalGames.Location = new System.Drawing.Point(120, 107);
            this.IlligalGames.Name = "IlligalGames";
            this.IlligalGames.Size = new System.Drawing.Size(89, 17);
            this.IlligalGames.TabIndex = 1;
            this.IlligalGames.Text = "Illegal Games";
            this.IlligalGames.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Format";
            // 
            // Format
            // 
            this.Format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Format.FormattingEnabled = true;
            this.Format.Items.AddRange(new object[] {
            "All",
            "OCG",
            "TCG",
            "OCG/TCG",
            "Anime",
            "Turbo Duel"});
            this.Format.Location = new System.Drawing.Point(113, 6);
            this.Format.Name = "Format";
            this.Format.Size = new System.Drawing.Size(104, 21);
            this.Format.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Game Type";
            // 
            // GameType
            // 
            this.GameType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GameType.FormattingEnabled = true;
            this.GameType.Items.AddRange(new object[] {
            "All",
            "Single",
            "Match",
            "Tag"});
            this.GameType.Location = new System.Drawing.Point(113, 31);
            this.GameType.Name = "GameType";
            this.GameType.Size = new System.Drawing.Size(104, 21);
            this.GameType.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Banlist";
            // 
            // BanList
            // 
            this.BanList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BanList.FormattingEnabled = true;
            this.BanList.Items.AddRange(new object[] {
            "All"});
            this.BanList.Location = new System.Drawing.Point(113, 56);
            this.BanList.Name = "BanList";
            this.BanList.Size = new System.Drawing.Size(104, 21);
            this.BanList.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Time Limit";
            // 
            // TimeLimit
            // 
            this.TimeLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TimeLimit.FormattingEnabled = true;
            this.TimeLimit.Items.AddRange(new object[] {
            "All",
            "3 minutes",
            "5 minutes"});
            this.TimeLimit.Location = new System.Drawing.Point(113, 81);
            this.TimeLimit.Name = "TimeLimit";
            this.TimeLimit.Size = new System.Drawing.Size(104, 21);
            this.TimeLimit.TabIndex = 9;
            // 
            // UpdateLabel
            // 
            this.UpdateLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.UpdateLabel.AutoSize = true;
            this.UpdateLabel.Location = new System.Drawing.Point(69, 181);
            this.UpdateLabel.Name = "UpdateLabel";
            this.UpdateLabel.Size = new System.Drawing.Size(106, 13);
            this.UpdateLabel.TabIndex = 2;
            this.UpdateLabel.Text = "Status: Not Updating";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.91597F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.08403F));
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.DeckSelect, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(238, 41);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 41);
            this.label6.TabIndex = 0;
            this.label6.Text = "Default Deck";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeckSelect
            // 
            this.DeckSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DeckSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeckSelect.FormattingEnabled = true;
            this.DeckSelect.Location = new System.Drawing.Point(98, 10);
            this.DeckSelect.Name = "DeckSelect";
            this.DeckSelect.Size = new System.Drawing.Size(137, 21);
            this.DeckSelect.TabIndex = 1;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(659, 479);
            this.tableLayoutPanel7.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RankedList);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(332, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(324, 473);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ranked";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UnrankedList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 473);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unranked";
            // 
            // SearchReset
            // 
            this.SearchReset.Interval = 1000;
            // 
            // GameListUpdateTimer
            // 
            this.GameListUpdateTimer.Interval = 1000;
            // 
            // RankedList
            // 
            this.RankedList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RankedList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RankedList.FormattingEnabled = true;
            this.RankedList.IntegralHeight = false;
            this.RankedList.ItemHeight = 50;
            this.RankedList.Location = new System.Drawing.Point(3, 16);
            this.RankedList.Name = "RankedList";
            this.RankedList.Size = new System.Drawing.Size(318, 454);
            this.RankedList.TabIndex = 0;
            // 
            // UnrankedList
            // 
            this.UnrankedList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UnrankedList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.UnrankedList.FormattingEnabled = true;
            this.UnrankedList.IntegralHeight = false;
            this.UnrankedList.ItemHeight = 50;
            this.UnrankedList.Location = new System.Drawing.Point(3, 16);
            this.UnrankedList.Name = "UnrankedList";
            this.UnrankedList.Size = new System.Drawing.Size(317, 454);
            this.UnrankedList.TabIndex = 0;
            // 
            // HubGameList_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 485);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HubGameList_frm";
            this.Text = "HubGameList_frm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.GroupBox groupBox3;
        private Components.DoubleBufferedListBox RankedList;
        private System.Windows.Forms.GroupBox groupBox1;
        private Components.DoubleBufferedListBox UnrankedList;
        private System.Windows.Forms.Timer SearchReset;
        private System.Windows.Forms.Label UpdateLabel;
        private System.Windows.Forms.Timer GameListUpdateTimer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button SearchRequest_Btn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button Host_btn;
        private System.Windows.Forms.Button Quick_Btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TextBox UserFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.CheckBox ActiveGames;
        private System.Windows.Forms.CheckBox IlligalGames;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Format;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox GameType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox BanList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox TimeLimit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox DeckSelect;
    }
}