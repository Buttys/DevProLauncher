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
        public LoginFrm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;

            Program.ChatServer.LoginReply += LoginResponse;

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

            usernameInput.KeyDown += UsernameInput_KeyDown;
            passwordInput.KeyDown += PasswordInput_KeyDown;

            PatchNotes.ScriptErrorsSuppressed = true;
            PatchNotes.Navigate(languageSelect.SelectedItem.ToString() == "German"
                                    ? "http://ygopro.de/update-news/"
                                    : "http://ygopro.de/update-news/?lang=en");
            PatchNotes.Navigating += WebRedirect;


            ApplyTranslation();

            if (Program.Config.SavePassword && !string.IsNullOrEmpty(Program.Config.EncodedPassword))
            {
                savePassCheckBox.Checked = true;
                passwordInput.Text = Program.Config.EncodedPassword;
                usernameInput.Text = Program.Config.SavedUsername;
            }

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
                savePassCheckBox.Text = Program.LanguageManager.Translation.LoginSavePass;
            }
        }

        private void WebRedirect(object sender, CancelEventArgs e)
        {
            try
            {
                var webbrowser = (WebBrowser) sender;
                if (!webbrowser.StatusText.StartsWith("http://ygopro.de"))
                {
                    e.Cancel = true;
                    Process.Start(webbrowser.StatusText);
                }
            }
            catch (Exception)
            {
                
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
                                    ? "http://ygopro.de/update-news/"
                                    : "http://ygopro.de/update-news/?lang=en");
            ApplyTranslation();
            Program.MainForm.ReLoadLanguage();
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

            bool encoded = (!string.IsNullOrEmpty(Program.Config.EncodedPassword) &&
                            passwordInput.Text == Program.Config.EncodedPassword &&
                            savePassCheckBox.Checked);
            Program.ChatServer.SendPacket(DevServerPackets.Login,
            JsonSerializer.SerializeToString(
            new LoginRequest { Username = usernameInput.Text, Password = (encoded ? passwordInput.Text:LauncherHelper.EncodePassword(passwordInput.Text)), UID = LauncherHelper.GetUID(), Version = Convert.ToInt32(Program.Version)}));
        }

        private void LoginResponse(DevClientPackets type, LoginData data)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<DevClientPackets, LoginData>(LoginResponse), type, data);
                return;
            }

            if (type == DevClientPackets.Banned)
            {
                MessageBox.Show("You are banned.");
            }
            else if (type == DevClientPackets.LoginFailed)
            {
                loginBtn.Enabled = true;
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
                    Program.MainForm.Login();
                    
                    if (savePassCheckBox.Checked)
                    {
                        if (!Program.Config.SavePassword || Program.Config.SavedUsername != usernameInput.Text)
                        {
                            Program.Config.SavePassword = true;
                            Program.Config.SavedUsername = usernameInput.Text;
                            Program.Config.EncodedPassword = LauncherHelper.EncodePassword(passwordInput.Text);
                            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
                        }
                    }
                    else
                    {
                        if (Program.Config.SavePassword)
                        {
                            Program.Config.SavePassword = false;
                            Program.Config.EncodedPassword = string.Empty;
                            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
                        }
                    }
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

        private void CheckmateBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.chkmate_btn_Click(sender, e);
        }
    }
}
