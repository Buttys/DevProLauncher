using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGOPro_Launcher.CardDatabase;
using YGOPro_Launcher.CardEnums;
using System.IO;

namespace YGOPro_Launcher
{
    public partial class CardInfoControl : Form
    {

        private CardsManager Manager;
        private Dictionary<string, string> CardList;

        public CardInfoControl()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            Manager = new CardsManager();
            CardList = new Dictionary<string, string>();
            Manager.Init();
           
            DeckList.DrawItem +=new DrawItemEventHandler(DeckList_DrawItem);
            DeckList.SelectedIndexChanged += new EventHandler(DeckList_SelectedIndexChanged);

        }

        public void LoadDeck(string path)
        {
            DeckList.Items.Clear();
            CardList.Clear();
            ReadDeck(path);

        }

        private void ReadDeck(string deck)
        {
                var lines = File.ReadAllLines(deck);

                foreach (string nonTrimmerLine in lines)
                {
                    string line = nonTrimmerLine.Trim();
                    if (line.Equals(string.Empty)) continue;
                    
                    if (line.StartsWith("#"))
                    {
                        if (line.Contains("main"))
                        {
                            DeckList.Items.Add("--Main--");
                        }
                        if (line.Contains("extra"))
                        {
                            DeckList.Items.Add("--Extra--");
                        }
                        continue;
                    }
                    if (line.StartsWith("!"))
                    {
                        DeckList.Items.Add("--Side--");
                        continue;
                    }

                    CardInfos card = Manager.FromId(Int32.Parse(line));
                    if (card == null) continue;
                    DeckList.Items.Add(card.Name);
                    if (!CardList.ContainsKey(card.Name))
                        CardList.Add(card.Name, line);

                //    item.BackColor = ((CardType)card.Type == CardType.Normal ? Color.Yellow :
                //((CardType)card.Type == CardType.Effect ? Color.Orange :
                //((CardType)card.Type == CardType.Fusion ? Color.Violet :
                //((CardType)card.Type == CardType.Synchro ? Color.White :
                //((CardType)card.Type == CardType.Xyz ? Color.Gray :
                //((CardType)card.Type == CardType.Ritual ? Color.LightBlue :
                //Color.Red))))));
                }
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

                CardInfos card = Manager.FromId(Int32.Parse(CardList[text]));
                
                Color itemcolor = 
                (card.HasType(CardType.Synchro) ? Color.White :
                (card.HasType(CardType.Xyz) ? Color.Gray :
                (card.HasType(CardType.Ritual) ? Color.CornflowerBlue :
                (card.HasType(CardType.Fusion) ? Color.MediumPurple :
                (card.HasType(CardType.Effect) ? Color.Orange :
                (card.HasType(CardType.Normal) ? Color.Yellow :
                (card.HasType(CardType.Trap) ? Color.Violet :
                (card.HasType(CardType.Spell) ? Color.LawnGreen :
                Color.Red))))))));

                g.FillRectangle(new SolidBrush(itemcolor), e.Bounds);

                // Print text
                g.DrawString(text, e.Font, (selected) ? Brushes.Blue : Brushes.Black,
                    DeckList.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        private void DeckList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CardList.ContainsKey(DeckList.SelectedItem.ToString()))
            {
                CardName.Text = "";
                CardID.Text = "";
                CardDetails.Text = "";
                return;
            }
            CardInfos card = Manager.FromId(Int32.Parse(CardList[DeckList.SelectedItem.ToString()]));
            if (card == null) return;
            CardName.Text = card.Name;
            CardID.Text = card.Id.ToString();
            
            CardDetails.Text = "";
            CardDetails.Text += card.GetCardTypes() + " ";

            if (card.HasType(CardType.Monster))
            {
                string level = string.Empty;
                for (int i = 0; i < card.Level; i++)
                {
                    level = level + "*";
                }
                CardDetails.Text += card.GetCardRace() + Environment.NewLine +
                    (card.HasType(CardType.Xyz) ? "Rank " : "Level ") + card.Level + " " +
                    "[" + level + "] " + ((card.Atk == -2) ? "?" : 
                     card.Atk.ToString()) + "/" + ((card.Def == -2) ? "?" : card.Def.ToString()) + Environment.NewLine;
            }
            else
            {
                CardDetails.Text += Environment.NewLine;
            }

            CardDetails.Text += card.Description;
        }
    }
}
