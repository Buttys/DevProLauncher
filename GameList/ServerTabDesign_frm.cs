using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
namespace YGOPro_Launcher
{
    public partial class ServerInterface_frm : Form
    {

        private Dictionary<string, ListViewItem> m_rooms;

        public ServerInterface_frm(string ServerName)
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            this.Name = ServerName;
            this.Text = ServerName;
            m_rooms = new Dictionary<string, ListViewItem>();
            listRooms.DoubleClick += new System.EventHandler(this.LoadRoom);
            RankedRooms.DoubleClick += new System.EventHandler(this.LoadRoom);
            FilterActive.CheckedChanged += new EventHandler(FilterGames);
            FilterTextBox.TextChanged += new EventHandler(FilterGames);
            ServerTabs.SelectedIndexChanged += new EventHandler(GameType_SelectedIndexChanged);
            GameType_SelectedIndexChanged(null, EventArgs.Empty);

            Program.ServerConnection.AddRooms += new NetClient.ServerRooms(OnRoomsList);
            Program.ServerConnection.AddRoom += new NetClient.ServerRooms(OnRoomCreated);
            Program.ServerConnection.RemoveRoom += new NetClient.ServerResponse(OnRoomRemoved);
            Program.ServerConnection.UpdateRoomStatus += new NetClient.ServerResponse(OnRoomStarted);
            Program.ServerConnection.UpdateRoomPlayers += new NetClient.ServerResponse(OnRoomPlayersUpdate);
            Program.ServerConnection.UserInfoUpdate += new NetClient.ServerResponse(UpdateUserInfo);
            LauncherHelper.GameClosed += new LauncherHelper.UpdateUserInfo(RequestUserWLD);
            listRooms.ColumnClick += new ColumnClickEventHandler(SortRooms);
            RankedRooms.ColumnClick += new ColumnClickEventHandler(SortRooms);

            Username.Text = Program.UserInfo.Username;

            if (Directory.Exists(Program.Config.LauncherDir + "deck/"))
            {
                string[] decks = Directory.GetFiles(Program.Config.LauncherDir + "deck/");
                foreach (string deck in decks)
                    DeckSelect.Items.Add(Path.GetFileNameWithoutExtension(deck));
            }
            DeckSelect.Text = Program.Config.DefaultDeck;
            DeckSelect.SelectedIndexChanged += new EventHandler(DeckSelect_SelectedValueChanged);
            ApplyTranslation();
            if (Program.UserInfo.Rank > 0)
            {
                listRooms.MouseUp +=new MouseEventHandler(listRooms_MouseUp);
                RankedRooms.MouseUp += new MouseEventHandler(RankedRooms_MouseUp);
            }

        }

        public void ApplyTranslation()
        {
            groupBox1.Text = Program.LanguageManager.Translation.GameServerInfo;
            ServerTabs.TabPages[0].Text = Program.LanguageManager.Translation.GameTabRanked;
            ServerTabs.TabPages[1].Text = Program.LanguageManager.Translation.GameTabUnranked;

            label1.Text = "# " + Program.LanguageManager.Translation.GameofRooms;
            label2.Text = "# " + Program.LanguageManager.Translation.GameofUnranked;
            label3.Text = "# " + Program.LanguageManager.Translation.GameofRanked;
            label5.Text = "# " + Program.LanguageManager.Translation.GameofOpenRooms;
            label4.Text = "# " + Program.LanguageManager.Translation.GameofPlayers;

            FilterActive.Text = Program.LanguageManager.Translation.GameFilterActive;

            ColumnRoomName.Text = Program.LanguageManager.Translation.GameColumnRoomName;
            ColumnBanList.Text = Program.LanguageManager.Translation.GameColumnBanList;
            ColumnTimer.Text = Program.LanguageManager.Translation.GameColumnTimer;
            ColumnType.Text = Program.LanguageManager.Translation.GameColumnType;
            ColumnRules.Text = Program.LanguageManager.Translation.GameColumnRules;
            ColumnMode.Text = Program.LanguageManager.Translation.GameColumnMode;
            ColumnState.Text = Program.LanguageManager.Translation.GameColumnState;
            ColumnPlayers.Text = Program.LanguageManager.Translation.GameColumnPlayers;

            RColumnRoomName.Text = Program.LanguageManager.Translation.GameColumnRoomName;
            RColumnBanList.Text = Program.LanguageManager.Translation.GameColumnBanList;
            RColumnTimer.Text = Program.LanguageManager.Translation.GameColumnTimer;
            RColumnType.Text = Program.LanguageManager.Translation.GameColumnType;
            RColumnRules.Text = Program.LanguageManager.Translation.GameColumnRules;
            RColumnMode.Text = Program.LanguageManager.Translation.GameColumnMode;
            RColumnState.Text = Program.LanguageManager.Translation.GameColumnState;
            RColumnPlayers.Text = Program.LanguageManager.Translation.GameColumnPlayers;

            DeckBtn.Text = Program.LanguageManager.Translation.GameBtnDeck;
            ReplaysBtn.Text = Program.LanguageManager.Translation.GameBtnReplay;
            ProfileBtn.Text = Program.LanguageManager.Translation.GameBtnProfile;
            OptionsBtn.Text = Program.LanguageManager.Translation.GameBtnOption;
            QuickBtn.Text = Program.LanguageManager.Translation.GameBtnQuick;
            HostBtn.Text = Program.LanguageManager.Translation.GameBtnHost;
            label14.Text = Program.LanguageManager.Translation.GameLabWLD;
            label13.Text = Program.LanguageManager.Translation.GameLabDeck;
            label11.Text = Program.LanguageManager.Translation.GameLabUser; 

        }

