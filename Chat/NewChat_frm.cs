using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        ChatClient server = new ChatClient();
        Dictionary<string, UserData> UserData = new Dictionary<string, UserData>();
        Dictionary<string, PmWindow_frm> PMWindows = new Dictionary<string, PmWindow_frm>();
        List<string> ChannelList = new List<string>();
        public bool autoscroll = true;

        public NewChat_frm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            //chat packets
            server.Login += new ChatClient.ServerResponse(LoginCheck);
            server.UserList += new ChatClient.ServerResponse(CreateUserList);
            server.AddUser += new ChatClient.ServerResponse(AddUser);
            server.RemoveUser += new ChatClient.ServerResponse(RemoveUser);
            server.FriendList += new ChatClient.ServerResponse(CreateFriendList);
            server.JoinChannel += new ChatClient.ServerResponse(ChannelAccept);
            server.Message += new ChatClient.ServerMessage(WriteMessage);
            server.Error += new ChatClient.ServerMessage(WriteMessage);
            server.DuelRequest += new ChatClient.ServerResponse(HandleDuelRequest);

            //form events
            UserSearch.Enter += new EventHandler(UserSearch_Enter);
            UserSearch.Leave += new EventHandler(UserSearch_Leave);
            UserSearch.TextChanged += new EventHandler(UserSearch_TextChanged);
            ChatInput.KeyPress += new KeyPressEventHandler(ChatInput_KeyPress);
            ChatRTB.LinkClicked += new LinkClickedEventHandler(Chat_LinkClicked);
            UserList.DoubleClick += List_DoubleClick;
            FriendList.DoubleClick += List_DoubleClick;
            ApplyOptionEvents();


            UserList.MouseUp += new MouseEventHandler(UserList_MouseUp);
            FriendList.MouseUp += new MouseEventHandler(FriendList_MouseUp);
            IgnoreList.MouseUp += IgnoreList_MouseUp;

            //custom form drawing
            UserList.DrawItem += new DrawItemEventHandler(UserList_DrawItem);
            FriendList.DrawItem += new DrawItemEventHandler(FriendList_DrawItem);

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
            ChatRTB.BackColor = Program.Config.ChatBGColor.ToColor();
            ChatInput.BackColor = Program.Config.ChatBGColor.ToColor();
            ChatInput.ForeColor = Program.Config.NormalTextColor.ToColor();
            UserSearch.BackColor = Program.Config.ChatBGColor.ToColor();

            UserList.BackColor = Program.Config.ChatBGColor.ToColor();
            FriendList.BackColor = Program.Config.ChatBGColor.ToColor();
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

            if (Program.Config.ChatFont == "")
                FontList.SelectedItem = ChatRTB.Font.Name;
            else
            {
                if (FontList.Items.Contains(Program.Config.ChatFont))
                {
                    FontList.SelectedItem = Program.Config.ChatFont;
                }
            }

            FontSize.Value = Program.Config.ChatSize;
            ChatRTB.Font = new Font(Program.Config.ChatFont, (float)Program.Config.ChatSize);
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
            WriteMessage(new ChatMessage(MessageType.System, Program.UserInfo, "System", "Attempting to connect.", false));
            if (server.Connect(Program.Config.ChatServerAddress, Program.Config.ChatPort))
                server.SendPacket("LOGIN||" + Program.UserInfo.Username + "||" + Program.Config.Password + "||" + LauncherHelper.GetUID());
            else
                WriteMessage(new ChatMessage(MessageType.System, Program.UserInfo, "System", "Failed to connect.", false));
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
                    WriteMessage(new ChatMessage(MessageType.System, Program.UserInfo, "System", "Unable to login.", false));
                else if (message == "Banned")
                    WriteMessage(new ChatMessage(MessageType.System, Program.UserInfo, "System", "You are banned.", false));
                else if (message == "LoginDown")
                    WriteMessage(new ChatMessage(MessageType.System, Program.UserInfo, "System", "Login Server is currently down or locked.", false));
                else
                {
                    WriteMessage(new ChatMessage(MessageType.System, Program.UserInfo, "System", "Connected to the DevPro Chat Server.", false));

                    JoinChannel("DevPro-" + Program.Config.Language);
                }
            }
        }
        private void JoinChannel(string channel)
        {
            server.SendPacket("JOIN||" + channel);
            WriteMessage(new ChatMessage(MessageType.System, Program.UserInfo, "System", "Join request for " + channel + " has been sent.", false));
        }
        private void LeaveChannel(string channel)
        {
            server.SendPacket("LEAVE||" + channel);
            if (ChannelList.Contains(channel))
                ChannelList.Remove(channel);
            if (ChannelSelect.Items.Contains(channel))
                ChannelSelect.Items.Remove(channel);
        }
        private void ChannelAccept(string channel)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(ChannelAccept), channel);
            }
            else
            {
                if (!ChannelList.Contains(channel))
                    ChannelList.Add(channel);
                if (!ChannelSelect.Items.Contains(channel))
                {
                    ChannelSelect.Items.Add(channel);
                    ChannelSelect.SelectedItem = channel;
                }
                WriteMessage(new ChatMessage(MessageType.Server, Program.UserInfo, "Server", "Join request for "+channel+" accepted.", false));
            }
        }

        public void WriteMessage(ChatMessage message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<ChatMessage>(WriteMessage), message);
            }
            else
            {
                if (message.Type == MessageType.PrivateMessage)
                {
                    if (PMWindows.ContainsKey(message.Channel))
                    {
                        PMWindows[message.Channel].WriteMessage(message);
                    }
                    else
                    {
                        PMWindows.Add(message.Channel, new PmWindow_frm(message.Channel, true, server));
                        PMWindows[message.Channel].WriteMessage(message);
                        PMWindows[message.Channel].Show();
                        PMWindows[message.Channel].FormClosed += Chat_frm_FormClosed;
                    }
                }
                else
                {

                    if (ChatRTB.Text != "")//start a new line unless theres no text
                        ChatRTB.AppendText(Environment.NewLine);
                    ChatRTB.Select(ChatRTB.TextLength, 0);
                    if (message.Type == MessageType.Message || message.Type == MessageType.PrivateMessage)
                    {
                        if (Program.Config.ShowTimeStamp)
                            WriteText(message.Time.ToString("[HH:mm] "), (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                        WriteText("[" + message.Channel + "]", (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                        WriteText("[", (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                        WriteText((Program.Config.ColorBlindMode && message.From.Rank > 0 ? "[Admin]" + message.From.Username : message.From.Username),
                            (Program.Config.ColorBlindMode ? Color.Black : message.UserColor));
                        WriteText("]: ", (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));

                        WriteText(message.FormattedMessage.Trim(), (Program.Config.ColorBlindMode ? Color.Black : message.MessageColor));

                    }
                    else if (message.Type == MessageType.System || message.Type == MessageType.Server)
                    {
                        WriteText("[" + message.Type + "] " + message.FormattedMessage,
                                (Program.Config.ColorBlindMode ? Color.Black : message.MessageColor));
                    }
                    else
                    {
                        WriteText((Program.Config.ColorBlindMode ? "[" + message.Type + "] " + message.FormattedMessage : message.FormattedMessage),
                            (Program.Config.ColorBlindMode ? Color.Black : message.MessageColor));
                    }

                    ChatRTB.SelectionStart = ChatRTB.TextLength;
                    ChatRTB.SelectionLength = 0;

                    if (autoscroll)
                        ChatRTB.ScrollToCaret();
                }
            }
        }
        private void Chat_frm_FormClosed(object sender, EventArgs e)
        {
            PmWindow_frm form = (PmWindow_frm)sender;
            PMWindows.Remove(form.Name);
        }
        private void WriteText(string text, Color color)
        {
            ChatRTB.Select(ChatRTB.TextLength, 0);
            ChatRTB.SelectionColor = color;
            ChatRTB.AppendText(text);
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
                    UserData.Add(info[0], new UserData() { Username = info[0], Rank = Int32.Parse(info[1]), LoginID = Int32.Parse(info[2]) });
                else
                    UserData[info[0]] = new UserData() { Username = info[0], Rank = Int32.Parse(info[1]), LoginID = Int32.Parse(info[2]) };

                if (info[0] == Program.UserInfo.Username)
                    Program.UserInfo.Rank = Int32.Parse(info[1]);


                if (!Program.Config.HideJoinLeave)
                {
                    if (FriendList.Items.Contains(info[0]))
                        WriteMessage(new ChatMessage(MessageType.Join, Program.UserInfo, null, "Your friend " + info[0] + " has joined the channel.", false));
                    else
                        WriteMessage(new ChatMessage(MessageType.Join, Program.UserInfo, null, info[0] + " has joined the channel.", false));

                }
                else
                {
                    if(FriendList.Items.Contains(info[0]))
                        WriteMessage(new ChatMessage(MessageType.Join, Program.UserInfo, null, "Your friend " + info[0] + " has logged in.", false));
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
                        WriteMessage(new ChatMessage(MessageType.Leave, Program.UserInfo, null, "Your friend " + user + " has left the channel.", false));
                    else
                        WriteMessage(new ChatMessage(MessageType.Leave, Program.UserInfo, null, user + " has left the channel.", false));

                }
                else
                {
                    if (FriendList.Items.Contains(user))
                        WriteMessage(new ChatMessage(MessageType.Leave, Program.UserInfo, null, "Your friend " + user + " has logged out.", false));
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

                // Print text
                g.DrawString((Program.Config.ColorBlindMode && UserData[text].Rank > 0 ? "[Admin] " + text: text), e.Font, (selected) ? Brushes.White :(Program.Config.ColorBlindMode ? Brushes.Black:  ChatMessage.GetUserColor(UserData[text].Rank)),
                    UserList.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        private void FriendList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < FriendList.Items.Count)
            {
                string text = FriendList.Items[index].ToString();
                Graphics g = e.Graphics;

                g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black) : new SolidBrush(Color.Blue)) : new SolidBrush(Program.Config.ChatBGColor.ToColor()), e.Bounds);

                // Print text
                g.DrawString((Program.Config.ColorBlindMode ? (UserList.Items.Contains(text) ? text + " (Online)" : text + " (Offline)") : text), e.Font,
                    (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : (UserList.Items.Contains(text) ? Brushes.Green : Brushes.Red)),
                    FriendList.GetItemRectangle(index).Location);
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
                

                if (parts[0].StartsWith("/"))
                {
                    string cmd = parts[0].Substring(1).ToLower();
                    if (cmd == "me")
                    {
                        if (ChannelSelect.SelectedIndex == -1)
                        {
                            WriteMessage(new ChatMessage(MessageType.System, null, "No channel Selected."));
                            return;
                        }
                        server.SendPacket("MSG||" + ChannelSelect.SelectedItem.ToString() + "||" + (int)MessageType.Me + "||" + LauncherHelper.StringToBase64(ChatInput.Text.Substring(parts[0].Length)));
                    }
                    else if (cmd == "join")
                    {
                        JoinChannel(ChatInput.Text.Substring(parts[0].Length).Trim());
                    }
                    else if (cmd == "leave")
                    {
                        LeaveChannel(ChatInput.Text.Substring(parts[0].Length).Trim());
                    }
                    else if (cmd == "users")
                    {
                        WriteMessage(new ChatMessage(MessageType.System, null, "There's " + UserData.Count + " users online."));
                    }
                    else if (cmd == "autoscroll")
                    {
                        autoscroll = !autoscroll;
                        WriteMessage(new ChatMessage(MessageType.System, null, (autoscroll ? "AutoScroll Enabled." : "AutoScroll Disabled.")));
                    }
                    else if (cmd == "help")
                    {
                        WriteMessage(new ChatMessage(MessageType.System, null, "Did you really think i would write a list of help commands? :("));
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
                        WriteMessage(new ChatMessage(MessageType.System, null, "The following admins are online: " + admins + "."));

                    }
                    else
                    {
                        server.SendPacket("ADMIN||" + cmd.ToUpper() + "||" + ChatInput.Text.Substring(parts[0].Length).Trim());
                    }
                }
                else
                {
                    if (ChannelSelect.SelectedIndex == -1)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, null, "No channel Selected."));
                        return;
                    }
                    server.SendPacket("MSG||" + ChannelSelect.SelectedItem.ToString() + "||" + (int)MessageType.Message + "||" + LauncherHelper.StringToBase64(ChatInput.Text));
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
                server.SendPacket("ADMIN||BAN||" + UserList.SelectedItem.ToString());
        }
        private void KickUser(object sender, EventArgs e)
        {
            if (UserList.SelectedItem == null)
                return;
            server.SendPacket("ADMIN||KICK||" + UserList.SelectedItem.ToString());
        }

        private void AddFriend(object sender, EventArgs e)
        {
            if (UserList.SelectedItem == null)
                return;

            if (UserList.SelectedItem.ToString() == Program.UserInfo.Username)
            {
                WriteMessage(new ChatMessage(MessageType.System, null, "You cannot be your own friend."));
                return;
            }

            foreach (object user in FriendList.Items)
            {
                if (user.ToString() == UserList.SelectedItem.ToString())
                {
                    WriteMessage(new ChatMessage(MessageType.System, null, UserList.SelectedItem.ToString() + " is already your friend."));
                    return;
                }
            }

            WriteMessage(new ChatMessage(MessageType.System, null, UserList.SelectedItem.ToString() + " has been added to your friend list."));
            FriendList.Items.Add(UserList.SelectedItem.ToString());
            server.SendPacket("ADDFRIEND||" + UserList.SelectedItem.ToString());
        }

        private void IgnoreUser(object sender, EventArgs e)
        {
            if (UserList.SelectedItem.ToString() == Program.UserInfo.Username)
            {
                WriteMessage(new ChatMessage(MessageType.System, null, "You cannot ignore yourself."));
                return;
            }
            if (IgnoreList.Items.Contains(UserList.SelectedItem.ToString()))
            {
                WriteMessage(new ChatMessage(MessageType.System, null, UserList.SelectedItem.ToString() + " is already on your ignore list."));
                return;
            }

            IgnoreList.Items.Add(UserList.SelectedItem.ToString());
            SaveIgnoreList();
            WriteMessage(new ChatMessage(MessageType.System, null, UserList.SelectedItem.ToString() + " has been added to your ignore list."));
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

        private void RemoveFriend(object sender, EventArgs e)
        {
            WriteMessage(new ChatMessage(MessageType.System, null, FriendList.SelectedItem.ToString() + " has been removed from your friendlist."));
            server.SendPacket("REMOVEFRIEND||" + FriendList.SelectedItem.ToString());
            FriendList.Items.Remove(FriendList.SelectedItem.ToString());
        }
        private void ViewProfile(object sender, EventArgs e)
        {
            ListBox list = (UsersControl.SelectedTab.Name == UsersTab.Name ? UserList : FriendList);
            if (list.SelectedItem == null)
                return;

            Profile_frm profile = new Profile_frm(list.SelectedItem.ToString());
            profile.ShowDialog();

        }

        private void RequestDuel(object sender, EventArgs e)
        {
            ListBox list = (UsersControl.SelectedTab.Name == UsersTab.Name ? UserList : FriendList);

            if (list.SelectedItem == null)
                return;

            if (list.SelectedItem.ToString() == Program.UserInfo.Username)
                WriteMessage(new ChatMessage(MessageType.System, null, "You cannot duel request your self."));
            else
            {
                Host form = new Host();
                server.SendPacket("REQUESTDUEL||" + list.SelectedItem.ToString() + "||"
                    + form.GenerateGameString(false));
                WriteMessage(new ChatMessage(MessageType.System, null, "Duel request sent to " + list.SelectedItem.ToString() + "."));
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
                        server.SendPacket("REFUSEDUEL||" + args[1]);
                        return;
                    }
                    RoomInfos info = RoomInfos.FromName(args[2], "", false);
                    DuelRequest_frm request = new DuelRequest_frm(args[1] + Program.LanguageManager.Translation.DuelReqestMessage + Environment.NewLine +
                        Program.LanguageManager.Translation.DuelRequestMode + RoomInfos.GameMode(info.Mode) + Program.LanguageManager.Translation.DuelRequestRules + RoomInfos.GameRule(info.Rule) + Program.LanguageManager.Translation.DuelRequestBanlist + LauncherHelper.GetBanListFromInt(info.BanList));


                    if (request.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, null, "You accepted " + args[1] + " duel request."));
                        server.SendPacket("ACCEPTDUEL||" + args[1] + "||" + args[2]);
                    }
                    else
                    {
                        WriteMessage(new ChatMessage(MessageType.System, null, "You refused " + args[1] + " duel request."));
                        server.SendPacket("REFUSEDUEL||" + args[1]);
                    }
                }
                else if (cmd == "REFUSE")
                {
                    WriteMessage(new ChatMessage(MessageType.System, null, args[1] + " refused your duel request."));
                }
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
            IgnoreList.Items.Remove(IgnoreList.SelectedItem);
            SaveIgnoreList();
            WriteMessage(new ChatMessage(MessageType.System, null, UserList.SelectedItem.ToString() + " has been removed from your ignore list."));
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
            ChatRTB.Font = new Font(Program.Config.ChatFont, (float)Program.Config.ChatSize);
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
            ListBox list = (UsersControl.SelectedTab.Name == UsersTab.Name ? UserList : FriendList);

            if (list.SelectedItem == null)
                return;

            if (!PMWindows.ContainsKey(list.SelectedItem.ToString()))
            {
                PMWindows.Add(list.SelectedItem.ToString(), new PmWindow_frm(list.SelectedItem.ToString(), true, server));
                PMWindows[list.SelectedItem.ToString()].Show();
                PMWindows[list.SelectedItem.ToString()].FormClosed += Chat_frm_FormClosed;
            }
            else
            {
                PMWindows[list.SelectedItem.ToString()].BringToFront();
            }
        }
    }
}
