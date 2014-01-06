namespace DevProLauncher.Windows.Components
{
    sealed partial class ReplayInfoControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ReplayInfo = new System.Windows.Forms.Label();
            this.VSText = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 386);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ReplayInfo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.VSText, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(235, 386);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ReplayInfo
            // 
            this.ReplayInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReplayInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReplayInfo.Location = new System.Drawing.Point(3, 35);
            this.ReplayInfo.Name = "ReplayInfo";
            this.ReplayInfo.Size = new System.Drawing.Size(229, 75);
            this.ReplayInfo.TabIndex = 1;
            this.ReplayInfo.Text = "No Replay Selected";
            // 
            // VSText
            // 
            this.VSText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VSText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VSText.Location = new System.Drawing.Point(3, 0);
            this.VSText.Name = "VSText";
            this.VSText.Size = new System.Drawing.Size(229, 35);
            this.VSText.TabIndex = 7;
            this.VSText.Text = "??? vs ???";
            this.VSText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReplayInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 386);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReplayInfoControl";
            this.Text = "ReplayInfoControlcs";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label ReplayInfo;
        private System.Windows.Forms.Label VSText;
    }
}