namespace YGOPro_Launcher.Chat
{
    partial class Chat_frm
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ChatInput = new System.Windows.Forms.TextBox();
            this.UserTabs = new System.Windows.Forms.TabControl();
            this.UserTab = new System.Windows.Forms.TabPage();
            this.UserList = new System.Windows.Forms.ListBox();
            this.FriendTab = new System.Windows.Forms.TabPage();
            this.FriendList = new System.Windows.Forms.ListBox();
            this.ChatTabs = new YGOPro_Launcher.Chat.FixedTabControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.UserTabs.SuspendLayout();
            this.UserTab.SuspendLayout();
            this.FriendTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.UserTabs, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(715, 379);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.76224F));
            this.tableLayoutPanel2.Controls.Add(this.ChatInput, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ChatTabs, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.87599F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.124011F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(569, 373);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // ChatInput
            // 
            this.ChatInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatInput.Location = new System.Drawing.Point(3, 349);
            this.ChatInput.Name = "ChatInput";
            this.ChatInput.Size = new System.Drawing.Size(563, 20);
            this.ChatInput.TabIndex = 0;
            // 
            // UserTabs
            // 
            this.UserTabs.Controls.Add(this.UserTab);
            this.UserTabs.Controls.Add(this.FriendTab);
            this.UserTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserTabs.Location = new System.Drawing.Point(578, 3);
            this.UserTabs.Name = "UserTabs";
            this.UserTabs.SelectedIndex = 0;
            this.UserTabs.Size = new System.Drawing.Size(134, 373);
            this.UserTabs.TabIndex = 2;
            // 
            // UserTab
            // 
            this.UserTab.Controls.Add(this.UserList);
            this.UserTab.Location = new System.Drawing.Point(4, 22);
            this.UserTab.Name = "UserTab";
            this.UserTab.Padding = new System.Windows.Forms.Padding(3);
            this.UserTab.Size = new System.Drawing.Size(126, 347);
            this.UserTab.TabIndex = 0;
            this.UserTab.Text = "Users";
            this.UserTab.UseVisualStyleBackColor = true;
            // 
            // UserList
            // 
            this.UserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserList.FormattingEnabled = true;
            this.UserList.IntegralHeight = false;
            this.UserList.Location = new System.Drawing.Point(3, 3);
            this.UserList.Name = "UserList";
            this.UserList.Size = new System.Drawing.Size(120, 341);
            this.UserList.TabIndex = 0;
            // 
            // FriendTab
            // 
            this.FriendTab.Controls.Add(this.FriendList);
            this.FriendTab.Location = new System.Drawing.Point(4, 22);
            this.FriendTab.Name = "FriendTab";
            this.FriendTab.Padding = new System.Windows.Forms.Padding(3);
            this.FriendTab.Size = new System.Drawing.Size(126, 347);
            this.FriendTab.TabIndex = 1;
            this.FriendTab.Text = "Friends";
            this.FriendTab.UseVisualStyleBackColor = true;
            // 
            // FriendList
            // 
            this.FriendList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FriendList.FormattingEnabled = true;
            this.FriendList.IntegralHeight = false;
            this.FriendList.Location = new System.Drawing.Point(3, 3);
            this.FriendList.Name = "FriendList";
            this.FriendList.Size = new System.Drawing.Size(120, 341);
            this.FriendList.TabIndex = 0;
            // 
            // ChatTabs
            // 
            this.ChatTabs.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.ChatTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatTabs.Location = new System.Drawing.Point(3, 3);
            this.ChatTabs.Name = "ChatTabs";
            this.ChatTabs.SelectedIndex = 0;
            this.ChatTabs.Size = new System.Drawing.Size(563, 340);
            this.ChatTabs.TabIndex = 2;
            // 
            // Chat_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 379);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Chat_frm";
            this.Text = "Chat_frm";
            this.Load += new System.EventHandler(this.Chat_frm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.UserTabs.ResumeLayout(false);
            this.UserTab.ResumeLayout(false);
            this.FriendTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox ChatInput;
        private FixedTabControl ChatTabs;
        private System.Windows.Forms.TabControl UserTabs;
        private System.Windows.Forms.TabPage UserTab;
        private System.Windows.Forms.ListBox UserList;
        private System.Windows.Forms.TabPage FriendTab;
        private System.Windows.Forms.ListBox FriendList;


    }
}