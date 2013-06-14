using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevProLauncher.Windows
{
    public partial class FileManager_frm : Form
    {
        public FileManager_frm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;

            FileManagerControl deckcontrol = new FileManagerControl("Decks", "deck/", ".ydk");
            TabPage decktab = new TabPage("Decks");
            decktab.Controls.Add(deckcontrol);
            fileTabs.TabPages.Add(decktab);

            FileManagerControl replaycontrol = new FileManagerControl("Replays", "replay/", ".yrp");
            TabPage replaytab = new TabPage("Replays");
            replaytab.Controls.Add(replaycontrol);
            fileTabs.TabPages.Add(replaytab);
        }
    }
}
