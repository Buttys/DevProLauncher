using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevProLauncher.Network.Data
{
    public class ServerInfo
    {
        public string serverName { get; private set; }
        public string serverAddress { get; private set; }
        public int serverPort { get; private set; }

        public ServerInfo(string name, string address, int port)
        {
            this.serverName = name;
            this.serverAddress = address;
            this.serverPort = port;
        }
    }
}
