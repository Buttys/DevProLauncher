using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGOPro_Launcher.Config;
using System.Diagnostics;

namespace YGOPro_Launcher.Support
{
    public partial class Support_frm : Form
    {
        Dictionary<string, string> descriptions = new Dictionary<string, string>();
        public Support_frm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            Program.ChatServer.DevPointMSG += new Chat.ChatClient.ServerResponse(HandlePackets);

            ApplyTranslation();

            //prevents the last items auto sizing
            LeftItems.Controls.Add(new Label(), 0, LeftItems.RowStyles.Count-1);
            RightItems.Controls.Add(new Label(), 0, LeftItems.RowStyles.Count - 1);
            OfferLink.Click += new EventHandler(OfferLink_Click);
            DonateLink.Click += new EventHandler(DonateLink_Click);
            refreshtimer.Tick += new EventHandler(refreshtimer_Tick);
        }
        private void ApplyTranslation()
        {
            LanguageInfo lang = Program.LanguageManager.Translation;

            AddItem(Properties.Resources.rankup, lang.SupportItem1Name, FormatString(lang.SupportItem1Des), 100, "DEVSTATUS", false);
            AddItem(Properties.Resources.maskchange, lang.SupportItem2Name, FormatString(lang.SupportItem2Des), 200, "DEVRENAME", true);
            AddItem(Properties.Resources.desruct, lang.SupportItem3Name, FormatString(lang.SupportItem3Des), 50, "DEVRESETRANK", false);
            AddItem(Properties.Resources.bookoflife, lang.SupportItem4Name, FormatString(lang.SupportItem4Des), 1000, "DEVUNBAN", true);
            AddItem(Properties.Resources.DNA, lang.SupportItem5Name, FormatString(lang.SupportItem5Des), 300, "DEVCOLOR", true);
            AddItem(Properties.Resources.sixsam, lang.SupportItem6Name, FormatString(lang.SupportItem6Des), 500, "DEVCREATETEAM", true);
            AddItem(Properties.Resources.message, lang.SupportItem7Name, FormatString(lang.SupportItem7Des), 150, "DEVMSG", true);
            descriptions.Add("DEVRENAME", lang.SupportRenameInput);
            descriptions.Add("DEVUNBAN", lang.SupportUnbanInput);
            descriptions.Add("DEVCREATETEAM", lang.SupportTeamNameInput);
            descriptions.Add("DEVMSG", lang.SupportMSGInput);

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

        private void RequestDevPointsBalence()
        {
            Program.ChatServer.SendPacket("GETDEVPOINTS");
        }

        private void HandlePackets(string command)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(HandlePackets), command);
            }
            else
            {
                string[] args = command.Split(new string[] { "||" }, StringSplitOptions.None);

                if (args[1] == "POINTS")
                    DevPointCount.Text = args[2];
                if (args[1] == "MSG")
                    MessageBox.Show(args[2]);
                if (args[1] == "USERNAME")
                {
                    Program.ChangeUsername(args[2]);
                }
            }
        }

        private void AddItem(Image image, string name, string des, int cost,string servercode,bool input)
        {
           TableLayoutPanel panel = (LeftItems.RowStyles.Count <= RightItems.RowStyles.Count ? LeftItems:RightItems);
           int row = panel.RowStyles.Count - 1;
           
           Support_item item = new Support_item(image, name, des, cost);
           item.button1.Click += new EventHandler(Getitem);
           item.button1.Name = (input ? "1":"0") + servercode;
     
           panel.Controls.Add(item, 0, row - 1);
           panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
           
        }

        private void Getitem(object handler, EventArgs e)
        {
            bool input = (((Button)handler).Name[0].ToString() == "1" ? true:false);
            string servercommand = ((Button)handler).Name.Substring(1);

            if (input)
            {
                if (servercommand != "DEVCOLOR")
                {
                    Input_frm form = new Input_frm("Input", descriptions[servercommand], "Confirm", "Cancel");
                    if (servercommand != "DEVCREATETEAM" && servercommand != "DEVMSG")
                        form.InputBox.KeyDown += new KeyEventHandler(Suppress_Space);

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
                        Program.ChatServer.SendPacket("DEVPOINTS||" + servercommand + "||" + form.InputBox.Text.Trim());
                    }
                }
                else
                {
                    ColorDialog selectcolor = new ColorDialog();
                    if (selectcolor.ShowDialog() == DialogResult.OK)
                    {
                        Program.ChatServer.SendPacket("DEVPOINTS||" + servercommand + "||" + selectcolor.Color.R + "," + selectcolor.Color.G + "," + selectcolor.Color.B);
                    }

                }
            }
            else
            {
                if(MessageBox.Show("Confirm","Are you sure?",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Program.ChatServer.SendPacket("DEVPOINTS||" + servercommand);
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
            if (Program.Config.DefaultServer == "DevPro EU")
                Process.Start("http://iframe.sponsorpay.com/?appid=11433&uid=" + Program.UserInfo.Username + "&pup0=eu");
            else if (Program.Config.DefaultServer == "DevPro USA")
                Process.Start("http://iframe.sponsorpay.com/?appid=11433&uid="+Program.UserInfo.Username+"&pup0=us");
            else
                MessageBox.Show("Offers are not available on this server.");
        }
        private void DonateLink_Click(object sender, EventArgs e)
        {
            if (Program.Config.DefaultServer == "DevPro EU")
                Process.Start("https://wallapi.com/api/?key=5a7ef592b505f1e3c5cdb9d4e8614790&uid=" + Program.UserInfo.Username + "&widget=w1_1");
            else if (Program.Config.DefaultServer == "DevPro USA")
                Process.Start("https://wallapi.com/api/?key=245f31f2dbeb2c49178d3c2eee29cfa9&uid=" + Program.UserInfo.Username + "&widget=w1_1");
            else
                MessageBox.Show("Donations are not available on this server.");
        }

        private void refreshbtn_Click(object sender, EventArgs e)
        {
            Program.ChatServer.SendPacket("GETDEVPOINTS");
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
