using System.Windows.Forms;

namespace DevProLauncher.Windows
{
    public partial class Browser_frm : Form
    {
        public Browser_frm()
        {
            InitializeComponent();
            browserWb.ScriptErrorsSuppressed = true;
            FormBorderStyle = FormBorderStyle.None;
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
        }

        public Browser_frm(string URL)
        {        
            InitializeComponent();
            browserWb.ScriptErrorsSuppressed = true;
            Navigate(URL, false);
            
        }

        public void Navigate(string url,bool force)
        {
            if (!string.IsNullOrEmpty(url) &&  browserWb.Url == null || force)
                browserWb.Navigate(url);
        }
    }
}
