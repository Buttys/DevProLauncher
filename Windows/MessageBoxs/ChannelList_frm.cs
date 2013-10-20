using System;
using System.Drawing;
using System.Windows.Forms;
using DevProLauncher.Network.Enums;
using DevProLauncher.Network.Data;

namespace DevProLauncher.Windows.MessageBoxs
{
    public partial class ChannelListFrm : Form
    {
        public ChannelListFrm()
        {
            InitializeComponent();
            FormClosed += RemoveEvents;
            ChannelList.DrawItem += DrawList_Channels;
            Program.ChatServer.ChannelRequest += GenerateChannelList;
            Program.ChatServer.SendPacket(DevServerPackets.ChannelList);
        }

        public void GenerateChannelList(ChannelData[] channels)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<ChannelData[]>(GenerateChannelList), (object)channels);
                return;
            }

            foreach (ChannelData channel in channels)
            {
                ChannelList.Items.Add(channel);
            }
        }
        public void RemoveEvents(object sender, EventArgs e)
        {
            FormClosed -= RemoveEvents;
// ReSharper disable DelegateSubtraction
            if (Program.ChatServer.ChannelRequest != null) Program.ChatServer.ChannelRequest -= GenerateChannelList;
// ReSharper restore DelegateSubtraction
        }

        private void DrawList_Channels(object sender, DrawItemEventArgs e)
        {
            var list = (ListBox)sender;
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < list.Items.Count)
            {
                var channel = (ChannelData)list.Items[index];
                bool defualt = channel.name == Program.Config.DefaultChannel;
                Graphics g = e.Graphics;

                g.FillRectangle((selected) ? (Program.Config.ColorBlindMode ? new SolidBrush(Color.Black) : new SolidBrush(Color.Blue)) : new SolidBrush(Program.Config.ChatBGColor.ToColor()), e.Bounds);

                //// Print text
                g.DrawString(channel.name + " (" + channel.userCount + ")" + (defualt ? " (Default)" : ""), e.Font,
                    (selected) ? Brushes.White : Brushes.Black ,
                    list.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        private void JoinBtn_Click(object sender, EventArgs e)
        {
            if (ChannelList.SelectedIndex == -1)
                return;

            Program.ChatServer.SendPacket(DevServerPackets.JoinChannel, ((ChannelData)ChannelList.SelectedItem).name);
            DialogResult = DialogResult.OK;
        }

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            var input = new InputFrm("Create Channel", "Enter Channel Name", "Create", "Cancel");
            if (input.ShowDialog() == DialogResult.OK)
            {
                Program.ChatServer.SendPacket(DevServerPackets.JoinChannel, input.InputBox.Text.Trim());
                DialogResult = DialogResult.OK;
            }
        }

        private void DefaultBtn_Click(object sender, EventArgs e)
        {
            if (ChannelList.SelectedIndex == -1)
                return;

            ChannelData channel = (ChannelData)ChannelList.SelectedItem;

            if (Program.Config.DefaultChannel == channel.name)
                Program.Config.DefaultChannel = string.Empty;
            else
                Program.Config.DefaultChannel = channel.name;
            ChannelList.Refresh();
            Program.SaveConfig(Program.ConfigurationFilename,Program.Config);
        }
    }
}
