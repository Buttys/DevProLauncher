using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace YGOPro_Launcher.Chat
{
    public class CustomRTB : RichTextBox
    {
        public event EventHandler ScrolledToBottom;

        private const int WM_VSCROLL = 0x115;
        private const int WM_MOUSEWHEEL = 0x20A;
        private const int WM_USER = 0x400;
        private const int SB_VERT = 1;
        private const int EM_SETSCROLLPOS = WM_USER + 222;
        private const int EM_GETSCROLLPOS = WM_USER + 221;

        [DllImport("user32.dll")]
        private static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, Int32 wMsg, Int32 wParam, ref Point lParam);

        public bool IsAtMaxScroll()
        {
            int minScroll;
            int maxScroll;
            GetScrollRange(this.Handle, SB_VERT, out minScroll, out maxScroll);
            Point rtfPoint = Point.Empty;
            SendMessage(this.Handle, EM_GETSCROLLPOS, 0, ref rtfPoint);

            int safezone = maxScroll / 100 * 10;

            return (rtfPoint.Y + this.ClientSize.Height >= maxScroll - safezone);
        }
        protected virtual void OnScrolledToBottom(EventArgs e)
        {
            if (ScrolledToBottom != null)
                ScrolledToBottom(this, e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (IsAtMaxScroll())
                OnScrolledToBottom(EventArgs.Empty);

            base.OnKeyUp(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_VSCROLL || m.Msg == WM_MOUSEWHEEL)
            {
                if (IsAtMaxScroll())
                    OnScrolledToBottom(EventArgs.Empty);
            }

            base.WndProc(ref m);
        }

    }
}