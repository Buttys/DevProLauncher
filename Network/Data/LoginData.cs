using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevProLauncher.Network.Data
{
    [Serializable]
    public class LoginData
    {
        public int LoginKey { get; set; }
        public int UserRank { get; set; }
    }
}
