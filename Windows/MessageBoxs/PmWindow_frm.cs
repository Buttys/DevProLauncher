using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevProLauncher.Windows.Components;
using DevProLauncher.Helpers;
using DevProLauncher.Windows.Enums;
using DevProLauncher.Network.Data;

namespace DevProLauncher.Windows.MessageBoxs
{
    public partial class PmWindow_frm : Form
    {
        public bool isprivate = false;

        public PmWindow_frm(string name, bool privatewindow)
        {
            InitializeComponent();            
            this.Name = name;
            this.Text = name;
            isprivate = privatewindow;

            ChatLog.Font = new System.Drawing.Font(Program.Config.ChatFont, (float)Program.Config.ChatSize);
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
                ChatHelper.WriteMessage(message, ChatLog, true);

                if(message.from != null)
                    if(message.from.username != Program.UserInfo.username && !ChatInput.Focused)
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
            ChatLog.BackColor = (Program.Config.ColorBlindMode ? Color.White : Program.Config.ChatBGColor.ToColor());
            ChatLog.Font = new Font(Program.Config.ChatFont, (float)Program.Config.ChatSize);
        }

        private void ChatInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (ChatInput.Text == "")
                    return;
                if (isprivate && ChatInput.Text.StartsWith("/me"))
                {
                    WriteMessage(new ChatMessage(MessageType.Message, CommandType.Me, Program.UserInfo, Name,Program.UserInfo.username + " " +  ChatInput.Text.Replace("/me","").Trim()));
                    Program.ChatServer.SendMessage(MessageType.PrivateMessage, CommandType.Me, Name, ChatInput.Text.Substring(4));
                }
                else if (isprivate && ChatInput.Text.StartsWith("/"))
                {
                    WriteMessage(new ChatMessage(MessageType.System, CommandType.None, Name, "Unknown Command."));
                    ChatInput.Clear();
                    e.Handled = true;
                    return;
                }
                else if (isprivate)
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
