using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevProLauncher.Helpers;
using DevProLauncher.Helpers.Enums;

namespace DevProLauncher.Windows.Components
{
    public partial class ReplayInfoControl : Form
    {
        public Dictionary<string, CardInfos> CardList = new Dictionary<string,CardInfos>();

        public ReplayInfoControl()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            DeckList.DrawItem += new DrawItemEventHandler(DeckList_DrawItem);
        }
        public void ReadReplay(string fileName)
        {
            try
            {
                ReplayReader.YgoReplay replay = new ReplayReader.YgoReplay();
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

                int opt = replay.DataReader.ReadInt32();
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

                if (!replay.Tag)
                {
                    bool playerfound = false;
                    string[] players = null;
                    if (!replay.Tag)
                        players = new string[] { hostname, clientname };
                    else
                        players = new string[] { hostname, clientname, player3, player4 };

                    foreach (string player in players)
                    {
                        List<string> cardnumbers = new List<string>();
                        cardnumbers.Add("#main");
                        int count = replay.DataReader.ReadInt32();
                        for (int i = 0; i < count; ++i)
                        {
                            cardnumbers.Add(replay.DataReader.ReadInt32().ToString());
                        }

                        count = replay.DataReader.ReadInt32();
                        cardnumbers.Add("#extra");
                        for (int i = 0; i < count; ++i)
                        {
                            cardnumbers.Add(replay.DataReader.ReadInt32().ToString());
                        }
                        if (player == Program.UserInfo.username)
                        {
                            playerfound = true;
                            DeckList.Items.Clear();
                            CardList.Clear();
                            foreach (string cardnumber in cardnumbers)
                            {
                                if (cardnumber.StartsWith("#"))
                                {
                                    if (cardnumber.Contains("main"))
                                    {
                                        DeckList.Items.Add("--Main--");
                                    }
                                    if (cardnumber.Contains("extra"))
                                    {
                                        DeckList.Items.Add("--Extra--");
                                    }
                                    continue;
                                }
                                CardInfos card = LauncherHelper.CardManager.FromId(Int32.Parse(cardnumber));
                                if (card == null) continue;
                                if (CardList.ContainsKey(card.Name))
                                {
                                    CardList[card.Name].Amount++;
                                }
                                else
                                {
                                    CardList.Add(card.Name, (CardInfos)card.Clone());
                                    DeckList.Items.Add(card.Name);
                                    CardList[card.Name].Amount++;
                                }
                            }
                        }
                    }

                    if (!playerfound)
                    {
                        CardList.Clear();
                        DeckList.Items.Clear();
                    }
                }
                else
                {
                    CardList.Clear();
                    DeckList.Items.Clear();
                }
            }
            catch
            {
                DeckList.Items.Clear();
                CardList.Clear();
                ReplayInfo.Text = "Error reading replay.";
            }
        }

        private string ExtractName(string name)
        {
            if (name.Contains("]"))
                return name.Split(']')[1];
            else
                return name;
        }

        private void DeckList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < DeckList.Items.Count)
            {
                string text = DeckList.Items[index].ToString();
                Graphics g = e.Graphics;
                if (!CardList.ContainsKey(text))
                {
                    g.FillRectangle(new SolidBrush(Color.White), e.Bounds);
                    g.DrawString(text, e.Font, (selected) ? Brushes.Blue : Brushes.Black,
                         DeckList.GetItemRectangle(index).Location);
                    e.DrawFocusRectangle();
                    return;
                }

                CardInfos card = CardList[text];

                Color itemcolor =
                (card.HasType(CardType.Trap) ? Color.Violet :
                (card.HasType(CardType.Spell) ? Color.LawnGreen :
                (card.HasType(CardType.Synchro) ? Color.White :
                (card.HasType(CardType.Xyz) ? Color.Gray :
                (card.HasType(CardType.Ritual) ? Color.CornflowerBlue :
                (card.HasType(CardType.Fusion) ? Color.MediumPurple :
                (card.HasType(CardType.Effect) ? Color.Orange :
                (card.HasType(CardType.Normal) ? Color.Yellow :

                Color.Red))))))));

                g.FillRectangle(new SolidBrush(itemcolor), e.Bounds);

                // Print text
                g.DrawString(text + " x" + card.Amount, e.Font, (selected) ? Brushes.Blue : Brushes.Black,
                    DeckList.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

    }
}
