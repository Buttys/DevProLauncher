using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using YGOPro_Launcher.Chat;
using System.Threading;

namespace YGOPro_Launcher
{
    public partial class Main_frm : Form
    {
       

        public Main_frm()
        {
            InitializeComponent();

            LauncherHelper.CardManager = new CardDatabase.CardsManager();
            LauncherHelper.CardManager.Init();

            char[] version = Program.Version.ToCharArray();
            this.Text = Program.LanguageManager.Translation.MainFormTitle + " v" + version[0] + "." + version[1] + "." + version[2] + " - " + Program.UserInfo.Username;

            TabPage FileManager = new TabPage(){ Name = "File Manager", Text = Program.LanguageManager.Translation.MainFileManagerTab};
            TabControl FileControl = new TabControl();
            FileControl.Dock = DockStyle.Fill;

            TabPage decktab = new TabPage() {Name = "Decks",Text =Program.LanguageManager.Translation.MainFileDeckTab};
            decktab.Controls.Add(new FileManager_frm("Decks", Program.Config.LauncherDir + "deck/", ".ydk"));
            
            TabPage replaytab = new TabPage() { Name = "Replays", Text = Program.LanguageManager.Translation.MainFileReplayTab };
            replaytab.Controls.Add(new FileManager_frm("Replays", Program.Config.LauncherDir + "replay/", ".yrp"));
            FileControl.TabPages.AddRange(new TabPage[] { decktab, replaytab });

            FileManager.Controls.Add(FileControl);

            TabPage ServerTab = new TabPage() { Text = Program.Config.ServerName, Name = Program.Config.ServerName };
            ServerTab.Controls.Add(new NewServerInterface_frm(Program.Config.ServerName));

            TabPage CustomizeTab = new TabPage() { Name = "Customize", Text = Program.LanguageManager.Translation.MainCustomizeTab};
            CustomizeTab.Controls.Add(new Customize_frm());

            TabPage AboutTab = new TabPage() { Name = "About", Text = Program.LanguageManager.Translation.MainAboutTab};
            AboutTab.Controls.Add(new About_frm());                    
            
            TabPage ChatTab = new TabPage() { Text = "Chat (Beta v2)", Name = Program.LanguageManager.Translation.MainChatTab};
            ChatTab.Controls.Add(new NewChat_frm());

            TabPage RankingTab = new TabPage() { Text = Program.LanguageManager.Translation.MainRankingTab, Name = "Ranking" };
            RankingTab.Controls.Add(new WebBrowserTab_frm());

            TabPage YoutubeTab = new TabPage() { Text = Program.LanguageManager.Translation.MainYoutubeTab, Name = "Youtube" };
            YoutubeTab.Controls.Add(new WebBrowserTab_frm());

            TabPage TornyTab = new TabPage() { Text = Program.LanguageManager.Translation.MainTornyTab, Name = "Tournament Room" };
            TornyTab.Controls.Add(new WebBrowserTab_frm());

            TabPage sponserTab = new TabPage() { Text = "Sponser", Name = "Sponser" };
            sponserTab.Controls.Add(new WebBrowserTab_frm());

            ServerControl.TabPages.AddRange(new TabPage[] { ServerTab,
            ChatTab, TornyTab,
            FileManager, CustomizeTab, AboutTab });

            if (Program.Config.Language == "German")
                ServerControl.TabPages.Add(sponserTab);

            Program.ServerConnection.ServerMessage += new NetClient.ServerResponse(ServerMessage);
            ConnectionCheck.Tick += new EventHandler(CheckConnection);
            ServerControl.SelectedIndexChanged += new EventHandler(NavigateOnClick);

            LauncherHelper.LoadBanlist();

        }

        private void CheckConnection(object sender, EventArgs e)
        {
            if (!Program.ServerConnection.CheckConnection())
            {
                this.Hide();
                ConnectionCheck.Enabled = false;
                if (MessageBox.Show("Disconnected from server.","Server",MessageBoxButtons.OK) == DialogResult.OK)
                {
                    Process process = new Process();
                    ProcessStartInfo startInfos = new ProcessStartInfo(Application.ExecutablePath, "-r");
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

        private void ServerMessage(string message)
        {
            MessageBox.Show(message, "Server Message", MessageBoxButtons.OK);
        }

        private TabPage CreateBrowserWindow(string name,string tabname)
        {
            TabPage page = new TabPage() { Name = name, Text = tabname};
            WebBrowser browser = new WebBrowser();
            browser.ScriptErrorsSuppressed = true;
            page.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
            return page;
        }

        private void Main_frm_Load(object sender, EventArgs e)
        {
            foreach (TabPage tab in ServerControl.TabPages)
            {
                    foreach (Control control in tab.Controls)
                    {
                        if (control is NewServerInterface_frm)
                        {
                            NewServerInterface_frm form = (NewServerInterface_frm)control;
                            form.RequestUserWLD();
                            Program.ServerConnection.SendPacket("GETROOMS");
                        }
                        if (control is NewChat_frm)
                        {
                            NewChat_frm form = (NewChat_frm)control;
                            Thread chatserver = new Thread(form.Connect);
                            chatserver.Name = "ChatServer";
                            chatserver.IsBackground = true;
                            chatserver.Start();
                        }
                    }
            }

            ConnectionCheck.Enabled = true;
        }

        private void NavigateOnClick(object sender,EventArgs e)
        {
            TabControl tabpanel = (TabControl)sender;
            TabPage tab = tabpanel.SelectedTab;

            foreach (Control control in tab.Controls)
            {
                if (control is WebBrowserTab_frm)
                {
                    WebBrowserTab_frm browser = (WebBrowserTab_frm)control;
                    if (tab.Name == "Tournament Room")
                    {
                        if(!browser.IsLoaded())
                            browser.Navigate("http://devpro.org/launcher/tournamentchat.php");
                    }
                    else if (tab.Name == "Youtube")
                    {
                        if (!browser.IsLoaded())
                            browser.Navigate("https://www.youtube.com/user/blub2blb");
                    }
                    else if (tab.Name == "Ranking")
                    {
                        if (!browser.IsLoaded())
                        {
                            if(Program.Config.ServerName == "DevPro USA")
                                browser.Navigate("http://devpro.org/launcher/ranking/ranking.php");
                            else
                                browser.Navigate("http://dev.ygopro-online.net/launcher/ranking/");
                        }
                    }
                    else if (tab.Name == "Sponser")
                    {
                        string useruid = Program.UserInfo.Username;
                        if (!browser.IsLoaded())
                            browser.Navigate("http://iframe.sponsorpay.com/?appid=11433&uid=" + useruid);
                    }
                }
            }
            
        }
    }
}
