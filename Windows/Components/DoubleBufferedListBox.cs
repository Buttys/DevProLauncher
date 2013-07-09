using System.Windows.Forms;
using System.Drawing;
using System;
namespace DevProLauncher.Windows.Components
{
    
    public sealed class DoubleBufferedListBox : ListBox
    {
        public DoubleBufferedListBox()
        {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);
            DrawMode = DrawMode.OwnerDrawFixed;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var iRegion = new Region(e.ClipRectangle);
            e.Graphics.FillRegion(new SolidBrush(BackColor), iRegion);
            if (Items.Count > 0)
            {
                for (int i = 0; i < Items.Count; ++i)
                {
                    Rectangle irect = GetItemRectangle(i);
                    if (e.ClipRectangle.IntersectsWith(irect))
                    {
                        if ((SelectionMode == SelectionMode.One && SelectedIndex == i)
                                    || (SelectionMode == SelectionMode.MultiSimple && SelectedIndices.Contains(i))
                                    || (SelectionMode == SelectionMode.MultiExtended && SelectedIndices.Contains(i)))
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, Font,
                                irect, i,
                                DrawItemState.Selected, ForeColor,
                                BackColor));
                        }
                        else
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, Font,
                                irect, i,
                                DrawItemState.Default, ForeColor,
                                BackColor));
                        }
                        iRegion.Complement(irect);
                    }
                }
            }
            base.OnPaint(e);
        }

        public void UpdateList()
	    {            
		    int i = 0;
		    BeginUpdate();
		    foreach (String item in Items)
		    {
		        if (Items.Count > i)
		            Items[i] = item;
			    else 
				   Items.Add(item);
				i++;
		    }
            while (Items.Count > i)
                Items.Remove(i);

            EndUpdate();
		}

     }

    }