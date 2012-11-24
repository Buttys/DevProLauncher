using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YGOPro_Launcher
{
    public partial class Admin_frm : Form
    {
        public Admin_frm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            InputBox.KeyPress +=new KeyPressEventHandler(InputBox_KeyPress);
            ServerLog.KeyDown +=new KeyEventHandler(ServerLog_KeyDown);
            Program.ServerConnection.AdminMessage += new NetClient.ServerResponse(ParseMessage);
            UserList.MouseUp += new MouseEventHandler(UserList_MouseUp);
        }

        private void UserList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = UserList.IndexFromPoint(e.Location);

                if (index == -1) return;
                else
                    UserList.SelectedIndex = index;

                ContextMenuStrip mnu = new ContextMenuStrip();
                ToolStripMenuItem mnukick = new ToolStripMenuItem("Kick");
                ToolStripMenuItem mnuban = new ToolStripMenuItem("Ban");

                mnukick.Click += new EventHandler(KickUser);
                mnuban.Click += new EventHandler(BanUser);

                mnu.Items.AddRange(new ToolStripItem[] { mnukick, mnuban });
                mnu.Show(UserList, e.Location);
            }
        }

        private void BanUser(object sender, EventArgs e)
        {
            Program.ServerConnection.SendPacket("ADMIN||BAN||" + UserList.SelectedItem.ToString());
        }
        private void KickUser(object sender, EventArgs e)
        {
            Program.ServerConnection.SendPacket("ADMIN||KICK||" + UserList.SelectedItem.ToString());
        }

        private void InputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (InputBox.Text == "") return;
                string[] args = InputBox.Text.Split(' ');
                if (args[0].ToLower() != "op")
                {
                    Program.ServerConnection.SendPacket("ADMIN||" + args[0].ToUpper() + "||" + InputBox.Text.Substring(0, args[0].Length).Trim());
                }
                else
                {
                    int rank = 0;
                    if (Int32.TryParse(args[args.Length - 1], out rank))
                    {
                        Program.ServerConnection.SendPacket("ADMIN||" + args[0].ToUpper() + "||" + InputBox.Text.Replace(args[0], "").Replace(args[args.Length - 1], "").Trim() + "||" + rank.ToString());
                    }
                    else
                    {
                        WriteMessage("Invalid args");
                    }
                }
                InputBox.Clear();
                e.Handled = true;
            }
        }

        private void ParseMessage(string command)
        {
            string[] args = command.Split(new string[] {"||"}, StringSplitOptions.None);
            string cmd = args[0];

            if (cmd == "MSG")
            {
                WriteMessage(args[1]);
            }
            else if (cmd == "USERS")
            {
                UpdateUsers(args[1]);
            }
            else
            {
                MessageBox.Show("Unknown server packet.");
            }
        }

        private void UpdateUsers(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateUsers), message); //had issues trying to use string[]
            }
            else
            {
                string[] users = message.Split(',');
                UserList.Items.Clear();
                foreach (string user in users)
                {
                    UserList.Items.Add(user);
                }
                UserCount.Text = "Users: " + UserList.Items.Count;
            }
        }

        private void ServerLog_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void WriteMessage(string message)
        {
            if (InvokeRequired)
            {
              this.Invoke(new Action<string>(WriteMessage),message);
            }
            else
            {
                if (ServerLog.Text != "")
                    ServerLog.AppendText(Environment.NewLine);
                
                ServerLog.Select(ServerLog.TextLength, 0);
                ServerLog.AppendText("Server >> " +message);
                ServerLog.SelectionStart = ServerLog.TextLength;
                ServerLog.SelectionLength = 0;
                ServerLog.ScrollToCaret();
            }

        }

    }
}
