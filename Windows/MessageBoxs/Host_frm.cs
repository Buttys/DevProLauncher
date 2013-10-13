using System;
using System.Globalization;
using System.Windows.Forms;
using DevProLauncher.Helpers;

namespace DevProLauncher.Windows.MessageBoxs
{
    public partial class Host : Form
    {
        public string GameName;

        public Host(bool options, bool isranked)
        {
            InitializeComponent();
            if (options)
            {
                TimeLimit.SelectedItem = Program.Config.TimeLimit;
                BanList.SelectedItem = Program.Config.BanList;
                Mode.SelectedItem = Program.Config.Mode;
                GameName = LauncherHelper.GenerateString().Substring(0, 5);
                CardRules.SelectedItem = Program.Config.CardRules;
                Priority.Checked = Program.Config.EnablePrority;
                ShuffleDeck.Checked = Program.Config.DisableShuffleDeck;
                CheckDeck.Checked = Program.Config.DisableCheckDeck;
                BanList.Items.AddRange(LauncherHelper.GetBanListArray());
                BanList.SelectedItem = Program.Config.BanList;
                if (BanList.SelectedItem == null && BanList.Items.Count > 0)
                    BanList.SelectedIndex = 0;
            }
            else
            {
                TimeLimit.SelectedIndex = 0;
                CardRules.SelectedIndex = 0;
                Mode.SelectedIndex = 0;
                GameName = LauncherHelper.GenerateString().Substring(0, 5);
                BanList.Items.AddRange(LauncherHelper.GetBanListArray());
                if (BanList.Items.Count > 0)
                    BanList.SelectedIndex = 0;
                

            }
            Mode.SelectedIndexChanged += DuelModeChanged;
            if(!isranked)
                CardRules.SelectedIndexChanged += CardRulesChanged;
            ApplyTranslation();
        }

        public Host()
        {
            InitializeComponent();

            TimeLimit.SelectedItem = Program.Config.chtTimeLimit;
            BanList.SelectedItem = Program.Config.chtBanList;
            Mode.SelectedItem = Program.Config.chtMode;
            GameName = LauncherHelper.GenerateString().Substring(0,5);
            CardRules.SelectedItem = Program.Config.chtCardRules;
            Priority.Checked = Program.Config.chtEnablePrority;
            ShuffleDeck.Checked = Program.Config.chtDisableShuffleDeck;
            CheckDeck.Checked = Program.Config.chtDisableCheckDeck;
            BanList.Items.AddRange(LauncherHelper.GetBanListArray());
            if (BanList.Items.Count > 0)
            {
                if (BanList.Items.Contains(Program.Config.chtBanList))
                {
                    BanList.SelectedItem = Program.Config.chtBanList;
                }
            }

            Mode.SelectedIndexChanged += DuelModeChanged;

            ApplyTranslation();
        }

        public void ApplyTranslation()
        {
            if (Program.LanguageManager.Loaded)
            {
                groupBox1.Text = Program.LanguageManager.Translation.hostGb1;
                groupBox2.Text = Program.LanguageManager.Translation.hostGb2;
                label6.Text = Program.LanguageManager.Translation.hostTimeLimit;
                label5.Text = Program.LanguageManager.Translation.hostBanlist;
                label3.Text = Program.LanguageManager.Translation.hostRules;
                label4.Text = Program.LanguageManager.Translation.hostMode;
                Priority.Text = Program.LanguageManager.Translation.hostPrio;
                CheckDeck.Text = Program.LanguageManager.Translation.hostCheckDeck;
                ShuffleDeck.Text = Program.LanguageManager.Translation.hostShuffle;
                label1.Text = Program.LanguageManager.Translation.hostLifep;
                HostBtn.Text = Program.LanguageManager.Translation.hostBtnHost;
                CancelBtn.Text = Program.LanguageManager.Translation.hostBtnCancel;
                label2.Text = Program.LanguageManager.Translation.hostpassword;
            }
        }

