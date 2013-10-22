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
            public BinaryReader DataReader;

            private byte[] _mFileContent;
            private byte[] _mData;

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
                    _mFileContent = File.ReadAllBytes(fileName);
                    BinaryReader reader = new BinaryReader(new MemoryStream(_mFileContent));
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
                return value.Substring(0, value.IndexOf("\0", StringComparison.Ordinal));
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
                int compressedSize = _mFileContent.Length - ReplaySize;
                if (!Compressed)
                {
                    _mData = reader.ReadBytes(compressedSize);
                    DataReader = new BinaryReader(new MemoryStream(_mData));
                    return;
                }

                Byte[] inData = new byte[compressedSize];
                Array.Copy(_mFileContent, ReplaySize, inData, 0, compressedSize);
                Byte[] outData = new byte[Header.DataSize];

                Decoder lzma = new Decoder();
                lzma.SetDecoderProperties(Header.Props);
                lzma.Code(new MemoryStream(inData), new MemoryStream(outData), compressedSize, Header.DataSize, null);

                _mData = outData;
                DataReader = new BinaryReader(new MemoryStream(_mData));
            }
        }
    }
}
