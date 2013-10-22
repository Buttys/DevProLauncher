using System.Drawing;
using DevProLauncher.Windows.Enums;

namespace DevProLauncher.Network.Data
{
    public class ChatMessage
    {
        public string message { get; set; }
        public string channel { get; set; }
        public UserData from { get; set; }
        public int type { get; set; }
        public int command { get; set; }

        public ChatMessage(MessageType type, CommandType command,UserData user, string channel, string message)
        {
            this.message = message;
            this.type = (int)type;
            this.channel = channel;
            this.command = (int)command;
            this.from = user;
        }
        public ChatMessage(MessageType type,CommandType command, string channel, string message)
        {
            this.message = message;
            this.type = (int)type;
            this.channel = channel;
            this.command = (int)command;
        }

        public Color MessageColor()
        {
            switch ((MessageType)type)
            {
                case MessageType.Message:
                    return Program.Config.NormalTextColor.ToColor();
                case MessageType.PrivateMessage:
                    return Program.Config.NormalTextColor.ToColor();
                case MessageType.Server:
                    return Program.Config.ServerMsgColor.ToColor();
                case MessageType.System:
                    return Program.Config.SystemColor.ToColor();
                case MessageType.Join:
                    return Program.Config.JoinColor.ToColor();
                case MessageType.Leave:
                    return Program.Config.LeaveColor.ToColor();
            }

            return Program.Config.NormalTextColor.ToColor();
        }

        public Color RankColor()
        {
            if (from == null) return Program.Config.Level0Color.ToColor();
            switch (from.rank)
            {
                case 0:
                    return Program.Config.Level0Color.ToColor();
                case 1:
                    return Program.Config.Level1Color.ToColor();
                case 2:
                    return Program.Config.Level2Color.ToColor();
                case 3:
                    return Program.Config.Level2Color.ToColor();
                case 4:
                    return Program.Config.Level1Color.ToColor();
                case 99:
                    return Program.Config.Level99Color.ToColor();
            }

            return Program.Config.Level0Color.ToColor();
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
                case 3:
                    return new SolidBrush(Program.Config.Level2Color.ToColor());
                case 4:
                    return new SolidBrush(Program.Config.Level1Color.ToColor());
                case 99:
                    return new SolidBrush(Program.Config.Level99Color.ToColor());
            }

            return new SolidBrush(Program.Config.Level0Color.ToColor());
        }
    }
}
