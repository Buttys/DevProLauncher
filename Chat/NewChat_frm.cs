using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGOPro_Launcher.Chat.Enums;
using YGOPro_Launcher.Config;
using System.Diagnostics;

namespace YGOPro_Launcher.Chat
{
    public partial class NewChat_frm : Form
    {
        Dictionary<string, UserData> UserData = new Dictionary<string, UserData>();
        Dictionary<string, PmWindow_frm> PMWindows = new Dictionary<string, PmWindow_frm>();
        public bool autoscroll = true;
        public bool joinchannel = false;

        public NewChat_frm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            //chat packets
            Program.ChatServer.Login += new ChatClient.ServerResponse(LoginCheck);
            Program.ChatServer.UserList += new ChatClient.ServerResponse(CreateUserList);
            Program.ChatServer.AddUser += new ChatClient.ServerResponse(AddUser);
            Program.ChatServer.RemoveUser += new ChatClient.ServerResponse(RemoveUser);
            Program.ChatServer.FriendList += new ChatClient.ServerResponse(CreateFriendList);
            Program.ChatServer.JoinChannel += new ChatClient.ServerResponse(ChannelAccept);
            Program.ChatServer.Message += new ChatClient.ServerMessage(WriteMessage);
            Program.ChatServer.Error += new ChatClient.ServerMessage(WriteMessage);
            Program.ChatServer.DuelRequest += new ChatClient.ServerResponse(HandleDuelRequest);
            Program.ChatServer.TeamRequest += new ChatClient.ServerResponse(HandleTeamRequest);

            //form events
            UserSearch.Enter += new EventHandler(UserSearch_Enter);
            UserSearch.Leave += new EventHandler(UserSearch_Leave);
            UserSearch.TextChanged += new EventHandler(UserSearch_TextChanged);
            ChatInput.KeyPress += new KeyPressEventHandler(ChatInput_KeyPress);
            UserList.DoubleClick += List_DoubleClick;
            FriendList.DoubleClick += List_DoubleClick;
            TeamList.DoubleClick += List_DoubleClick;
            ApplyOptionEvents();


            UserList.MouseUp += new MouseEventHandler(UserList_MouseUp);
            FriendList.MouseUp += new MouseEventHandler(FriendList_MouseUp);
            TeamList.MouseUp += new MouseEventHandler(TeamList_MouseUp);
            IgnoreList.MouseUp += new MouseEventHandler(IgnoreList_MouseUp);

            //custom form drawing
            UserList.DrawItem += new DrawItemEventHandler(UserList_DrawItem);
            FriendList.DrawItem += new DrawItemEventHandler(DrawList_OnlineOffline);
            TeamList.DrawItem += new DrawItemEventHandler(DrawList_OnlineOffline);

            ChatHelper.LoadChatTags();
            
            LoadIgnoreList();
            ApplyTranslations();
            ApplyChatSettings();
        }
 
        private void ApplyOptionEvents()
        {
            foreach (Control control in tableLayoutPanel14.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox check = (CheckBox)control;
                    check.CheckStateChanged += new EventHandler(Option_CheckStateChanged);
                }
            }
            foreach (FontFamily fontFamily in  FontFamily.Families)
            {
                if(fontFamily.IsStyleAvailable(FontStyle.Regular))
                    FontList.Items.Add(fontFamily.Name);
            }

            FontList.SelectedIndexChanged += new EventHandler(FontSettings_Changed);
            FontSize.ValueChanged += new EventHandler(FontSettings_Changed);
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

