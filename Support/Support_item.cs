using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YGOPro_Launcher.Support
{
    public partial class Support_item : Form
    {
        public Support_item(Image image,string name,string des, int cost)
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            ItemImage.Image = image;
            ItemName.Text = name;
            ItemDes.Text = des;
            ItemCost.Text = cost + " DevPoints";
        }
    }
}
