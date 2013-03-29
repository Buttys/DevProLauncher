using System;
using System.Windows.Forms;
using YGOPro_Launcher.Chat.Enums;
using System.Drawing;
using System.Diagnostics;

namespace YGOPro_Launcher.Chat
{
    public class ChatWindow : TabPage
    {
        CustomRTB ChatLog = new CustomRTB();
        public bool isprivate = false;
        public bool issystemtab = false;
        public ChatWindow(string name,bool privatewindow)
        {
            this.Name = name;
            this.Text = name;
            this.Controls.Add(ChatLog);
            ChatLog.Dock = DockStyle.Fill;
            isprivate = privatewindow;

            ChatLog.Font = new System.Drawing.Font("Arial", 12);

            ChatLog.LinkClicked += new LinkClickedEventHandler(ChatLog_LinkClicked);
            ChatLog.ReadOnly = true;
            ApplyNewSettings();
            ChatLog.TabStop = false;

        }

        private void ChatLog_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        public void WriteMessage(ChatMessage message, bool autoscroll)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<ChatMessage,bool>(WriteMessage), message,autoscroll);
            }
            else
            {
                ChatHelper.WriteMessage(message, ChatLog, autoscroll);
            }

        }

        private void WriteText(string text, Color color)
        {
            ChatLog.Select(ChatLog.TextLength, 0);
            ChatLog.SelectionColor = color;
            ChatLog.AppendText(text);
        }

        private void Chat_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (ChatLog.SelectedText == "") return;
                ContextMenuStrip mnu = new ContextMenuStrip();
                ToolStripMenuItem mnucopy = new ToolStripMenuItem("Copy");

                mnucopy.Click += new EventHandler(CopyText);

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
            try { ChatLog.Font = new Font(Program.Config.ChatFont, (float)Program.Config.ChatSize); }
            catch { }
        }

    }
}
