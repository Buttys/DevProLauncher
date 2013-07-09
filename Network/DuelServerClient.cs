using System;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using DevProLauncher.Network.Enums;
using System.Windows.Forms;
using DevProLauncher.Network.Data;
using ServiceStack.Text;
using DevProLauncher.Windows.Enums;

namespace DevProLauncher.Network
{
    public class DuelServerClient
    {
        bool m_isConnected;
        private TcpClient m_client;
        private BinaryReader m_reader;
        private readonly Thread m_receiveThread;
        private readonly object m_lock;
        private DateTime m_pingRequest;

        public delegate void ServerResponse(string message);
        public delegate void Command(PacketCommand command);
        public delegate void ServerRooms(RoomInfos[] rooms);
        public delegate void GameRoomUpdate(RoomInfos room);
        public delegate void ServerDisconnected();
        public ServerDisconnected Disconnected;
        public ServerResponse OnFatalError;
        public ServerResponse RemoveRoom;
        public Command UpdateRoomPlayers;
        public GameRoomUpdate CreateRoom;
        public ServerResponse UpdateRoomStatus;
        public ServerRooms AddRooms;

        public DuelServerClient()
        {
            m_lock = new object();
            m_receiveThread = new Thread(Receive) { IsBackground = true };
            OnFatalError += FatalError;
        }

        public bool Connect(string address, int port)
        {
            try
            {
                m_client = new TcpClient();
                m_client.Connect(address, port);
                m_reader = new BinaryReader(m_client.GetStream());
                m_isConnected = true;
                m_receiveThread.Start();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void Disconnect()
        {
            if (m_isConnected)
            {
                m_isConnected = !m_isConnected;
                if (m_client != null)
                    m_client.Close();
            }
        }
        public void SendPacket(DevServerPackets type, string data)
        {
            SendPacket(type, Encoding.UTF8.GetBytes(data));
        }

        private void SendPacket(DevServerPackets type, byte[] data)
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write((byte)type);
            writer.Write((short)(data.Length));
            writer.Write(data);
            SendPacket(stream.ToArray());
        }
        public void SendPacket(DevServerPackets type)
        {
            if (type == DevServerPackets.Ping) m_pingRequest = DateTime.Now;
            SendPacket(new[] { (byte)type });
        }
        public void SendMessage(MessageType type, CommandType command, string channel, string message)
        {
            SendPacket(DevServerPackets.ChatMessage, 
                JsonSerializer.SerializeToString(new ChatMessage(type,command,channel,message)));
        }

        private void SendPacket(byte[] packet)
        {
            if (!m_isConnected)
                return;
            try
            {
                try
                {
                    m_client.Client.Send(packet, packet.Length, SocketFlags.None);
                }
                catch (Exception)
                {
                    Disconnect();
                }
            }
            catch (Exception)
            {
                Disconnect();
            }
        }
        private bool isLargePacket(DevClientPackets packet)
        {
            switch (packet)
            {
                case DevClientPackets.GameList:
                    return true;
                case DevClientPackets.UserList:
                    return true;
                case DevClientPackets.FriendList:
                    return true;
                case DevClientPackets.TeamList:
                    return true;
                case DevClientPackets.ChannelList:
                    return true;
                default:
                    return false;
            }
        }

        private bool isOneByte(DevClientPackets packet)
        {

            switch (packet)
            {
                case DevClientPackets.Banned:
                    return true;
                case DevClientPackets.LoginFailed:
                    return true;
                case DevClientPackets.RegisterAccept:
                    return true;
                case DevClientPackets.RegisterFailed:
                    return true;
                case DevClientPackets.Pong:
                    return true;
                case DevClientPackets.Kicked:
                    return true;
                case DevClientPackets.RefuseDuelRequest:
                    return true;
                default:
                    return false;
            }
        }

        private void Receive()
        {
            try
            {
                while (m_isConnected)
                {

                    var packet = (DevClientPackets)m_reader.ReadByte();
                    int len = 0;
                    byte[] content = null;
                    if (!isOneByte(packet))
                    {
                        if (isLargePacket(packet))
                        {
                            len = m_reader.ReadInt32();
                            content = m_reader.ReadBytes(len);
                        }
                        else
                        {
                            len = m_reader.ReadInt16();
                            content = m_reader.ReadBytes(len);
                        }
                    }

                    lock (m_lock)
                    {
                        if(len>0)
                        {
                            if (content != null)
                            {
                                var reader = new BinaryReader(new MemoryStream(content));
                                OnCommand(new MessageReceived(packet,content,reader));
                            }
                        }
                        else
                            OnCommand(new MessageReceived(packet,null,null));
                    }

                    Thread.Sleep(1);
                }
            }
            catch (Exception)
            {
                Disconnect();
            }
        }
        private void OnCommand(MessageReceived e)
        {
            switch (e.Packet)
            {
                case DevClientPackets.GameList:                
                    if (AddRooms != null)
                        AddRooms(JsonSerializer.DeserializeFromString<RoomInfos[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.CreateRoom:
                    if (CreateRoom != null)
                        CreateRoom(JsonSerializer.DeserializeFromString<RoomInfos>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.RemoveRoom:                
                    if (RemoveRoom != null)
                        RemoveRoom(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;
                case DevClientPackets.UpdatePlayers:
                    if (UpdateRoomPlayers != null)
                        UpdateRoomPlayers(JsonSerializer.DeserializeFromString<PacketCommand>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.RoomStart:
                    if (UpdateRoomStatus != null)
                        UpdateRoomStatus(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;
                case DevClientPackets.Pong:
                    MessageBox.Show("PONG!: " + -(int)m_pingRequest.Subtract(DateTime.Now).TotalMilliseconds);
                    break;
                default:                
                    if (OnFatalError != null)
                        OnFatalError("Unknown packet received: " + e.Packet.ToString());
                    break;

            }
        }
        public bool Connected()
        {
            return m_client.Connected;
        }

        private void FatalError(string message)
        {
            MessageBox.Show(message);
        }
    }
}
