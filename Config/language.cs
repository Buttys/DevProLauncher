using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YGOPro_Launcher
{
    public class Language
    {
        // Quickhost frm
        public string QuickHostSetting, QuickHostBtn;

        // Program.cs
        public string pmsbProgRun, pMsbErrorToServer, pMsbBadLog, pMsbOldVers, pMsbException;

        // Login_frm.cs
        public string LoginUserName;
        public string LoginPassWord;
        public string LoginLanguage;
        public string LoginLoginButton;
        public string LoginRegisterButton;
        public string LoginAutoLogin;
        public string LoginMsb1;
        public string LoginMsb2;
        public string LoginMsb3;

        // Register_frm.cs
        public string RegistLbUser;
        public string RegistLbPw;
        public string RegistLbPw2;
        public string RegistBtnRegister;
        public string RegistBtnCancel;
        public string RegistMsb1;
        public string RegistMsb2;
        public string RegistMsb3;
        public string RegistMsb4;
        public string RegistMsb5;

        // ServerTabDesign_frm
        public string GameServerInfo;
        public string GameofRooms;
        public string GameofUnranked;
        public string GameofRanked;
        public string GameofOpenRooms;
        public string GameofPlayers;
        public string GameFilterActive;
        public string GameColumnRoomName, GameColumnType, GameColumnRules, GameColumnMode, GameColumnState, GameColumnPlayers;

        // Option_frm.cs
        public string optionGb1, optionGb2, optionGb3, optionMsbForget;
        public string optionUser, optionDeck, optionBtnAutoLogin, optionAntialias, optionCbSound, optionCbMusic;
        public string optionCbDirect, optionCbFull, optionTexts, optionTextf, optionBtnQuick, optionBtnSave, optionBtnCancel;

        // Host_frm.cs
        public string hostGb1, hostGb2;
        public string hostRules, hostMode;
        public string hostPrio, hostCheckDeck, hostShuffle;
        public string hostLifep, hostGameN, hostBtnHost, hostBtnCancel;

        // FileManager_frm.cs
        public string fileBtnRename, fileBtnDelete, fileBtnFolder, fileBtnGame, fileMsgNoExist, fileMsgNoSelect;
        public string fileMsbMulti, fileAskDelete, fileNewName, fileInputConfirm;

        public void Load(string languageFileName)
        {
            if (!File.Exists(languageFileName))
                return;

            var lines = File.ReadAllLines(languageFileName, System.Text.Encoding.Default);

            foreach (string nonTrimmerLine in lines)
            {
                string line = nonTrimmerLine.Trim();
                if (line.Equals(string.Empty) || !line.Contains("=") || line.StartsWith("#")) continue;

                string[] data = line.Split('=');
                string variable = data[0].Trim().ToLower();
                string value = data[1].Trim();
                switch (variable)
                {
                    // Quick Host
                    case "quickhostsetting": QuickHostSetting = value;
                        break;
                    case "quickhostbtn": QuickHostBtn = value;
                        break;

                    // Program.cs
                    case "pmsbprogrun": pmsbProgRun = value;
                        break;
                    case "pmsberrortoserver": pMsbErrorToServer = value;
                        break;
                    case "pmsbbadlog": pMsbBadLog = value;
                        break;
                    case "pmsboldvers": pMsbOldVers = value;
                        break;
                    case "pmsbexception": pMsbException = value;
                        break;

                    // Login_frm.cs
                    case "loginusername": LoginUserName = value;
                        break;
                    case "loginpassword": LoginPassWord = value;
                        break;
                    case "loginlanguage": LoginLanguage = value;
                        break;
                    case "loginloginbutton": LoginLoginButton = value;
                        break;
                    case "loginregisterbutton": LoginRegisterButton = value;
                        break;
                    case "loginautologin": LoginAutoLogin = value;
                        break;
                    case "loginmsb1": LoginMsb1 = value;
                        break;
                    case "loginmsb2": LoginMsb2 = value;
                        break;
                    case "loginmsb3": LoginMsb3 = value;
                        break;

                    // Register_frm.cs
                    case "registlbuser": RegistLbUser = value;
                        break;
                    case "registlbpw":  RegistLbPw = value;
                        break;
                    case "registlbpw2":  RegistLbPw2 = value;
                        break;
                    case "registbtnregister": RegistBtnRegister = value;
                        break;
                    case "registbtncancel": RegistBtnCancel = value;
                        break;
                    case "registmsb1": RegistMsb1 = value;
                        break;
                    case "registmsb2": RegistMsb2 = value;
                        break;
                    case "registmsb3": RegistMsb3 = value;
                        break;
                    case "registmsb4": RegistMsb4 = value;
                        break;
                    case "registmsb5": RegistMsb5 = value;
                        break;

                    // ServerTabDesign_frm.cs
                    case "gameserverinfo":  GameServerInfo = value;
                        break;
                    case "gameofrooms": GameofRooms = value;
                        break;
                    case "gameofunranked": GameofUnranked = value;
                        break;
                    case "gameofranked":  GameofRanked = value;
                        break;
                    case "gameofopenrooms": GameofOpenRooms = value;
                        break;
                    case "gameofplayers": GameofPlayers = value;
                        break;
                    case "gamefilteractive":  GameFilterActive = value;
                        break;
                    case "gamecolumnroomname": GameColumnRoomName = value;
                        break;
                    case "gamecolumntype":  GameColumnType = value;
                        break;
                    case "gamecolumnrules":  GameColumnRules = value;
                        break;
                    case "gamecolumnmode": GameColumnMode = value;
                        break;
                    case "gamecolumnstate": GameColumnState = value;
                        break;
                    case "gamecolumnplayers": GameColumnPlayers = value;
                        break;

                    // Options_frm.cs
                    case "optiongb1": optionGb1 = value;
                        break;
                    case "optiongb2": optionGb2 = value;
                        break;
                    case "optiongb3": optionGb3 = value;
                        break;
                    case "optionuser": optionUser = value;
                        break;
                    case "optiondeck": optionDeck = value;
                        break;
                    case "optionbtnautologin": optionBtnAutoLogin = value;
                        break;
                    case "optionantialias": optionAntialias = value;
                        break;
                    case "optioncbsound": optionCbSound = value;
                        break;
                    case "optioncbmusic": optionCbMusic = value;
                        break;
                    case "optioncbdirect": optionCbDirect = value;
                        break;
                    case "optioncbfull": optionCbFull = value;
                        break;
                    case "optiontexts": optionTexts = value;
                        break;
                    case "optiontextf": optionTextf = value;
                        break;
                    case "optionbtnquick": optionBtnQuick = value;
                        break;
                    case "optionbtnsave": optionBtnSave = value;
                        break;
                    case "optionbtncancel": optionBtnCancel = value;
                        break;
                    case "optionmsbforget": optionMsbForget = value;
                        break; 
                    
                    // Host_frm.cs
                    case "hostgb1": hostGb1 = value;
                        break;
                    case "hostgb2": hostGb2 = value;
                        break;
                    case "hostrules": hostRules = value;
                        break;
                    case "hostmode": hostMode = value;
                        break;
                    case "hostprio": hostPrio = value;
                        break;
                    case "hostcheckdeck": hostCheckDeck = value;
                        break;
                    case "hostshuffle": hostShuffle = value;
                        break;
                    case "hostlifep": hostLifep = value;
                        break;
                    case "hostgamen": hostGameN = value;
                        break;
                    case "hostbtnhost": hostBtnHost = value;
                        break;
                    case "hostbtncancel": hostBtnCancel = value;
                        break;

                    // FileManager_frm.cs
                    case "filebtnrename": fileBtnRename = value;
                        break;
                    case "filebtndelete": fileBtnDelete = value;
                        break;
                    case "filebtnfolder": fileBtnFolder = value;
                        break;
                    case "filebtngame": fileBtnGame = value;
                        break;
                    case "filemsgnoexist": fileMsgNoExist = value;
                        break;
                    case "filemsgnoselect": fileMsgNoSelect = value;
                        break;
                    case "filemsbmulti": fileMsbMulti = value;
                        break;
                    case "fileaskdelete": fileAskDelete = value;
                        break;
                    case "filenewname": fileNewName = value;
                        break;
                    case "fileinputconfirm": fileInputConfirm = value;
                        break;
                }
            }
        }
    }
}
