using System;
using System.Windows.Forms;

namespace DevProLauncher.Windows.MessageBoxs
{
    public partial class ErrorReportFrm : Form
    {
        public ErrorReportFrm(Exception error)
        {
            InitializeComponent();
            label1.Text = Program.LanguageManager.Translation.pMsbException;
            ErrorReport.Text += error;
        }
    }
}
