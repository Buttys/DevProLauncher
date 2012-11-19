using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGOPro_Launcher.Config;

namespace YGOPro_Launcher
{
    public partial class Profile_frm : Form
    {
        public Profile_frm()
        {
            InitializeComponent();
            ApplyTranslation();
            Username.Text += Program.UserInfo.Username;
            wld.Text += Program.UserInfo.Wins + "/" + Program.UserInfo.Loses + "/" + Program.UserInfo.Draws;
        }

        public void ApplyTranslation()
        {
            LanguageInfo language = Program.LanguageManager.Translation;

            Username.Text = language.profileLblUsername;
            wld.Text = language.profileLblwld;
            rank.Text = language.profileLblRank;
            team.Text = language.profileLblTeam;
        }
    }
}
