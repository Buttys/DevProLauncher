using System;
using System.Windows.Forms;
using YGOPro_Launcher.Chat.Enums;
using System.Drawing;

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
                    WriteText("<", Color.Black);
                    WriteText(message.From.Username, message.UserColor);
                    WriteText("> ", Color.Black);
                    WriteText(message.FormattedMessage.Trim(), message.MessageColor);

                }
                else if (message.Type == MessageType.System)
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

    }
}
