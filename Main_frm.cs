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
            this.Text = this.Text + " v" + version[0] + "." + version[1] + "." + version[2];

            TabPage FileManager = new TabPage("File Manager");
            TabControl FileControl = new TabControl();
            FileControl.Dock = DockStyle.Fill;

            TabPage decktab = new TabPage() {Name = "Decks",Text ="Decks" };
            decktab.Controls.Add(new FileManager_frm("Decks", Program.Config.LauncherDir + "deck/", ".ydk"));
            
            TabPage replaytab = new TabPage() { Name = "Replays", Text = "Replays" };
            replaytab.Controls.Add(new FileManager_frm("Replays", Program.Config.LauncherDir + "replay/", ".yrp"));
            FileControl.TabPages.AddRange(new TabPage[] { decktab, replaytab });

            FileManager.Controls.Add(FileControl);

            TabPage ServerTab = new TabPage() { Text = Program.Config.ServerName, Name = Program.Config.ServerName };
            ServerTab.Controls.Add(new ServerInterface_frm(Program.Config.ServerName));

            TabPage CustomizeTab = new TabPage() { Text = "Customize", Name = "Customize" };
            CustomizeTab.Controls.Add(new Customize_frm());

            TabPage AboutTab = new TabPage() { Text = "About", Name = "About" };
            AboutTab.Controls.Add(new About_frm());

            if (Program.UserInfo.Rank > 0)
            {
                TabPage AdminTab = new TabPage() { Text = "Admin Panel", Name = "Admin" };
                AdminTab.Controls.Add(new Admin_frm());


                ServerControl.TabPages.AddRange(new TabPage[] { ServerTab, 
                CreateBrowserWindow("Ranking"),
                CreateBrowserWindow("Chat"),
                CreateBrowserWindow("Youtube"),
                FileManager, CustomizeTab, AboutTab,AdminTab });
                if (Program.UserInfo.Rank >= 2)
                {
                    TabPage ChatTab = new TabPage() { Text = "Chat (Alpha)", Name = "Chat (Alpha)" };
                    ChatTab.Controls.Add(new Chat_frm());

                    ServerControl.TabPages.Add(ChatTab);
                }

            }
            else
            {
                ServerControl.TabPages.AddRange(new TabPage[] { ServerTab,
                CreateBrowserWindow("Ranking"),
                CreateBrowserWindow("Chat"),
                CreateBrowserWindow("Youtube"),
                FileManager, CustomizeTab, AboutTab });
            }
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

        private TabPage CreateBrowserWindow(string name)
        {
            TabPage page = new TabPage(name);
            page.Name = name;
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
                        if (control is ServerInterface_frm)
                        {
                            ServerInterface_frm form = (ServerInterface_frm)control;
                            form.RequestUserWLD();
                            Program.ServerConnection.SendPacket("GETROOMS");
                        }
                        if (control is Chat_frm)
                        {
                            Chat_frm form = (Chat_frm)control;
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
                if (control is WebBrowser)
                {
                    WebBrowser browser = (WebBrowser)control;
                    if (browser.Url == null)
                    {
                        if (tab.Name == "Chat")
                            browser.Navigate("http://liberty.mainframe-irc.net:20003/?nick=&channels=ygopro");
                        else if (tab.Name == "Youtube")
                            browser.Navigate("https://www.youtube.com/user/blub2blb");
                        else if (tab.Name == "Ranking")
                            browser.Navigate("http://h2092539.stratoserver.net/ranking/ranking.php");
                    }
                }
            }

        }
    }
}
