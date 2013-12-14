using System.Drawing;

namespace DevProLauncher.Config
{
    using System.Collections.Generic;
    using Helpers;

    public class Configuration
    {
        public string ServerName;
        public string ServerAddress = "91.250.87.52";
        public string ChatServerAddress = "91.250.87.52";
        public string UpdaterAddress = "http://ygopro.de/launcher/version.php";
        public string ServerInfoAddress = "http://ygopro.de/launcher/server.php";
        public int ServerPort = 6922;
        public string DefaultServer = "";
        
        public int GamePort = 8911;
        public int ChatPort = 8933;
        public string GameExe = "devpro.dll";
        public string LauncherDir =  "";
        public string DefaultUsername = "";
        public string DefaultDeck = "";
        public bool EnableSound = true;
        public bool EnableMusic = true;
        public bool Enabled3D = false;
        public int Antialias = 0;
        public bool AutoLogin = false;
        public bool Fullscreen = false;
        public bool SavePassword = false;
        public string EncodedPassword = "";
        public string SavedUsername = "";
        public string GameFont = "simhei.ttf"; //only ger
        public string ChatFont = "";
        public int FontSize = 12; //only ger
        public decimal ChatSize = 8.25m;
        public string Language = "English"; // confirm Language
        public int Skin = -1;
        public bool AutoPlacing = true;
        public bool RandomPlacing = false;
        public bool AutoChain = true;
        public bool NoChainDelay = false;
        public bool EnableCustomSleeves = true;
        public string IgnoreList = "";
        public string DefaultChannel = "";

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

        //chat settings
        public bool HideJoinLeave = true;
        public bool ColorBlindMode = false;
        public bool ShowTimeStamp = false;
        public bool RefuseDuelRequests = false;
        public bool PmWindows = true;
        public bool UsernameColors = true;
        public bool RefuseTeamInvites = false;

        public SerializableColor ChatBGColor = new SerializableColor(Color.White);
        public SerializableColor NormalTextColor = new SerializableColor(Color.Black);
        public SerializableColor Level99Color = new SerializableColor(Color.Green);
        public SerializableColor Level4Color = new SerializableColor(Color.Red);
        public SerializableColor Level3Color = new SerializableColor(Color.Blue);
        public SerializableColor Level2Color = new SerializableColor(Color.IndianRed);
        public SerializableColor Level1Color = new SerializableColor(Color.RoyalBlue);
        public SerializableColor Level0Color = new SerializableColor(Color.Black);
        public SerializableColor ServerMsgColor = new SerializableColor(Color.Red);
        public SerializableColor MeMsgColor = new SerializableColor(Color.DeepPink);
        public SerializableColor JoinColor = new SerializableColor(Color.Green);
        public SerializableColor LeaveColor = new SerializableColor(Color.Gray);
        public SerializableColor SystemColor = new SerializableColor(Color.Purple);

        public string chtBanList = "";
        public string chtTimeLimit = "3 minutes";
        public string chtCardRules = "OCG/TCG";
        public string chtMode = "Single";
        public bool chtEnablePrority = false;
        public bool chtDisableCheckDeck = false;
        public bool chtDisableShuffleDeck = false;
        public string chtLifepoints = "8000";
        public string chtGameName = LauncherHelper.GenerateString().Substring(0, 5);
        public bool ConfigReset181000 = true;
        public bool NewUpdate = false;

        public List<string> ChatChannels = new List<string>();

        // DropBox Settings
        public string AppKey = "xxvyeb0w8ndl3kz";
        public string AppSecret = "sn6ggwgxk3zdsc6";
    }
}
