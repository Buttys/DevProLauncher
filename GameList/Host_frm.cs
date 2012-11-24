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
    public partial class Host : Form
    {
        private Dictionary<string, int> Banlists = new Dictionary<string,int>();

        public Host(bool options)
        {
            InitializeComponent();
            LoadBanlist();
            if (options)
            {
                TimeLimit.SelectedItem = Program.Config.TimeLimit;
                BanList.SelectedItem = Program.Config.BanList;
                Mode.SelectedItem = Program.Config.Mode;
                GameName.Text = Program.Config.GameName;
                CardRules.SelectedItem = Program.Config.CardRules;
                Priority.Checked = Program.Config.EnablePrority;
                ShuffleDeck.Checked = Program.Config.DisableShuffleDeck;
                CheckDeck.Checked = Program.Config.DisableCheckDeck;
            }
            else
            {
                TimeLimit.SelectedIndex = 0;
                CardRules.SelectedIndex = 0;
                Mode.SelectedIndex = 0;
                GameName.Text = LauncherHelper.GenerateString().Substring(0, 5);
                BanList.SelectedIndex = 0;
            }
            Mode.SelectedIndexChanged += DuelModeChanged;
            CardRules.SelectedIndexChanged += new EventHandler(CardRulesChanged);
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
                label2.Text = Program.LanguageManager.Translation.hostGameN;
                HostBtn.Text = Program.LanguageManager.Translation.hostBtnHost;
                CancelBtn.Text = Program.LanguageManager.Translation.hostBtnCancel;
            }
        }

        public void LoadBanlist()
        {
            if(!File.Exists(Program.Config.LauncherDir + "lflist.conf"))
                return;

            BanList.Items.Clear();
            var lines = File.ReadAllLines(Program.Config.LauncherDir +"lflist.conf");

            foreach (string nonTrimmerLine in lines)
            {
                string line = nonTrimmerLine.Trim();
                if (line.StartsWith("!"))
                {
                    Banlists.Add(line.Substring(1),Banlists.Count);
                    BanList.Items.Add(line.Substring(1));
                }
            }
        }

        private void CardRulesChanged(object sender, EventArgs e)
        {
            if (CardRules.SelectedIndex >= 3)
            {
                BanList.SelectedIndex = 1;
                BanList.Enabled = false;
            }
            else
            {
                BanList.Enabled = true;
                BanList.SelectedItem = Program.Config.BanList;
            }
        }

        private void DuelModeChanged(object sender, EventArgs e)
        {
            if ((Mode.SelectedIndex == 1))
                LifePoints.Text = "16000";
            if ((Mode.SelectedIndex == 0))
                LifePoints.Text = "8000";
            if ((Mode.SelectedIndex == 2))
                LifePoints.Text = "8000";
        }

        public string GenerateURI(string server, string port, bool isranked)
        {
            string gamestring = null;
            if ((this.CardRules.Text == "OCG"))
                gamestring = "0";
            else if ((this.CardRules.Text == "TCG"))
                gamestring = "1";
            else if ((this.CardRules.Text == "OCG/TCG"))
                gamestring = "2";
            else if ((this.CardRules.Text == "Anime"))
                gamestring = "4";
            else if ((this.CardRules.Text == "Turbo Duel"))
                gamestring = "5";
            else
                gamestring = "3";
            if ((this.Mode.Text == "Single"))
                gamestring = gamestring + "0";
            else if ((this.Mode.Text == "Match"))
                gamestring = gamestring + "1";
            else
                gamestring = gamestring + "2";

            gamestring += Banlists[BanList.SelectedItem.ToString()];

            gamestring += TimeLimit.SelectedIndex;

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

            gamestring = gamestring + LifePoints.Text + "," + (isranked ? "R" : "U") + "," + GameName.Text;

            return "ygopro:/" + server + "/" + port + "/" + gamestring;
        }
    }
}
