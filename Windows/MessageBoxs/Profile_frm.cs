using System;
using System.Windows.Forms;
using DevProLauncher.Config;
using DevProLauncher.Network.Enums;

namespace DevProLauncher.Windows.MessageBoxs
{
    public partial class ProfileFrm : Form
    {
        private readonly string m_profileUsername;

        public ProfileFrm(string userName)
        {
            m_profileUsername = userName;
            InitializeComponent();
            ApplyTranslation();
            Username.Text += userName;
            Program.ChatServer.UserStats += UpdatePlayerProfile;
            FormClosed += OnClose;
        }
        public void OnClose(object sender, EventArgs e)
        {
            if (Program.ChatServer.UserStats != null) 
                Program.ChatServer.UserStats -= UpdatePlayerProfile;
        }

        public void ApplyTranslation()
        {
            LanguageInfo language = Program.LanguageManager.Translation;

            Text = language.profileName;
            Username.Text = language.profileLblUsername;
            rank.Text = language.profileLblRank;
            UserLevel.Text = language.profileLvl; 
            singlerank.Text = language.profileLblSingleRank;
            team.Text = language.profileLblTeam;
            elo.Text = language.profileLblElo;
            singleelo.Text = language.profileLblSingleElo;
            groupBox4.Text = language.profileLblwld;
            groupBox2.Text = language.profileGb2;
            groupBox3.Text = language.profileGb3;
            groupBox5.Text = language.profileGb4;
            groupBox6.Text = language.profileGb5;
            groupBox1.Text = language.profileGb1;
            rwin.Text = language.profileWin;
            uwin.Text = language.profileWin;
            rlose.Text = language.profileLose;
            ulose.Text = language.profileLose;

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

            // team
            groupBox7.Text = language.profileGb2;
            groupBox8.Text = language.profileGb3;
            groupBox9.Text = language.profileGb4;
            Ratio.Text = language.profileTRatio;
            TeamLevel.Text = language.profileLvl;
            TRank.Text = language.profileLblTRank;

            txtTWinLP0.Text = language.profileLblLP;
            txtTWinSurrendered.Text = language.profileLblSurrendered;
            txtTWin0Cards.Text = language.profileLbl0Cards;
            txtTWinTimeLimit.Text = language.profileLblTimeLimit;
            txtTWinRageQuit.Text = language.profileLblDisconnect;
            txtTWinExodia.Text = language.profileLblExodia;
            txtTWinCountdown.Text = language.profileLblFinalCountdown;
            txtTWinVennominaga.Text = language.profileLblVennominaga;
            txtTWinHorakhty.Text = language.profileLblHorakhty;
            txtTWinExodius.Text = language.profileLblExodius;
            txtTWinDestinyBoard.Text = language.profileLblDestinyBoard;
            txtTWinLastTurn.Text = language.profileLblLastTurn;
            txtTWinDestinyLeo.Text = language.profileLblDestinyLeo;
            txtTWinUnknown.Text = language.profileLblUnknown;

        }

