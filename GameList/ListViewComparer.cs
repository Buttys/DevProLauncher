using System;
using System.Windows.Forms;
using System.Collections;
public class ListViewItemComparer : IComparer
{
    private int column;
    private bool numeric = false;

    public int Column
    {
        get { return column; }
        set { column = value; }
    }

    public bool Numeric
    {
        get { return numeric; }
        set { numeric = value; }
    }

    public ListViewItemComparer(int columnIndex)
    {
        Column = columnIndex;
    }

    public int Compare(object x, object y)
    {
        ListViewItem itemX = x as ListViewItem;
        ListViewItem itemY = y as ListViewItem;

        if (itemX == null && itemY == null)
            return 0;
        else if (itemX == null)
            return -1;
        else if (itemY == null)
            return 1;

        if (itemX == itemY) return 0;

        if (Numeric)
        {
            decimal itemXVal, itemYVal;

            if (!Decimal.TryParse(itemX.SubItems[Column].Text, out itemXVal))
            {
                itemXVal = 0;
            }
            if (!Decimal.TryParse(itemY.SubItems[Column].Text, out itemYVal))
            {
                itemYVal = 0;
            }

            return Decimal.Compare(itemXVal, itemYVal);
        }
        else
        {
            string itemXText = itemX.SubItems[Column].Text;
            string itemYText = itemY.SubItems[Column].Text;

            return String.Compare(itemXText, itemYText);
        }
    }
}