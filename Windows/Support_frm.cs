using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Diagnostics;
using DevProLauncher.Config;
using DevProLauncher.Windows.Components;
using DevProLauncher.Windows.MessageBoxs;
using DevProLauncher.Network.Enums;
using DevProLauncher.Network.Data;
using ServiceStack.Text;

namespace DevProLauncher.Windows
{
    public sealed partial class SupportFrm : Form
    {
        readonly Dictionary<string, string> m_descriptions = new Dictionary<string, string>();
        public SupportFrm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            Program.ChatServer.DevPointMsg += HandlePackets;

            ApplyTranslation();

            //prevents the last items auto sizing
            LeftItems.Controls.Add(new Label(), 0, LeftItems.RowStyles.Count-1);
            RightItems.Controls.Add(new Label(), 0, LeftItems.RowStyles.Count - 1);

            int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
            RightItems.Padding = new Padding(0, 0, vertScrollWidth, 0);

            OfferLink.Click += OfferLink_Click;
            DonateLink.Click += DonateLink_Click;
            refreshtimer.Tick += refreshtimer_Tick;
        }
        public void ApplyTranslation()
        {
            LanguageInfo lang = Program.LanguageManager.Translation;

            AddItem(Properties.Resources.rankup, lang.SupportItem1Name, FormatString(lang.SupportItem1Des), 100, "DEVSTATUS", false);
            AddItem(Properties.Resources.maskchange, lang.SupportItem2Name, FormatString(lang.SupportItem2Des), 200, "DEVRENAME", true);
            AddItem(Properties.Resources.desruct, lang.SupportItem3Name, FormatString(lang.SupportItem3Des), 50, "DEVRESETRANK", false);
            AddItem(Properties.Resources.rankup, lang.SupportItem8Name, FormatString(lang.SupportItem8Des), 300, "NULL", false);
            AddItem(Properties.Resources.DNA, lang.SupportItem5Name, FormatString(lang.SupportItem5Des), 300, "DEVCOLOR", true);
            AddItem(Properties.Resources.sixsam, lang.SupportItem6Name, FormatString(lang.SupportItem6Des), 500, "DEVCREATETEAM", true);
            AddItem(Properties.Resources.message, lang.SupportItem7Name, FormatString(lang.SupportItem7Des), 150, "DEVMSG", true);
            AddItem(Properties.Resources.bookoflife, lang.SupportItem4Name, FormatString(lang.SupportItem4Des), 1000, "DEVUNBAN", true);
            m_descriptions.Add("DEVRENAME", lang.SupportRenameInput);
            m_descriptions.Add("DEVUNBAN", lang.SupportUnbanInput);
            m_descriptions.Add("DEVCREATETEAM", lang.SupportTeamNameInput);
            m_descriptions.Add("DEVMSG", lang.SupportMSGInput);

            groupBox4.Text = lang.SupportBalance;
            groupBox2.Text = lang.Supportgb2;
            label1.Text = FormatString(lang.Supportgb2text);
            groupBox1.Text = lang.Supportgb3;
            label3.Text = lang.Supportgb3text;
            groupBox3.Text = lang.Supportgb4;
            label2.Text = lang.Supportgb4text;
        }
        private string FormatString(string text)
        {
            return text.Replace("||", Environment.NewLine);
        }

        private void HandlePackets(PacketCommand command)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<PacketCommand>(HandlePackets), command);
            }
            else
            {
                if (command.Command == "POINTS")
                    DevPointCount.Text = command.Data;
                if (command.Command == "MSG")
                    MessageBox.Show(command.Data);
                if (command.Command == "USERNAME")
                {
                    Program.ChangeUsername(command.Data);
                }
            }
        }

        private void AddItem(Image image, string name, string des, int cost,string servercode,bool input)
        {
           TableLayoutPanel panel = (LeftItems.RowStyles.Count <= RightItems.RowStyles.Count ? LeftItems:RightItems);
           int row = panel.RowStyles.Count - 1;
           
           var item = new SupportItem(image, name, des, cost);
           item.button1.Click += Getitem;
           item.button1.Name = (input ? "1":"0") + servercode;
     
           panel.Controls.Add(item, 0, row - 1);
           panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
           
        }

        private void Getitem(object handler, EventArgs e)
        {
            bool input = (((Button)handler).Name[0].ToString(CultureInfo.InvariantCulture) == "1");
            string servercommand = ((Button)handler).Name.Substring(1);

            if (input)
            {
                if (servercommand != "DEVCOLOR")
                {
                    var form = new InputFrm("Input", m_descriptions[servercommand], "Confirm", "Cancel");
                    if (servercommand != "DEVCREATETEAM" && servercommand != "DEVMSG")
                        form.InputBox.KeyDown += Suppress_Space;

                    if (servercommand == "DEVCREATETEAM")
                        form.InputBox.MaxLength = 20;
                    else if (servercommand == "DEVMSG")
                        form.InputBox.MaxLength = 250;
                    else
                        form.InputBox.MaxLength = 14;

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (form.InputBox.Text == "")
                        {
                            MessageBox.Show("Input cannot be empty");
                            return;
                        }
                        Program.ChatServer.SendPacket(DevServerPackets.DevPointCommand,
                            JsonSerializer.SerializeToString(
                            new PacketCommand
                                { Command = servercommand,
                                                  Data = form.InputBox.Text.Trim()
                        }));
                    }
                }
                else
                {
                    var selectcolor = new ColorDialog();
                    if (selectcolor.ShowDialog() == DialogResult.OK)
                    {
                        Program.ChatServer.SendPacket(DevServerPackets.DevPointCommand,
                            JsonSerializer.SerializeToString(
                            new PacketCommand
                                {
                                Command = servercommand,
                                Data = selectcolor.Color.R + "," + selectcolor.Color.G + "," + selectcolor.Color.B
                            }));
                    }

                }
            }
            else
            {
                if (servercommand == "NULL")
                {
                    Process.Start("http://ygopro.de/login/");
                    return;
                }

                if(MessageBox.Show("Confirm","Are you sure?",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Program.ChatServer.SendPacket(DevServerPackets.DevPointCommand,
                            JsonSerializer.SerializeToString(
                            new PacketCommand
                                {
                                Command = servercommand
                            }));
                } 
            }
        }

        private void Suppress_Space(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                e.SuppressKeyPress = true;
        }
        private void OfferLink_Click(object sender, EventArgs e)
        {
            Process.Start("http://iframe.sponsorpay.com/?appid=11433&uid=" + Program.UserInfo.username + "&pup0=eu");
        }
        private void DonateLink_Click(object sender, EventArgs e)
        {
            Process.Start("https://wallapi.com/api/?key=5a7ef592b505f1e3c5cdb9d4e8614790&uid=" + Program.UserInfo.username + "&widget=w1_1");
        }

        private void refreshbtn_Click(object sender, EventArgs e)
        {
            Program.ChatServer.SendPacket(DevServerPackets.DevPoints);
            refreshbtn.Enabled = false;
            refreshtimer.Start();
        }
        private void refreshtimer_Tick(object sender, EventArgs e)
        {
            refreshbtn.Enabled = true;
            refreshtimer.Stop();
        }
    }
}
