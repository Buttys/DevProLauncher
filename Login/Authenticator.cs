using System.Security.Authentication;
using System;
using System.Windows.Forms;
using YgoServer.NetworkData;
using Newtonsoft.Json;
using YgoServer.Helpers;

namespace YGOPro_Launcher.Login
{
    class Authenticator
    {
        private string _username;
        private readonly string _encodedPassword;
        private readonly NetClient _connection;
        private readonly UserData _userInfo;
        public delegate void Reset();
        public Reset ResetTimeout;

        public Authenticator(string username, string encodedPassword, NetClient connection, UserData userData)
        {
            _username = username;
            _userInfo = userData;
            _encodedPassword = encodedPassword;
            _connection = connection;
            _connection.LoginReply += new NetClient.LoginResponse(LoginResponse);
        }

        public void Authenticate()
        {
            Authenticate(_username, _encodedPassword);
        }

        public void Authenticate(string username, string password)
        {
            if (username == "")
            {
                throw new AuthenticationException("Username can't be empty!");
            }

            _username = username;
           
            //_connection.SendPacket("LOGIN||" + username + "||" + password + "||" + LauncherHelper.GetUID());
            _connection.SendPacket(ServerPackets.Login, 
                UnicodeConverter.GetBytes(
                JsonConvert.SerializeObject(new LoginRequest() { Username = username, Password = password, UID = LauncherHelper.GetUID() })
                ));
        }

        private void LoginResponse(ClientPackets type, LoginData data)
        {
            if (type == ClientPackets.Banned)
            {
                if (ResetTimeout != null)
                    ResetTimeout();
                MessageBox.Show("You are banned.");
            }
            else if (type == ClientPackets.LoginFailed)
            {
                if (ResetTimeout != null)
                    ResetTimeout();
                MessageBox.Show("Incorrect Password or Username.");
            }
            else
            {
                _userInfo.Username = _username;
                _userInfo.LoginKey = data.LoginKey.ToString();
                _userInfo.Rank = data.UserRank;
            }
        }

    }
}
