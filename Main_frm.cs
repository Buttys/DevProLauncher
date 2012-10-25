using System;
using System.Windows.Forms;
namespace YGOPro_Launcher
{
    public partial class Main_frm : Form
    {
        public Main_frm()
        {
            InitializeComponent();
            char[] version = Program.Version.ToCharArray();
            this.Text = this.Text + " v" + version[0] + "." + version[1] + "." + version[2];

            TabPage FileManager = new TabPage("File Manager");
            TabControl FileControl = new TabControl();
            FileControl.Dock = DockStyle.Fill;
            FileControl.TabPages.Add(new FileManagerTab("Decks", Program.Config.LauncherDir +"deck/",".ydk"));
            FileControl.TabPages.Add(new FileManagerTab("Replays", Program.Config.LauncherDir + "replay/", ".yrp"));
            FileManager.Controls.Add(FileControl);

            ServerControl.TabPages.AddRange(new TabPage[] { new ServerTab(Program.Config.ServerName), 
                CreateBrowserWindow("Chat", "http://liberty.mainframe-irc.net:20003/?nick=&channels=ygopro"),
                FileManager, new CustomizeTab() });

        }

        private void Main_Load(object sender, System.EventArgs e)
        {
            Program.ServerConnection.SendPacket("GETROOMS");
            foreach (TabPage page in ServerControl.TabPages)
            {
                if (page is ServerTab)
                {
                    ServerTab tab = (ServerTab)page;
                     tab.RequestUserWLD();
                    
                }
            }
        }

        private TabPage CreateBrowserWindow(string name, string url)
        {
            TabPage page = new TabPage(name);
            WebBrowser browser = new WebBrowser();
            browser.Navigate(url);
            browser.ScriptErrorsSuppressed = true;
            page.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
            return page;
        }
    }
}
