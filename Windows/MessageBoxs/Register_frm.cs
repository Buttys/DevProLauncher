using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevProLauncher.Network.Enums;
using DevProLauncher.Network.Data;
using ServiceStack.Text;
using DevProLauncher.Helpers;

namespace DevProLauncher.Windows.MessageBoxs
{
    public partial class RegisterFrm : Form
    {
        public RegisterFrm()
        {
            InitializeComponent();
            ApplyTranslation();
            Program.ChatServer.RegisterReply += RegisterResponse;
            UsernameInput.KeyDown += UsernameInput_KeyDown;
            FormClosing += ResetEvents;
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
            Program.ChatServer.SendPacket(DevServerPackets.Register, JsonSerializer.SerializeToString(
                new LoginRequest
                    { 
                    Username = UsernameInput.Text, 
                    Password= LauncherHelper.EncodePassword(PasswordInput.Text), 
                    UID= LauncherHelper.GetUID()
                }));
            RegisterBtn.Enabled = false;
        }

        private void RegisterResponse(DevClientPackets packet)
        {
            if (!IsDisposed)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<DevClientPackets>(RegisterResponse),packet);
                    return;
                }
                if (packet == DevClientPackets.RegisterAccept)
                {
                    if (MessageBox.Show(Program.LanguageManager.Translation.RegistMsb4) == DialogResult.OK)
                    {
                        DialogResult = DialogResult.OK;
                    }
                }
                else if (packet == DevClientPackets.RegisterFailed)
                {
                    MessageBox.Show(Program.LanguageManager.Translation.RegistMsb5);
                }
                RegisterBtn.Enabled = true;
            }

        }

        private void ResetEvents(object sender, EventArgs e)
        {
            Program.ChatServer.RegisterReply -= RegisterResponse;
        }
    }
}
