namespace DevProLauncher.Network.Data
{
    public class ServerInfo
    {
        public string serverName { get; private set; }
        public string serverAddress { get; private set; }
        public int serverPort { get; private set; }

        public ServerInfo(string name, string address, int port)
        {
            serverName = name;
            serverAddress = address;
            serverPort = port;
        }
    }
}
