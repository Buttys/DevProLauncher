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

namespace YGOPro_Launcher.Chat
{
    public partial class Chat_frm : Form
    {
        ChatClient server = new ChatClient();
        Dictionary<string,UserData> UserData = new Dictionary<string,UserData>();
        Dictionary<string, PmWindow_frm> PmWindows = new Dictionary<string, PmWindow_frm>();
        
        public Chat_frm()
        {
            
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            ChatTabs.TabPages.Add(new ChatWindow("DevPro",false));
            ChatInput.KeyPress += new KeyPressEventHandler(ChatInput_KeyPress);

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
            UserList.DoubleClick += UserList_DoubleClick;
            UserList.MouseUp += new MouseEventHandler(UserList_MouseUp);
            FriendList.MouseUp += new MouseEventHandler(FriendList_MouseUp);
            IgnoreList.MouseUp += IgnoreList_MouseUp;

            DonateIMG.Click += new EventHandler(DonateClick);

            LoadIgnoreList();
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
                server.SendPacket("LOGIN||" + Program.UserInfo.Username + "||" + Program.Config.Password);
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
                        window.WriteMessage(message);
                    }
                    else
                    {
                        window.WriteMessage(message);
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
                    UserData.Clear();
                    UserList.Items.Clear();
                    string[] users = userlist.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string user in users)
                    {
                        string[] info = user.Split(',');
                        UserList.Items.Add(info[0]);
                        UserData.Add(info[0], new UserData() { Username = info[0], Rank = Int32.Parse(info[1]) });
                    }
                    SortUserList();
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
                    string[] info = userinfo.Split(',');
                    foreach (object user in UserList.Items)
                    {
                        if (info[0] == user.ToString())
                        {
                            return;
                        }
                    }

                    UserList.Items.Add(info[0]);
                    if (!UserData.ContainsKey(info[0]))
                        UserData.Add(info[0], new UserData() { Username = info[0], Rank = Int32.Parse(info[1]) });
                    else
                        UserData[info[0]] = new UserData(){ Username = info[0], Rank = Int32.Parse(info[1]) };
                
                if(!Program.Config.HideJoinLeave)
                    NewMessage(new ChatMessage(MessageType.Join, Program.UserInfo, "DevPro", info[0] + " has joined.", false));
                else if(FriendListContains(info[0]))
                    NewMessage(new ChatMessage(MessageType.Join, Program.UserInfo, "DevPro", "Your friend " + info[0] + " has logged in.", false));
                    
                if(UserData[info[0]].Rank > 0)
                        SortUserList();
            }
        }

        private void RemoveUser(string username)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(RemoveUser), username);
            }
            else
            {
                    foreach (object user in UserList.Items)
                    {
                        if (username == user.ToString())
                        {
                            if (UserData.ContainsKey(user.ToString()))
                                UserList.Items.Remove(user);
                            if (!Program.Config.HideJoinLeave)
                                NewMessage(new ChatMessage(MessageType.Leave, Program.UserInfo, "DevPro", user + " has left.", false));
                            else if (FriendListContains(username))
                                NewMessage(new ChatMessage(MessageType.Leave, Program.UserInfo, "DevPro", "Your friend " + user + " has logged off.", false));
                            return;
                        }
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
                if (CurrentChatWindow().isprivate && ChatInput.Text.StartsWith("/"))
                {
                    NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "Commands are not supported in private windows."));
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
                                CreateChatWindow(parts[1],true);
                                NewMessage(new ChatMessage(MessageType.Message, Program.UserInfo, parts[1], ChatInput.Text.Substring(parts[0].Length + parts[1].Length + 1), false));
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
                    else if (Program.UserInfo.Rank > 0)
                    {
                        if (cmd != "op")
                        {
                            server.SendPacket("ADMIN||" + cmd.ToUpper() + "||" + ChatInput.Text.Replace(parts[0], "").Trim());
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
                ChatInput.Clear();
                e.Handled = true;
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

                ContextMenuStrip mnu = new ContextMenuStrip();
                ToolStripMenuItem mnuprofile = new ToolStripMenuItem("View Profile");
                ToolStripMenuItem mnuduel = new ToolStripMenuItem("Request Duel");
                ToolStripMenuItem mnufriend = new ToolStripMenuItem("Add to friends");
                ToolStripMenuItem mnuignore = new ToolStripMenuItem("Ignore User");
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

                ContextMenuStrip mnu = new ContextMenuStrip();
                ToolStripMenuItem mnuremovefriend = new ToolStripMenuItem("Remove Friend");
                ToolStripMenuItem mnuprofile = new ToolStripMenuItem("View Profile");
                ToolStripMenuItem mnuduel = new ToolStripMenuItem("Request Duel");

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
                ToolStripMenuItem mnuremoveignore = new ToolStripMenuItem("Remove");

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
            if(MessageBox.Show("Are you sure you want to ban " + UserList.SelectedItem.ToString(),"Ban User",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                server.SendPacket("ADMIN||BAN||" + UserList.SelectedItem.ToString());
        }
        private void KickUser(object sender, EventArgs e)
        {
            server.SendPacket("ADMIN||KICK||" + UserList.SelectedItem.ToString());
        }

        private void AddFriend(object sender, EventArgs e)
        {
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
            ListBox list = (UserTabs.SelectedTab.Name == "UserTab" ? UserList:FriendList);
            Profile_frm profile = new Profile_frm(list.SelectedItem.ToString());
            profile.ShowDialog();

        }

        private void RequestDuel(object sender, EventArgs e)
        {
            ListBox list = (UserTabs.SelectedTab.Name == "UserTab" ? UserList : FriendList);
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
                    g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black): new SolidBrush(Color.Blue)) : new SolidBrush(Color.White), e.Bounds);
                    g.DrawString(text, e.Font, (selected) ? Brushes.White : Brushes.Black,
                         UserList.GetItemRectangle(index).Location);
                    e.DrawFocusRectangle();
                    return;
                }

                g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black) : new SolidBrush(Color.Blue)) : new SolidBrush(Color.White), e.Bounds);

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

                g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black): new SolidBrush(Color.Blue)) : new SolidBrush(Color.White), e.Bounds);

                // Print text
                g.DrawString((Program.Config.ColorBlindMode ? (UserListContains(text) ? text +" (Online)" :text + " (Offline)"):text), e.Font, 
                    (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black: (UserListContains(text) ? Brushes.Green : Brushes.Red)),
                    FriendList.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        private void UserList_DoubleClick(object sender, EventArgs e)
        {
            CreateChatWindow(UserList.SelectedItem.ToString(), true);
        }

        private void SortUserList()
        {
                List<object> sortedlist = new List<object>();

                foreach (object user in UserList.Items)
                {
                    if (UserData.ContainsKey(user.ToString()))
                        if (UserData[user.ToString()].Rank > 0)
                            sortedlist.Add(user.ToString());
                }

                foreach (object user in UserList.Items)
                {
                    if (UserData.ContainsKey(user.ToString()))
                        if (UserData[user.ToString()].Rank == 0)
                            sortedlist.Add(user.ToString());
                }

                UserList.Items.Clear();
                UserList.Items.AddRange(sortedlist.ToArray());
        }

        private void OptionsBtn_Click(object sender, EventArgs e)
        {
            ChatOptions_frm form = new ChatOptions_frm();

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ApplyNewSettings();
                Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
            }
        }

        private void ApplyNewSettings()
        {
            foreach (ChatWindow chat in ChatTabs.TabPages)
            {
                chat.ApplyNewSettings();
                SortUserList();
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
