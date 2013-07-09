using System.Windows.Forms;
using DevProLauncher.Windows.Components;

namespace DevProLauncher.Windows
{
    public sealed partial class FileManagerFrm : Form
    {
        public FileManagerFrm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;

            var deckcontrol = new FileManagerControl("Decks", "deck/", ".ydk");
            var decktab = new TabPage("Decks");
            decktab.Controls.Add(deckcontrol);
            fileTabs.TabPages.Add(decktab);

            var replaycontrol = new FileManagerControl("Replays", "replay/", ".yrp");
            var replaytab = new TabPage("Replays");
            replaytab.Controls.Add(replaycontrol);
            fileTabs.TabPages.Add(replaytab);
        }
    }
}
