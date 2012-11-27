using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGOPro_Launcher.Config;

namespace YGOPro_Launcher
{
    public partial class Profile_frm : Form
    {
        public Profile_frm()
        {
            InitializeComponent();
            ApplyTranslation();
            Username.Text += Program.UserInfo.Username;
            wld.Text += Program.UserInfo.Wins + "/" + Program.UserInfo.Loses + "/" + Program.UserInfo.Draws;
            Program.ServerConnection.ProfileMessage += new NetClient.ServerResponse(ProfileUpdate);

        }

        public void ApplyTranslation()
        {
            LanguageInfo language = Program.LanguageManager.Translation;

            Username.Text = language.profileLblUsername;
            wld.Text = language.profileLblwld;
            rank.Text = language.profileLblRank;
            team.Text = language.profileLblTeam;
            groupBox1.Text = language.profileGb1;
            groupBox2.Text = language.profileGb2;
            groupBox3.Text = language.profileGb3;
            groupBox4.Text = language.profileGb4;
            groupBox5.Text = language.profileGb5;

            //unranked
            txtUWinLP0.Text = language.profileLblLP;
            txtUWinSurrendered.Text = language.profileLblSurrendered;
            txtUWin0Cards.Text = language.profileLbl0Cards;
            txtUWinTimeLimit.Text = language.profileLblTimeLimit;
            txtUWinRageQuit.Text = language.profileLblDisconnect;
            txtUWinExodia.Text = language.profileLblExodia;
            txtUWinCountdown.Text = language.profileLblFinalCountdown;
            txtUWinVennominaga.Text = language.profileLblVennominaga;
            txtUWinHorakhty.Text = language.profileLblHorakhty;
            txtUWinExodius.Text = language.profileLblExodius;
            txtUWinDestinyBoard.Text = language.profileLblDestinyBoard;
            txtUWinLastTurn.Text = language.profileLblLastTurn;
            txtUWinDestinyLeo.Text = language.profileLblDestinyLeo;
            txtUWinUnknown.Text = language.profileLblUnknown;

            txtULOSELP0.Text = language.profileLblLP;
            txtULOSESurrendered.Text = language.profileLblSurrendered;
            txtULOSE0Cards.Text = language.profileLbl0Cards;
            txtULOSETimeLimit.Text = language.profileLblTimeLimit;
            txtULOSERageQuit.Text = language.profileLblDisconnect;
            txtULOSEExodia.Text = language.profileLblExodia;
            txtULOSECountdown.Text = language.profileLblFinalCountdown;
            txtULOSEVennominaga.Text = language.profileLblVennominaga;
            txtULOSEHorakhty.Text = language.profileLblHorakhty;
            txtULOSEExodius.Text = language.profileLblExodius;
            txtULOSEDestinyBoard.Text = language.profileLblDestinyBoard;
            txtULOSELastTurn.Text = language.profileLblLastTurn;
            txtULOSEDestinyLeo.Text = language.profileLblDestinyLeo;
            txtULOSEUnknown.Text = language.profileLblUnknown;

            //ranked
            txtRWinLP0.Text = language.profileLblLP;
            txtRWinSurrendered.Text = language.profileLblSurrendered;
            txtRWin0Cards.Text = language.profileLbl0Cards;
            txtRWinTimeLimit.Text = language.profileLblTimeLimit;
            txtRWinRageQuit.Text = language.profileLblDisconnect;
            txtRWinExodia.Text = language.profileLblExodia;
            txtRWinCountdown.Text = language.profileLblFinalCountdown;
            txtRWinVennominaga.Text = language.profileLblVennominaga;
            txtRWinHorakhty.Text = language.profileLblHorakhty;
            txtRWinExodius.Text = language.profileLblExodius;
            txtRWinDestinyBoard.Text = language.profileLblDestinyBoard;
            txtRWinLastTurn.Text = language.profileLblLastTurn;
            txtRWinDestinyLeo.Text = language.profileLblDestinyLeo;
            txtRWinUnknown.Text = language.profileLblUnknown;

            txtRLOSELP0.Text = language.profileLblLP;
            txtRLOSESurrendered.Text = language.profileLblSurrendered;
            txtRLOSE0Cards.Text = language.profileLbl0Cards;
            txtRLOSETimeLimit.Text = language.profileLblTimeLimit;
            txtRLOSERageQuit.Text = language.profileLblDisconnect;
            txtRLOSEExodia.Text = language.profileLblExodia;
            txtRLOSECountdown.Text = language.profileLblFinalCountdown;
            txtRLOSEVennominaga.Text = language.profileLblVennominaga;
            txtRLOSEHorakhty.Text = language.profileLblHorakhty;
            txtRLOSEExodius.Text = language.profileLblExodius;
            txtRLOSEDestinyBoard.Text = language.profileLblDestinyBoard;
            txtRLOSELastTurn.Text = language.profileLblLastTurn;
            txtRLOSEDestinyLeo.Text = language.profileLblDestinyLeo;
            txtRLOSEUnknown.Text = language.profileLblUnknown;

        }

