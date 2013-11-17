using System;
namespace DevProLauncher.Network.Data
{
    [Serializable]
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Version { get; set; }
        public string UID { get; set; }
    }
}
