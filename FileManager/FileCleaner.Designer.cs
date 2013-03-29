namespace YGOPro_Launcher.FileManager
{
    partial class FileCleaner
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
            this.updateBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // updateBar
            // 
            this.updateBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateBar.Location = new System.Drawing.Point(0, 0);
            this.updateBar.Name = "updateBar";
            this.updateBar.Size = new System.Drawing.Size(228, 32);
            this.updateBar.TabIndex = 0;
            // 
            // FileCleaner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 32);
            this.ControlBox = false;
            this.Controls.Add(this.updateBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FileCleaner";
            this.Text = "Cleaning Files";
            this.Load += new System.EventHandler(this.FileCleaner_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar updateBar;
    }
}