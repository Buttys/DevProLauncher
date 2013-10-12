using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevProLauncher.Config;
using DevProLauncher.Helpers;
using DevProLauncher.Network.Data;
using DevProLauncher.Network.Enums;
using DevProLauncher.Windows.MessageBoxs;
using ServiceStack.Text;

namespace DevProLauncher.Windows
{
    public sealed partial class HubGameList_frm : Form
    {

        private readonly Dictionary<string, RoomInfos> m_rooms = new Dictionary<string, RoomInfos>();
        private List<string> ServerList = new List<string>();
        private string checkmateusr, checkmatepass;

        public HubGameList_frm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;

            Format.SelectedIndex = 0;
            GameType.SelectedIndex = 0;
            BanList.SelectedIndex = 0;
            TimeLimit.SelectedIndex = 0;

            BanList.Items.AddRange(LauncherHelper.GetBanListArray());

            Program.ChatServer.AddRooms += OnRoomsList;
            Program.ChatServer.CreateRoom += OnRoomCreate;
            Program.ChatServer.RemoveRoom += OnRoomRemoved;
            Program.ChatServer.UpdateRoomStatus += OnRoomStarted;
            Program.ChatServer.UpdateRoomPlayers += OnRoomPlayersUpdate;
            Program.ChatServer.AddGameServer += AddServer;
            Program.ChatServer.RemoveGameServer += RemoveServer;
            RankedList.DrawItem += GameListBox_DrawItem;
            UnrankedList.DrawItem += GameListBox_DrawItem;
            UnrankedList.DoubleClick += LoadRoom;
            RankedList.DoubleClick += LoadRoom;

            SearchReset.Tick += ResetSearch;
            GameListUpdateTimer.Tick += UpdateGameListTimer;

            RefreshDeckList();
            LauncherHelper.DeckEditClosed += RefreshDeckList;
            DeckSelect.SelectedIndexChanged += DeckSelect_SelectedValueChanged;

            ApplyTranslation();

        }

        public void ApplyTranslation()
        {
            LanguageInfo info = Program.LanguageManager.Translation;

            groupBox1.Text = info.GameUnranked;
            groupBox3.Text = info.GameRanked;
            groupBox2.Text = info.GameSearch;
            label6.Text = info.GameDefualtDeck;
            label4.Text = info.GameFormat;
            label3.Text = info.GameType;
            label2.Text = info.GameBanList;
            label5.Text = info.GameTimeLimit;
            ActiveGames.Text = info.GameActive;
            IlligalGames.Text = info.GameIlligal;
            label1.Text = info.GameUserFilter;
            SearchRequest_Btn.Text = info.GameBtnSearch;
            Host_btn.Text = info.GameBtnHost;
            Quick_Btn.Text = info.GameBtnQuick;
            UpdateLabel.Text = info.GameNotUpdating;
        }

        public void RefreshDeckList()
        {

            if (InvokeRequired)
            {
                Invoke(new Action(RefreshDeckList));
            }
            else
            {
                DeckSelect.Items.Clear();
                if (Directory.Exists(Program.Config.LauncherDir + "deck/"))
                {
                    string[] decks = Directory.GetFiles(Program.Config.LauncherDir + "deck/");
                    foreach (string deck in decks)
                        // ReSharper disable AssignNullToNotNullAttribute
                        DeckSelect.Items.Add(Path.GetFileNameWithoutExtension(deck));
                    // ReSharper restore AssignNullToNotNullAttribute
                }
                DeckSelect.Text = Program.Config.DefaultDeck;
            }
        }

        private void DeckSelect_SelectedValueChanged(object sender, EventArgs e)
        {
            Program.Config.DefaultDeck = DeckSelect.SelectedItem.ToString();
            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
        }

