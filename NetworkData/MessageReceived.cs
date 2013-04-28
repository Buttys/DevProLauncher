using System.IO;
namespace YgoServer.NetworkData
{
    public class MessageReceived
    {
        public byte[] Raw { get; private set; }
        public BinaryReader Reader { get; private set; }

        public MessageReceived(byte[] raw, BinaryReader reader)
        {
            Raw = raw;
            Reader = reader;
        }
    }
}
