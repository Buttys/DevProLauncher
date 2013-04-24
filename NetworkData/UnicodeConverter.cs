using System;
using System.Text;

namespace YgoServer.Helpers
{
    public static class UnicodeConverter
    {
        public static byte[] GetBytes(string message, int len)
        {
            byte[] data = new byte[len];
            byte[] messageData = Encoding.Unicode.GetBytes(message);
            int messageLen = messageData.Length < len - 2 ? messageData.Length : len - 2;
            Array.Copy(messageData, data, messageLen);
            return data;
        }

        public static byte[] GetBytes(string message)
        {
            return Encoding.Unicode.GetBytes(message);
        }

        public static string GetString(byte[] data)
        {
            string value = Encoding.Unicode.GetString(data);
            int index = value.IndexOf('\0');
            if (index != -1) value = value.Substring(0, index);
            return value;
        }
    }
}