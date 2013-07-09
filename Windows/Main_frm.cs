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
        public GameListFrm GameWindow;

        readonly LoginFrm _loginWindow;
        readonly ChatFrm _chatWindow;
        readonly SupportFrm _devpointWindow;
        readonly FileManagerFrm _filemanagerWindow;
        readonly CustomizeFrm _customizerWindow;

        public MainFrm()
        {
            InitializeComponent();

            var version = Program.Version.ToCharArray();
            Text = "DevPro" + " v" + version[0] + "." + version[1] + "." + version[2] + " r" + Program.Version[3];

            LauncherHelper.LoadBanlist();

            var loginTab = new TabPage("Login");
            _loginWindow = new LoginFrm();
            loginTab.Controls.Add(_loginWindow);
            mainTabs.TabPages.Add(loginTab);

            _chatWindow = new ChatFrm();
            GameWindow = new GameListFrm("DevPro");
            _devpointWindow = new SupportFrm();
            _filemanagerWindow = new FileManagerFrm();
            _customizerWindow = new CustomizeFrm();
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
                _loginWindow.Connected();
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
            gamelistTab.Controls.Add(GameWindow);
            mainTabs.TabPages.Add(gamelistTab);

            var chatTab = new TabPage("Chat (Beta v4)");
            chatTab.Controls.Add(_chatWindow);
            mainTabs.TabPages.Add(chatTab);

            var filemanagerTab = new TabPage("File Manager");
            filemanagerTab.Controls.Add(_filemanagerWindow);
            mainTabs.TabPages.Add(filemanagerTab);

            var cuztomizerTab = new TabPage("Customizer");
            cuztomizerTab.Controls.Add(_customizerWindow);
            mainTabs.TabPages.Add(cuztomizerTab);

            var devpointTab = new TabPage("Support DevPro");
            devpointTab.Controls.Add(_devpointWindow);
            mainTabs.TabPages.Add(devpointTab);
                
            ConnectionCheck.Enabled = true;
            ConnectionCheck.Tick += CheckConnection;
            
            UpdateUsername();
            if (Program.DuelServer.Connect(Program.Config.ServerAddress, Program.Config.GamePort))
            {
                Program.DuelServer.SendPacket(DevServerPackets.Login,JsonSerializer.SerializeToString(
                    new LoginRequest { Username = Program.UserInfo.username, Password = Program.Config.Password, UID = LauncherHelper.GetUID() }));
            }
            Program.ChatServer.SendPacket(DevServerPackets.UserList);
            Program.ChatServer.SendPacket(DevServerPackets.FriendList);
            Program.ChatServer.SendPacket(DevServerPackets.DevPoints);

        }

        public void UpdateUsername()
        {
            Text = "DevPro" + " v" + Program.Version[0] + "." + Program.Version[1] + "." + Program.Version[2] + " r" + Program.Version[3] + " - " + Program.UserInfo.username;
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
