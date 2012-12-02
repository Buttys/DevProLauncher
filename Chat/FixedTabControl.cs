using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace YGOPro_Launcher.Chat
{
    class FixedTabControl : TabControl
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            SetWindowTheme(this.Handle, "", "");
            base.OnHandleCreated(e);
        }

        [DllImportAttribute("uxtheme.dll")]
        private static extern int SetWindowTheme(IntPtr hWnd, string appname, string idlist);

    }
}
