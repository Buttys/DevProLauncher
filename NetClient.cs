using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Text;
using YgoServer.NetworkData;
using Newtonsoft.Json;
namespace YGOPro_Launcher
{
    public class NetClient
    {
        public string Name { get; private set; }
        public bool IsConnected { get; private set; }

        private TcpClient m_client;
        private BinaryReader m_reader;
        private Thread m_receiveThread;
        private object m_lock;

        private Queue<MessageReceived> m_messageQueue;
        private Thread m_parserThread;

        public delegate void ServerResponse(string message);
        public delegate void ClientPacket(ClientPackets packet);
        public delegate void LoginResponse(ClientPackets type, LoginData data);
        public delegate void ServerRooms(RoomInfos[] rooms);
        public delegate void GameRoomUpdate(RoomInfos room);
        public delegate void ServerDisconnected();
        public ServerDisconnected Disconnected;
        public LoginResponse LoginReply;
        public ClientPacket RegisterReply;
        public ServerResponse OnFatalError;
        public ServerResponse RemoveRoom;
        public GameRoomUpdate UpdateRoomPlayers;
        public ServerResponse UpdateRoomStatus;
        public ServerResponse ServerMessage;
        public ServerRooms AddRooms;

        public DateTime pingrequest;

        public NetClient()
        {
            m_messageQueue = new Queue<MessageReceived>();
            m_lock = new object();
            m_receiveThread = new Thread(Receive);
            m_receiveThread.IsBackground = true;
            m_parserThread = new Thread(Parse);
            m_parserThread.IsBackground = true;
            OnFatalError += new ServerResponse(FatalError);
        }

        public bool CheckConnection()
        {
            return m_client.Connected;
        }

        private void FatalError(string message)
        {
            MessageBox.Show(message);
        }

        public bool Connect(string ServerName, string address, int port)
        {
            try
            {
                Name = ServerName;
                m_client = new TcpClient();
                m_client.Connect(address, port);
                m_reader = new BinaryReader(m_client.GetStream());
                IsConnected = true;
                m_receiveThread.Start();
                m_parserThread.Start();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Disconnect()
        {
            OnDisconnected();
            if(m_client != null)
                m_client.Close();
        }

        public void SendPacket(ServerPackets type, byte[] data)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((byte)type);
            writer.Write((short)(data.Length));
            writer.Write(data);
            SendPacket(stream.ToArray());
        }
        public void SendPacket(ServerPackets type)
        {
            SendPacket(new byte[] { (byte)type });
        }

        private void SendPacket(byte[] packet)
        {
            if (!IsConnected)
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
                OnDisconnected();
            }
        }

        private bool isLargePacket(ClientPackets packet)
        {
            return packet == ClientPackets.GameList;
        }

        private bool isOneByte(ClientPackets packet)
        {
            return (packet == ClientPackets.Banned || packet == ClientPackets.LoginFailed ||
                    packet == ClientPackets.RegisterAccept || packet == ClientPackets.RegisterFailed ||
                    packet == ClientPackets.Pong || packet == ClientPackets.Kicked);
        }

        private void Receive()
        {
            try
            {
                while (IsConnected)
                {

                    ClientPackets packet = (ClientPackets)m_reader.ReadByte();
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
                        if (len > 0)
                        {
                            BinaryReader reader = new BinaryReader(new MemoryStream(content));
                            m_messageQueue.Enqueue(new MessageReceived(packet, content, reader));
                        }
                        else
                        {
                            m_messageQueue.Enqueue(new MessageReceived(packet, null, null));
                        }
                    }

                    Thread.Sleep(1);
                }
            }
            catch (Exception)
            {
                OnDisconnected();
            }
        }

        private void Parse()
        {
            while (IsConnected)
            {
                MessageReceived message = null;
                lock (m_lock)
                {
                    if (m_messageQueue.Count > 0)
                        message = m_messageQueue.Dequeue();
                }
                if (message != null)
                    OnCommand(message);
                Thread.Sleep(1);
            }
        }

        private void OnCommand(MessageReceived e)
        {
            if (e.Packet == ClientPackets.LoginAccepted)
            {
                if (LoginReply != null)
                    LoginReply(e.Packet, JsonConvert.DeserializeObject<LoginData>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
            }
            else if (e.Packet == ClientPackets.LoginFailed)
            {
                if (LoginReply != null)
                    LoginReply(e.Packet, null);
            }
            else if (e.Packet == ClientPackets.RegisterAccept)
            {
                if (RegisterReply != null)
                    RegisterReply(e.Packet);
            }
            else if (e.Packet == ClientPackets.RegisterFailed)
            {
                if (RegisterReply != null)
                    RegisterReply(e.Packet);
            }
            else if (e.Packet == ClientPackets.GameList)
            {
                if (AddRooms != null)
                    AddRooms(JsonConvert.DeserializeObject<RoomInfos[]>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
            }
            else if (e.Packet == ClientPackets.RemoveRoom)
            {
                if (RemoveRoom != null)
                    RemoveRoom(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
            }
            else if (e.Packet == ClientPackets.UpdatePlayers)
            {
                if (UpdateRoomPlayers != null)
                    UpdateRoomPlayers(JsonConvert.DeserializeObject<RoomInfos>(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length))));
            }
            else if (e.Packet == ClientPackets.RoomStart)
            {
                if (UpdateRoomStatus != null)
                    UpdateRoomStatus(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
            }
            else if (e.Packet == ClientPackets.Pong)
            {
                MessageBox.Show("PONG!: " + -(int)pingrequest.Subtract(DateTime.Now).TotalMilliseconds);
            }
            else if (e.Packet == ClientPackets.ServerMessage)
            {
                if (ServerMessage != null)
                    ServerMessage(Encoding.UTF8.GetString(e.Reader.ReadBytes(e.Raw.Length)));
            }
            else
            {
                if (OnFatalError != null)
                    OnFatalError("Unknown packet received.");
            }
        }

        private void OnDisconnected()
        {
            if (!IsConnected) return;

            IsConnected = false;
            Disconnect();
        }
    }
}