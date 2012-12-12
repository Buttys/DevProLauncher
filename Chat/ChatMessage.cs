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
                        return Color.Black;
                    case MessageType.PrivateMessage:
                        return Color.Black;
                    case MessageType.Server:
                        return Color.Red;
                    case MessageType.System:
                        return Color.Purple;
                    case MessageType.Me:
                        return Color.DeepPink;
                    case MessageType.Join:
                        return Color.Green;
                    case MessageType.Leave:
                        return Color.Gray;
                }

                return Color.Black;
            }
        }

        public Color UserColor
        {
            get
            {
                switch (From.Rank)
                {
                    case 0:
                        return Color.Black;
                    case 1:
                        return Color.RoyalBlue;
                    case 2:
                        return Color.Red;
                    case 99:
                        return Color.Green;
                }

                return Color.Black;
            }
        }

        public static Brush GetUserColor(int rank)
        {
                switch (rank)
                {
                    case 0:
                        return Brushes.Black;
                    case 1:
                        return Brushes.RoyalBlue;
                    case 2:
                        return Brushes.Red;
                    case 99:
                        return Brushes.Green;
                }

                return Brushes.Black;
        }
    }
}
