using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevProLauncher.Network.Enums;
using DevProLauncher.Helpers;
using System.Diagnostics;
using ServiceStack.Text;
using DevProLauncher.Network.Data;

namespace DevProLauncher.Windows
{
    public partial class Main_frm : Form
    {
        public GameList_frm gameWindow;
        
        Login_frm loginWindow;
        Chat_frm chatWindow;
        Support_frm devpointWindow;
        FileManager_frm filemanagerWindow;
        Customize_frm customizerWindow;

        public Main_frm()
        {
            InitializeComponent();

            char[] version = Program.Version.ToCharArray();
            this.Text = "DevPro" + " v" + version[0] + "." + version[1] + "." + version[2] + " r" + Program.Version[3];

            LauncherHelper.LoadBanlist();

            TabPage loginTab = new TabPage("Login");
            loginWindow = new Login_frm();
            loginTab.Controls.Add(loginWindow);
            mainTabs.TabPages.Add(loginTab);

            chatWindow = new Chat_frm();
            gameWindow = new GameList_frm("DevPro");
            devpointWindow = new Support_frm();
            filemanagerWindow = new FileManager_frm();
            customizerWindow = new Customize_frm();
            LauncherHelper.CardManager.Init();

            Thread connectThread = new Thread(Loaded);
            connectThread.Start();
        }

        private void Loaded()
        {
            if (!Program.ChatServer.Connect(Program.Config.ServerAddress, Program.Config.ChatPort))
                MessageBox.Show(Program.LanguageManager.Translation.pMsbErrorToServer);
            else
                loginWindow.Connected();
        }
        public void Login()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(Login));
                return;
            }

            mainTabs.TabPages.Remove(mainTabs.SelectedTab);

            TabPage gamelistTab = new TabPage("GameList");
            gamelistTab.Controls.Add(gameWindow);
            mainTabs.TabPages.Add(gamelistTab);

            TabPage chatTab = new TabPage("Chat (Beta v4)");
            chatTab.Controls.Add(chatWindow);
            mainTabs.TabPages.Add(chatTab);

            TabPage filemanagerTab = new TabPage("File Manager");
            filemanagerTab.Controls.Add(filemanagerWindow);
            mainTabs.TabPages.Add(filemanagerTab);

            TabPage cuztomizerTab = new TabPage("Customizer");
            cuztomizerTab.Controls.Add(customizerWindow);
            mainTabs.TabPages.Add(cuztomizerTab);

            TabPage devpointTab = new TabPage("Support DevPro");
            devpointTab.Controls.Add(devpointWindow);
            mainTabs.TabPages.Add(devpointTab);
                
            ConnectionCheck.Enabled = true;
            ConnectionCheck.Tick += new EventHandler(CheckConnection);
            
            UpdateUsername();
            if (Program.DuelServer.Connect(Program.Config.ServerAddress, Program.Config.GamePort))
            {
                Program.DuelServer.SendPacket(DevServerPackets.Login,JsonSerializer.SerializeToString<LoginRequest>(
                    new LoginRequest() { Username = Program.UserInfo.username, Password = Program.Config.Password, UID = LauncherHelper.GetUID() }));
            }
            Program.ChatServer.SendPacket(DevServerPackets.UserList);
            Program.ChatServer.SendPacket(DevServerPackets.FriendList);
            Program.ChatServer.SendPacket(DevServerPackets.DevPoints);

        }

        public void UpdateUsername()
        {
            this.Text = "DevPro" + " v" + Program.Version[0] + "." + Program.Version[1] + "." + Program.Version[2] + " r" + Program.Version[3] + " - " + Program.UserInfo.username;
        }

        private void CheckConnection(object sender, EventArgs e)
        {
            if (!Program.ChatServer.Connected())
            {
                System.Windows.Forms.Timer ConnectionCheck = (System.Windows.Forms.Timer)sender;
                this.Hide();
                ConnectionCheck.Enabled = false;
                if (MessageBox.Show("Disconnected from server.", "Server", MessageBoxButtons.OK) == DialogResult.OK)
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
    }
}
