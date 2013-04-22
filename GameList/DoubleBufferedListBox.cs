using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System;
namespace YGOPro_Launcher
{
    
    public class DoubleBufferedListBox : ListBox
    {
        public DoubleBufferedListBox()
        {
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Region iRegion = new Region(e.ClipRectangle);
            e.Graphics.FillRegion(new SolidBrush(this.BackColor), iRegion);
            if (this.Items.Count > 0)
            {
                for (int i = 0; i < this.Items.Count; ++i)
                {
                    System.Drawing.Rectangle irect = this.GetItemRectangle(i);
                    if (e.ClipRectangle.IntersectsWith(irect))
                    {
                        if ((this.SelectionMode == SelectionMode.One && this.SelectedIndex == i)
                                    || (this.SelectionMode == SelectionMode.MultiSimple && this.SelectedIndices.Contains(i))
                                    || (this.SelectionMode == SelectionMode.MultiExtended && this.SelectedIndices.Contains(i)))
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                                irect, i,
                                DrawItemState.Selected, this.ForeColor,
                                this.BackColor));
                        }
                        else
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                                irect, i,
                                DrawItemState.Default, this.ForeColor,
                                this.BackColor));
                        }
                        iRegion.Complement(irect);
                    }
                }
            }
            base.OnPaint(e);
        }

    }
}