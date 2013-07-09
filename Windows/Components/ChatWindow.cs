using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using DevProLauncher.Helpers;
using DevProLauncher.Network.Data;

namespace DevProLauncher.Windows.Components
{
    public class ChatWindow : TabPage
    {
        readonly CustomRTB _chatLog = new CustomRTB();
        public bool IsPrivate = false;
        public bool IsSystemtab = false;
        public ChatWindow(string name,bool privatewindow)
        {
            Name = name;
            Text = name;
            Controls.Add(_chatLog);
            _chatLog.Dock = DockStyle.Fill;
            IsPrivate = privatewindow;

            _chatLog.Font = new Font("Arial", 12);

            _chatLog.LinkClicked += ChatLog_LinkClicked;
            _chatLog.ReadOnly = true;
            ApplyNewSettings();
            _chatLog.TabStop = false;

        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public void WriteMessage(ChatMessage message, bool autoscroll)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ChatMessage,bool>(WriteMessage), message,autoscroll);
            }
            else
            {
                ChatHelper.WriteMessage(message, _chatLog, autoscroll);
            }

        }

        private void ChatLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        public void ApplyNewSettings()
        {
            _chatLog.BackColor = (Program.Config.ColorBlindMode ? Color.White : Program.Config.ChatBGColor.ToColor());
            _chatLog.Font = new Font(Program.Config.ChatFont, (float)Program.Config.ChatSize);
        }

    }
}
