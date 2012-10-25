using System;
using System.IO;
using System.Windows.Forms;

namespace YGOPro_Launcher
{
    public class Configuration
    {
        public string ServerName;
        public string ServerAddress;
        public string UpdaterAddress;
        public string ServerInfoAddress;
        public int ServerPort;
        public int GamePort;
        public string GameEXE;
        public string LauncherDir;
        public string DefualtUsername;
        public string DefualtDeck;
        public bool EnableSound;
        public bool EnableMusic;
        public bool Enabled3d;
        public int Antialias;
        public bool Fullscreen;

        //quickhost settings
        public string CardRules;
        public string Mode;
        public bool EnablePrority;
        public bool DisableCheckDeck;
        public bool DisableShuffleDeck;
        public string Lifepoints;
        public string GameName;
        public bool DebugMode;

        public void Load(string configFileName)
        {
            ServerAddress = "85.214.205.124";
            UpdaterAddress = "http://dev.ygopro-online.net/launcher/checkversion.php";
            ServerInfoAddress = "http://dev.ygopro-online.net/launcher/serverinfo.php";
            ServerPort = 6911;
            GamePort = 6922;
            GameEXE = "ygopro_vs.exe";
            LauncherDir = "";
            ServerName = "YGOPro";
            DefualtUsername = "";
            DefualtDeck = "";
            EnableSound = true;
            EnableMusic = true;
            Enabled3d = true;
            Antialias = 2;
            Fullscreen = false;
            CardRules = "OCG/TCG";
            Mode = "Single";
            EnablePrority = false;
            DisableCheckDeck = false;
            DisableShuffleDeck = false;
            Lifepoints = "8000";
            GameName = LauncherHelper.GenerateString().Substring(0, 5);
            DebugMode = false;

            if (!File.Exists(configFileName))
            {
                MessageBox.Show("File " + configFileName + " was not found. Using default settings.");
                return;
            }

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
            writer.WriteLine("");
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