using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevProLauncher.Windows.MessageBoxs
{
    public partial class DuelRequest_frm : Form
    {
        public DuelRequest_frm(string requesttext)
        {
            InitializeComponent();
            RequestText.Text = requesttext;
        }
    }
}
