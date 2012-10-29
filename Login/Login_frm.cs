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

        internal Login_frm(Configuration configuration, NetClient connection, Authenticator authenticator)
        {
            InitializeComponent();

            _configuration = configuration;
            _connection = connection;
            _authenticator = authenticator;

            UsernameInput.Text = _configuration.DefaultUsername;
            AutoLoginCheckBox.Checked = _configuration.AutoLogin;
            PasswordInput.KeyPress += new KeyPressEventHandler(PasswordInput_KeyPress);
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
                MessageBox.Show("Not connected to the server.");
                return;
            }
            if (UsernameInput.Text == "")
            {
                MessageBox.Show("Please enter username.");
                return;
            }
            if (PasswordInput.Text == "")
            {
                MessageBox.Show("Please enter password.");
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
