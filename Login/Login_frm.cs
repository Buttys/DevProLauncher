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
        public Login_frm()
        {
            InitializeComponent();
            Program.ServerConnection.LoginReply += new NetClient.ServerResponse(LoginResponse);
            UsernameInput.Text = Program.Config.DefualtUsername;
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
            if (!Program.ServerConnection.IsConnected)
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
            if (Program.ServerConnection.IsConnected)
                Program.ServerConnection.SendPacket("LOGIN|" + UsernameInput.Text + "|" + LauncherHelper.EncodePassword(PasswordInput.Text));
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
                    Program.UserInfo.Username = UsernameInput.Text;
                    Program.UserInfo.Rank = 0;
                    Program.UserInfo.LoginKey = message;
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
