using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using YGOPro_Launcher.Chat.Enums;
using YGOPro_Launcher.Config;
using System.Diagnostics;
using YgoServer.NetworkData;

namespace YGOPro_Launcher.Chat
{
    public partial class NewChat_frm : Form
    {
        private readonly Dictionary<string, UserData> _userData = new Dictionary<string, UserData>();
        private readonly Dictionary<string, PmWindow_frm> _pmWindows = new Dictionary<string, PmWindow_frm>();
        public bool autoscroll = true;
        public bool joinchannel = false;

        public NewChat_frm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            //chat packets
            Program.ChatServer.Login += LoginCheck;
            Program.ChatServer.UserList += CreateUserList;
            Program.ChatServer.AddUser += AddUser;
            Program.ChatServer.RemoveUser += RemoveUser;
            Program.ChatServer.FriendList += CreateFriendList;
            Program.ChatServer.JoinChannel += ChannelAccept;
            Program.ChatServer.Message += WriteMessage;
            Program.ChatServer.Error += WriteMessage;
            Program.ChatServer.DuelRequest += HandleDuelRequest;
            Program.ChatServer.TeamRequest += HandleTeamRequest;

            //form events
            UserSearch.Enter += UserSearch_Enter;
            UserSearch.Leave += UserSearch_Leave;
            UserSearch.TextChanged += UserSearch_TextChanged;
            ChatInput.KeyPress += ChatInput_KeyPress;
            UserList.DoubleClick += List_DoubleClick;
            FriendList.DoubleClick += List_DoubleClick;
            TeamList.DoubleClick += List_DoubleClick;
            ApplyOptionEvents();

            UserList.MouseUp += UserList_MouseUp;
            FriendList.MouseUp += FriendList_MouseUp;
            TeamList.MouseUp += TeamList_MouseUp;
            IgnoreList.MouseUp += IgnoreList_MouseUp;

            //custom form drawing
            UserList.DrawItem += UserList_DrawItem;
            FriendList.DrawItem += DrawList_OnlineOffline;
            TeamList.DrawItem += DrawList_OnlineOffline;

            ChatHelper.LoadChatTags();
            
            LoadIgnoreList();
            ApplyTranslations();
            ApplyChatSettings();
        }

        private void ApplyOptionEvents()
        {
            foreach (var checkBox in tableLayoutPanel14.Controls.OfType<CheckBox>())
            {
                checkBox.CheckStateChanged += Option_CheckStateChanged;
            }

            foreach (FontFamily fontFamily in FontFamily.Families.Where(fontFamily => fontFamily.IsStyleAvailable(FontStyle.Regular)))
            {
                FontList.Items.Add(fontFamily.Name);
            }

            FontList.SelectedIndexChanged += FontSettings_Changed;
            FontSize.ValueChanged += FontSettings_Changed;
        }

        public void ApplyChatSettings()
        {
            ChatInput.BackColor = Program.Config.ChatBGColor.ToColor();
            ChatInput.ForeColor = Program.Config.NormalTextColor.ToColor();
            UserSearch.BackColor = Program.Config.ChatBGColor.ToColor();

            UserList.BackColor = Program.Config.ChatBGColor.ToColor();
            FriendList.BackColor = Program.Config.ChatBGColor.ToColor();
            TeamList.BackColor = Program.Config.ChatBGColor.ToColor();
            IgnoreList.BackColor = Program.Config.ChatBGColor.ToColor();
            IgnoreList.ForeColor = Program.Config.NormalTextColor.ToColor();

            BackgroundColorBtn.BackColor = Program.Config.ChatBGColor.ToColor();
            NormalTextColorBtn.BackColor = Program.Config.NormalTextColor.ToColor();
            Level99ColorBtn.BackColor = Program.Config.Level99Color.ToColor();
            Level2ColorBtn.BackColor = Program.Config.Level2Color.ToColor();
            Level1ColorBtn.BackColor = Program.Config.Level1Color.ToColor();
            NormalUserColorBtn.BackColor = Program.Config.Level0Color.ToColor();
            ServerColorBtn.BackColor = Program.Config.ServerMsgColor.ToColor();
            MeColorBtn.BackColor = Program.Config.MeMsgColor.ToColor();
            JoinColorBtn.BackColor = Program.Config.JoinColor.ToColor();
            LeaveColorBtn.BackColor = Program.Config.LeaveColor.ToColor();
            SystemColorBtn.BackColor = Program.Config.SystemColor.ToColor();
            HideJoinLeavechk.Checked = Program.Config.HideJoinLeave;
            Colorblindchk.Checked = Program.Config.ColorBlindMode;
            Timestampchk.Checked = Program.Config.ShowTimeStamp;
            DuelRequestchk.Checked = Program.Config.RefuseDuelRequests;
            pmwindowchk.Checked = Program.Config.PmWindows;
            usernamecolorchk.Checked = Program.Config.UsernameColors;
            refuseteamchk.Checked = Program.Config.RefuseTeamInvites;

            if (!string.IsNullOrEmpty(Program.Config.ChatFont))
            {
                if (FontList.Items.Contains(Program.Config.ChatFont))
                {
                    FontList.SelectedItem = Program.Config.ChatFont;
                }
            }

            FontSize.Value = Program.Config.ChatSize;
        }

