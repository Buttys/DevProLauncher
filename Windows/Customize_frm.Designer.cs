namespace DevProLauncher.Windows
{
    sealed partial class CustomizeFrm
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
            this.ContentList = new System.Windows.Forms.ListView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ViewSelect = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddContentBtn = new System.Windows.Forms.Button();
            this.PreviewBtn = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.BackUpBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ThemeSelect = new System.Windows.Forms.ComboBox();
            this.AddThemeBtn = new System.Windows.Forms.Button();
            this.RemoveThemeBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ContentList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(802, 384);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // ContentList
            // 
            this.ContentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentList.Location = new System.Drawing.Point(3, 3);
            this.ContentList.Name = "ContentList";
            this.ContentList.Size = new System.Drawing.Size(796, 337);
            this.ContentList.TabIndex = 0;
            this.ContentList.UseCompatibleStateImageBehavior = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.55263F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.44737F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 346);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(796, 35);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ViewSelect);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.AddContentBtn);
            this.flowLayoutPanel1.Controls.Add(this.PreviewBtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(429, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(364, 29);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // ViewSelect
            // 
            this.ViewSelect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ViewSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ViewSelect.FormattingEnabled = true;
            this.ViewSelect.Items.AddRange(new object[] {
            "Covers",
            "Backgrounds",
            "GameBackgrounds",
            "Attack",
            "Activate",
            "Chain",
            "Equip",
            "Rock",
            "Paper",
            "Sissors",
            "Life Points Color",
            "Life Points Bar",
            "Target",
            "Negated",
            "Music",
            "SoundEffects",
            "Field",
            "FieldTransparent",
            "Mask",
            "Numbers"});
            this.ViewSelect.Location = new System.Drawing.Point(240, 4);
            this.ViewSelect.Name = "ViewSelect";
            this.ViewSelect.Size = new System.Drawing.Size(121, 21);
            this.ViewSelect.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Content";
            // 
            // AddContentBtn
            // 
            this.AddContentBtn.Location = new System.Drawing.Point(109, 3);
            this.AddContentBtn.Name = "AddContentBtn";
            this.AddContentBtn.Size = new System.Drawing.Size(75, 23);
            this.AddContentBtn.TabIndex = 3;
            this.AddContentBtn.Text = "Add";
            this.AddContentBtn.UseVisualStyleBackColor = true;
            this.AddContentBtn.Click += new System.EventHandler(this.AddContentBtn_Click);
            // 
            // PreviewBtn
            // 
            this.PreviewBtn.Location = new System.Drawing.Point(28, 3);
            this.PreviewBtn.Name = "PreviewBtn";
            this.PreviewBtn.Size = new System.Drawing.Size(75, 23);
            this.PreviewBtn.TabIndex = 2;
            this.PreviewBtn.Text = "Preview";
            this.PreviewBtn.UseVisualStyleBackColor = true;
            this.PreviewBtn.Click += new System.EventHandler(this.PreviewBtn_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.BackUpBtn);
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.ThemeSelect);
            this.flowLayoutPanel2.Controls.Add(this.AddThemeBtn);
            this.flowLayoutPanel2.Controls.Add(this.RemoveThemeBtn);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(420, 29);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // BackUpBtn
            // 
            this.BackUpBtn.Location = new System.Drawing.Point(3, 3);
            this.BackUpBtn.Name = "BackUpBtn";
            this.BackUpBtn.Size = new System.Drawing.Size(75, 23);
            this.BackUpBtn.TabIndex = 4;
            this.BackUpBtn.Text = "BackUp";
            this.BackUpBtn.UseVisualStyleBackColor = true;
            this.BackUpBtn.Click += new System.EventHandler(this.BackUpBtn_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Theme";
            // 
            // ThemeSelect
            // 
            this.ThemeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ThemeSelect.FormattingEnabled = true;
            this.ThemeSelect.Location = new System.Drawing.Point(130, 3);
            this.ThemeSelect.Name = "ThemeSelect";
            this.ThemeSelect.Size = new System.Drawing.Size(121, 21);
            this.ThemeSelect.TabIndex = 5;
            // 
            // AddThemeBtn
            // 
            this.AddThemeBtn.Location = new System.Drawing.Point(257, 3);
            this.AddThemeBtn.Name = "AddThemeBtn";
            this.AddThemeBtn.Size = new System.Drawing.Size(75, 23);
            this.AddThemeBtn.TabIndex = 6;
            this.AddThemeBtn.Text = "Add";
            this.AddThemeBtn.UseVisualStyleBackColor = true;
            this.AddThemeBtn.Click += new System.EventHandler(this.AddThemeBtn_Click);
            // 
            // RemoveThemeBtn
            // 
            this.RemoveThemeBtn.Location = new System.Drawing.Point(338, 3);
            this.RemoveThemeBtn.Name = "RemoveThemeBtn";
            this.RemoveThemeBtn.Size = new System.Drawing.Size(75, 23);
            this.RemoveThemeBtn.TabIndex = 7;
            this.RemoveThemeBtn.Text = "Remove";
            this.RemoveThemeBtn.UseVisualStyleBackColor = true;
            this.RemoveThemeBtn.Click += new System.EventHandler(this.RemoveThemeBtn_Click);
            // 
            // CustomizeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(802, 384);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomizeFrm";
            this.Text = "Customize_frm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView ContentList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox ViewSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button PreviewBtn;
        private System.Windows.Forms.Button AddContentBtn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button BackUpBtn;
        private System.Windows.Forms.ComboBox ThemeSelect;
        private System.Windows.Forms.Button AddThemeBtn;
        private System.Windows.Forms.Button RemoveThemeBtn;
        private System.Windows.Forms.Label label2;
    }
}