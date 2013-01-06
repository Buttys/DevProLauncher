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
                
                if (ChatLog.Text != "")//start a new line unless theres no text
                        ChatLog.AppendText(Environment.NewLine);
               ChatLog.Select(ChatLog.TextLength, 0);
                if (message.Type == MessageType.Message || message.Type == MessageType.PrivateMessage)
                {
                    if(Program.Config.ShowTimeStamp)
                        WriteText(message.Time.ToString("[HH:mm] "),(Program.Config.ColorBlindMode ? Color.Black: Color.FromName(Program.Config.NormalTextColor)));
                    
                    WriteText("<", (Program.Config.ColorBlindMode ? Color.Black : Color.FromName(Program.Config.NormalTextColor)));
                    WriteText((Program.Config.ColorBlindMode && message.From.Rank > 0 ? "[Admin] " + message.From.Username: message.From.Username),
                        (Program.Config.ColorBlindMode ? Color.Black : message.UserColor));
                    WriteText("> ", (Program.Config.ColorBlindMode ? Color.Black : Color.FromName(Program.Config.NormalTextColor)));
                    
                    WriteText(message.FormattedMessage.Trim(), (Program.Config.ColorBlindMode ? Color.Black :message.MessageColor));

                }
                else if (message.Type == MessageType.System || message.Type == MessageType.Join 
                    || message.Type == MessageType.Leave || message.Type == MessageType.Server ||
                    message.Type == MessageType.Me)
                {
                    WriteText((Program.Config.ColorBlindMode ? "[" +message.Type + "] " + message.FormattedMessage : message.FormattedMessage),
                        (Program.Config.ColorBlindMode ? Color.Black : message.MessageColor));
                }
                
                ChatLog.SelectionStart = ChatLog.TextLength;
                ChatLog.SelectionLength = 0;

                if(autoscroll)
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

        public void ApplyNewSettings()
        {
            ChatLog.BackColor = (Program.Config.ColorBlindMode ? Color.White: Color.FromName(Program.Config.ChatBGColor));
        }

    }
}
