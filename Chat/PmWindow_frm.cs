using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGOPro_Launcher.Chat.Enums;

namespace YGOPro_Launcher.Chat
{
    public partial class PmWindow_frm : Form
    {
        public bool isprivate = false;
        public ChatClient server;
        public PmWindow_frm(string name, bool privatewindow,ChatClient connection)
        {

            this.server = connection;
            InitializeComponent();            
            this.Name = name;
            this.Text = name;
            isprivate = privatewindow;

            ChatLog.Font = new System.Drawing.Font("Arial", 12);
            ChatLog.ReadOnly = true;

            ChatLog.MouseUp += new MouseEventHandler(Chat_MouseUp);
            ChatLog.LinkClicked += new LinkClickedEventHandler(ChatLog_LinkClicked);
            ChatInput.KeyPress += new KeyPressEventHandler(ChatInput_KeyPress);
            
            this.Activated += new EventHandler(ChatInput_Click);
            ApplyNewSettings();
        }

        private void ChatInput_Click(object sender, EventArgs e)
        {
            FlashWindow.Stop(this);
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
                    if (Program.Config.ShowTimeStamp)
                        WriteText(message.Time.ToString("[HH:mm] "), (Program.Config.ColorBlindMode ? Color.Black : Color.FromName(Program.Config.NormalTextColor)));

                    WriteText("<", (Program.Config.ColorBlindMode ? Color.Black : Color.FromName(Program.Config.NormalTextColor)));
                    WriteText((Program.Config.ColorBlindMode && message.From.Rank > 0 ? "[Admin] " + message.From.Username : message.From.Username),
                        (Program.Config.ColorBlindMode ? Color.Black : message.UserColor));
                    WriteText("> ", (Program.Config.ColorBlindMode ? Color.Black : Color.FromName(Program.Config.NormalTextColor)));

                    WriteText(message.FormattedMessage.Trim(), (Program.Config.ColorBlindMode ? Color.Black : message.MessageColor));

                }
                else if (message.Type == MessageType.System || message.Type == MessageType.Join
                    || message.Type == MessageType.Leave || message.Type == MessageType.Server ||
                    message.Type == MessageType.Me)
                {
                    WriteText((Program.Config.ColorBlindMode ? "[" + message.Type + "] " + message.FormattedMessage : message.FormattedMessage),
                        (Program.Config.ColorBlindMode ? Color.Black : message.MessageColor));
                }

                ChatLog.SelectionStart = ChatLog.TextLength;
                ChatLog.SelectionLength = 0;
                ChatLog.ScrollToCaret();

                if(message.From.Username != Program.UserInfo.Username && !ChatInput.Focused)
                    FlashWindow.Start(this);

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
            ChatLog.BackColor = (Program.Config.ColorBlindMode ? Color.White : Color.FromName(Program.Config.ChatBGColor));
        }

        private void ChatInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (ChatInput.Text == "")
                    return;
                if (isprivate && ChatInput.Text.StartsWith("/"))
                {
                    WriteMessage(new ChatMessage(MessageType.System, Name, "Commands are not supported in private windows."));
                    return;
                }
                
                if (isprivate)
                {
                    WriteMessage(new ChatMessage(MessageType.Message, Program.UserInfo, Name, ChatInput.Text, false));
                    server.SendPacket("MSG||" + Name + "||" + (int)MessageType.PrivateMessage + "||" + LauncherHelper.StringToBase64(ChatInput.Text));
                }
                else
                {
                    server.SendPacket("MSG||" + Name + "||" + (int)MessageType.Message + "||" + LauncherHelper.StringToBase64(ChatInput.Text));
                }
                
                ChatInput.Clear();
                e.Handled = true;
            }
        }

    }
}
