using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Text;
using DevProLauncher.Network;
using DevProLauncher.Network.Enums;
using DevProLauncher.Network.Data;
using DevProLauncher.Helpers;
using DevProLauncher.Windows.MessageBoxs;
using System.Collections.Concurrent;

namespace DevProLauncher.Windows
{
    public partial class GameList_frm : Form
    {

        public Dictionary<string, RoomInfos> m_rooms;

        public GameList_frm(string ServerName)
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            this.Name = ServerName;
            this.Text = ServerName;


            m_rooms = new Dictionary<string, RoomInfos>();
            FilterActive.CheckedChanged += FilterGames;
            FilterTextBox.TextChanged += FilterGames;
            Program.DuelServer.addRooms += OnRoomsList;
            Program.DuelServer.createRoom += OnRoomCreate;
            Program.DuelServer.removeRoom += OnRoomRemoved;
            Program.DuelServer.updateRoomStatus += OnRoomStarted;
            Program.DuelServer.updateRoomPlayers += OnRoomPlayersUpdate;
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
            Random ran = new Random();
            Host form = new Host(Program.Server,false, false);
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
                form.CardRules.SelectedIndex = 2;
                if (form.Mode.Text == "Tag")
                    form.LifePoints.Text = "16000";
                else
                    form.LifePoints.Text = "8000";
            }
            else
            {
                if(Program.Config.Lifepoints != ((mode == "Tag") ? "16000":"8000"))
                {
                    if (MessageBox.Show(Program.LanguageManager.Translation.GameLPChange, Program.LanguageManager.Translation.hostLifep, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (mode == "Tag")
                            form.LifePoints.Text = "16000";
                        else
                            form.LifePoints.Text = "8000";

                    }
                }
            }

            RoomInfos userinfo = RoomInfos.FromName(form.GenerateURI(isranked));

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

            if (MatchedRooms.Count > 0)
            {
                int selectroom = ran.Next(MatchedRooms.Count);
                form.GameName = MatchedRooms[selectroom].roomName;
            }

            LauncherHelper.GenerateConfig(Program.Server,form.GenerateURI(isranked));
            LauncherHelper.RunGame("-j");
            return;
        }
        private void HostBtn_Click(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            Host form = new Host(Program.Server,false, (button.Name == "RankedHostBtn"));
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

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                if (m_rooms.ContainsKey(form.PasswordInput.Text))
                {
                    MessageBox.Show(Program.LanguageManager.Translation.GamePasswordExsists);
                    return;
                }
                LauncherHelper.GenerateConfig(Program.Server,form.GenerateURI((button.Name == "RankedHostBtn") ? true : false));
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
            string roomname = room.roomName;
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
                string[] players = m_rooms[item].playerList;
                playercount = playercount + players.Length;
                if (!m_rooms[item].hasStarted) openroom++;
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
            if (!m_rooms.ContainsKey(room.roomName))
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
            ListBox rooms = (ListBox)sender;
            if (rooms.SelectedIndex == -1)
                return;
            if (!m_rooms.ContainsKey(rooms.SelectedItem.ToString()))
                return;

            RoomInfos item = m_rooms[rooms.SelectedItem.ToString()];
            if (item.isLocked)
            {
                Input_frm form = new Input_frm("", Program.LanguageManager.Translation.GameEnterPassword, Program.LanguageManager.Translation.QuickHostBtn, Program.LanguageManager.Translation.optionBtnCancel);
                form.InputBox.MaxLength = 4;
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (form.InputBox.Text != item.roomName)
                    {
                        MessageBox.Show(Program.LanguageManager.Translation.GameWrongPassword);
                        return;
                    }
                }
                else
                { return; }
            }

            if (item.hasStarted)
            {
                MessageBox.Show("Spectating games in progress is unavailable.. Please join them before they start.");
                return;
            }

