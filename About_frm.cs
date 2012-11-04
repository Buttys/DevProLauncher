using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YGOPro_Launcher
{
    public partial class About_frm : Form
    {
        public About_frm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            ApplyTranslation();
        }

        public void ApplyTranslation()
        {
            AboutText.Text = Program.LanguageManager.Translation.aAboutText;
            label1.Text = Program.LanguageManager.Translation.aboutLabel1;
            label5.Text = Program.LanguageManager.Translation.aboutLabel5;
        }
    }
}
