using System;
using System.Drawing;

namespace DevProLauncher.Network.Data
{
    [Serializable]
    public class UserData
    {
        public string username { get; set; }
        public int rank { get; set; }
        public int a { get; set; }
        public int r { get; set; }
        public int g{ get; set; }
        public int b { get; set; }
        public string team { get; set; }
        public int teamRank { get; set; }
        public bool Online { get; set; }

        public Color getUserColor() { return Color.FromArgb(a, r, g, b); }
    }
}
