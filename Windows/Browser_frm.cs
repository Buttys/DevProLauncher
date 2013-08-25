using System.Windows.Forms;

namespace DevProLauncher.Windows
{
    public partial class Browser_frm : Form
    {
        string url;

        public Browser_frm(string URL)
        {

            url = URL;
            
            InitializeComponent();
            Init();
            
        }

        private void Init()
        {
            if(!string.IsNullOrEmpty(url))
                browserWb.Navigate(url);
        }
    }
}
