using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YGOPro_Launcher
{
    public partial class Login_frm : Form
    {

        private readonly Configuration _configuration;

        private readonly NetClient _connection;

        private readonly UserData _userInfo;

        public Login_frm(Configuration configuration, NetClient connection, UserData userData)
        {
            InitializeComponent();
            _configuration = configuration;
            _connection = connection;
            _userInfo = userData;
            _connection.LoginReply += new NetClient.ServerResponse(LoginResponse);
            UsernameInput.Text = _configuration.DefualtUsername;
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
                MessageBox.Show("Please enter username,");
                return;
            }
            if (PasswordInput.Text == "")
            {
                MessageBox.Show("Please enter username,");
                return;
            }
            if (_connection.IsConnected)
                _connection.SendPacket("LOGIN|" + UsernameInput.Text + "|" + LauncherHelper.EncodePassword(PasswordInput.Text));
            else
                MessageBox.Show("Not connected to server.");

        }

        private void LoginResponse(string message)
        {
            if (message != "" && !IsDisposed)
            {
                if (message == "" || UsernameInput.Text == "")
                {
                    MessageBox.Show("There was an error logging in. Please try again");
                    return;
                }
                if (message == "Failed") MessageBox.Show(message,"Login Error",MessageBoxButtons.OK);
                else
                {
                    _userInfo.Username = UsernameInput.Text;
                    _userInfo.Rank = 0;
                    _userInfo.LoginKey = message;
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
            {
                if(!IsDisposed)
                    MessageBox.Show("There was an error logging in. Please try again");
            }

        }

    }
}
