using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace YGOPro_Launcher
{
    public static class FormStyler
    {
        public static void ApplyStlye(Form form,StyleInfo info)
        {

            foreach (Control control in form.Controls)
            {
                if (control is TableLayoutPanel)
                {
                    foreach (Control secondarycontrol in ((TableLayoutPanel)control).Controls)
                    {
                        SetColorStyle(secondarycontrol,info);
                    }
                }
            }

            form.BackColor = info.FormColor;

        }

        static void SetColorStyle(Control control, StyleInfo info)
        {
            if (control is TableLayoutPanel)
            {
                foreach (Control othercontrol in ((TableLayoutPanel)control).Controls)
                {
                    SetColorStyle(othercontrol,info);
                }
            }
            else if (control is FlowLayoutPanel)
            {
                foreach (Control othercontrol in ((FlowLayoutPanel)control).Controls)
                {
                    SetColorStyle(othercontrol,info);
                }
            }
            else if (control is Label)
            {
                //apply changes to control here
                Label text = (Label)control;
                text.ForeColor = info.LabelTextColor;
                text.BackColor = info.LabelBackColor;
            }
            else if (control is Button)
            {

            }
            else if (control is CheckBox)
            {
                CheckBox checkbox = (CheckBox)control;
                checkbox.ForeColor = info.FormTextColor;
                checkbox.BackColor = info.FormColor;
            }
        }

    }
}
