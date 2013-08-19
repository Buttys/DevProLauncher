using System;

namespace DevProLauncher.Network.Data
{
    [Serializable]
    public class LoginData
    {
        public int LoginKey { get; set; }
        public int UserRank { get; set; }
        public string Team { get; set; }
        public int TeamRank { get; set; }
        public string Username { get; set; }
    }
}
