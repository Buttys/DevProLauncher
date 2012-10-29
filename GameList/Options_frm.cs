using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace YGOPro_Launcher
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            Username.Text = Program.Config.DefualtUsername;
            
            Antialias.Text = Program.Config.Antialias.ToString();
            EnableMusic.Checked = Program.Config.EnableMusic;
            EnableSound.Checked = Program.Config.EnableSound;
            Enabled3d.Checked = Program.Config.Enabled3d;
            Fullscreen.Checked = Program.Config.Fullscreen;
            if (Directory.Exists(Program.Config.LauncherDir + "deck/"))
            {
                string[] decks = Directory.GetFiles(Program.Config.LauncherDir + "deck/");
                foreach (string deck in decks)
                    DefualtDeck.Items.Add(Path.GetFileNameWithoutExtension(deck));
            }
            DefualtDeck.Text = Program.Config.DefualtDeck;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Program.Config.DefualtDeck = DefualtDeck.Text;
            Program.Config.DefualtUsername = Username.Text;
            Program.Config.Antialias = Convert.ToInt32(Antialias.Text);
            Program.Config.EnableSound = EnableSound.Checked;
            Program.Config.EnableMusic = EnableMusic.Checked;
            Program.Config.Enabled3d = Enabled3d.Checked;
            Program.Config.Fullscreen = Fullscreen.Checked;

            Program.Config.Save(Program.ConfigurationFilename);
            DialogResult = DialogResult.OK;

        }

        private void QuickSettingsBtn_Click(object sender, EventArgs e)
        {
            Host form = new Host();
            form.Text = "Quick Host Settings";
            form.HostBtn.Text = "Confirm";

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Program.Config.CardRules = form.CardRules.Text;
                Program.Config.Mode = form.Mode.Text;
                Program.Config.EnablePrority = form.Priority.Checked;
                Program.Config.DisableCheckDeck = form.CheckDeck.Checked;
                Program.Config.DisableShuffleDeck = form.ShuffleDeck.Checked;
                Program.Config.Lifepoints = form.LifePoints.Text;
                Program.Config.GameName = form.GameName.Text;
            }

        }

        private void ForgetAutoLoginButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to forget auto login credentials?", "Confirmation required", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Program.Config.Password = "";
                Program.Config.AutoLogin = false;
                Program.Config.Save(Program.ConfigurationFilename);
            }
        }
    }
}
