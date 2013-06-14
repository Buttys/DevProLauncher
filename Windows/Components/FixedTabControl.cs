using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace DevProLauncher.Windows.Components
{
    class FixedTabControl : TabControl
    {

        public delegate bool PreRemoveTab(int indx);
        public PreRemoveTab PreRemoveTabPage;

        public FixedTabControl()
            : base()
        {
            PreRemoveTabPage = null;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            SetWindowTheme(this.Handle, "", "");
            base.OnHandleCreated(e);
        }

        [DllImportAttribute("uxtheme.dll")]
        private static extern int SetWindowTheme(IntPtr hWnd, string appname, string idlist);


        private void CloseTab(int i)
        {
            ChatWindow window = (ChatWindow)this.TabPages[i];
            if (window.Name == "DevPro")
                return;

            if (PreRemoveTabPage != null)
            {
                bool closeIt = PreRemoveTabPage(i);
                if (!closeIt)
                    return;
            }
            TabPages.Remove(TabPages[i]);
        }

    }
}
