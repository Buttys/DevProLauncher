using System;
namespace YGOPro_Launcher.Config
{
    [Serializable]
    public class LanguageInfo
    {
        // Quickhost frm
        public string QuickHostSetting = "Quick Host Settings";
        public string QuickHostBtn = "Confirm";

        // Program.cs
        public string pmsbProgRun = "Program already running";
        public string pMsbErrorToServer = "Error Connecting to server";
        public string pMsbBadLog = "Bad Login";
        public string pMsbOldVers = "You have a too old version of the launcher. Please reinstall it.";
        public string pMsbException = "Oooops! Something bad happened! Software will now exit!";

        // Login_frm.cs
        public string LoginUserName = "Username";
        public string LoginPassWord = "Password";
        public string LoginLanguage = "Language";
        public string LoginLoginButton = "Login";
        public string LoginRegisterButton = "Register";
        public string LoginAutoLogin = "Log me in automatically next time";
        public string LoginMsb1 = "Not connected to the server.";
        public string LoginMsb2 = "Please enter username.";
        public string LoginMsb3 = "Please enter password.";

        // Register_frm.cs
        public string RegistLbUser = "Username";
        public string RegistLbPw = "Password";
        public string RegistLbPw2 = "Confirm Password";
        public string RegistBtnRegister = "Register";
        public string RegistBtnCancel = "Cancel";
        public string RegistMsb1 = "Please confirm your password";
        public string RegistMsb2 = "Please enter password";
        public string RegistMsb3 = "Please enter username.";
        public string RegistMsb4 = "Registering Complete!";
        public string RegistMsb5 = "Username Exists";

        // ServerTabDesign_frm
        public string GameServerInfo = "Server Details";
        public string GameTabRanked = "Ranked";
        public string GameTabUnranked = "Unranked";
        public string GameofRooms = "of Rooms";
        public string GameofUnranked = "of Unranked";
        public string GameofRanked = "of Ranked";
        public string GameofOpenRooms = "of Open Rooms";
        public string GameofPlayers = "of Players";
        public string GameFilterActive = "Filter Active Games";
        public string GameColumnRoomName = "Room Name";
        public string GameColumnType = "Type";
        public string GameColumnRules = "Rules";
        public string GameColumnMode = "Mode";
        public string GameColumnState = "State";
        public string GameColumnPlayers = "Players";
        public string GameColumnBanList = "Banlist";
        public string GameColumnTimer = "Timer";
        public string GameBtnDeck = "Deck Edit"; 
        public string GameBtnReplay = "Replays";
        public string GameBtnOption = "Options";
        public string GameBtnProfile = "Profile";
        public string GameBtnHost = "Host";
        public string GameBtnQuick = "Quick";
        public string GameLabWLD = "Win/Lose/Draw";
        public string GameLabDeck = "Deck:";
        public string GameLabUser = "Username:";

        // Option_frm.cs
        public string optionGb1 = "User Settings";
        public string optionGb2 = "Game Settings";
        public string optionGb3 = "Font Settings";
        public string optionGb4 = "Luancher Settings";
        public string optionMsbForget = "Do you really want to forget auto login credentials?";
        public string optionUser = "Default Username";
        public string optionDeck = "Defualt Deck";
        public string optionBtnAutoLogin = "Forget Auto Login";
        public string optionAntialias = "Antialias";
        public string optionCbSound = "Enable Sound";
        public string optionCbMusic = "Enable Music";
        public string optionCbDirect = "Enable Directx";
        public string optionCbFull = "Fullscreen";
        public string optionTexts = "Font Size";
        public string optionTextf = "Game Font";
        public string optionBtnQuick = "Quick Host Settings";
        public string optionBtnSave = "Save";
        public string optionBtnCancel = "Cancel";
        public string optionCbCustomSkin = "Use Custom Skin";
        public string optionCbAutoPlacing = "Auto Card Placing";
        public string optionCbRandomPlacing = "Random Card Placing";
        public string optionCbAutoChain = "Auto Chain Order";
        public string optionCbNoChainDelay = "No Delay for Chain";

        // Host_frm.cs
        public string hostGb1 = "Settings";
        public string hostGb2 = "Additional Options";
        public string hostBanlist = "Ban List";
        public string hostTimeLimit = "Time Limit";
        public string hostRules = "Card Rules";
        public string hostMode = "Duel Mode";
        public string hostPrio = "Enable Priority";
        public string hostCheckDeck = "Disable Check Deck";
        public string hostShuffle = "Disable Shuffle Deck";
        public string hostLifep = "Lifepoints";
        public string hostGameN = "Game Name";
        public string hostBtnHost = "Host";
        public string hostBtnCancel = "Cancel";

        // FileManager_frm.cs
        public string fileBtnRename = "Rename";
        public string fileBtnDelete = "Delete";
        public string fileBtnFolder = "Open Folder";
        public string fileBtnGame = "Open Game";
        public string fileMsgNoExist = "does no exsist";
        public string fileMsgNoSelect = "Nothing selected";
        public string fileMsbMulti = "Can't Rename multiple items";
        public string fileAskDelete = "Are you sure you want to delete the following item(s) ";
        public string fileNewName = "Enter new name";
        public string fileInputConfirm = "Rename";
        public string fileBtnRefresh = "Refresh";
        public string fileBtnImport = "Import";

        // About_frm.cs
        public string aboutLabel1 = "About DevPro";
        public string aAboutText = "The YGOPro Online Development Group is a rag tag team of programmers, philanthropist, and generally knowledgeable people in the YGOPro community that seek to better and progress the Yu-Gi-Oh! Online community as a whole by providing services and software to allow duelist to grow by dueling and forming lasting friendships.";
        public string aboutLabel5 = "DevPro Contributors";

        // Customize_frm.cs
        public string cusRemoveBtn = "Remove";
        public string cusAddThemeBtn = "Add";
        public string cusAddContentBtn = "Add";
        public string cusPreview = "Preview";
        public string cusBackup = "Back Up";
        public string cusLabelTheme = "Theme";
        public string cusLabelCont = "Content";

        //Profile_frm.cs
        public string profileGb1 = "User Info";
        public string profileGb2 = "Unranked";
        public string profileGb3 = "Ranked";
        public string profileGb4 = "Win Stats";
        public string profileGb5 = "Lose Stats";
        public string profileLblLP = "LP Reached 0";
        public string profileLblSurrendered = "Surrendered";
        public string profileLbl0Cards = "0 Cards Left";
        public string profileLblTimeLimit = "Time Limit Up";
        public string profileLblDisconnect = "Rage Quit/D/C";
        public string profileLblExodia = "Exodia";
        public string profileLblFinalCountdown = "Final Countdown";
        public string profileLblVennominaga = "Vennominaga";
        public string profileLblHorakhty = "Horakhty";
        public string profileLblExodius = "Exodius";
        public string profileLblDestinyBoard = "Destiny Board";
        public string profileLblLastTurn = "Last Turn";
        public string profileLblDestinyLeo = "Destiny Leo";
        public string profileLblUnknown = "Unknown";
        public string profileLblUsername = "Username: ";
        public string profileLblwld = "Win/Lose/Draw: ";
        public string profileLblRank = "Rank: ";
        public string profileLblTeam = "Team: ";
    }
}
