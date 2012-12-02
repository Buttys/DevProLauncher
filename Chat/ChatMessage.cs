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
        public Color UserColor = Color.Black;
        public Color MessageColor = Color.Black;
        public string FormattedMessage;
        public string Channel;
        public UserData From;
        public MessageType Type;
        

        public ChatMessage(MessageType type, UserData user,string channel,string message,bool isbinary)
        {
            if (isbinary) FormattedMessage = LauncherHelper.BinaryToString(message);
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
    }
}
