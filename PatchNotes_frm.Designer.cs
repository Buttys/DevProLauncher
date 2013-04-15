namespace YGOPro_Launcher
{
    partial class PatchNotes_frm
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
            this.PatchNotes = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // PatchNotes
            // 
            this.PatchNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PatchNotes.Location = new System.Drawing.Point(0, 0);
            this.PatchNotes.Name = "PatchNotes";
            this.PatchNotes.ReadOnly = true;
            this.PatchNotes.Size = new System.Drawing.Size(590, 388);
            this.PatchNotes.TabIndex = 0;
            this.PatchNotes.Text = "";
            // 
            // PatchNotes_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 388);
            this.Controls.Add(this.PatchNotes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PatchNotes_frm";
            this.Text = "PatchNotes_frm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox PatchNotes;
    }
}