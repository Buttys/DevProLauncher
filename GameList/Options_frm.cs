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
        private Language lang = new Language();

        public Settings()
        {
            InitializeComponent();
            Username.Text = Program.Config.DefaultUsername;
            
            Antialias.Text = Program.Config.Antialias.ToString();
            EnableMusic.Checked = Program.Config.EnableMusic;
            EnableSound.Checked = Program.Config.EnableSound;
            Enabled3d.Checked = Program.Config.Enabled3D;
            Fullscreen.Checked = Program.Config.Fullscreen;
            nUDFontsize.Text = Program.Config.TextSize.ToString();
            cBTextfont.Text = Program.Config.TextFont;
            if (Directory.Exists(Program.Config.LauncherDir + "deck/"))
            {
                string[] decks = Directory.GetFiles(Program.Config.LauncherDir + "deck/");
                foreach (string deck in decks)
                    DefualtDeck.Items.Add(Path.GetFileNameWithoutExtension(deck));
            }
            DefualtDeck.Text = Program.Config.DefaultDeck;
            lang.Load(Program.Config.language + ".conf");
            newText();
        }

        private void newText()
        {
            groupBox1.Text = lang.optionGb1;
            groupBox2.Text = lang.optionGb2;
            groupBox3.Text = lang.optionGb3;
            label1.Text = lang.optionUser;
            label5.Text = lang.optionDeck;
            ForgetAutoLoginButton.Text = lang.optionBtnAutoLogin;
            label4.Text = lang.optionAntialias;
            EnableSound.Text = lang.optionCbSound;
            EnableMusic.Text = lang.optionCbMusic;
            Enabled3d.Text = lang.optionCbDirect;
            Fullscreen.Text = lang.optionCbFull;
            label2.Text = lang.optionTexts;
            label3.Text = lang.optionTextf;
            QuickSettingsBtn.Text = lang.optionBtnQuick;
            SaveBtn.Text = lang.optionBtnSave;
            CancelBtn.Text = lang.optionBtnCancel;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Program.Config.DefaultDeck = DefualtDeck.Text;
            Program.Config.DefaultUsername = Username.Text;
            Program.Config.Antialias = Convert.ToInt32(Antialias.Text);
            Program.Config.EnableSound = EnableSound.Checked;
            Program.Config.EnableMusic = EnableMusic.Checked;
            Program.Config.Enabled3D = Enabled3d.Checked;
            Program.Config.Fullscreen = Fullscreen.Checked;
            Program.Config.TextFont = cBTextfont.Text;
            Program.Config.TextSize = Convert.ToInt32(nUDFontsize.Text);

            Program.Config.Save(Program.ConfigurationFilename);
            DialogResult = DialogResult.OK;

        }

        private void QuickSettingsBtn_Click(object sender, EventArgs e)
        {
            Host form = new Host();
            form.Text = lang.QuickHostSetting;
            form.HostBtn.Text = lang.QuickHostBtn;

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
            if (MessageBox.Show(lang.optionMsbForget, "Confirmation required", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Program.Config.Password = "";
                Program.Config.AutoLogin = false;
                Program.Config.Save(Program.ConfigurationFilename);
            }
        }
    }
}
