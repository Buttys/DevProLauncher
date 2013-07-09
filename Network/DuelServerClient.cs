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
        bool _isConnected;
        private TcpClient _mClient;
        private BinaryReader _mReader;
        private readonly Thread _mReceiveThread;
        private readonly object _mLock;
        private DateTime _pingrequest;

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
            _mLock = new object();
            _mReceiveThread = new Thread(Receive) { IsBackground = true };
            OnFatalError += FatalError;
        }

        public bool Connect(string address, int port)
        {
            try
            {
                _mClient = new TcpClient();
                _mClient.Connect(address, port);
                _mReader = new BinaryReader(_mClient.GetStream());
                _isConnected = true;
                _mReceiveThread.Start();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void Disconnect()
        {
            if (_isConnected)
            {
                _isConnected = !_isConnected;
                if (_mClient != null)
                    _mClient.Close();
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
            if (type == DevServerPackets.Ping) _pingrequest = DateTime.Now;
            SendPacket(new[] { (byte)type });
        }
        public void SendMessage(MessageType type, CommandType command, string channel, string message)
        {
            SendPacket(DevServerPackets.ChatMessage, 
                JsonSerializer.SerializeToString(new ChatMessage(type,command,channel,message)));
        }

        private void SendPacket(byte[] packet)
        {
            if (!_isConnected)
                return;
            try
            {
                try
                {
                    _mClient.Client.Send(packet, packet.Length, SocketFlags.None);
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
                while (_isConnected)
                {

                    var packet = (DevClientPackets)_mReader.ReadByte();
                    int len = 0;
                    byte[] content = null;
                    if (!isOneByte(packet))
                    {
                        if (isLargePacket(packet))
                        {
                            len = _mReader.ReadInt32();
                            content = _mReader.ReadBytes(len);
                        }
                        else
                        {
                            len = _mReader.ReadInt16();
                            content = _mReader.ReadBytes(len);
                        }
                    }

                    lock (_mLock)
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
                    MessageBox.Show("PONG!: " + -(int)_pingrequest.Subtract(DateTime.Now).TotalMilliseconds);
                    break;
                default:                
                    if (OnFatalError != null)
                        OnFatalError("Unknown packet received: " + e.Packet.ToString());
                    break;

            }
        }
        public bool Connected()
        {
            return _mClient.Connected;
        }

        private void FatalError(string message)
        {
            MessageBox.Show(message);
        }
    }
}
