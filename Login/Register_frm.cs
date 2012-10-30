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
    public partial class Register_frm : Form
    {
        public Register_frm()
        {
            InitializeComponent();
            Program.ServerConnection.RegisterReply += new NetClient.ServerResponse(RegisterResponse);
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            if (ConfirmInput.Text == "")
            {
                MessageBox.Show("Please confirm your password");
                return;
            }
            if (PasswordInput.Text == "")
            {
                MessageBox.Show("Please enter password");
                return;
            }
            if (UsernameInput.Text == "")
            {
                MessageBox.Show("Please enter username.");
                return;
            }
           Program.ServerConnection.SendPacket("REGISTER|" + UsernameInput.Text + "|" + LauncherHelper.EncodePassword(PasswordInput.Text) + "|" + LauncherHelper.GetUID());
        }

        private void RegisterResponse(string message)
        {
            if (!this.IsDisposed)
            {
                if (message == "OK")
                {
                    if (MessageBox.Show("Registering Complete!") == System.Windows.Forms.DialogResult.OK)
                    {
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
                else if (message == "USERNAME_EXISTS")
                {
                    MessageBox.Show(message);
                }
            }

        }
    }
}