        private void UpdateGameListTimer(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(UpdateGameListTimer), sender, e);
                return;
            }

            string[] parts = UpdateLabel.Text.Split(' ');
            int value = Int32.Parse(parts[parts.Length -2]);

            if (value == 0)
            {
                UpdateLabel.Text = Program.LanguageManager.Translation.GameNotUpdating;
                GameListUpdateTimer.Enabled = false;
                RankedList.Items.Clear();
                UnrankedList.Items.Clear();
            }
            else
            {
                UpdateLabel.Text = Program.LanguageManager.Translation.GameUpdating1 + (value -1) + Program.LanguageManager.Translation.GameUpdating2;
            }
        }

        private void ResetSearch(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(ResetSearch), sender, e);
                return;
            }

            if (SearchRequest_Btn.Text == "1")
            {
                SearchRequest_Btn.Enabled = true;
                SearchRequest_Btn.Text = Program.LanguageManager.Translation.GameBtnSearch;
                SearchReset.Enabled = false;
            }
            else
            {
                int value = Int32.Parse(SearchRequest_Btn.Text);
                SearchRequest_Btn.Text = (value - 1).ToString(CultureInfo.InvariantCulture);
            }
        }

        private void AddServer(string server)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddServer), server);
                return;
            }
            if (!ServerList.Contains(server))
                ServerList.Add(server);

        }

        private void RemoveServer(string server)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(RemoveServer), server);
                return;
            }

            if (ServerList.Contains(server))
            {
                ServerList.Remove(server);
                RemoveServerRooms(server);
            }
        }

        private void RemoveServerRooms(string server)
        {
            List<string> removerooms = new List<string>();

            foreach (string room in m_rooms.Keys)
            {
                if (m_rooms.ContainsKey(room))
                {
                    if (m_rooms[room].server == server)
                        removerooms.Add(room);
                }
            }

            foreach (string removeroom in removerooms)
            {
                if (m_rooms.ContainsKey(removeroom))
                    m_rooms.Remove(removeroom);
            }

            OnRoomsList(m_rooms.Values.ToArray());

        }

        public void OnRoomsList(RoomInfos[] rooms)
        {
            Invoke(new Action<RoomInfos[]>(InternalRoomsList), new object[] { rooms });
        }

        private void InternalRoomsList(RoomInfos[] rooms)
        {
            m_rooms.Clear();
            UnrankedList.Items.Clear();
            RankedList.Items.Clear();
            foreach (RoomInfos room in rooms)
            {
                InternalRoomCreated(room);
            }
            UpdateLabel.Text = Program.LanguageManager.Translation.GameUpdating1 + 60 + Program.LanguageManager.Translation.GameUpdating2;
            GameListUpdateTimer.Enabled = true;
        }

        public void OnRoomCreated(RoomInfos[] room)
        {
            Invoke(new Action<RoomInfos>(InternalRoomCreated), room[0]);
        }

        private void InternalRoomCreated(RoomInfos room)
        {
            string roomname = room.GetRoomName();
            if (m_rooms.ContainsKey(roomname))
                return;
            m_rooms.Add(roomname, room);
            ListBox rooms = (room.isRanked ? RankedList : UnrankedList);
            
            rooms.Items.Add(roomname);
        }

        public List<object> ObjectKeys()
        {
            return m_rooms.Keys.Cast<object>().ToList();
        }

        public void OnRoomStarted(string roomname)
        {
            Invoke(new Action<string>(InternalRoomStarted), roomname);
        }

        private void InternalRoomStarted(string roomname)
        {
            if (!m_rooms.ContainsKey(roomname)) return;

            RoomInfos item = m_rooms[roomname];
            ListBox rooms = (item.isRanked ? RankedList : UnrankedList);
            item.hasStarted = true;
            if(!ActiveGames.Checked)
                rooms.Items.Remove(roomname);
        }

        public void OnRoomRemoved(string roomname)
        {
            Invoke(new Action<string>(InternalRoomRemoved), roomname);
        }

        private void InternalRoomRemoved(string roomname)
        {
            if (!m_rooms.ContainsKey(roomname)) return;
            RoomInfos room = m_rooms[roomname];
            if (room.isRanked)
                RankedList.Items.Remove(roomname);
            else
                UnrankedList.Items.Remove(roomname);
            m_rooms.Remove(roomname);
        }

        public void OnRoomCreate(RoomInfos room)
        {
            if (!m_rooms.ContainsKey(room.GetRoomName()))
            {
                Invoke(new Action<RoomInfos>(InternalRoomCreated), room);
            }
        }

        public void OnRoomPlayersUpdate(PacketCommand data)
        {
            if (m_rooms.ContainsKey(data.Command))
            {
                Invoke(new Action<string, string[]>(InternalRoomPlayersUpdate), data.Command, ((object)data.Data.Split(',')));
            }
        }

        private void InternalRoomPlayersUpdate(string room, string[] data)
        {
            string roomname = room;
            if (!m_rooms.ContainsKey(roomname)) return;
            RoomInfos item = m_rooms[roomname];

            item.playerList = data;

            if (item.isRanked)
                RankedList.UpdateList();
            else
                UnrankedList.UpdateList();
        }

        private void SearchRequest_Btn_Click(object sender, EventArgs e)
        {
            Program.ChatServer.SendPacket(DevServerPackets.GameList, JsonSerializer.SerializeToString(
                new SearchRequest(
                    (Format.SelectedIndex == -1 ? Format.SelectedIndex : Format.SelectedIndex-1),
                    (GameType.SelectedIndex == -1 ? GameType.SelectedIndex : GameType.SelectedIndex-1),
                    (BanList.SelectedIndex == -1 ? BanList.SelectedIndex : BanList.SelectedIndex-1),
                    (TimeLimit.SelectedIndex == -1 ? TimeLimit.SelectedIndex : TimeLimit.SelectedIndex-1),
                    ActiveGames.Checked, IlligalGames.Checked, lockedChk.Checked, UserFilter.Text
                    )));
            SearchRequest_Btn.Enabled = false;
            SearchRequest_Btn.Text = "5";
            SearchReset.Enabled = true;
        }

        private void Host_btn_Click(object sender, EventArgs e)
        {
            HostBtn_MouseUp(sender, new MouseEventArgs(MouseButtons.Right, 1, 1, 1, 1));
        }

        private void HostGame(object sender, EventArgs e)
        {
            var button = (ToolStripMenuItem)sender;
            var form = new Host(false, (button.Name == "Ranked"));
            if (button.Name == "Ranked")
            {
                form.Mode.Items.Clear();
                form.Mode.Items.AddRange(new object[] { "Single", "Match", "Tag" });
                form.Mode.SelectedItem = "Match";
                if (form.BanList.Items.Count > 0)
                    form.BanList.SelectedIndex = 0;
                form.CardRules.SelectedIndexChanged += form.FormatChanged;
                form.BanList.Enabled = false;
                form.Priority.Enabled = false;
                form.ShuffleDeck.Enabled = false;
                form.CheckDeck.Enabled = false;
                form.LifePoints.Enabled = false;
                form.CardRules.Items.Clear();
                form.CardRules.Items.AddRange(new object[] { "TCG", "OCG" });
                form.CardRules.SelectedItem = "TCG";
            }

            if (form.ShowDialog() == DialogResult.OK)
            {

                if (m_rooms.ContainsKey(form.PasswordInput.Text))
                {
                    MessageBox.Show(Program.LanguageManager.Translation.GamePasswordExsists);
                    return;
                }
                if (Program.ServerList.Count == 0)
                {
                    MessageBox.Show(Program.LanguageManager.Translation.GameNoServers);
                    return;
                }

                LauncherHelper.GenerateConfig(GetServer(), form.GenerateURI((button.Name == "Ranked")));
                LauncherHelper.RunGame("-j");
            }
        }

        private void QuickBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || sender is Button)
            {
                var mnu = new ContextMenuStrip();
                var mnuRanked = new ToolStripMenuItem("Ranked");
                var mnuUnRanked = new ToolStripMenuItem("UnRanked");
                var mnuSingle = new ToolStripMenuItem("Single") { Name = "Single" };
                var mnuMatch = new ToolStripMenuItem("Match") { Name = "Match" };
                var mnuTag = new ToolStripMenuItem("Tag") { Name = "Tag" };
                var mnuRSingle = new ToolStripMenuItem("Single") { Name = "RSingle" };
                var mnuRMatch = new ToolStripMenuItem("Match") { Name = "RMatch" };
                var mnuRTag = new ToolStripMenuItem("Tag") { Name = "RTag" };

                mnuRanked.DropDownItems.AddRange(new ToolStripItem[]{ mnuRSingle,mnuRMatch,mnuRTag});
                mnuRanked.DropDownDirection = ToolStripDropDownDirection.Right;
                mnuUnRanked.DropDownItems.AddRange(new ToolStripItem[] { mnuSingle, mnuMatch, mnuTag });
                mnuUnRanked.DropDownDirection = ToolStripDropDownDirection.Right;

                mnuSingle.Click += QuickHostItem_Click;
                mnuMatch.Click += QuickHostItem_Click;
                mnuTag.Click += QuickHostItem_Click;
                mnuRSingle.Click += QuickHostItem_Click;
                mnuRMatch.Click += QuickHostItem_Click;
                mnuRTag.Click += QuickHostItem_Click;



                mnu.Items.AddRange(new ToolStripItem[] {mnuRanked, mnuUnRanked});

                mnu.DefaultDropDownDirection = ToolStripDropDownDirection.BelowRight;
                mnu.Show((Button)sender, new Point(0, 0 - mnu.Height));
            }
        }

        private void QuickHostItem_Click(object sender, EventArgs e)
        {
            var button = (ToolStripItem)sender;
            QuickHost((button.Name.StartsWith("R")) ? button.Name.Substring(1) : button.Name, (button.Name.StartsWith("R")));
        }

        private void QuickHost(string mode, bool isranked)
        {
            var ran = new Random();
            var form = new Host(false, false)
            {
                CardRules = { Text = Program.Config.CardRules },
                Mode = { Text = mode },
                Priority = { Checked = Program.Config.EnablePrority },
                CheckDeck = { Checked = Program.Config.DisableCheckDeck },
                ShuffleDeck = { Checked = Program.Config.DisableShuffleDeck },
                LifePoints = { Text = Program.Config.Lifepoints },
                GameName = LauncherHelper.GenerateString().Substring(0, 5),
                BanList = { SelectedItem = Program.Config.BanList },
                TimeLimit = { SelectedItem = Program.Config.TimeLimit }
            };

            ListBox list = (isranked) ? RankedList : UnrankedList;

            if (isranked)
            {
                form.BanList.SelectedIndex = 0;
                form.CheckDeck.Checked = false;
                form.ShuffleDeck.Checked = false;
                form.Priority.Checked = false;
                form.CardRules.SelectedIndex = 2;
                form.LifePoints.Text = form.Mode.Text == "Tag" ? "16000" : "8000";
            }
            else
            {
                if (Program.Config.Lifepoints != ((mode == "Tag") ? "16000" : "8000"))
                {
                    if (MessageBox.Show(Program.LanguageManager.Translation.GameLPChange, Program.LanguageManager.Translation.hostLifep, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        form.LifePoints.Text = mode == "Tag" ? "16000" : "8000";

                    }
                }
            }

            RoomInfos userinfo = RoomInfos.FromName(form.GenerateURI(isranked));

            var matchedRooms = (from object room in list.Items where m_rooms.ContainsKey(room.ToString()) select m_rooms[room.ToString()] into info where RoomInfos.CompareRoomInfo(userinfo, info) select info).ToList();
            string server = string.Empty;
            if (matchedRooms.Count > 0)
            {
                var selectroom = ran.Next(matchedRooms.Count);
                form.GameName = matchedRooms[selectroom].roomName;
                server = matchedRooms[selectroom].server;
            }

            if (string.IsNullOrEmpty(server))
            {
                LauncherHelper.GenerateConfig(GetServer(), form.GenerateURI(isranked));
                LauncherHelper.RunGame("-j");
            }
            else
            {
                LauncherHelper.GenerateConfig(Program.ServerList[server], form.GenerateURI(isranked));
                LauncherHelper.RunGame("-j");
            }

        }

        private void HostBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || sender is Button)
            {
                var mnu = new ContextMenuStrip();
                var mnuranked = new ToolStripMenuItem("Ranked");
                mnuranked.Name = "Ranked";
                var mnuunranked = new ToolStripMenuItem("Unranked");

                mnuranked.Click += HostGame;
                mnuunranked.Click += HostGame;

                mnu.Items.AddRange(new ToolStripItem[]
                {
                    mnuunranked,
                    mnuranked
                });

                mnu.DefaultDropDownDirection = ToolStripDropDownDirection.BelowRight;
                mnu.Show((Button)sender, new Point(0, 0 - mnu.Height));
            }
        }

        public void LoadRoom(object sender, EventArgs e)
        {
            var rooms = (ListBox)sender;
            if (rooms.SelectedIndex == -1)
                return;
            if (!m_rooms.ContainsKey(rooms.SelectedItem.ToString()))
                return;

            RoomInfos item = m_rooms[rooms.SelectedItem.ToString()];
            if (item.isLocked)
            {
                var form = new InputFrm(string.Empty, Program.LanguageManager.Translation.GameEnterPassword, Program.LanguageManager.Translation.QuickHostBtn, Program.LanguageManager.Translation.optionBtnCancel)
                {
                    InputBox = { MaxLength = 4 }
                };
                if (!item.hasStarted)
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (form.InputBox.Text != item.roomName)
                        {
                            MessageBox.Show(Program.LanguageManager.Translation.GameWrongPassword);
                            return;
                        }
                    }
                    else
                        return;
                }
            }

            if (Program.ServerList.ContainsKey(item.server))
            {
                LauncherHelper.GenerateConfig(Program.ServerList[item.server], item.ToName());
                LauncherHelper.RunGame("-j");
            }
        }

        public ServerInfo GetServer()
        {
            if (Program.ServerList.Count == 0)
                return null;

            ServerInfo server;
            int serverselect = Program.Rand.Next(0, ServerList.Count);

            if (Program.ServerList.ContainsKey(ServerList[serverselect]))
                server = Program.ServerList[ServerList[serverselect]];
            else
            {
                MessageBox.Show(Program.LanguageManager.Translation.GameNoServers);
                return null;
            }


            return server;
        }

        private void GameListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var list = (ListBox)sender;
            e.DrawBackground();

            if (e.Index == -1)
                return;
            var index = e.Index;
            var room = list.Items[index].ToString();
            var selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            var g = e.Graphics;
            RoomInfos info = null;
            if (m_rooms.ContainsKey(room))
                info = m_rooms[room];

            //item info

            string playerstring;

            if (info == null)
            {
                playerstring = "??? vs ???";
            }
            else
            {
                bool istag = (info.mode == 2);
                string[] players = info.playerList;

                if (players.Length == 0)
                {
                    playerstring = "??? vs ???";
                }
                else
                {
                    if (istag)
                    {
                        string player1 = players[0].Trim();
                        string player2 = (players.Length > 1) ? players[1].Trim() : "???";
                        string player3 = (players.Length > 2) ? players[2].Trim() : "???";
                        string player4 = (players.Length > 3) ? players[3].Trim() : "???";
                        playerstring = player1 + ", " + player2 + " vs " + player3 + ", " + player4;
                    }
                    else
                    {
                        string player1 = players[0].Trim();
                        string player2 = (players.Length > 1) ? players[1].Trim() : "???";

                        playerstring = player1 + " vs " + player2;
                    }
                }

            }


            var bounds = list.GetItemRectangle(index);
            var rulesize = e.Graphics.MeasureString((info == null) ? "???" : RoomInfos.GameRule(info.rule), e.Font);
            var playersSize = e.Graphics.MeasureString(playerstring, e.Font);
            var lockedsize = e.Graphics.MeasureString((info == null) ? "???" : (info.isLocked ? Program.LanguageManager.Translation.GameLocked : Program.LanguageManager.Translation.GameOpen), e.Font);
            SolidBrush backgroundcolor;

            var offset = new Size(5, 5);

            if (info == null)
            {
                backgroundcolor = new SolidBrush(Color.Red);
            }
            else
            {
                backgroundcolor = new SolidBrush(info.hasStarted ? Color.LightGray :
                (info.isIllegal ? Color.LightCoral :
                (info.rule == 4 ? Color.Violet :
                (info.rule == 5 ? Color.Gold :
                (info.mode == 2 ? Color.LightGreen :
                (info.mode == 1 ? Color.LightSteelBlue :
                Color.LightBlue))))));
            }

            //draw item
            g.FillRectangle(backgroundcolor, e.Bounds);
            g.DrawLines((selected) ? new Pen(Brushes.Purple, 5) : new Pen(Brushes.Black, 5),
                new[] { new Point(bounds.X, bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height), new Point(bounds.X, bounds.Y + bounds.Height), new Point(bounds.X, bounds.Y) });
            //toplet
            g.DrawString((info == null) ? "???/???/???" : RoomInfos.GameMode(info.mode) + " / " + LauncherHelper.GetBanListFromInt(info.banListType) + " / " + (info.timer == 0 ? "3 mins" : "5 mins"), e.Font, Brushes.Black,
                list.GetItemRectangle(index).Location + offset);
            //topright
            g.DrawString((info == null) ? "???" : RoomInfos.GameRule(info.rule), e.Font, Brushes.Black,
                new Rectangle(bounds.X + (bounds.Width - (int)rulesize.Width) - offset.Width, bounds.Y + offset.Height, bounds.Width, bounds.Height));
            ////bottomright
            g.DrawString((info == null) ? "???" : (info.isLocked ? Program.LanguageManager.Translation.GameLocked : Program.LanguageManager.Translation.GameOpen),
                e.Font, Brushes.Black,
                new Rectangle(bounds.X + (bounds.Width - (int)lockedsize.Width) - offset.Width, bounds.Y + (bounds.Height - (int)lockedsize.Height) - offset.Height, bounds.Width, bounds.Height));
            //bottom center
            g.DrawString(playerstring, e.Font, Brushes.Black,
                new Rectangle(bounds.X + ((bounds.Width / 2) - ((int)playersSize.Width / 2)), bounds.Y + (bounds.Height - (int)playersSize.Height) - offset.Height, bounds.Width, bounds.Height));
            e.DrawFocusRectangle();
        }

        private void Quick_Btn_Click(object sender, EventArgs e)
        {
            QuickBtn_MouseUp(sender, new MouseEventArgs(MouseButtons.Right, 1, 1, 1, 1));
        }

        private void chkmate_btn_Click(object sender, EventArgs e)
        {
            Checkmate_frm form = new Checkmate_frm(string.IsNullOrEmpty(checkmateusr) ? Program.UserInfo.username:checkmateusr,
                string.IsNullOrEmpty(checkmatepass) ? "":checkmatepass);
            if (form.ShowDialog() == DialogResult.OK)
            {
                checkmateusr = form.Username.Text;
                checkmatepass = form.Password.Text;
                LauncherHelper.GenerateCheckmateConfig(Program.Checkmate, checkmateusr,checkmatepass);
                LauncherHelper.RunGame("-j");
            }
        }

    }
}
