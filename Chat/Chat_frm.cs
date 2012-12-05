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

        }

        private void Connect()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(Connect));
            }
            else
            {
                if (server.Connect("86.0.24.143", 7922))
                {
                    ChatWindow window = CurrentChatWindow();
                    window.WriteMessage(new ChatMessage(MessageType.System,window.Name,"Connected to DevPro chat server."));
                    server.SendPacket("LOGIN||" + Program.UserInfo.Username + "||" + Program.Config.Password);
                }
                else
                {
                    ChatWindow window = CurrentChatWindow();
                    window.WriteMessage(new ChatMessage(Enums.MessageType.System, window.Name, "Failed to connect to server."));
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
                UserList.Items.Clear();
                string[] users = userlist.Split(new string[] {";" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string user in users)
                {
                    string[] info = user.Split(',');
                    UserList.Items.Add(info[0]);
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
                        UserList.Items.Remove(user);
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
                    else
                    {
                        NewMessage(new ChatMessage(MessageType.System,CurrentChatWindow().Name,"Unknown command."));
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

        private void Chat_frm_Load(object sender, EventArgs e)
        {
            Thread chatserver = new Thread(Connect);
            chatserver.Name = "ChatServer";
            chatserver.IsBackground = true;
            chatserver.Start();
        }
    }
}