            LauncherHelper.GenerateConfig(Program.Server, item.GenerateURI());
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
                    mnu.Items.AddRange(new ToolStripItem[] { mnuSingle, mnuMatch, mnuTag });
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
            QuickBtn_MouseUp(QuickBtn, new MouseEventArgs(System.Windows.Forms.MouseButtons.Right,1,1,1,1));
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

            if (e.Index == -1)
                return;
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


            Rectangle Bounds = list.GetItemRectangle(index);
            SizeF GameNamesize = e.Graphics.MeasureString((info == null) ? "???" : info.roomName, e.Font);
            SizeF Rulesize = e.Graphics.MeasureString((info == null) ? "???" : RoomInfos.GameRule(info.rule), e.Font);
            SizeF playersSize = e.Graphics.MeasureString(playerstring, e.Font);
            SizeF infoListsize = e.Graphics.MeasureString((info == null) ? "???/???/???" : RoomInfos.GameMode(info.mode) + " / " + LauncherHelper.GetBanListFromInt(info.banListType) + " / " +(info.timer == 0 ? "3 mins" : "5 mins") , e.Font);
            SizeF lockedsize = e.Graphics.MeasureString((info == null) ? "???" : (info.isLocked ? Program.LanguageManager.Translation.GameLocked : Program.LanguageManager.Translation.GameOpen), e.Font);
            bool illegal = true;
            SolidBrush backgroundcolor = null;

            Size offset = new Size(5, 5);

            if (info == null)
            {
                backgroundcolor = new SolidBrush(Color.Red);
            }
            else
            {
                illegal = (info.rule <= 2 ? info.banListType > 0 : false) || info.isNoCheckDeck || info.isNoShuffleDeck || info.enablePriority || (info.mode == 2) ? info.startLp != 16000 : info.startLp != 8000 || info.startHand != 5 || info.drawCount != 1;
                backgroundcolor = new SolidBrush(info.hasStarted ? Color.LightGray :
                (illegal ? Color.LightCoral :
                (info.rule == 4 ? Color.Violet :
                (info.rule == 5 ? Color.Gold :
                (info.mode == 2 ? Color.LightGreen :
                (info.mode == 1 ? Color.LightSteelBlue :
                Color.LightBlue))))));
            }

            //draw item
            g.FillRectangle(backgroundcolor, e.Bounds);
            g.DrawLines((selected) ? new Pen(Brushes.Purple, 5) : new Pen(Brushes.Black, 5),
                new Point[] { new Point(Bounds.X, Bounds.Y), new Point(Bounds.X + Bounds.Width, Bounds.Y), new Point(Bounds.X + Bounds.Width, Bounds.Y + Bounds.Height), new Point(Bounds.X, Bounds.Y + Bounds.Height), new Point(Bounds.X, Bounds.Y) });
            //toplet
            g.DrawString((info == null) ? "???/???/???" : RoomInfos.GameMode(info.mode) + " / " + LauncherHelper.GetBanListFromInt(info.banListType) + " / " + (info.timer == 0 ? "3 mins" : "5 mins"), e.Font, Brushes.Black,
                list.GetItemRectangle(index).Location + offset);
            //topright
            g.DrawString((info == null) ? "???" : RoomInfos.GameRule(info.rule), e.Font, Brushes.Black,
                new Rectangle(Bounds.X + (Bounds.Width - (int)Rulesize.Width) - offset.Width, Bounds.Y + offset.Height, Bounds.Width, Bounds.Height));
            ////bottomright
            g.DrawString((info == null) ? "???" : (info.isLocked ? Program.LanguageManager.Translation.GameLocked:Program.LanguageManager.Translation.GameOpen), 
                e.Font, Brushes.Black,
                new Rectangle(Bounds.X + (Bounds.Width - (int)lockedsize.Width) - offset.Width, Bounds.Y + (Bounds.Height - (int)lockedsize.Height) - offset.Height, Bounds.Width, Bounds.Height));
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