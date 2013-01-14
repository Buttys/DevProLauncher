using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using System.Diagnostics;
namespace YGOPro_Launcher
{
    public partial class NewServerInterface_frm : Form
    {

        public Dictionary<string, RoomInfos> m_rooms;

        //private GameListBox UnrankedList;
        //private GameListBox RankedList;

        public NewServerInterface_frm(string ServerName)
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            this.Name = ServerName;
            this.Text = ServerName;


            m_rooms = new Dictionary<string, RoomInfos>();
            FilterActive.CheckedChanged += new EventHandler(FilterGames);
            FilterTextBox.TextChanged += new EventHandler(FilterGames);
            Program.ServerConnection.AddRooms += new NetClient.ServerRooms(OnRoomsList);
            Program.ServerConnection.AddRoom += new NetClient.ServerRooms(OnRoomCreated);
            Program.ServerConnection.RemoveRoom += new NetClient.ServerResponse(OnRoomRemoved);
            Program.ServerConnection.UpdateRoomStatus += new NetClient.ServerResponse(OnRoomStarted);
            Program.ServerConnection.UpdateRoomPlayers += new NetClient.ServerResponse(OnRoomPlayersUpdate);
            Program.ServerConnection.UserInfoUpdate += new NetClient.ServerResponse(UpdateUserInfo);
            //LauncherHelper.GameClosed += new LauncherHelper.UpdateUserInfo(RequestUserWLD);
            LauncherHelper.DeckEditClosed += new LauncherHelper.UpdateUserInfo(RefreshDeckList);
            RankedList.DrawItem += new DrawItemEventHandler(GameListBox_DrawItem);
            UnrankedList.DrawItem += new DrawItemEventHandler(GameListBox_DrawItem);
            UnrankedList.DoubleClick += new System.EventHandler(this.LoadRoom);
            RankedList.DoubleClick += new System.EventHandler(this.LoadRoom);
            RefreshDeckList();
            DeckSelect.SelectedIndexChanged += new EventHandler(DeckSelect_SelectedValueChanged);

            if (Program.UserInfo.Rank > 0)
            {
                RankedList.MouseUp += new MouseEventHandler(RightClickRoom);
                UnrankedList.MouseUp += new MouseEventHandler(RightClickRoom);
            }

            ApplyTranslation();

            QuickBtn.MouseUp += new MouseEventHandler(QuickBtn_MouseUp);
            RankedQuickBtn.MouseUp += new MouseEventHandler(QuickBtn_MouseUp);
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

        private void RightClickRoom(object sender, MouseEventArgs e)
        {
            ListBox list = (ListBox)sender;
            if (e.Button == MouseButtons.Right)
            {
                int index = list.IndexFromPoint(e.X, e.Y);
                if (index == -1) return;

                list.SelectedIndex = index;
                string roomname = list.Items[index].ToString();

                RoomInfos info = null;

                if (!m_rooms.ContainsKey(roomname))
                    return;
                else
                    info = m_rooms[roomname];

                ContextMenuStrip mnu = new ContextMenuStrip();
                List<ToolStripMenuItem> mnuitems = new List<ToolStripMenuItem>();

                mnuitems.Add(new ToolStripMenuItem("Kill: " + info.RoomName));
                string[] players = info.Players.Split(',');
                foreach (string player in players)
                {
                    mnuitems.Add(new ToolStripMenuItem("Disconnect: " + player));
                }


                foreach (ToolStripMenuItem mnuitem in mnuitems)
                {
                    if (mnuitem.Text == "Kill: "+ info.RoomName)
                        mnuitem.Click += new EventHandler(KillRoom);
                    else
                        mnuitem.Click += new EventHandler(DisconnectUser);
                }

                mnu.Items.AddRange(mnuitems.ToArray());
                mnu.Show(list, e.Location);
            }
        }

        private void DisconnectUser(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            Program.ServerConnection.SendPacket("ADMIN||GKICK||" + item.Text.Replace("Disconnect:", "").Trim());

        }

