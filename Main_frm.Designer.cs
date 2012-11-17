namespace YGOPro_Launcher
{
    partial class Main_frm
    {

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_frm));
            this.ServerControl = new System.Windows.Forms.TabControl();
            this.ConnectionCheck = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ServerControl
            // 
            this.ServerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerControl.Location = new System.Drawing.Point(0, 0);
            this.ServerControl.Name = "ServerControl";
            this.ServerControl.SelectedIndex = 0;
            this.ServerControl.Size = new System.Drawing.Size(934, 491);
            this.ServerControl.TabIndex = 0;
            // 
            // ConnectionCheck
            // 
            this.ConnectionCheck.Enabled = true;
            this.ConnectionCheck.Interval = 5000;
            // 
            // Main_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(934, 491);
            this.Controls.Add(this.ServerControl);
            this.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main_frm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YGOPro Launcher";
            this.Load += new System.EventHandler(this.Main_frm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ServerControl;
        private System.Windows.Forms.Timer ConnectionCheck;
        private System.ComponentModel.IContainer components;
    }
}