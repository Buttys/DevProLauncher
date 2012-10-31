using System;
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

        Language _language = new Language();

        internal Login_frm(Configuration configuration, NetClient connection, Authenticator authenticator)
        {
            InitializeComponent();

            _configuration = configuration;
            _connection = connection;
            _authenticator = authenticator;

            UsernameInput.Text = _configuration.DefaultUsername;
            AutoLoginCheckBox.Checked = _configuration.AutoLogin;
            PasswordInput.KeyPress += new KeyPressEventHandler(PasswordInput_KeyPress);

            sprache.Text = Program.Config.language;
            newText();
        }

        private void newText()
        {
            _language.Load(Program.Config.language + ".conf");
            label1.Text = _language.LoginUserName;
            label2.Text = _language.LoginPassWord;
            label3.Text = _language.LoginLanguage;
            LoginBtn.Text = _language.LoginLoginButton;
            RegisterBtn.Text = _language.LoginRegisterButton;
            AutoLoginCheckBox.Text = _language.LoginAutoLogin;
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
                MessageBox.Show(_language.LoginMsb1);
                return;
            }
            if (UsernameInput.Text == "")
            {
                MessageBox.Show(_language.LoginMsb1);
                return;
            }
            if (PasswordInput.Text == "")
            {
                MessageBox.Show(_language.LoginMsb1);
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

        private void sprache_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Config.language = sprache.Text;

            switch (Program.Config.language)
            {
                case "english":
                    Program.Config.language = "english";
                    newText();
                    break;
                case "german":
                    Program.Config.language = "german";
                    newText();
                    break;
                case "spain":
                    Program.Config.language = "spain";
                    newText();
                    break;
                case "french":
                    Program.Config.language = "french";
                    newText();
                    break;
                case "italy":
                    Program.Config.language = "italy";
                    newText();
                    break;
                default:
                    Program.Config.language = "english";
                    newText();
                    break;
            }

            
        }
    }
}
