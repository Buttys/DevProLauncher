using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevProLauncher.Network.Data;
using DevProLauncher.Network.Enums;
using DevProLauncher.Windows.MessageBoxs;
using DevProLauncher.Config;
using ServiceStack.Text;

namespace DevProLauncher.Windows
{
    public sealed partial class RankingFrm : UserControl
    {
        public delegate void ServerResponse(string message);
        public ServerResponse Ranking;
        public RankingFrm()
        {
            InitializeComponent();

            ApplyTranslation();

            Dock = DockStyle.Fill;
            Visible = true;

            SingleRankingListBox.DrawItem += GameListBox_DrawItem;
            MatchRankingListBox.DrawItem += GameListBox_DrawItem;

            SingleRankingListBox.DoubleClick += ShowProfile;
            MatchRankingListBox.DoubleClick += ShowProfile;
        }
        public void ApplyTranslation()
        {
            LanguageInfo info = Program.LanguageManager.Translation;

            SingleRankLbl.Text      = info.RankingRank;
            SingleUsernameLbl.Text  = info.RankingUsername;
            SingleEloLbl.Text       = info.RankingElo;
            SingleWinLbl.Text       = info.RankingWin;
            SingleLossLbl.Text      = info.RankingLoss;
            SingleDrawLbl.Text      = info.RankingDraw;
            SingleTeamLbl.Text      = info.RankingTeam;

            tableLayoutPanel5.Text      = info.RankingRank;
            MatchUsernameLbl.Text  = info.RankingUsername;
            MatchEloLbl.Text       = info.RankingElo;
            MatchWinLbl.Text       = info.RankingWin;
            MatchLossLbl.Text      = info.RankingLoss;
            MatchDrawLbl.Text      = info.RankingDraw;
            MatchTeamLbl.Text      = info.RankingTeam;

            loadBtn.Text = info.RankingLoadBtn;
        }
        private void GameListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var list = (ListBox)sender;
            e.DrawBackground();

            if (e.Index == -1)
                return;
            var index = e.Index;
            var parts = list.Items[index].ToString();
            var selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            var g = e.Graphics;

            string[] playerparts = parts.Split(',');
            string playername = playerparts[0];
            string playerelo = playerparts[1];
            string playerwins = playerparts[2];
            string playerloses = playerparts[3];
            string playerdraws = playerparts[4];
            string playerteam = playerparts[5];

            SolidBrush backgroundcolor;
            if (index % 2 == 0)
                backgroundcolor = new SolidBrush(Color.Orange);
            else
                backgroundcolor = new SolidBrush(Color.LightGray);

            var bounds = list.GetItemRectangle(index);
            var playersSize = e.Graphics.MeasureString(playername, e.Font);

            var offset = new Size(bounds.X+5, 5);
            var Nameoffset  = new Size((int)(bounds.Width * 0.1), 5);
            var Elooffset   = new Size((int)(bounds.Width * 0.3), 5);
            var Winoffset   = new Size((int)(bounds.Width * 0.45), 5);
            var Loseoffset  = new Size((int)(bounds.Width * 0.55), 5);
            var Drawoffset  = new Size((int)(bounds.Width * 0.65), 5);
            var Teamoffset  = new Size((int)(bounds.Width * 0.75), 5);

            //draw item
            g.FillRectangle(backgroundcolor, e.Bounds);
            g.DrawLines((selected) ? new Pen(Brushes.Purple, 5) : new Pen(Brushes.Black, 5),
                new[] { new Point(bounds.X, bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height), new Point(bounds.X, bounds.Y + bounds.Height), new Point(bounds.X, bounds.Y) });
            //Rank
            g.DrawString((index+1).ToString(), e.Font, Brushes.Black,
                list.GetItemRectangle(index).Location + offset);
            //Name
            g.DrawString(playername, e.Font, Brushes.Black,
                list.GetItemRectangle(index).Location + Nameoffset);
            //Elo
            g.DrawString(playerelo, e.Font, Brushes.Black,
                list.GetItemRectangle(index).Location + Elooffset);
            //Win
            g.DrawString(playerwins, e.Font, Brushes.Black,
                list.GetItemRectangle(index).Location + Winoffset);
            //Lose
            g.DrawString(playerloses, e.Font, Brushes.Black,
                list.GetItemRectangle(index).Location + Loseoffset);
            //Draw
            g.DrawString(playerdraws, e.Font, Brushes.Black,
                list.GetItemRectangle(index).Location + Drawoffset);
            //Team
            g.DrawString(playerteam, e.Font, Brushes.Black,
                list.GetItemRectangle(index).Location + Teamoffset);
            e.DrawFocusRectangle();
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            Program.ChatServer.Ranking += UpdateRanking;
            Program.ChatServer.SendPacket(DevServerPackets.GetRanking, JsonSerializer.SerializeToString(
            new RankingRequest(
                )));
        }

        private void UpdateRanking(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<String>(UpdateRanking), message);
                return;
            }

            string[] parts = message.Split('&');

            string[] singleparts = parts[0].Split('|');
            SingleRankingListBox.Items.Clear();

            for (int i = 0; i < singleparts.Length; i++)
            {
                //do stuff
                SingleRankingListBox.Items.Add(singleparts[i]);
            }

            string[] matchparts = parts[1].Split('|');
            MatchRankingListBox.Items.Clear();

            for (int i = 0; i < matchparts.Length; i++)
            {
                //do stuff
                MatchRankingListBox.Items.Add(matchparts[i]);
            }
        }
        public void ShowProfile(object sender, EventArgs e)
        {
            var player = (ListBox)sender;
            if (player.SelectedIndex == -1)
                return;

            string []parts = player.Text.Split(',');
            string name = parts[0];

            var profile = new ProfileFrm(name);
            profile.ShowDialog();
        }
    }
}
