using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace YGOPro_Launcher.Config
{
    public class Configuration
    {
        public string ServerName;
        public string ServerAddress = "85.214.205.124";
        public string ChatServerAddress = "85.214.205.124";
        public string UpdaterAddress = "http://dev.ygopro-online.net/launcher/checkversion.php";
        public string ServerInfoAddress = "http://dev.ygopro-online.net/launcher/serverinfo.php";
        public int ServerPort = 6922;
        
        public int GamePort = 6911;
        public int ChatPort = 6666;
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
        public string IgnoreList = "";

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
        public bool PmWindows = true;
        public bool ColorBlindMode = false;
        public bool ShowTimeStamp = false;
        public bool RefuseDuelRequests = false;

        public string ChatBGColor = "White";
        public string NormalTextColor = "Black";
        public string Level99Color = "Green";
        public string Level2Color = "Red";
        public string Level1Color = "RoyalBlue";
        public string Level0Color = "Black";
        public string ServerMsgColor = "Red";
        public string MeMsgColor = "DeepPink";
        public string JoinColor = "Green";
        public string LeaveColor = "Gray";
        public string SystemColor = "Purple";

        public string chtBanList = "";
        public string chtTimeLimit = "3 minutes";
        public string chtCardRules = "OCG/TCG";
        public string chtMode = "Single";
        public bool chtEnablePrority = false;
        public bool chtDisableCheckDeck = false;
        public bool chtDisableShuffleDeck = false;
        public string chtLifepoints = "8000";
        public string chtGameName = LauncherHelper.GenerateString().Substring(0, 5);


    }
}