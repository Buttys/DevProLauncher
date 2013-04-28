using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using YGOPro_Launcher.Chat;
using System.Threading;
using YGOPro_Launcher.Support;
using YgoServer.NetworkData;

namespace YGOPro_Launcher
{
    public partial class Main_frm : Form
    {
       

        public Main_frm()
        {
            InitializeComponent();

            if (LauncherHelper.CardManager == null)
            {
                LauncherHelper.CardManager = new CardDatabase.CardsManager();
                LauncherHelper.CardManager.Init();
            }

            char[] version = Program.Version.ToCharArray();
            this.Text = "DevPro" + " v" + version[0] + "." + version[1] + "." + version[2] + " r" + Program.Version[3] + " - " + Program.UserInfo.Username;

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
            
            TabPage ChatTab = new TabPage() { Text = "Chat (Beta v3)", Name = Program.LanguageManager.Translation.MainChatTab};
            ChatTab.Controls.Add(new NewChat_frm());


            TabPage supportTab = new TabPage() { Text = "Support DevPro", Name = "Support" };
            supportTab.Controls.Add(new Support.Support_frm());

            TabPage patchTab = new TabPage() { Text = "Patch Notes", Name = "Notes" };
            patchTab.Controls.Add(new PatchNotes_frm());

            ServerControl.TabPages.AddRange(new TabPage[] { ServerTab,patchTab,
            ChatTab,
            FileManager, CustomizeTab,supportTab, AboutTab });

            Program.ServerConnection.ServerMessage += new NetClient.ServerResponse(ServerMessage);
            ConnectionCheck.Tick += new EventHandler(CheckConnection);

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
        public void UpdateUsername(string username)
        {
            this.Text = "DevPro" + " v" + Program.Version[0] + "." + Program.Version[1] + "." + Program.Version[2] + " r" + Program.Version[3] + " - " + Program.UserInfo.Username;
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
                            Program.ServerConnection.SendPacket(ServerPackets.GameList);
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
  
        
    }
}
