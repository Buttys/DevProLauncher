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
        public string GameExe = "devpro.dll";
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
        public string GameFont = "simhei.ttf"; //only ger
        public int FontSize = 12; //only ger
        public string Language = "English"; // confirm Language
        public bool UseSkin = false;
        public bool AutoPlacing = true;
        public bool RandomPlacing = false;
        public bool AutoChain = true;
        public bool NoChainDelay = false;

        //quickhost settings
        public string BanList = "";
        public string TimeLimit = "3 minutes";
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
                    case "gamefont":
                        GameFont = value;
                        break;
                    case "fontsize":
                        FontSize = Convert.ToInt32(value);
                        break;
                    case "language":
                        Language = value;
                        break;
                    case "fullscreen":
                        Fullscreen = Convert.ToBoolean(value);
                        break;
                    case "cardrules":
                        CardRules = value;
                        break;
                    case "banlist":
                        BanList = value;
                        break;
                    case "timelimit":
                        TimeLimit = value;
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
                        string builtpassword = "";
                        if (data.Length > 2)
                        {
                            for (int i = 1; i < data.Length; i++)
                            {
                                if (data[i] == "")
                                    builtpassword += "=";
                                else
                                    builtpassword += data[i].Trim();
                            }
                            Password = builtpassword;
                        }
                        else
                            Password = value;
                        break;
                    case "useskin":
                        UseSkin = Convert.ToBoolean(value);
                        break;
                    case "autoplacing":
                        AutoPlacing = Convert.ToBoolean(value);
                        break;
                    case "randomplacing":
                        RandomPlacing = Convert.ToBoolean(value);
                        break;
                    case "autochain":
                        AutoChain = Convert.ToBoolean(value);
                        break;
                    case "nochaindelay":
                        NoChainDelay = Convert.ToBoolean(value);
                        break;
                }
            }

            if (DebugMode)
            {
                ServerName = "Debug";
                ServerAddress = "86.0.24.143";
                ServerPort = 7922;
                GamePort = 7911;
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
            writer.WriteLine("language = " + Language);
            writer.WriteLine("textfont = " + GameFont);
            writer.WriteLine("fontsize = " + FontSize);
            writer.WriteLine("useskin = " + UseSkin);
            writer.WriteLine("autoplacing = " + AutoPlacing);
            writer.WriteLine("randomplacing = " + RandomPlacing);
            writer.WriteLine("autochain = " + AutoChain);
            writer.WriteLine("nochaindelay = " + NoChainDelay);
            writer.WriteLine("");
            writer.WriteLine("#Quick Host Settings");
            writer.WriteLine("banlist = " + BanList);
            writer.WriteLine("cardrules = " + CardRules);
            writer.WriteLine("mode = " + Mode);
            writer.WriteLine("timelimit = " + TimeLimit);
            writer.WriteLine("enableprority = " + EnablePrority);
            writer.WriteLine("disablecheckdeck = " + DisableCheckDeck);
            writer.WriteLine("disableshuffledeck = " + DisableShuffleDeck);
            writer.WriteLine("lifepoints = " + Lifepoints);
            writer.WriteLine("gamename = " + GameName);
            if (DebugMode == true)
                writer.WriteLine("debugmode = " + DebugMode);

            writer.Close();
        }
    }
}