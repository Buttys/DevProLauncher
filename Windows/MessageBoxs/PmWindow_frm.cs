using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevProLauncher.Windows.Components;
using DevProLauncher.Helpers;
using DevProLauncher.Windows.Enums;
using DevProLauncher.Network.Data;

namespace DevProLauncher.Windows.MessageBoxs
{
    public partial class PmWindowFrm : Form
    {
        public bool IsPrivate = false;

        public PmWindowFrm(string name, bool privatewindow)
        {
            InitializeComponent();            
            Name = name;
            Text = name;
            IsPrivate = privatewindow;

            ChatLog.Font = new Font(Program.Config.ChatFont, (float)Program.Config.ChatSize);
            ChatLog.ReadOnly = true;

            ChatLog.MouseUp += Chat_MouseUp;
            ChatLog.LinkClicked += ChatLog_LinkClicked;
            ChatInput.KeyPress += ChatInput_KeyPress;
            
            Activated += ChatInput_Click;
            ApplyNewSettings();
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void ChatInput_Click(object sender, EventArgs e)
        {
            FlashWindow.Stop(this);
        }

        public void WriteMessage(ChatMessage message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ChatMessage>(WriteMessage), message);
            }
            else
            {
                ChatHelper.WriteMessage(message, ChatLog, true);

                if(message.from != null)
                    if(message.from.username != Program.UserInfo.username && !ChatInput.Focused)
                        FlashWindow.Start(this);
            }

        }

        private void Chat_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (ChatLog.SelectedText == "") return;
                var mnu = new ContextMenuStrip();
                var mnucopy = new ToolStripMenuItem("Copy");

                mnucopy.Click += CopyText;

                mnu.Items.Add(mnucopy);

                mnu.Show(ChatLog, e.Location);
            }
        }

        private void CopyText(object sender, EventArgs e)
        {
            Clipboard.SetText(ChatLog.SelectedText);
        }

        private void ChatLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        public void ApplyNewSettings()
        {
            ChatLog.BackColor = (Program.Config.ColorBlindMode ? Color.White : Program.Config.ChatBGColor.ToColor());
            ChatLog.Font = new Font(Program.Config.ChatFont, (float)Program.Config.ChatSize);
        }

        private void ChatInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (ChatInput.Text == "")
                    return;
                if (IsPrivate && ChatInput.Text.StartsWith("/me"))
                {
                    WriteMessage(new ChatMessage(MessageType.Message, CommandType.Me, Program.UserInfo, Name,Program.UserInfo.username + " " +  ChatInput.Text.Replace("/me","").Trim()));
                    Program.ChatServer.SendMessage(MessageType.PrivateMessage, CommandType.Me, Name, ChatInput.Text.Substring(4));
                }
                else if (IsPrivate && ChatInput.Text.StartsWith("/"))
                {
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Name, "Unknown Command."));
                    ChatInput.Clear();
                    e.Handled = true;
                    return;
                }
                else if (IsPrivate)
                {
                    WriteMessage(new ChatMessage(MessageType.PrivateMessage, CommandType.None, Program.UserInfo, Name, ChatInput.Text));
                    Program.ChatServer.SendMessage(MessageType.PrivateMessage, CommandType.None, Name, ChatInput.Text);
                }
                
                ChatInput.Clear();
                e.Handled = true;
            }
        }

    }
}
