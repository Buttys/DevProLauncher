using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using DevProLauncher.Network.Data;
using DevProLauncher.Helpers;
using DevProLauncher.Windows.MessageBoxs;

namespace DevProLauncher.Windows
{
    public sealed partial class GameListFrm : Form
    {

        private readonly Dictionary<string, RoomInfos> m_rooms;

        public GameListFrm(string serverName)
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            Name = serverName;
            Text = serverName;

            m_rooms = new Dictionary<string, RoomInfos>();
            FilterActive.CheckedChanged += FilterGames;
            FilterTextBox.TextChanged += FilterGames;
            Program.ChatServer.AddRooms += OnRoomsList;
            Program.ChatServer.CreateRoom += OnRoomCreate;
            Program.ChatServer.RemoveRoom += OnRoomRemoved;
            Program.ChatServer.UpdateRoomStatus += OnRoomStarted;
            Program.ChatServer.UpdateRoomPlayers += OnRoomPlayersUpdate;
            Program.ChatServer.AddGameServer += AddServer;
            Program.ChatServer.RemoveGameServer += RemoveServer;

            LauncherHelper.DeckEditClosed += RefreshDeckList;
            RankedList.DrawItem += GameListBox_DrawItem;
            UnrankedList.DrawItem += GameListBox_DrawItem;
            UnrankedList.DoubleClick += LoadRoom;
            RankedList.DoubleClick += LoadRoom;
            RefreshDeckList();
            DeckSelect.SelectedIndexChanged += DeckSelect_SelectedValueChanged;

            ApplyTranslation();

            QuickBtn.MouseUp += QuickBtn_MouseUp;
            RankedQuickBtn.MouseUp += QuickBtn_MouseUp;
            ServerList.SelectedIndex = 0;
        }

