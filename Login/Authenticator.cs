using System.Security.Authentication;
using System;
using System.Windows.Forms;

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
            _connection.LoginReply += new NetClient.ServerResponse(LoginResponse);
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
           
            _connection.SendPacket("LOGIN|" + username + "|" + password);
        }

        private void LoginResponse(string message)
        {
            if (message == "") return;
            string[] args = message.Split('|');
          
            if (args[0] == "Banned")
            {               
                if (ResetTimeout != null)
                    ResetTimeout();
                MessageBox.Show("You are banned.");
            }
            else if (args[0] == "Failed")
            {    
                if (ResetTimeout != null)
                    ResetTimeout();
                MessageBox.Show("Incorrect Password or Username.");
            }
            else if (args[0] == "LoginDown")
            {
                if (ResetTimeout != null)
                    ResetTimeout();
                MessageBox.Show("Login Server is down.");
            }
            else
            {
                _userInfo.Username = _username;
                _userInfo.Rank = Int32.Parse(args[1]);
                _userInfo.LoginKey = args[0];
            }
        }

    }
}
