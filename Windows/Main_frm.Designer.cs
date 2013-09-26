namespace DevProLauncher.Windows
{
    partial class MainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.ConnectionCheck = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mainTabs = new System.Windows.Forms.TabControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.OptionsBtn = new System.Windows.Forms.Button();
            this.OfflineBtn = new System.Windows.Forms.Button();
            this.DeckBtn = new System.Windows.Forms.Button();
            this.ReplaysBtn = new System.Windows.Forms.Button();
            this.ProfileBtn = new System.Windows.Forms.Button();
            this.siteBtn = new System.Windows.Forms.Button();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.TeamProfileBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectionCheck
            // 
            this.ConnectionCheck.Interval = 5000;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.mainTabs, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(918, 469);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // mainTabs
            // 
            this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabs.Location = new System.Drawing.Point(3, 3);
            this.mainTabs.Name = "mainTabs";
            this.mainTabs.SelectedIndex = 0;
            this.mainTabs.Size = new System.Drawing.Size(912, 423);
            this.mainTabs.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 528F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.MessageLabel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 432);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(912, 34);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.OptionsBtn);
            this.flowLayoutPanel1.Controls.Add(this.OfflineBtn);
            this.flowLayoutPanel1.Controls.Add(this.DeckBtn);
            this.flowLayoutPanel1.Controls.Add(this.ReplaysBtn);
            this.flowLayoutPanel1.Controls.Add(this.TeamProfileBtn);
            this.flowLayoutPanel1.Controls.Add(this.ProfileBtn);
            this.flowLayoutPanel1.Controls.Add(this.siteBtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(387, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(522, 28);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // OptionsBtn
            // 
            this.OptionsBtn.Location = new System.Drawing.Point(453, 3);
            this.OptionsBtn.Name = "OptionsBtn";
            this.OptionsBtn.Size = new System.Drawing.Size(66, 23);
            this.OptionsBtn.TabIndex = 9;
            this.OptionsBtn.Text = "Options";
            this.OptionsBtn.UseVisualStyleBackColor = true;
            this.OptionsBtn.Click += new System.EventHandler(this.OptionsBtn_Click);
            // 
            // OfflineBtn
            // 
            this.OfflineBtn.Location = new System.Drawing.Point(381, 3);
            this.OfflineBtn.Name = "OfflineBtn";
            this.OfflineBtn.Size = new System.Drawing.Size(66, 23);
            this.OfflineBtn.TabIndex = 10;
            this.OfflineBtn.Text = "Offline";
            this.OfflineBtn.UseVisualStyleBackColor = true;
            this.OfflineBtn.Click += new System.EventHandler(this.OfflineBtn_Click);
            // 
            // DeckBtn
            // 
            this.DeckBtn.Location = new System.Drawing.Point(309, 3);
            this.DeckBtn.Name = "DeckBtn";
            this.DeckBtn.Size = new System.Drawing.Size(66, 23);
            this.DeckBtn.TabIndex = 2;
            this.DeckBtn.Text = "Deck Edit";
            this.DeckBtn.UseVisualStyleBackColor = true;
            this.DeckBtn.Click += new System.EventHandler(this.DeckBtn_Click);
            // 
            // ReplaysBtn
            // 
            this.ReplaysBtn.Location = new System.Drawing.Point(237, 3);
            this.ReplaysBtn.Name = "ReplaysBtn";
            this.ReplaysBtn.Size = new System.Drawing.Size(66, 23);
            this.ReplaysBtn.TabIndex = 3;
            this.ReplaysBtn.Text = "Replays";
            this.ReplaysBtn.UseVisualStyleBackColor = true;
            this.ReplaysBtn.Click += new System.EventHandler(this.ReplaysBtn_Click);
            // 
            // ProfileBtn
            // 
            this.ProfileBtn.Enabled = false;
            this.ProfileBtn.Location = new System.Drawing.Point(93, 3);
            this.ProfileBtn.Name = "ProfileBtn";
            this.ProfileBtn.Size = new System.Drawing.Size(66, 23);
            this.ProfileBtn.TabIndex = 8;
            this.ProfileBtn.Text = "Profile";
            this.ProfileBtn.UseVisualStyleBackColor = true;
            this.ProfileBtn.Click += new System.EventHandler(this.ProfileBtn_Click);
            // 
            // siteBtn
            // 
            this.siteBtn.Location = new System.Drawing.Point(12, 3);
            this.siteBtn.Name = "siteBtn";
            this.siteBtn.Size = new System.Drawing.Size(75, 23);
            this.siteBtn.TabIndex = 11;
            this.siteBtn.Text = "Site";
            this.siteBtn.UseVisualStyleBackColor = true;
            this.siteBtn.Click += new System.EventHandler(this.siteBtn_Click);
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageLabel.Location = new System.Drawing.Point(3, 0);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(378, 34);
            this.MessageLabel.TabIndex = 0;
            this.MessageLabel.Text = "Server messages will spawn here.";
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TeamProfileBtn
            // 
            this.TeamProfileBtn.Enabled = false;
            this.TeamProfileBtn.Location = new System.Drawing.Point(165, 3);
            this.TeamProfileBtn.Name = "TeamProfileBtn";
            this.TeamProfileBtn.Size = new System.Drawing.Size(66, 23);
            this.TeamProfileBtn.TabIndex = 13;
            this.TeamProfileBtn.Text = "Team";
            this.TeamProfileBtn.UseVisualStyleBackColor = true;
            this.TeamProfileBtn.Click += new System.EventHandler(this.TeamProfileBtn_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 469);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFrm";
            this.Text = "DevPro Launcher";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer ConnectionCheck;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl mainTabs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button OptionsBtn;
        private System.Windows.Forms.Button OfflineBtn;
        private System.Windows.Forms.Button DeckBtn;
        private System.Windows.Forms.Button ReplaysBtn;
        private System.Windows.Forms.Button ProfileBtn;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Button siteBtn;
        private System.Windows.Forms.Button TeamProfileBtn;
    }
}

