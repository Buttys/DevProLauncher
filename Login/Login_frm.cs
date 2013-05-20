using System;
using System.IO;
using System.Windows.Forms;
using YGOPro_Launcher.Config;
using YGOPro_Launcher.Login;
using System.Diagnostics;

namespace YGOPro_Launcher
{
    public partial class Login_frm : Form
    {

        private readonly Configuration _configuration;

        private readonly NetClient _connection;

        private readonly Authenticator _authenticator;

        internal Login_frm(Configuration configuration, NetClient connection, Authenticator authenticator)
        {
            InitializeComponent();
            this.Text = "DevPro" + " v" + Program.Version[0] + "." + Program.Version[1] + "." + Program.Version[2] + " r" + Program.Version[3];
            _configuration = configuration;
            _connection = connection;
            _authenticator = authenticator;

            UsernameInput.Text = _configuration.DefaultUsername;
            //AutoLoginCheckBox.Checked = _configuration.AutoLogin;
            _authenticator.ResetTimeout += new Authenticator.Reset(ResetTimeOut);
            _authenticator.NotifyLogin += new Authenticator.Done(LoginDone);

            if (Directory.Exists(LanguageManager.Path))
            {
                string[] languages = Directory.GetDirectories(LanguageManager.Path);
                foreach (string language in languages)
                {
                    string[] split = language.Split('/');
                    if (split.Length > 1)
                        LanguageSelect.Items.Add(split[1]);
                }

                LanguageSelect.SelectedItem = Program.Config.Language;
                LanguageSelect.SelectedIndexChanged += new EventHandler(LanguageSelect_SelectedIndexChanged);
            }
            else
            {
                LanguageSelect.Items.Add("English");
                LanguageSelect.SelectedIndex = 0;
            }

            foreach (Server server in Program.ServerList)
            {
                ServerSelect.Items.Add(server.ServerName);
            }
            ServerSelect.SelectedItem = Program.Config.DefaultServer;
            LoginTimeOut.Tick += new EventHandler(LoginTimeOut_Tick);
            UsernameInput.KeyDown += new KeyEventHandler(UsernameInput_KeyDown);
            ServerSelect.SelectedIndexChanged += new EventHandler(ServerSelect_SelectedIndexChanged);

            ApplyTranslation();
            //FormStyler.ApplyStlye(this, new StyleInfo());
        }

        public void ApplyTranslation()
        {
            if (Program.LanguageManager.Loaded)
            {
                Font = new System.Drawing.Font("ARIALUNI", 8);
                label1.Text = Program.LanguageManager.Translation.LoginUserName;
                label2.Text = Program.LanguageManager.Translation.LoginPassWord;
                label3.Text = Program.LanguageManager.Translation.LoginLanguage;
                LoginBtn.Text = Program.LanguageManager.Translation.LoginLoginButton;
                RegisterBtn.Text = Program.LanguageManager.Translation.LoginRegisterButton;
                //AutoLoginCheckBox.Text = Program.LanguageManager.Translation.LoginAutoLogin;
                OfflineBtn.Text = Program.LanguageManager.Translation.LoginBtnOffline;
            }
        }

        private void UsernameInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                e.SuppressKeyPress = true;
        }

        private void LoginTimeOut_Tick(object sender, EventArgs e)
        {
            if (!IsDisposed)
            {
                if (Program.UserInfo.Username == "" && Program.UserInfo.LoginKey == "")
                {
                    ResetTimeOut();
                    MessageBox.Show("Login Timeout");
                }
                else
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        private void LoginDone()
        {
            if (!IsDisposed)
            {
                if (Program.UserInfo.Username != "" && Program.UserInfo.LoginKey != "")
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        private void ServerSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsDisposed)
            {
                if (ServerSelect.SelectedIndex == -1)
                    return;
                Server info = null;
                foreach (Server server in Program.ServerList)
                    if (server.ServerName == ServerSelect.SelectedItem.ToString())
                        info = server;
                if (info == null)
                    return;

                Program.Config.DefaultServer = info.ServerName;
                Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
                Process process = new Process();
                ProcessStartInfo startInfos = new ProcessStartInfo(Application.ExecutablePath, "-r");
                process.StartInfo = startInfos;
                process.Start();
                Application.Exit();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (DialogResult != DialogResult.OK)
                Application.Exit();
        }

        public void ResetTimeOut()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ResetTimeOut));
            }
            else
            {
                if (!IsDisposed)
                {
                    LoginTimeOut.Enabled = false;
                    this.Enabled = true;
                }
            }
        }

        private void LanguageSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Config.Language = LanguageSelect.SelectedItem.ToString();
            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
            Program.LanguageManager.Load(LanguageSelect.SelectedItem.ToString());
            ApplyTranslation();
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            Register_frm form = new Register_frm();
            form.ShowDialog();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (ServerSelect.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select a server.");
                return;
            }
            this.Enabled = false;
            LoginTimeOut.Enabled = true;
            if (!_connection.IsConnected)
            {
                if (!Program.ServerConnection.Connect(Program.Config.ServerName, Program.Config.ServerAddress, Program.Config.ServerPort))
                {
                    MessageBox.Show(Program.LanguageManager.Translation.pMsbErrorToServer);
                    ResetTimeOut();
                    return;
                }
            }
            if (UsernameInput.Text == "")
            {
                MessageBox.Show(Program.LanguageManager.Translation.LoginMsb2);
                return;
            }
            if (PasswordInput.Text == "")
            {
                MessageBox.Show(Program.LanguageManager.Translation.LoginMsb3);
                return;
            }

            _authenticator.Authenticate(UsernameInput.Text, LauncherHelper.EncodePassword(PasswordInput.Text));

            //if (AutoLoginCheckBox.Checked)
            //{
            //    _configuration.DefaultUsername = UsernameInput.Text;
            //    _configuration.Password = LauncherHelper.EncodePassword(PasswordInput.Text);
            //    _configuration.AutoLogin = AutoLoginCheckBox.Checked;
            //    _configuration.DefaultServer = ServerSelect.SelectedItem.ToString();
            //    Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
            //}
            _configuration.Password = LauncherHelper.EncodePassword(PasswordInput.Text);
        }

        private void OfflineBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.RunGame(null);
        }



    }
}
