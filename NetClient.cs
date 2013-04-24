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
        public delegate void LoginResponse(ClientPackets type, LoginData data);
        public delegate void ServerRooms(RoomInfos[] rooms);
        public delegate void GameRoomUpdate(RoomInfos room);
        public delegate void ServerDisconnected();
        public ServerDisconnected Disconnected;
        public LoginResponse LoginReply;
        public ServerResponse RegisterReply;
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
            writer.Write((short)(data.Length + 1));
            writer.Write((byte)type);
            writer.Write(data);
            SendPacket(stream.ToArray());
        }
        public void SendPacket(ServerPackets type)
        {
            SendPacket(new byte[] { 0x01, 0x00, (byte)type });
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

        public void SendPacket(string packet)
        {
            if (!IsConnected)
                return;
            try
            {
                byte[] data = Encoding.Default.GetBytes(packet + "\n");
                m_client.Client.Send(data, data.Length, SocketFlags.None);
            }
            catch (Exception)
            {
                OnDisconnected();
            }
        }

        private void Receive()
        {
            try
            {
                while (IsConnected)
                {
                    int len = m_reader.ReadInt16();
                    byte[] content = m_reader.ReadBytes(len);
                    if (content != null)
                    {
                        lock (m_lock)
                        {
                            BinaryReader reader = new BinaryReader(new MemoryStream(content));
                            m_messageQueue.Enqueue(new MessageReceived(content,reader));
                        }
                    }
                    else
                    {
                      OnDisconnected();
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
            ClientPackets cmd = (ClientPackets)e.Reader.ReadByte();
            if (cmd == ClientPackets.LoginAccepted)
            {
                if (LoginReply != null)
                    LoginReply(cmd, JsonConvert.DeserializeObject<LoginData>(Encoding.Unicode.GetString(e.Reader.ReadBytes(e.Raw.Length - 1))));
            }
            else if (cmd == ClientPackets.LoginFailed)
            {
                if (LoginReply != null)
                    LoginReply(cmd, null);
            }
            else if (cmd == ClientPackets.RegisterAccept)
            {
                //if (RegisterReply != null)
                //    RegisterReply(args[1]);
            }
            else if (cmd == ClientPackets.RegisterFailed)
            {

            }
            else if (cmd == ClientPackets.GameList)
            {
                if (AddRooms != null)
                    AddRooms(JsonConvert.DeserializeObject<RoomInfos[]>(Encoding.Unicode.GetString(e.Reader.ReadBytes(e.Raw.Length - 1))));
            }
            else if (cmd == ClientPackets.RemoveRoom)
            {
                if (RemoveRoom != null)
                    RemoveRoom(Encoding.Unicode.GetString(e.Reader.ReadBytes(e.Raw.Length - 1)));
            }
            else if (cmd == ClientPackets.UpdatePlayers)
            {
                if (UpdateRoomPlayers != null)
                    UpdateRoomPlayers(JsonConvert.DeserializeObject<RoomInfos>(Encoding.Unicode.GetString(e.Reader.ReadBytes(e.Raw.Length - 1))));
            }
            else if (cmd == ClientPackets.RoomStart)
            {
                if (UpdateRoomStatus != null)
                    UpdateRoomStatus(Encoding.Unicode.GetString(e.Reader.ReadBytes(e.Raw.Length - 1)));
            }
            else if (cmd == ClientPackets.Pong)
            {
                MessageBox.Show("PONG!: " + -(int)pingrequest.Subtract(DateTime.Now).TotalMilliseconds);
            }
            else if (cmd == ClientPackets.ServerMessage)
            {
                if (ServerMessage != null)
                    ServerMessage(Encoding.Unicode.GetString(e.Reader.ReadBytes(e.Raw.Length - 1)));
            }
            else
            {
                if (OnFatalError != null)
                    OnFatalError("Unknown packet received.");
            }
        }

        private void OnCommand(string command)
        {
            string[] args = command.Split(new string[]{"||"},StringSplitOptions.None);
            string cmd = args[0];

            //if (cmd == "ROOMS")
            //{
            //    List<RoomInfos> rooms = new List<RoomInfos>();
            //    for (int i = 1; i < args.Length; ++i)
            //    {
            //        string[] infos = args[i].Split(';');
            //        RoomInfos room = RoomInfos.FromName(infos[0], infos[1], infos[2] == "1");
            //        if (room != null) rooms.Add(room);
            //    }
            //    //if (AddRooms != null)
            //    //    AddRooms(rooms.ToArray());
            //}
            ////else if (cmd == "-ROOM")
            ////{
            ////    if (RemoveRoom != null)
            ////        RemoveRoom(args[1]);
            ////}
            ////else if (cmd == "PLAYERS")
            ////{
            ////    if (UpdateRoomPlayers != null)
            ////        UpdateRoomPlayers(args[1] + "||" + args[2]+ "||" + args[3]);
            ////}
            ////else if (cmd == "START")
            ////{
            ////    if (UpdateRoomStatus != null)
            ////        UpdateRoomStatus(args[1]);
            ////}
            //else if (cmd == "REGISTER")
            //{
            //    if (RegisterReply != null)
            //        RegisterReply(args[1]);
            //}
            //else if (cmd == "MSG")
            //{
            //    if (ServerMessage != null)
            //        ServerMessage(args[1]);
            //}
            //else if (cmd == "PONG")
            //{
            //    MessageBox.Show("PONG!: " + -(int)pingrequest.Subtract(DateTime.Now).TotalMilliseconds);
            //}
            //else
            //{
            //    if (OnFatalError != null)
            //        OnFatalError("Unknown packet received.");
            //}
        }

        private void OnDisconnected()
        {
            if (!IsConnected) return;

            IsConnected = false;
            Disconnect();
        }
    }
}