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
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();
            Username.Text += Program.UserInfo.Username;
            wld.Text += Program.UserInfo.Wins + "/" + Program.UserInfo.Loses + "/" + Program.UserInfo.Draws;
        }
    }
}
