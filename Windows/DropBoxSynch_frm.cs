using System;
using System.Windows.Forms;
using DevProLauncher.Controller;

namespace DevProLauncher.Windows
{
    public partial class DropBoxSynch_frm : Form
    {
        public DropBoxSynch_frm()
        {
            InitializeComponent();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {

            DropBoxController.syncAcc(false);

            DropBoxController.filesync();

        }

      

    }
}
