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
            Username.Text = Program.Config.DefaultUsername;
            
            Antialias.Text = Program.Config.Antialias.ToString();
            EnableMusic.Checked = Program.Config.EnableMusic;
            EnableSound.Checked = Program.Config.EnableSound;
            Enabled3d.Checked = Program.Config.Enabled3D;
            Fullscreen.Checked = Program.Config.Fullscreen;
            GameFont.Text = Program.Config.GameFont;
            FontSize.Value = Program.Config.FontSize;
            if (Directory.Exists(Program.Config.LauncherDir + "deck/"))
            {
                string[] decks = Directory.GetFiles(Program.Config.LauncherDir + "deck/");
                foreach (string deck in decks)
                    DefualtDeck.Items.Add(Path.GetFileNameWithoutExtension(deck));
            }
            DefualtDeck.Text = Program.Config.DefaultDeck;
        }

        private void ApplyTranslation()
        {
            if (Program.LanguageManager.Loaded)
            {
                groupBox1.Text = Program.LanguageManager.Translation.optionGb1;
                groupBox2.Text = Program.LanguageManager.Translation.optionGb2;
                groupBox3.Text = Program.LanguageManager.Translation.optionGb3;
                label1.Text = Program.LanguageManager.Translation.optionUser;
                label5.Text = Program.LanguageManager.Translation.optionDeck;
                ForgetAutoLoginButton.Text = Program.LanguageManager.Translation.optionBtnAutoLogin;
                label4.Text = Program.LanguageManager.Translation.optionAntialias;
                EnableSound.Text = Program.LanguageManager.Translation.optionCbSound;
                EnableMusic.Text = Program.LanguageManager.Translation.optionCbMusic;
                Enabled3d.Text = Program.LanguageManager.Translation.optionCbDirect;
                Fullscreen.Text = Program.LanguageManager.Translation.optionCbFull;
                label2.Text = Program.LanguageManager.Translation.optionTexts;
                label3.Text = Program.LanguageManager.Translation.optionTextf;
                QuickSettingsBtn.Text = Program.LanguageManager.Translation.optionBtnQuick;
                SaveBtn.Text = Program.LanguageManager.Translation.optionBtnSave;
                CancelBtn.Text = Program.LanguageManager.Translation.optionBtnCancel;
            }
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
            Program.Config.FontSize = (int)FontSize.Value;
            Program.Config.GameFont = GameFont.Text;
            Program.Config.Save(Program.ConfigurationFilename);
            DialogResult = DialogResult.OK;

        }

        private void QuickSettingsBtn_Click(object sender, EventArgs e)
        {
            Host form = new Host();
            form.Text = Program.LanguageManager.Translation.QuickHostSetting;
            form.HostBtn.Text = Program.LanguageManager.Translation.QuickHostBtn;

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
            if (MessageBox.Show(Program.LanguageManager.Translation.optionMsbForget, "Confirmation required", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Program.Config.Password = "";
                Program.Config.AutoLogin = false;
                Program.Config.Save(Program.ConfigurationFilename);
            }
        }
    }
}
