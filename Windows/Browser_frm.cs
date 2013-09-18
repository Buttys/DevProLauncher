using System.Windows.Forms;

namespace DevProLauncher.Windows
{
    public partial class Browser_frm : Form
    {
        public Browser_frm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            browser.ScriptErrorsSuppressed = true;
        }

        public void Navigate(string url,bool force)
        {
            if((browser.Url == null || force) && !string.IsNullOrEmpty(url))
                browser.Navigate(url);
        }
    }
}
