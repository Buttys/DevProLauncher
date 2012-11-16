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
                CreateBrowserWindow("Chat", "http://liberty.mainframe-irc.net:20003/?nick=&channels=ygopro"),
                CreateBrowserWindow("Youtube", "https://www.youtube.com/user/blub2blb"),
                FileManager, CustomizeTab, AboutTab,AdminTab });
            }
            else
            {
                ServerControl.TabPages.AddRange(new TabPage[] { ServerTab, 
                CreateBrowserWindow("Chat", "http://liberty.mainframe-irc.net:20003/?nick=&channels=ygopro"),
                CreateBrowserWindow("Youtube", "http://www.youtube.com/user/blub2blb"),
                FileManager, CustomizeTab, AboutTab });
            }
            Program.ServerConnection.ServerMessage += new NetClient.ServerResponse(ServerMessage);

        }

        private void ServerMessage(string message)
        {
            MessageBox.Show(message, "Server Message", MessageBoxButtons.OK);
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

        private void Main_frm_Load(object sender, EventArgs e)
        {
            foreach (TabPage tab in ServerControl.TabPages)
            {
                if (tab.Name == Program.Config.ServerName)
                {
                    foreach (Control control in tab.Controls)
                    {
                        if (control is ServerInterface_frm)
                        {
                            ServerInterface_frm form = (ServerInterface_frm)control;
                            form.RequestUserWLD();
                            Program.ServerConnection.SendPacket("GETROOMS");
                            break;
                        }
                    }
                }
            }
        }
    }
}
