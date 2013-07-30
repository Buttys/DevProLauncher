using System;
using System.Windows.Forms;
using System.Threading;
using DevProLauncher.Network.Enums;
using DevProLauncher.Helpers;
using System.Diagnostics;
using ServiceStack.Text;
using DevProLauncher.Network.Data;

namespace DevProLauncher.Windows
{
    public partial class MainFrm : Form
    {
        readonly GameListFrm m_gameWindow;
        readonly LoginFrm m_loginWindow;
        readonly ChatFrm m_chatWindow;
        readonly SupportFrm m_devpointWindow;
        readonly FileManagerFrm m_filemanagerWindow;
        readonly CustomizeFrm m_customizerWindow;

        public MainFrm()
        {
            InitializeComponent();

            var version = Program.Version.ToCharArray();
            Text = "DevPro" + " v" + version[0] + "." + version[1] + "." + version[2] + " r" + Program.Version[3];

            LauncherHelper.LoadBanlist();

            var loginTab = new TabPage("Login");
            m_loginWindow = new LoginFrm();
            loginTab.Controls.Add(m_loginWindow);
            mainTabs.TabPages.Add(loginTab);

            m_chatWindow = new ChatFrm();
            m_gameWindow = new GameListFrm("DevPro");
            m_devpointWindow = new SupportFrm();
            m_filemanagerWindow = new FileManagerFrm();
            m_customizerWindow = new CustomizeFrm();
            LauncherHelper.CardManager.Init();

            var connectThread = new Thread(Loaded);
            connectThread.Start();
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void Loaded()
        {
            if (!Program.ChatServer.Connect(Program.Config.ServerAddress, Program.Config.ChatPort))
                MessageBox.Show(Program.LanguageManager.Translation.pMsbErrorToServer);
            else
                m_loginWindow.Connected();
        }
        public void Login()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(Login));
                return;
            }

            mainTabs.TabPages.Remove(mainTabs.SelectedTab);

            var gamelistTab = new TabPage("GameList");
            gamelistTab.Controls.Add(m_gameWindow);
            mainTabs.TabPages.Add(gamelistTab);

            var chatTab = new TabPage("Chat (Beta v4)");
            chatTab.Controls.Add(m_chatWindow);
            mainTabs.TabPages.Add(chatTab);

            var filemanagerTab = new TabPage("File Manager");
            filemanagerTab.Controls.Add(m_filemanagerWindow);
            mainTabs.TabPages.Add(filemanagerTab);

            var cuztomizerTab = new TabPage("Customizer");
            cuztomizerTab.Controls.Add(m_customizerWindow);
            mainTabs.TabPages.Add(cuztomizerTab);

            var devpointTab = new TabPage("Support DevPro");
            devpointTab.Controls.Add(m_devpointWindow);
            mainTabs.TabPages.Add(devpointTab);
                
            ConnectionCheck.Enabled = true;
            ConnectionCheck.Tick += CheckConnection;
            
            UpdateUsername();

            Program.ChatServer.SendPacket(DevServerPackets.UserList);
            Program.ChatServer.SendPacket(DevServerPackets.FriendList);
            Program.ChatServer.SendPacket(DevServerPackets.DevPoints);
            Program.ChatServer.SendPacket(DevServerPackets.GameList);

        }

        public void UpdateUsername()
        {
            Text = "DevPro" + " v" + Program.Version[0] + "." + Program.Version[1] + "." + Program.Version[2] + " r" + Program.Version[3] + " - " + Program.UserInfo.username;
        }

        public void ReLoadLanguage()
        {
            m_gameWindow.ApplyTranslation();
            m_filemanagerWindow.ApplyTranslations();
            m_customizerWindow.ApplyTranslation();
            m_chatWindow.ApplyTranslations();
        }

        private void CheckConnection(object sender, EventArgs e)
        {
            if (!Program.ChatServer.Connected())
            {
                var connectionCheck = (System.Windows.Forms.Timer)sender;
                Hide();
                connectionCheck.Enabled = false;
                if (MessageBox.Show("Disconnected from server.", "Server", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    var process = new Process();
                    var startInfos = new ProcessStartInfo(Application.ExecutablePath, "-r");
                    process.StartInfo = startInfos;
                    process.Start();
                    Application.Exit();
                }
                else
                {
                    Application.Exit();
                }

            }
        }
    }
}
