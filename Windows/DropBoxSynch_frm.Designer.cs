namespace DevProLauncher.Windows
{
    partial class DropBoxSynch_frm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.descriptionLb = new System.Windows.Forms.Label();
            this.submitBtn = new System.Windows.Forms.Button();
            this.optionsGB = new System.Windows.Forms.GroupBox();
            this.deckCB = new System.Windows.Forms.CheckBox();
            this.allCB = new System.Windows.Forms.CheckBox();
            this.replayCB = new System.Windows.Forms.CheckBox();
            this.texturesCB = new System.Windows.Forms.CheckBox();
            this.skinsCB = new System.Windows.Forms.CheckBox();
            this.soundsCB = new System.Windows.Forms.CheckBox();
            this.optionsGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // descriptionLb
            // 
            this.descriptionLb.AutoSize = true;
            this.descriptionLb.Location = new System.Drawing.Point(12, 9);
            this.descriptionLb.Name = "descriptionLb";
            this.descriptionLb.Size = new System.Drawing.Size(226, 26);
            this.descriptionLb.TabIndex = 3;
            this.descriptionLb.Text = "synchronize your decks and replays with every\r\nclient connected with your  DropBo" +
                "x account";
            // 
            // submitBtn
            // 
            this.submitBtn.Location = new System.Drawing.Point(13, 42);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(226, 23);
            this.submitBtn.TabIndex = 7;
            this.submitBtn.Text = "Sync";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // optionsGB
            // 
            this.optionsGB.Controls.Add(this.deckCB);
            this.optionsGB.Controls.Add(this.allCB);
            this.optionsGB.Controls.Add(this.replayCB);
            this.optionsGB.Controls.Add(this.texturesCB);
            this.optionsGB.Controls.Add(this.skinsCB);
            this.optionsGB.Controls.Add(this.soundsCB);
            this.optionsGB.Location = new System.Drawing.Point(15, 72);
            this.optionsGB.Name = "optionsGB";
            this.optionsGB.Size = new System.Drawing.Size(222, 97);
            this.optionsGB.TabIndex = 16;
            this.optionsGB.TabStop = false;
            this.optionsGB.Text = "options";
            // 
            // deckCB
            // 
            this.deckCB.AutoSize = true;
            this.deckCB.Location = new System.Drawing.Point(17, 22);
            this.deckCB.Name = "deckCB";
            this.deckCB.Size = new System.Drawing.Size(80, 17);
            this.deckCB.TabIndex = 9;
            this.deckCB.Text = "sync decks";
            this.deckCB.UseVisualStyleBackColor = true;
            // 
            // allCB
            // 
            this.allCB.AutoSize = true;
            this.allCB.Location = new System.Drawing.Point(114, 68);
            this.allCB.Name = "allCB";
            this.allCB.Size = new System.Drawing.Size(61, 17);
            this.allCB.TabIndex = 14;
            this.allCB.Text = "sync all";
            this.allCB.UseVisualStyleBackColor = true;
            // 
            // replayCB
            // 
            this.replayCB.AutoSize = true;
            this.replayCB.Location = new System.Drawing.Point(17, 45);
            this.replayCB.Name = "replayCB";
            this.replayCB.Size = new System.Drawing.Size(84, 17);
            this.replayCB.TabIndex = 10;
            this.replayCB.Text = "sync replays";
            this.replayCB.UseVisualStyleBackColor = true;
            // 
            // texturesCB
            // 
            this.texturesCB.AutoSize = true;
            this.texturesCB.Location = new System.Drawing.Point(114, 45);
            this.texturesCB.Name = "texturesCB";
            this.texturesCB.Size = new System.Drawing.Size(88, 17);
            this.texturesCB.TabIndex = 13;
            this.texturesCB.Text = "sync textures";
            this.texturesCB.UseVisualStyleBackColor = true;
            // 
            // skinsCB
            // 
            this.skinsCB.AutoSize = true;
            this.skinsCB.Location = new System.Drawing.Point(17, 68);
            this.skinsCB.Name = "skinsCB";
            this.skinsCB.Size = new System.Drawing.Size(75, 17);
            this.skinsCB.TabIndex = 11;
            this.skinsCB.Text = "sync skins";
            this.skinsCB.UseVisualStyleBackColor = true;
            // 
            // soundsCB
            // 
            this.soundsCB.AutoSize = true;
            this.soundsCB.Location = new System.Drawing.Point(114, 22);
            this.soundsCB.Name = "soundsCB";
            this.soundsCB.Size = new System.Drawing.Size(85, 17);
            this.soundsCB.TabIndex = 12;
            this.soundsCB.Text = "sync sounds";
            this.soundsCB.UseVisualStyleBackColor = true;
            // 
            // DropBoxSynch_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 184);
            this.Controls.Add(this.optionsGB);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.descriptionLb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DropBoxSynch_frm";
            this.Text = "Synchronize with the Cloud";
            this.Load += new System.EventHandler(this.DropBoxSynch_frm_Load);
            this.optionsGB.ResumeLayout(false);
            this.optionsGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label descriptionLb;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.GroupBox optionsGB;
        private System.Windows.Forms.CheckBox deckCB;
        private System.Windows.Forms.CheckBox allCB;
        private System.Windows.Forms.CheckBox replayCB;
        private System.Windows.Forms.CheckBox texturesCB;
        private System.Windows.Forms.CheckBox skinsCB;
        private System.Windows.Forms.CheckBox soundsCB;
    }
}