        private void CardRulesChanged(object sender, EventArgs e)
        {
            if (CardRules.SelectedIndex >= 3)
            {
                BanList.SelectedIndex = 2;
                BanList.Enabled = false;
            }
            else
            {
                BanList.Enabled = true;
                BanList.SelectedItem = Program.Config.BanList;
            }
        }

        public void FormatChanged(object sender, EventArgs e)
        {
            if (CardRules.SelectedItem.ToString() == "TCG")
            {
                if (BanList.Items.Count > 0)
                    BanList.SelectedIndex = 0;
            }
            else
            {
                if (BanList.Items.Count > 1)
                    BanList.SelectedIndex = 1;
            }


        }

        private void DuelModeChanged(object sender, EventArgs e)
        {
            LifePoints.Text = (Mode.SelectedItem.ToString() == "Tag") ? "16000" : "8000";
        }

        public string GenerateURI(bool isranked)
        {
            string gamestring;
            if (CardRules.Text == "OCG")
                gamestring = "0";
            else if (CardRules.Text == "TCG")
                gamestring = "1";
            else if (CardRules.Text == "OCG/TCG")
                gamestring = "2";
            else if (CardRules.Text == "Anime")
                gamestring = "4";
            else if (CardRules.Text == "Turbo Duel")
                gamestring = "5";
            else
                gamestring = "3";
            if (Mode.Text == "Single")
                gamestring = gamestring + "0";
            else if (Mode.Text == "Match")
                gamestring = gamestring + "1";
            else
                gamestring = gamestring + "2";

            gamestring += (TimeLimit.SelectedIndex == -1 ? "0" : TimeLimit.SelectedIndex.ToString(CultureInfo.InvariantCulture));

            if ((Priority.Checked))
                gamestring = gamestring + "T";
            else
                gamestring = gamestring + "O";
            if ((CheckDeck.Checked))
                gamestring = gamestring + "T";
            else
                gamestring = gamestring + "O";
            if ((ShuffleDeck.Checked))
                gamestring = gamestring + "T";
            else
                gamestring = gamestring + "O";

            int banlistvalue = BanList.SelectedItem == null ? 0 : LauncherHelper.GetBanListValue(BanList.SelectedItem.ToString());

            gamestring = gamestring + LifePoints.Text + "," + banlistvalue + ",5,1," + (isranked ? "R" : "U") + (PasswordInput.Text == "" ? "" : "L") + "," + (PasswordInput.Text == "" ? GameName : PasswordInput.Text);

            return gamestring;
        }

        public string GenerateGameString(bool isranked)
        {
            string gamestring;
            if (CardRules.Text == "OCG")
                gamestring = "0";
            else if (CardRules.Text == "TCG")
                gamestring = "1";
            else if (CardRules.Text == "OCG/TCG")
                gamestring = "2";
            else if (CardRules.Text == "Anime")
                gamestring = "4";
            else if (CardRules.Text == "Turbo Duel")
                gamestring = "5";
            else
                gamestring = "3";
            if (Mode.Text == "Single")
                gamestring = gamestring + "0";
            else if (Mode.Text == "Match")
                gamestring = gamestring + "1";
            else
                gamestring = gamestring + "2";

            gamestring += (TimeLimit.SelectedIndex == -1 ? "0":TimeLimit.SelectedIndex.ToString(CultureInfo.InvariantCulture));

            if ((Priority.Checked))
                gamestring = gamestring + "T";
            else
                gamestring = gamestring + "O";
            if ((CheckDeck.Checked))
                gamestring = gamestring + "T";
            else
                gamestring = gamestring + "O";
            if ((ShuffleDeck.Checked))
                gamestring = gamestring + "T";
            else
                gamestring = gamestring + "O";

            int banlistvalue = BanList.SelectedItem == null ? 0 : LauncherHelper.GetBanListValue(BanList.SelectedItem.ToString());

            gamestring = gamestring + LifePoints.Text + "," + banlistvalue + ",5,1,"  + (isranked ? "R" : "U")+"L" + "," + GameName;

            return gamestring;
        }
    }
}
