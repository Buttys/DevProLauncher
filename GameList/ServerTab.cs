﻿using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.IO;
using System.Drawing;
namespace YGOPro_Launcher
{
    public partial class ServerTab : TabPage
    {

        private Dictionary<string, ListViewItem> m_rooms;

        public ServerTab(string ServerName)
        {
            InitializeComponent();
            this.Controls.Add(MainPanel);
            this.Name = ServerName;
            this.Text = ServerName;
            m_rooms = new Dictionary<string, ListViewItem>();
            listRooms.DoubleClick += new System.EventHandler(this.LoadRoom);
            FilterActive.CheckedChanged += new EventHandler(FilterGames);
            FilterTextBox.TextChanged += new EventHandler(FilterGames);
            GameType.SelectedIndexChanged += new EventHandler(GameType_SelectedIndexChanged);
            QuickBtn.Click += new EventHandler(QuickBtn_Click);
            HostBtn.Click += new EventHandler(HostBtn_Click);
            DeckBtn.Click += new EventHandler(DeckBtn_Click);
            ReplaysBtn.Click += new EventHandler(ReplaysBtn_Click);
            ProfileBtn.Click += new EventHandler(ProfileBtn_Click);
            OptionsBtn.Click += new EventHandler(OptionsBtn_Click);

            Program.ServerConnection.AddRooms += new NetClient.ServerRooms(OnRoomsList);
            Program.ServerConnection.AddRoom += new NetClient.ServerRooms(OnRoomCreated);
            Program.ServerConnection.RemoveRoom += new NetClient.ServerResponse(OnRoomRemoved);
            Program.ServerConnection.UpdateRoomStatus += new NetClient.ServerResponse(OnRoomStarted);
            Program.ServerConnection.UpdateRoomPlayers += new NetClient.ServerResponse(OnRoomPlayersUpdate);

            Username.Text = Program.UserInfo.Username;

            if (Directory.Exists(Program.Config.LauncherDir + "deck/"))
            {
                string[] decks = Directory.GetFiles(Program.Config.LauncherDir + "deck/");
                foreach (string deck in decks)
                    DeckSelect.Items.Add(Path.GetFileNameWithoutExtension(deck));
            }
            DeckSelect.Text = Program.Config.DefualtDeck;

        }


        public void RequestUserWLD()
        {
                Program.ServerConnection.SendPacket("WLD");
        }

        private void DeckSelect_SelectedValueChanged(object sender, EventArgs e)
        {
            Program.Config.DefualtDeck = DeckSelect.SelectedItem.ToString();
            Program.Config.Save(Program.ConfigurationFilename);
        }

        private void GameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterGames(null,EventArgs.Empty);
            if (GameType.Text == "Ranked")
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
            MessageBox.Show("Not Implemented");

        }

        private void OptionsBtn_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void QuickBtn_Click(object sender, EventArgs e)
        {
            Host form = new Host();
            form.CardRules.Text = Program.Config.CardRules;
            form.Mode.Text = Program.Config.Mode;
            form.Priority.Checked = Program.Config.EnablePrority;
            form.CheckDeck.Checked = Program.Config.DisableCheckDeck;
            form.ShuffleDeck.Checked = Program.Config.DisableShuffleDeck;
            form.LifePoints.Text = Program.Config.Lifepoints;
            form.GameName.Text = Program.Config.GameName;
            
            LauncherHelper.GenerateConfig(form.GenerateURI(Program.Config.ServerAddress, Program.Config.GamePort.ToString(), (GameType.Text == "Ranked") ? true : false));
            LauncherHelper.RunGame("-j");
            return;
        }
        private void HostBtn_Click(object sender, EventArgs e)
        {
            Host form = new Host();
            if (GameType.Text == "Ranked")
            {
                form.Mode.Items.Clear();
                form.Mode.Items.AddRange(new object[] { "Match", "Tag" });
                form.Mode.Text = "Match";
                form.Priority.Enabled = false;
                form.ShuffleDeck.Enabled = false;
                form.CheckDeck.Enabled = false;
                form.LifePoints.Enabled = false;
                form.CardRules.Items.Clear();
                form.CardRules.Items.AddRange(new object[] { "OCG/TCG", "TCG", "OCG" });
            }

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LauncherHelper.GenerateConfig(form.GenerateURI(Program.Config.ServerAddress, Program.Config.GamePort.ToString(), (GameType.Text == "Ranked") ? true : false));
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

            string rule = "TCG/OCG";
            if (room.Rule == 1) rule = "TCG";
            if (room.Rule == 0) rule = "OCG";
            if (room.Rule == 4) rule = "Anime";
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
                (room.Mode == 2 ? Color.LightGreen :
                (room.Mode == 1 ? Color.LightSteelBlue :
                Color.LightBlue)));


