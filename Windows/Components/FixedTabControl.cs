using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DevProLauncher.Windows.Components
{
    class FixedTabControl : TabControl
    {

        public delegate bool PreRemoveTab(int indx);
        public PreRemoveTab PreRemoveTabPage;

        public FixedTabControl()
        {
            PreRemoveTabPage = null;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            SetWindowTheme(Handle, "", "");
            base.OnHandleCreated(e);
        }

        [DllImportAttribute("uxtheme.dll")]
        private static extern int SetWindowTheme(IntPtr hWnd, string appname, string idlist);
    }
}
