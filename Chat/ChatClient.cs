using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Text;
using YGOPro_Launcher.Chat.Enums;
namespace YGOPro_Launcher.Chat
{
    public class ChatClient
    {
        public bool IsConnected { get; private set; }

        private TcpClient m_client;
        private StreamReader m_reader;
        private Thread m_receiveThread;
        private object m_lock;

        private Queue<string> m_messageQueue;
        private Thread m_parserThread;

        public delegate void ServerResponse(string message);
        public delegate void ServerMessage(ChatMessage message);
        public delegate void ServerDisconnected();
        public ServerResponse UserList;
        public ServerResponse AddUser;
        public ServerResponse RemoveUser;
        public ServerResponse Login;
        public ServerResponse DuelRequest;
        public ServerResponse FriendList;
        public ServerMessage Message;
        public ServerMessage Error;
        public ServerResponse JoinChannel;
        public ServerResponse LeaveChannel;
        public ServerResponse DevPointMSG;
        public DateTime PingRequest;

        public ChatClient()
        {
            m_messageQueue = new Queue<string>();
            m_lock = new object();
            m_receiveThread = new Thread(Receive);
            m_receiveThread.IsBackground = true;
            m_parserThread = new Thread(Parse);
            m_parserThread.IsBackground = true;
        }

        public bool CheckConnection()
        {
            return m_client.Connected;
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
                byte[] data = Encoding.UTF8.GetBytes(packet + "\n");
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
            string[] args = command.Split(new string[]{"||"},StringSplitOptions.None);
            string cmd = args[0];

            if (cmd == "USERS")
            {
                if (UserList != null)
                    UserList(args[1]);
            }
            else if (cmd == "+USER")
            {
                if (AddUser != null)
                    AddUser(args[1]);
            }
            else if (cmd == "-USER")
            {
                if (RemoveUser != null)
                    RemoveUser(args[1] + "||" + args[2]);
            }
            else if (cmd == "FRIENDS")
            {
                if (FriendList != null)
                    FriendList(args[1]);
            }
            else if (cmd == "JOINCHANNELACCEPT")
            {
                if (JoinChannel != null)
                    JoinChannel(args[1]);
            }
            else if (cmd == "LOGIN")
            {
                if (args[1] != "")
                {
                    SendPacket("GETFRIENDS");
                }
                if (Login != null)
                    Login(args[1]);
            }
            else if (cmd == "MSG")
            {
                string[] userinfo = args[1].Split(',');
                if (Message != null)
                    Message(new ChatMessage((MessageType)Convert.ToInt32(args[3]), new UserData() { Username = userinfo[0], Rank = Convert.ToInt32(userinfo[1]) }, args[2], args[4], true));
            }
            else if (cmd == "STARTDUEL")
            {
                if (DuelRequest != null)
                    DuelRequest("START||" + args[1]);
            }
            else if (cmd == "DUELREQUEST")
            {
                if (DuelRequest != null)
                    DuelRequest("REQUEST||" + args[1] + "||" + args[2]);
            }
            else if (cmd == "REFUSEDUEL")
            {
                if (DuelRequest != null)
                    DuelRequest("REFUSE||" + args[1]);
            }
            else if (cmd == "ACCEPTDUEL")
            {
                if (Message != null)
                    Message(new ChatMessage(MessageType.System, "DevPro", args[1] + " has accepted your duel request."));
            }
            else if (cmd == "DEVPOINTS")
            {
                if (DevPointMSG != null)
                    DevPointMSG(command);
            }
            else if (cmd == "ADMIN")
            {
                if (args.Length >= 3)
                {
                    if (Message != null)
                        Message(new ChatMessage(MessageType.Server, "DevPro", args[2]));
                }
            }
            else if (cmd == "PONG")
            {
                if (Message != null)
                    Message(new ChatMessage(MessageType.Server, "DevPro","Pong!: " + -(int)PingRequest.Subtract(DateTime.Now).TotalMilliseconds + "ms"));
            }
            else
            {
                if (Error != null)
                    Error(new ChatMessage(MessageType.System, "DevPro", "Unknown Packet - " + command));
            }
        }

        private void OnDisconnected()
        {
            if (!IsConnected) return;
            if (Error != null)
                Error(new ChatMessage(MessageType.System, "DevPro", "Disconnected from server."));
            IsConnected = false;
            Disconnect();
        }
    }
}