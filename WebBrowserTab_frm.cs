using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YGOPro_Launcher
{
    public partial class WebBrowserTab_frm : Form
    {
        
        public WebBrowserTab_frm()
        {

            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            Browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(Browser_DocumentCompleted);
        }
        public void Navigate(string url)
        {
            PageStatus.Text = "Loading";
            System.Threading.Thread t = new System.Threading.Thread(() =>
            {
                Browser.Navigate(url);
            });
           
            t.ApartmentState = System.Threading.ApartmentState.STA;
            t.Start();
        }
        public void LoadScript(string script)
        {
            Browser.DocumentText = script;
            PageStatus.Text = "Complete";
        }
        public bool IsLoaded()
        {
            return Browser.Url != null;
        }
        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            PageStatus.Text = "Complete";
        }


    }
}
