using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevProLauncher.Network.Enums;
using System.IO;

namespace DevProLauncher.Network
{
    public class MessageReceived
    {
        public DevClientPackets Packet { get; private set; }
        public byte[] Raw { get; private set; }
        public BinaryReader Reader { get; private set; }

        public MessageReceived(DevClientPackets packet, byte[] raw, BinaryReader reader)
        {
            Packet = packet;
            Raw = raw;
            Reader = reader;
        }
    }
}
