namespace DevProLauncher.Windows
{
    partial class Browser_frm
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
            this.browserWb = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // browserWb
            // 
            this.browserWb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserWb.Location = new System.Drawing.Point(0, 0);
            this.browserWb.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserWb.Name = "browserWb";
            this.browserWb.Size = new System.Drawing.Size(803, 545);
            this.browserWb.TabIndex = 0;
            // 
            // Browser_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 545);
            this.Controls.Add(this.browserWb);
            this.Name = "Browser_frm";
            this.Text = "Browser";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser browserWb;
    }
}