        private void ProfileUpdate(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(ProfileUpdate), message);
            }
            else
            {

                if (!IsDisposed)
                {
                    string[] sections = message.Split(new string[] {"||"}, StringSplitOptions.None);
                    rank.Text += sections[0];
                    team.Text += sections[1];
                    string[] unrankedparts = sections[2].Split(',');
                    string[] rankedparts = sections[3].Split(',');

                    if (unrankedparts[0] != "NotFound")
                    {
                        UWinLP0.Text = unrankedparts[0];
                        UWinSurrendered.Text = unrankedparts[1];
                        UWin0Cards.Text = unrankedparts[2];
                        UWinTimeLimit.Text = unrankedparts[3];
                        UWinRageQuit.Text = unrankedparts[4];
                        UWinExodia.Text = unrankedparts[5];
                        UWinCountdown.Text = unrankedparts[6];
                        UWinVennominaga.Text = unrankedparts[7];
                        UWinHorakhty.Text = unrankedparts[8];
                        UWinExodius.Text = unrankedparts[9];
                        UWinDestinyBoard.Text = unrankedparts[10];
                        UWinLastTurn.Text = unrankedparts[11];
                        UWinDestinyLeo.Text = unrankedparts[12];
                        UWinUnknown.Text = unrankedparts[13];

                        ULOSELP0.Text = unrankedparts[14];
                        ULOSESurrendered.Text = unrankedparts[15];
                        ULOSE0Cards.Text = unrankedparts[16];
                        ULOSETimeLimit.Text = unrankedparts[17];
                        ULOSERageQuit.Text = unrankedparts[18];
                        ULOSEExodia.Text = unrankedparts[19];
                        ULOSECountdown.Text = unrankedparts[20];
                        ULOSEVennominaga.Text = unrankedparts[21];
                        ULOSEHorakhty.Text = unrankedparts[22];
                        ULOSEExodius.Text = unrankedparts[23];
                        ULOSEDestinyBoard.Text = unrankedparts[24];
                        ULOSELastTurn.Text = unrankedparts[25];
                        ULOSEDestinyLeo.Text = unrankedparts[26];
                        ULOSEUnknown.Text = unrankedparts[27];
                    }

                    if (rankedparts[0] != "NotFound")
                    {
                        RWinLP0.Text = rankedparts[0];
                        RWinSurrendered.Text = rankedparts[1];
                        RWin0Cards.Text = rankedparts[2];
                        RWinTimeLimit.Text = rankedparts[3];
                        RWinRageQuit.Text = rankedparts[4];
                        RWinExodia.Text = rankedparts[5];
                        RWinCountdown.Text = rankedparts[6];
                        RWinVennominaga.Text = rankedparts[7];
                        RWinHorakhty.Text = rankedparts[8];
                        RWinExodius.Text = rankedparts[9];
                        RWinDestinyBoard.Text = rankedparts[10];
                        RWinLastTurn.Text = rankedparts[11];
                        RWinDestinyLeo.Text = rankedparts[12];
                        RWinUnknown.Text = rankedparts[13];

                        RLOSELP0.Text = rankedparts[14];
                        RLOSESurrendered.Text = rankedparts[15];
                        RLOSE0Cards.Text = rankedparts[16];
                        RLOSETimeLimit.Text = rankedparts[17];
                        RLOSERageQuit.Text = rankedparts[18];
                        RLOSEExodia.Text = rankedparts[19];
                        RLOSECountdown.Text = rankedparts[20];
                        RLOSEVennominaga.Text = rankedparts[21];
                        RLOSEHorakhty.Text = rankedparts[22];
                        RLOSEExodius.Text = rankedparts[23];
                        RLOSEDestinyBoard.Text = rankedparts[24];
                        RLOSELastTurn.Text = rankedparts[25];
                        RLOSEDestinyLeo.Text = rankedparts[26];
                        RLOSEUnknown.Text = rankedparts[27];
                    }

                }
            }
        }

        private void Profile_frm_Load(object sender, EventArgs e)
        {
            Program.ServerConnection.SendPacket("STATS");
        }
    }
}
