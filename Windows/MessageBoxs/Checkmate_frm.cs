﻿using System.Windows.Forms;

namespace DevProLauncher.Windows.MessageBoxs
{
    public partial class Checkmate_frm : Form
    {
        public Checkmate_frm(string username, string password)
        {
            InitializeComponent();

            label1.Text = "- To register an account simply type a username and password! \n" +
                          "- To play without registering you can simply leave the password field blank. \n";

            foreach (var server in Program.CheckmateServerList)
                ServerSelect.Items.Add(server.Value.serverName);

            ServerSelect.SelectedIndex = 0;
            Username.Text = username;
            Password.Text = password;
            ApplyTranslation();
        }
        private void ApplyTranslation()
        {
            DevProLauncher.Config.LanguageInfo info = Program.LanguageManager.Translation;

            label1.Text = info.checkmateInfo;
            label2.Text = info.checkmateUser;
            label3.Text = info.checkmatePw;
            Playbtn.Text = info.checkmateBtn;
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
