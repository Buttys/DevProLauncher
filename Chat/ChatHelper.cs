using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YGOPro_Launcher.Chat.Enums;
using System.Drawing;

namespace YGOPro_Launcher.Chat
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

        public static void WriteMessage(ChatMessage message, CustomRTB window,bool autoscroll)
        {
            if (window.Text != "")//start a new line unless theres no text
                window.AppendText(Environment.NewLine);
            window.Select(window.TextLength, 0);
            if (message.Type == MessageType.Message || message.Type == MessageType.PrivateMessage)
            {
                if (Program.Config.ShowTimeStamp)
                    WriteText(window, message.Time.ToString("[HH:mm] "), (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                WriteText(window, "[", (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));
                WriteText(window, (Program.Config.ColorBlindMode && message.From.Rank > 0 ? "[Admin]" + message.From.Username : message.From.Username),
                    (Program.Config.ColorBlindMode ? Color.Black : message.UserColor));
                WriteText(window, "]: ", (Program.Config.ColorBlindMode ? Color.Black : Program.Config.NormalTextColor.ToColor()));

                if (message.From.Rank == 0)
                    WriteText(window, message.FormattedMessage.Trim(), (Program.Config.ColorBlindMode ? Color.Black : message.MessageColor));
                else
                    FormatText(message.FormattedMessage.Trim(),window);

            }
            else if (message.Type == MessageType.System || message.Type == MessageType.Server)
            {
                WriteText(window, "[" + message.Type + "] " + message.FormattedMessage,
                        (Program.Config.ColorBlindMode ? Color.Black : message.MessageColor));
            }
            else
            {
                WriteText(window, (Program.Config.ColorBlindMode ? "[" + message.Type + "] " + message.FormattedMessage : message.FormattedMessage),
                    (Program.Config.ColorBlindMode ? Color.Black : message.MessageColor));
            }

            window.SelectionStart = window.TextLength;
            window.SelectionLength = 0;

            if (autoscroll)
                window.ScrollToCaret();
        }
        private static void WriteText(CustomRTB window, string text, Color color)
        {
            window.Select(window.TextLength, 0);
            window.SelectionColor = color;
            window.AppendText(text);
        }
        private static void FormatText(string message ,CustomRTB window)
        {
            window.AppendText(message);
            window.Select(window.Text.Length - message.Length, message.Length);
            string strRTF = window.SelectedRtf;

            int iCTableStart = strRTF.IndexOf("colortbl;");

            if (iCTableStart != -1)
            {
                int iCTableEnd = strRTF.IndexOf('}', iCTableStart);
                strRTF = strRTF.Remove(iCTableStart, iCTableEnd - iCTableStart);

                strRTF = strRTF.Insert(iCTableStart,
                    "{\\colortbl ;\\red" + Program.Config.NormalTextColor.R + "\\green" + Program.Config.NormalTextColor.G + "\\blue" + Program.Config.NormalTextColor.B + ";\\red128\\green0\\blue0;\\red0\\green128\\blue0;\\red0\\green0\\blue255;}");
            }
            else
            {
                int iRTFLoc = strRTF.IndexOf("\\rtf");
                int iInsertLoc = strRTF.IndexOf('{', iRTFLoc);
                if (iInsertLoc == -1) iInsertLoc = strRTF.IndexOf('}', iRTFLoc) - 1;

                strRTF = strRTF.Insert(iInsertLoc,
                    "{\\colortbl ;\\red" + Program.Config.NormalTextColor.R + "\\green" + Program.Config.NormalTextColor.G + "\\blue" + Program.Config.NormalTextColor.B + ";\\red128\\green0\\blue0;\\red0\\green128\\blue0;\\red0\\green0\\blue255;}");
            }
            string[] tags = GetMessageTags(strRTF);

            foreach (string tag in tags)
            {
                strRTF = strRTF.Replace("[" + tag + "]", ChatTags[tag]);
                strRTF = strRTF.Replace("[" + tag.ToLower() + "]", ChatTags[tag]);
                strRTF = strRTF.Replace("[/" + tag + "]", "\\cf1");
                strRTF = strRTF.Replace("[/" + tag.ToLower() + "]", "\\cf1");
            }

            window.SelectedRtf = strRTF;
        }
        private static string[] GetMessageTags(string message)
        {
            List<string> containedtags = new List<string>();
            foreach (string tag in ChatTags.Keys)
            {
                if (message.Contains("[" + tag + "]") || message.Contains("[" + tag.ToLower() + "]"))
                {
                    if (message.Contains("[/" + tag + "]") || message.Contains("[/" + tag.ToLower() + "]"))
                        containedtags.Add(tag);
                }
            }
            return containedtags.ToArray();
        }
    }
}
