using System;
using System.Windows.Forms;
using YGOPro_Launcher.Chat.Enums;

namespace YGOPro_Launcher.Chat
{
    public class ChatWindow : TabPage
    {
        RichTextBox ChatLog = new RichTextBox();
        public bool isprivate = false;
        public ChatWindow(string name)
        {
            this.Name = name;
            this.Text = name;
            this.Controls.Add(ChatLog);
            ChatLog.Dock = DockStyle.Fill;

            ChatLog.KeyDown += new KeyEventHandler(ChatLog_KeyDown);
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
                if (message.Type == MessageType.Message)
                {
                    ChatLog.AppendText("<" + message.From.Username + "> " + message.FormattedMessage.Trim());
                }
                else if (message.Type == MessageType.System)
                {
                    ChatLog.AppendText(message.FormattedMessage);
                }                    
                
                ChatLog.SelectionStart = ChatLog.TextLength;
                ChatLog.SelectionLength = 0;
                ChatLog.ScrollToCaret();
            }

        }

    }
}
