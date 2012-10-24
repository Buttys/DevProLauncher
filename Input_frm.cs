using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YGOPro_Launcher
{
    public partial class Input_frm : Form
    {
        public Input_frm(string title, string message, string confirmbtn, string cancelbtn)
        {
            this.InitializeComponent();
            this.Text = title;
            this.msglabel.Text = message;
            this.Confirmbtn.Text = confirmbtn;
            this.Cancelbtn.Text = cancelbtn;
        }

        private void Confirmbtn_Click(object sender, EventArgs e)
        {

            if (InputBox.Text == "") MessageBox.Show("Input box is empty.", "Error", MessageBoxButtons.OK);
            else
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
