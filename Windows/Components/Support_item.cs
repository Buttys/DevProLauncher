using System.Drawing;
using System.Windows.Forms;

namespace DevProLauncher.Windows.Components
{
    public sealed partial class SupportItem : Form
    {
        public SupportItem(Image image,string name,string des, int cost)
        {
            InitializeComponent();
            TopLevel = false;
            //Dock = DockStyle.Fill;
            Anchor = AnchorStyles.None;
            Visible = true;
            ItemImage.Image = image;
            ItemName.Text = name;
            ItemDes.Text = des;
            ItemCost.Text = cost + " DevPoints";
        }
    }
}
