using System;
using System.IO;

namespace YGOPro_Launcher.Config
{
    public class Configuration
    {
        public string ServerName;
        public string ServerAddress = "85.214.205.124";
        public string UpdaterAddress = "http://dev.ygopro-online.net/launcher/checkversion.php";
        public string ServerInfoAddress = "http://dev.ygopro-online.net/launcher/serverinfo.php";
        public int ServerPort = 6922;
        public int GamePort = 6911;
        public string GameExe = "ygopro_vs.exe";
        public string LauncherDir =  "";
        public string DefaultUsername = "";
        public string DefaultDeck = "";
        public bool EnableSound = true;
        public bool EnableMusic = true;
        public bool Enabled3D = true;
        public int Antialias = 0;
        public bool AutoLogin = false;
        public bool Fullscreen = false;
        public string Password = "";

        //quickhost settings
        public string CardRules = "OCG/TCG";
        public string Mode = "Single";
        public bool EnablePrority = false;
        public bool DisableCheckDeck = false;
        public bool DisableShuffleDeck = false;
        public string Lifepoints = "8000";
        public string GameName = LauncherHelper.GenerateString().Substring(0, 5);
        public bool DebugMode = false;

        public void Load(string configFileName)
        {
            if (!File.Exists(configFileName))
                return;

            var lines = File.ReadAllLines(configFileName);

            foreach (string nonTrimmerLine in lines) {
                string line = nonTrimmerLine.Trim();
                if (line.Equals(string.Empty) || !line.Contains("=") || line.StartsWith("#")) continue;

                string[] data = line.Split('=');
                string variable = data[0].Trim().ToLower();
                string value = data[1].Trim().ToLower();
                switch (variable)
                {
                    case "servername":
                        ServerName = value;
                        break;
                    case "serverport":
                        ServerPort = Convert.ToInt32(value);
                        break;
                    case "serveraddress":
                        ServerAddress = value;
                        break;
                    case "serverinfoaddress":
                        ServerInfoAddress = value;
                        break;
                    case "gameport":
                        GamePort = Convert.ToInt32(value);
                        break;
                    case "gameexe":
                        GameExe = value;
                        break;
                    case "launcherdir":
                        LauncherDir = value;
                        break;
                    case "updateraddress":
                        UpdaterAddress = value;
                        break;
                    case "defualtusername":
                        DefaultUsername = value;
                        break;
                    case "defualtdeck":
                        DefaultDeck = value;
                        break;
                    case "enablesound":
                        EnableSound = Convert.ToBoolean(value);
                        break;
                    case "enablemusic":
                        EnableMusic = Convert.ToBoolean(value);
                        break;
                    case "enabled3d":
                        Enabled3D = Convert.ToBoolean(value);
                        break;
                    case "antialias":
                        Antialias = Convert.ToInt32(value);
                        break;
                    case "fullscreen":
                        Fullscreen = Convert.ToBoolean(value);
                        break;
                    case "cardrules":
                        CardRules = value;
                        break;
                    case "mode":
                        Mode = value;
                        break;
                    case "enablepriority":
                        EnablePrority = Convert.ToBoolean(value);
                        break;
                    case "disablecheckdeck":
                        DisableCheckDeck = Convert.ToBoolean(value);
                        break;
                    case "disableshuffledeck":
                        DisableShuffleDeck = Convert.ToBoolean(value);
                        break;
                    case "lifepoints":
                        Lifepoints = value;
                        break;
                    case "gamename":
                        GameName = value;
                        break;
                    case "debugmode":
                        DebugMode = Convert.ToBoolean(value);
                        break;
                    case "autologin":
                        AutoLogin = Convert.ToBoolean(value);
                        break;
                    case "password":
                        Password = value;
                        break;
                }
            }

            if (DebugMode)
            {
                ServerName = "Debug";
                ServerAddress = "127.0.0.1";
                ServerPort = 8922;
                GamePort = 8911;
                UpdaterAddress = "http://127.0.0.1/launcher/checkversion.php";
                ServerInfoAddress = "http://127.0.0.1/launcher/ServerInfo.php";
            }
        }

        public void Save(string configFileName)
        {
            if ((File.Exists(configFileName)))
                File.Delete(configFileName);

            StreamWriter writer = new StreamWriter(configFileName);

            writer.WriteLine("#Server Settings");
            writer.WriteLine("serveraddress = " + ServerAddress);
            writer.WriteLine("serverport = " + ServerPort);
            writer.WriteLine("gameport = " + GamePort);
            writer.WriteLine("updateraddress = " + UpdaterAddress);
            writer.WriteLine("serverinfoaddress = " + ServerInfoAddress);
            writer.WriteLine("");
            writer.WriteLine("#Game Settings");
            writer.WriteLine("launcherdir = " + LauncherDir);
            writer.WriteLine("gameexe = " + GameExe);
            writer.WriteLine("defualtusername = " + DefaultUsername);
            writer.WriteLine("defualtdeck = " + DefaultDeck);
            writer.WriteLine("antialias = " + Antialias);
            writer.WriteLine("enablesound = " + EnableSound);
            writer.WriteLine("enablemusic = " + EnableMusic);
            writer.WriteLine("enabled3d = " + Enabled3D);
            writer.WriteLine("fullscreen = " + Fullscreen);
            writer.WriteLine("autologin = " + AutoLogin);
            writer.WriteLine("password = " + Password);
            writer.WriteLine("");
            writer.WriteLine("#Quick Host Settings");
            writer.WriteLine("cardrules = " + CardRules);
            writer.WriteLine("mode = " + Mode);
            writer.WriteLine("enableprority = " + EnablePrority);
            writer.WriteLine("disablecheckdeck = " + DisableCheckDeck);
            writer.WriteLine("disableshuffledeck = " + DisableShuffleDeck);
            writer.WriteLine("lifepoints = " + Lifepoints);
            writer.WriteLine("gamename = " + GameName);

            writer.Close();
        }
    }
}