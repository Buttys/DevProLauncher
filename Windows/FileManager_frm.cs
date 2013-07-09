using System.Windows.Forms;
using DevProLauncher.Windows.Components;

namespace DevProLauncher.Windows
{
    public sealed partial class FileManagerFrm : Form
    {
        private readonly FileManagerControl m_deckTab;
        private readonly FileManagerControl m_replayTab;
        public FileManagerFrm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;

            m_deckTab = new FileManagerControl("Decks", "deck/", ".ydk");
            var decktab = new TabPage("Decks");
            decktab.Controls.Add(m_deckTab);
            fileTabs.TabPages.Add(decktab);

            m_replayTab = new FileManagerControl("Replays", "replay/", ".yrp");
            var replaytab = new TabPage("Replays");
            replaytab.Controls.Add(m_replayTab);
            fileTabs.TabPages.Add(replaytab);
        }

        public void ApplyTranslations()
        {
            m_deckTab.ApplyTranslation();
            m_replayTab.ApplyTranslation();
        }
    }
}
