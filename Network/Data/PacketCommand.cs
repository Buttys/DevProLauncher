using System;
namespace DevProLauncher.Network.Data
{
    [Serializable]
    public class PacketCommand
    {
        public string Command { get; set; }
        public string Data { get; set; }
    }
}