        private void UpdatePlayerProfile(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdatePlayerProfile), message);
            }
            else
            {
                if (!IsDisposed)
                {
                    string[] sections = message.Split(new[] {"||"}, StringSplitOptions.None);
                    rank.Text += sections[0];
                    singlerank.Text += sections[1];
                    UserLevel.Text +=  sections[6];
                    elo.Text += sections[7];
                    singleelo.Text += sections[8]; 
                    if (sections[2] == "not found")
                        MatchWLD.Text = "0/0/0";
                    else
                    {
                        string[] values = sections[2].Split(',');
                        MatchWLD.Text = values[0] + "/" + values[1] + "/" + values[2];
                        SingleWLD.Text = values[3] + "/" + values[4] + "/" + values[5];
                        TagWLD.Text = values[6] + "/" + values[7] + "/" + values[8];
                    }

                    string teamName = sections[3];
                    team.Text += teamName;

                    if (teamName.Equals("None"))
                    {
                        ((Control)tabControl1.TabPages[1]).Enabled = false;
                    }
                    else
                    {
                        TeamName.Text = "Team: " + teamName;
                        Program.ChatServer.TeamStats += UpdateTeamProfile;
                        Program.ChatServer.SendPacket(DevServerPackets.TeamStats, teamName);
                    }

                    string[] unrankedparts = sections[4].Split(',');
                    string[] rankedparts = sections[5].Split(',');

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

        private void UpdateTeamProfile(string message)
        {
            if (IsDisposed)
                return;
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateTeamProfile), message);
            }
            else
            {
                string[] rankedparts = message.Split(',');

                if (rankedparts[0] != "NotFound")
                {
                    TeamName.Text += team.Text;

                    TWinLP0.Text = rankedparts[0];
                    TWinSurrendered.Text = rankedparts[1];
                    TWin0Cards.Text = rankedparts[2];
                    TWinTimeLimit.Text = rankedparts[3];
                    TWinRageQuit.Text = rankedparts[4];
                    TWinExodia.Text = rankedparts[5];
                    TWinCountdown.Text = rankedparts[6];
                    TWinVennominaga.Text = rankedparts[7];
                    TWinHorakhty.Text = rankedparts[8];
                    TWinExodius.Text = rankedparts[9];
                    TWinDestinyBoard.Text = rankedparts[10];
                    TWinLastTurn.Text = rankedparts[11];
                    TWinDestinyLeo.Text = rankedparts[12];
                    TWinUnknown.Text = rankedparts[13];

                    TLOSELP0.Text = rankedparts[14];
                    TLOSESurrendered.Text = rankedparts[15];
                    TLOSE0Cards.Text = rankedparts[16];
                    TLOSETimeLimit.Text = rankedparts[17];
                    TLOSERageQuit.Text = rankedparts[18];
                    TLOSEExodia.Text = rankedparts[19];
                    TLOSECountdown.Text = rankedparts[20];
                    TLOSEVennominaga.Text = rankedparts[21];
                    TLOSEHorakhty.Text = rankedparts[22];
                    TLOSEExodius.Text = rankedparts[23];
                    TLOSEDestinyBoard.Text = rankedparts[24];
                    TLOSELastTurn.Text = rankedparts[25];
                    TLOSEDestinyLeo.Text = rankedparts[26];
                    TLOSEUnknown.Text = rankedparts[27];

                    TSingleWLD.Text = rankedparts[28] + "/" + rankedparts[29] + "/" + rankedparts[30];
                    TMatchWLD.Text = rankedparts[31] + "/" + rankedparts[32] + "/" + rankedparts[33];
                    TTagWLD.Text = rankedparts[34] + "/" + rankedparts[35] + "/" + rankedparts[36];

                    TeamLevel.Text +=  rankedparts[37];
                    TRank.Text += rankedparts[38];

                    int totalwins = Convert.ToInt32(rankedparts[28]) + Convert.ToInt32(rankedparts[31]) + Convert.ToInt32(rankedparts[34]);
                    int totalgames = Convert.ToInt32(rankedparts[28]) + Convert.ToInt32(rankedparts[29]) + Convert.ToInt32(rankedparts[30])
                        + Convert.ToInt32(rankedparts[31]) + Convert.ToInt32(rankedparts[32]) + Convert.ToInt32(rankedparts[33])
                        + Convert.ToInt32(rankedparts[34]) + Convert.ToInt32(rankedparts[35]) + Convert.ToInt32(rankedparts[36]);
                    if (totalgames != 0)
                    {
                        var ratio = (totalwins * 100) / totalgames;
                        Ratio.Text += ratio.ToString("#.##") + "%";
                    }
                    else
                        Ratio.Text +="0%";

                }
            }
        }

        private void Profile_frm_Load(object sender, EventArgs e)
        {
            Program.ChatServer.SendPacket(DevServerPackets.Stats, m_profileUsername);
        }
    }
}
