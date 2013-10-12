using System;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using DevProLauncher.Config;

namespace DevProLauncher.Windows.MessageBoxs
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            Username.Text = Program.Config.DefaultUsername;
            
            Antialias.Text = Program.Config.Antialias.ToString(CultureInfo.InvariantCulture);
            EnableMusic.Checked = Program.Config.EnableMusic;
            EnableSound.Checked = Program.Config.EnableSound;
            Enabled3d.Checked = Program.Config.Enabled3D;
            Fullscreen.Checked = Program.Config.Fullscreen;
            GameFont.Text = Program.Config.GameFont;
            FontSize.Value = Program.Config.FontSize;
            AutoPlacing.Checked = Program.Config.AutoPlacing;
            RandomPlacing.Checked = Program.Config.RandomPlacing;
            AutoChain.Checked = Program.Config.AutoChain;
            NoDelay.Checked = Program.Config.NoChainDelay;
            EnableSleeveLoading.Checked = Program.Config.EnableCustomSleeves;

            if (Directory.Exists(Program.Config.LauncherDir + "skins/"))
            {
                string[] folders = Directory.GetDirectories(Path.Combine(Program.Config.LauncherDir ,"skins\\"));
                foreach (var folder in folders)
                    SkinList.Items.Add(Path.GetFileName(folder));
            }            
            
            if(SkinList.Items.Count  > Program.Config.Skin + 1)
                SkinList.SelectedIndex = Program.Config.Skin + 1;

            if (Directory.Exists(Program.Config.LauncherDir + "deck/"))
            {
                string[] decks = Directory.GetFiles(Program.Config.LauncherDir + "deck/");
                foreach (string deck in decks)
                    DefualtDeck.Items.Add(Path.GetFileNameWithoutExtension(deck));
            }
            DefualtDeck.Text = Program.Config.DefaultDeck;

            ApplyTranslation();
        }

        private void ApplyTranslation()
        {
            if (Program.LanguageManager.Loaded)
            {
                groupBox1.Text = Program.LanguageManager.Translation.optionGb1;
                groupBox2.Text = Program.LanguageManager.Translation.optionGb2;
                groupBox3.Text = Program.LanguageManager.Translation.optionGb3;
                groupBox4.Text = Program.LanguageManager.Translation.optionGb4;
                label1.Text = Program.LanguageManager.Translation.optionUser;
                label5.Text = Program.LanguageManager.Translation.optionDeck;
                label6.Text = Program.LanguageManager.Translation.optionAntialias;
                EnableSound.Text = Program.LanguageManager.Translation.optionCbSound;
                EnableMusic.Text = Program.LanguageManager.Translation.optionCbMusic;
                Enabled3d.Text = Program.LanguageManager.Translation.optionCbDirect;
                Fullscreen.Text = Program.LanguageManager.Translation.optionCbFull;
                label2.Text = Program.LanguageManager.Translation.optionTexts;
                label3.Text = Program.LanguageManager.Translation.optionTextf;
                QuickSettingsBtn.Text = Program.LanguageManager.Translation.optionBtnQuick;
                SaveBtn.Text = Program.LanguageManager.Translation.optionBtnSave;
                CancelBtn.Text = Program.LanguageManager.Translation.optionBtnCancel;
                AutoPlacing.Text = Program.LanguageManager.Translation.optionCbAutoPlacing;
                RandomPlacing.Text = Program.LanguageManager.Translation.optionCbRandomPlacing;
                AutoChain.Text = Program.LanguageManager.Translation.optionCbAutoChain;
                NoDelay.Text = Program.LanguageManager.Translation.optionCbNoChainDelay;
                EnableSleeveLoading.Text = Program.LanguageManager.Translation.optionCbEnableSleeves;

                LanguageInfo lang = Program.LanguageManager.Translation;

                RequestSettingsbtn.Text = lang.chatoptionsBtnRequestSettings;
                SaveBtn.Text = lang.chatoptionsBtnSave;
                CancelBtn.Text = lang.chatoptionsBtnCancel;

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
            Program.Config.Skin = SkinList.SelectedIndex == -1 ? -1: SkinList.SelectedIndex - 1;
            Program.Config.AutoPlacing = AutoPlacing.Checked;
            Program.Config.RandomPlacing = RandomPlacing.Checked;
            Program.Config.AutoChain = AutoChain.Checked;
            Program.Config.NoChainDelay = NoDelay.Checked;
            Program.Config.EnableCustomSleeves = EnableSleeveLoading.Checked;

            Program.SaveConfig(Program.ConfigurationFilename,Program.Config);
            DialogResult = DialogResult.OK;

        }

        private void QuickSettingsBtn_Click(object sender, EventArgs e)
        {

            var form = new Host(true, false)
                {
                    Text = Program.LanguageManager.Translation.QuickHostSetting,
                    HostBtn = {Text = Program.LanguageManager.Translation.QuickHostBtn}
                };

            if (form.ShowDialog() == DialogResult.OK)
            {
                Program.Config.CardRules = form.CardRules.Text;
                Program.Config.Mode = form.Mode.Text;
                Program.Config.EnablePrority = form.Priority.Checked;
                Program.Config.DisableCheckDeck = form.CheckDeck.Checked;
                Program.Config.DisableShuffleDeck = form.ShuffleDeck.Checked;
                Program.Config.Lifepoints = form.LifePoints.Text;
                Program.Config.BanList = form.BanList.Text;
                Program.Config.TimeLimit = form.TimeLimit.Text;
            }

        }

        private void RequestSettingsbtn_Click(object sender, EventArgs e)
        {
            var form = new Host
                {
                    Text = Program.LanguageManager.Translation.chatoptionsRequestFormText,
                    HostBtn = {Text = Program.LanguageManager.Translation.chatoptionsBtnSave},
                    ShuffleDeck = {Enabled = false},
                    CheckDeck = {Enabled = false},
                    Priority = {Enabled = false},
                    LifePoints = {Enabled = false}
                };
            form.Mode.Items.Remove("Tag");

            if (form.ShowDialog() != DialogResult.OK) return;
            Program.Config.chtCardRules = form.CardRules.Text;
            Program.Config.chtMode = form.Mode.Text;
            Program.Config.chtEnablePrority = form.Priority.Checked;
            Program.Config.chtDisableCheckDeck = form.CheckDeck.Checked;
            Program.Config.chtDisableShuffleDeck = form.ShuffleDeck.Checked;
            Program.Config.chtLifepoints = form.LifePoints.Text;
            Program.Config.chtBanList = form.BanList.Text;
            Program.Config.chtTimeLimit = form.TimeLimit.Text;
        }

        private void dropboxbtn_Click(object sender, EventArgs e)
        {
            DropBoxSynch_frm dbsyncfrm = new DropBoxSynch_frm();
            dbsyncfrm.Show();
        }
    }
}
