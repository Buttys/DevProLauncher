using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using YGOPro_Launcher.Chat.Enums;
namespace YGOPro_Launcher.Chat
{
    public class ChatMessage
    {
        public string FormattedMessage;
        public string Channel;
        public UserData From;
        public MessageType Type;
        public DateTime Time;
        

        public ChatMessage(MessageType type, UserData user,string channel,string message,bool isencoded)
        {
            if (isencoded) FormattedMessage = LauncherHelper.Base64toString(message);
            else FormattedMessage = message;
            Type = type;
            From = user;
            Channel = channel;
            Time = DateTime.Now;
        }
        public ChatMessage(MessageType type, string channel, string message)
        {
            FormattedMessage = message;
            Type = type;
            Channel = channel;
            Time = DateTime.Now;
        }

        public Color MessageColor
        {
            get
            {
                switch (Type)
                {
                    case MessageType.Message:
                        return Program.Config.NormalTextColor.ToColor();
                    case MessageType.PrivateMessage:
                        return Program.Config.NormalTextColor.ToColor();
                    case MessageType.Server:
                        return Program.Config.ServerMsgColor.ToColor();
                    case MessageType.System:
                        return Program.Config.SystemColor.ToColor();
                    case MessageType.Me:
                        return Program.Config.MeMsgColor.ToColor();
                    case MessageType.Join:
                        return Program.Config.JoinColor.ToColor();
                    case MessageType.Leave:
                        return Program.Config.LeaveColor.ToColor();
                }

                return Program.Config.NormalTextColor.ToColor();
            }
        }

        public Color UserColor
        {
            get
            {
                switch (From.Rank)
                {
                    case 0:
                        return Program.Config.Level0Color.ToColor();
                    case 1:
                        return Program.Config.Level1Color.ToColor();
                    case 2:
                        return Program.Config.Level2Color.ToColor();
                    case 99:
                        return Program.Config.Level99Color.ToColor();
                }

                return Program.Config.Level0Color.ToColor();
            }
        }

        public static Brush GetUserColor(int rank)
        {
                switch (rank)
                {
                    case 0:
                        return new SolidBrush(Program.Config.Level0Color.ToColor());
                    case 1:
                        return new SolidBrush(Program.Config.Level1Color.ToColor());
                    case 2:
                        return new SolidBrush(Program.Config.Level2Color.ToColor());
                    case 99:
                        return new SolidBrush(Program.Config.Level99Color.ToColor());
                }

                return new SolidBrush(Program.Config.Level0Color.ToColor());
        }
    }
}
