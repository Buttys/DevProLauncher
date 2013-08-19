﻿using DevProLauncher.Windows.Components;
namespace DevProLauncher.Windows
{
    sealed partial class ChatFrm
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
            this.UserTab = new System.Windows.Forms.TabPage();
            this.FriendTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.ChatInput = new System.Windows.Forms.TextBox();
            this.ChannelListBtn = new System.Windows.Forms.Button();
            this.LeaveBtn = new System.Windows.Forms.Button();
            this.ChannelTabs = new DevProLauncher.Windows.Components.FixedTabControl();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.UsersControl = new DevProLauncher.Windows.Components.FixedTabControl();
            this.UsersTab = new System.Windows.Forms.TabPage();
            this.UserListTabs = new DevProLauncher.Windows.Components.FixedTabControl();
            this.OnlineTab = new System.Windows.Forms.TabPage();
            this.UserList = new System.Windows.Forms.ListBox();
            this.TeamTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.AddUserBtn = new System.Windows.Forms.Button();
            this.TeamStatsbtn = new System.Windows.Forms.Button();
            this.TeamList = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.TeamNameLabel = new System.Windows.Forms.Label();
            this.FriendsTab = new System.Windows.Forms.TabPage();
            this.FriendList = new System.Windows.Forms.ListBox();
            this.IgnoreTab = new System.Windows.Forms.TabPage();
            this.IgnoreList = new System.Windows.Forms.ListBox();
            this.OptionsTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.refuseteamchk = new System.Windows.Forms.CheckBox();
            this.usernamecolorchk = new System.Windows.Forms.CheckBox();
            this.Colorblindchk = new System.Windows.Forms.CheckBox();
            this.Timestampchk = new System.Windows.Forms.CheckBox();
            this.DuelRequestchk = new System.Windows.Forms.CheckBox();
            this.HideJoinLeavechk = new System.Windows.Forms.CheckBox();
            this.pmwindowchk = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.SystemColorBtn = new System.Windows.Forms.Button();
            this.LeaveColorBtn = new System.Windows.Forms.Button();
            this.JoinColorBtn = new System.Windows.Forms.Button();
            this.MeColorBtn = new System.Windows.Forms.Button();
            this.ServerColorBtn = new System.Windows.Forms.Button();
            this.NormalUserColorBtn = new System.Windows.Forms.Button();
            this.Level1ColorBtn = new System.Windows.Forms.Button();
            this.Level2ColorBtn = new System.Windows.Forms.Button();
            this.Level99ColorBtn = new System.Windows.Forms.Button();
            this.NormalTextColorBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.BackgroundColorBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.FontList = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.FontSize = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.UserSearch = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.UsersControl.SuspendLayout();
            this.UsersTab.SuspendLayout();
            this.UserListTabs.SuspendLayout();
            this.OnlineTab.SuspendLayout();
            this.TeamTab.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.FriendsTab.SuspendLayout();
            this.IgnoreTab.SuspendLayout();
            this.OptionsTab.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FontSize)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserTab
            // 
            this.UserTab.BackColor = System.Drawing.SystemColors.Control;
            this.UserTab.Location = new System.Drawing.Point(4, 4);
            this.UserTab.Name = "UserTab";
            this.UserTab.Padding = new System.Windows.Forms.Padding(3);
            this.UserTab.Size = new System.Drawing.Size(155, 463);
            this.UserTab.TabIndex = 0;
            this.UserTab.Text = "Users";
            // 
            // FriendTab
            // 
            this.FriendTab.Location = new System.Drawing.Point(4, 4);
            this.FriendTab.Name = "FriendTab";
            this.FriendTab.Padding = new System.Windows.Forms.Padding(3);
            this.FriendTab.Size = new System.Drawing.Size(155, 463);
            this.FriendTab.TabIndex = 1;
            this.FriendTab.Text = "Friends";
            this.FriendTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 193F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(923, 531);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ChannelTabs, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(724, 525);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel5.Controls.Add(this.ChatInput, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.ChannelListBtn, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.LeaveBtn, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 495);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(718, 27);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // ChatInput
            // 
            this.ChatInput.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChatInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatInput.Location = new System.Drawing.Point(3, 3);
            this.ChatInput.MaxLength = 250;
            this.ChatInput.Name = "ChatInput";
            this.ChatInput.Size = new System.Drawing.Size(546, 20);
            this.ChatInput.TabIndex = 4;
            // 
            // ChannelListBtn
            // 
            this.ChannelListBtn.Location = new System.Drawing.Point(555, 3);
            this.ChannelListBtn.Name = "ChannelListBtn";
            this.ChannelListBtn.Size = new System.Drawing.Size(74, 21);
            this.ChannelListBtn.TabIndex = 5;
            this.ChannelListBtn.Text = "Channel List";
            this.ChannelListBtn.UseVisualStyleBackColor = true;
            this.ChannelListBtn.Click += new System.EventHandler(this.ChannelListBtn_Click);
            // 
            // LeaveBtn
            // 
            this.LeaveBtn.Location = new System.Drawing.Point(638, 3);
            this.LeaveBtn.Name = "LeaveBtn";
            this.LeaveBtn.Size = new System.Drawing.Size(75, 21);
            this.LeaveBtn.TabIndex = 6;
            this.LeaveBtn.Text = "Leave";
            this.LeaveBtn.UseVisualStyleBackColor = true;
            this.LeaveBtn.Click += new System.EventHandler(this.LeaveBtn_Click);
            // 
            // ChannelTabs
            // 
            this.ChannelTabs.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.ChannelTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChannelTabs.Location = new System.Drawing.Point(3, 3);
            this.ChannelTabs.Multiline = true;
            this.ChannelTabs.Name = "ChannelTabs";
            this.ChannelTabs.SelectedIndex = 0;
            this.ChannelTabs.Size = new System.Drawing.Size(718, 486);
            this.ChannelTabs.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.UsersControl, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(733, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(187, 525);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // UsersControl
            // 
            this.UsersControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.UsersControl.Controls.Add(this.UsersTab);
            this.UsersControl.Controls.Add(this.IgnoreTab);
            this.UsersControl.Controls.Add(this.OptionsTab);
            this.UsersControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsersControl.Location = new System.Drawing.Point(3, 3);
            this.UsersControl.Name = "UsersControl";
            this.UsersControl.SelectedIndex = 0;
            this.UsersControl.Size = new System.Drawing.Size(181, 486);
            this.UsersControl.TabIndex = 1;
            // 
            // UsersTab
            // 
            this.UsersTab.Controls.Add(this.UserListTabs);
            this.UsersTab.Location = new System.Drawing.Point(4, 4);
            this.UsersTab.Name = "UsersTab";
            this.UsersTab.Padding = new System.Windows.Forms.Padding(3);
            this.UsersTab.Size = new System.Drawing.Size(173, 460);
            this.UsersTab.TabIndex = 0;
            this.UsersTab.Text = "Users";
            this.UsersTab.UseVisualStyleBackColor = true;
            // 
            // UserListTabs
            // 
            this.UserListTabs.Controls.Add(this.OnlineTab);
            this.UserListTabs.Controls.Add(this.TeamTab);
            this.UserListTabs.Controls.Add(this.FriendsTab);
            this.UserListTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserListTabs.Location = new System.Drawing.Point(3, 3);
            this.UserListTabs.Multiline = true;
            this.UserListTabs.Name = "UserListTabs";
            this.UserListTabs.SelectedIndex = 0;
            this.UserListTabs.Size = new System.Drawing.Size(167, 454);
            this.UserListTabs.TabIndex = 0;
            // 
            // OnlineTab
            // 
            this.OnlineTab.Controls.Add(this.UserList);
            this.OnlineTab.Location = new System.Drawing.Point(4, 22);
            this.OnlineTab.Name = "OnlineTab";
            this.OnlineTab.Padding = new System.Windows.Forms.Padding(3);
            this.OnlineTab.Size = new System.Drawing.Size(159, 428);
            this.OnlineTab.TabIndex = 0;
            this.OnlineTab.Text = "Online";
            this.OnlineTab.UseVisualStyleBackColor = true;
            // 
            // UserList
            // 
            this.UserList.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.UserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.UserList.FormattingEnabled = true;
            this.UserList.IntegralHeight = false;
            this.UserList.Location = new System.Drawing.Point(3, 3);
            this.UserList.Name = "UserList";
            this.UserList.Size = new System.Drawing.Size(153, 422);
            this.UserList.TabIndex = 2;
            // 
            // TeamTab
            // 
            this.TeamTab.Controls.Add(this.tableLayoutPanel9);
            this.TeamTab.Location = new System.Drawing.Point(4, 22);
            this.TeamTab.Name = "TeamTab";
            this.TeamTab.Padding = new System.Windows.Forms.Padding(3);
            this.TeamTab.Size = new System.Drawing.Size(159, 428);
            this.TeamTab.TabIndex = 1;
            this.TeamTab.Text = "Team";
            this.TeamTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.TeamList, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel10, 0, 1);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 3;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(153, 422);
            this.tableLayoutPanel9.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.AddUserBtn);
            this.flowLayoutPanel1.Controls.Add(this.TeamStatsbtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 392);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(147, 27);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // AddUserBtn
            // 
            this.AddUserBtn.Location = new System.Drawing.Point(3, 3);
            this.AddUserBtn.Name = "AddUserBtn";
            this.AddUserBtn.Size = new System.Drawing.Size(66, 23);
            this.AddUserBtn.TabIndex = 0;
            this.AddUserBtn.Text = "Add";
            this.AddUserBtn.UseVisualStyleBackColor = true;
            this.AddUserBtn.Click += new System.EventHandler(this.AddUserBtn_Click);
            // 
            // TeamStatsbtn
            // 
            this.TeamStatsbtn.Location = new System.Drawing.Point(75, 3);
            this.TeamStatsbtn.Name = "TeamStatsbtn";
            this.TeamStatsbtn.Size = new System.Drawing.Size(66, 23);
            this.TeamStatsbtn.TabIndex = 1;
            this.TeamStatsbtn.Text = "Stats";
            this.TeamStatsbtn.UseVisualStyleBackColor = true;
            this.TeamStatsbtn.Click += new System.EventHandler(this.TeamStatsbtn_Click);
            // 
            // TeamList
            // 
            this.TeamList.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.TeamList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TeamList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.TeamList.FormattingEnabled = true;
            this.TeamList.IntegralHeight = false;
            this.TeamList.Location = new System.Drawing.Point(3, 3);
            this.TeamList.Name = "TeamList";
            this.TeamList.Size = new System.Drawing.Size(147, 358);
            this.TeamList.TabIndex = 2;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Controls.Add(this.TeamNameLabel, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 367);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(147, 19);
            this.tableLayoutPanel10.TabIndex = 5;
            // 
            // TeamNameLabel
            // 
            this.TeamNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TeamNameLabel.Location = new System.Drawing.Point(3, 0);
            this.TeamNameLabel.Name = "TeamNameLabel";
            this.TeamNameLabel.Size = new System.Drawing.Size(141, 19);
            this.TeamNameLabel.TabIndex = 4;
            this.TeamNameLabel.Text = "Team: None";
            this.TeamNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FriendsTab
            // 
            this.FriendsTab.Controls.Add(this.FriendList);
            this.FriendsTab.Location = new System.Drawing.Point(4, 22);
            this.FriendsTab.Name = "FriendsTab";
            this.FriendsTab.Size = new System.Drawing.Size(159, 428);
            this.FriendsTab.TabIndex = 2;
            this.FriendsTab.Text = "Friends";
            this.FriendsTab.UseVisualStyleBackColor = true;
            // 
            // FriendList
            // 
            this.FriendList.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.FriendList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FriendList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.FriendList.FormattingEnabled = true;
            this.FriendList.IntegralHeight = false;
            this.FriendList.Location = new System.Drawing.Point(0, 0);
            this.FriendList.Name = "FriendList";
            this.FriendList.Size = new System.Drawing.Size(159, 428);
            this.FriendList.TabIndex = 2;
            // 
            // IgnoreTab
            // 
            this.IgnoreTab.Controls.Add(this.IgnoreList);
            this.IgnoreTab.Location = new System.Drawing.Point(4, 4);
            this.IgnoreTab.Name = "IgnoreTab";
            this.IgnoreTab.Padding = new System.Windows.Forms.Padding(3);
            this.IgnoreTab.Size = new System.Drawing.Size(173, 460);
            this.IgnoreTab.TabIndex = 3;
            this.IgnoreTab.Text = "Ignore";
            this.IgnoreTab.UseVisualStyleBackColor = true;
            // 
            // IgnoreList
            // 
            this.IgnoreList.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.IgnoreList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IgnoreList.FormattingEnabled = true;
            this.IgnoreList.IntegralHeight = false;
            this.IgnoreList.Location = new System.Drawing.Point(3, 3);
            this.IgnoreList.Name = "IgnoreList";
            this.IgnoreList.Size = new System.Drawing.Size(167, 454);
            this.IgnoreList.TabIndex = 2;
            // 
            // OptionsTab
            // 
            this.OptionsTab.BackColor = System.Drawing.SystemColors.Window;
            this.OptionsTab.Controls.Add(this.tableLayoutPanel4);
            this.OptionsTab.Location = new System.Drawing.Point(4, 4);
            this.OptionsTab.Name = "OptionsTab";
            this.OptionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.OptionsTab.Size = new System.Drawing.Size(173, 460);
            this.OptionsTab.TabIndex = 2;
            this.OptionsTab.Text = "Options";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox7, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox5, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 198F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(167, 454);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tableLayoutPanel14);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(161, 192);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Settings";
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 1;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Controls.Add(this.refuseteamchk, 0, 6);
            this.tableLayoutPanel14.Controls.Add(this.usernamecolorchk, 0, 5);
            this.tableLayoutPanel14.Controls.Add(this.Colorblindchk, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.Timestampchk, 0, 1);
            this.tableLayoutPanel14.Controls.Add(this.DuelRequestchk, 0, 2);
            this.tableLayoutPanel14.Controls.Add(this.HideJoinLeavechk, 0, 3);
            this.tableLayoutPanel14.Controls.Add(this.pmwindowchk, 0, 4);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 7;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(155, 173);
            this.tableLayoutPanel14.TabIndex = 0;
            // 
            // refuseteamchk
            // 
            this.refuseteamchk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.refuseteamchk.AutoSize = true;
            this.refuseteamchk.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.refuseteamchk.Location = new System.Drawing.Point(15, 153);
            this.refuseteamchk.Name = "refuseteamchk";
            this.refuseteamchk.Size = new System.Drawing.Size(124, 17);
            this.refuseteamchk.TabIndex = 8;
            this.refuseteamchk.Text = "Refuse Team Invites";
            this.refuseteamchk.UseVisualStyleBackColor = true;
            // 
            // usernamecolorchk
            // 
            this.usernamecolorchk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.usernamecolorchk.AutoSize = true;
            this.usernamecolorchk.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.usernamecolorchk.Location = new System.Drawing.Point(24, 129);
            this.usernamecolorchk.Name = "usernamecolorchk";
            this.usernamecolorchk.Size = new System.Drawing.Size(106, 17);
            this.usernamecolorchk.TabIndex = 7;
            this.usernamecolorchk.Text = "Username Colors";
            this.usernamecolorchk.UseVisualStyleBackColor = true;
            // 
            // Colorblindchk
            // 
            this.Colorblindchk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Colorblindchk.AutoSize = true;
            this.Colorblindchk.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Colorblindchk.Location = new System.Drawing.Point(24, 4);
            this.Colorblindchk.Name = "Colorblindchk";
            this.Colorblindchk.Size = new System.Drawing.Size(106, 17);
            this.Colorblindchk.TabIndex = 2;
            this.Colorblindchk.Text = "Color Blind Mode";
            this.Colorblindchk.UseVisualStyleBackColor = true;
            // 
            // Timestampchk
            // 
            this.Timestampchk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Timestampchk.AutoSize = true;
            this.Timestampchk.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Timestampchk.Location = new System.Drawing.Point(21, 29);
            this.Timestampchk.Name = "Timestampchk";
            this.Timestampchk.Size = new System.Drawing.Size(112, 17);
            this.Timestampchk.TabIndex = 3;
            this.Timestampchk.Text = "Show Time Stamp";
            this.Timestampchk.UseVisualStyleBackColor = true;
            // 
            // DuelRequestchk
            // 
            this.DuelRequestchk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DuelRequestchk.AutoSize = true;
            this.DuelRequestchk.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DuelRequestchk.Location = new System.Drawing.Point(32, 54);
            this.DuelRequestchk.Name = "DuelRequestchk";
            this.DuelRequestchk.Size = new System.Drawing.Size(90, 17);
            this.DuelRequestchk.TabIndex = 4;
            this.DuelRequestchk.Text = "Refuse Duels";
            this.DuelRequestchk.UseVisualStyleBackColor = true;
            // 
            // HideJoinLeavechk
            // 
            this.HideJoinLeavechk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.HideJoinLeavechk.AutoSize = true;
            this.HideJoinLeavechk.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.HideJoinLeavechk.Location = new System.Drawing.Point(25, 79);
            this.HideJoinLeavechk.Name = "HideJoinLeavechk";
            this.HideJoinLeavechk.Size = new System.Drawing.Size(105, 17);
            this.HideJoinLeavechk.TabIndex = 5;
            this.HideJoinLeavechk.Text = "Hide Join/Leave";
            this.HideJoinLeavechk.UseVisualStyleBackColor = true;
            // 
            // pmwindowchk
            // 
            this.pmwindowchk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pmwindowchk.AutoSize = true;
            this.pmwindowchk.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pmwindowchk.Location = new System.Drawing.Point(33, 104);
            this.pmwindowchk.Name = "pmwindowchk";
            this.pmwindowchk.Size = new System.Drawing.Size(89, 17);
            this.pmwindowchk.TabIndex = 6;
            this.pmwindowchk.Text = "PM Windows";
            this.pmwindowchk.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel11);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 300);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(161, 151);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Color Style";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.AutoScroll = true;
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.83333F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.16667F));
            this.tableLayoutPanel11.Controls.Add(this.SystemColorBtn, 1, 10);
            this.tableLayoutPanel11.Controls.Add(this.LeaveColorBtn, 1, 9);
            this.tableLayoutPanel11.Controls.Add(this.JoinColorBtn, 1, 8);
            this.tableLayoutPanel11.Controls.Add(this.MeColorBtn, 1, 7);
            this.tableLayoutPanel11.Controls.Add(this.ServerColorBtn, 1, 6);
            this.tableLayoutPanel11.Controls.Add(this.NormalUserColorBtn, 1, 5);
            this.tableLayoutPanel11.Controls.Add(this.Level1ColorBtn, 1, 4);
            this.tableLayoutPanel11.Controls.Add(this.Level2ColorBtn, 1, 3);
            this.tableLayoutPanel11.Controls.Add(this.Level99ColorBtn, 1, 2);
            this.tableLayoutPanel11.Controls.Add(this.NormalTextColorBtn, 1, 1);
            this.tableLayoutPanel11.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel11.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel11.Controls.Add(this.label4, 0, 5);
            this.tableLayoutPanel11.Controls.Add(this.label9, 0, 4);
            this.tableLayoutPanel11.Controls.Add(this.label10, 0, 3);
            this.tableLayoutPanel11.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel11.Controls.Add(this.label12, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.label14, 0, 8);
            this.tableLayoutPanel11.Controls.Add(this.label15, 0, 9);
            this.tableLayoutPanel11.Controls.Add(this.label16, 0, 10);
            this.tableLayoutPanel11.Controls.Add(this.BackgroundColorBtn, 1, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tableLayoutPanel11.RowCount = 12;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(155, 132);
            this.tableLayoutPanel11.TabIndex = 1;
            // 
            // SystemColorBtn
            // 
            this.SystemColorBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SystemColorBtn.Location = new System.Drawing.Point(106, 253);
            this.SystemColorBtn.Name = "SystemColorBtn";
            this.SystemColorBtn.Size = new System.Drawing.Size(19, 19);
            this.SystemColorBtn.TabIndex = 38;
            this.SystemColorBtn.UseVisualStyleBackColor = true;
            this.SystemColorBtn.Click += new System.EventHandler(this.ApplyNewColor);
            // 
            // LeaveColorBtn
            // 
            this.LeaveColorBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeaveColorBtn.Location = new System.Drawing.Point(106, 228);
            this.LeaveColorBtn.Name = "LeaveColorBtn";
            this.LeaveColorBtn.Size = new System.Drawing.Size(19, 19);
            this.LeaveColorBtn.TabIndex = 37;
            this.LeaveColorBtn.UseVisualStyleBackColor = true;
            this.LeaveColorBtn.Click += new System.EventHandler(this.ApplyNewColor);
            // 
            // JoinColorBtn
            // 
            this.JoinColorBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JoinColorBtn.Location = new System.Drawing.Point(106, 203);
            this.JoinColorBtn.Name = "JoinColorBtn";
            this.JoinColorBtn.Size = new System.Drawing.Size(19, 19);
            this.JoinColorBtn.TabIndex = 36;
            this.JoinColorBtn.UseVisualStyleBackColor = true;
            this.JoinColorBtn.Click += new System.EventHandler(this.ApplyNewColor);
            // 
            // MeColorBtn
            // 
            this.MeColorBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MeColorBtn.Location = new System.Drawing.Point(106, 178);
            this.MeColorBtn.Name = "MeColorBtn";
            this.MeColorBtn.Size = new System.Drawing.Size(19, 19);
            this.MeColorBtn.TabIndex = 35;
            this.MeColorBtn.UseVisualStyleBackColor = true;
            this.MeColorBtn.Click += new System.EventHandler(this.ApplyNewColor);
            // 
            // ServerColorBtn
            // 
            this.ServerColorBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerColorBtn.Location = new System.Drawing.Point(106, 153);
            this.ServerColorBtn.Name = "ServerColorBtn";
            this.ServerColorBtn.Size = new System.Drawing.Size(19, 19);
            this.ServerColorBtn.TabIndex = 34;
            this.ServerColorBtn.UseVisualStyleBackColor = true;
            this.ServerColorBtn.Click += new System.EventHandler(this.ApplyNewColor);
            // 
            // NormalUserColorBtn
            // 
            this.NormalUserColorBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NormalUserColorBtn.Location = new System.Drawing.Point(106, 128);
            this.NormalUserColorBtn.Name = "NormalUserColorBtn";
            this.NormalUserColorBtn.Size = new System.Drawing.Size(19, 19);
            this.NormalUserColorBtn.TabIndex = 33;
            this.NormalUserColorBtn.UseVisualStyleBackColor = true;
            this.NormalUserColorBtn.Click += new System.EventHandler(this.ApplyNewColor);
            // 
            // Level1ColorBtn
            // 
            this.Level1ColorBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Level1ColorBtn.Location = new System.Drawing.Point(106, 103);
            this.Level1ColorBtn.Name = "Level1ColorBtn";
            this.Level1ColorBtn.Size = new System.Drawing.Size(19, 19);
            this.Level1ColorBtn.TabIndex = 32;
            this.Level1ColorBtn.UseVisualStyleBackColor = true;
            this.Level1ColorBtn.Click += new System.EventHandler(this.ApplyNewColor);
            // 
            // Level2ColorBtn
            // 
            this.Level2ColorBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Level2ColorBtn.Location = new System.Drawing.Point(106, 78);
            this.Level2ColorBtn.Name = "Level2ColorBtn";
            this.Level2ColorBtn.Size = new System.Drawing.Size(19, 19);
            this.Level2ColorBtn.TabIndex = 31;
            this.Level2ColorBtn.UseVisualStyleBackColor = true;
            this.Level2ColorBtn.Click += new System.EventHandler(this.ApplyNewColor);
            // 
            // Level99ColorBtn
            // 
            this.Level99ColorBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Level99ColorBtn.Location = new System.Drawing.Point(106, 53);
            this.Level99ColorBtn.Name = "Level99ColorBtn";
            this.Level99ColorBtn.Size = new System.Drawing.Size(19, 19);
            this.Level99ColorBtn.TabIndex = 30;
            this.Level99ColorBtn.UseVisualStyleBackColor = true;
            this.Level99ColorBtn.Click += new System.EventHandler(this.ApplyNewColor);
            // 
            // NormalTextColorBtn
            // 
            this.NormalTextColorBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NormalTextColorBtn.Location = new System.Drawing.Point(106, 28);
            this.NormalTextColorBtn.Name = "NormalTextColorBtn";
            this.NormalTextColorBtn.Size = new System.Drawing.Size(19, 19);
            this.NormalTextColorBtn.TabIndex = 29;
            this.NormalTextColorBtn.UseVisualStyleBackColor = true;
            this.NormalTextColorBtn.Click += new System.EventHandler(this.ApplyNewColor);
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 25);
            this.label8.TabIndex = 20;
            this.label8.Text = "/Me Message";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 25);
            this.label7.TabIndex = 18;
            this.label7.Text = "Server Message";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 25);
            this.label4.TabIndex = 17;
            this.label4.Text = "Normal Username";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 25);
            this.label9.TabIndex = 16;
            this.label9.Text = "Level 1";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 25);
            this.label10.TabIndex = 15;
            this.label10.Text = "Level 2";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(3, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 25);
            this.label11.TabIndex = 14;
            this.label11.Text = "Level 99";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(3, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 25);
            this.label12.TabIndex = 13;
            this.label12.Text = "Normal Text";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(97, 25);
            this.label13.TabIndex = 12;
            this.label13.Text = "Chat Background";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(3, 200);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(97, 25);
            this.label14.TabIndex = 22;
            this.label14.Text = "Join";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(3, 225);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(97, 25);
            this.label15.TabIndex = 23;
            this.label15.Text = "Leave";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(3, 250);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(97, 25);
            this.label16.TabIndex = 26;
            this.label16.Text = "System";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BackgroundColorBtn
            // 
            this.BackgroundColorBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackgroundColorBtn.Location = new System.Drawing.Point(106, 3);
            this.BackgroundColorBtn.Name = "BackgroundColorBtn";
            this.BackgroundColorBtn.Size = new System.Drawing.Size(19, 19);
            this.BackgroundColorBtn.TabIndex = 27;
            this.BackgroundColorBtn.UseVisualStyleBackColor = true;
            this.BackgroundColorBtn.Click += new System.EventHandler(this.ApplyNewColor);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel7);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 201);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 93);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Font Settings";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.06452F));
            this.tableLayoutPanel7.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.FontList, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 0, 2);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 3;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.58537F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.41463F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(155, 74);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Font";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FontList
            // 
            this.FontList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FontList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FontList.FormattingEnabled = true;
            this.FontList.Location = new System.Drawing.Point(3, 18);
            this.FontList.Name = "FontList";
            this.FontList.Size = new System.Drawing.Size(149, 21);
            this.FontList.TabIndex = 0;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.FontSize, 1, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 44);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(149, 27);
            this.tableLayoutPanel8.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 27);
            this.label2.TabIndex = 4;
            this.label2.Text = "Font Size";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FontSize
            // 
            this.FontSize.DecimalPlaces = 2;
            this.FontSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FontSize.Location = new System.Drawing.Point(77, 3);
            this.FontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FontSize.Name = "FontSize";
            this.FontSize.Size = new System.Drawing.Size(69, 20);
            this.FontSize.TabIndex = 3;
            this.FontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FontSize.Value = new decimal(new int[] {
            825,
            0,
            0,
            131072});
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.UserSearch, 0, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 495);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(181, 24);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // UserSearch
            // 
            this.UserSearch.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.UserSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserSearch.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.UserSearch.Location = new System.Drawing.Point(3, 3);
            this.UserSearch.Name = "UserSearch";
            this.UserSearch.Size = new System.Drawing.Size(175, 20);
            this.UserSearch.TabIndex = 1;
            this.UserSearch.Text = "Search";
            this.UserSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ChatFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 531);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChatFrm";
            this.Text = "NewChat_frm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.UsersControl.ResumeLayout(false);
            this.UsersTab.ResumeLayout(false);
            this.UserListTabs.ResumeLayout(false);
            this.OnlineTab.ResumeLayout(false);
            this.TeamTab.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.FriendsTab.ResumeLayout(false);
            this.IgnoreTab.ResumeLayout(false);
            this.OptionsTab.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FontSize)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage UserTab;
        private System.Windows.Forms.TabPage FriendTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private FixedTabControl UsersControl;
        private System.Windows.Forms.TabPage UsersTab;
        private System.Windows.Forms.TabPage OptionsTab;
        private System.Windows.Forms.TabPage IgnoreTab;
        private System.Windows.Forms.ListBox IgnoreList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button BackgroundColorBtn;
        private System.Windows.Forms.Button SystemColorBtn;
        private System.Windows.Forms.Button LeaveColorBtn;
        private System.Windows.Forms.Button JoinColorBtn;
        private System.Windows.Forms.Button MeColorBtn;
        private System.Windows.Forms.Button ServerColorBtn;
        private System.Windows.Forms.Button NormalUserColorBtn;
        private System.Windows.Forms.Button Level1ColorBtn;
        private System.Windows.Forms.Button Level2ColorBtn;
        private System.Windows.Forms.Button Level99ColorBtn;
        private System.Windows.Forms.Button NormalTextColorBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private System.Windows.Forms.CheckBox HideJoinLeavechk;
        private System.Windows.Forms.CheckBox Colorblindchk;
        private System.Windows.Forms.CheckBox Timestampchk;
        private System.Windows.Forms.CheckBox DuelRequestchk;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TextBox UserSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox FontList;
        private System.Windows.Forms.NumericUpDown FontSize;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label2;
        private FixedTabControl ChannelTabs;
        private System.Windows.Forms.CheckBox pmwindowchk;
        private System.Windows.Forms.CheckBox usernamecolorchk;
        private System.Windows.Forms.CheckBox refuseteamchk;
        private FixedTabControl UserListTabs;
        private System.Windows.Forms.TabPage OnlineTab;
        private System.Windows.Forms.TabPage TeamTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.ListBox TeamList;
        private System.Windows.Forms.TabPage FriendsTab;
        private System.Windows.Forms.ListBox FriendList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button AddUserBtn;
        private System.Windows.Forms.Button TeamStatsbtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Label TeamNameLabel;
        private System.Windows.Forms.ListBox UserList;
        private System.Windows.Forms.TextBox ChatInput;
        private System.Windows.Forms.Button ChannelListBtn;
        private System.Windows.Forms.Button LeaveBtn;
    }
}