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
            this.submitBtn.Location = new System.Drawing.Point(13, 43);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(230, 23);
            this.submitBtn.TabIndex = 7;
            this.submitBtn.Text = "Sync";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // DropBoxSynch_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 79);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.descriptionLb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DropBoxSynch_frm";
            this.Text = "Synchronize with the Cloud";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label descriptionLb;
        private System.Windows.Forms.Button submitBtn;
    }
}