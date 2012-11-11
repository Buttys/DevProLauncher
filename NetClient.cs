using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Text;
namespace YGOPro_Launcher
{
    public class NetClient
    {
        public bool IsConnected { get; private set; }

        private TcpClient m_client;
        private StreamReader m_reader;
        private Thread m_receiveThread;
        private object m_lock;

        private Queue<string> m_messageQueue;
        private Thread m_parserThread;
        
        public delegate void ServerResponse(string message);
        public delegate void ServerRooms(RoomInfos[] rooms);
        public delegate void ServerDisconnected();
        public ServerDisconnected Disconnected;
        public ServerResponse LoginReply;
        public ServerResponse RegisterReply;
        public ServerResponse OnFatalError;
        public ServerResponse UserInfoUpdate;
        public ServerResponse RemoveRoom;
        public ServerResponse UpdateRoomPlayers;
        public ServerResponse UpdateRoomStatus;
        public ServerResponse AdminMessage;
        public ServerResponse ServerMessage;
        public ServerRooms AddRooms;
        public ServerRooms AddRoom;

        public NetClient()
        {
            m_messageQueue = new Queue<string>();
            m_lock = new object();
            m_receiveThread = new Thread(Receive);
            m_receiveThread.IsBackground = true;
            m_parserThread = new Thread(Parse);
            m_parserThread.IsBackground = true;
            OnFatalError += new ServerResponse(FatalError);
        }

        private void FatalError(string message)
        {
            MessageBox.Show(message);
        }

        public bool Connect(string address, int port)
        {
            try
            {
                m_client = new TcpClient();
                m_client.Connect(address, port);
                m_reader = new StreamReader(m_client.GetStream());
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
            m_client.Close();
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
                        string line = m_reader.ReadLine();
                        if (line != null)
                        {
                            lock (m_lock)
                                m_messageQueue.Enqueue(line);
                        }
                        else
                            OnDisconnected();
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
                string message = null;
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

        private void OnCommand(string command)
        {
            string[] args = command.Split('|');
            string cmd = args[0];

            if (cmd == "ROOMS")
            {
                List<RoomInfos> rooms = new List<RoomInfos>();
                for (int i = 1; i < args.Length; ++i)
                {
                    string[] infos = args[i].Split(';');
                    RoomInfos room = RoomInfos.FromName(infos[0], infos[1], infos[2] == "1");
                    if (room != null) rooms.Add(room);
                }
                if (AddRooms != null)
                    AddRooms(rooms.ToArray());
            }
            else if (cmd == "+ROOM")
            {
                string[] infos = args[1].Split(';');
                RoomInfos room = RoomInfos.FromName(infos[0], infos[1], infos[2] == "1");
                if (AddRoom != null)
                    AddRoom(new RoomInfos[] { room });
            }
            else if (cmd == "-ROOM")
            {
                if (RemoveRoom != null)
                    RemoveRoom(args[1]);
            }
            else if (cmd == "PLAYERS")
            {
                if (UpdateRoomPlayers != null)
                    UpdateRoomPlayers(args[1] + "|" + args[2]);
            }
            else if (cmd == "START")
            {
                if (UpdateRoomStatus != null)
                    UpdateRoomStatus(args[1]);
            }
            else if (cmd == "REGISTER")
            {
                if (RegisterReply != null) 
                    RegisterReply(args[1]);
            }
            else if (cmd == "LOGIN")
            {
                if (LoginReply != null)
                    LoginReply(args[1] + ( args.Length > 2 ? "|" + args[2]: ""));
            }
            else if (cmd == "WLD")
            {
                if (UserInfoUpdate != null) 
                    UserInfoUpdate(args[1]);
            }
            else if (cmd == "ADMIN")
            {
                if (args.Length >= 3)
                {
                    if (AdminMessage != null)
                        AdminMessage(args[1] + "|" + args[2]);
                }
            }
            else if (cmd == "MSG")
            {
                if (ServerMessage != null)
                    ServerMessage(args[1]);
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

                if (OnFatalError != null)
                    OnFatalError("Disconnected from server.");
            
        }
    }
}