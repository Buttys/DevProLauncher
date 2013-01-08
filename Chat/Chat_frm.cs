using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using YGOPro_Launcher.Chat.Enums;
using System.Media;
using System.Diagnostics;
using YGOPro_Launcher.Config;

namespace YGOPro_Launcher.Chat
{
    public partial class Chat_frm : Form
    {
        ChatClient server = new ChatClient();
        Dictionary<string,UserData> UserData = new Dictionary<string,UserData>();
        Dictionary<string, PmWindow_frm> PmWindows = new Dictionary<string, PmWindow_frm>();
        List<string> MsgHistory = new List<string>();
        Dictionary<string, List<string>> ChannelUsers = new Dictionary<string, List<string>>();
        int Historyindex = -1;
        
        public Chat_frm()
        {
            
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            
            ChatTabs.TabPages.Add(new ChatWindow("DevPro",false));
            ChatTabs.SelectedIndexChanged += new EventHandler(ChatTabsChanged);
            ChatInput.KeyPress += new KeyPressEventHandler(ChatInput_KeyPress);
            ChatInput.KeyDown += new KeyEventHandler(Input_KeyDown);

            server.Message += new ChatClient.ServerMessage(NewMessage);
            server.UserList += new ChatClient.ServerResponse(CreateUserList);
            server.AddUser += new ChatClient.ServerResponse(AddUser);
            server.RemoveUser += new ChatClient.ServerResponse(RemoveUser);
            server.Login += new ChatClient.ServerResponse(LoginCheck);
            server.DuelRequest += new ChatClient.ServerResponse(HandleDuelRequest);
            server.Error += new ChatClient.ServerMessage(NewMessage);
            server.FriendList += new ChatClient.ServerResponse(LoadFriends);

            UserList.DrawItem += new DrawItemEventHandler(UserList_DrawItem);
     
            FriendList.DrawItem += new DrawItemEventHandler(FriendList_DrawItem);
            UserList.DoubleClick += List_DoubleClick;
            FriendList.DoubleClick += List_DoubleClick;

            UserList.MouseUp += new MouseEventHandler(UserList_MouseUp);
            FriendList.MouseUp += new MouseEventHandler(FriendList_MouseUp);
            IgnoreList.MouseUp += IgnoreList_MouseUp;

            DonateIMG.Click += new EventHandler(DonateClick);

            LoadIgnoreList();
            ApplyTranslation();
            ApplyNewSettings();
            
        }

        private void ApplyTranslation()
        {
            LanguageInfo lang = Program.LanguageManager.Translation;

            OptionsBtn.Text = lang.chatBtnoptions;
            UserTabs.TabPages[0].Text = lang.chatTabUsers;
            UserTabs.TabPages[1].Text = lang.chatTabFriends;
            UserTabs.TabPages[2].Text = lang.chatTabIgnore;

        }

        private void ChatTabsChanged(object sender, EventArgs e)
        {
            UserList.Items.Clear();

            if (!ChannelUsers.ContainsKey(CurrentChatWindow().Name))
                ChannelUsers.Add(CurrentChatWindow().Name, new List<string>());

            foreach (string user in ChannelUsers[CurrentChatWindow().Name])
            {
                UserList.Items.Add(user);
            }
            SortUserList();
        }



        private void DonateClick(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=KH8843F5JWUNS");
        }

        public void LoadIgnoreList()
        {
            string[] users = Program.Config.IgnoreList.Split(new string[] {","},StringSplitOptions.RemoveEmptyEntries);
            foreach (string user in users)
                IgnoreList.Items.Add(user);
        }

        public void LoadFriends(string message)
        {
            if (message == "not found")
                return;

            string[] friends = message.Split(',');
            foreach (string friend in friends)
                FriendList.Items.Add(friend);

        }

