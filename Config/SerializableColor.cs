using System.Drawing;

namespace DevProLauncher.Config
{
    public struct SerializableColor
    {
        public int A;
        public int R;
        public int G;
        public int B;

        public SerializableColor(Color color)
        {
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
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