        private void listRooms_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem item = listRooms.GetItemAt(e.X, e.Y);
                if (item == null) return;

                item.Selected = true;

                ContextMenuStrip mnu = new ContextMenuStrip();
                List<ToolStripMenuItem> mnuitems = new List<ToolStripMenuItem>();

                mnuitems.Add(new ToolStripMenuItem("Kill Room"));
                string[] players = item.SubItems[7].Text.Split(',');
                foreach (string player in players)
                {
                    mnuitems.Add(new ToolStripMenuItem("Disconnect: " + player.Trim()));
                }


                foreach (ToolStripMenuItem mnuitem in mnuitems)
                {
                    if (mnuitem.Text == "Kill Room")
                        mnuitem.Click += new EventHandler(KillRoom);
                    else
                        mnuitem.Click += new EventHandler(DisconnectUser);
                }

                mnu.Items.AddRange(mnuitems.ToArray());
                mnu.Show(listRooms, e.Location);
            }
        }

        private void RankedRooms_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem item = RankedRooms.GetItemAt(e.X, e.Y);
                if (item == null) return;

                item.Selected = true;

                ContextMenuStrip mnu = new ContextMenuStrip();
                List<ToolStripMenuItem> mnuitems = new List<ToolStripMenuItem>(); 

                mnuitems.Add(new ToolStripMenuItem("Kill Room"));
                string[] players = item.SubItems[7].Text.Split(',');
                foreach (string player in players)
                {
                    mnuitems.Add(new ToolStripMenuItem("Disconnect: " +player));
                }
               

                foreach (ToolStripMenuItem mnuitem in mnuitems)
                {
                    if (mnuitem.Text == "Kill Room")
                        mnuitem.Click += new EventHandler(KillRoom);
                    else
                        mnuitem.Click += new EventHandler(DisconnectUser);
                }

                mnu.Items.AddRange(mnuitems.ToArray());
                mnu.Show(RankedRooms, e.Location);
            }
        }

        private void DisconnectUser(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            Program.ServerConnection.SendPacket("ADMIN||GKICK||" + item.Text.Replace("Disconnect:", "").Trim());

        }

        private void KillRoom(object sender, EventArgs e)
        {
            ListView rooms = (ServerTabs.SelectedTab.Name == "Ranked" ? RankedRooms : listRooms);
            Program.ServerConnection.SendPacket("ADMIN||KILL||" + rooms.SelectedItems[0].Text);
        }

        public void RequestUserWLD()
        {
            Program.ServerConnection.SendPacket("WLD");
        }

        private void DeckSelect_SelectedValueChanged(object sender, EventArgs e)
        {
            Program.Config.DefaultDeck = DeckSelect.SelectedItem.ToString();
            Program.SaveConfig(Program.ConfigurationFilename,Program.Config);
        }

        private void GameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterGames(null, EventArgs.Empty);
            if (ServerTabs.SelectedTab.Name == "Ranked")
                QuickBtn.Enabled = false;
            else
                QuickBtn.Enabled = true;

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
                    Username.Text = Program.UserInfo.Username;
                    if (message == "not found") return;
                    string[] values = message.Split(',');
                    Program.UserInfo.Wins = Int32.Parse(values[0]);
                    Program.UserInfo.Loses = Int32.Parse(values[1]);
                    Program.UserInfo.Draws = Int32.Parse(values[2]);
                    Record.Text = values[0] + "/" + values[1] + "/" + values[2];
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

        private void QuickBtn_Click(object sender, EventArgs e)
        {
            Host form = new Host(false,false);
            form.CardRules.Text = Program.Config.CardRules;
            form.Mode.Text = Program.Config.Mode;
            form.Priority.Checked = Program.Config.EnablePrority;
            form.CheckDeck.Checked = Program.Config.DisableCheckDeck;
            form.ShuffleDeck.Checked = Program.Config.DisableShuffleDeck;
            form.LifePoints.Text = Program.Config.Lifepoints;
            form.GameName.Text = Program.Config.GameName;
            form.BanList.SelectedItem = Program.Config.BanList;
            form.TimeLimit.SelectedItem = Program.Config.TimeLimit;

            LauncherHelper.GenerateConfig(form.GenerateURI(Program.Config.ServerAddress, Program.Config.GamePort.ToString(), (ServerTabs.SelectedTab.Name == "Ranked") ? true : false));
            LauncherHelper.RunGame("-j");
            return;
        }
        private void HostBtn_Click(object sender, EventArgs e)
        {
            Host form = new Host(false, (ServerTabs.SelectedTab.Name == "Ranked"));
            if (ServerTabs.SelectedTab.Name == "Ranked")
            {
                form.Mode.Items.Clear();
                form.Mode.Items.AddRange(new object[] { "Match", "Tag" });
                form.Mode.SelectedItem = "Match";
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
                LauncherHelper.GenerateConfig(form.GenerateURI(Program.Config.ServerAddress, Program.Config.GamePort.ToString(), (ServerTabs.SelectedTab.Name == "Ranked") ? true : false));
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
            listRooms.Items.Clear();
            foreach (RoomInfos room in rooms)
            {
                InternalRoomCreated(room);
            }
            UpdateServerInfo();

        }

        public void OnRoomCreated(RoomInfos[] room)
        {
            listRooms.Invoke(new Action<RoomInfos>(InternalRoomCreated), room[0]);
        }

        private void InternalRoomCreated(RoomInfos room)
        {
            if (m_rooms.ContainsKey(room.RoomName))
                return;

            ListViewItem item = new ListViewItem(room.RoomName);

            m_rooms.Add(room.RoomName, item);

            string roomtype = "Unranked";
            if (room.isRanked) roomtype = "Ranked";
            item.SubItems.Add(roomtype);

            item.SubItems.Add(LauncherHelper.GetBanListFromInt(room.BanList));
            
            item.SubItems.Add((room.Timer == 0 ? "3 mins":"5 mins"));
            
            string rule = "TCG/OCG";
            if (room.Rule == 1) rule = "TCG";
            if (room.Rule == 0) rule = "OCG";
            if (room.Rule == 4) rule = "Anime";
            if (room.Rule == 5) rule = "Turbo Duel";
            item.SubItems.Add(rule);

            string type = "Single";
            if (room.Mode == 1) type = "Match";
            if (room.Mode == 2) type = "Tag";
            item.SubItems.Add(type);

           

            item.SubItems.Add(room.Started ? "Started" : "Waiting");

            item.SubItems.Add(room.Players);
            item.SubItems.Add(room.GenerateURI(Program.Config.ServerAddress, Program.Config.ServerPort));
            bool illegal = room.NoCheckDeck || room.NoShuffleDeck || room.EnablePriority || (room.Mode == 2) ? room.StartLp != 16000 : room.StartLp != 8000 || room.StartHand != 5 || room.DrawCount != 1;


            item.BackColor = room.Started ? Color.LightGray :
                (illegal ? Color.LightCoral :
                (room.Rule == 4 ? Color.Violet:
                (room.Rule == 5 ? Color.Gold:
                (room.Mode == 2 ? Color.LightGreen :
                (room.Mode == 1 ? Color.LightSteelBlue :
                Color.LightBlue)))));

            ListView rooms = (ServerTabs.SelectedTab.Name == "Ranked" ? RankedRooms : listRooms);

            if (FilterActive.Checked)
            {
                if (m_rooms[room.RoomName].SubItems[6].Text.Contains("Waiting") &&
                    m_rooms[room.RoomName].SubItems[1].Text.Contains(ServerTabs.SelectedTab.Name))
                {
                    if (m_rooms[room.RoomName].SubItems[7].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        m_rooms[room.RoomName].SubItems[0].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                    {
                        rooms.Items.Add(m_rooms[room.RoomName]);
                    }
                }
            }
            else
            {
                if (m_rooms[room.RoomName].SubItems[1].Text.Contains(ServerTabs.SelectedTab.Name))
                {
                    if (m_rooms[room.RoomName].SubItems[7].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        m_rooms[room.RoomName].SubItems[0].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                    {
                        rooms.Items.Add(m_rooms[room.RoomName]);
                    }
                }
            }


        }

        public void FilterGames(object sender, EventArgs e)
        {
            ListView rooms = (ServerTabs.SelectedTab.Name == "Ranked" ? RankedRooms : listRooms);

            rooms.Items.Clear();
            foreach (string item in ObjectKeys())
            {
                if (FilterActive.Checked)
                {
                    if (m_rooms[item].SubItems[6].Text.Contains("Waiting") &&
                        m_rooms[item].SubItems[1].Text.Contains(ServerTabs.SelectedTab.Name))
                    {
                        if (m_rooms[item].SubItems[7].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            m_rooms[item].SubItems[0].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                        {
                            rooms.Items.Add(m_rooms[item]);
                        }
                    }
                }
                else
                {
                    if (m_rooms[item].SubItems[1].Text.Contains(ServerTabs.SelectedTab.Name))
                    {
                        if (m_rooms[item].SubItems[7].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            m_rooms[item].SubItems[0].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                        {
                            rooms.Items.Add(m_rooms[item]);
                        }
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
                string[] players = m_rooms[item].SubItems[7].Text.Split(',');
                playercount = playercount + players.Length;
                if (m_rooms[item].SubItems[6].Text == "Waiting") openroom++;
                if (m_rooms[item].SubItems[1].Text == "Ranked") rankedrooms++; else unrankedrooms++;
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
            ListView rooms = (ServerTabs.SelectedTab.Name == "Ranked" ? RankedRooms : listRooms);
            ListViewItem item = m_rooms[roomname];

            item.BackColor = Color.LightGray;
            item.SubItems[6].Text = "Started";
            if (FilterActive.Checked) rooms.Items.Remove(item);
        }

        public void OnRoomRemoved(string roomname)
        {
            Invoke(new Action<string>(InternalRoomRemoved), roomname);

        }

        private void InternalRoomRemoved(string roomname)
        {
            if (!m_rooms.ContainsKey(roomname)) return;
            ListView rooms = (ServerTabs.SelectedTab.Name == "Ranked" ? RankedRooms : listRooms);
            ListViewItem item = m_rooms[roomname];
            rooms.Items.Remove(item);
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
            ListViewItem item = m_rooms[roomname];

            item.SubItems[7].Text = players;

            UpdateServerInfo();
        }

        public void LoadRoom(object sender, EventArgs e)
        {
            ListView rooms = (ServerTabs.SelectedTab.Name == "Ranked" ? RankedRooms : listRooms);
            ListViewItem item = rooms.SelectedItems[0];
            LauncherHelper.GenerateConfig(item.SubItems[8].Text);
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

        private void SortRooms(object sender, ColumnClickEventArgs e)
        {
            ListView rooms = (ServerTabs.SelectedTab.Name == "Ranked" ? RankedRooms : listRooms);
            ListViewItemComparer sorter = rooms.ListViewItemSorter as ListViewItemComparer;

            if (sorter == null)
            {
                sorter = new ListViewItemComparer(e.Column);
                rooms.ListViewItemSorter = sorter;
            }
            else
            {
                sorter.Column = e.Column;
            }

            rooms.Sort();
        }

    }
}