        public void Connect()
        {
            if (server.Connect(Program.Config.ServerAddress, Program.Config.ChatPort))
            {
                server.SendPacket("LOGIN||" + Program.UserInfo.Username + "||" + Program.Config.Password + "||" + LauncherHelper.GetUID());
            }
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
                {
                    NewMessage(new ChatMessage(MessageType.System, Program.UserInfo, CurrentChatWindow().Name, "Unable to login.", false));
                }
                else if (message == "Banned")
                {
                    NewMessage(new ChatMessage(MessageType.System, Program.UserInfo, CurrentChatWindow().Name, "You are banned.", false));
                }
                else if (message == "LoginDown")
                {
                    NewMessage(new ChatMessage(MessageType.System, Program.UserInfo, CurrentChatWindow().Name, "Login Server is currently down or locked.", false));
                }
                else
                {
                    NewMessage(new ChatMessage(MessageType.System, Program.UserInfo, CurrentChatWindow().Name, "Connected to the DevPro chat server.", false));

                    if (Program.Config.Language != "English")
                    {
                        CreateChatWindow("DevPro-" + Program.Config.Language, false);
                        server.SendPacket("JOIN||" + "DevPro-" + Program.Config.Language);
                    }
                    if (Program.UserInfo.Rank > 0)
                    {
                        CreateChatWindow("Dev", false);
                        server.SendPacket("JOIN||" + "Dev");
                    }

                    ChatTabs.SelectedIndex = 0;
                }
            }
        }

        private void Chat_frm_FormClosed(object sender, EventArgs e)
        {
            PmWindow_frm form = (PmWindow_frm)sender;
            PmWindows.Remove(form.Name);
        }

