using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevProLauncher.Helpers;
using DevProLauncher.Helpers.Enums;

namespace DevProLauncher.Windows.Components
{
    public sealed partial class ReplayInfoControl : Form
    {

        public ReplayInfoControl()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
        }

        public void ReadReplay(string fileName)
        {
            try
            {
                var replay = new ReplayReader.YgoReplay();
                if (!replay.FromFile(fileName))
                {
                    ReplayInfo.Text = "Error opening replay.";
                    return;
                }

                string hostname = ExtractName(replay.ReadString(40));
                string clientname = ExtractName(replay.ReadString(40));

                string player3 = "";
                string player4 = "";
                if (replay.Tag)
                {
                    player3 = ExtractName(replay.ReadString(40));
                    player4 = ExtractName(replay.ReadString(40));
                }

                int startlp = 0;
                int starthand = 0;
                int drawcount = 0;
                if (!replay.Tag)
                {
                    startlp = replay.DataReader.ReadInt32();
                    starthand = replay.DataReader.ReadInt32();
                    drawcount = replay.DataReader.ReadInt32();
                }

                replay.DataReader.ReadInt32();
                if (replay.Tag)
                {
                    VSText.Text = hostname + ", " + clientname + " vs " + player3 + ", " + player4;
                }
                else
                {
                    VSText.Text = hostname + " vs " + clientname;
                }

                ReplayInfo.Text += Environment.NewLine;
                ReplayInfo.Text = "Replay Type: " + (replay.Tag ? "Tag" : "Single");
                if (!replay.Tag)
                {
                    ReplayInfo.Text += Environment.NewLine;
                    ReplayInfo.Text += "StartLP: " + startlp;
                    ReplayInfo.Text += Environment.NewLine;
                    ReplayInfo.Text += "StartHand: " + starthand;
                    ReplayInfo.Text += Environment.NewLine;
                    ReplayInfo.Text += "DrawCount: " + drawcount;
                    ReplayInfo.Text += Environment.NewLine;
                }

            }
            catch
            {
                ReplayInfo.Text = "Error reading replay.";
            }
        }

        private string ExtractName(string name)
        {
            if (name.Contains("]"))
                return name.Split(']')[1];
            return name;
        }
    }
}
