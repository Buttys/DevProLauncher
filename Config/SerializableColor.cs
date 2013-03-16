using System;
using System.Drawing;

namespace YGOPro_Launcher.Config
{
    public struct SerializableColor
    {
        public int A;
        public int R;
        public int G;
        public int B;

        public SerializableColor(Color color)
        {
            this.A = color.A;
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }

        public static SerializableColor FromColor(Color color)
        {
            return new SerializableColor(color);
        }

        public Color ToColor()
        {
            return Color.FromArgb(A, R, G, B);
        }
    }
}
