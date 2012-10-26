namespace YGOPro_Launcher
{
    partial class Settings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DefualtDeck = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.Enabled3d = new System.Windows.Forms.CheckBox();
            this.Fullscreen = new System.Windows.Forms.CheckBox();
            this.EnableMusic = new System.Windows.Forms.CheckBox();
            this.EnableSound = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Antialias = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.QuickSettingsBtn = new System.Windows.Forms.Button();
            this.QuickHostSettingsbtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Antialias)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.75125F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.29268F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.70732F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 220);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Settings";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.Username, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.DefualtDeck, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.06383F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.93617F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(272, 55);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // Username
            // 
            this.Username.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Username.Location = new System.Drawing.Point(139, 4);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(130, 20);
            this.Username.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Defualt Username";
            // 
            // DefualtDeck
            // 
            this.DefualtDeck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DefualtDeck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DefualtDeck.FormattingEnabled = true;
            this.DefualtDeck.Location = new System.Drawing.Point(139, 31);
            this.DefualtDeck.Name = "DefualtDeck";
            this.DefualtDeck.Size = new System.Drawing.Size(130, 21);
            this.DefualtDeck.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Default Deck";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 98);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Game Settings";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.Enabled3d, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.Fullscreen, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.EnableMusic, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.EnableSound, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.Antialias, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(272, 79);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // Enabled3d
            // 
            this.Enabled3d.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Enabled3d.AutoSize = true;
            this.Enabled3d.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Enabled3d.Location = new System.Drawing.Point(38, 56);
            this.Enabled3d.Name = "Enabled3d";
            this.Enabled3d.Size = new System.Drawing.Size(95, 17);
            this.Enabled3d.TabIndex = 2;
            this.Enabled3d.Text = "Enable Directx";
            this.Enabled3d.UseVisualStyleBackColor = true;
            // 
            // Fullscreen
            // 
            this.Fullscreen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Fullscreen.AutoSize = true;
            this.Fullscreen.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Fullscreen.Location = new System.Drawing.Point(139, 56);
            this.Fullscreen.Name = "Fullscreen";
            this.Fullscreen.Size = new System.Drawing.Size(92, 17);
            this.Fullscreen.TabIndex = 3;
            this.Fullscreen.Text = "Fullscreen      ";
            this.Fullscreen.UseVisualStyleBackColor = true;
            // 
            // EnableMusic
            // 
            this.EnableMusic.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.EnableMusic.AutoSize = true;
            this.EnableMusic.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.EnableMusic.Location = new System.Drawing.Point(139, 29);
            this.EnableMusic.Name = "EnableMusic";
            this.EnableMusic.Size = new System.Drawing.Size(90, 17);
            this.EnableMusic.TabIndex = 1;
            this.EnableMusic.Text = "Enable Music";
            this.EnableMusic.UseVisualStyleBackColor = true;
            // 
            // EnableSound
            // 
            this.EnableSound.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.EnableSound.AutoSize = true;
            this.EnableSound.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.EnableSound.Location = new System.Drawing.Point(40, 29);
            this.EnableSound.Name = "EnableSound";
            this.EnableSound.Size = new System.Drawing.Size(93, 17);
            this.EnableSound.TabIndex = 0;
            this.EnableSound.Text = "Enable Sound";
            this.EnableSound.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Antialias";
            // 
            // Antialias
            // 
            this.Antialias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Antialias.Location = new System.Drawing.Point(139, 3);
            this.Antialias.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.Antialias.Name = "Antialias";
            this.Antialias.ReadOnly = true;
            this.Antialias.Size = new System.Drawing.Size(130, 20);
            this.Antialias.TabIndex = 5;
            this.Antialias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.CancelBtn);
            this.flowLayoutPanel1.Controls.Add(this.SaveBtn);
            this.flowLayoutPanel1.Controls.Add(this.QuickSettingsBtn);
            this.flowLayoutPanel1.Controls.Add(this.QuickHostSettingsbtn);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 187);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(278, 28);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(200, 3);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(119, 3);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 0;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // QuickSettingsBtn
            // 
            this.QuickSettingsBtn.Location = new System.Drawing.Point(3, 3);
            this.QuickSettingsBtn.Name = "QuickSettingsBtn";
            this.QuickSettingsBtn.Size = new System.Drawing.Size(110, 23);
            this.QuickSettingsBtn.TabIndex = 5;
            this.QuickSettingsBtn.Text = "Quick Host Settings";
            this.QuickSettingsBtn.UseVisualStyleBackColor = true;
            this.QuickSettingsBtn.Click += new System.EventHandler(this.QuickSettingsBtn_Click);
            // 
            // QuickHostSettingsbtn
            // 
            this.QuickHostSettingsbtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.QuickHostSettingsbtn.Location = new System.Drawing.Point(150, 32);
            this.QuickHostSettingsbtn.Name = "QuickHostSettingsbtn";
            this.QuickHostSettingsbtn.Size = new System.Drawing.Size(125, 23);
            this.QuickHostSettingsbtn.TabIndex = 4;
            this.QuickHostSettingsbtn.Text = "Quick Host Settings";
            this.QuickHostSettingsbtn.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 220);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Antialias)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DefualtDeck;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox Enabled3d;
        private System.Windows.Forms.CheckBox Fullscreen;
        private System.Windows.Forms.CheckBox EnableMusic;
        private System.Windows.Forms.CheckBox EnableSound;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button QuickHostSettingsbtn;
        private System.Windows.Forms.Button QuickSettingsBtn;
        private System.Windows.Forms.NumericUpDown Antialias;
    }
}