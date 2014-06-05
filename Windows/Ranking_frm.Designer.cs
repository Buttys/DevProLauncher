namespace DevProLauncher.Windows
{
    partial class RankingFrm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SingleRankingGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.SingleRankingListBox = new DevProLauncher.Windows.Components.DoubleBufferedListBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.SingleRankLbl = new System.Windows.Forms.Label();
            this.SingleUsernameLbl = new System.Windows.Forms.Label();
            this.SingleEloLbl = new System.Windows.Forms.Label();
            this.SingleWinLbl = new System.Windows.Forms.Label();
            this.SingleLossLbl = new System.Windows.Forms.Label();
            this.SingleDrawLbl = new System.Windows.Forms.Label();
            this.SingleTeamLbl = new System.Windows.Forms.Label();
            this.MatchRankingGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.MatchUsernameLbl = new System.Windows.Forms.Label();
            this.MatchEloLbl = new System.Windows.Forms.Label();
            this.MatchWinLbl = new System.Windows.Forms.Label();
            this.MatchLossLbl = new System.Windows.Forms.Label();
            this.MatchDrawLbl = new System.Windows.Forms.Label();
            this.MatchTeamLbl = new System.Windows.Forms.Label();
            this.MatchRankLbl = new System.Windows.Forms.Label();
            this.MatchRankingListBox = new DevProLauncher.Windows.Components.DoubleBufferedListBox();
            this.loadBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SingleRankingGroupBox.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.MatchRankingGroupBox.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SingleRankingGroupBox
            // 
            this.SingleRankingGroupBox.Controls.Add(this.tableLayoutPanel2);
            this.SingleRankingGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SingleRankingGroupBox.Location = new System.Drawing.Point(3, 3);
            this.SingleRankingGroupBox.Name = "SingleRankingGroupBox";
            this.SingleRankingGroupBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SingleRankingGroupBox.Size = new System.Drawing.Size(535, 421);
            this.SingleRankingGroupBox.TabIndex = 0;
            this.SingleRankingGroupBox.TabStop = false;
            this.SingleRankingGroupBox.Text = "Single Rankings";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.SingleRankingListBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.772009F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.22799F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(529, 402);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // SingleRankingListBox
            // 
            this.SingleRankingListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SingleRankingListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.SingleRankingListBox.FormattingEnabled = true;
            this.SingleRankingListBox.ItemHeight = 25;
            this.SingleRankingListBox.Location = new System.Drawing.Point(3, 30);
            this.SingleRankingListBox.Name = "SingleRankingListBox";
            this.SingleRankingListBox.Size = new System.Drawing.Size(523, 369);
            this.SingleRankingListBox.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 7;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.SingleRankLbl, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.SingleUsernameLbl, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.SingleEloLbl, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.SingleWinLbl, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.SingleLossLbl, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.SingleDrawLbl, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.SingleTeamLbl, 6, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(523, 21);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // SingleRankLbl
            // 
            this.SingleRankLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SingleRankLbl.AutoSize = true;
            this.SingleRankLbl.Location = new System.Drawing.Point(3, 0);
            this.SingleRankLbl.Name = "SingleRankLbl";
            this.SingleRankLbl.Size = new System.Drawing.Size(33, 21);
            this.SingleRankLbl.TabIndex = 0;
            this.SingleRankLbl.Text = "Rank";
            // 
            // SingleUsernameLbl
            // 
            this.SingleUsernameLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SingleUsernameLbl.AutoSize = true;
            this.SingleUsernameLbl.Location = new System.Drawing.Point(55, 0);
            this.SingleUsernameLbl.Name = "SingleUsernameLbl";
            this.SingleUsernameLbl.Size = new System.Drawing.Size(55, 21);
            this.SingleUsernameLbl.TabIndex = 1;
            this.SingleUsernameLbl.Text = "Username";
            // 
            // SingleEloLbl
            // 
            this.SingleEloLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SingleEloLbl.AutoSize = true;
            this.SingleEloLbl.Location = new System.Drawing.Point(159, 0);
            this.SingleEloLbl.Name = "SingleEloLbl";
            this.SingleEloLbl.Size = new System.Drawing.Size(22, 21);
            this.SingleEloLbl.TabIndex = 2;
            this.SingleEloLbl.Text = "Elo";
            // 
            // SingleWinLbl
            // 
            this.SingleWinLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SingleWinLbl.AutoSize = true;
            this.SingleWinLbl.Location = new System.Drawing.Point(237, 0);
            this.SingleWinLbl.Name = "SingleWinLbl";
            this.SingleWinLbl.Size = new System.Drawing.Size(31, 21);
            this.SingleWinLbl.TabIndex = 3;
            this.SingleWinLbl.Text = "Wins";
            // 
            // SingleLossLbl
            // 
            this.SingleLossLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SingleLossLbl.AutoSize = true;
            this.SingleLossLbl.Location = new System.Drawing.Point(289, 0);
            this.SingleLossLbl.Name = "SingleLossLbl";
            this.SingleLossLbl.Size = new System.Drawing.Size(40, 21);
            this.SingleLossLbl.TabIndex = 4;
            this.SingleLossLbl.Text = "Losses";
            // 
            // SingleDrawLbl
            // 
            this.SingleDrawLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SingleDrawLbl.AutoSize = true;
            this.SingleDrawLbl.Location = new System.Drawing.Point(341, 0);
            this.SingleDrawLbl.Name = "SingleDrawLbl";
            this.SingleDrawLbl.Size = new System.Drawing.Size(37, 21);
            this.SingleDrawLbl.TabIndex = 5;
            this.SingleDrawLbl.Text = "Draws";
            // 
            // SingleTeamLbl
            // 
            this.SingleTeamLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SingleTeamLbl.Location = new System.Drawing.Point(393, 0);
            this.SingleTeamLbl.Name = "SingleTeamLbl";
            this.SingleTeamLbl.Size = new System.Drawing.Size(34, 21);
            this.SingleTeamLbl.TabIndex = 6;
            this.SingleTeamLbl.Text = "Team";
            // 
            // MatchRankingGroupBox
            // 
            this.MatchRankingGroupBox.Controls.Add(this.tableLayoutPanel4);
            this.MatchRankingGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MatchRankingGroupBox.Location = new System.Drawing.Point(544, 3);
            this.MatchRankingGroupBox.Name = "MatchRankingGroupBox";
            this.MatchRankingGroupBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MatchRankingGroupBox.Size = new System.Drawing.Size(536, 421);
            this.MatchRankingGroupBox.TabIndex = 1;
            this.MatchRankingGroupBox.TabStop = false;
            this.MatchRankingGroupBox.Text = "Match Rankings";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.MatchRankingListBox, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.77F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.23F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(530, 402);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 7;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.MatchUsernameLbl, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.MatchEloLbl, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.MatchWinLbl, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.MatchLossLbl, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.MatchDrawLbl, 5, 0);
            this.tableLayoutPanel5.Controls.Add(this.MatchTeamLbl, 6, 0);
            this.tableLayoutPanel5.Controls.Add(this.MatchRankLbl, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(524, 21);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // MatchUsernameLbl
            // 
            this.MatchUsernameLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MatchUsernameLbl.Location = new System.Drawing.Point(55, 0);
            this.MatchUsernameLbl.Name = "MatchUsernameLbl";
            this.MatchUsernameLbl.Size = new System.Drawing.Size(55, 21);
            this.MatchUsernameLbl.TabIndex = 1;
            this.MatchUsernameLbl.Text = "Username";
            // 
            // MatchEloLbl
            // 
            this.MatchEloLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MatchEloLbl.Location = new System.Drawing.Point(159, 0);
            this.MatchEloLbl.Name = "MatchEloLbl";
            this.MatchEloLbl.Size = new System.Drawing.Size(22, 21);
            this.MatchEloLbl.TabIndex = 2;
            this.MatchEloLbl.Text = "Elo";
            // 
            // MatchWinLbl
            // 
            this.MatchWinLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MatchWinLbl.Location = new System.Drawing.Point(237, 0);
            this.MatchWinLbl.Name = "MatchWinLbl";
            this.MatchWinLbl.Size = new System.Drawing.Size(31, 21);
            this.MatchWinLbl.TabIndex = 3;
            this.MatchWinLbl.Text = "Wins";
            // 
            // MatchLossLbl
            // 
            this.MatchLossLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MatchLossLbl.Location = new System.Drawing.Point(289, 0);
            this.MatchLossLbl.Name = "MatchLossLbl";
            this.MatchLossLbl.Size = new System.Drawing.Size(40, 21);
            this.MatchLossLbl.TabIndex = 4;
            this.MatchLossLbl.Text = "Losses";
            // 
            // MatchDrawLbl
            // 
            this.MatchDrawLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MatchDrawLbl.Location = new System.Drawing.Point(341, 0);
            this.MatchDrawLbl.Name = "MatchDrawLbl";
            this.MatchDrawLbl.Size = new System.Drawing.Size(37, 21);
            this.MatchDrawLbl.TabIndex = 5;
            this.MatchDrawLbl.Text = "Draws";
            // 
            // MatchTeamLbl
            // 
            this.MatchTeamLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MatchTeamLbl.Location = new System.Drawing.Point(393, 0);
            this.MatchTeamLbl.Name = "MatchTeamLbl";
            this.MatchTeamLbl.Size = new System.Drawing.Size(34, 21);
            this.MatchTeamLbl.TabIndex = 6;
            this.MatchTeamLbl.Text = "Team";
            // 
            // MatchRankLbl
            // 
            this.MatchRankLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MatchRankLbl.Location = new System.Drawing.Point(3, 0);
            this.MatchRankLbl.Name = "MatchRankLbl";
            this.MatchRankLbl.Size = new System.Drawing.Size(33, 21);
            this.MatchRankLbl.TabIndex = 0;
            this.MatchRankLbl.Text = "Rank";
            // 
            // MatchRankingListBox
            // 
            this.MatchRankingListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MatchRankingListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.MatchRankingListBox.FormattingEnabled = true;
            this.MatchRankingListBox.ItemHeight = 25;
            this.MatchRankingListBox.Location = new System.Drawing.Point(3, 30);
            this.MatchRankingListBox.Name = "MatchRankingListBox";
            this.MatchRankingListBox.Size = new System.Drawing.Size(524, 369);
            this.MatchRankingListBox.TabIndex = 0;
            // 
            // loadBtn
            // 
            this.loadBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.loadBtn.AutoSize = true;
            this.loadBtn.Location = new System.Drawing.Point(991, 430);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(89, 35);
            this.loadBtn.TabIndex = 2;
            this.loadBtn.Text = "Load Rankings";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47F));
            this.tableLayoutPanel1.Controls.Add(this.MatchRankingGroupBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.SingleRankingGroupBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.loadBtn, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1083, 468);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // RankingFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RankingFrm";
            this.Size = new System.Drawing.Size(1083, 468);
            this.SingleRankingGroupBox.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.MatchRankingGroupBox.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SingleRankingGroupBox;
        private Components.DoubleBufferedListBox SingleRankingListBox;
        private System.Windows.Forms.GroupBox MatchRankingGroupBox;
        private Components.DoubleBufferedListBox MatchRankingListBox;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label SingleRankLbl;
        private System.Windows.Forms.Label SingleUsernameLbl;
        private System.Windows.Forms.Label SingleEloLbl;
        private System.Windows.Forms.Label SingleWinLbl;
        private System.Windows.Forms.Label SingleDrawLbl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label SingleLossLbl;
        private System.Windows.Forms.Label SingleTeamLbl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label MatchRankLbl;
        private System.Windows.Forms.Label MatchUsernameLbl;
        private System.Windows.Forms.Label MatchEloLbl;
        private System.Windows.Forms.Label MatchWinLbl;
        private System.Windows.Forms.Label MatchLossLbl;
        private System.Windows.Forms.Label MatchDrawLbl;
        private System.Windows.Forms.Label MatchTeamLbl;
    }
}