        public void ApplyTranslations()
        {
            LanguageInfo lang = Program.LanguageManager.Translation;
            //options
            groupBox7.Text = lang.chatoptionsGb2;
            groupBox5.Text = lang.chatoptionsGb3;
            HideJoinLeavechk.Text = lang.chatoptionsLblHideJoinLeave;
            Colorblindchk.Text = lang.chatoptionsLblColorBlindMode;
            Timestampchk.Text = lang.chatoptionsLblShowTimeStamp;
            DuelRequestchk.Text = lang.chatoptionsLblRefuseDuelRequests;
            label13.Text = lang.chatoptionsLblChatBackground;
            label12.Text = lang.chatoptionsLblNormalText;
            label11.Text = lang.chatoptionsLblLevel99;
            label10.Text = lang.chatoptionsLblLevel2;
            label9.Text = lang.chatoptionsLblLevel1;
            label4.Text = lang.chatoptionsLblNormalUser;
            label7.Text = lang.chatoptionsLblServerMessages;
            label8.Text = lang.chatoptionsLblMeMessage;
            label14.Text = lang.chatoptionsLblJoin;
            label15.Text = lang.chatoptionsLblLeave;
            label16.Text = lang.chatoptionsLblSystem;
        }

        public void LoadIgnoreList()
        {
            string[] users = Program.Config.IgnoreList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string user in users)
            {
                IgnoreList.Items.Add(user);
            }
        }

        public void Connect()
        {
            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo, "System", "Attempting to connect.", false));
            if (Program.ChatServer.Connect(Program.Config.ChatServerAddress, Program.Config.ChatPort))
            {
                Program.ChatServer.SendPacket("LOGIN||" + Program.UserInfo.Username + "||" + Program.Config.Password + "||" + LauncherHelper.GetUID());
                return;
            }

            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo, "System", "Failed to connect.", false));
        }

        private void LoginCheck(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(LoginCheck), message);
            }
            else
            {
                if (message == "Failed")
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo, "System", "Unable to login.", false));
                else if (message == "Banned")
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo, "System", "You are banned.", false));
                else if (message == "LoginDown")
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo, "System", "Login Server is currently down or locked.", false));
                else
                {
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo, "System", "Connected to the DevPro Chat Server.", false));
                    if (!Program.Config.ChatChannels.Any())
                    {
                        JoinChannel("DevPro-" + Program.Config.Language);
                    }
                    else
                    {
                        Program.Config.ChatChannels.ForEach(JoinChannel);
                    }

                    Program.ChatServer.SendPacket("GETDEVPOINTS");
                }
            }
        }

        private void JoinChannel(string channel)
        {
            Program.ChatServer.SendPacket("JOIN||" + channel);
            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo, "System", "Join request for " + channel + " has been sent.", false));
        }

        private void LeaveChannel(string channel)
        {
            Program.ChatServer.SendPacket("LEAVE||" + channel);
            if (GetChatWindow(channel) != null)
            {
                ChannelTabs.TabPages.Remove(GetChatWindow(channel));
                Program.Config.ChatChannels.Remove(channel);
                Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
            }
        }

        private void ChannelAccept(string channel)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(ChannelAccept), channel);
                return;
            }

            if (GetChatWindow(channel) == null)
            {
                ChannelTabs.TabPages.Add(new ChatWindow(channel, false));
                ChannelTabs.SelectedTab = GetChatWindow(channel);
                if (!Program.Config.ChatChannels.Contains(channel))
                {
                    Program.Config.ChatChannels.Add(channel);
                    Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
                }
            }

            if (!string.IsNullOrEmpty(Program.UserInfo.Team) && GetChatWindow(MessageType.Team.ToString()) == null)
            {
                ChannelTabs.TabPages.Add(new ChatWindow(MessageType.Team.ToString(), false));
            }

            WriteMessage(new ChatMessage(MessageType.Server, CommandType.None, Program.UserInfo, "Server", "Join request for " + channel + " accepted.", false));
            
            if (!joinchannel)
            {
                joinchannel = true;
                if (GetChatWindow(MessageType.System.ToString()) != null)
                {
                    ChannelTabs.TabPages.Remove(GetChatWindow(MessageType.System.ToString()));
                }
            }
        }

        public void WriteMessage(ChatMessage message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ChatMessage>(WriteMessage), message);
                return;
            }

            if(message.From != null && IgnoreList.Items.Contains(message.From.Username))
            {
                return;
            }

            ChatWindow window = null;
            if (message.Type == MessageType.Server || message.Type == MessageType.System)
            {
                window = (ChatWindow)ChannelTabs.SelectedTab;
                if (window == null)
                {
                    window = new ChatWindow(message.Type.ToString(), true) { issystemtab = true};
                    ChannelTabs.TabPages.Add(window);
                }
                else window.WriteMessage(message, autoscroll);
            }
            else if (message.Type == MessageType.Join || message.Type == MessageType.Leave || message.Channel == null)
            {
                window = GetChatWindow(message.Channel) ?? (ChatWindow)ChannelTabs.SelectedTab;
                if (window == null)
                {
                    window = new ChatWindow(message.Type.ToString(), true) { issystemtab = true };
                    ChannelTabs.TabPages.Add(window);
                }
            }
            else if (message.Type == MessageType.PrivateMessage && Program.Config.PmWindows)
            {
                if (_pmWindows.ContainsKey(message.Channel))
                {
                    _pmWindows[message.Channel].WriteMessage(message);
                }
                else
                {
                    _pmWindows.Add(message.Channel, new PmWindow_frm(message.Channel, true, Program.ChatServer));
                    _pmWindows[message.Channel].WriteMessage(message);
                    _pmWindows[message.Channel].Show();
                    _pmWindows[message.Channel].FormClosed += Chat_frm_FormClosed;
                }
            }
            else if (message.Type == MessageType.Team)
            {
                window = GetChatWindow(message.Type.ToString());
                if (window == null)
                {
                    window = new ChatWindow(message.Type.ToString(), message.Type == MessageType.PrivateMessage);
                    ChannelTabs.TabPages.Add(window);
                }
                else window.WriteMessage(message, autoscroll);
            }
            else
            {
                window = window ?? GetChatWindow(message.Channel);
                if (window == null)
                {
                    window = new ChatWindow(message.Channel, message.Type == MessageType.PrivateMessage);
                    ChannelTabs.TabPages.Add(window);
                }

                window.WriteMessage(message, autoscroll);
            }
        }

        private ChatWindow GetChatWindow(string name)
        {
            return ChannelTabs.TabPages.Cast<ChatWindow>().FirstOrDefault(x => x.Name == name);
        }

        private void Chat_frm_FormClosed(object sender, EventArgs e)
        {
            _pmWindows.Remove(((PmWindow_frm)sender).Name);
        }

        private void UserSearch_Enter(object sender, EventArgs e)
        {
            if (UserSearch.Text == "Search")
            {
                UserSearch.Text = "";
                UserSearch.ForeColor = Program.Config.NormalTextColor.ToColor();
            }
        }

        private void UserSearch_Leave(object sender, EventArgs e)
        {
            if (UserSearch.Text == "")
            {
                UserSearch.Text = "Search";
                UserSearch.ForeColor = SystemColors.WindowFrame;
            }
        }
        private void UserSearch_TextChanged(object sender, EventArgs e)
        {
            IEnumerable<string> users = _userData.Keys;
            if (UserSearch.Text != "" && UserSearch.Text != "Search")
            {
                users = users.Where(user => user.ToLower().Contains(UserSearch.Text.ToLower()));
            }

            UserList.Items.Clear();
            UserList.Items.AddRange(users.ToArray<object>());
        }

        private void CreateUserList(string userlist)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(CreateUserList), userlist);
            }
            else
            {
                string[] parts = userlist.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                string[] users = parts[0].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string[] info in users.Select(user => user.Split(',')))
                {
                    if (_userData.ContainsKey(info[0]))
                        _userData[info[0]] = new UserData { Username = info[0], Rank = Int32.Parse(info[1]), LoginID = Int32.Parse(info[2]) };
                    else
                        _userData.Add(info[0], new UserData { Username = info[0], Rank = Int32.Parse(info[1]), LoginID = Int32.Parse(info[2]) });
                }

                UserList.Items.AddRange(_userData.Keys.ToArray<object>());
            }
        }
        private void AddUser(string userinfo)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddUser), userinfo);
                return;
            }

            string[] parts = userinfo.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

            string[] info = parts[0].Split(',');

            if (!_userData.ContainsKey(info[0]))
                _userData.Add(info[0], new UserData { Username = info[0], Rank = Int32.Parse(info[1]), LoginID = Int32.Parse(info[2]), UserColor = Color.FromArgb(255, Convert.ToInt32(info[3]), Convert.ToInt32(info[4]), Convert.ToInt32(info[5])) });
            else
                _userData[info[0]] = new UserData { Username = info[0], Rank = Int32.Parse(info[1]), LoginID = Int32.Parse(info[2]), UserColor = Color.FromArgb(255, Convert.ToInt32(info[3]), Convert.ToInt32(info[4]), Convert.ToInt32(info[5])) };
            
            if (info[0] == Program.UserInfo.Username)
            {
                var user = _userData[info[0]];
                Program.UserInfo.Rank = user.Rank;
                Program.UserInfo.UserColor = user.UserColor;
                Program.UserInfo.Team = info[6];
                Program.UserInfo.TeamRank = Convert.ToInt32(info[7]);
                if (Program.UserInfo.Team != string.Empty)
                {
                    if (GetChatWindow(MessageType.Team.ToString()) == null)
                    {
                        ChannelTabs.TabPages.Add(new ChatWindow(MessageType.Team.ToString(), false));
                        TeamNameLabel.Text = "Team: " + info[6];
                        Program.ChatServer.SendPacket("TEAMUSERS");
                    }
                }
            }

            if (info[6] != string.Empty && info[6] == Program.UserInfo.Team)
            {
                if (!TeamList.Items.Contains(info[0]))
                {
                    TeamList.Items.Add(info[0]);
                }
            }
            else if (TeamList.Items.Contains(info[0]))
            {
                TeamList.Items.Remove(info[0]);
            }

            if (!Program.Config.HideJoinLeave)
            {
                WriteMessage(new ChatMessage(MessageType.Join, CommandType.None, Program.UserInfo, null, string.Format("{0}{1} has joined the channel.", (FriendList.Items.Contains(info[0]) ? "Your friend " : string.Empty), info[0]), false));
            }
            else if (FriendList.Items.Contains(info[0]))
            {
                WriteMessage(new ChatMessage(MessageType.Join, CommandType.None, Program.UserInfo, null, string.Format("Your friend {0} has logged in.", info[0]), false));
            }

            if (UserSearch.Text == "" || UserSearch.Text == "Search" || info[0].ToLower().Contains(UserSearch.Text.ToLower()))
            {
                if (!UserList.Items.Contains(info[0]))
                {
                    UserList.Items.Add(info[0]);
                }
            }
        }

        private void RemoveUser(string userinfo)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(RemoveUser), userinfo);
                return;
            }

            string[] parts = userinfo.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            string user = parts[0];
            if (!Program.Config.HideJoinLeave)
            {
                WriteMessage(new ChatMessage(MessageType.Leave, CommandType.None, Program.UserInfo, null, string.Format("{0}{1} has left the channel.", (FriendList.Items.Contains(user) ? "Your friend " : string.Empty), user), false));
            }
            else if (FriendList.Items.Contains(user))
            {
                WriteMessage(new ChatMessage(MessageType.Leave, CommandType.None, Program.UserInfo, null, string.Format("Your friend {0} has logged out.", user), false));
            }

            if (_userData.ContainsKey(user))
            {
                if (_userData[user].LoginID == Int32.Parse(parts[1]))
                {
                    if (UserList.Items.Contains(user))
                        UserList.Items.Remove(user);
                    _userData.Remove(user);
                }
            }
        }

        public void CreateFriendList(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(CreateFriendList), message);
                return;
            }

            if (message == "not found")
            {
                return;
            }

            FriendList.Items.Clear();
            FriendList.Items.AddRange(message.Split(',').ToArray<object>());
        }

        private void UserList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index < 0 && index >= UserList.Items.Count)
            {
                e.DrawFocusRectangle();
                return;
            }
            
            string text = UserList.Items[index].ToString();
            Graphics g = e.Graphics;
            if (!_userData.ContainsKey(text))
            {
                g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black) : new SolidBrush(Color.Blue)) : new SolidBrush(Program.Config.ChatBGColor.ToColor()), e.Bounds);
                g.DrawString(text, e.Font, (selected) ? Brushes.White : Brushes.Black, UserList.GetItemRectangle(index).Location);
                e.DrawFocusRectangle();
                return;
            }

            g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black) : new SolidBrush(Color.Blue)) : new SolidBrush(Program.Config.ChatBGColor.ToColor()), e.Bounds);

            if (_userData[text].Rank > 0)
            {
                // Print text
                g.DrawString("[", e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : new SolidBrush(Program.Config.NormalTextColor.ToColor())),
                    UserList.GetItemRectangle(index).Location);

                g.DrawString("Dev", e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : ChatMessage.GetUserColor(_userData[text].Rank)),
                  new Point(UserList.GetItemRectangle(index).Location.X + (int)g.MeasureString("[",e.Font).Width - 1 ,UserList.GetItemRectangle(index).Location.Y));
                g.DrawString("]", e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : new SolidBrush(Program.Config.NormalTextColor.ToColor())),
                    new Point(UserList.GetItemRectangle(index).Location.X + (int)g.MeasureString("[Dev", e.Font).Width, UserList.GetItemRectangle(index).Location.Y));
                if (_userData[text].UserColor.ToArgb() == Color.Black.ToArgb())
                {
                    g.DrawString(text, e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : new SolidBrush(Program.Config.NormalTextColor.ToColor())),
                        new Point(UserList.GetItemRectangle(index).Location.X + (int)g.MeasureString("[Dev]", e.Font).Width, UserList.GetItemRectangle(index).Location.Y));
                }
                else
                {
                    g.DrawString(text, e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : new SolidBrush(_userData[text].UserColor)),
                        new Point(UserList.GetItemRectangle(index).Location.X + (int)g.MeasureString("[Dev]", e.Font).Width, UserList.GetItemRectangle(index).Location.Y));
                }
            }
            else
            {
                if (_userData[text].UserColor.ToArgb() == Color.Black.ToArgb())
                {
                    // Print text
                    g.DrawString(text, e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : new SolidBrush(Program.Config.NormalTextColor.ToColor())),
                        UserList.GetItemRectangle(index).Location);
                }
                else
                {
                    // Print text
                    g.DrawString(text, e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : new SolidBrush(_userData[text].UserColor)),
                        UserList.GetItemRectangle(index).Location);
                }
                
            }

            e.DrawFocusRectangle();
        }

        private void DrawList_OnlineOffline(object sender, DrawItemEventArgs e)
        {
            var list = (ListBox)sender;
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < list.Items.Count)
            {
                string text = list.Items[index].ToString();
                Graphics g = e.Graphics;

                g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black) : new SolidBrush(Color.Blue)) : new SolidBrush(Program.Config.ChatBGColor.ToColor()), e.Bounds);

                //// Print text
                g.DrawString((Program.Config.ColorBlindMode ? (_userData.ContainsKey(text) ? text + " (Online)" : text + " (Offline)") : text), e.Font,
                    (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : (_userData.ContainsKey(text) ? Brushes.Green : Brushes.Red)),
                    list.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        private void ApplyNewColor(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectcolor = new ColorDialog
                {
                    Color = button.BackColor,
                    AllowFullOpen = true
                };

            if (selectcolor.ShowDialog() == DialogResult.OK)
            {
                switch (button.Name)
                {
                    case "BackgroundColorBtn":
                        Program.Config.ChatBGColor = new SerializableColor(selectcolor.Color);
                        break;
                    case "SystemColorBtn":
                        Program.Config.SystemColor = new SerializableColor(selectcolor.Color);
                        break;
                    case "LeaveColorBtn":
                        Program.Config.LeaveColor = new SerializableColor(selectcolor.Color);
                        break;
                    case "JoinColorBtn":
                        Program.Config.JoinColor = new SerializableColor(selectcolor.Color);
                        break;
                    case "MeColorBtn":
                        Program.Config.MeMsgColor = new SerializableColor(selectcolor.Color);
                        break;
                    case "ServerColorBtn":
                        Program.Config.ServerMsgColor = new SerializableColor(selectcolor.Color);
                        break;
                    case "NormalUserColorBtn":
                        Program.Config.Level0Color = new SerializableColor(selectcolor.Color);
                        break;
                    case "Level1ColorBtn":
                        Program.Config.Level1Color = new SerializableColor(selectcolor.Color);
                        break;
                    case "Level2ColorBtn":
                        Program.Config.Level2Color = new SerializableColor(selectcolor.Color);
                        break;
                    case "Level99ColorBtn":
                        Program.Config.Level99Color = new SerializableColor(selectcolor.Color);
                        break;
                    case "NormalTextColorBtn":
                        Program.Config.NormalTextColor = new SerializableColor(selectcolor.Color);
                        break;
                }

                button.BackColor = selectcolor.Color;
                Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
                ApplyChatSettings();
            }
        }
        
        private bool HandleCommand(string part, ChatWindow selectedTab)
        {
            var cmd = part.Substring(1).ToLower();
            switch(cmd)
            {
                case "me":
                    if (selectedTab == null)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "No channel Selected."));
                        return false;
                    }
                    
                    var isTeam = selectedTab.Name == MessageType.Team.ToString();
                    Program.ChatServer.SendMessage(isTeam ? MessageType.Team : MessageType.Message, CommandType.Me, selectedTab.Name, ChatInput.Text.Substring(part.Length + 1));
                    break;
                case "join":
                    JoinChannel(ChatInput.Text.Substring(part.Length).Trim());
                    break;
                case "leave":
                    if (selectedTab == null)
                    {
                        return false;
                    }
                    
                    if (selectedTab.isprivate)
                    {
                        ChannelTabs.TabPages.Remove(selectedTab);
                    }
                    else
                    {
                        LeaveChannel(selectedTab.Name);
                    }
                    
                    break;
                case "users":
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "There's " + _userData.Count + " users online."));
                    break;
                case "ping":
                    Program.ChatServer.PingRequest = DateTime.Now;
                    Program.ChatServer.SendPacket("PING");
                    break;
                case "pinggame":
                    Program.ServerConnection.pingrequest = DateTime.Now;
                    Program.ServerConnection.SendPacket(ServerPackets.Ping);
                    break;
                case "autoscroll":
                    autoscroll = !autoscroll;
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, (autoscroll ? "AutoScroll Enabled." : "AutoScroll Disabled.")));
                    break;
                case "help":
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Basic Commands --"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/admin - Get admin list"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/users - Get user count"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/me - Displays Username + Message"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/join - Join a other channel"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/leave - Leave the current channel"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/autoscroll - Enable/Disable autoscroll"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/ping - Ping the chat server"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/pinggame - Ping the game server"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/help - Displays this list your reading now"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/uptime - Displays how long the server has been online"));

                    if (Program.UserInfo.Rank != 0)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Donator Commands --"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "[red][/red] [blue][/blue] [green][/green]- Color tags, wrap your text with them to change its color"));
                    }

                    if (Program.UserInfo.Rank > 0)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Level 1 Commands --"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/kick username reason - Kick a user"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/msg - Send a server message"));
                    }

                    if (Program.UserInfo.Rank > 1)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Level 2 Commands --"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/ban username reason - Ban a user"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/unban username - Unban a user"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/ip username - Get a users IP"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/banip ip - Ban a IP"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/unbanip ip - Unbans IP"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/getbanlist - Get ban list"));
                    }

                    if (Program.UserInfo.Rank == 99)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Level 99 Commands --"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/op username level - Set a users level"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/addpoints amount of DevPoints - Give a user DevPoints"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/removepoints amount of DevPoints - Remove DevPoints from a user"));
                    }

                    if (Program.UserInfo.TeamRank >= 0 && Program.UserInfo.Team != string.Empty)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Team Commands --"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/leaveteam - leave the team"));
                    }

                    if (Program.UserInfo.TeamRank >= 1)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Team User Level 1 Commands --"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/teamadd username - add a user to the team"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/teamremove username - remove a user from the team"));
                    }

                    if (Program.UserInfo.TeamRank == 99)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Team User Level 99 Commands --"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/teamdisband - disbands the team"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/teamop username level - promote user in the team"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/teamchangeleader username - change the leader of a team"));
                    }
                    
                    break;
                case "teamdisband":
                    if (MessageBox.Show("Are you sure?", "Confirm team disband", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Program.ChatServer.SendPacket("ADMIN||" + cmd.ToUpper() + "||" + ChatInput.Text.Substring(part.Length).Trim());
                    }
                    
                    break;
                case "admin":
                    string admins = string.Join(", ", _userData.Where(x => x.Value.Rank > 0).Select(x => x.Key));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "The following admins are online: " + admins + "."));
                    break;
                default:
                    Program.ChatServer.SendPacket("ADMIN||" + cmd.ToUpper() + "||" + ChatInput.Text.Substring(part.Length).Trim());
                    break;
            }

            return true;
        }

        private void ChatInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13 || string.IsNullOrWhiteSpace(ChatInput.Text))
            {
                return;
            }
            
            string[] parts = ChatInput.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Here, parts could only be empty if the whole input would be whitespace, which is already checked above.
            ////if (parts.Length == 0)
            ////{
            ////    return;
            ////}

            var selectedTab = (ChatWindow)ChannelTabs.SelectedTab;
            if (parts[0].StartsWith("/"))
            {
                if(!HandleCommand(parts[0], selectedTab))
                {
                    return;
                }
            }
            else
            {
                if (selectedTab == null)
                {
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "No channel Selected."));
                    return;
                }
                
                if (selectedTab.issystemtab)
                {
                    ChatInput.Clear();
                    return;
                }

                if (selectedTab.isprivate)
                {
                    WriteMessage(new ChatMessage(MessageType.Message, CommandType.None, Program.UserInfo, selectedTab.Name, ChatInput.Text, false));
                    //Program.ChatServer.SendPacket("MSG||" + selectedTab.Name + "||" + (int)MessageType.PrivateMessage + "||" + LauncherHelper.StringToBase64(ChatInput.Text));
                    Program.ChatServer.SendMessage(MessageType.PrivateMessage, CommandType.None, selectedTab.Name, ChatInput.Text);
                }
                else
                {
                    var isTeam = selectedTab.Name == MessageType.Team.ToString();
                    Program.ChatServer.SendMessage(isTeam ? MessageType.Team : MessageType.Message, CommandType.None, selectedTab.Name, ChatInput.Text);
                }
            }

            ChatInput.Clear();
            e.Handled = true;
        }

        private void Option_CheckStateChanged(object sender, EventArgs e)
        {
            var check = (CheckBox)sender;
            switch (check.Name)
            {
                case "Colorblindchk":
                    Program.Config.ColorBlindMode = check.Checked;
                    break;
                case "Timestampchk":
                    Program.Config.ShowTimeStamp = check.Checked;
                    break;
                case "DuelRequestchk":
                    Program.Config.RefuseDuelRequests = check.Checked;
                    break;
                case "HideJoinLeavechk":
                    Program.Config.HideJoinLeave = check.Checked;
                    break;
                case "usernamecolorchk":
                    Program.Config.UsernameColors = usernamecolorchk.Checked;
                    break;
                case "refuseteamchk":
                    Program.Config.RefuseTeamInvites = refuseteamchk.Checked;
                    break;
                case "pmwindowchk":
                    Program.Config.PmWindows = check.Checked;
                    if (Program.Config.PmWindows)
                    {
                        ChannelTabs.TabPages
                                   .Cast<ChatWindow>()
                                   .Where(x => x.isprivate)
                                   .ToList()
                                   .ForEach(window => ChannelTabs.TabPages.Remove(window));
                    }
                    else
                    {
                        _pmWindows.Values.ToList().ForEach(x => x.Close());
                    }
                    
                    break;
            }

            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
        }

        private void UserList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = UserList.IndexFromPoint(e.Location);

                if (index == -1)
                {
                    return;
                }

                UserList.SelectedIndex = index;

                if (UserList.SelectedItem == null)
                {
                    return;
                }

                var mnu = new ContextMenuStrip();
                var mnuprofile = new ToolStripMenuItem(Program.LanguageManager.Translation.chatViewProfile);
                var mnuduel = new ToolStripMenuItem(Program.LanguageManager.Translation.chatRequestDuel);
                var mnufriend = new ToolStripMenuItem(Program.LanguageManager.Translation.chatAddFriend);
                var mnuignore = new ToolStripMenuItem(Program.LanguageManager.Translation.chatIgnoreUser);
                var mnukick = new ToolStripMenuItem("Kick");
                var mnuban = new ToolStripMenuItem("Ban");

                mnukick.Click += KickUser;
                mnuban.Click += BanUser;
                mnuprofile.Click += ViewProfile;
                mnuduel.Click += RequestDuel;
                mnufriend.Click += AddFriend;
                mnuignore.Click += IgnoreUser;

                mnu.Items.AddRange(new ToolStripItem[] { mnuprofile, mnuduel, mnufriend, mnuignore });

                if (Program.UserInfo.Rank > 0)
                    mnu.Items.Add(mnukick);
                if (Program.UserInfo.Rank > 1)
                    mnu.Items.Add(mnuban);

                mnu.Show(UserList, e.Location);
            }
        }

        private void BanUser(object sender, EventArgs e)
        {
            if (UserList.SelectedItem != null && MessageBox.Show("Are you sure you want to ban " + UserList.SelectedItem, "Ban User", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.ChatServer.SendPacket("ADMIN||BAN||" + UserList.SelectedItem);
            }
        }

        private void KickUser(object sender, EventArgs e)
        {
            if (UserList.SelectedItem == null)
            {
                return;
            }

            Program.ChatServer.SendPacket("ADMIN||KICK||" + UserList.SelectedItem);
        }

        private void AddFriend(object sender, EventArgs e)
        {
            if (UserList.SelectedItem == null)
            {
                return;
            }

            if (UserList.SelectedItem.ToString() == Program.UserInfo.Username)
            {
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You cannot be your own friend."));
                return;
            }

            if (FriendList.Items.Cast<object>().Any(user => user.ToString().ToLower() == UserList.SelectedItem.ToString().ToLower()))
            {
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, UserList.SelectedItem + " is already your friend."));
                return;
            }

            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, UserList.SelectedItem + " has been added to your friend list."));
            FriendList.Items.Add(UserList.SelectedItem.ToString());
            Program.ChatServer.SendPacket("ADDFRIEND||" + UserList.SelectedItem);
        }

        private void IgnoreUser(object sender, EventArgs e)
        {
            if (UserList.SelectedItem.ToString() == Program.UserInfo.Username)
            {
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You cannot ignore yourself."));
                return;
            }

            if (IgnoreList.Items.Contains(UserList.SelectedItem.ToString()))
            {
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, UserList.SelectedItem + " is already on your ignore list."));
                return;
            }

            IgnoreList.Items.Add(UserList.SelectedItem.ToString());
            SaveIgnoreList();
            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, UserList.SelectedItem + " has been added to your ignore list."));
        }

        private void FriendList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            int index = FriendList.IndexFromPoint(e.Location);

            if (index == -1)
            {
                return;
            }

            FriendList.SelectedIndex = index;

            if (FriendList.SelectedItem == null)
            {
                return;
            }

            var mnu = new ContextMenuStrip();
            var mnuremovefriend = new ToolStripMenuItem(Program.LanguageManager.Translation.chatRemoveFriend);
            var mnuprofile = new ToolStripMenuItem(Program.LanguageManager.Translation.chatViewProfile);
            var mnuduel = new ToolStripMenuItem(Program.LanguageManager.Translation.chatRequestDuel);

            mnuremovefriend.Click += RemoveFriend;
            mnuprofile.Click += ViewProfile;
            mnuduel.Click += RequestDuel;

            mnu.Items.AddRange(new ToolStripItem[] { mnuprofile, mnuduel, mnuremovefriend });

            mnu.Show(FriendList, e.Location);
        }

        private void TeamList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = TeamList.IndexFromPoint(e.Location);

                if (index == -1)
                {
                    return;
                }

                TeamList.SelectedIndex = index;

                if (TeamList.SelectedItem == null)
                {
                    return;
                }

                var mnu = new ContextMenuStrip();
                var mnuprofile = new ToolStripMenuItem(Program.LanguageManager.Translation.chatViewProfile);
                var mnuduel = new ToolStripMenuItem(Program.LanguageManager.Translation.chatRequestDuel);
                var mnuremoveteam = new ToolStripMenuItem("Remove from Team");

                //mnuremovefriend.Click += new EventHandler(RemoveFriend);
                mnuprofile.Click += ViewProfile;
                mnuduel.Click += RequestDuel;
                mnuremoveteam.Click += RemoveFromTeam;

                mnu.Items.AddRange(
                    Program.UserInfo.TeamRank > 0
                        ? new ToolStripItem[] { mnuprofile, mnuduel, mnuremoveteam }
                        : new ToolStripItem[] { mnuprofile, mnuduel });

                mnu.Show(TeamList, e.Location);
            }
        }

        private void RemoveFromTeam(object sender, EventArgs e)
        {
            if (TeamList.SelectedIndex == -1)
            {
                return;
            }

            if (MessageBox.Show("Are you sure?", "Remove User", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.ChatServer.SendPacket("ADMIN||TEAMREMOVE||" + TeamList.SelectedItem);
            }

        }

        private void RemoveFriend(object sender, EventArgs e)
        {
            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, FriendList.SelectedItem + " has been removed from your friendlist."));
            Program.ChatServer.SendPacket("REMOVEFRIEND||" + FriendList.SelectedItem);
            FriendList.Items.Remove(FriendList.SelectedItem.ToString());
        }

        private void ViewProfile(object sender, EventArgs e)
        {
            ListBox list = (UserListTabs.SelectedTab.Name == OnlineTab.Name ? UserList :(UserListTabs.SelectedTab.Name == TeamTab.Name ? TeamList: FriendList));
            if (list.SelectedItem == null)
                return;

            var profile = new Profile_frm(list.SelectedItem.ToString());
            profile.ShowDialog();
        }

        private void RequestDuel(object sender, EventArgs e)
        {
            ListBox list = (UserListTabs.SelectedTab.Name == OnlineTab.Name ? UserList : (UserListTabs.SelectedTab.Name == TeamTab.Name ? TeamList : FriendList));

            if (list.SelectedItem == null)
            {
                return;
            }

            if (list.SelectedItem.ToString() == Program.UserInfo.Username)
            {
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You cannot duel request your self."));
            }
            else
            {
                var form = new Host();
                Program.ChatServer.SendPacket("REQUESTDUEL||" + list.SelectedItem + "||" + form.GenerateGameString(false));
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "Duel request sent to " + list.SelectedItem + "."));
            }
        }

        private void HandleDuelRequest(string command)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(HandleDuelRequest), command);
                return;
            }

            string[] args = command.Split(new[] { "||" }, StringSplitOptions.None);
            string cmd = args[0];

            switch (cmd)
            {
                case "START":
                    LauncherHelper.GenerateConfig("ygopro:/" + Program.Config.ServerAddress + "/" + Program.Config.GamePort + "/" + args[1]);
                    LauncherHelper.RunGame("-j");
                    break;
                case "REQUEST":
                    if (Program.Config.RefuseDuelRequests)
                    {
                        Program.ChatServer.SendPacket("REFUSEDUEL||" + args[1]);
                        return;
                    }

                    RoomInfos info = RoomInfos.FromName(args[2], false);
                    var request = new DuelRequest_frm(
                        args[1]
                        + Program.LanguageManager.Translation.DuelReqestMessage
                        + Environment.NewLine
                        + Program.LanguageManager.Translation.DuelRequestMode
                        + RoomInfos.GameMode(info.mode)
                        + Program.LanguageManager.Translation.DuelRequestRules
                        + RoomInfos.GameRule(info.rule)
                        + Program.LanguageManager.Translation.DuelRequestBanlist
                        + LauncherHelper.GetBanListFromInt(info.banListType));

                    if (request.ShowDialog() == DialogResult.Yes)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You accepted " + args[1] + " duel request."));
                        Program.ChatServer.SendPacket("ACCEPTDUEL||" + args[1] + "||" + args[2]);
                    }
                    else
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You refused " + args[1] + " duel request."));
                        Program.ChatServer.SendPacket("REFUSEDUEL||" + args[1]);
                    }

                    break;
                case "REFUSE":
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, args[1] + " refused your duel request."));
                    break;
            }
        }

        private void HandleTeamRequest(string command)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(HandleTeamRequest), command);
            }
            else
            {
                string[] args = command.Split(new[] { "||" }, StringSplitOptions.None);
                string cmd = args[0];

                switch (cmd)
                {
                    case "JOIN":
                        if (Program.Config.RefuseTeamInvites)
                        {
                            Program.ChatServer.SendPacket("TEAMREFUSE||AUTO");
                            return;
                        }

                        if (MessageBox.Show("You have been invited to join the team " + args[1], "Team Request", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo.Username, "You have accepted the team invite to join " + args[1]));
                            Program.ChatServer.SendPacket("TEAMACCEPT");
                        }
                        else
                        {
                            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo.Username, "You have refused the team invite to join " + args[1]));
                            Program.ChatServer.SendPacket("TEAMREFUSE");
                        }

                        break;
                    case "LEAVE":
                        Program.UserInfo.Team = string.Empty;
                        Program.UserInfo.TeamRank = 0;
                        TeamNameLabel.Text = "Team: None";
                        TeamList.Items.Clear();
                        ChannelTabs.TabPages.Remove(GetChatWindow(MessageType.Team.ToString()));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo.Username, "You have left the team."));
                        break;
                    case "REMOVED":
                        Program.UserInfo.Team = string.Empty;
                        Program.UserInfo.TeamRank = 0;
                        TeamNameLabel.Text = "Team: None";
                        TeamList.Items.Clear();
                        ChannelTabs.TabPages.Remove(GetChatWindow(MessageType.Team.ToString()));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo.Username, "You have been removed from the team."));
                        break;
                    case "DISBAND":
                        foreach (string user in _userData.Keys.Where(user => _userData[user].Team == args[1]))
                        {
                            _userData[user].Team = string.Empty;
                            _userData[user].TeamRank = 0;
                        }

                        if (Program.UserInfo.Team == args[1])
                        {
                            Program.UserInfo.Team = string.Empty;
                            Program.UserInfo.TeamRank = 0;
                            TeamNameLabel.Text = "Team: None";
                            TeamList.Items.Clear();
                            ChannelTabs.TabPages.Remove(GetChatWindow(MessageType.Team.ToString()));
                        }

                        break;
                    case "USERS":
                        TeamList.Items.Clear();
                        string[] parts = args[1].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string user in parts)
                        {
                            TeamList.Items.Add(user);
                        }

                        break;
                    default:
                        WriteMessage(new ChatMessage(MessageType.Server, CommandType.None, Program.UserInfo.Username, "Unknown packet:" + command));
                        break;
                }
            }
        }

        private void IgnoreList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = IgnoreList.IndexFromPoint(e.Location);

                if (index == -1)
                {
                    return;
                }

                IgnoreList.SelectedIndex = index;

                var mnu = new ContextMenuStrip();
                var mnuremoveignore = new ToolStripMenuItem(Program.LanguageManager.Translation.chatRemoveIgnore);

                mnuremoveignore.Click += UnignoreUser;

                mnu.Items.Add(mnuremoveignore);

                mnu.Show(IgnoreList, e.Location);
            }
        }
        private void UnignoreUser(object sender, EventArgs e)
        {
            if (IgnoreList.SelectedItem == null)
            {
                return;
            }

            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, IgnoreList.SelectedItem + " has been removed from your ignore list."));
            IgnoreList.Items.Remove(IgnoreList.SelectedItem);
            SaveIgnoreList();
        }

        private void SaveIgnoreList()
        {
            string ignorestring = string.Join(", ", IgnoreList.Items);
            Program.Config.IgnoreList = ignorestring;
            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
        }

        private void FontSettings_Changed(object sender, EventArgs e)
        {
            var box = sender as ComboBox;
            var down = sender as NumericUpDown;
            if (box != null)
            {
                ComboBox fontstyle = box;
                Program.Config.ChatFont = fontstyle.SelectedItem.ToString();
            }
            else if (down != null)
            {
                NumericUpDown size = down;
                Program.Config.ChatSize = size.Value;
            }

            foreach (ChatWindow tab in ChannelTabs.TabPages)
            {
                tab.ApplyNewSettings();
            }

            foreach (var form in _pmWindows.Values)
            {
                form.ApplyNewSettings();
            }

            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
        }

        ////private void Chat_LinkClicked(object sender, LinkClickedEventArgs e)
        ////{
        ////    Process.Start(e.LinkText);
        ////}

        private void List_DoubleClick(object sender, EventArgs e)
        {
            ListBox list = (UserListTabs.SelectedTab.Name == OnlineTab.Name ?UserList :(UserListTabs.SelectedTab.Name == TeamTab.Name ? TeamList: FriendList));

            if (list.SelectedItem == null)
            {
                return;
            }

            if (Program.Config.PmWindows)
            {
                if (!_pmWindows.ContainsKey(list.SelectedItem.ToString()))
                {
                    _pmWindows.Add(list.SelectedItem.ToString(), new PmWindow_frm(list.SelectedItem.ToString(), true, Program.ChatServer));
                    _pmWindows[list.SelectedItem.ToString()].Show();
                    _pmWindows[list.SelectedItem.ToString()].FormClosed += Chat_frm_FormClosed;
                }
                else
                {
                    _pmWindows[list.SelectedItem.ToString()].BringToFront();
                }
            }
            else
            {
                if (GetChatWindow(list.SelectedItem.ToString()) == null)
                {
                    ChannelTabs.TabPages.Add(new ChatWindow(list.SelectedItem.ToString(), true));
                    ChannelTabs.SelectedTab = GetChatWindow(list.SelectedItem.ToString());
                }
                else
                {
                    ChannelTabs.SelectedTab = GetChatWindow(list.SelectedItem.ToString());
                }
            }
        }

        private void AddUserBtn_Click(object sender, EventArgs e)
        {
            if (Program.UserInfo.Team == string.Empty)
            {
                MessageBox.Show("You are not in a team.", "No u", MessageBoxButtons.OK);
                return;
            }

            if(Program.UserInfo.TeamRank <= 0)
            {
                MessageBox.Show("Your rank is too low.", "No u", MessageBoxButtons.OK);
                return;
            }
            
            var form = new Input_frm("Add Team Member","Enter Users name","Send","Cancel");
            form.InputBox.MaxLength = 14;

            if(form.ShowDialog() == DialogResult.OK)
            {
                Program.ChatServer.SendPacket("ADMIN||TEAMADD||" + form.InputBox.Text);
            }

        }

        private void TeamStatsbtn_Click(object sender, EventArgs e)
        {
            if (Program.UserInfo.Team == string.Empty)
            {
                MessageBox.Show("You are not in a team.", "No u", MessageBoxButtons.OK);
                return;
            }

            var form = new TeamProfile_frm(Program.UserInfo.Team);
            form.Show();
        }
    }
}
