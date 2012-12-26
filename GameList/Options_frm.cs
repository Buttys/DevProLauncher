using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using YGOPro_Launcher.Config;

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
            UseSkin.Checked = Program.Config.UseSkin;
            AutoPlacing.Checked = Program.Config.AutoPlacing;
            RandomPlacing.Checked = Program.Config.RandomPlacing;
            AutoChain.Checked = Program.Config.AutoChain;
            NoDelay.Checked = Program.Config.NoChainDelay;
            if (Directory.Exists(Program.Config.LauncherDir + "deck/"))
            {
                string[] decks = Directory.GetFiles(Program.Config.LauncherDir + "deck/");
                foreach (string deck in decks)
                    DefualtDeck.Items.Add(Path.GetFileNameWithoutExtension(deck));
            }
            DefualtDeck.Text = Program.Config.DefaultDeck;
            UseSkin.CheckedChanged += new EventHandler(UseSkin_CheckedChanged);

            ChatBgColor.SelectedItem = Program.Config.ChatBGColor;
            NormalTextColor.SelectedItem = Program.Config.NormalTextColor;
            Level99Color.SelectedItem = Program.Config.Level99Color;
            Level2Color.SelectedItem = Program.Config.Level2Color;
            Level1Color.SelectedItem = Program.Config.Level1Color;
            Level0Color.SelectedItem = Program.Config.Level0Color;
            ServerColor.SelectedItem = Program.Config.ServerMsgColor;
            MeColor.SelectedItem = Program.Config.MeMsgColor;
            JoinColor.SelectedItem = Program.Config.JoinColor;
            LeaveColor.SelectedItem = Program.Config.LeaveColor;
            SystemColor.SelectedItem = Program.Config.SystemColor;
            HideJoinLeavechk.Checked = Program.Config.HideJoinLeave;
            PmWindowschk.Checked = Program.Config.PmWindows;
            Colorblindchk.Checked = Program.Config.ColorBlindMode;
            Timestampchk.Checked = Program.Config.ShowTimeStamp;
            DuelRequestchk.Checked = Program.Config.RefuseDuelRequests;

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
                ForgetAutoLoginButton.Text = Program.LanguageManager.Translation.optionBtnAutoLogin;
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
                UseSkin.Text = Program.LanguageManager.Translation.optionCbCustomSkin;
                AutoPlacing.Text = Program.LanguageManager.Translation.optionCbAutoPlacing;
                RandomPlacing.Text = Program.LanguageManager.Translation.optionCbRandomPlacing;
                AutoChain.Text = Program.LanguageManager.Translation.optionCbAutoChain;
                NoDelay.Text = Program.LanguageManager.Translation.optionCbNoChainDelay;

                LanguageInfo lang = Program.LanguageManager.Translation;

                this.OptionTabControl.TabPages[1].Text = lang.chatoptionsFormName;
                groupBox7.Text = lang.chatoptionsGb2;
                groupBox5.Text = lang.chatoptionsGb3;
                groupBox6.Text = lang.chatoptionsGb4;
                HideJoinLeavechk.Text = lang.chatoptionsLblHideJoinLeave;
                PmWindowschk.Text = lang.chatoptionsLblPmWindows;
                Colorblindchk.Text = lang.chatoptionsLblColorBlindMode;
                Timestampchk.Text = lang.chatoptionsLblShowTimeStamp;
                DuelRequestchk.Text = lang.chatoptionsLblRefuseDuelRequests;
                label13.Text = lang.chatoptionsLblChatBackground;
                label12.Text = lang.chatoptionsLblNormalText;
                label11.Text = lang.chatoptionsLblLevel99;
                label10.Text = lang.chatoptionsLblLevel2;
                label9.Text = lang.chatoptionsLblLevel1;
                label4.Text = lang.chatoptionsLblNormalUser;
                label7.Text = lang.chatoptionsLblServerMessages;
                label8.Text = lang.chatoptionsLblMeMessage;
                label14.Text = lang.chatoptionsLblJoin;
                label15.Text = lang.chatoptionsLblLeave;
                label16.Text = lang.chatoptionsLblSystem;
                RequestSettingsbtn.Text = lang.chatoptionsBtnRequestSettings;
                SaveBtn.Text = lang.chatoptionsBtnSave;
                CancelBtn.Text = lang.chatoptionsBtnCancel;

            }
        }

        private void UseSkin_CheckedChanged(object sender, EventArgs e)
        {
            if (!Enabled3d.Checked)
            {
                Enabled3d.Checked = true;
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
            Program.Config.UseSkin = UseSkin.Checked;
            Program.Config.AutoPlacing = AutoPlacing.Checked;
            Program.Config.RandomPlacing = RandomPlacing.Checked;
            Program.Config.AutoChain = AutoChain.Checked;
            Program.Config.NoChainDelay = NoDelay.Checked;

            Program.Config.ChatBGColor = ChatBgColor.SelectedItem.ToString();
            Program.Config.NormalTextColor = NormalTextColor.SelectedItem.ToString();
            Program.Config.Level99Color = Level99Color.SelectedItem.ToString();
            Program.Config.Level2Color = Level2Color.SelectedItem.ToString();
            Program.Config.Level1Color = Level1Color.SelectedItem.ToString();
            Program.Config.Level0Color = Level0Color.SelectedItem.ToString();
            Program.Config.ServerMsgColor = ServerColor.SelectedItem.ToString();
            Program.Config.MeMsgColor = MeColor.SelectedItem.ToString();
            Program.Config.JoinColor = JoinColor.SelectedItem.ToString();
            Program.Config.LeaveColor = LeaveColor.SelectedItem.ToString();
            Program.Config.SystemColor = SystemColor.SelectedItem.ToString();
            Program.Config.PmWindows = PmWindowschk.Checked;
            Program.Config.HideJoinLeave = HideJoinLeavechk.Checked;
            Program.Config.ColorBlindMode = Colorblindchk.Checked;
            Program.Config.ShowTimeStamp = Timestampchk.Checked;
            Program.Config.RefuseDuelRequests = DuelRequestchk.Checked;

            Program.SaveConfig(Program.ConfigurationFilename,Program.Config);
            DialogResult = DialogResult.OK;

        }

        private void QuickSettingsBtn_Click(object sender, EventArgs e)
        {
            Host form = new Host(true, false);
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
                Program.Config.BanList = form.BanList.Text;
                Program.Config.TimeLimit = form.TimeLimit.Text;
            }

        }

        private void RequestSettingsbtn_Click(object sender, EventArgs e)
        {
            Host form = new Host();
            form.Text = Program.LanguageManager.Translation.chatoptionsRequestFormText;
            form.HostBtn.Text = Program.LanguageManager.Translation.chatoptionsBtnSave;
            form.ShuffleDeck.Enabled = false;
            form.CheckDeck.Enabled = false;
            form.Priority.Enabled = false;
            form.LifePoints.Enabled = false;
            form.Mode.Items.Remove("Tag");

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Program.Config.chtCardRules = form.CardRules.Text;
                Program.Config.chtMode = form.Mode.Text;
                Program.Config.chtEnablePrority = form.Priority.Checked;
                Program.Config.chtDisableCheckDeck = form.CheckDeck.Checked;
                Program.Config.chtDisableShuffleDeck = form.ShuffleDeck.Checked;
                Program.Config.chtLifepoints = form.LifePoints.Text;
                Program.Config.chtGameName = form.GameName.Text;
                Program.Config.chtBanList = form.BanList.Text;
                Program.Config.chtTimeLimit = form.TimeLimit.Text;
            }
        }

        private void ForgetAutoLoginButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Program.LanguageManager.Translation.optionMsbForget, "Confirmation required", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Program.Config.Password = "";
                Program.Config.AutoLogin = false;
                Program.SaveConfig(Program.ConfigurationFilename,Program.Config);
            }
        }
    }
}
