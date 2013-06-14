using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevProLauncher.Network.Data
{
    [Serializable]
    public class LogoutData
    {
        public string Username { get; set; }
        public int LoginID { get; set; }
    }
}
