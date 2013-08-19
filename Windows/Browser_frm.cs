using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevProLauncher.Windows
{
    public partial class Browser_frm : Form
    {
        string url = null;

        public Browser_frm(string URL)
        {

            url = URL;
            
            InitializeComponent();
            Init();
            
        }

        private void Init()
        {
            browserWb.Navigate(url);
        }
    }
}
