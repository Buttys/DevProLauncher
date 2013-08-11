using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
