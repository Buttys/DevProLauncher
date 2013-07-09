using System;
using System.Windows.Forms;

namespace DevProLauncher.Windows.MessageBoxs
{
    public partial class InputFrm : Form
    {
        public InputFrm(string title, string message, string confirmbtn, string cancelbtn)
        {
            InitializeComponent();
            Text = title;
            msglabel.Text = message;
            Confirmbtn.Text = confirmbtn;
            Cancelbtn.Text = cancelbtn;
            InputBox.KeyPress += KeyPress_Enter;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void Confirmbtn_Click(object sender, EventArgs e)
        {
            if (InputBox.Text == "") MessageBox.Show("Input box is empty.", "Error", MessageBoxButtons.OK);
            else
            DialogResult = DialogResult.OK;
        }
        private void KeyPress_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (InputBox.Text == "")
                    return;

                DialogResult = DialogResult.OK;
            }
        }
    }
}
