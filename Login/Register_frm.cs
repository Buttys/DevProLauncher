﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace YGOPro_Launcher
{
    public partial class Register_frm : Form
    {
        public Register_frm()
        {
            InitializeComponent();
            ApplyTranslation();
            Program.ServerConnection.RegisterReply += new NetClient.ServerResponse(RegisterResponse);
            UsernameInput.KeyDown += new KeyEventHandler(UsernameInput_KeyDown);
        }

        public void ApplyTranslation()
        {
            if (Program.LanguageManager.Loaded)
            {
                label1.Text = Program.LanguageManager.Translation.RegistLbUser;
                label2.Text = Program.LanguageManager.Translation.RegistLbPw;
                label3.Text = Program.LanguageManager.Translation.RegistLbPw2;
                RegisterBtn.Text = Program.LanguageManager.Translation.RegistBtnRegister;
                CancelBtn.Text = Program.LanguageManager.Translation.RegistBtnCancel;
            }
        }

        private void UsernameInput_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
                e.SuppressKeyPress = true;
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            if (ConfirmInput.Text != PasswordInput.Text)
            {
                MessageBox.Show("Confirm password is wrong.");
                return;
            }
            if (ConfirmInput.Text == "")
            {
                MessageBox.Show(Program.LanguageManager.Translation.RegistMsb1);
                return;
            }
            if (PasswordInput.Text == "")
            {
                MessageBox.Show(Program.LanguageManager.Translation.RegistMsb2);
                return;
            }
            if (UsernameInput.Text == "")
            {
                MessageBox.Show(Program.LanguageManager.Translation.RegistMsb3);
                return;
            }
            if (!Regex.IsMatch(UsernameInput.Text, "^[a-zA-Z0-9_]*$"))
            {
                MessageBox.Show(Program.LanguageManager.Translation.RegistMsb6);
                return;
            }
           Program.ServerConnection.SendPacket("REGISTER||" + UsernameInput.Text + "||" + LauncherHelper.EncodePassword(PasswordInput.Text) + "||" + LauncherHelper.GetUID());
        }

        private void RegisterResponse(string message)
        {
            if (!this.IsDisposed)
            {
                if (message == "OK")
                {
                    if (MessageBox.Show(Program.LanguageManager.Translation.RegistMsb4) == System.Windows.Forms.DialogResult.OK)
                    {
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
                else if (message == "USERNAME_EXISTS")
                {
                    MessageBox.Show(Program.LanguageManager.Translation.RegistMsb5);
                }
            }

        }
    }
}
