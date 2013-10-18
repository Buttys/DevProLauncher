using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using DevProLauncher.Windows.Components;
using DevProLauncher.Windows.Enums;
using DevProLauncher.Network.Data;

namespace DevProLauncher.Helpers
{
    public static class ChatHelper
    {
        public static Dictionary<string, string> ChatTags = new Dictionary<string, string>();

        public static void LoadChatTags()
        {
            ChatTags.Add("Red", "\\cf2");
            ChatTags.Add("Green", "\\cf3");
            ChatTags.Add("Blue", "\\cf4");
        }

        public static void WriteMessage(ChatMessage message, CustomRTB window, bool autoscroll)
        {
            if (window.Text != "")//start a new line unless theres no text
                window.AppendText(Environment.NewLine);
            window.Select(window.TextLength, 0);
            
            if ((CommandType)message.command == CommandType.Me)
            {
                string LogFile = "";
                string LogText = "";
                string LogDir = @"Logs\";
                if ((MessageType)message.type == MessageType.Message)
                {
                    LogFile = message.channel + DateTime.Now.ToString(".dd") + ".txt";
                    LogText += DateTime.Now.ToString("[HH:mm] ");
                    LogDir += @"Channel\" + DateTime.Now.ToString("MMMM_yyyy") + @"\";
                }
                else if ((MessageType)message.type == MessageType.PrivateMessage)
                {
                    LogFile = message.channel + ".txt";
                    LogText += DateTime.Now.ToString("[dd/MM/yy | HH:mm] ");
                    LogDir += @"PMs\";
                }
                else if ((MessageType)message.type == MessageType.Team)
                {
                    LogFile = "Team.txt";
                    LogText += DateTime.Now.ToString("[HH:mm] ");
                }
                if (Program.Config.ShowTimeStamp)
                {
                    WriteText(window, DateTime.Now.ToString("[HH:mm] "), (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                }
                if (message.from.rank > 0)
                {
                    WriteText(window, "[", (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                    if (message.from.rank == 1 || message.from.rank == 4)
                    {
                        WriteText(window, "Dev", (Program.Config.ColorBlindMode ? Color.Black : message.RankColor()));
                        LogText += "[Dev]";
                    }
                    else if (message.from.rank == 2 || message.from.rank == 3)
                    {
                        WriteText(window, "Mod", (Program.Config.ColorBlindMode ? Color.Black : message.RankColor()));
                        LogText += "[Mod]";
                    }
                    else if (message.from.rank == 99)
                    {
                        LogText += "[Dev]";
                        WriteText(window, "Dev", (Program.Config.ColorBlindMode ? Color.Black : message.RankColor()));
                    }
                    WriteText(window, "]", (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                }
                WriteText(window, message.message, Program.Config.MeMsgColor.ToColor());
                LogText += message.message.Trim();
                if (LogFile != "")
                    if (!Directory.Exists(LogDir))
                        Directory.CreateDirectory(LogDir);
                File.AppendAllText(LogDir + LogFile, LogText + Environment.NewLine);
            }
            else if ((MessageType)message.type == MessageType.Team && (CommandType)message.command == CommandType.TeamServerMessage)
            {
                WriteText(window, DateTime.Now.ToString("[HH:mm]") + "[TeamMessage] " + message.message,(Program.Config.ColorBlindMode ? Color.Black : Program.Config.ServerMsgColor.ToColor()));
            }
            else if ((MessageType)message.type == MessageType.Message || (MessageType)message.type == MessageType.PrivateMessage || (MessageType)message.type == MessageType.Team)
            {
                string LogText = "";
                if (Program.Config.ShowTimeStamp)
                {
                    WriteText(window, DateTime.Now.ToString("[HH:mm] "), (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                }
                if (message.from.rank > 0)
                {
                    WriteText(window, "[", (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                    if (message.from.rank == 1 || message.from.rank == 4)
                    {
                        WriteText(window, "Dev", (Program.Config.ColorBlindMode ? Color.Black : message.RankColor()));
                        LogText += "[Dev]";
                    }
                    else if (message.from.rank == 2 || message.from.rank == 3)
                    {
                        WriteText(window, "Mod", (Program.Config.ColorBlindMode ? Color.Black : message.RankColor()));
                        LogText += "[Mod]";
                    }
                    else if (message.from.rank == 99)
                    {
                        LogText += "[Dev]";
                        WriteText(window, "Dev", (Program.Config.ColorBlindMode ? Color.Black : message.RankColor()));
                    }
                    WriteText(window, "]", (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                }
                WriteText(window, "[", (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                if (Program.Config.UsernameColors)
                {
                    if (message.from.getUserColor().ToArgb() == Color.Black.ToArgb())
                    {
                        WriteText(window, message.from.username,
                            (Program.Config.ColorBlindMode ? Color.Black : Program.Config.Level0Color.ToColor()));
                    }
                    else
                    {
                        WriteText(window, message.from.username,
                            (Program.Config.ColorBlindMode ? Color.Black : message.from.getUserColor()));
                    }
                }
                else
                {
                    WriteText(window, message.from.username,
                        (Program.Config.ColorBlindMode ? Color.Black : Program.Config.Level0Color.ToColor()));
                }
                WriteText(window, "]: ", (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));

                //if (message.from.rank == 0)
                    WriteText(window, message.message.Trim(), (Program.Config.ColorBlindMode ? Color.Black : message.MessageColor()));
                //else
                //    FormatText(message.message.Trim(), window);
                string LogFile = "";
                string LogDirectory = @"Logs\";
                if ((MessageType)message.type == MessageType.Message)
                {
                    LogDirectory += @"Channel\" + DateTime.Now.ToString("MMMM_yyyy") + @"\";
                    LogText = DateTime.Now.ToString("[HH:mm] ") + LogText;
                    LogFile = message.channel + DateTime.Now.ToString(".dd") + ".txt";
                }
                else if ((MessageType)message.type == MessageType.PrivateMessage) 
                {
                    LogDirectory += @"PMs\"; ;
                    LogText = DateTime.Now.ToString("[dd/MM/yy | HH:mm] ") + LogText;
                    LogFile = message.channel + ".txt";
                }
                else if ((MessageType)message.type == MessageType.Team)
                    LogFile = "Team.txt";
                LogText += "[" + message.from.username + "] " + message.message.Trim();
                if (LogFile != "")
                    if (!Directory.Exists(LogDirectory))
                        Directory.CreateDirectory(LogDirectory);
                    File.AppendAllText(LogDirectory + LogFile, LogText + Environment.NewLine);

            }
            else if ((MessageType)message.type == MessageType.System || (MessageType)message.type == MessageType.Server)
            {
                if (Program.Config.ShowTimeStamp)
                {
                    WriteText(window, DateTime.Now.ToString("[HH:mm] "), (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                }
                WriteText(window,"[", (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                WriteText(window, ((MessageType)message.type).ToString() ,(Program.Config.ColorBlindMode ? Color.Black : message.MessageColor()));
                WriteText(window, "]: ", (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                WriteText(window, message.message, (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
            }
            else
            {
                WriteText(window, (Program.Config.ColorBlindMode ? "[" + (MessageType)message.type + "] " + message.message : message.message),
                    (Program.Config.ColorBlindMode ? Color.Black : message.MessageColor()));
            }

            //window.SelectionStart = window.TextLength;
            //window.SelectionLength = 0;

            if (autoscroll)
                window.ScrollToCaret();
        }

        public static void SendMessage(string chatInput,string channel,bool isprivate)
        {
            Program.ChatServer.SendMessage(isprivate ? MessageType.PrivateMessage : MessageType.Message,
                                           CommandType.None, channel, chatInput);
        }

        private static void WriteText(CustomRTB window, string text, Color color)
        {
            window.Select(window.TextLength, 0);
            window.SelectionColor = color;
            window.AppendText(text);
        }

        private static void FormatText(string message, CustomRTB window)
        {
            window.AppendText(message);
            window.Select(window.Text.Length - message.Length, message.Length);
            string strRTF = window.SelectedRtf;

            int iCTableStart = strRTF.IndexOf("colortbl;", StringComparison.Ordinal);

            if (iCTableStart != -1)
            {
                int iCTableEnd = strRTF.IndexOf('}', iCTableStart);
                strRTF = strRTF.Remove(iCTableStart, iCTableEnd - iCTableStart);

                strRTF = strRTF.Insert(iCTableStart,
                    "{\\colortbl ;\\red" + Program.Config.NormalTextColor.R + "\\green" + Program.Config.NormalTextColor.G + "\\blue" + Program.Config.NormalTextColor.B + ";\\red128\\green0\\blue0;\\red0\\green128\\blue0;\\red0\\green0\\blue255;}");
            }
            else
            {
                int iRTFLoc = strRTF.IndexOf("\\rtf", StringComparison.Ordinal);
                int iInsertLoc = strRTF.IndexOf('{', iRTFLoc);
                if (iInsertLoc == -1) iInsertLoc = strRTF.IndexOf('}', iRTFLoc) - 1;

                strRTF = strRTF.Insert(iInsertLoc,
                    "{\\colortbl ;\\red" + Program.Config.NormalTextColor.R + "\\green" + Program.Config.NormalTextColor.G + "\\blue" + Program.Config.NormalTextColor.B + ";\\red128\\green0\\blue0;\\red0\\green128\\blue0;\\red0\\green0\\blue255;}");
            }
            IEnumerable<string> tags = GetMessageTags(strRTF);

            foreach (string tag in tags)
            {
                strRTF = strRTF.Replace("[" + tag + "]", ChatTags[tag]);
                strRTF = strRTF.Replace("[" + tag.ToLower() + "]", ChatTags[tag]);
                strRTF = strRTF.Replace("[/" + tag + "]", "\\cf1");
                strRTF = strRTF.Replace("[/" + tag.ToLower() + "]", "\\cf1");
            }

            window.SelectedRtf = strRTF;
        }

        private static IEnumerable<string> GetMessageTags(string message)
        {
            return ChatTags.Keys.Where(tag => message.Contains("[" + tag + "]") || message.Contains("[" + tag.ToLower() + "]")).Where(tag => message.Contains("[/" + tag + "]") || message.Contains("[/" + tag.ToLower() + "]")).ToArray();
        }
    }
}
