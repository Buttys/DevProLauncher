using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevProLauncher.Network.Data;
using DevProLauncher.Windows.Enums;
using DevProLauncher.Windows.Components;
using DevProLauncher.Windows.MessageBoxs;
using DevProLauncher.Helpers;
using DevProLauncher.Config;
using DevProLauncher.Network.Enums;
using ServiceStack.Text;

namespace DevProLauncher.Windows
{
    public sealed partial class ChatFrm : Form
    {
        private Dictionary<string,List<UserData>> m_channelData = new Dictionary<string, List<UserData>>(); 
        private readonly Dictionary<string, PmWindowFrm> m_pmWindows = new Dictionary<string, PmWindowFrm>();
        private List<UserData> m_filterUsers; 
        public bool Autoscroll = true;
        public bool Joinchannel = false;
        private bool m_onlineMode;
        private bool m_friendMode;
        private Timer m_searchReset;
        private bool m_autoJoined;


        public ChatFrm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            m_searchReset = new Timer {Interval = 1000};
            m_filterUsers = new List<UserData>();
            //chat packets
            Program.ChatServer.UserListUpdate += UpdateUserList;
            Program.ChatServer.UpdateUserInfo += UpdateUserInfo;
            Program.ChatServer.FriendList += CreateFriendList;
            Program.ChatServer.TeamList += CreateTeamList;
            Program.ChatServer.JoinChannel += ChannelAccept;
            Program.ChatServer.ChatMessage += WriteMessage;
            Program.ChatServer.DuelRequest += HandleDuelRequest;
            Program.ChatServer.TeamRequest += HandleTeamRequest;
            Program.ChatServer.DuelAccepted += StartDuelRequest;
            Program.ChatServer.DuelRefused += DuelRequestRefused;
            Program.ChatServer.ChannelUserList += UpdateOrAddChannelList;
            Program.ChatServer.AddUserToChannel += AddChannelUser;
            Program.ChatServer.RemoveUserFromChannel += RemoveChannelUser;

            //form events
            ChannelTabs.SelectedIndexChanged += UpdateChannelList;
            UserSearch.Enter += UserSearch_Enter;
            UserSearch.Leave += UserSearch_Leave;
            UserSearch.TextChanged += UserSearch_TextChanged;
            UserListTabs.SelectedIndexChanged += UserSearch_Reset;
            ChatInput.KeyPress += ChatInput_KeyPress;
            ChannelList.DoubleClick += List_DoubleClick;
            UserList.DoubleClick += List_DoubleClick;
            m_searchReset.Tick += SearchTick;
            ApplyOptionEvents();

            ChannelList.MouseUp += UserList_MouseUp;
            UserList.MouseUp += UserList_MouseUp;
            IgnoreList.MouseUp += IgnoreList_MouseUp;

            //custom form drawing
            ChannelList.DrawItem += UserList_DrawItem;
            UserList.DrawItem += UserList_DrawItem;

            ChatHelper.LoadChatTags();
            
            LoadIgnoreList();
            ApplyTranslations();
            ApplyChatSettings();

            WriteSystemMessage("Welcome to the DevPro chat system!");
            WriteSystemMessage("To join a channel please click the channel list button.");
        }

        public void LoadDefualtChannel()
        {
            if (!string.IsNullOrEmpty(Program.Config.DefaultChannel) && !m_autoJoined)
            {
                ChatWindow channel = GetChatWindow(Program.Config.DefaultChannel);

                if (channel == null)
                {
                    Program.ChatServer.SendPacket(DevServerPackets.JoinChannel, Program.Config.DefaultChannel);
                    m_autoJoined = true;
                }
            }
        }