            if (Program.Config.ChatFont != "")
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
            string[] users = Program.Config.IgnoreList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string user in users)
                IgnoreList.Items.Add(user);
        }

        public void Connect()
        {
            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo, "System", "Attempting to connect.", false));
            if (Program.ChatServer.Connect(Program.Config.ChatServerAddress, Program.Config.ChatPort))
            {
                Program.ChatServer.SendPacket("LOGIN||" + Program.UserInfo.Username + "||" + Program.Config.Password + "||" + LauncherHelper.GetUID());
            }
            else
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

                    JoinChannel("DevPro-" + Program.Config.Language);
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
                ChannelTabs.TabPages.Remove(GetChatWindow(channel));
        }
        private void ChannelAccept(string channel)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(ChannelAccept), channel);
            }
            else
            {

                if (GetChatWindow(channel) == null)
                {
                    ChannelTabs.TabPages.Add(new ChatWindow(channel, false));
                    ChannelTabs.SelectedTab = GetChatWindow(channel);
                }
                if (Program.UserInfo.Team != string.Empty && GetChatWindow(MessageType.Team.ToString()) == null)
                    ChannelTabs.TabPages.Add(new ChatWindow(MessageType.Team.ToString(), false));
                WriteMessage(new ChatMessage(MessageType.Server, CommandType.None, Program.UserInfo, "Server", "Join request for " + channel + " accepted.", false));
                
                if (!joinchannel)
                {
                    joinchannel = true;
                    if (GetChatWindow(MessageType.System.ToString()) != null)
                        ChannelTabs.TabPages.Remove(GetChatWindow(MessageType.System.ToString()));
                }
                
            }
        }

        public void WriteMessage(ChatMessage message)
        {

            if (InvokeRequired)
            {
                Invoke(new Action<ChatMessage>(WriteMessage), message);
            }
            else
            {
                if(message.From != null)
                    if (IgnoreList.Items.Contains(message.From.Username))
                        return;

                if (message.Type == MessageType.Server || message.Type == MessageType.System)
                {
                    ChatWindow window = (ChatWindow)ChannelTabs.SelectedTab;
                    if (window == null)
                    {
                        ChannelTabs.TabPages.Add(new ChatWindow(message.Type.ToString(), true) { issystemtab = true});
                        GetChatWindow(message.Type.ToString()).WriteMessage(message, autoscroll);
                    }
                    else
                        window.WriteMessage(message, autoscroll);
                }
                else if (message.Type == MessageType.Join || message.Type == MessageType.Leave || message.Channel == null)
                {
                    if (GetChatWindow(message.Channel) != null)
                    {
                        GetChatWindow(message.Channel).WriteMessage(message, autoscroll);
                    }
                    else
                    {
                        ChatWindow window = (ChatWindow)ChannelTabs.SelectedTab;
                        if (window == null)
                        {
                            ChannelTabs.TabPages.Add(new ChatWindow(message.Type.ToString(), true) { issystemtab = true });
                            GetChatWindow(message.Type.ToString()).WriteMessage(message, autoscroll);
                        }
                        else
                            window.WriteMessage(message, autoscroll);
                    }
                }
                else if (message.Type == MessageType.PrivateMessage && Program.Config.PmWindows)
                {
                    if (PMWindows.ContainsKey(message.Channel))
                    {
                        PMWindows[message.Channel].WriteMessage(message);
                    }
                    else
                    {
                        PMWindows.Add(message.Channel, new PmWindow_frm(message.Channel, true, Program.ChatServer));
                        PMWindows[message.Channel].WriteMessage(message);
                        PMWindows[message.Channel].Show();
                        PMWindows[message.Channel].FormClosed += Chat_frm_FormClosed;
                    }
                }
                else if (message.Type == MessageType.Team)
                {
                    if (GetChatWindow(message.Type.ToString()) != null)
                    {
                        GetChatWindow(message.Type.ToString()).WriteMessage(message, autoscroll);
                    }
                    else
                    {
                        ChannelTabs.TabPages.Add(new ChatWindow(message.Type.ToString(), (message.Type == MessageType.PrivateMessage ? true : false)));
                        GetChatWindow(message.Type.ToString()).WriteMessage(message, autoscroll);
                    }
                }
                else if (GetChatWindow(message.Channel) != null)
                    GetChatWindow(message.Channel).WriteMessage(message, autoscroll);
                else
                {
                    ChannelTabs.TabPages.Add(new ChatWindow(message.Channel, (message.Type == MessageType.PrivateMessage ? true : false)));
                    GetChatWindow(message.Channel).WriteMessage(message, autoscroll);
                }
            }
        }

        private ChatWindow GetChatWindow(string name)
        {
            foreach (ChatWindow window in ChannelTabs.TabPages)
            {
                if (window.Name == name)
                    return window;
            }
            return null;
        }

        private void Chat_frm_FormClosed(object sender, EventArgs e)
        {
            PmWindow_frm form = (PmWindow_frm)sender;
            PMWindows.Remove(form.Name);
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
            //search
            if (UserSearch.Text == "" || UserSearch.Text == "Search")
            {
                UserList.Items.Clear();
                UserList.Items.AddRange(UserData.Keys.ToArray());
            }
            else
            {
                UserList.Items.Clear();
                string[] users = UserData.Keys.ToArray();
                List<string> foundusers = new List<string>();

                foreach (string user in users)
                {
                    if (user.ToLower().Contains(UserSearch.Text.ToLower()))
                        foundusers.Add(user);
                }
                UserList.Items.AddRange(foundusers.ToArray());
            }

        }

        private void CreateUserList(string userlist)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(CreateUserList), userlist);
            }
            else
            {
                string[] parts = userlist.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                string[] users = parts[0].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string user in users)
                {
                    string[] info = user.Split(',');

                    if (UserData.ContainsKey(info[0]))
                        UserData[info[0]] = new UserData() { Username = info[0], Rank = Int32.Parse(info[1]), LoginID = Int32.Parse(info[2]) };
                    else
                        UserData.Add(info[0], new UserData() { Username = info[0], Rank = Int32.Parse(info[1]), LoginID = Int32.Parse(info[2]) });
                }

                UserList.Items.AddRange(UserData.Keys.ToArray());
            }
        }
        private void AddUser(string userinfo)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddUser), userinfo);
            }
            else
            {
                string[] parts = userinfo.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

                string[] info = parts[0].Split(',');

                if (!UserData.ContainsKey(info[0]))
                    UserData.Add(info[0], new UserData() { Username = info[0], Rank = Int32.Parse(info[1]), LoginID = Int32.Parse(info[2]), UserColor = Color.FromArgb(255, Convert.ToInt32(info[3]), Convert.ToInt32(info[4]), Convert.ToInt32(info[5])) });
                else
                    UserData[info[0]] = new UserData() { Username = info[0], Rank = Int32.Parse(info[1]), LoginID = Int32.Parse(info[2]), UserColor = Color.FromArgb(255, Convert.ToInt32(info[3]), Convert.ToInt32(info[4]), Convert.ToInt32(info[5])) };

                if (info[0] == Program.UserInfo.Username)
                {
                    Program.UserInfo.Rank = Int32.Parse(info[1]);
                    Program.UserInfo.UserColor = Color.FromArgb(255, Convert.ToInt32(info[3]), Convert.ToInt32(info[4]), Convert.ToInt32(info[5]));
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
                        TeamList.Items.Add(info[0]);
                }
                else
                {
                    if (TeamList.Items.Contains(info[0]))
                        TeamList.Items.Remove(info[0]);
                }

                

                if (!Program.Config.HideJoinLeave)
                {
                    if (FriendList.Items.Contains(info[0]))
                        WriteMessage(new ChatMessage(MessageType.Join, CommandType.None, Program.UserInfo, null, "Your friend " + info[0] + " has joined the channel.", false));
                    else
                        WriteMessage(new ChatMessage(MessageType.Join, CommandType.None, Program.UserInfo, null, info[0] + " has joined the channel.", false));

                }
                else
                {
                    if(FriendList.Items.Contains(info[0]))
                        WriteMessage(new ChatMessage(MessageType.Join, CommandType.None, Program.UserInfo, null, "Your friend " + info[0] + " has logged in.", false));
                }
                if (UserSearch.Text == "" || UserSearch.Text == "Search")
                {
                    if (!UserList.Items.Contains(info[0]))
                        UserList.Items.Add(info[0]);
                }
                else
                {
                    if (info[0].ToLower().Contains(UserSearch.Text.ToLower()))
                    {
                        if (!UserList.Items.Contains(info[0]))
                            UserList.Items.Add(info[0]);
                    }
                }
            }
        }
        private void RemoveUser(string userinfo)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(RemoveUser), userinfo);
            }
            else
            {

                string[] parts = userinfo.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                string user = parts[0];
                if (!Program.Config.HideJoinLeave)
                {
                    if (FriendList.Items.Contains(user))
                        WriteMessage(new ChatMessage(MessageType.Leave, CommandType.None, Program.UserInfo, null, "Your friend " + user + " has left the channel.", false));
                    else
                        WriteMessage(new ChatMessage(MessageType.Leave, CommandType.None, Program.UserInfo, null, user + " has left the channel.", false));

                }
                else
                {
                    if (FriendList.Items.Contains(user))
                        WriteMessage(new ChatMessage(MessageType.Leave, CommandType.None, Program.UserInfo, null, "Your friend " + user + " has logged out.", false));
                }

                if (UserData.ContainsKey(user))
                {
                    if (UserData[user].LoginID == Int32.Parse(parts[1]))
                    {
                        if (UserList.Items.Contains(user))
                            UserList.Items.Remove(user);
                        UserData.Remove(user);
                    }
                }

            }
        }
        public void CreateFriendList(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(CreateFriendList), message);
            }
            else
            {
                if (message == "not found")
                    return;

                string[] friends = message.Split(',');
                FriendList.Items.Clear();
                FriendList.Items.AddRange(friends);
            }
        }

        private void UserList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < UserList.Items.Count)
            {
                string text = UserList.Items[index].ToString();
                Graphics g = e.Graphics;
                if (!UserData.ContainsKey(text))
                {
                    g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black) : new SolidBrush(Color.Blue)) : new SolidBrush(Program.Config.ChatBGColor.ToColor()), e.Bounds);
                    g.DrawString(text, e.Font, (selected) ? Brushes.White : Brushes.Black,
                         UserList.GetItemRectangle(index).Location);
                    e.DrawFocusRectangle();
                    return;
                }

                g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black) : new SolidBrush(Color.Blue)) : new SolidBrush(Program.Config.ChatBGColor.ToColor()), e.Bounds);

                if (UserData[text].Rank > 0)
                {
                    // Print text
                    g.DrawString("[", e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : new SolidBrush(Program.Config.NormalTextColor.ToColor())),
                        UserList.GetItemRectangle(index).Location);

                    g.DrawString("Dev", e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : ChatMessage.GetUserColor(UserData[text].Rank)),
                      new Point(UserList.GetItemRectangle(index).Location.X + (int)g.MeasureString("[",e.Font).Width - 1 ,UserList.GetItemRectangle(index).Location.Y));
                    g.DrawString("]", e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : new SolidBrush(Program.Config.NormalTextColor.ToColor())),
                        new Point(UserList.GetItemRectangle(index).Location.X + (int)g.MeasureString("[Dev", e.Font).Width, UserList.GetItemRectangle(index).Location.Y));
                    if (UserData[text].UserColor.ToArgb() == Color.Black.ToArgb())
                    {
                        g.DrawString(text, e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : new SolidBrush(Program.Config.NormalTextColor.ToColor())),
                            new Point(UserList.GetItemRectangle(index).Location.X + (int)g.MeasureString("[Dev]", e.Font).Width, UserList.GetItemRectangle(index).Location.Y));
                    }
                    else
                    {
                        g.DrawString(text, e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : new SolidBrush(UserData[text].UserColor)),
                            new Point(UserList.GetItemRectangle(index).Location.X + (int)g.MeasureString("[Dev]", e.Font).Width, UserList.GetItemRectangle(index).Location.Y));
                    }
                }
                else
                {
                    if (UserData[text].UserColor.ToArgb() == Color.Black.ToArgb())
                    {
                        // Print text
                        g.DrawString(text, e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : new SolidBrush(Program.Config.NormalTextColor.ToColor())),
                            UserList.GetItemRectangle(index).Location);
                    }
                    else
                    {
                        // Print text
                        g.DrawString(text, e.Font, (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : new SolidBrush(UserData[text].UserColor)),
                            UserList.GetItemRectangle(index).Location);
                    }
                    
                }
            }

            e.DrawFocusRectangle();
        }

        private void DrawList_OnlineOffline(object sender, DrawItemEventArgs e)
        {
            ListBox list = (ListBox)sender;
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < list.Items.Count)
            {
                string text = list.Items[index].ToString();
                Graphics g = e.Graphics;

                g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black) : new SolidBrush(Color.Blue)) : new SolidBrush(Program.Config.ChatBGColor.ToColor()), e.Bounds);

                //// Print text
                g.DrawString((Program.Config.ColorBlindMode ? (UserData.ContainsKey(text) ? text + " (Online)" : text + " (Offline)") : text), e.Font,
                    (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : (UserData.ContainsKey(text) ? Brushes.Green : Brushes.Red)),
                    list.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }


        private void ApplyNewColor(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ColorDialog selectcolor = new ColorDialog();
            selectcolor.Color = button.BackColor;
            selectcolor.AllowFullOpen = true;
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
        private void ChatInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (ChatInput.Text == "")
                    return;
                string[] parts = ChatInput.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 0)
                    return;

                if (parts[0].StartsWith("/"))
                {
                    string cmd = parts[0].Substring(1).ToLower();
                    if (cmd == "me")
                    {
                        if (ChannelTabs.SelectedTab == null)
                        {
                            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "No channel Selected."));
                            return;
                        }
                        if(((ChatWindow)ChannelTabs.SelectedTab).Name == MessageType.Team.ToString())
                            Program.ChatServer.SendMessage(MessageType.Team, CommandType.Me, ((ChatWindow)ChannelTabs.SelectedTab).Name, ChatInput.Text.Substring(parts[0].Length + 1));
                        else
                            Program.ChatServer.SendMessage(MessageType.Message, CommandType.Me, ((ChatWindow)ChannelTabs.SelectedTab).Name, ChatInput.Text.Substring(parts[0].Length + 1));
                    }
                    else if (cmd == "join")
                    {
                        JoinChannel(ChatInput.Text.Substring(parts[0].Length).Trim());
                    }
                    else if (cmd == "leave")
                    {
                        if (((ChatWindow)ChannelTabs.SelectedTab) == null)
                            return;
                        if (((ChatWindow)ChannelTabs.SelectedTab).isprivate)
                            ChannelTabs.TabPages.Remove(((ChatWindow)ChannelTabs.SelectedTab));
                        else
                            LeaveChannel(((ChatWindow)ChannelTabs.SelectedTab).Name);
                    }
                    else if (cmd == "users")
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "There's " + UserData.Count + " users online."));
                    }
                    else if (cmd == "ping")
                    {
                        Program.ChatServer.PingRequest = DateTime.Now;
                        Program.ChatServer.SendPacket("PING");
                    }
                    else if (cmd == "pinggame")
                    {
                        Program.ServerConnection.pingrequest = DateTime.Now;
                        Program.ServerConnection.SendPacket("PING");
                    }
                    else if (cmd == "autoscroll")
                    {
                        autoscroll = !autoscroll;
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, (autoscroll ? "AutoScroll Enabled." : "AutoScroll Disabled.")));
                    }
                    else if (cmd == "help")
                    {
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
                        if (Program.UserInfo.Rank < 0 || Program.UserInfo.Rank > 0)
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

                    }
                    else if (cmd == "teamdisband")
                    {
                        if (MessageBox.Show("Are you sure?", "Confirm team disband", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Program.ChatServer.SendPacket("ADMIN||" + cmd.ToUpper() + "||" + ChatInput.Text.Substring(parts[0].Length).Trim());
                        }
                    }
                    else if (cmd == "admin")
                    {

                        string admins = "";
                        foreach (string user in UserData.Keys)
                        {
                            if (UserData[user].Rank > 0)
                            {
                                if (admins == "")
                                    admins = user;
                                else
                                    admins += ", " + user;
                            }
                        }
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "The following admins are online: " + admins + "."));

                    }
                    else
                    {
                        Program.ChatServer.SendPacket("ADMIN||" + cmd.ToUpper() + "||" + ChatInput.Text.Substring(parts[0].Length).Trim());
                    }
                }
                else
                {
                    if (ChannelTabs.SelectedTab == null)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "No channel Selected."));
                        return;
                    }
                    if (((ChatWindow)ChannelTabs.SelectedTab).issystemtab)
                    {
                        ChatInput.Clear();
                        return;
                    }

                    if (((ChatWindow)ChannelTabs.SelectedTab).isprivate)
                    {
                        WriteMessage(new ChatMessage(MessageType.Message, CommandType.None, Program.UserInfo, ((ChatWindow)ChannelTabs.SelectedTab).Name, ChatInput.Text, false));
                        //Program.ChatServer.SendPacket("MSG||" + ((ChatWindow)ChannelTabs.SelectedTab).Name + "||" + (int)MessageType.PrivateMessage + "||" + LauncherHelper.StringToBase64(ChatInput.Text));
                        Program.ChatServer.SendMessage(MessageType.PrivateMessage, CommandType.None, ((ChatWindow)ChannelTabs.SelectedTab).Name, ChatInput.Text);
                    }
                    else if(((ChatWindow)ChannelTabs.SelectedTab).Name == MessageType.Team.ToString())
                        Program.ChatServer.SendMessage(MessageType.Team, CommandType.None, ((ChatWindow)ChannelTabs.SelectedTab).Name, ChatInput.Text);
                    else
                        Program.ChatServer.SendMessage(MessageType.Message, CommandType.None, ((ChatWindow)ChannelTabs.SelectedTab).Name, ChatInput.Text);
                }

                ChatInput.Clear();
                e.Handled = true;
            }
            
        }
        private void Option_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
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
                        List<ChatWindow> removelist = new List<ChatWindow>();
                        foreach (ChatWindow window in ChannelTabs.TabPages)
                        {
                            if (window.isprivate)
                                removelist.Add(window);
                        }
                        foreach(ChatWindow window in removelist)
                            ChannelTabs.TabPages.Remove(window);
                    }
                    else
                    {
                        List<string> removelist = new List<string>();
                        foreach (string window in PMWindows.Keys)
                        {
                            removelist.Add(window);
                        }
                        foreach(string window in removelist)
                            PMWindows[window].Close();
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

                if (index == -1) return;
                else
                    UserList.SelectedIndex = index;

                if (UserList.SelectedItem == null)
                    return;

                ContextMenuStrip mnu = new ContextMenuStrip();
                ToolStripMenuItem mnuprofile = new ToolStripMenuItem(Program.LanguageManager.Translation.chatViewProfile);
                ToolStripMenuItem mnuduel = new ToolStripMenuItem(Program.LanguageManager.Translation.chatRequestDuel);
                ToolStripMenuItem mnufriend = new ToolStripMenuItem(Program.LanguageManager.Translation.chatAddFriend);
                ToolStripMenuItem mnuignore = new ToolStripMenuItem(Program.LanguageManager.Translation.chatIgnoreUser);
                ToolStripMenuItem mnukick = new ToolStripMenuItem("Kick");
                ToolStripMenuItem mnuban = new ToolStripMenuItem("Ban");

                mnukick.Click += new EventHandler(KickUser);
                mnuban.Click += new EventHandler(BanUser);
                mnuprofile.Click += new EventHandler(ViewProfile);
                mnuduel.Click += new EventHandler(RequestDuel);
                mnufriend.Click += new EventHandler(AddFriend);
                mnuignore.Click += new EventHandler(IgnoreUser);

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
            if (UserList.SelectedItem == null)
                return;
            if (MessageBox.Show("Are you sure you want to ban " + UserList.SelectedItem.ToString(), "Ban User", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                Program.ChatServer.SendPacket("ADMIN||BAN||" + UserList.SelectedItem.ToString());
        }
        private void KickUser(object sender, EventArgs e)
        {
            if (UserList.SelectedItem == null)
                return;
            Program.ChatServer.SendPacket("ADMIN||KICK||" + UserList.SelectedItem.ToString());
        }

        private void AddFriend(object sender, EventArgs e)
        {
            if (UserList.SelectedItem == null)
                return;

            if (UserList.SelectedItem.ToString() == Program.UserInfo.Username)
            {
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You cannot be your own friend."));
                return;
            }

            foreach (object user in FriendList.Items)
            {
                if (user.ToString().ToLower() == UserList.SelectedItem.ToString().ToLower())
                {
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, UserList.SelectedItem.ToString() + " is already your friend."));
                    return;
                }
            }

            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, UserList.SelectedItem.ToString() + " has been added to your friend list."));
            FriendList.Items.Add(UserList.SelectedItem.ToString());
            Program.ChatServer.SendPacket("ADDFRIEND||" + UserList.SelectedItem.ToString());
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
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, UserList.SelectedItem.ToString() + " is already on your ignore list."));
                return;
            }

            IgnoreList.Items.Add(UserList.SelectedItem.ToString());
            SaveIgnoreList();
            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, UserList.SelectedItem.ToString() + " has been added to your ignore list."));
        }

        private void FriendList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = FriendList.IndexFromPoint(e.Location);

                if (index == -1) return;
                else
                    FriendList.SelectedIndex = index;

                if (FriendList.SelectedItem == null)
                    return;

                ContextMenuStrip mnu = new ContextMenuStrip();
                ToolStripMenuItem mnuremovefriend = new ToolStripMenuItem(Program.LanguageManager.Translation.chatRemoveFriend);
                ToolStripMenuItem mnuprofile = new ToolStripMenuItem(Program.LanguageManager.Translation.chatViewProfile);
                ToolStripMenuItem mnuduel = new ToolStripMenuItem(Program.LanguageManager.Translation.chatRequestDuel);

                mnuremovefriend.Click += new EventHandler(RemoveFriend);
                mnuprofile.Click += new EventHandler(ViewProfile);
                mnuduel.Click += new EventHandler(RequestDuel);

                mnu.Items.AddRange(new ToolStripMenuItem[] { mnuprofile, mnuduel, mnuremovefriend });

                mnu.Show(FriendList, e.Location);
            }
        }

        private void TeamList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = TeamList.IndexFromPoint(e.Location);

                if (index == -1) return;
                else
                    TeamList.SelectedIndex = index;

                if (TeamList.SelectedItem == null)
                    return;

                ContextMenuStrip mnu = new ContextMenuStrip();
                ToolStripMenuItem mnuprofile = new ToolStripMenuItem(Program.LanguageManager.Translation.chatViewProfile);
                ToolStripMenuItem mnuduel = new ToolStripMenuItem(Program.LanguageManager.Translation.chatRequestDuel);
                ToolStripMenuItem mnuremoveteam = new ToolStripMenuItem("Remove from Team");

                //mnuremovefriend.Click += new EventHandler(RemoveFriend);
                mnuprofile.Click += new EventHandler(ViewProfile);
                mnuduel.Click += new EventHandler(RequestDuel);
                mnuremoveteam.Click += new EventHandler(RemoveFromTeam);

                if(Program.UserInfo.TeamRank > 0)
                    mnu.Items.AddRange(new ToolStripMenuItem[] { mnuprofile, mnuduel, mnuremoveteam });
                else
                    mnu.Items.AddRange(new ToolStripMenuItem[] { mnuprofile, mnuduel });

                mnu.Show(TeamList, e.Location);
            }
        }

        private void RemoveFromTeam(object sender, EventArgs e)
        {
            if (TeamList.SelectedIndex == -1)
                return;
            if (MessageBox.Show("Are you sure?", "Remove User", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.ChatServer.SendPacket("ADMIN||TEAMREMOVE||" + TeamList.SelectedItem.ToString());
            }

        }

        private void RemoveFriend(object sender, EventArgs e)
        {
            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, FriendList.SelectedItem.ToString() + " has been removed from your friendlist."));
            Program.ChatServer.SendPacket("REMOVEFRIEND||" + FriendList.SelectedItem.ToString());
            FriendList.Items.Remove(FriendList.SelectedItem.ToString());
        }
        private void ViewProfile(object sender, EventArgs e)
        {
            ListBox list = (UserListTabs.SelectedTab.Name == OnlineTab.Name ? UserList :(UserListTabs.SelectedTab.Name == TeamTab.Name ? TeamList: FriendList));
            if (list.SelectedItem == null)
                return;

            Profile_frm profile = new Profile_frm(list.SelectedItem.ToString());
            profile.ShowDialog();

        }

        private void RequestDuel(object sender, EventArgs e)
        {
            ListBox list = (UserListTabs.SelectedTab.Name == OnlineTab.Name ? UserList : (UserListTabs.SelectedTab.Name == TeamTab.Name ? TeamList : FriendList));

            if (list.SelectedItem == null)
                return;

            if (list.SelectedItem.ToString() == Program.UserInfo.Username)
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You cannot duel request your self."));
            else
            {
                Host form = new Host();
                Program.ChatServer.SendPacket("REQUESTDUEL||" + list.SelectedItem.ToString() + "||"
                    + form.GenerateGameString(false));
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "Duel request sent to " + list.SelectedItem.ToString() + "."));
            }
        }

        private void HandleDuelRequest(string command)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(HandleDuelRequest), command);
            }
            else
            {
                string[] args = command.Split(new string[] { "||" }, StringSplitOptions.None);
                string cmd = args[0];

                if (cmd == "START")
                {
                    LauncherHelper.GenerateConfig("ygopro:/" + Program.Config.ServerAddress + "/" + Program.Config.GamePort + "/" + args[1]);
                    LauncherHelper.RunGame("-j");
                }
                else if (cmd == "REQUEST")
                {
                    if (Program.Config.RefuseDuelRequests)
                    {
                        Program.ChatServer.SendPacket("REFUSEDUEL||" + args[1]);
                        return;
                    }
                    RoomInfos info = RoomInfos.FromName(args[2], "", false);
                    DuelRequest_frm request = new DuelRequest_frm(args[1] + Program.LanguageManager.Translation.DuelReqestMessage + Environment.NewLine +
                        Program.LanguageManager.Translation.DuelRequestMode + RoomInfos.GameMode(info.Mode) + Program.LanguageManager.Translation.DuelRequestRules + RoomInfos.GameRule(info.Rule) + Program.LanguageManager.Translation.DuelRequestBanlist + LauncherHelper.GetBanListFromInt(info.BanList));


                    if (request.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You accepted " + args[1] + " duel request."));
                        Program.ChatServer.SendPacket("ACCEPTDUEL||" + args[1] + "||" + args[2]);
                    }
                    else
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You refused " + args[1] + " duel request."));
                        Program.ChatServer.SendPacket("REFUSEDUEL||" + args[1]);
                    }
                }
                else if (cmd == "REFUSE")
                {
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, args[1] + " refused your duel request."));
                }
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
                string[] args = command.Split(new string[] { "||" }, StringSplitOptions.None);
                string cmd = args[0];

                if (cmd == "JOIN")
                {
                    if (Program.Config.RefuseTeamInvites)
                    {
                        Program.ChatServer.SendPacket("TEAMREFUSE||AUTO");
                        return;
                    }
                    if (MessageBox.Show("You have been invited to join the team " + args[1], "Team Request", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        WriteMessage(new ChatMessage(MessageType.System,CommandType.None,Program.UserInfo.Username,"You have accepted the team invite to join " + args[1]));
                        Program.ChatServer.SendPacket("TEAMACCEPT");
                    }
                    else
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo.Username, "You have refused the team invite to join " + args[1]));
                        Program.ChatServer.SendPacket("TEAMREFUSE");
                    }
                }
                else if (cmd == "LEAVE")
                {
                    Program.UserInfo.Team = string.Empty;
                    Program.UserInfo.TeamRank = 0;
                    TeamNameLabel.Text = "Team: None";
                    TeamList.Items.Clear();
                    ChannelTabs.TabPages.Remove(GetChatWindow(MessageType.Team.ToString()));
                    WriteMessage(new ChatMessage(MessageType.System,CommandType.None,Program.UserInfo.Username,"You have left the team."));
                }
                else if (cmd == "REMOVED")
                {
                    Program.UserInfo.Team = string.Empty;
                    Program.UserInfo.TeamRank = 0;
                    TeamNameLabel.Text = "Team: None";
                    TeamList.Items.Clear();
                    ChannelTabs.TabPages.Remove(GetChatWindow(MessageType.Team.ToString()));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo.Username, "You have been removed from the team."));
                }
                else if (cmd == "DISBAND")
                {
                    foreach (string user in UserData.Keys)
                    {
                        if (UserData[user].Team == args[1])
                        {
                            UserData[user].Team = string.Empty;
                            UserData[user].TeamRank = 0;
                        }
                    }
                    if (Program.UserInfo.Team == args[1])
                    {
                        Program.UserInfo.Team = string.Empty;
                        Program.UserInfo.TeamRank = 0;
                        TeamNameLabel.Text = "Team: None";
                        TeamList.Items.Clear();
                        ChannelTabs.TabPages.Remove(GetChatWindow(MessageType.Team.ToString()));
                    }
                }
                else if (cmd == "USERS")
                {
                    TeamList.Items.Clear();
                    string[] parts = args[1].Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string user in parts)
                    {
                        TeamList.Items.Add(user);
                    }
                }
                else
                    WriteMessage(new ChatMessage(MessageType.Server, CommandType.None, Program.UserInfo.Username, "Unknown packet:" + command));

         
            }
        }

        private void IgnoreList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = IgnoreList.IndexFromPoint(e.Location);

                if (index == -1) return;
                else
                    IgnoreList.SelectedIndex = index;

                ContextMenuStrip mnu = new ContextMenuStrip();
                ToolStripMenuItem mnuremoveignore = new ToolStripMenuItem(Program.LanguageManager.Translation.chatRemoveIgnore);

                mnuremoveignore.Click += new EventHandler(UnignoreUser);

                mnu.Items.Add(mnuremoveignore);

                mnu.Show(IgnoreList, e.Location);
            }
        }
        private void UnignoreUser(object sender, EventArgs e)
        {
            if (IgnoreList.SelectedItem == null)
                return;
            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, IgnoreList.SelectedItem.ToString() + " has been removed from your ignore list."));
            IgnoreList.Items.Remove(IgnoreList.SelectedItem);
            SaveIgnoreList();
        }
        private void SaveIgnoreList()
        {
            string ignorestring = null;
            foreach (object user in IgnoreList.Items)
            {
                if (ignorestring == null)
                    ignorestring += user.ToString();
                else
                    ignorestring += "," + user.ToString();
            }
            Program.Config.IgnoreList = ignorestring;
            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);

        }
        private void FontSettings_Changed(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                ComboBox fontstyle = (ComboBox)sender;
                Program.Config.ChatFont = fontstyle.SelectedItem.ToString();
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown size = (NumericUpDown)sender;
                Program.Config.ChatSize = size.Value;
            }
            foreach (ChatWindow tab in ChannelTabs.TabPages)
            {
                tab.ApplyNewSettings();
            }
            foreach (string form in PMWindows.Keys)
            {
                PMWindows[form].ApplyNewSettings();
            }

            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
        }
        private void Chat_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
        private void List_DoubleClick(object sender, EventArgs e)
        {
            ListBox list = (UserListTabs.SelectedTab.Name == OnlineTab.Name ?UserList :(UserListTabs.SelectedTab.Name == TeamTab.Name ? TeamList: FriendList));

            if (list.SelectedItem == null)
                return;
            if (Program.Config.PmWindows)
            {
                if (!PMWindows.ContainsKey(list.SelectedItem.ToString()))
                {
                    PMWindows.Add(list.SelectedItem.ToString(), new PmWindow_frm(list.SelectedItem.ToString(), true, Program.ChatServer));
                    PMWindows[list.SelectedItem.ToString()].Show();
                    PMWindows[list.SelectedItem.ToString()].FormClosed += Chat_frm_FormClosed;
                }
                else
                {
                    PMWindows[list.SelectedItem.ToString()].BringToFront();
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
            
            Input_frm form = new Input_frm("Add Team Member","Enter Users name","Send","Cancel");
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
            TeamProfile_frm form = new TeamProfile_frm(Program.UserInfo.Team);
            form.Show();
        }
    }
}
