using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace YGOPro_Launcher.Chat
{
    class FixedTabControl : TabControl
    {

        public delegate bool PreRemoveTab(int indx);
        public PreRemoveTab PreRemoveTabPage;

        public FixedTabControl()
            :base()
        {
            PreRemoveTabPage = null;
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            SetWindowTheme(this.Handle, "", "");
            base.OnHandleCreated(e);
        }

        [DllImportAttribute("uxtheme.dll")]
        private static extern int SetWindowTheme(IntPtr hWnd, string appname, string idlist);

        protected override void OnDrawItem(DrawItemEventArgs e)
        {


            string titel = this.TabPages[e.Index].Text;
            Font f = this.Font;
            Rectangle r = e.Bounds;            
            Brush b = new SolidBrush(Color.Black);
            Pen p = new Pen(b);
            r = GetTabRect(e.Index);
            r.Offset(2, 2);
            if (this.TabPages[e.Index].Name == "DevPro")
            {
                base.OnDrawItem(e);
                e.Graphics.DrawString(titel, f, b, new PointF(r.X, r.Y));
                return;
            }
            r.Width = 5;
            r.Height = 5;

            e.Graphics.DrawLine(p, r.X, r.Y + 4, r.X + r.Width, r.Y + r.Height + 4);
            e.Graphics.DrawLine(p, r.X + r.Width, r.Y + 4, r.X, r.Y + r.Height + 4);
            e.Graphics.DrawString(titel, f, b, new PointF(r.X + 5, r.Y));
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            Point p = e.Location;
            for (int i = 0; i < TabCount; i++)
            {
                Rectangle r = GetTabRect(i);
                r.Offset(2, 2);
                r.Width = 5;
                r.Height = 5;
                r.Height += 4;
                r.Y += 4;
                if (r.Contains(p))
                {
                    CloseTab(i);
                }
            }
        }

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