        private void SearchTick(object sender,EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(SearchTick), sender, e);
                return;
            }

            if (userSearchBtn.Text == "1")
            {
                userSearchBtn.Enabled = true;
                adminSearchBtn.Enabled = true;
                teamSearchBtn.Enabled = true;
                friendSearchBtn.Enabled = true;

                userSearchBtn.Text = "Search";
                adminSearchBtn.Text = "Admins";
                teamSearchBtn.Text = "Team";
                friendSearchBtn.Text = "Friends";

                m_searchReset.Enabled = false;
            }
            else
            {
                int value = Int32.Parse(userSearchBtn.Text);
                userSearchBtn.Text = (value - 1).ToString();
                adminSearchBtn.Text = (value - 1).ToString();
                teamSearchBtn.Text = (value - 1).ToString();
                friendSearchBtn.Text = (value - 1).ToString();
            }
        }

        private void EnableSearchReset()
        {
            UserListTab.Focus();
            userSearchBtn.Enabled = false;
            adminSearchBtn.Enabled = false;
            teamSearchBtn.Enabled = false;
            friendSearchBtn.Enabled = false;
            userSearchBtn.Text = "5";
            adminSearchBtn.Text = "5";
            teamSearchBtn.Text = "5";
            friendSearchBtn.Text = "5";
            m_searchReset.Enabled = true;
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

            ChannelList.BackColor = Program.Config.ChatBGColor.ToColor();
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

        private void JoinChannel(string channel)
        {
            Program.ChatServer.SendPacket(DevServerPackets.JoinChannel, channel);
            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo, "System", "Join request for " + channel + " has been sent."));
        }

        private void LeaveChannel(string channel)
        {
            Program.ChatServer.SendPacket(DevServerPackets.LeaveChannel, channel);
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

            if (!string.IsNullOrEmpty(Program.UserInfo.team) && GetChatWindow(MessageType.Team.ToString()) == null)
            {
                ChannelTabs.TabPages.Add(new ChatWindow(MessageType.Team.ToString(), false));
            }

            WriteMessage(new ChatMessage(MessageType.Server, CommandType.None, Program.UserInfo, "Server", "Join request for " + channel + " accepted."));
            
            if (!Joinchannel)
            {
                Joinchannel = true;
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

            if(message.from != null && IgnoreList.Items.Contains(message.from.username))
            {
                return;
            }

            ChatWindow window;
            if ((MessageType)message.type == MessageType.Server || (MessageType)message.type == MessageType.System)
            {
                window = (ChatWindow)ChannelTabs.SelectedTab;
                if (window == null)
                {
                    window = new ChatWindow(((MessageType)message.type).ToString(), true) { IsSystemtab = true };
                    ChannelTabs.TabPages.Add(window);
                    window.WriteMessage(message, Autoscroll);
                }
                else window.WriteMessage(message, Autoscroll);
            }
            else if ((MessageType)message.type == MessageType.Join || (MessageType)message.type == MessageType.Leave || message.channel == null)
            {
                window = GetChatWindow(message.channel) ?? (ChatWindow)ChannelTabs.SelectedTab;
                if (window == null)
                {
                    window = new ChatWindow(message.type.ToString(CultureInfo.InvariantCulture), true) { IsSystemtab = true };
                    ChannelTabs.TabPages.Add(window);
                    window.WriteMessage(message, Autoscroll);
                }
                else window.WriteMessage(message, Autoscroll);
            }
            else if ((MessageType)message.type == MessageType.PrivateMessage && Program.Config.PmWindows)
            {
                if (m_pmWindows.ContainsKey(message.channel))
                {
                    m_pmWindows[message.channel].WriteMessage(message);
                }
                else
                {
                    m_pmWindows.Add(message.channel, new PmWindowFrm(message.channel, true));
                    m_pmWindows[message.channel].WriteMessage(message);
                    m_pmWindows[message.channel].Show();
                    m_pmWindows[message.channel].FormClosed += Chat_frm_FormClosed;
                }
            }
            else if ((MessageType)message.type == MessageType.Team)
            {
                window = GetChatWindow(((MessageType)message.type).ToString());
                if (window == null)
                {
                    window = new ChatWindow(((MessageType)message.type).ToString(), (MessageType)message.type == MessageType.PrivateMessage);
                    ChannelTabs.TabPages.Add(window);
                    window.WriteMessage(message, Autoscroll);
                }
                else window.WriteMessage(message, Autoscroll);
            }
            else
            {
                window = GetChatWindow(message.channel);
                if (window == null)
                {
                    window = new ChatWindow(message.channel, (MessageType)message.type == MessageType.PrivateMessage);
                    ChannelTabs.TabPages.Add(window);
                }

                window.WriteMessage(message, Autoscroll);
            }
        }

        public void WriteSystemMessage(string message)
        {
            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, message));
        }

        private ChatWindow GetChatWindow(string name)
        {
            return ChannelTabs.TabPages.Cast<ChatWindow>().FirstOrDefault(x => x.Name == name);
        }

        private void Chat_frm_FormClosed(object sender, EventArgs e)
        {
            m_pmWindows.Remove(((PmWindowFrm)sender).Name);
        }

        private void UserSearch_Enter(object sender, EventArgs e)
        {
            if (UserSearch.Text == "Search")
            {
                UserSearch.ForeColor = Program.Config.NormalTextColor.ToColor();
                UserSearch.Text = "";
            }
        }

        private void UserSearch_Leave(object sender, EventArgs e)
        {
            if (UserSearch.Text == "")
            {
                UserSearch.ForeColor = SystemColors.WindowFrame;
                UserSearch.Text = "Search";
            }
        }

        private void UserSearch_Reset(object sender, EventArgs e)
        {
            UserSearch.ForeColor = SystemColors.WindowFrame;
            UserSearch.Text = "Search";
        }

        private void UserSearch_TextChanged(object sender, EventArgs e)
        {
            if (UserListTabs.SelectedTab.Name == ChannelTab.Name)
            {
                ChatWindow window = (ChatWindow) ChannelTabs.SelectedTab;

                if (window != null)
                {
                    if (m_channelData.ContainsKey(window.Name))
                    {
                        IEnumerable<UserData> users = m_channelData[window.Name];
                        if (!string.IsNullOrEmpty(UserSearch.Text) && UserSearch.ForeColor != SystemColors.WindowFrame)
                        {
                            users = users.Where(user => user.username.ToLower().Contains(UserSearch.Text.ToLower()));
                        }

                        ChannelList.Items.Clear();
                        ChannelList.Items.AddRange(users.ToArray<object>());
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(UserSearch.Text) && UserSearch.ForeColor != SystemColors.WindowFrame)
                {
                    UserList.Items.Clear();
                    foreach (UserData user in m_filterUsers)
                    {
                        if (user.username.ToLower().Contains(UserSearch.Text.ToLower()))
                            UserList.Items.Add(user);
                    }
                }
                else
                {
                    UserList.Items.Clear();
                    UserList.Items.AddRange(m_filterUsers.ToArray());
                }
            }
        }

        private void UpdateUserInfo(UserData user)
        {
            Program.UserInfo = user;
            Program.MainForm.UpdateUsername();
            if (!string.IsNullOrEmpty(Program.UserInfo.team))
            {
                LoadTeamWindow();
                Program.MainForm.SetTeamProfile(true);
            }
            else
                Program.MainForm.SetTeamProfile(false);
        }

        private void UpdateChannelList(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object,EventArgs>(UpdateChannelList),sender,e);
                return;
            }
            ChatWindow window = (ChatWindow) ChannelTabs.SelectedTab;
            if (window != null)
            {
                if (m_channelData.ContainsKey(window.Name))
                {
                    List<UserData> users = m_channelData[window.Name];
                    ChannelList.Items.Clear();
                    ChannelList.Items.AddRange(users.ToArray());
                }
            }
            else
            {
                ChannelList.Items.Clear();
            }
        }

        private void UpdateOrAddChannelList(ChannelUsers users)
        {
            if (m_channelData.ContainsKey(users.Name))
                m_channelData[users.Name] = new List<UserData>(users.Users);
            else
                m_channelData.Add(users.Name,new List<UserData>(users.Users));
            UpdateChannelList(this,EventArgs.Empty);
        }

        private void AddOrRemoveChannelUser(UserData channelUser, bool remove)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<UserData, bool>(AddOrRemoveChannelUser), channelUser, remove);
                return;
            }

            UserData toRemove = null;
            foreach (UserData user in ChannelList.Items)
            {
                if (user.username == channelUser.username)
                    toRemove = user;
            }
            if(toRemove != null)
                ChannelList.Items.Remove(toRemove);
            if (!remove)
            {
                ChannelList.Items.Add(channelUser);
            }
        }

        private void AddChannelUser(ChannelUsers user)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ChannelUsers>(AddChannelUser), user);
                return;
            }

            if (m_channelData.ContainsKey(user.Name))
            {
                UserData founduser = null;
                foreach (UserData channeluser in m_channelData[user.Name])
                {
                    if (channeluser.username == user.Users[0].username)
                        founduser = channeluser;
                }
                if (founduser == null)
                    m_channelData[user.Name].Add(user.Users[0]);
                else
                {
                    m_channelData[user.Name].Remove(founduser);
                    m_channelData[user.Name].Add(user.Users[0]);
                }
            }

            ChatWindow window = (ChatWindow)ChannelTabs.SelectedTab;
            if (window != null)
            {
                if(user.Name == window.Name)
                    AddOrRemoveChannelUser(user.Users[0],false);
            }
        }

        private void RemoveChannelUser(ChannelUsers user)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ChannelUsers>(RemoveChannelUser), user);
                return;
            }
            if (m_channelData.ContainsKey(user.Name))
            {
                UserData founduser = null;
                foreach (UserData channeluser in m_channelData[user.Name])
                {
                    if (channeluser.username == user.Users[0].username)
                        founduser = channeluser;
                }
                if (founduser != null)
                    m_channelData[user.Name].Remove(founduser);
            }

            ChatWindow window = (ChatWindow)ChannelTabs.SelectedTab;
            if (window != null)
            {
                if (user.Name == window.Name)
                    AddOrRemoveChannelUser(user.Users[0], true);
            }
        }

        private void UpdateUserList(UserData[] userlist)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<UserData[]>(UpdateUserList), (object)userlist);
                return;
            }
            m_onlineMode = false;
            m_friendMode = false;
            UserList.Items.Clear();
            UserList.Items.AddRange(userlist);
            m_filterUsers.Clear();
            m_filterUsers.AddRange(userlist);
        }

        private void LoadTeamWindow()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(LoadTeamWindow));
                return;
            }

            if (GetChatWindow(MessageType.Team.ToString()) == null)
            {
                ChannelTabs.TabPages.Add(new ChatWindow(MessageType.Team.ToString(), false));
            }
        }

        public void CreateFriendList(UserData[] friends)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<UserData[]>(CreateFriendList), (object)friends);
                return;
            }

            m_friendMode = true;
            m_onlineMode = true;
            UserList.Items.Clear();
            UserList.Items.AddRange(friends);
            m_filterUsers.Clear();
            m_filterUsers.AddRange(friends);
        }

        public void CreateTeamList(UserData[] users)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<UserData[]>(CreateTeamList), (object)users);
                return;
            }

            m_onlineMode = true;
            UserList.Items.Clear();
            UserList.Items.AddRange(users);
            m_filterUsers.Clear();
            m_filterUsers.AddRange(users);
        }

        private void UserList_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox list = (ListBox) sender;
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index == -1)
                return;

            if (index < 0 && index >= list.Items.Count)
            {
                e.DrawFocusRectangle();
                return;
            }

            UserData user = (UserData)list.Items[index];
            Graphics g = e.Graphics;

            g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black) : new SolidBrush(Color.Blue)) : new SolidBrush(Program.Config.ChatBGColor.ToColor()), e.Bounds);

            if (!m_onlineMode)
            {
                if (user.rank > 0)
                {
                    // Print text
                    g.DrawString("[", e.Font,
                                 (selected)
                                     ? Brushes.White
                                     : (Program.Config.ColorBlindMode
                                            ? Brushes.Black
                                            : new SolidBrush(Program.Config.NormalTextColor.ToColor())),
                                 list.GetItemRectangle(index).Location);

                    if (user.rank == 1 || user.rank == 4)
                        g.DrawString("Dev", e.Font,
                                     (selected)
                                         ? Brushes.White
                                         : (Program.Config.ColorBlindMode
                                                ? Brushes.Black
                                                : ChatMessage.GetUserColor(user.rank)),
                                     new Point(
                                         list.GetItemRectangle(index).Location.X +
                                         (int) g.MeasureString("[", e.Font).Width - 1,
                                         list.GetItemRectangle(index).Location.Y));
                    else if (user.rank == 2 || user.rank == 3)
                        g.DrawString("Mod", e.Font,
                                     (selected)
                                         ? Brushes.White
                                         : (Program.Config.ColorBlindMode
                                                ? Brushes.Black
                                                : ChatMessage.GetUserColor(user.rank)),
                                     new Point(
                                         list.GetItemRectangle(index).Location.X +
                                         (int) g.MeasureString("[", e.Font).Width - 1,
                                         list.GetItemRectangle(index).Location.Y));
                    else if (user.rank == 99)
                        g.DrawString("Dev", e.Font,
                                     (selected)
                                         ? Brushes.White
                                         : (Program.Config.ColorBlindMode
                                                ? Brushes.Black
                                                : ChatMessage.GetUserColor(user.rank)),
                                     new Point(
                                         list.GetItemRectangle(index).Location.X +
                                         (int) g.MeasureString("[", e.Font).Width - 1,
                                         list.GetItemRectangle(index).Location.Y));
                    g.DrawString("]", e.Font,
                                 (selected)
                                     ? Brushes.White
                                     : (Program.Config.ColorBlindMode
                                            ? Brushes.Black
                                            : new SolidBrush(Program.Config.NormalTextColor.ToColor())),
                                 new Point(
                                     list.GetItemRectangle(index).Location.X +
                                     (int) g.MeasureString("[Dev", e.Font).Width,
                                     list.GetItemRectangle(index).Location.Y));
                    if (user.getUserColor().ToArgb() == Color.Black.ToArgb())
                    {
                        g.DrawString(user.username, e.Font,
                                     (selected)
                                         ? Brushes.White
                                         : (Program.Config.ColorBlindMode
                                                ? Brushes.Black
                                                : new SolidBrush(Program.Config.NormalTextColor.ToColor())),
                                     new Point(
                                         list.GetItemRectangle(index).Location.X +
                                         (int) g.MeasureString("[Dev]", e.Font).Width,
                                         list.GetItemRectangle(index).Location.Y));
                    }
                    else
                    {
                        g.DrawString(user.username, e.Font,
                                     (selected)
                                         ? Brushes.White
                                         : (Program.Config.ColorBlindMode
                                                ? Brushes.Black
                                                : new SolidBrush(user.getUserColor())),
                                     new Point(
                                         list.GetItemRectangle(index).Location.X +
                                         (int) g.MeasureString("[Dev]", e.Font).Width,
                                         list.GetItemRectangle(index).Location.Y));
                    }
                }
                else
                {
                    if (user.getUserColor().ToArgb() == Color.Black.ToArgb())
                    {
                        // Print text
                        g.DrawString(user.username, e.Font,
                                     (selected)
                                         ? Brushes.White
                                         : (Program.Config.ColorBlindMode
                                                ? Brushes.Black
                                                : new SolidBrush(Program.Config.NormalTextColor.ToColor())),
                                     list.GetItemRectangle(index).Location);
                    }
                    else
                    {
                        // Print text
                        g.DrawString(user.username, e.Font,
                                     (selected)
                                         ? Brushes.White
                                         : (Program.Config.ColorBlindMode
                                                ? Brushes.Black
                                                : new SolidBrush(user.getUserColor())),
                                     list.GetItemRectangle(index).Location);
                    }

                }
            }
            else
            {
                //// Print text
                g.DrawString((Program.Config.ColorBlindMode ? (user.Online ? user.username + " (Online)" : user.username + " (Offline)") : user.username), e.Font,
                    (selected) ? Brushes.White : (Program.Config.ColorBlindMode ? Brushes.Black : (user.Online ? Brushes.Green : Brushes.Red)),
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
                    Program.ChatServer.SendMessage(isTeam ? MessageType.Team : MessageType.Message, CommandType.Me, selectedTab.Name, ChatInput.Text.Substring(part.Length).Trim());
                    break;
                case "join":
                    JoinChannel(ChatInput.Text.Substring(part.Length).Trim());
                    break;
                case "leave":
                    if (selectedTab == null)
                    {
                        return false;
                    }
                    
                    if (selectedTab.IsPrivate)
                    {
                        ChannelTabs.TabPages.Remove(selectedTab);
                    }
                    else
                    {
                        LeaveChannel(selectedTab.Name);
                    }
                    
                    break;
                case "users":
                    //WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "There's " + m_userData.Count + " users online."));
                    break;
                case "ping":
                    Program.ChatServer.SendPacket(DevServerPackets.Ping);
                    break;
                case "autoscroll":
                    Autoscroll = !Autoscroll;
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, (Autoscroll ? "AutoScroll Enabled." : "AutoScroll Disabled.")));
                    break;
                case "help":
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Basic Commands --"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/me - Displays Username + Message"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/join - Join a other channel"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/leave - Leave the current channel"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/autoscroll - Enable/Disable autoscroll"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/ping - Ping the server"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/help - Displays this list your reading now"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/uptime - Displays how long the server has been online"));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/stats - Shows how many users are online, dueling, and how many duels"));


                    if (Program.UserInfo.rank != 0)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Donator Commands --"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "None at this moment"));
                    }

                    if(Program.UserInfo.rank == 1)
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, " -- Level 1 users are classed as helpers and don't need any extra commands"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/msg - Send a server message"));

                        

                    if (Program.UserInfo.rank > 1)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Level 2 Commands --"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/kick username reason - Kick a user"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/msg - Send a server message"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/mute - Prevents a user from talking"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/unmute - Allows a muted user to talk again"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/smsg - Sends a server message that displays on the bottom of the launcher"));
                    }

                    if (Program.UserInfo.rank > 2)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Level 3 Commands --"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/ban username time reason - Ban a user, time format has to be in hours also you must give a reason."));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/unban username - Unban a user"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/ip username - Get a users IP"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/banip ip - Ban a IP"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/unbanip ip - Unbans IP"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/getbanlist - Get ban list"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/roomowner roomname - Get the creator of a channel"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/killroom roomname - force a chat channel to close"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/getuid username - Gets the UID of a username"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/getuidaccounts uid - Gets the accnount names registered under the UID"));
                    }

                    if (Program.UserInfo.rank == 99)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Level 99 Commands --"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/op username level - Set a users level"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/addpoints amount of DevPoints - Give a user DevPoints"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/removepoints amount of DevPoints - Remove DevPoints from a user"));
                    }

                    if (Program.UserInfo.teamRank >= 0 && Program.UserInfo.team != string.Empty)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Team Commands --"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/leaveteam - leave the team"));
                    }

                    if (Program.UserInfo.teamRank >= 1)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "-- Team User Level 1 Commands --"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/teamadd username - add a user to the team"));
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "/teamremove username - remove a user from the team"));
                    }

                    if (Program.UserInfo.teamRank == 99)
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
                        Program.ChatServer.SendPacket(DevServerPackets.ChatCommand, JsonSerializer.SerializeToString(new PacketCommand { Command = cmd.ToUpper(), Data = ChatInput.Text.Substring(part.Length).Trim() }));
                    }
                    
                    break;
                case "admin":
                    //string admins = string.Join(", ", m_userData.Where(x => x.Value.rank > 0).Select(x => x.Key));
                    //WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "The following admins are online: " + admins + "."));
                    break;
                default:
                    Program.ChatServer.SendPacket(DevServerPackets.ChatCommand, JsonSerializer.SerializeToString(new PacketCommand { Command = cmd.ToUpper(), Data = ChatInput.Text.Substring(part.Length).Trim() }));
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
                
                if (selectedTab.IsSystemtab)
                {
                    ChatInput.Clear();
                    return;
                }

                if (selectedTab.IsPrivate)
                {
                    WriteMessage(new ChatMessage(MessageType.PrivateMessage, CommandType.None, Program.UserInfo, selectedTab.Name, ChatInput.Text));
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
                                   .Where(x => x.IsPrivate)
                                   .ToList()
                                   .ForEach(window => ChannelTabs.TabPages.Remove(window));
                    }
                    else
                    {
                        m_pmWindows.Values.ToList().ForEach(x => x.Close());
                    }
                    
                    break;
            }

            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
        }

        private void UserList_MouseUp(object sender, MouseEventArgs e)
        {
            ListBox list = (ListBox) sender;

            if (e.Button == MouseButtons.Right)
            {
                int index = list.IndexFromPoint(e.Location);

                if (index == -1)
                {
                    return;
                }

                list.SelectedIndex = index;

                if (list.SelectedItem == null)
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
                var mnuremovefriend = new ToolStripMenuItem(Program.LanguageManager.Translation.chatRemoveFriend);
                var mnuremoveteam = new ToolStripMenuItem("Remove from Team");

                mnukick.Click += KickUser;
                mnuban.Click += BanUser;
                mnuprofile.Click += ViewProfile;
                mnuduel.Click += RequestDuel;
                mnufriend.Click += AddFriend;
                mnuignore.Click += IgnoreUser;
                mnuremovefriend.Click += RemoveFriend;
                mnuremoveteam.Click += RemoveFromTeam;

                if (!m_onlineMode)
                {
                    mnu.Items.AddRange(new ToolStripItem[] {mnuprofile, mnuduel, mnufriend, mnuignore});               
                    
                    if (Program.UserInfo.rank > 0)
                        mnu.Items.Add(mnukick);
                    if (Program.UserInfo.rank > 1)
                        mnu.Items.Add(mnuban);
                }
                else
                {
                    UserData user = (UserData) list.SelectedItem;
                    mnu.Items.Add(mnuprofile);
                    if (user.Online)
                        mnu.Items.Add(mnuduel);
                    if (m_friendMode)
                        mnu.Items.Add(mnuremovefriend);
                    else
                    {
                        if (Program.UserInfo.teamRank > 0)
                            mnu.Items.Add(mnuremoveteam);
                    }
                }

                mnu.Show(list, e.Location);
            }
        }

        private void BanUser(object sender, EventArgs e)
        {
            ListBox list = UserListTabs.SelectedTab.Name == ChannelTab.Name ? ChannelList : UserList;
            if (list.SelectedItem != null && MessageBox.Show("Are you sure you want to ban " + ((UserData)list.SelectedItem).username, "Ban User", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.ChatServer.SendPacket(DevServerPackets.ChatCommand, JsonSerializer.SerializeToString(
                    new PacketCommand { Command = "BAN", Data = ((UserData)list.SelectedItem).username }));
            }
        }

        private void KickUser(object sender, EventArgs e)
        {
            ListBox list = UserListTabs.SelectedTab.Name == ChannelTab.Name ? ChannelList : UserList;
            if (list.SelectedItem == null)
            {
                return;
            }
            Program.ChatServer.SendPacket(DevServerPackets.ChatCommand, JsonSerializer.SerializeToString(
                new PacketCommand { Command = "KICK", Data = ((UserData)list.SelectedItem).username }));
        }

        private void AddFriend(object sender, EventArgs e)
        {
            ListBox list = UserListTabs.SelectedTab.Name == ChannelTab.Name ? ChannelList : UserList;
            if (list.SelectedItem == null)
            {
                return;
            }

            if (((UserData)list.SelectedItem).username == Program.UserInfo.username)
            {
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You cannot be your own friend."));
                return;
            }

            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, ((UserData)list.SelectedItem).username + " has been added to your friend list."));
            Program.ChatServer.SendPacket(DevServerPackets.AddFriend, ((UserData)list.SelectedItem).username);
        }

        private void IgnoreUser(object sender, EventArgs e)
        {
            ListBox list = UserListTabs.SelectedTab.Name == ChannelTab.Name ? ChannelList : UserList;
            if (((UserData)list.SelectedItem).username == Program.UserInfo.username)
            {
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You cannot ignore yourself."));
                return;
            }

            if (IgnoreList.Items.Contains(((UserData)list.SelectedItem).username))
            {
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, ((UserData)list.SelectedItem).username + " is already on your ignore list."));
                return;
            }

            IgnoreList.Items.Add(((UserData)list.SelectedItem).username);
            SaveIgnoreList();
            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, ((UserData)list.SelectedItem).username + " has been added to your ignore list."));
        }

        private void RemoveFromTeam(object sender, EventArgs e)
        {
            if (UserList.SelectedIndex == -1)
            {
                return;
            }

            if (MessageBox.Show("Are you sure?", "Remove User", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.ChatServer.SendPacket(DevServerPackets.ChatCommand,
                    JsonSerializer.SerializeToString(new PacketCommand { Command = "TEAMREMOVE", Data = ((UserData)UserList.SelectedItem).username }));
            }

        }

        private void RemoveFriend(object sender, EventArgs e)
        {
            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, ((UserData)UserList.SelectedItem).username + " has been removed from your friendlist."));
            Program.ChatServer.SendPacket(DevServerPackets.RemoveFriend, ((UserData)UserList.SelectedItem).username);
            UserList.Items.Remove(UserList.SelectedItem);
        }

        private void ViewProfile(object sender, EventArgs e)
        {
            ListBox list = (UserListTabs.SelectedTab.Name == ChannelTab.Name ? ChannelList : UserList);
            if (list.SelectedItem == null)
                return;

            var profile = new ProfileFrm(list.SelectedItem is string ? list.SelectedItem.ToString():((UserData)list.SelectedItem).username);
            profile.ShowDialog();
        }

        private void RequestDuel(object sender, EventArgs e)
        {
            ListBox list = (UserListTabs.SelectedTab.Name == ChannelTab.Name ? ChannelList : UserList);
            if (list.SelectedItem == null)
            {
                return;
            }

            if ((list.SelectedItem is string ? list.SelectedItem.ToString() : ((UserData)list.SelectedItem).username) == Program.UserInfo.username)
            {
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You cannot duel request your self."));
            }
            else
            {
                var form = new Host();
                ServerInfo server = Program.MainForm.GameWindow.GetServer();
                if (server == null)
                {
                    MessageBox.Show("No Server Available.");
                    return;
                }

                Program.ChatServer.SendPacket(DevServerPackets.RequestDuel,
                    JsonSerializer.SerializeToString(
                    new DuelRequest
                        {
                            username = list.SelectedItem is string ? list.SelectedItem.ToString() : ((UserData)list.SelectedItem).username, 
                            duelformatstring = form.GenerateGameString(false),
                            server = server.serverName
                        }));
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "Duel request sent to " + (list.SelectedItem is string ? list.SelectedItem.ToString() : ((UserData)list.SelectedItem).username) + "."));
            }
        }

        private void HandleDuelRequest(DuelRequest command)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<DuelRequest>(HandleDuelRequest), command);
                return;
            }

            if (Program.Config.RefuseDuelRequests)
            {
                Program.ChatServer.SendPacket(DevServerPackets.RefuseDuel);
                return;
            }

            RoomInfos info = RoomInfos.FromName(command.duelformatstring);
            var request = new DuelRequestFrm(
                command.username
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
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You accepted " + command.username + " duel request."));
                Program.ChatServer.SendPacket(DevServerPackets.AcceptDuel);
            }
            else
            {
                WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "You refused " + command.username + " duel request."));
                Program.ChatServer.SendPacket(DevServerPackets.RefuseDuel);
            }
        }

        public void StartDuelRequest(DuelRequest request)
        {
            ServerInfo server = null;
            if (Program.ServerList.ContainsKey(request.server))
                server = Program.ServerList[request.server];
            if (server != null)
            {
                LauncherHelper.GenerateConfig(server, request.duelformatstring);
                LauncherHelper.RunGame("-j");
            }
        }

        public void DuelRequestRefused()
        {
            WriteMessage(new ChatMessage(MessageType.System, CommandType.None, null, "Your duel request has been refused."));
        }

        private void HandleTeamRequest(PacketCommand command)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<PacketCommand>(HandleTeamRequest), command);
                return;
            }
            switch(command.Command)
            {
                case "JOIN":

                    if (Program.Config.RefuseTeamInvites)
                    {
                        Program.ChatServer.SendPacket(DevServerPackets.TeamCommand,
                            JsonSerializer.SerializeToString(new PacketCommand { Command = "AUTOREFUSE"}));
                        return;
                    }

                    if (MessageBox.Show(command.Data + " has invited you to join a team.", "Team Request", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo.username, "You have accepted the team invite."));
                        Program.ChatServer.SendPacket(DevServerPackets.TeamCommand,
                            JsonSerializer.SerializeToString(new PacketCommand { Command = "ACCEPT"}));
                    }
                    else
                    {
                        WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo.username, "You have refused the team invite."));
                        Program.ChatServer.SendPacket(DevServerPackets.TeamCommand,
                            JsonSerializer.SerializeToString(new PacketCommand { Command = "REFUSE"}));
                    }
                    break;
                case "LEAVE":
                    Program.UserInfo.team = string.Empty;
                    Program.UserInfo.teamRank = 0;
                    ChannelTabs.TabPages.Remove(GetChatWindow(MessageType.Team.ToString()));
                    Program.MainForm.SetTeamProfile(false);
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo.username, "You have left the team."));
                    break;
                case "REMOVED":
                    Program.UserInfo.team = string.Empty;
                    Program.UserInfo.teamRank = 0;
                    ChannelTabs.TabPages.Remove(GetChatWindow(MessageType.Team.ToString()));
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Program.UserInfo.username, "You have been removed from the team."));
                    Program.MainForm.SetTeamProfile(false);
                    break;
                case "DISBAND":
                    if (Program.UserInfo.team == command.Data)
                    {
                        Program.UserInfo.team = string.Empty;
                        Program.UserInfo.teamRank = 0;
                        ChannelTabs.TabPages.Remove(GetChatWindow(MessageType.Team.ToString()));
                        Program.MainForm.SetTeamProfile(false);
                    }

                    break;
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
            var ignoredusers = new string[IgnoreList.Items.Count];