        public void ApplyTranslation()
        {
            groupBox1.Text = Program.LanguageManager.Translation.GameServerInfo;

            label1.Text = "# " + Program.LanguageManager.Translation.GameofRooms;
            label2.Text = "# " + Program.LanguageManager.Translation.GameofUnranked;
            label3.Text = "# " + Program.LanguageManager.Translation.GameofRanked;
            label5.Text = "# " + Program.LanguageManager.Translation.GameofOpenRooms;
            label4.Text = "# " + Program.LanguageManager.Translation.GameofPlayers;

            FilterActive.Text = Program.LanguageManager.Translation.GameFilterActive;

            DeckBtn.Text = Program.LanguageManager.Translation.GameBtnDeck;
            ReplaysBtn.Text = Program.LanguageManager.Translation.GameBtnReplay;
            ProfileBtn.Text = Program.LanguageManager.Translation.GameBtnProfile;
            OptionsBtn.Text = Program.LanguageManager.Translation.GameBtnOption;
            QuickBtn.Text = Program.LanguageManager.Translation.GameBtnQuick;
            RankedQuickBtn.Text = Program.LanguageManager.Translation.GameBtnQuick;
            HostBtn.Text = Program.LanguageManager.Translation.GameBtnHost;
            RankedHostBtn.Text = Program.LanguageManager.Translation.GameBtnHost;
            OfflineBtn.Text = Program.LanguageManager.Translation.GameBtnOffline;
            LogoutBtn.Text = Program.LanguageManager.Translation.GameBtnLogout;
            groupBox2.Text = Program.LanguageManager.Translation.GameTabUnranked;
            groupBox3.Text = Program.LanguageManager.Translation.GameTabRanked;

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

        private void AddServer(string server)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddServer), server);
                return;
            }
            if (!ServerList.Items.Contains(server))
                ServerList.Items.Add(server);

        }

        private void RemoveServer(string server)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(RemoveServer), server);
                return;
            }

            if (ServerList.Items.Contains(server))
            {
                ServerList.Items.Remove(server);
                if(ServerList.SelectedIndex == -1)
                    ServerList.SelectedIndex = 0;
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
                    if(m_rooms[room].server == server)
                        removerooms.Add(room);
                }
            }

            foreach (string removeroom in removerooms)
            {
                if (m_rooms.ContainsKey(removeroom))
                    m_rooms.Remove(removeroom);
            }

            FilterGames(null,EventArgs.Empty);
            UpdateServerInfo();
        }

    
        private void UpdateServerInfo()
        {

            if (InvokeRequired)
            {
                Invoke(new Action(UpdateServerInfo));
            }
            else
            {
                string openrooms;
                string numberofplayers;
                string numberofrooms;
                string ranked;
                string unranked;
                GameData(out numberofrooms, out openrooms, out numberofplayers, out ranked, out unranked);

                NumberofRooms.Text = numberofrooms;
                NumberOfOpenRooms.Text = openrooms;
                NumberOfPlayers.Text = numberofplayers;
                NumberofRanked.Text = ranked;
                NumberOfUnranked.Text = unranked;
            }
        }

        private void DeckBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.GenerateConfig();
            LauncherHelper.RunGame("-d");
        }
        private void ReplaysBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.GenerateConfig();
            LauncherHelper.RunGame("-r");
        }

        private void ProfileBtn_Click(object sender, EventArgs e)
        {
            var profile = new ProfileFrm();
            profile.ShowDialog();

        }

        private void OptionsBtn_Click(object sender, EventArgs e)
        {
            var settings = new Settings();
            settings.ShowDialog();

        }

        private void QuickHost(string mode,bool isranked)
        {
            var ran = new Random();
            var form = new Host(false, false)
                {
                    CardRules = {Text = Program.Config.CardRules},
                    Mode = {Text = mode},
                    Priority = {Checked = Program.Config.EnablePrority},
                    CheckDeck = {Checked = Program.Config.DisableCheckDeck},
                    ShuffleDeck = {Checked = Program.Config.DisableShuffleDeck},
                    LifePoints = {Text = Program.Config.Lifepoints},
                    GameName = LauncherHelper.GenerateString().Substring(0, 5),
                    BanList = {SelectedItem = Program.Config.BanList},
                    TimeLimit = {SelectedItem = Program.Config.TimeLimit}
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
                if(Program.Config.Lifepoints != ((mode == "Tag") ? "16000":"8000"))
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
                LauncherHelper.GenerateConfig(GetServer(),form.GenerateURI(isranked));
                LauncherHelper.RunGame("-j");
            }
            else
            {
                LauncherHelper.GenerateConfig(Program.ServerList[server],form.GenerateURI(isranked));
                LauncherHelper.RunGame("-j");
            }

        }
        private void HostBtn_Click(object sender, EventArgs e)
        {

            var button = (Button)sender;
            var form = new Host(false, (button.Name == "RankedHostBtn"));
            if (button.Name == "RankedHostBtn")
            {
                form.Mode.Items.Clear();
                form.Mode.Items.AddRange(new object[] {"Single", "Match", "Tag" });
                form.Mode.SelectedItem = "Match";
                if (form.BanList.Items.Count > 0)
                    form.BanList.SelectedIndex = 0;
                form.BanList.Enabled = false;
                form.TimeLimit.Items.Clear();
                form.TimeLimit.Items.Add("Server Defualt");
                form.TimeLimit.SelectedItem = "Server Defualt";
                form.TimeLimit.Enabled = false;
                form.Priority.Enabled = false;
                form.ShuffleDeck.Enabled = false;
                form.CheckDeck.Enabled = false;
                form.LifePoints.Enabled = false;
                form.CardRules.Items.Clear();
                form.CardRules.Items.AddRange(new object[] { "TCG", "OCG"});
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
                    MessageBox.Show("No Servers are Available.");
                    return;
                }

                LauncherHelper.GenerateConfig(GetServer(), form.GenerateURI((button.Name == "RankedHostBtn")));
                LauncherHelper.RunGame("-j");
        }
        }

        public ServerInfo GetServer()
        {
            ServerInfo server;
            if (ServerList.SelectedIndex == 0)
            {
                int serverselect = Program.Rand.Next(1, ServerList.Items.Count);

                if (Program.ServerList.ContainsKey(ServerList.Items[serverselect].ToString()))
                    server = Program.ServerList[ServerList.Items[serverselect].ToString()];
                else
                {
                    MessageBox.Show("Server not found.");
                    return null;
                }
            }
            else
            {
                if (Program.ServerList.ContainsKey(ServerList.SelectedItem.ToString()))
                    server = Program.ServerList[ServerList.SelectedItem.ToString()];
                else
                {
                    MessageBox.Show("Server not found.");
                    return null;
                }
            }
            return server;
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
            UpdateServerInfo();

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

            if (FilterActive.Checked)
            {
                if (!m_rooms[roomname].hasStarted)
                {
                    if (m_rooms[roomname].Contains(FilterTextBox.Text.ToLower()) ||
                        m_rooms[roomname].roomName.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                    {
                        rooms.Items.Add(roomname);
                    }
                }
            }
            else
            {
                if (m_rooms[roomname].Contains(FilterTextBox.Text.ToLower()) ||
                        m_rooms[roomname].roomName.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                    {
                        rooms.Items.Add(roomname);
                    }
            }

            UpdateServerInfo();
        }

        public void FilterGames(object sender, EventArgs e)
        {
            RankedList.Items.Clear();
            UnrankedList.Items.Clear();

            foreach (string item in ObjectKeys())
            {
                if (FilterActive.Checked)
                {
                    if (!m_rooms[item].hasStarted)
                    {
                        if (m_rooms[item].Contains(FilterTextBox.Text.ToLower()) ||
                            m_rooms[item].roomName.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                        {
                            if (m_rooms[item].isRanked)
                                RankedList.Items.Add(item);
                            else
                                UnrankedList.Items.Add(item);
                        }
                    }
                }
                else
                {
                        if (m_rooms[item].Contains(FilterTextBox.Text.ToLower()) ||
                            m_rooms[item].roomName.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                        {
                            if (m_rooms[item].isRanked)
                                RankedList.Items.Add(item);
                            else
                                UnrankedList.Items.Add(item);
                        }
                }
            }

        }

        public List<object> ObjectKeys()
        {
            return m_rooms.Keys.Cast<object>().ToList();
        }

        public string GameData(out string numberofrooms, out string openrooms, out string numberofplayers, out string ranked, out string unranked)
        {
            int playercount = 0;
            int openroom = 0;
            int rooms = 0;
            int rankedrooms = 0;
            int unrankedrooms = 0;
            foreach (string item in ObjectKeys())
            {
                string[] players = m_rooms[item].playerList;
                playercount = playercount + players.Length;
                if (!m_rooms[item].hasStarted) openroom++;
                if (m_rooms[item].isRanked) rankedrooms++; else unrankedrooms++;
                rooms++;
            }
            numberofrooms = rooms.ToString(CultureInfo.InvariantCulture);
            numberofplayers = playercount.ToString(CultureInfo.InvariantCulture);
            ranked = rankedrooms.ToString(CultureInfo.InvariantCulture);
            unranked = unrankedrooms.ToString(CultureInfo.InvariantCulture);
            return openrooms = openroom.ToString(CultureInfo.InvariantCulture);
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
            if (FilterActive.Checked) rooms.Items.Remove(roomname);
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
            UpdateServerInfo();
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
                Invoke(new Action<string,string[]>(InternalRoomPlayersUpdate),data.Command, ((object)data.Data.Split(',')));
            }
        }

        private void InternalRoomPlayersUpdate(string room,string[] data)
        {
            string roomname = room;
            if (!m_rooms.ContainsKey(roomname)) return;
            RoomInfos item = m_rooms[roomname];

            item.playerList = data;

            if (item.isRanked)
                RankedList.UpdateList();
            else
                UnrankedList.UpdateList();

            UpdateServerInfo();
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
                        InputBox = {MaxLength = 4}
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

            LauncherHelper.GenerateConfig(Program.ServerList[item.server], item.GenerateURI());
            LauncherHelper.RunGame("-j");
        }

        private void FilterTextBox_Enter(object sender, EventArgs e)
        {
            if (FilterTextBox.Text == "Search")
            {
                FilterTextBox.Text = "";
                FilterTextBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void FilterTextBox_Leave(object sender, EventArgs e)
        {
            if (FilterTextBox.Text == "")
            {
                FilterTextBox.Text = "Search";
                FilterTextBox.ForeColor = SystemColors.WindowFrame;
            }
        }

        private void QuickBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || sender is Button)
            {
                var mnu = new ContextMenuStrip();
                var mnuSingle = new ToolStripMenuItem("Single");
                var mnuMatch = new ToolStripMenuItem("Match");
                var mnuTag = new ToolStripMenuItem("Tag");
                if (((Button)sender).Name == "RankedQuickBtn")
                {
                    mnuSingle.Name = "RSingle";
                    mnuMatch.Name = "RMatch";
                    mnuTag.Name = "RTag";
                }
                else
                {
                    mnuSingle.Name = "Single";
                    mnuMatch.Name = "Match";
                    mnuTag.Name = "Tag";
                }

                mnuSingle.Click += QuickHost_Click;
                mnuMatch.Click += QuickHost_Click;
                mnuTag.Click += QuickHost_Click;

                mnu.Items.AddRange(((Button) sender).Name == "RankedQuickBtn"
                                       ? new ToolStripItem[] {mnuSingle, mnuMatch, mnuTag}
                                       : new ToolStripItem[] {mnuTag, mnuMatch, mnuSingle});

                mnu.DefaultDropDownDirection = ToolStripDropDownDirection.BelowRight;
                mnu.Show((Button)sender, new Point(0, 0 - mnu.Height));
            }
        }

        private void QuickHost_Click(object sender, EventArgs e)
        {
            var button = (ToolStripItem)sender;
            QuickHost((button.Name.StartsWith("R")) ? button.Name.Substring(1):button.Name,(button.Name.StartsWith("R")));
        }

        private void QuickBtn_Click(object sender, EventArgs e)
        {
            QuickBtn_MouseUp(QuickBtn, new MouseEventArgs(MouseButtons.Right,1,1,1,1));
        }

        private void OfflineBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.RunGame(null);
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            var process = new Process();
            var info = new ProcessStartInfo(Application.ExecutablePath, "-r -l");
            process.StartInfo = info;
            process.Start();
            Application.Exit();
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
                new [] { new Point(bounds.X, bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height), new Point(bounds.X, bounds.Y + bounds.Height), new Point(bounds.X, bounds.Y) });
            //toplet
            g.DrawString((info == null) ? "???/???/???" : RoomInfos.GameMode(info.mode) + " / " + LauncherHelper.GetBanListFromInt(info.banListType) + " / " + (info.timer == 0 ? "3 mins" : "5 mins"), e.Font, Brushes.Black,
                list.GetItemRectangle(index).Location + offset);
            //topright
            g.DrawString((info == null) ? "???" : RoomInfos.GameRule(info.rule), e.Font, Brushes.Black,
                new Rectangle(bounds.X + (bounds.Width - (int)rulesize.Width) - offset.Width, bounds.Y + offset.Height, bounds.Width, bounds.Height));
            ////bottomright
            g.DrawString((info == null) ? "???" : (info.isLocked ? Program.LanguageManager.Translation.GameLocked:Program.LanguageManager.Translation.GameOpen), 
                e.Font, Brushes.Black,
                new Rectangle(bounds.X + (bounds.Width - (int)lockedsize.Width) - offset.Width, bounds.Y + (bounds.Height - (int)lockedsize.Height) - offset.Height, bounds.Width, bounds.Height));
            //bottom center
            g.DrawString(playerstring, e.Font, Brushes.Black,
                new Rectangle(bounds.X + ((bounds.Width / 2) - ((int)playersSize.Width / 2)), bounds.Y + (bounds.Height - (int)playersSize.Height) - offset.Height, bounds.Width, bounds.Height));
            e.DrawFocusRectangle();
        }

    }
}