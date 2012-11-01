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
    public partial class Host : Form
    {
        private Language lang = new Language();

        public Host()
        {
            InitializeComponent();
            CardRules.SelectedIndex = 0;
            Mode.SelectedIndex = 0;
            GameName.Text = LauncherHelper.GenerateString().Substring(0, 5);
            Mode.SelectedIndexChanged += DuelModeChanged;
            lang.Load(Program.Config.language + ".conf");
            newText();
        }

        private void newText()
        {
            groupBox1.Text = lang.hostGb1;
            groupBox2.Text = lang.hostGb2;
            label3.Text = lang.hostRules;
            label4.Text = lang.hostMode;
            Priority.Text = lang.hostPrio;
            CheckDeck.Text = lang.hostCheckDeck;
            ShuffleDeck.Text = lang.hostShuffle;
            label1.Text = lang.hostLifep;
            label2.Text = lang.hostGameN;
            HostBtn.Text = lang.hostBtnHost;
            CancelBtn.Text = lang.hostBtnCancel;
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

        private void HostBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