        private void KillRoom(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            Program.ServerConnection.SendPacket("ADMIN||KILL||" + item.Text.Replace("Kill: ", "").Trim());
        }

        public void RequestUserWLD()
        {
            Program.ServerConnection.SendPacket("WLD");
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
                        DeckSelect.Items.Add(Path.GetFileNameWithoutExtension(deck));
                }
                DeckSelect.Text = Program.Config.DefaultDeck;
            }
        }

        private void DeckSelect_SelectedValueChanged(object sender, EventArgs e)
        {
            Program.Config.DefaultDeck = DeckSelect.SelectedItem.ToString();
            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
        }

    
        private void UpdateServerInfo()
        {

            if (InvokeRequired)
            {
                Invoke(new Action(UpdateServerInfo));
            }
            else
            {
                string openrooms = "0";
                string numberofplayers = "0";
                string numberofrooms = "0";
                string ranked = "";
                string unranked = "";
                GameData(out numberofrooms, out openrooms, out numberofplayers, out ranked, out unranked);

                NumberofRooms.Text = numberofrooms;
                NumberOfOpenRooms.Text = openrooms;
                NumberOfPlayers.Text = numberofplayers;
                NumberofRanked.Text = ranked;
                NumberOfUnranked.Text = unranked;
            }
        }

        public void UpdateUserInfo(string message)
        {
            if (!Program.ServerConnection.IsConnected) return;

            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string>(UpdateUserInfo), message);
                }
                else
                {
                    //Username.Text = Program.UserInfo.Username;
                    if (message == "not found") return;
                    string[] values = message.Split(',');
                    Program.UserInfo.Wins = Int32.Parse(values[0]);
                    Program.UserInfo.Loses = Int32.Parse(values[1]);
                    Program.UserInfo.Draws = Int32.Parse(values[2]);
                    //Record.Text = values[0] + "/" + values[1] + "/" + values[2];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DeckBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.GenerateConfig("ygopro:/" + Program.Config.ServerAddress + "/" + Program.Config.GamePort + "/20000,U,Edit");
            LauncherHelper.RunGame("-d");
        }
        private void ReplaysBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.GenerateConfig("ygopro:/" + Program.Config.ServerAddress + "/" + Program.Config.GamePort + "/20000,U,Replay");
            LauncherHelper.RunGame("-r");
        }

        private void ProfileBtn_Click(object sender, EventArgs e)
        {
            Profile_frm profile = new Profile_frm();
            profile.ShowDialog();

        }

        private void OptionsBtn_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();

        }

        private void QuickHost(string mode,bool isranked)
        {
            Host form = new Host(false, false);
            form.CardRules.Text = Program.Config.CardRules;
            form.Mode.Text = mode;
            form.Priority.Checked = Program.Config.EnablePrority;
            form.CheckDeck.Checked = Program.Config.DisableCheckDeck;
            form.ShuffleDeck.Checked = Program.Config.DisableShuffleDeck;
            form.LifePoints.Text = Program.Config.Lifepoints;
            form.GameName = LauncherHelper.GenerateString().Substring(0, 5);
            form.BanList.SelectedItem = Program.Config.BanList;
            form.TimeLimit.SelectedItem = Program.Config.TimeLimit;

            ListBox list = (isranked) ? RankedList : UnrankedList;

            if (isranked)
            {
                form.BanList.SelectedIndex = 0;
                form.CheckDeck.Checked = false;
                form.ShuffleDeck.Checked = false;
                form.Priority.Checked = false;
                if (form.Mode.Text == "Tag")
                    form.LifePoints.Text = "16000";
                else
                    form.LifePoints.Text = "8000";
            }
            else
            {
                if(Program.Config.Lifepoints != ((mode == "Tag") ? "16000":"8000"))
                {
                    if (MessageBox.Show("Your quick host settings does not follow the reccomend lifepoints rule, do you want to change this to the defualt?", "LifePoints", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (mode == "Tag")
                            form.LifePoints.Text = "16000";
                        else
                            form.LifePoints.Text = "8000";

                    }
                }
            }

            RoomInfos userinfo = RoomInfos.FromName(form.GenerateURI(Program.Config.ServerAddress, Program.Config.GamePort.ToString(), isranked).Split('/')[3], "", false);

            List<RoomInfos> MatchedRooms = new List<RoomInfos>();

            foreach (object room in list.Items)
            {
                if (!m_rooms.ContainsKey(room.ToString()))
                    continue;

                RoomInfos info = m_rooms[room.ToString()];
                if (!RoomInfos.CompareRoomInfo(userinfo, info))
                    continue;
                else
                    MatchedRooms.Add(info);
            }

            Random random = new Random();
            if (MatchedRooms.Count > 0)
            {
                int selectroom = random.Next(MatchedRooms.Count);
                form.GameName = MatchedRooms[selectroom].RoomName;
            }

            LauncherHelper.GenerateConfig(form.GenerateURI(Program.Config.ServerAddress, Program.Config.GamePort.ToString(), isranked));
            LauncherHelper.RunGame("-j");
            return;
        }
        private void HostBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Host form = new Host(false, (button.Name == "RankedHostBtn"));
            if (button.Name == "RankedHostBtn")
            {
                form.Mode.Items.Clear();
                form.Mode.Items.AddRange(new object[] { "Match", "Tag" });
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
                form.CardRules.Items.AddRange(new object[] { "OCG/TCG", "TCG", "OCG" });
                form.CardRules.SelectedItem = "OCG/TCG";
            }

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LauncherHelper.GenerateConfig(form.GenerateURI(Program.Config.ServerAddress, Program.Config.GamePort.ToString(), (button.Name == "RankedHostBtn") ? true : false));
                LauncherHelper.RunGame("-j");
            }
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
            if (m_rooms.ContainsKey(room.RoomName))
                return;
            m_rooms.Add(room.RoomName, room);
            ListBox rooms = (room.isRanked ? RankedList : UnrankedList);

            if (FilterActive.Checked)
            {
                if (!m_rooms[room.RoomName].Started)
                {
                    if (m_rooms[room.RoomName].Players.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        m_rooms[room.RoomName].RoomName.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                    {
                        rooms.Items.Add(m_rooms[room.RoomName].RoomName);
                    }
                }
            }
            else
            {
                    if (m_rooms[room.RoomName].Players.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        m_rooms[room.RoomName].RoomName.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                    {
                        rooms.Items.Add(m_rooms[room.RoomName].RoomName);
                    }
            }


        }

        public void FilterGames(object sender, EventArgs e)
        {
            RankedList.Items.Clear();
            UnrankedList.Items.Clear();

            foreach (string item in ObjectKeys())
            {
                if (FilterActive.Checked)
                {
                    if (!m_rooms[item].Started)
                    {
                        if (m_rooms[item].Players.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            m_rooms[item].RoomName.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                        {
                            if (m_rooms[item].isRanked)
                                RankedList.Items.Add(m_rooms[item].RoomName);
                            else
                                UnrankedList.Items.Add(m_rooms[item].RoomName);
                        }
                    }
                }
                else
                {
                        if (m_rooms[item].Players.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            m_rooms[item].RoomName.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                        {
                            if (m_rooms[item].isRanked)
                                RankedList.Items.Add(m_rooms[item].RoomName);
                            else
                                UnrankedList.Items.Add(m_rooms[item].RoomName);
                        }
                }
            }

        }

        public List<object> ObjectKeys()
        {
            List<object> keydata = new List<object>();

            foreach (string key in m_rooms.Keys)
            {
                keydata.Add(key);
            }

            return keydata;
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
                string[] players = m_rooms[item].Players.Split(',');
                playercount = playercount + players.Length;
                if (!m_rooms[item].Started) openroom++;
                if (m_rooms[item].isRanked) rankedrooms++; else unrankedrooms++;
                rooms++;
            }
            numberofrooms = rooms.ToString();
            numberofplayers = playercount.ToString();
            ranked = rankedrooms.ToString();
            unranked = unrankedrooms.ToString();
            return openrooms = openroom.ToString();
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
            item.Started = true;
            if (FilterActive.Checked) rooms.Items.Remove(item.RoomName);
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
                RankedList.Items.Remove(room.RoomName);
            else
                UnrankedList.Items.Remove(room.RoomName);
            m_rooms.Remove(roomname);
            UpdateServerInfo();
        }

        public void OnRoomPlayersUpdate(string message)
        {
            string[] roomdata = message.Split('|');

            Invoke(new Action<string, string>(InternalRoomPlayersUpdate), roomdata[0], (roomdata.Length > 1) ? roomdata[1] : "");

        }

        private void InternalRoomPlayersUpdate(string roomname, string players)
        {
            if (!m_rooms.ContainsKey(roomname)) return;
            RoomInfos item = m_rooms[roomname];

            item.Players = players;

            if (item.isRanked)
                RankedList.Refresh();
            else
                UnrankedList.Refresh();

            UpdateServerInfo();
        }

        public void LoadRoom(object sender, EventArgs e)
        {
            ListBox rooms = (ListBox)sender;
            if (!m_rooms.ContainsKey(rooms.SelectedItem.ToString()))
                return;

            RoomInfos item = m_rooms[rooms.SelectedItem.ToString()];
            if (item.Started)
            {
                MessageBox.Show("Spectating games in progress is unavailable.. Please join them before they start.");
                return;
            }
            LauncherHelper.GenerateConfig(item.GenerateURI(Program.Config.ServerAddress,Program.Config.GamePort));
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
            if (e.Button == System.Windows.Forms.MouseButtons.Right || sender is Button)
            {
                ContextMenuStrip mnu = new ContextMenuStrip();
                ToolStripMenuItem mnuSingle = new ToolStripMenuItem("Single");
                ToolStripMenuItem mnuMatch = new ToolStripMenuItem("Match");
                ToolStripMenuItem mnuTag = new ToolStripMenuItem("Tag");
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

                mnuSingle.Click += new EventHandler(QuickHost_Click);
                mnuMatch.Click += new EventHandler(QuickHost_Click);
                mnuTag.Click += new EventHandler(QuickHost_Click);

                if (((Button)sender).Name == "RankedQuickBtn")
                {
                    mnu.Items.AddRange(new ToolStripItem[] { mnuMatch, mnuTag });
                }
                else
                {
                    mnu.Items.AddRange(new ToolStripItem[] { mnuTag, mnuMatch, mnuSingle });
                }

                mnu.DefaultDropDownDirection = ToolStripDropDownDirection.BelowRight;
                mnu.Show((Button)sender, new Point(0, 0 - mnu.Height));
            }
        }

        private void QuickHost_Click(object sender, EventArgs e)
        {
            ToolStripItem button = (ToolStripItem)sender;
            QuickHost((button.Name.StartsWith("R")) ? button.Name.Substring(1):button.Name,(button.Name.StartsWith("R")));
        }

        private void QuickBtn_Click(object sender, EventArgs e)
        {
            QuickBtn_MouseUp(QuickBtn, new MouseEventArgs(System.Windows.Forms.MouseButtons.Right,1,1,1,1));//wasnt accepting MouseventArgs.Empty!?
        }

        private void OfflineBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.RunGame(null);
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo(Application.ExecutablePath, "-r -l");
            process.StartInfo = info;
            process.Start();
            Application.Exit();
        }

        private void GameListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox list = (ListBox)sender;
            e.DrawBackground();

            int index = e.Index;
            string room = list.Items[index].ToString();
            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            Graphics g = e.Graphics;
            RoomInfos info = null;
            if (m_rooms.ContainsKey(room))
                info = m_rooms[room];

            //item info

            string playerstring = "";

            if (info == null)
            {
                playerstring = "??? vs ???";
            }
            else
            {
                bool istag = (info.Mode == 2);
                string[] players = info.Players.Split(',');

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


            Rectangle Bounds = list.GetItemRectangle(index);
            SizeF GameNamesize = e.Graphics.MeasureString((info == null) ? "???" : info.RoomName, e.Font);
            SizeF Rulesize = e.Graphics.MeasureString((info == null) ? "???" : RoomInfos.GameRule(info.Rule), e.Font);
            SizeF playersSize = e.Graphics.MeasureString(playerstring, e.Font);
            SizeF infoListsize = e.Graphics.MeasureString((info == null) ? "???/???/???" : RoomInfos.GameMode(info.Mode) + " / " + LauncherHelper.GetBanListFromInt(info.BanList) + " / " +(info.Timer == 0 ? "3 mins" : "5 mins") , e.Font);
            bool illegal = true;
            SolidBrush backgroundcolor = null;

            Size offset = new Size(5, 5);

            if (info == null)
            {
                backgroundcolor = new SolidBrush(Color.Red);
            }
            else
            {
                illegal = (info.Rule <= 2 ? info.BanList > 0 : false) || info.NoCheckDeck || info.NoShuffleDeck || info.EnablePriority || (info.Mode == 2) ? info.StartLp != 16000 : info.StartLp != 8000 || info.StartHand != 5 || info.DrawCount != 1;
                backgroundcolor = new SolidBrush(info.Started ? Color.LightGray :
                (illegal ? Color.LightCoral :
                (info.Rule == 4 ? Color.Violet :
                (info.Rule == 5 ? Color.Gold :
                (info.Mode == 2 ? Color.LightGreen :
                (info.Mode == 1 ? Color.LightSteelBlue :
                Color.LightBlue))))));
            }

            //draw item
            g.FillRectangle(backgroundcolor, e.Bounds);
            g.DrawLines((selected) ? new Pen(Brushes.Purple, 5) : new Pen(Brushes.Black, 5),
                new Point[] { new Point(Bounds.X, Bounds.Y), new Point(Bounds.X + Bounds.Width, Bounds.Y), new Point(Bounds.X + Bounds.Width, Bounds.Y + Bounds.Height), new Point(Bounds.X, Bounds.Y + Bounds.Height), new Point(Bounds.X, Bounds.Y) });
            //toplet
            g.DrawString((info == null) ? "???/???/???" : RoomInfos.GameMode(info.Mode) + " / " + LauncherHelper.GetBanListFromInt(info.BanList) + " / " + (info.Timer == 0 ? "3 mins" : "5 mins"), e.Font, Brushes.Black,
                list.GetItemRectangle(index).Location + offset);
            //topright
            g.DrawString((info == null) ? "???" : RoomInfos.GameRule(info.Rule), e.Font, Brushes.Black,
                new Rectangle(Bounds.X + (Bounds.Width - (int)Rulesize.Width) - offset.Width, Bounds.Y + offset.Height, Bounds.Width, Bounds.Height));
            ////bottomright
            //g.DrawString("", e.Font, (selected) ? Brushes.White : Brushes.Black,
            //    new Rectangle(Bounds.X + (Bounds.Width - (int)Timersize.Width), Bounds.Y + (Bounds.Height - (int)Timersize.Height), Bounds.Width, Bounds.Height));
            ////bottomleft
            //g.DrawString("", e.Font, (selected) ? Brushes.White : Brushes.Black,
            //    new Rectangle(Bounds.X, Bounds.Y + (Bounds.Height - (int)Modesize.Height), Bounds.Width, Bounds.Height));
            ////top center
            //g.DrawString("", e.Font, (selected) ? Brushes.White : Brushes.Black,
            //    new Rectangle(Bounds.X + ((Bounds.Width / 2) - ((int)BanListsize.Width / 2)), Bounds.Y, Bounds.Width, Bounds.Height));
            //bottom center
            g.DrawString(playerstring, e.Font, Brushes.Black,
                new Rectangle(Bounds.X + ((Bounds.Width / 2) - ((int)playersSize.Width / 2)), Bounds.Y + (Bounds.Height - (int)playersSize.Height) - offset.Height, Bounds.Width, Bounds.Height));
            e.DrawFocusRectangle();
        }

    }
}
