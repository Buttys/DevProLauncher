using System;
using System.Windows.Forms;
using DevProLauncher.Config;
using DevProLauncher.Network.Enums;
using DevProLauncher.Helpers;
using System.Diagnostics;
using DevProLauncher.Windows.MessageBoxs;

namespace DevProLauncher.Windows
{
    public partial class MainFrm : Form
    {
        public HubGameList_frm GameWindow;
        LoginFrm m_loginWindow;
        ChatFrm m_chatWindow;
        SupportFrm m_devpointWindow;
        FileManagerFrm m_filemanagerWindow;
        CustomizeFrm m_customizerWindow;
        Browser_frm m_wcsBrowser;
        Browser_frm m_faqBrowser;


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
            
            m_wcsBrowser = new Browser_frm();
            m_wcsBrowser.FormBorderStyle = FormBorderStyle.None;
            m_chatWindow = new ChatFrm();
            GameWindow = new HubGameList_frm();
            m_devpointWindow = new SupportFrm();
            m_filemanagerWindow = new FileManagerFrm();
            m_customizerWindow = new CustomizeFrm();
            m_faqBrowser = new Browser_frm();
            m_faqBrowser.FormBorderStyle = FormBorderStyle.None;

            Program.ChatServer.ServerMessage += ServerMessage;

            mainTabs.SelectedIndexChanged += TabChange;

            ApplyTranslation();

            //if (!String.IsNullOrEmpty(Properties.Settings.Default.DropBoxUserToken) && !String.IsNullOrEmpty(Properties.Settings.Default.DropBoxUserSecret))
            //{
            //    DropNetClient dbctrl = new DropNetClient(Program.Config.AppKey, Program.Config.AppSecret);

            //    dbctrl.UserLogin = new DropNet.Models.UserLogin();

            //    dbctrl.UserLogin.Token = Properties.Settings.Default.DropBoxUserToken;
            //    dbctrl.UserLogin.Secret = Properties.Settings.Default.DropBoxUserSecret;

            //    DropBoxController.filesyncAsync();
            //}

        }

        public void ApplyTranslation()
        {
            LanguageInfo info = Program.LanguageManager.Translation;

            OptionsBtn.Text = info.chatBtnoptions;
            ProfileBtn.Text = info.MainProfileBtn;
            DeckBtn.Text = info.MainDeckBtn;
            ReplaysBtn.Text = info.MainReplaysBtn;
            OfflineBtn.Text = info.MainOfflineBtn;
            //DBSyncBtn.Text = info.MainSyncBtn;
            siteBtn.Text = info.MainSiteBtn;
            MessageLabel.Text = info.MainServerMessage;
        }

        private void ServerMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(ServerMessage), message);
                return;
            }
            MessageLabel.Text = message;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
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

            var chatTab = new TabPage("Chat (Beta v4.2)");
            chatTab.Controls.Add(m_chatWindow);
            mainTabs.TabPages.Add(chatTab);

            var wcsTab = new TabPage("Downloads");
            wcsTab.Controls.Add(m_wcsBrowser);
            mainTabs.TabPages.Add(wcsTab);

            var filemanagerTab = new TabPage("File Manager");
            filemanagerTab.Controls.Add(m_filemanagerWindow);
            mainTabs.TabPages.Add(filemanagerTab);

            var cuztomizerTab = new TabPage("Customizer");
            cuztomizerTab.Controls.Add(m_customizerWindow);
            mainTabs.TabPages.Add(cuztomizerTab);

            var devpointTab = new TabPage("Support DevPro");
            devpointTab.Controls.Add(m_devpointWindow);
            mainTabs.TabPages.Add(devpointTab);

            var faqTab = new TabPage("FAQ");
            faqTab.Controls.Add(m_faqBrowser);
            mainTabs.TabPages.Add(faqTab);
                
            ConnectionCheck.Enabled = true;
            ConnectionCheck.Tick += CheckConnection;
            
            UpdateUsername();

            ProfileBtn.Enabled = true;
            if (!string.IsNullOrEmpty(Program.UserInfo.team))
                TeamProfileBtn.Enabled = true;

            Program.ChatServer.SendPacket(DevServerPackets.DevPoints);

        }

        public void UpdateUsername()
        {
            Text = "DevPro" + " v" + Program.Version[0] + "." + Program.Version[1] + "." + Program.Version[2] + " r" + Program.Version[3] + " - " + Program.UserInfo.username;
        }

        public void ReLoadLanguage()
        {
            GameWindow.ApplyTranslation();
            m_filemanagerWindow.ApplyTranslations();
            m_customizerWindow.ApplyTranslation();
            m_chatWindow.ApplyTranslations();
        }

        private void CheckConnection(object sender, EventArgs e)
        {
            if (!Program.ChatServer.Connected())
            {
                var connectionCheck = (Timer)sender;
                Hide();
                connectionCheck.Enabled = false;
                if (MessageBox.Show(!string.IsNullOrEmpty(Program.ChatServer.ServerKickBanMessage) ? Program.ChatServer.ServerKickBanMessage: "Disconnected from server.", "Server", MessageBoxButtons.OK) == DialogResult.OK)
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

        private void OfflineBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.RunGame("");
        }

        private void siteBtn_Click(object sender, EventArgs e)
        {
            Process.Start("http://devpro.org/blog");
        }

        private void DeckBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.GenerateConfig();
            LauncherHelper.RunGame("-d");
        }
        private void ReplaysBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.GenerateConfig();
            LauncherHelper.RunGame("-r");
        }

        private void ProfileBtn_Click(object sender, EventArgs e)
        {
            var profile = new ProfileFrm();
            profile.ShowDialog();
        }

        private void OptionsBtn_Click(object sender, EventArgs e)
        {
            var settings = new Settings();
            settings.ShowDialog();

        }

        private void TabChange(object sender, EventArgs e)
        {
            if (mainTabs.SelectedIndex == 2)
                m_wcsBrowser.Navigate("http://ygopro.de/launcher/events.php", false);
            else if (mainTabs.SelectedIndex == mainTabs.TabPages.Count - 1)
                m_faqBrowser.Navigate("http://ygopro.de/en/faq/", false);
            else if (mainTabs.SelectedIndex == 1)
                m_chatWindow.LoadDefualtChannel();
        }

        private void TeamProfileBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Program.UserInfo.team))
            {
                MessageBox.Show("You are not in a team.", "No u", MessageBoxButtons.OK);
                return;
            }

            var form = new TeamProfileFrm(Program.UserInfo.team);
            form.Show();
        }
        public void SetTeamProfile(bool value)
        {
            TeamProfileBtn.Enabled = value;
        }
    }
}
