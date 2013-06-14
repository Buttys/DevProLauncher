using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using DevProLauncher.Network.Enums;
using System.Windows.Forms;
using DevProLauncher.Network.Data;
using ServiceStack.Text;
using DevProLauncher.Windows.Components;
using DevProLauncher.Windows.Enums;

namespace DevProLauncher.Network
{
    public class ChatClient
    {
        bool isConnected = false;
        private TcpClient m_client;
        private BinaryReader m_reader;
        private Thread m_receiveThread;
        private object m_lock;
        private DateTime pingrequest;

        public delegate void ServerResponse(string message);
        public delegate void Command(PacketCommand command);
        public delegate void ClientPacket(DevClientPackets packet);
        public delegate void LoginResponse(DevClientPackets type, LoginData data);
        public delegate void ServerRooms(RoomInfos[] rooms);
        public delegate void GameRoomUpdate(RoomInfos room);
        public delegate void ServerDisconnected();        
        public delegate void UserInfo(UserData user);
        public delegate void UserList(UserData[] users);
        public delegate void Logout(LogoutData user);
        public delegate void ServerMessage(ChatMessage message);
        public delegate void UserDuelRequest(DuelRequest data);
        public delegate void DuelRequestRefused();
        public delegate void ChannelList(ChannelData[] channels);
        public delegate void StringList(string[] data);
        public ServerDisconnected disconnected;
        public LoginResponse loginReply;
        public ClientPacket registerReply;
        public ServerResponse onFatalError;
        public ServerResponse removeRoom;
        public ServerResponse userStats;
        public ServerResponse teamStats;
        public Command updateRoomPlayers;
        public GameRoomUpdate createRoom;
        public ServerResponse updateRoomStatus;
        public ServerResponse serverMessage;
        public ServerRooms addRooms;
        public Command DevPointMSG;
        public UserInfo AddUser;
        public UserList AddUsers;
        public Logout RemoveUser;
        public StringList FriendList;
        public ServerResponse JoinChannel;
        public ServerMessage Message;
        public UserDuelRequest DuelRequest;
        public UserDuelRequest DuelAccepted;
        public DuelRequestRefused DuelRefused;
        public Command TeamRequest;
        public StringList teamList;
        public ChannelList ChannelRequest;
        public ServerResponse AddGameServer;
        public ServerResponse RemoveGameServer;

        public ChatClient()
        {
            m_lock = new object();
            m_receiveThread = new Thread(Receive) { IsBackground = true };
            onFatalError += FatalError;
        }

        public bool Connect(string address, int port)
        {
            try
            {
                m_client = new TcpClient();
                m_client.Connect(address, port);
                m_reader = new BinaryReader(m_client.GetStream());
                isConnected = true;
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
            if (isConnected)
            {
                isConnected = !isConnected;
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
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((byte)type);
            writer.Write((short)(data.Length));
            writer.Write(data);
            SendPacket(stream.ToArray());
        }
        public void SendPacket(DevServerPackets type)
        {
            if (type == DevServerPackets.Ping) pingrequest = DateTime.Now;
            SendPacket(new byte[] { (byte)type });
        }
        public void SendMessage(MessageType type, CommandType command, string channel, string message)
        {
            SendPacket(DevServerPackets.ChatMessage, 
                JsonSerializer.SerializeToString<ChatMessage>(new ChatMessage(type,command,channel,message)));
        }

        private void SendPacket(byte[] packet)
        {
            if (!isConnected)
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
                while (isConnected)
                {

                    DevClientPackets packet = (DevClientPackets)m_reader.ReadByte();
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
                            BinaryReader reader = new BinaryReader(new MemoryStream(content));
                            OnCommand(new MessageReceived(packet,content,reader));
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
                case DevClientPackets.LoginAccepted:
                    if (loginReply != null)
                        loginReply(e.Packet, JsonSerializer.DeserializeFromString<LoginData>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.LoginFailed:
                    if (loginReply != null)
                        loginReply(e.Packet, null);
                    break;
                case DevClientPackets.Banned:
                    MessageBox.Show("You are banned.");
                    break;
                case DevClientPackets.RegisterAccept:                
                    if (registerReply != null)
                        registerReply(e.Packet);
                    break;
                case DevClientPackets.RegisterFailed:
                    if (registerReply != null)
                        registerReply(e.Packet);
                    break;
                case DevClientPackets.UserStats:
                    if(userStats != null)
                        userStats(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;
                case DevClientPackets.TeamStats:
                    if (teamStats != null)
                        teamStats(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;
                case DevClientPackets.Pong:
                    MessageBox.Show("PONG!: " + -(int)pingrequest.Subtract(DateTime.Now).TotalMilliseconds);
                    break;
                case DevClientPackets.ServerMessage:                
                    if (serverMessage != null)
                        serverMessage(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    break;
                case DevClientPackets.AddUser:                
                    if (AddUser != null)
                        AddUser(JsonSerializer.DeserializeFromString<UserData>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.RemoveUser:                
                    if (RemoveUser != null)
                        RemoveUser(JsonSerializer.DeserializeFromString<LogoutData>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.UserList:                
                    if (AddUsers != null)
                        AddUsers(JsonSerializer.DeserializeFromString<UserData[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.GameServers:                
                    ServerInfo[] servers = JsonSerializer.DeserializeFromString<ServerInfo[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
                    foreach (ServerInfo server in servers)
                        if (!Program.ServerList.ContainsKey(server.serverName))
                        {
                            Program.ServerList.Add(server.serverName, server);
                            if (AddGameServer != null)
                                AddGameServer(server.serverName);
                        }
                    break;
                case DevClientPackets.AddServer:                
                    ServerInfo serverinfo = JsonSerializer.DeserializeFromString<ServerInfo>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
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
                        FriendList(JsonSerializer.DeserializeFromString<string[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.TeamList:
                    if (teamList != null)
                        teamList(JsonSerializer.DeserializeFromString<string[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.Message:                
                    if (Message != null)
                        Message(JsonSerializer.DeserializeFromString<ChatMessage>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.DevPoints:                
                    if (DevPointMSG != null)
                        DevPointMSG(JsonSerializer.DeserializeFromString<PacketCommand>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.DuelRequest:
                    if (DuelRequest != null)
                        DuelRequest(JsonSerializer.DeserializeFromString<DuelRequest>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
                    break;
                case DevClientPackets.AcceptDuelRequest:
                    string user = Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length));
                    if(Message != null)
                        Message(new ChatMessage(MessageType.Server,CommandType.None,null,user + " has accepted your duel request."));
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
                default:                
                    if (onFatalError != null)
                        onFatalError("Unknown packet received: " + e.Packet.ToString());
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
