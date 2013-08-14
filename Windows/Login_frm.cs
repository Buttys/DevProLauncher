using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevProLauncher.Helpers;
using System.Diagnostics;
using System.IO;
using DevProLauncher.Config;
using DevProLauncher.Network.Enums;
using DevProLauncher.Network.Data;
using ServiceStack.Text;
using DevProLauncher.Windows.MessageBoxs;

namespace DevProLauncher.Windows
{
    public sealed partial class LoginFrm : Form
    {
        private readonly Timer m_loginTimeOut = new Timer();
        public LoginFrm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;

            Program.ChatServer.LoginReply += LoginResponse;

            m_loginTimeOut.Interval = 3000;
            usernameInput.Text = Program.Config.DefaultUsername;

            if (Directory.Exists(LanguageManager.Path))
            {
                string[] languages = Directory.GetDirectories(LanguageManager.Path);
                foreach (string language in languages)
                {
                    string[] split = language.Split('/');
                    if (split.Length > 1)
                        languageSelect.Items.Add(split[1]);
                }

                languageSelect.SelectedItem = Program.Config.Language;
                languageSelect.SelectedIndexChanged += LanguageSelect_SelectedIndexChanged;
            }
            else
            {
                languageSelect.Items.Add("English");
                languageSelect.SelectedIndex = 0;
            }

            m_loginTimeOut.Tick += LoginTimeOut_Tick;
            usernameInput.KeyDown += UsernameInput_KeyDown;
            passwordInput.KeyDown += PasswordInput_KeyDown;

            PatchNotes.Navigate(languageSelect.SelectedItem.ToString() == "German"
                                    ? "http://ygopro.de/patches/"
                                    : "http://ygopro.de/patches/?lang=en");
            PatchNotes.Navigating += WebRedirect;


            ApplyTranslation();
        }

        public bool Connect()
        {
            return Program.ChatServer.Connect(Program.Config.ServerAddress, Program.Config.ChatPort);

        }

        public void ApplyTranslation()
        {
            if (Program.LanguageManager.Loaded)
            {
                label1.Text = Program.LanguageManager.Translation.LoginUserName;
                label2.Text = Program.LanguageManager.Translation.LoginPassWord;
                label3.Text = Program.LanguageManager.Translation.LoginLanguage;
                loginBtn.Text = Program.LanguageManager.Translation.LoginLoginButton;
                registerBtn.Text = Program.LanguageManager.Translation.LoginRegisterButton;
            }
        }

        private void WebRedirect(object sender, CancelEventArgs e)
        {
            var webbrowser = (WebBrowser)sender;
            if (!webbrowser.StatusText.StartsWith("http://ygopro.de"))
            {
                e.Cancel = true;
                Process.Start(webbrowser.StatusText);
            }
        }

        private void UsernameInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                e.SuppressKeyPress = true;
        }

        private void LanguageSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Config.Language = languageSelect.SelectedItem.ToString();
            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
            Program.LanguageManager.Load(languageSelect.SelectedItem.ToString());
            PatchNotes.Navigate(languageSelect.SelectedItem.ToString() == "German"
                                    ? "http://ygopro.de/patches/"
                                    : "http://ygopro.de/patches/?lang=en");
            ApplyTranslation();
            Program.MainForm.ReLoadLanguage();
        }

        private void LoginTimeOut_Tick(object sender, EventArgs e)
        {
            if (!IsDisposed)
            {
                ResetTimeOut();
                MessageBox.Show("Login Timeout");
            }
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
                    m_loginTimeOut.Enabled = false;
                    Enabled = true;
                    loginBtn.Enabled = true;
                }
            }
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (!Program.ChatServer.Connected())
            {
                if (!Connect())
                {
                    MessageBox.Show(Program.LanguageManager.Translation.pMsbErrorToServer);
                    return;
                }
            }

            loginBtn.Enabled = false;
            m_loginTimeOut.Enabled = true;
            if (usernameInput.Text == "")
            {
                MessageBox.Show(Program.LanguageManager.Translation.LoginMsb2);
                return;
            }
            if (passwordInput.Text == "")
            {
                MessageBox.Show(Program.LanguageManager.Translation.LoginMsb3);
                return;
            }
            Program.ChatServer.SendPacket(DevServerPackets.Login,
            JsonSerializer.SerializeToString(
            new LoginRequest { Username = usernameInput.Text, Password = LauncherHelper.EncodePassword(passwordInput.Text), UID = LauncherHelper.GetUID() }));
            Program.Config.Password = LauncherHelper.EncodePassword(passwordInput.Text);
        }

        private void LoginResponse(DevClientPackets type, LoginData data)
        {
            if (type == DevClientPackets.Banned)
            {
                MessageBox.Show("You are banned.");
            }
            else if (type == DevClientPackets.LoginFailed)
            {
                ResetTimeOut();
                MessageBox.Show("Incorrect Password or Username.");
            }
            else
            {
                if (Program.UserInfo == null)
                {
                    Program.UserInfo = new UserData
                        {
                            rank = data.UserRank,
                            username = data.Username,
                            team = data.Team,
                            teamRank = data.TeamRank
                        };
                    Program.LoginKey = data.LoginKey;
                    ResetTimeOut();
                    Program.MainForm.Login();
                }
            }
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            if (!Program.ChatServer.Connected())
            {
                if (!Connect())
                {
                    MessageBox.Show(Program.LanguageManager.Translation.pMsbErrorToServer);
                    return;
                }
            }

            var form = new RegisterFrm();
            form.ShowDialog();
        }

        private void PasswordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (loginBtn.Enabled)
                    loginBtn_Click(sender, null);
                else
                    MessageBox.Show("Not connected to server.");
        }

        private void cloudBtn_Click(object sender, EventArgs e)
        {
            DropBoxSynch_frm dbsfrm = new DropBoxSynch_frm();

            dbsfrm.ShowDialog();
        }
    }
}
