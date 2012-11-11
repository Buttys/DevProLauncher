namespace YGOPro_Launcher
{
    partial class Admin_frm
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
            this.InputBox = new System.Windows.Forms.TextBox();
            this.ServerLog = new System.Windows.Forms.RichTextBox();
            this.UserList = new System.Windows.Forms.ListBox();
            this.UserCount = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.InputBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ServerLog, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.UserList, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.UserCount, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(716, 395);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // InputBox
            // 
            this.InputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputBox.Location = new System.Drawing.Point(3, 373);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(560, 20);
            this.InputBox.TabIndex = 0;
            // 
            // ServerLog
            // 
            this.ServerLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerLog.Location = new System.Drawing.Point(3, 3);
            this.ServerLog.Name = "ServerLog";
            this.ServerLog.Size = new System.Drawing.Size(560, 364);
            this.ServerLog.TabIndex = 1;
            this.ServerLog.Text = "Commands: kill [room], ban [user], unban [user], kick [user], msg [message], user" +
    "s, restart, shutdown";
            // 
            // UserList
            // 
            this.UserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserList.FormattingEnabled = true;
            this.UserList.IntegralHeight = false;
            this.UserList.Location = new System.Drawing.Point(569, 3);
            this.UserList.Name = "UserList";
            this.UserList.Size = new System.Drawing.Size(144, 364);
            this.UserList.TabIndex = 2;
            // 
            // UserCount
            // 
            this.UserCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UserCount.AutoSize = true;
            this.UserCount.Location = new System.Drawing.Point(641, 376);
            this.UserCount.Name = "UserCount";
            this.UserCount.Size = new System.Drawing.Size(0, 13);
            this.UserCount.TabIndex = 3;
            // 
            // Admin_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 395);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Admin_frm";
            this.Text = "Admin_frm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.RichTextBox ServerLog;
        private System.Windows.Forms.ListBox UserList;
        private System.Windows.Forms.Label UserCount;
    }
}