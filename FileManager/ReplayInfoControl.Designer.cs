namespace YGOPro_Launcher
{
    partial class ReplayInfoControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ReplayInfo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ReplayInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 386);
            this.panel1.TabIndex = 0;
            // 
            // ReplayInfo
            // 
            this.ReplayInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReplayInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReplayInfo.Location = new System.Drawing.Point(0, 0);
            this.ReplayInfo.Name = "ReplayInfo";
            this.ReplayInfo.Size = new System.Drawing.Size(235, 386);
            this.ReplayInfo.TabIndex = 0;
            this.ReplayInfo.Text = "Not Implemented";
            // 
            // ReplayInfoControlcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 386);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReplayInfoControlcs";
            this.Text = "ReplayInfoControlcs";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ReplayInfo;
    }
}