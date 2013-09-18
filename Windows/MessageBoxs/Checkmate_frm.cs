using System.Windows.Forms;

namespace DevProLauncher.Windows.MessageBoxs
{
    public partial class Checkmate_frm : Form
    {
        public Checkmate_frm(string username, string password)
        {
            InitializeComponent();

            label1.Text = "- To register an account simply type a username and password! \n" +
                          "- To play without registering you can simply leave the password field blank. \n" +
                          "- Anime related cards may not work!";
            Username.Text = username;
            Password.Text = password;
        }

        private void Playbtn_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(Username.Text))
            {
                MessageBox.Show("Insert Username");
                return;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
