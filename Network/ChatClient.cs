using System;
using System.Linq;
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
    public class ChatClient
    {
        bool m_isConnected;
        private TcpClient m_client;
        private BinaryReader m_reader;
        private readonly Thread m_receiveThread;
        private readonly object m_lock;
        private DateTime m_pingRequest;

        public delegate void ServerResponse(string message);
        public delegate void Command(PacketCommand command);
        public delegate void ClientPacket(DevClientPackets packet);
        public delegate void LoginResponse(DevClientPackets type, LoginData data);
        public delegate void ServerRooms(RoomInfos[] rooms);
        public delegate void GameRoomUpdate(RoomInfos room);
        public delegate void ServerDisconnected();        
        public delegate void Message(ChatMessage message);
        public delegate void UserDuelRequest(DuelRequest data);
        public delegate void DuelRequestRefused();
        public delegate void UserInfo(UserData info);
        public delegate void UserList(UserData[] info);
        public delegate void ChannelList(ChannelData[] channels);
        public delegate void StringList(string[] data);
        public delegate void ChannelUsersUpdate(ChannelUsers users);

        public UserInfo UpdateUserInfo;
        public UserList UserListUpdate;
        public ChannelUsersUpdate ChannelUserList;
        public ChannelUsersUpdate AddUserToChannel;
        public ChannelUsersUpdate RemoveUserFromChannel;
        public ServerDisconnected Disconnected;
        public LoginResponse LoginReply;
        public ClientPacket RegisterReply;
        public ServerResponse OnFatalError;
        public ServerResponse RemoveRoom;
        public ServerResponse UserStats;
        public ServerResponse TeamStats;
        public Command UpdateRoomPlayers;
        public GameRoomUpdate CreateRoom;
        public ServerResponse UpdateRoomStatus;
        public ServerResponse ServerMessage;
        public ServerRooms AddRooms;
        public Command DevPointMsg;
        public UserList FriendList;
        public ServerResponse JoinChannel;
        public Message ChatMessage;
        public UserDuelRequest DuelRequest;
        public UserDuelRequest DuelAccepted;
        public DuelRequestRefused DuelRefused;
        public Command TeamRequest;
        public UserList TeamList;
        public ChannelList ChannelRequest;
        public ServerResponse AddGameServer;
        public ServerResponse RemoveGameServer;
        public ServerResponse Kicked;
        public ServerResponse Banned;


        public ChatClient()
        {
            m_client = new TcpClient();
            m_lock = new object();
            m_receiveThread = new Thread(Receive) { IsBackground = true };
            OnFatalError += FatalError;
        }

        public bool Connect(string address, int port)
        {
            try
            {
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
                case DevClientPackets.UserList:
                case DevClientPackets.FriendList:
                case DevClientPackets.TeamList:
                case DevClientPackets.ChannelList:
                case DevClientPackets.ChannelUsers:
                    return true;
                default:
                    return false;
            }
        }

        private bool isOneByte(DevClientPackets packet)
        {

            switch (packet)
            {
                case DevClientPackets.LoginFailed:
                case DevClientPackets.RegisterAccept:
                case DevClientPackets.RegisterFailed:
                case DevClientPackets.Pong:
                case DevClientPackets.RefuseDuelRequest:
                    return true;
                default:
                    return false;
            }
        }

        private void Receive()
        {
#if !DEBUG
            try
            {
#endif
                while (m_isConnected)
                {
                    if (CheckDisconnected())
                    {
                        Disconnect();
                        return;
                    }

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
#if !DEBUG
            }
            catch (Exception)
            {
                Disconnect();
            }
#endif
        }

        private bool CheckDisconnected()
        {
            return (m_client.Client.Poll(1, SelectMode.SelectRead) && m_client.Available == 0);
        }
        private void OnCommand(MessageReceived e)
        {
            switch (e.Packet)
            {
                case DevClientPackets.LoginAccepted:
                    if (LoginReply != null)
                        LoginReply(e.Packet, JsonSerializer.DeserializeFromString<LoginData>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.LoginFailed:
                    if (LoginReply != null)
                        LoginReply(e.Packet, null);
                    break;
                case DevClientPackets.Banned:
                    string message = Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length));
                    if (string.IsNullOrEmpty(message))
                    {
                        if (Banned != null)
                            Banned("You are banned.");
                    }
                    else
                    {
                        if (Banned != null)
                            Banned(message);
                    }

                    break;
                case DevClientPackets.RegisterAccept:                
                    if (RegisterReply != null)
                        RegisterReply(e.Packet);
                    break;
                case DevClientPackets.RegisterFailed:
                    if (RegisterReply != null)
                        RegisterReply(e.Packet);
                    break;
                case DevClientPackets.UserStats:
                    if(UserStats != null)
                        UserStats(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;
                case DevClientPackets.TeamStats:
                    if (TeamStats != null)
                        TeamStats(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;
                case DevClientPackets.Pong:
                    MessageBox.Show("PONG!: " + -(int)m_pingRequest.Subtract(DateTime.Now).TotalMilliseconds);
                    break;
                case DevClientPackets.ServerMessage:
                    if (ServerMessage != null)
                        ServerMessage(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;
                case DevClientPackets.ChannelUsers:
                    if(ChannelUserList != null)
                        ChannelUserList(JsonSerializer.DeserializeFromString<ChannelUsers>(
                            Encoding.UTF8.GetString(e.Raw)));
                    break;
                case DevClientPackets.AddChannelUser:
                    if (AddUserToChannel != null)
                        AddUserToChannel(JsonSerializer.DeserializeFromString<ChannelUsers>(
                            Encoding.UTF8.GetString(e.Raw)));
                    break;
                case DevClientPackets.RemoveChannelUser:
                    if (RemoveUserFromChannel != null)
                        RemoveUserFromChannel(JsonSerializer.DeserializeFromString<ChannelUsers>(
                            Encoding.UTF8.GetString(e.Raw)));
                    break;
                case DevClientPackets.UpdateUserInfo:
                    if (UpdateUserInfo != null)
                        UpdateUserInfo(JsonSerializer.DeserializeFromString<UserData>(Encoding.UTF8.GetString(e.Raw)));
                    break;
                case DevClientPackets.UserList:
                    if (UserListUpdate != null)
                        UserListUpdate(JsonSerializer.DeserializeFromString<UserData[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.GameServers:                
                    var servers = JsonSerializer.DeserializeFromString<ServerInfo[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    foreach (ServerInfo server in servers.Where(server => !Program.ServerList.ContainsKey(server.serverName)))
                    {
                        Program.ServerList.Add(server.serverName, server);
                        if (AddGameServer != null)
                            AddGameServer(server.serverName);
                    }
                    break;
                case DevClientPackets.AddServer:                
                    var serverinfo = JsonSerializer.DeserializeFromString<ServerInfo>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    if (!Program.ServerList.ContainsKey(serverinfo.serverName))
                    {
                        Program.ServerList.Add(serverinfo.serverName, serverinfo);
                        if (AddGameServer != null)
                            AddGameServer(serverinfo.serverName);
                    }
                    break;
                case DevClientPackets.RemoveServer:                
                    string removeserver = Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length));
                    if (Program.ServerList.ContainsKey(removeserver))
                    {
                        Program.ServerList.Remove(removeserver);
                        if (RemoveGameServer != null)
                            RemoveGameServer(removeserver);
                    }
                    break;
                case DevClientPackets.JoinChannelAccept:                
                    if (JoinChannel != null)
                        JoinChannel(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;
                case DevClientPackets.ChannelList:                
                    if (ChannelRequest != null)
                        ChannelRequest(JsonSerializer.DeserializeFromString<ChannelData[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.FriendList:
                    if (FriendList != null)
                        FriendList(JsonSerializer.DeserializeFromString<UserData[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.TeamList:
                    if (TeamList != null)
                        TeamList(JsonSerializer.DeserializeFromString<UserData[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.Message:
                    if (ChatMessage != null)
                        ChatMessage(JsonSerializer.DeserializeFromString<ChatMessage>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.DevPoints:                
                    if (DevPointMsg != null)
                        DevPointMsg(JsonSerializer.DeserializeFromString<PacketCommand>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.DuelRequest:
                    if (DuelRequest != null)
                        DuelRequest(JsonSerializer.DeserializeFromString<DuelRequest>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.AcceptDuelRequest:
                    string user = Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length));
                    if (ChatMessage != null)
                        ChatMessage(new ChatMessage(MessageType.Server, CommandType.None, null, user + " has accepted your duel request."));
                    break;
                case DevClientPackets.StartDuel:                    
                    if(DuelAccepted != null)
                        DuelAccepted(JsonSerializer.DeserializeFromString<DuelRequest>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.RefuseDuelRequest:
                    if(DuelRefused != null)
                        DuelRefused();
                    break;
                case DevClientPackets.TeamRequest:
                    if (TeamRequest != null)
                        TeamRequest(JsonSerializer.DeserializeFromString<PacketCommand>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
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
                case DevClientPackets.Kicked:
                    if (Kicked != null)
                        Kicked(Encoding.UTF8.GetString(e.Raw));
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
