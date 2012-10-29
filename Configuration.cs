using System;
using System.IO;
using System.Windows.Forms;

namespace YGOPro_Launcher
{
    public class Configuration
    {
        public string ServerName;
        public string ServerAddress = "85.214.205.124";
        public string UpdaterAddress = "http://dev.ygopro-online.net/launcher/checkversion.php";
        public string ServerInfoAddress = "http://dev.ygopro-online.net/launcher/serverinfo.php";
        public int ServerPort = 6911;
        public int GamePort = 6922;
        public string GameEXE = "ygopro_vs.exe";
        public string LauncherDir =  "";
        public string DefualtUsername = "";
        public string DefualtDeck = "";
        public bool EnableSound = true;
        public bool EnableMusic = true;
        public bool Enabled3d = true;
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

            StreamReader reader = new StreamReader(File.OpenRead(configFileName));
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null) continue;
                line = line.Trim();
                if (line.Equals(string.Empty)) continue;
                if (!line.Contains("=")) continue;
                if (line.StartsWith("#")) continue;

                string[] data = line.Split(new char[] {'='}, 2);
                string variable = data[0].Trim().ToLower();
                string value = data[1].Trim();
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
                        GameEXE = value;
                        break;
                    case "launcherdir":
                        LauncherDir = value;
                        break;
                    case "updateraddress":
                        UpdaterAddress = value;
                        break;
                    case "defualtusername":
                        DefualtUsername = value;
                        break;
                    case "defualtdeck":
                        DefualtDeck = value;
                        break;
                    case "enablesound":
                        EnableSound = Convert.ToBoolean(value);
                        break;
                    case "enablemusic":
                        EnableMusic = Convert.ToBoolean(value);
                        break;
                    case "enabled3d":
                        Enabled3d = Convert.ToBoolean(value);
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
            reader.Close();

            if (DebugMode)
            {
                ServerAddress = "127.0.0.1";
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
            writer.WriteLine("gameexe = " + GameEXE);
            writer.WriteLine("defualtusername = " + DefualtUsername);
            writer.WriteLine("defualtdeck = " + DefualtDeck);
            writer.WriteLine("antialias = " + Antialias);
            writer.WriteLine("enablesound = " + (EnableSound ? "true" : "false"));
            writer.WriteLine("enablemusic = " + (EnableMusic ? "true" : "false"));
            writer.WriteLine("enabled3d = " + (Enabled3d ? "true" : "false"));
            writer.WriteLine("fullscreen = " + (Fullscreen ? "true" : "false"));
            writer.WriteLine("autologin = " + (AutoLogin ? "true" : "false"));
            writer.WriteLine("password = " + Password);
            writer.WriteLine("#Quick Host Settings");
            writer.WriteLine("cardrules = " + CardRules);
            writer.WriteLine("mode = " + Mode);
            writer.WriteLine("enableprority = " + (EnablePrority ? "true":"false"));
            writer.WriteLine("disablecheckdeck = " + (DisableCheckDeck ? "true" : "false"));
            writer.WriteLine("disableshuffledeck = " + (DisableShuffleDeck ? "true" : "false"));
            writer.WriteLine("lifepoints = " + Lifepoints);
            writer.WriteLine("gamename = " + GameName);

            writer.Close();
        }
    }
}