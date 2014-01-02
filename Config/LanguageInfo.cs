using System;
namespace DevProLauncher.Config
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
        public string LoginSavePass = "Save Password";

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
        public string RegistMsb5 = "This username has been taken.";
        public string RegistMsb6 = "Username cannot contain special characters";
        public string RegistMsb7 = "Email address reqired.";
        public string RegistMsb8 = "Invalid email address.";

        // HubGameList_frm

        public string GameRanked = "Ranked";
        public string GameUnranked = "Unranked";
        public string GameDefualtDeck = "Default Deck";
        public string GameSearch = "Search";
        public string GameFormat = "Format";
        public string GameType = "Game Type";
        public string GameBanList = "Banlist";
        public string GameTimeLimit = "Time Limit";
        public string GameActive = "Active Games";
        public string GameIlligal = "Illegal Games";
        public string GameUserFilter = "User Filter";
        public string GameBtnSearch = "Search";
        public string GameBtnHost = "Host";
        public string GameBtnQuick = "Quick";
        public string GameNotUpdating = "Status: Not Updating";
        public string GameUpdating1 = "Status: Updating for ";
        public string GameUpdating2 = " seconds.";
        public string GameSpectate = "Random Spectate";

        public string GameNoServers = "No servers are available.";


        public string GamePasswordExsists = "A room with that password already exists.";
        public string GameLocked = "Locked";
        public string GameOpen = "Open";
        public string GameLPChange = "Your quick host settings does not follow the recommend lifepoints rule, do you want to change this to the default?";
        public string GameEnterPassword = "Enter password";
        public string GameWrongPassword = "Incorrect password";


        // Option_frm.cs
        public string optionGb1 = "User Settings";
        public string optionGb2 = "Game Settings";
        public string optionGb3 = "Font Settings";
        public string optionGb4 = "Launcher Settings";
        public string optionMsbForget = "Do you really want to forget auto login credentials?";
        public string optionUser = "Default Username";
        public string optionDeck = "Default Deck";
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
        public string optionCbEnableSleeves = "Enable User Covers";

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
        public string hostpassword = "Password";

        // FileManager_frm.cs
        public string fileBtnRename = "Rename";
        public string fileBtnDelete = "Delete";
        public string fileBtnFolder = "Open Folder";
        public string fileBtnGame = "Open Game";
        public string fileMsgNoExist = "Does not exist";
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
        public string profileName = "Profile";
        public string profileWin = "Win";
        public string profileLose = "Lose";
        public string profileGb1 = "User Info";
        public string profileGb2 = "Stats";
        public string profileGb3 = "Condition";
        public string profileGb4 = "Ranked";
        public string profileGb5 = "Unranked";
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
        public string profileLblwld = "Win/Lose/Draw";
        public string profileLblRank = "Rank: ";
        public string profileLblTeam = "Team: ";

        //ChatOptions_frm.cs

        public string chatoptionsFormName = "Chat Options";
        public string chatoptionsGb1 = "Chat Settings";
        public string chatoptionsGb2 = "Other Settings";
        public string chatoptionsGb3 = "Color Style";
        public string chatoptionsGb4 = "Channel Settings";
        public string chatoptionsLblHideJoinLeave = "Hide Join/Leave";
        public string chatoptionsLblPmWindows = "PM Windows";
        public string chatoptionsLblColorBlindMode = "Color Blind Mode";
        public string chatoptionsLblShowTimeStamp = "Show Time Stamp";
        public string chatoptionsLblRefuseDuelRequests = "Refuse Duel Requests";
        public string chatoptionsLblChatBackground = "Chat Background";
        public string chatoptionsLblNormalText = "Normal Text";
        public string chatoptionsLblLevel99 = "Level 99";
        public string chatoptionsLblLevel2 = "Level 2";
        public string chatoptionsLblLevel1 = "Level 1";
        public string chatoptionsLblNormalUser = "Normal Username";
        public string chatoptionsLblServerMessages = "Server Message";
        public string chatoptionsLblMeMessage = "/Me Message";
        public string chatoptionsLblJoin = "Join";
        public string chatoptionsLblLeave = "Leave";
        public string chatoptionsLblSystem = "System";
        public string chatoptionsBtnRequestSettings = "Request Settings";
        public string chatoptionsBtnSave = "Save";
        public string chatoptionsBtnCancel = "Cancel";
        public string chatoptionsRequestFormText = "Duel Request Settings";

        //Chat_frm.cs

        public string chatBtnoptions = "Settings";
        public string chatTabUsers = "Users";
        public string chatTabFriends = "Friends";
        public string chatTabIgnore = "Ignore";
        public string chatUserCount = "User Count: ";
        public string chatAddFriend = "Add to Friends";
        public string chatRequestDuel = "Request Duel";
        public string chatIgnoreUser = "Ignore User";
        public string chatViewProfile = "View Profile";
        public string chatRemoveFriend = "Remove Friend";
        public string chatRemoveIgnore = "Remove";
        public string chatAutoScroll = "Auto-Scroll";

        //Main_frm.cs

        public string MainFormTitle = "YGOPro Launcher ";
        public string MainChatTab = "Chat (Beta)";
        public string MainRankingTab = "Ranking";
        public string MainTornyTab = "Tournament Room";
        public string MainYoutubeTab = "Youtube";
        public string MainFileManagerTab = "File Manager";
        public string MainFileDeckTab = "Decks";
        public string MainFileReplayTab = "Replays";
        public string MainCustomizeTab = "Customize";
        public string MainAboutTab = "About";

        public string MainSyncBtn = "Dropbox Sync";
        public string MainSiteBtn = "Site";
        public string MainDeckBtn = "Deck Edit";
        public string MainReplaysBtn = "Replays";
        public string MainOfflineBtn = "Offline";
        public string MainProfileBtn = "Profile";
        public string MainServerMessage = "Server messages will spawn here.";

        //DuelRequest_frm
        public string DuelReqestMessage = " has challenged you to a ranked duel! Do you accept?";
        public string DuelRequestBanlist = " Banlist: ";
        public string DuelRequestRules = " Rules: ";
        public string DuelRequestMode = " Type: ";

        //DevPro SupportTab
        public string SupportItem1Name = "Donator Status";
        public string SupportItem1Des = "Unlocks Donator status in chat, Orange username and additional chat commands.";
        public string SupportItem2Name = "Change Username";
        public string SupportItem2Des = "Change your account name while retaining all your ranked stats.";
        public string SupportItem3Name = "Reset Ranking Stats";
        public string SupportItem3Des = "Reset your ranking stats.";
        public string SupportItem4Name = "Unban User";
        public string SupportItem4Des = "Unban a user who was banned.";
        public string SupportItem5Name = "Custom Username Color";
        public string SupportItem5Des = "Change the color of your username in chat.";
        public string SupportItem6Name = "Create Team";
        public string SupportItem6Des = "Create a team.";
        public string SupportItem7Name = "Send Server Message";
        public string SupportItem7Des = "Send a server message to everyone.(Limited: 3 Messages per day)";
        public string SupportItem8Name = "Custom Sleeves";
        public string SupportItem8Des = "Upload a custom sleeve that all users will be able to see in duels.";

        public string SupportBalance = "Balance";
        public string Supportgb2 = "Support DevPro";
        public string Supportgb2text = "The DevPoint system is here to reward users who help out and support DevPro. While the 2 options below will earn you DevPoints you can also earn points for helping DevPro too!  || ||- Taking part in DevPro Events  - Fixing DevPro bugs Launcher or card scripts! || - Supporting the growth of DevPro";
        public string Supportgb3 = "Earn DevPoints";
        public string Supportgb3text = "Earn points by completing offfers.";
        public string Supportgb4 = "Donate!";
        public string Supportgb4text = "Earn by donating (Paypal, PaySafe, SMS and more)";

        public string SupportRenameInput = "Enter New Username";
        public string SupportUnbanInput = "Enter Username";
        public string SupportTeamNameInput = "Enter Team Name";
        public string SupportMSGInput = "Enter Message";
    }
}
