using System;
using System.IO;
using System.Windows.Forms;
using YGOPro_Launcher.Config;
using YGOPro_Launcher.Login;

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

            _configuration = configuration;
            _connection = connection;
            _authenticator = authenticator;

            UsernameInput.Text = _configuration.DefaultUsername;
            AutoLoginCheckBox.Checked = _configuration.AutoLogin;
            PasswordInput.KeyPress += new KeyPressEventHandler(PasswordInput_KeyPress);

            if (Directory.Exists(LanguageManager.Path))
            {
                string[] languages = Directory.GetDirectories(LanguageManager.Path);
                foreach (string language in languages)
                {
                    LanguageSelect.Items.Add(language.Split('/')[1]);
                }

                LanguageSelect.SelectedItem = Program.Config.Language;
                LanguageSelect.SelectedIndexChanged += new EventHandler(LanguageSelect_SelectedIndexChanged);
            }
            else
            {
                LanguageSelect.Items.Add("Files Not Found");
                LanguageSelect.SelectedIndex = 0;
            }
            ApplyTranslation();
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
                AutoLoginCheckBox.Text = Program.LanguageManager.Translation.LoginAutoLogin;
            }
        }

        private void LanguageSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Config.Language = LanguageSelect.SelectedItem.ToString();
            Program.Config.Save(Program.ConfigurationFilename);
            Program.LanguageManager.Load(LanguageSelect.SelectedItem.ToString());
            ApplyTranslation();
        }

        private void PasswordInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                LoginBtn_Click(null, EventArgs.Empty);
            }
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            Register_frm form = new Register_frm();
            form.ShowDialog();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (!_connection.IsConnected)
            {
                MessageBox.Show(Program.LanguageManager.Translation.LoginMsb1);
                return;
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
            if (AutoLoginCheckBox.Checked)
            {
                _configuration.DefaultUsername = UsernameInput.Text;
                _configuration.Password = LauncherHelper.EncodePassword(PasswordInput.Text);
                _configuration.AutoLogin = AutoLoginCheckBox.Checked;
                _configuration.Save(Program.ConfigurationFilename);
            }
            DialogResult = DialogResult.OK;
        }



    }
}
