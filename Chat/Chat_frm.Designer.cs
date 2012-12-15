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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.ChatInput = new System.Windows.Forms.TextBox();
            this.OptionsBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.UserTabs = new System.Windows.Forms.TabControl();
            this.UserTab = new System.Windows.Forms.TabPage();
            this.UserList = new System.Windows.Forms.ListBox();
            this.FriendTab = new System.Windows.Forms.TabPage();
            this.FriendList = new System.Windows.Forms.ListBox();
            this.IgnoreTab = new System.Windows.Forms.TabPage();
            this.IgnoreList = new System.Windows.Forms.ListBox();
            this.DonateIMG = new System.Windows.Forms.PictureBox();
            this.ChatTabs = new YGOPro_Launcher.Chat.FixedTabControl();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.UserTabs.SuspendLayout();
            this.UserTab.SuspendLayout();
            this.FriendTab.SuspendLayout();
            this.IgnoreTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DonateIMG)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.ChatTabs, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(569, 373);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.Controls.Add(this.ChatInput, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.OptionsBtn, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 343);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(563, 27);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // ChatInput
            // 
            this.ChatInput.AcceptsTab = true;
            this.ChatInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatInput.Location = new System.Drawing.Point(3, 3);
            this.ChatInput.Name = "ChatInput";
            this.ChatInput.Size = new System.Drawing.Size(477, 20);
            this.ChatInput.TabIndex = 1;
            this.ChatInput.TabStop = false;
            // 
            // OptionsBtn
            // 
            this.OptionsBtn.Location = new System.Drawing.Point(486, 3);
            this.OptionsBtn.Name = "OptionsBtn";
            this.OptionsBtn.Size = new System.Drawing.Size(74, 21);
            this.OptionsBtn.TabIndex = 2;
            this.OptionsBtn.TabStop = false;
            this.OptionsBtn.Text = "Options";
            this.OptionsBtn.UseVisualStyleBackColor = true;
            this.OptionsBtn.Click += new System.EventHandler(this.OptionsBtn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(715, 379);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.UserTabs, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.DonateIMG, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(578, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(134, 373);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // UserTabs
            // 
            this.UserTabs.Controls.Add(this.UserTab);
            this.UserTabs.Controls.Add(this.FriendTab);
            this.UserTabs.Controls.Add(this.IgnoreTab);
            this.UserTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserTabs.Location = new System.Drawing.Point(3, 3);
            this.UserTabs.Name = "UserTabs";
            this.UserTabs.SelectedIndex = 0;
            this.UserTabs.Size = new System.Drawing.Size(128, 329);
            this.UserTabs.TabIndex = 3;
            this.UserTabs.TabStop = false;
            // 
            // UserTab
            // 
            this.UserTab.Controls.Add(this.UserList);
            this.UserTab.Location = new System.Drawing.Point(4, 22);
            this.UserTab.Name = "UserTab";
            this.UserTab.Padding = new System.Windows.Forms.Padding(3);
            this.UserTab.Size = new System.Drawing.Size(120, 303);
            this.UserTab.TabIndex = 0;
            this.UserTab.Text = "Users";
            this.UserTab.UseVisualStyleBackColor = true;
            // 
            // UserList
            // 
            this.UserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.UserList.FormattingEnabled = true;
            this.UserList.IntegralHeight = false;
            this.UserList.Location = new System.Drawing.Point(3, 3);
            this.UserList.Name = "UserList";
            this.UserList.Size = new System.Drawing.Size(114, 297);
            this.UserList.TabIndex = 0;
            this.UserList.TabStop = false;
            // 
            // FriendTab
            // 
            this.FriendTab.Controls.Add(this.FriendList);
            this.FriendTab.Location = new System.Drawing.Point(4, 22);
            this.FriendTab.Name = "FriendTab";
            this.FriendTab.Padding = new System.Windows.Forms.Padding(3);
            this.FriendTab.Size = new System.Drawing.Size(120, 303);
            this.FriendTab.TabIndex = 1;
            this.FriendTab.Text = "Friends";
            this.FriendTab.UseVisualStyleBackColor = true;
            // 
            // FriendList
            // 
            this.FriendList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FriendList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.FriendList.FormattingEnabled = true;
            this.FriendList.IntegralHeight = false;
            this.FriendList.Location = new System.Drawing.Point(3, 3);
            this.FriendList.Name = "FriendList";
            this.FriendList.Size = new System.Drawing.Size(114, 297);
            this.FriendList.TabIndex = 0;
            // 
            // IgnoreTab
            // 
            this.IgnoreTab.Controls.Add(this.IgnoreList);
            this.IgnoreTab.Location = new System.Drawing.Point(4, 22);
            this.IgnoreTab.Name = "IgnoreTab";
            this.IgnoreTab.Size = new System.Drawing.Size(120, 303);
            this.IgnoreTab.TabIndex = 2;
            this.IgnoreTab.Text = "Ignore";
            this.IgnoreTab.UseVisualStyleBackColor = true;
            // 
            // IgnoreList
            // 
            this.IgnoreList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IgnoreList.FormattingEnabled = true;
            this.IgnoreList.IntegralHeight = false;
            this.IgnoreList.Location = new System.Drawing.Point(0, 0);
            this.IgnoreList.Name = "IgnoreList";
            this.IgnoreList.Size = new System.Drawing.Size(120, 303);
            this.IgnoreList.TabIndex = 1;
            // 
            // DonateIMG
            // 
            this.DonateIMG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DonateIMG.Image = global::YGOPro_Launcher.Properties.Resources.btn_donate_LG;
            this.DonateIMG.Location = new System.Drawing.Point(3, 338);
            this.DonateIMG.Name = "DonateIMG";
            this.DonateIMG.Size = new System.Drawing.Size(128, 32);
            this.DonateIMG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.DonateIMG.TabIndex = 4;
            this.DonateIMG.TabStop = false;
            // 
            // ChatTabs
            // 
            this.ChatTabs.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.ChatTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatTabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.ChatTabs.Location = new System.Drawing.Point(3, 3);
            this.ChatTabs.Name = "ChatTabs";
            this.ChatTabs.SelectedIndex = 0;
            this.ChatTabs.Size = new System.Drawing.Size(563, 334);
            this.ChatTabs.TabIndex = 2;
            this.ChatTabs.TabStop = false;
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
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.UserTabs.ResumeLayout(false);
            this.UserTab.ResumeLayout(false);
            this.FriendTab.ResumeLayout(false);
            this.IgnoreTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DonateIMG)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private FixedTabControl ChatTabs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox ChatInput;
        private System.Windows.Forms.Button OptionsBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TabControl UserTabs;
        private System.Windows.Forms.TabPage UserTab;
        private System.Windows.Forms.ListBox UserList;
        private System.Windows.Forms.TabPage FriendTab;
        private System.Windows.Forms.ListBox FriendList;
        private System.Windows.Forms.TabPage IgnoreTab;
        private System.Windows.Forms.ListBox IgnoreList;
        private System.Windows.Forms.PictureBox DonateIMG;




    }
}