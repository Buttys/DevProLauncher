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

namespace YGOPro_Launcher.Chat
{
    public partial class Chat_frm : Form
    {
        ChatClient server = new ChatClient();
        Dictionary<string,UserData> UserData = new Dictionary<string,UserData>();
        
        public Chat_frm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            ChatTabs.TabPages.Add(new ChatWindow("DevPro"));
            ChatInput.KeyPress += new KeyPressEventHandler(ChatInput_KeyPress);

            server.Message += new ChatClient.ServerMessage(NewMessage);
            server.UserList += new ChatClient.ServerResponse(CreateUserList);
            server.AddUser += new ChatClient.ServerResponse(AddUser);
            server.RemoveUser += new ChatClient.ServerResponse(RemoveUser);
            server.Login += new ChatClient.ServerResponse(LoginCheck);
            server.DuelRequest += new ChatClient.ServerResponse(HandleDuelRequest);
            server.Error += new ChatClient.ServerMessage(NewMessage);
            

            UserList.DrawItem += new DrawItemEventHandler(UserList_DrawItem);
            UserList.MouseUp += new MouseEventHandler(UserList_MouseUp);
            FriendList.MouseUp += new MouseEventHandler(FriendList_MouseUp);

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

        private void NewMessage(ChatMessage message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ChatMessage>(NewMessage), message);
            }
            else
            {
                ChatWindow window = GetChatWindow(message.Channel);
                if (window == null)
                {
                    ChatTabs.TabPages.Add(new ChatWindow(message.Channel));
                    window = GetChatWindow(message.Channel);
                    window.WriteMessage(message);
                }
                else
                {
                    window.WriteMessage(message);
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
                string[] users = userlist.Split(new string[] {";" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string user in users)
                {
                    string[] info = user.Split(',');
                    UserList.Items.Add(info[0]);
                    UserData.Add(info[0], new UserData() { Username = info[0], Rank = Int32.Parse(info[1]) });
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
                string[] info = userinfo.Split(',');
                foreach (object user in UserList.Items)
                {
                    if (info[0] == user.ToString())
                    {
                        return;
                    }
                }

                UserList.Items.Add(info[0]);
                if(!UserData.ContainsKey(info[0]))
                    UserData.Add(info[0], new UserData() { Username = info[0], Rank = Int32.Parse(info[1]) });
                NewMessage(new ChatMessage(MessageType.Join, Program.UserInfo, CurrentChatWindow().Name, info[0] + " has joined.", false));
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
                        NewMessage(new ChatMessage(MessageType.Leave, Program.UserInfo, CurrentChatWindow().Name, user + " has left.", false));
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
                                CreateChatWindow(parts[1]);
                                NewMessage(new ChatMessage(MessageType.Message,Program.UserInfo, CurrentChatWindow().Name, ChatInput.Text.Substring(parts[0].Length + parts[1].Length +1),false));
                            }
                            else
                                NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, parts[1] + " not found."));
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
                    server.SendPacket("MSG||" + CurrentChatWindow().Name + "||" + LauncherHelper.StringToBinary(ChatInput.Text));
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

        private void CreateChatWindow(string name)
        {
            foreach (TabPage window in ChatTabs.TabPages)
            {
                if (window.Name == name)
                {
                    ChatTabs.SelectedTab = window;
                    return;
                }
            }

            ChatTabs.TabPages.Add(new ChatWindow(name));
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
                ToolStripMenuItem mnukick = new ToolStripMenuItem("Kick");
                ToolStripMenuItem mnuban = new ToolStripMenuItem("Ban");

                mnukick.Click += new EventHandler(KickUser);
                mnuban.Click += new EventHandler(BanUser);
                mnuprofile.Click += new EventHandler(ViewProfile);
                mnuduel.Click += new EventHandler(RequestDuel);
                mnufriend.Click += new EventHandler(AddFriend);

                mnu.Items.AddRange(new ToolStripItem[] {mnuprofile, mnuduel, mnufriend });

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

                mnuremovefriend.Click += new EventHandler(RemoveFriend);

                mnu.Items.Add(mnuremovefriend);

                mnu.Show(FriendList, e.Location);
            }
        }

        private void RemoveFriend(object sender, EventArgs e)
        {
            NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, FriendList.SelectedItem.ToString() + " has been removed from your friendlist."));
            FriendList.Items.Remove(FriendList.SelectedItem);
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
        }

        private void ViewProfile(object sender, EventArgs e)
        {
            Profile_frm profile = new Profile_frm(UserList.SelectedItem.ToString());
            profile.ShowDialog();

        }

        private void RequestDuel(object sender, EventArgs e)
        {
            if (UserList.SelectedItem.ToString() == Program.UserInfo.Username)
                NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "You cannot duel request your self."));
            else
                server.SendPacket("REQUESTDUEL||" + UserList.SelectedItem.ToString());
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
                    if (MessageBox.Show(args[1] + " has challenged you to a ranked duel! Do you accept?", "Duel Request", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        NewMessage(new ChatMessage(MessageType.System, CurrentChatWindow().Name, "You accepted " + args[1] + " duel request."));
                        server.SendPacket("ACCEPTDUEL||" + args[1]);
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
                    g.FillRectangle(new SolidBrush(Color.White), e.Bounds);
                    g.DrawString(text, e.Font, (selected) ? Brushes.Blue : Brushes.Black,
                         UserList.GetItemRectangle(index).Location);
                    e.DrawFocusRectangle();
                    return;
                }

                g.FillRectangle(new SolidBrush(Color.White), e.Bounds);

                // Print text
                g.DrawString(text , e.Font, (selected) ? Brushes.Blue :  ChatMessage.GetUserColor(UserData[text].Rank),
                    UserList.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }
    }
}
