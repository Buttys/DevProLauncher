using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YGOPro_Launcher.Chat
{
    public partial class TeamProfile_frm : Form
    {
        public TeamProfile_frm(string team)
        {
            InitializeComponent();
            TeamName.Text = "Team: " + team;
            Program.ChatServer.TeamStats += new ChatClient.ServerResponse(UpdateProfile);
            Program.ChatServer.SendPacket("TEAMSTATS||" + team);
        }

        private void UpdateProfile(string message)
        {
            if (this.IsDisposed)
                return;
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateProfile), message);
            }
            else
            {
                string[] rankedparts = message.Split(',');

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

                    SingleWLD.Text = rankedparts[28] + "/" + rankedparts[29] + "/" + rankedparts[30];
                    MatchWLD.Text = rankedparts[31] + "/" + rankedparts[32] + "/" + rankedparts[33];
                    TagWLD.Text = rankedparts[34] + "/" + rankedparts[35] + "/" + rankedparts[36];

                    TeamLevel.Text = "Lvl: " + rankedparts[37];
                    Rank.Text = "Rank: " + rankedparts[38];

                    int totalwins = Convert.ToInt32(rankedparts[28]) + Convert.ToInt32(rankedparts[31]) + Convert.ToInt32(rankedparts[34]);
                    int totalgames = Convert.ToInt32(rankedparts[28]) + Convert.ToInt32(rankedparts[29]) + Convert.ToInt32(rankedparts[30]) 
                        +Convert.ToInt32(rankedparts[31]) + Convert.ToInt32(rankedparts[32]) + Convert.ToInt32(rankedparts[33])
                        + Convert.ToInt32(rankedparts[34]) + Convert.ToInt32(rankedparts[35]) + Convert.ToInt32(rankedparts[36]);
                    if (totalgames != 0)
                    {
                        var ratio = (totalwins * 100) / totalgames;
                        Ratio.Text = "W/L Ratio: " + ratio.ToString("#.##") + "%";
                    }
                    else
                        Ratio.Text = "W/L Ratio: 0%";

                }
            }
        }
    }
}
