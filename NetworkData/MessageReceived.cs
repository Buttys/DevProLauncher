using System.IO;
namespace YgoServer.NetworkData
{
    public class MessageReceived
    {
        public ClientPackets Packet { get; private set; }
        public byte[] Raw { get; private set; }
        public BinaryReader Reader { get; private set; }

        public MessageReceived(ClientPackets packet, byte[] raw, BinaryReader reader)
        {
            Packet = packet;
            Raw = raw;
            Reader = reader;
        }
    }
}
