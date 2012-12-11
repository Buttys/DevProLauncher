using System;
using System.Windows.Forms;
using YGOPro_Launcher.Chat.Enums;
using System.Drawing;
using System.Diagnostics;

namespace YGOPro_Launcher.Chat
{
    public class ChatWindow : TabPage
    {
        RichTextBox ChatLog = new RichTextBox();
        public bool isprivate = false;
        public ChatWindow(string name,bool privatewindow)
        {
            this.Name = name;
            this.Text = name;
            this.Controls.Add(ChatLog);
            ChatLog.Dock = DockStyle.Fill;
            isprivate = privatewindow;

            ChatLog.KeyDown += new KeyEventHandler(ChatLog_KeyDown);
            ChatLog.MouseUp += new MouseEventHandler(Chat_MouseUp);
            ChatLog.LinkClicked += new LinkClickedEventHandler(ChatLog_LinkClicked);
        }

        private void ChatLog_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        public void WriteMessage(ChatMessage message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<ChatMessage>(WriteMessage), message);
            }
            else
            {                   
                
                if (ChatLog.Text != "")//start a new line unless theres no text
                        ChatLog.AppendText(Environment.NewLine);
               ChatLog.Select(ChatLog.TextLength, 0);
                if (message.Type == MessageType.Message || message.Type == MessageType.PrivateMessage)
                {
                    WriteText("<", Color.Black);
                    WriteText(message.From.Username, message.UserColor);
                    WriteText("> ", Color.Black);
                    WriteText(message.FormattedMessage.Trim(), message.MessageColor);

                }
                else if (message.Type == MessageType.System || message.Type == MessageType.Join 
                    || message.Type == MessageType.Leave || message.Type == MessageType.Server ||
                    message.Type == MessageType.Me)
                {
                    WriteText(message.FormattedMessage, message.MessageColor);
                }
                
                ChatLog.SelectionStart = ChatLog.TextLength;
                ChatLog.SelectionLength = 0;
                ChatLog.ScrollToCaret();
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

    }
}
