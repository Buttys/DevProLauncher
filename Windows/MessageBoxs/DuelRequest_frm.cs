using System.Windows.Forms;

namespace DevProLauncher.Windows.MessageBoxs
{
    public partial class DuelRequestFrm : Form
    {
        public DuelRequestFrm(string requesttext)
        {
            InitializeComponent();
            RequestText.Text = requesttext;
        }
    }
}
