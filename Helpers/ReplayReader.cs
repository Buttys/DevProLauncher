using Decoder = SevenZip.Compression.LZMA.Decoder;
using System.IO;
using System;
using System.Text;

namespace DevProLauncher.Helpers
{
    class ReplayReader
    {
        public class YgoReplay
        {
            public struct ReplayHeader
            {
                public uint Id;
                public uint Version;
                public uint Flag;
                public uint Seed;
                public uint DataSize;
                public uint Hash;
                public byte[] Props;
            }

            public const int ReplaySize = 32;

            public ReplayHeader Header;
            private byte[] m_fileContent;
            private byte[] m_data;

            public BinaryReader DataReader;

            public bool Compressed
            {
                get { return (Header.Flag & 0x1) != 0; }
            }

            public bool Tag
            {
                get { return (Header.Flag & 0x2) != 0; }
            }

            public bool FromFile(string fileName)
            {
                try
                {
                    m_fileContent = File.ReadAllBytes(fileName);
                    BinaryReader reader = new BinaryReader(new MemoryStream(m_fileContent));
                    HandleHeader(reader);
                    HandleData(reader);
                    reader.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

            public string ReadString(int length)
            {
                string value = Encoding.Unicode.GetString(DataReader.ReadBytes(length));
                return value.Substring(0, value.IndexOf("\0"));
            }

            private void HandleHeader(BinaryReader reader)
            {
                Header.Id = reader.ReadUInt32();
                Header.Version = reader.ReadUInt32();
                Header.Flag = reader.ReadUInt32();
                Header.Seed = reader.ReadUInt32();
                Header.DataSize = reader.ReadUInt32();
                Header.Hash = reader.ReadUInt32();
                Header.Props = reader.ReadBytes(8);
            }

            private void HandleData(BinaryReader reader)
            {
                int compressedSize = m_fileContent.Length - ReplaySize;
                if (!Compressed)
                {
                    m_data = reader.ReadBytes(compressedSize);
                    DataReader = new BinaryReader(new MemoryStream(m_data));
                    return;
                }

                byte[] inData = new byte[compressedSize];
                Array.Copy(m_fileContent, ReplaySize, inData, 0, compressedSize);
                byte[] outData = new byte[Header.DataSize];

                Decoder lzma = new Decoder();
                lzma.SetDecoderProperties(Header.Props);
                lzma.Code(new MemoryStream(inData), new MemoryStream(outData), compressedSize, Header.DataSize, null);

                m_data = outData;
                DataReader = new BinaryReader(new MemoryStream(m_data));
            }
        }

    }
}
