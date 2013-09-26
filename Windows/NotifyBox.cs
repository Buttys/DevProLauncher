using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevProLauncher.Windows
{
    public partial class NotifyBox : Form
    {

        Point pt = new Point();
        Timer animation = new Timer();
        Timer fadeout = new Timer();
        Timer duration = new Timer();
        Screen DisplayScreen;
        int lifetime;
           
        public NotifyBox()
        {
            this.Hide();

            InitializeComponent();
            DisplayScreen = Screen.FromControl(Panel);
            beginStartAnimation();

            this.Show();

            animation.Tick += new EventHandler(animation_Tick);
            animation.Interval = 3;
            animation.Start();

            fadeout.Tick += new EventHandler(fadeout_Tick);
            fadeout.Interval = 5;

            duration.Tick += new EventHandler(duration_Tick);
            duration.Interval = 10;

        }

        void duration_Tick(object sender, EventArgs e)
        {
            if (lifetime >= 250)
            {
                duration.Stop();
                fadeout.Start();
            }
            else
            {
                lifetime++;
            }
        }

        void fadeout_Tick(object sender, EventArgs e)
        {
            if (this.Location.Y < DisplayScreen.Bounds.Height)
            {
                Point ptSt = new Point(this.Location.X, this.Location.Y);
                ptSt.Y += 2;

                this.Location = ptSt;
            }
            else
            {
                fadeout.Stop();
                Close();
            }
        }

        void animation_Tick(object sender, EventArgs e)
        {

            if (this.Location.Y >= DisplayScreen.Bounds.Height - this.Size.Height - 50)
            {
                Point ptSt = new Point(this.Location.X, this.Location.Y);
                ptSt.Y -= 2;

                this.Location = ptSt;
            }
            else
            {
                animation.Stop();
                duration.Start();
            }

        }
        
        private void beginStartAnimation()
        {
            
            Point startLoc = new Point((DisplayScreen.Bounds.Width - this.Size.Width), DisplayScreen.Bounds.Height);
            this.Location = startLoc;
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = Control.MousePosition;
            
            mousePos.X -= pt.X;
            mousePos.Y -= pt.Y;
           
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Location = mousePos;
            }
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            pt = e.Location;
        }

        private void Panel_DoubleClick(object sender, EventArgs e)
        {
            fadeout.Start();
        }

        public void setMsg(string Msg)
        {
            lbMsg.Text = Msg;
        }

    }
}
