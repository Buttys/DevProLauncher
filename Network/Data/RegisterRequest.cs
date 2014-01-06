namespace DevProLauncher.Network.Data
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Version { get; set; }
        public string UID { get; set; }
        public string Email { get; set; }
    }
}
