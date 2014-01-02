namespace DevProLauncher.Windows.Components
{
    sealed partial class FileManagerControl
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
            this.FileList = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ImportBtn = new System.Windows.Forms.Button();
            this.GameBtn = new System.Windows.Forms.Button();
            this.OpenBtn = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.RenameBtn = new System.Windows.Forms.Button();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 248F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.Controls.Add(this.FileList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(810, 451);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // FileList
            // 
            this.FileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileList.FormattingEnabled = true;
            this.FileList.Location = new System.Drawing.Point(3, 3);
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(446, 445);
            this.FileList.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ImportBtn);
            this.flowLayoutPanel1.Controls.Add(this.GameBtn);
            this.flowLayoutPanel1.Controls.Add(this.OpenBtn);
            this.flowLayoutPanel1.Controls.Add(this.DeleteBtn);
            this.flowLayoutPanel1.Controls.Add(this.RenameBtn);
            this.flowLayoutPanel1.Controls.Add(this.RefreshBtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(703, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(104, 445);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // ImportBtn
            // 
            this.ImportBtn.Location = new System.Drawing.Point(3, 415);
            this.ImportBtn.Name = "ImportBtn";
            this.ImportBtn.Size = new System.Drawing.Size(99, 27);
            this.ImportBtn.TabIndex = 4;
            this.ImportBtn.Text = "Import";
            this.ImportBtn.UseVisualStyleBackColor = true;
            this.ImportBtn.Click += new System.EventHandler(this.ImportBtn_Click);
            // 
            // GameBtn
            // 
            this.GameBtn.Location = new System.Drawing.Point(3, 382);
            this.GameBtn.Name = "GameBtn";
            this.GameBtn.Size = new System.Drawing.Size(99, 27);
            this.GameBtn.TabIndex = 0;
            this.GameBtn.Text = "Open Game";
            this.GameBtn.UseVisualStyleBackColor = true;
            this.GameBtn.Click += new System.EventHandler(this.GameBtn_Click);
            // 
            // OpenBtn
            // 
            this.OpenBtn.Location = new System.Drawing.Point(3, 349);
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(99, 27);
            this.OpenBtn.TabIndex = 1;
            this.OpenBtn.Text = "Open Folder";
            this.OpenBtn.UseVisualStyleBackColor = true;
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Location = new System.Drawing.Point(3, 316);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(99, 27);
            this.DeleteBtn.TabIndex = 2;
            this.DeleteBtn.Text = "Delete";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteItem);
            // 
            // RenameBtn
            // 
            this.RenameBtn.Location = new System.Drawing.Point(3, 283);
            this.RenameBtn.Name = "RenameBtn";
            this.RenameBtn.Size = new System.Drawing.Size(99, 27);
            this.RenameBtn.TabIndex = 3;
            this.RenameBtn.Text = "Rename";
            this.RenameBtn.UseVisualStyleBackColor = true;
            this.RenameBtn.Click += new System.EventHandler(this.RenameItem);
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Location = new System.Drawing.Point(3, 250);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(99, 27);
            this.RefreshBtn.TabIndex = 5;
            this.RefreshBtn.Text = "Refresh";
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // FileManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(810, 451);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FileManagerControl";
            this.Text = "FileManagerDesign";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox FileList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button GameBtn;
        private System.Windows.Forms.Button OpenBtn;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.Button RenameBtn;
        private System.Windows.Forms.Button ImportBtn;
        private System.Windows.Forms.Button RefreshBtn;

    }
}