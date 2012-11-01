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
        private Language lang = new Language();

        public Register_frm()
        {
            InitializeComponent();
            Program.ServerConnection.RegisterReply += new NetClient.ServerResponse(RegisterResponse);
            lang.Load(Program.Config.language + ".conf");
            newText();
        }

        private void newText()
        {
            label1.Text = lang.RegistLbUser;
            label2.Text = lang.RegistLbPw;
            label3.Text = lang.RegistLbPw2;
            RegisterBtn.Text = lang.RegistBtnRegister;
            CancelBtn.Text = lang.RegistBtnCancel;
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            if (ConfirmInput.Text == "")
            {
                MessageBox.Show(lang.RegistMsb1);
                return;
            }
            if (PasswordInput.Text == "")
            {
                MessageBox.Show(lang.RegistMsb2);
                return;
            }
            if (UsernameInput.Text == "")
            {
                MessageBox.Show(lang.RegistMsb3);
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
                    if (MessageBox.Show(lang.RegistMsb4) == System.Windows.Forms.DialogResult.OK)
                    {
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
                else if (message == "USERNAME_EXISTS")
                {
                    MessageBox.Show(lang.RegistMsb5);
                }
            }

        }
    }
}