// ReSharper disable CoVariantArrayConversion
            IgnoreList.Items.CopyTo(ignoredusers, 0);
// ReSharper restore CoVariantArrayConversion
            string ignorestring = string.Join(",", ignoredusers);
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

            foreach (var form in m_pmWindows.Values)
            {
                form.ApplyNewSettings();
            }

            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
        }

        private void List_DoubleClick(object sender, EventArgs e)
        {
            ListBox list = UserListTabs.SelectedTab.Name == ChannelTab.Name ? ChannelList:UserList;

            if (list.SelectedIndex == -1)
                return;

            string user = list.Name == ChannelList.Name || list.Name == UserList.Name ? 
                ((UserData)list.SelectedItem).username : list.SelectedItem.ToString();

            if (Program.Config.PmWindows)
            {
                if (!m_pmWindows.ContainsKey(user))
                {
                    m_pmWindows.Add(user, new PmWindowFrm(user, true));
                    m_pmWindows[user].Show();
                    m_pmWindows[user].FormClosed += Chat_frm_FormClosed;
                }
                else
                {
                    m_pmWindows[user].BringToFront();
                }
            }
            else
            {
                if (GetChatWindow(user) == null)
                {
                    ChannelTabs.TabPages.Add(new ChatWindow(user, true));
                    ChannelTabs.SelectedTab = GetChatWindow(user);
                }
                else
                {
                    ChannelTabs.SelectedTab = GetChatWindow(user);
                }
            }
        }

        private void ChannelListBtn_Click(object sender, EventArgs e)
        {
            var channellist = new ChannelListFrm();
            channellist.ShowDialog();
        }

        private void LeaveBtn_Click(object sender, EventArgs e)
        {
            if (ChannelTabs.SelectedIndex != -1)
            {
                var selectedTab = (ChatWindow)ChannelTabs.SelectedTab;
                if (selectedTab.IsPrivate)
                {
                    ChannelTabs.TabPages.Remove(selectedTab);
                }
                else
                {
                    LeaveChannel(selectedTab.Text);
                }
            }
        }

        private void userSearchBtn_Click(object sender, EventArgs e)
        {
            string searchinfo = UserSearch.ForeColor == SystemColors.WindowFrame ? string.Empty : UserSearch.Text;
            Program.ChatServer.SendPacket(DevServerPackets.UserList,
                JsonSerializer.SerializeToString(new PacketCommand(){ Command = "USERS", Data = searchinfo}));
            EnableSearchReset();
            
        }

        private void adminSearchBtn_Click(object sender, EventArgs e)
        {
            string searchinfo = UserSearch.ForeColor == SystemColors.WindowFrame ? string.Empty : UserSearch.Text;
            Program.ChatServer.SendPacket(DevServerPackets.UserList,
                JsonSerializer.SerializeToString(new PacketCommand() { Command = "ADMIN", Data = searchinfo }));
            EnableSearchReset();
            
        }

        private void teamSearchBtn_Click(object sender, EventArgs e)
        {
            Program.ChatServer.SendPacket(DevServerPackets.UserList,
                JsonSerializer.SerializeToString(new PacketCommand() { Command = "TEAM", Data = string.Empty }));
            EnableSearchReset();
        }

        private void friendSearchBtn_Click(object sender, EventArgs e)
        {
            Program.ChatServer.SendPacket(DevServerPackets.UserList,
                JsonSerializer.SerializeToString(new PacketCommand() { Command = "FRIENDS", Data = string.Empty }));
            EnableSearchReset();
        }
    }
}