        private void NewMessage(ChatMessage message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ChatMessage>(NewMessage), message);
            }
            else
            {
                if (message.Type == MessageType.PrivateMessage ||
                    message.Type == MessageType.Message ||
                    message.Type == MessageType.Me)
                {
                    if (IgnoreContainsUser(message.From.Username))
                        return;
                }

                if (Program.Config.PmWindows && message.Type == MessageType.PrivateMessage)
                {
                    if (PmWindows.ContainsKey(message.Channel))
                    {
                        PmWindows[message.Channel].WriteMessage(message);
                    }
                    else
                    {
                        PmWindows.Add(message.Channel, new PmWindow_frm(message.Channel, true,server));
                        PmWindows[message.Channel].WriteMessage(message);
                        PmWindows[message.Channel].Show();
                        PmWindows[message.Channel].FormClosed += Chat_frm_FormClosed;
                    }

                }
                else
                {

                    ChatWindow window = GetChatWindow(message.Channel);
                    if (window == null)
                    {
                        ChatTabs.TabPages.Add(new ChatWindow(message.Channel, true));
                        window = GetChatWindow(message.Channel);
                        window.WriteMessage(message,AutoScrollChat.Checked);
                    }
                    else
                    {
                        window.WriteMessage(message, AutoScrollChat.Checked);
                    }

                    if (ChatTabs.SelectedTab.Name != window.Name && window.isprivate)
                        SystemSounds.Beep.Play();
                }
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
                        {
                            UserData[info[0]] = new UserData() { Username = info[0], Rank = Int32.Parse(info[1]) };
                        }
                        else
                        {
                            UserData.Add(info[0], new UserData() { Username = info[0], Rank = Int32.Parse(info[1]) });
                        }

                        if (ChannelUsers.ContainsKey(parts[1]))
                        {
                            if (!ChannelUsers[parts[1]].Contains(info[0]))
                            {
                                ChannelUsers[parts[1]].Add(info[0]);
                            }

                        }
                        else
                        {
                                ChannelUsers.Add(parts[1], new List<string>());
                                ChannelUsers[parts[1]].Add(info[0]);
                        }


                        ChatTabsChanged(null, EventArgs.Empty);
                        UpdateUserCount();

                    }
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
                
                if (!ChannelUsers.ContainsKey(parts[1]))
                    ChannelUsers.Add(parts[1], new List<string>());

                    string[] info = parts[0].Split(',');
                    foreach (object user in UserList.Items)
                    {
                        if (info[0] == user.ToString())
                        {
                            if (!ChannelContainsUser(info[0], parts[1]))
                                ChannelUsers[parts[1]].Add(info[0]);
                            if (!UserData.ContainsKey(info[0]))
                                UserData.Add(info[0], new UserData() { Username = info[0], Rank = Int32.Parse(info[1]) });
                            else
                                UserData[info[0]] = new UserData() { Username = info[0], Rank = Int32.Parse(info[1]) };

                            if (info[0] == Program.UserInfo.Username)
                                Program.UserInfo.Rank = Int32.Parse(info[1]);

                            ChatTabsChanged(null, EventArgs.Empty);
                            return;
                        }
                    }

                    if (!UserData.ContainsKey(info[0]))
                        UserData.Add(info[0], new UserData() { Username = info[0], Rank = Int32.Parse(info[1]) });
                    else
                        UserData[info[0]] = new UserData(){ Username = info[0], Rank = Int32.Parse(info[1]) };
                
                if(!ChannelContainsUser(info[0],parts[1]))
                    ChannelUsers[parts[1]].Add(info[0]);

                if (!Program.Config.HideJoinLeave)
                {
                    if (FriendListContains(info[0]))
                        NewMessage(new ChatMessage(MessageType.Join, Program.UserInfo, parts[1], "Your friend " + info[0] + " has joined the channel.", false));
                    else
                        NewMessage(new ChatMessage(MessageType.Join, Program.UserInfo, parts[1], info[0] + " has joined the channel.", false));

                }
                else
                {

                    if (!Program.Config.HideJoinLeave)
                        NewMessage(new ChatMessage(MessageType.Join, Program.UserInfo, parts[1], info[0] + " has joined.", false));
                    else if (FriendListContains(info[0]))
                        NewMessage(new ChatMessage(MessageType.Join, Program.UserInfo, parts[1], "Your friend " + info[0] + " has logged in.", false));
                }

                if (parts[1] == CurrentChatWindow().Name)
                {
                    InsetIntoUserList(info[0]);
                    UpdateUserCount();
                }
               
            }
        }

        private bool ChannelContainsUser(string username,string channel)
        {
            if (ChannelUsers.ContainsKey(channel))
            {
                foreach (string user in ChannelUsers[channel])
                {
                    if (user == username)
                        return true;
                }
            }
            else
                return false;

            return false;

        }

        private void RemoveUser(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(RemoveUser), message);
            }
            else
            {
                string[] parts = message.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

                if (ChannelUsers.ContainsKey(parts[1]))
                {
                    if(ChannelUsers[parts[1]].Contains(parts[0]))
                        ChannelUsers[parts[1]].Remove(parts[0]);
                }
                if (!Program.Config.HideJoinLeave)
                {
                    if (FriendListContains(parts[0]))
                        NewMessage(new ChatMessage(MessageType.Leave, Program.UserInfo, parts[1], "Your friend " + parts[0] + " has left the channel.", false));
                    else
                        NewMessage(new ChatMessage(MessageType.Leave, Program.UserInfo, parts[1], parts[0] + " has left the channel.", false));

                }
                else
                {

                    if (!Program.Config.HideJoinLeave)
                        NewMessage(new ChatMessage(MessageType.Leave, Program.UserInfo, parts[1], parts[0] + " has left.", false));
                    else if (FriendListContains(parts[0]))
                        NewMessage(new ChatMessage(MessageType.Leave, Program.UserInfo, parts[1], "Your friend " + parts[0] + " has logged out.", false));
                }

                if (parts[1] == CurrentChatWindow().Name)
                {
                    //UserList.Items.Remove(parts[0]);
                    for (int i = 0; i < UserList.Items.Count - 1; i++)
                    {
                        if (UserList.Items[i].ToString() == parts[0])
                        {
                            UserList.Items.RemoveAt(i);
                            break;
                        }
                    }
                    UpdateUserCount();

                }
                

            }
        }

        private bool UserExsists(string username)
        {
            foreach (object user in UserList.Items)
            {
                if (username == user.ToString())
                    return true;
            }

            return false;
        }

        private void ChatInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (ChatInput.Text == "")
                    return;

                Dictionary<string, int> lettercount = new Dictionary<string, int>();

                foreach (char letter in ChatInput.Text)
                {
                    if (!lettercount.ContainsKey(letter.ToString()))
                        lettercount.Add(letter.ToString(), 1);
                    else
                        lettercount[letter.ToString()]++;
                }

                if (lettercount.Keys.Count <= 1)
                {
                    foreach (string key in lettercount.Keys)
                    {
                        if (lettercount[key] > 5)
                        {
                            ChatInput.Clear();
                            return;
                        }
                    }
                }

                if (CurrentChatWindow().isprivate && ChatInput.Text.StartsWith("/"))
                {
                    string[] parts = ChatInput.Text.Split(' ');
                    string cmd = parts[0].Substring(1);

                    if (cmd == "leave" || cmd == "part" || cmd == "quit")
                    {
                        if (CurrentChatWindow().Name == "DevPro")
                        {
                            NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "You cannot leave the master channel."));
                            ChatInput.Clear();
                            return;
                        }
                        ChatTabs.TabPages.Remove(CurrentChatWindow());
                        ChatInput.Clear();
                    }
                    else if (cmd == "join")
                    {
                        if (GetChatWindow(parts[1]) == null)
                        {
                            CreateChatWindow(parts[1], false);
                            server.SendPacket("JOIN||" + parts[1]);
                        }
                    }
                    else
                    {
                        NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "Commands are not supported in private windows."));
                    }
                    return;
                }
                if (ChatInput.Text.StartsWith("/"))
                {
                    string[] parts = ChatInput.Text.Split(' ');
                    string cmd = parts[0].Substring(1);
                    if (cmd == "w")
                    {
                        if (parts.Length < 3)
                        {
                            NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "Invalid args."));
                        }
                        else
                        {
                            if (UserExsists(parts[1]))
                            {
                                if (Program.Config.PmWindows)
                                {
                                    if (PmWindows.ContainsKey(parts[1]))
                                    {
                                        PmWindows[parts[1]].WriteMessage(new ChatMessage(MessageType.Message, Program.UserInfo, parts[1], ChatInput.Text.Substring(parts[0].Length + parts[1].Length + 1), false));
                                    }
                                    else
                                    {
                                        PmWindows.Add(parts[1], new PmWindow_frm(parts[1], true, server));
                                        PmWindows[parts[1]].WriteMessage(new ChatMessage(MessageType.Message, Program.UserInfo, parts[1], ChatInput.Text.Substring(parts[0].Length + parts[1].Length + 1), false));
                                        PmWindows[parts[1]].Show();
                                        PmWindows[parts[1]].FormClosed += Chat_frm_FormClosed;
                                    }
                                }
                                else
                                {
                                    CreateChatWindow(parts[1], true);
                                    NewMessage(new ChatMessage(MessageType.Message, Program.UserInfo, parts[1], ChatInput.Text.Substring(parts[0].Length + parts[1].Length + 1), false));
                                }
                                server.SendPacket("MSG||" + parts[1] + "||" + (int)MessageType.PrivateMessage + "||" + LauncherHelper.StringToBase64(ChatInput.Text.Substring(parts[0].Length + parts[1].Length + 1)));
                            }
                            else
                                NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, parts[1] + " not found."));
                        }
                    }
                    else if (cmd == "me")
                    {
                        string messagetext = ChatInput.Text.Substring(3);
                        server.SendPacket("MSG||" + CurrentChatWindow().Name + "||" + (int)MessageType.Me + "||" + LauncherHelper.StringToBase64(messagetext));
                    }
                    else if (cmd == "join")
                    {
                        if (GetChatWindow(parts[1]) == null)
                        {
                            CreateChatWindow(parts[1], false);
                            server.SendPacket("JOIN||" + parts[1]);
                        }
                    }
                    else if (cmd == "leave" || cmd == "part" || cmd == "quit")
                    {
                        if (CurrentChatWindow().Name == "DevPro")
                        {
                            NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "You cannot leave the master channel."));
                            ChatInput.Clear();
                            return;
                        }
                        if(Program.Config.PmWindows && !CurrentChatWindow().isprivate)
                            server.SendPacket("LEAVE||" + CurrentChatWindow().Name);
                        ChatTabs.TabPages.Remove(CurrentChatWindow());

                    }
                    else if (Program.UserInfo.Rank > 0)
                    {
                        if (cmd != "op")
                        {
                            server.SendPacket("ADMIN||" + cmd.ToUpper() + "||" + ChatInput.Text.Replace(parts[0], "").Trim());
                            Program.ServerConnection.SendPacket("ADMIN||" + cmd.ToUpper() + "||" + ChatInput.Text.Replace(parts[0], "").Trim());
                            AddChatHistory(ChatInput.Text);
                        }
                        else
                        {
                            int rank = 0;
                            if (Int32.TryParse(parts[parts.Length - 1], out rank))
                            {
                                string username = null;
                                for (int i = 0; i < parts.Length - 1; i++)
                                {
                                    if (i != 0 && i != parts.Length - 1)
                                    {
                                        if (username == null)
                                            username += parts[i];
                                        else
                                            username += " " + parts[i];
                                    }

                                }

                                server.SendPacket("ADMIN||OP||" + username.Trim() + "||" + rank.ToString());
                            }
                            else
                            {

                                NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "Invalid args."));
                            }
                        }

                    }
                    else
                    {

                        NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "Unknown command."));
                    }
                }
                else
                {
                    if (CurrentChatWindow().isprivate)
                    {
                        NewMessage(new ChatMessage(MessageType.Message, Program.UserInfo, CurrentChatWindow().Name, ChatInput.Text, false));
                        server.SendPacket("MSG||" + CurrentChatWindow().Name + "||" + (int)MessageType.PrivateMessage + "||" + LauncherHelper.StringToBase64(ChatInput.Text));
                    }
                    else
                    {
                        server.SendPacket("MSG||" + CurrentChatWindow().Name + "||" + (int)MessageType.Message + "||" + LauncherHelper.StringToBase64(ChatInput.Text));
                    }
                }
                AddChatHistory(ChatInput.Text);
                ChatInput.Clear();
                e.Handled = true;
            }
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Down:
                    if (MsgHistory.Count == 0)
                        return;

                    if (Historyindex <= 0)
                        Historyindex = MsgHistory.Count - 1;
                    else
                        Historyindex--;
                    ChatInput.Text = MsgHistory[Historyindex];
                    break;
                    
                case Keys.Up:
                    if (MsgHistory.Count == 0)
                        return;

                    
                    if (Historyindex != MsgHistory.Count - 1)
                        Historyindex++;
                    else
                        Historyindex = 0;
                    ChatInput.Text = MsgHistory[Historyindex];
                    break;
                case Keys.Tab:
                    string[] parts = ChatInput.Text.Split(' ');
                    foreach (string user in UserList.Items)
                    {
                        if (user.ToLower().StartsWith(parts[parts.Length - 1].ToLower()))
                        {
                           ChatInput.Text = ChatInput.Text.Substring(ChatInput.Text.Length - parts[parts.Length - 1].Length, parts[parts.Length - 1].Length);
                           ChatInput.Text += user;
                            break;
                        }
                    }

                    break;
            }
            
        }

        private void AddChatHistory(string message)
        {
            if (MsgHistory.Count != 5)
            {
                MsgHistory.Add(message);
            }
            else
            {
                MsgHistory.RemoveAt(0);
                MsgHistory.Add(message);
            }
        }

        private ChatWindow CurrentChatWindow()
        {
            return (ChatWindow)ChatTabs.SelectedTab;
        }
        private ChatWindow GetChatWindow(string channel)
        {
            foreach (ChatWindow window in ChatTabs.TabPages)
            {
                if (window.Name == channel)
                    return window;
            }
            return null;
        }

        private void CreateChatWindow(string name,bool isprivate)
        {
            foreach (TabPage window in ChatTabs.TabPages)
            {
                if (window.Name == name)
                {
                    ChatTabs.SelectedTab = window;
                    return;
                }
            }

            ChatTabs.TabPages.Add(new ChatWindow(name,isprivate));
            ChatTabs.SelectedTab = GetChatWindow(name);

        }

        private void UserList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = UserList.IndexFromPoint(e.Location);

                if (index == -1) return;
                else
                    UserList.SelectedIndex = index;

                if(UserList.SelectedItem == null)
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

                mnu.Items.AddRange(new ToolStripMenuItem[] { mnuprofile, mnuduel, mnuremovefriend});

                mnu.Show(FriendList, e.Location);
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

        private void RemoveFriend(object sender, EventArgs e)
        {
                NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, FriendList.SelectedItem.ToString() + " has been removed from your friendlist."));
                server.SendPacket("REMOVEFRIEND||" + FriendList.SelectedItem.ToString());
                FriendList.Items.Remove(FriendList.SelectedItem.ToString());
        }

        private bool UserListContains(string username)
        {
            foreach(object user in UserList.Items)
            {
                if (username == user.ToString())
                    return true;
            }

            return false;
        }

        private bool FriendListContains(string username)
        {
            foreach (object user in FriendList.Items)
            {
                if (username == user.ToString())
                    return true;
            }

            return false;
        }

        private void IgnoreUser(object sender, EventArgs e)
        {
            if (UserList.SelectedItem.ToString() == Program.UserInfo.Username)
            {
                NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "You cannot ignore yourself."));
                return;
            }
            if (IgnoreContainsUser(UserList.SelectedItem.ToString()))
            {
                NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, UserList.SelectedItem.ToString() + " is already on your ignore list."));
                return;
            }

            IgnoreList.Items.Add(UserList.SelectedItem.ToString());
            SaveIgnoreList();
            NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, UserList.SelectedItem.ToString() + " has been added to your ignore list."));
        }

        private void UnignoreUser(object sender, EventArgs e)
        {
            IgnoreList.Items.Remove(IgnoreList.SelectedItem);
            SaveIgnoreList();
            NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, UserList.SelectedItem.ToString() + " has been removed from your ignore list."));
        }

        private bool IgnoreContainsUser(string username)
        {
            foreach (object user in IgnoreList.Items)
            {
                if (username == user.ToString())
                {
                    return true;
                }
            }

            return false;
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
            Program.SaveConfig(Program.ConfigurationFilename,Program.Config);

        }

        private void BanUser(object sender, EventArgs e)
        {
            if (UserList.SelectedItem == null)
                return;
            if(MessageBox.Show("Are you sure you want to ban " + UserList.SelectedItem.ToString(),"Ban User",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
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
                    NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "You cannot be your own friend."));
                    return;
                }

                foreach (object user in FriendList.Items)
                {
                    if (user.ToString() == UserList.SelectedItem.ToString())
                    {
                        NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, UserList.SelectedItem.ToString() + " is already your friend."));
                        return;
                    }
                }

                NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, UserList.SelectedItem.ToString() + " has been added to your friend list."));
                FriendList.Items.Add(UserList.SelectedItem.ToString());
                server.SendPacket("ADDFRIEND||" + UserList.SelectedItem.ToString());
        }

        private void ViewProfile(object sender, EventArgs e)
        {
            ListBox list = (UserTabs.SelectedTab.Text == Program.LanguageManager.Translation.chatTabUsers ? UserList:FriendList);
            if (list.SelectedItem == null)
                return;
            
            Profile_frm profile = new Profile_frm(list.SelectedItem.ToString());
            profile.ShowDialog();

        }

        private void RequestDuel(object sender, EventArgs e)
        {
            ListBox list = (UserTabs.SelectedTab.Text == Program.LanguageManager.Translation.chatTabUsers ? UserList : FriendList);

            if (list.SelectedItem == null)
                return;
            
            if (list.SelectedItem.ToString() == Program.UserInfo.Username)
                NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "You cannot duel request your self."));
            else
            {
                Host form = new Host();
                server.SendPacket("REQUESTDUEL||" + list.SelectedItem.ToString() +"||"
                    + form.GenerateGameString(false));
                NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "Duel request sent to " + list.SelectedItem.ToString() + "."));
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
                    DuelRequest_frm request = new DuelRequest_frm(args[1] + " has challenged you to a ranked duel! Do you accept?" + Environment.NewLine +
                        "Type: " + RoomInfos.GameMode(info.Mode) + " Rules: " + RoomInfos.GameRule(info.Rule) + " Banlist: " + LauncherHelper.GetBanListFromInt(info.BanList));


                    if (request.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                    {
                        NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "You accepted " + args[1] + " duel request."));
                        server.SendPacket("ACCEPTDUEL||" + args[1] + "||" + args[2]);
                    }
                    else
                    {
                        NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "You refused " + args[1] + " duel request."));
                        server.SendPacket("REFUSEDUEL||" + args[1]);
                    }
                }
                else if (cmd == "REFUSE")
                {
                    NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, args[1] + " refused your duel request."));
                }
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
                    g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black) : new SolidBrush(Color.Blue)) : new SolidBrush(Color.FromName(Program.Config.ChatBGColor)), e.Bounds);
                    g.DrawString(text, e.Font, (selected) ? Brushes.White : Brushes.Black,
                         UserList.GetItemRectangle(index).Location);
                    e.DrawFocusRectangle();
                    return;
                }

                g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black) : new SolidBrush(Color.Blue)) : new SolidBrush(Color.FromName(Program.Config.ChatBGColor)), e.Bounds);

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
            if (index >= 0 && index < UserList.Items.Count)
            {
                string text = FriendList.Items[index].ToString();
                Graphics g = e.Graphics;

                g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black): new SolidBrush(Color.Blue)) : new SolidBrush(Color.FromName(Program.Config.ChatBGColor)), e.Bounds);

                // Print text
                g.DrawString((Program.Config.ColorBlindMode ? (UserListContains(text) ? text +" (Online)" :text + " (Offline)"):text), e.Font, 
                    (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black: (UserListContains(text) ? Brushes.Green : Brushes.Red)),
                    FriendList.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        private void List_DoubleClick(object sender, EventArgs e)
        {
            ListBox list = (UserTabs.SelectedTab.Text == Program.LanguageManager.Translation.chatTabUsers ? UserList : FriendList);

            if (list.SelectedItem == null)
                return;

            if (Program.Config.PmWindows)
            {
                if (!PmWindows.ContainsKey(list.SelectedItem.ToString()))
                {
                    PmWindows.Add(list.SelectedItem.ToString(), new PmWindow_frm(list.SelectedItem.ToString(), true, server));
                    PmWindows[list.SelectedItem.ToString()].Show();
                    PmWindows[list.SelectedItem.ToString()].FormClosed += Chat_frm_FormClosed;
                }
                else
                {
                    PmWindows[list.SelectedItem.ToString()].BringToFront();
                }
            }
            else
            {
                CreateChatWindow(list.SelectedItem.ToString(), true);
            }
                           
        }

        private void InsetIntoUserList(string username)
        {
            List<string> adminlist = new List<string>();
            List<string> botlist = new List<string>();
            List<string> normaluserlist = new List<string>();
            int rank = 0;

            if (UserData.ContainsKey(username))
                rank = UserData[username].Rank;

            foreach (object user in UserList.Items)
            {
                if (UserData.ContainsKey(user.ToString()))
                    if (UserData[user.ToString()].Rank > 0 && UserData[user.ToString()].Rank != 10)
                        adminlist.Add(user.ToString());
            }

            if (rank > 0 && rank != 10)
                adminlist.Add(username);

            adminlist.Sort();

            foreach (object user in UserList.Items)
            {
                if (UserData.ContainsKey(user.ToString()))
                    if (UserData[user.ToString()].Rank == 10)
                        botlist.Add(user.ToString());
            }

            if (rank == 10)
                botlist.Add(username);

            botlist.Sort();

            foreach (object user in UserList.Items)
            {
                if (UserData.ContainsKey(user.ToString()))
                    if (UserData[user.ToString()].Rank == 0)
                        normaluserlist.Add(user.ToString());
            }

            if (rank == 0)
                normaluserlist.Add(username);

            normaluserlist.Sort();

            if (rank >0 && rank !=  10)
            {
                for (int i = 0; i < adminlist.Count - 1; i++)
                {
                    if (adminlist[i] == username)
                    {
                        UserList.Items.Insert(i, username);
                        return;
                    }
                }
                UserList.Items.Insert(0, username);
                return;
            }

            if (rank == 10)
            {
                for (int i = 0; i < botlist.Count - 1; i++)
                {
                    if (botlist[i] == username)
                    {
                        UserList.Items.Insert(adminlist.Count + i, username);
                        return;
                    }
                }
                UserList.Items.Insert(adminlist.Count, username);
                return;
            }

            if (rank == 0)
            {
                for (int i = 0; i < normaluserlist.Count - 1; i++)
                {
                    if (normaluserlist[i] == username)
                    {
                        UserList.Items.Insert(adminlist.Count + botlist.Count + i, username);
                        return;
                    }
                }
                UserList.Items.Insert(adminlist.Count + botlist.Count, username);
                return;
            }

        }

        private void SortUserList()
        {
            List<string> adminlist = new List<string>();
            List<string> botlist = new List<string>();
            List<string> normaluserlist = new List<string>();


            foreach (object user in UserList.Items)
            {
                if (UserData.ContainsKey(user.ToString()))
                    if (UserData[user.ToString()].Rank > 0 && UserData[user.ToString()].Rank != 10)
                        adminlist.Add(user.ToString());
            }
            adminlist.Sort();

            foreach (object user in UserList.Items)
            {
                if (UserData.ContainsKey(user.ToString()))
                    if (UserData[user.ToString()].Rank == 10)
                        botlist.Add(user.ToString());
            }
            botlist.Sort();

            foreach (object user in UserList.Items)
            {
                if (UserData.ContainsKey(user.ToString()))
                    if (UserData[user.ToString()].Rank == 0)
                        normaluserlist.Add(user.ToString());
            }

            normaluserlist.Sort();

            UserList.Items.Clear();
            UserList.Items.AddRange(adminlist.ToArray());
            UserList.Items.AddRange(botlist.ToArray());
            UserList.Items.AddRange(normaluserlist.ToArray());

        }

        private void OptionsBtn_Click(object sender, EventArgs e)
        {
            Settings form = new Settings();
            form.OptionTabControl.SelectedIndex = 1;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ApplyNewSettings();
            }
        }

        private void UpdateUserCount()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(UpdateUserCount));
            }
            else
            {
                UserCount.Text = Program.LanguageManager.Translation.chatUserCount + ChannelUsers[CurrentChatWindow().Name].Count;
            }
        }

        public void ApplyNewSettings()
        {

            foreach (ChatWindow chat in ChatTabs.TabPages)
            {
                chat.ApplyNewSettings();
                UserList.BackColor = Color.FromName(Program.Config.ChatBGColor);
                FriendList.BackColor = Color.FromName(Program.Config.ChatBGColor);
                IgnoreList.BackColor = Color.FromName(Program.Config.ChatBGColor);
                IgnoreList.ForeColor = Color.FromName(Program.Config.NormalTextColor);
                ChatTabsChanged(null, EventArgs.Empty);
            }
            if (!Program.Config.PmWindows)
            {
                List<string> keys = new List<string>();
                foreach (string key in PmWindows.Keys)
                    keys.Add(key);
                foreach (string key in keys)
                    PmWindows[key].Close();
            }
            else
            {
                List<ChatWindow> removelist = new List<ChatWindow>();
                foreach (ChatWindow tab in ChatTabs.TabPages)
                {
                    if (tab.isprivate)
                        removelist.Add(tab);
                }

                foreach (ChatWindow tab in removelist)
                    ChatTabs.TabPages.Remove(tab);
            }

        }

    }
}
