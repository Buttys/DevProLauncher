using System;

namespace DevProLauncher.Network.Data
{
    [Serializable]
    public class ChannelData
    {
        public string name { get; set; }
        public int userCount { get; set; }
        public bool isPrivate { get; set; }
        //public string[] users { get; set; }

        public ChannelData()
        {
            isPrivate = false;
        }
    }
}