            if (FilterActive.Checked)
            {
                if (m_rooms[room.RoomName].SubItems[4].Text.Contains("Waiting") &&
                    m_rooms[room.RoomName].SubItems[1].Text.Contains(GameType.Text))
                {
                    if (m_rooms[room.RoomName].SubItems[5].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        m_rooms[room.RoomName].SubItems[1].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                    {
                        listRooms.Items.Add(m_rooms[room.RoomName]);
                    }
                }
            }
            else
            {
                if (m_rooms[room.RoomName].SubItems[1].Text.Contains(GameType.Text))
                {
                    if (m_rooms[room.RoomName].SubItems[5].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        m_rooms[room.RoomName].SubItems[1].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                        FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                    {
                        listRooms.Items.Add(m_rooms[room.RoomName]);
                    }
                }
            }


        }

        public void FilterGames(object sender, EventArgs e)
        {
            listRooms.Items.Clear();
            foreach (string item in ObjectKeys())
            {
                if (FilterActive.Checked)
                {
                    if (m_rooms[item].SubItems[4].Text.Contains("Waiting") &&
                        m_rooms[item].SubItems[1].Text.Contains(GameType.Text))
                    {
                        if (m_rooms[item].SubItems[5].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            m_rooms[item].SubItems[1].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                        {
                            listRooms.Items.Add(m_rooms[item]);
                        }
                    }
                }
                else
                {
                    if (m_rooms[item].SubItems[1].Text.Contains(GameType.Text))
                    {
                        if (m_rooms[item].SubItems[5].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            m_rooms[item].SubItems[1].Text.ToLower().Contains(FilterTextBox.Text.ToLower()) ||
                            FilterTextBox.Text == "Search" || FilterTextBox.Text == "")
                        {
                            listRooms.Items.Add(m_rooms[item]);
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
                string[] players = m_rooms[item].SubItems[5].Text.Split(',');
                playercount = playercount + players.Length;
                if (m_rooms[item].SubItems[4].Text == "Waiting") openroom++;
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
            ListViewItem item = m_rooms[roomname];

            item.BackColor = Color.LightGray;
            item.SubItems[4].Text = "Started";
            if (FilterActive.Checked) listRooms.Items.Remove(item);
        }

        public void OnRoomRemoved(string roomname)
        {
            Invoke(new Action<string>(InternalRoomRemoved), roomname);

        }

        private void InternalRoomRemoved(string roomname)
        {
            if (!m_rooms.ContainsKey(roomname)) return;
            ListViewItem item = m_rooms[roomname];
            listRooms.Items.Remove(item);
            m_rooms.Remove(roomname);
            UpdateServerInfo();
        }

        public void OnRoomPlayersUpdate(string message)
        {
            string[] roomdata = message.Split('|');
            
            Invoke(new Action<string, string>(InternalRoomPlayersUpdate), roomdata[0], (roomdata.Length > 1) ? roomdata[1] : "" );

        }

        private void InternalRoomPlayersUpdate(string roomname, string players)
        {
            if (!m_rooms.ContainsKey(roomname)) return;
            ListViewItem item = m_rooms[roomname];

            item.SubItems[5].Text = players;

            UpdateServerInfo();
        }

        public void LoadRoom(object sender, EventArgs e)
        {
            ListViewItem item = listRooms.SelectedItems[0];
            LauncherHelper.GenerateConfig(item.SubItems[6].Text);
            LauncherHelper.RunGame("-j");
        }

    }
}
