using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGOPro_Launcher;
using YGOPro_Launcher.Config;

namespace YGOPro_Launcher.Chat
{
    public partial class ChatOptions_frm : Form
    {
        public ChatOptions_frm()
        {
            InitializeComponent();
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

            ApplyTranslations();
        }

        private void ApplyTranslations()
        {

            LanguageInfo lang = Program.LanguageManager.Translation;

            this.Text = lang.chatoptionsFormName;
            groupBox1.Text = lang.chatoptionsGb1;
            groupBox4.Text = lang.chatoptionsGb2;
            groupBox2.Text = lang.chatoptionsGb3;
            groupBox3.Text = lang.chatoptionsGb4;
            HideJoinLeavechk.Text = lang.chatoptionsLblHideJoinLeave;
            PmWindowschk.Text = lang.chatoptionsLblPmWindows;
            Colorblindchk.Text = lang.chatoptionsLblColorBlindMode;
            Timestampchk.Text = lang.chatoptionsLblShowTimeStamp;
            DuelRequestchk.Text = lang.chatoptionsLblRefuseDuelRequests;
            label1.Text = lang.chatoptionsLblChatBackground;
            label2.Text = lang.chatoptionsLblNormalText;
            label3.Text = lang.chatoptionsLblLevel99;
            label4.Text = lang.chatoptionsLblLevel2;
            label5.Text = lang.chatoptionsLblLevel1;
            label6.Text = lang.chatoptionsLblNormalUser;
            label7.Text = lang.chatoptionsLblServerMessages;
            label8.Text = lang.chatoptionsLblMeMessage;
            label9.Text = lang.chatoptionsLblJoin;
            label10.Text = lang.chatoptionsLblLeave;
            label11.Text = lang.chatoptionsLblSystem;
            RequestSettingsbtn.Text = lang.chatoptionsBtnRequestSettings;
            SaveBtn.Text = lang.chatoptionsBtnSave;
            CancelBtn.Text = lang.chatoptionsBtnCancel;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {

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

            DialogResult = System.Windows.Forms.DialogResult.OK;
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


    }
}
