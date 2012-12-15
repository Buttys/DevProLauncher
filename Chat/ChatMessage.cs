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
        

        public ChatMessage(MessageType type, UserData user,string channel,string message,bool isencoded)
        {
            if (isencoded) FormattedMessage = LauncherHelper.Base64toString(message);
            else FormattedMessage = message;
            Type = type;
            From = user;
            Channel = channel;
        }
        public ChatMessage(MessageType type, string channel, string message)
        {
            FormattedMessage = message;
            Type = type;
            Channel = channel;
        }

        public Color MessageColor
        {
            get
            {
                switch (Type)
                {
                    case MessageType.Message:
                        return Color.FromName(Program.Config.NormalTextColor);
                    case MessageType.PrivateMessage:
                        return Color.FromName(Program.Config.NormalTextColor);
                    case MessageType.Server:
                        return Color.FromName(Program.Config.ServerMsgColor);
                    case MessageType.System:
                        return Color.FromName(Program.Config.SystemColor);
                    case MessageType.Me:
                        return Color.FromName(Program.Config.MeMsgColor);
                    case MessageType.Join:
                        return Color.FromName(Program.Config.JoinColor);
                    case MessageType.Leave:
                        return Color.FromName(Program.Config.LeaveColor);
                }

                return Color.FromName(Program.Config.NormalTextColor);
            }
        }

        public Color UserColor
        {
            get
            {
                switch (From.Rank)
                {
                    case 0:
                        return Color.FromName(Program.Config.Level0Color);
                    case 1:
                        return Color.FromName(Program.Config.Level1Color);
                    case 2:
                        return Color.FromName(Program.Config.Level2Color);
                    case 99:
                        return Color.FromName(Program.Config.Level99Color);
                }

                return Color.FromName(Program.Config.Level0Color);
            }
        }

        public static Brush GetUserColor(int rank)
        {
                switch (rank)
                {
                    case 0:
                        return new SolidBrush(Color.FromName(Program.Config.Level0Color));
                    case 1:
                        return new SolidBrush(Color.FromName(Program.Config.Level1Color));
                    case 2:
                        return new SolidBrush(Color.FromName(Program.Config.Level2Color));
                    case 99:
                        return new SolidBrush(Color.FromName(Program.Config.Level99Color));
                }

                return new SolidBrush(Color.FromName(Program.Config.Level0Color));
        }
    }
}
