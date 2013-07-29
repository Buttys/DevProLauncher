using System.Drawing;
using System;
namespace DevProLauncher.Network.Data
{
    [Serializable]
    public class RoomInfos
    {
        public int banListType { get; set; }
        public int timer { get; set; }
        public int rule { get; set; }
        public int mode { get; set; }

        public bool enablePriority { get; set; }
        public bool isNoCheckDeck { get; set; }
        public bool isNoShuffleDeck { get; set; }
        public bool isLocked { get; set; }
        public bool isRanked { get; set; }
        public bool isAnimemode { get; set; }
        public bool isIllegal { get; set; }
        public bool hasStarted { get; set; }

        public int startLp { get; set; }
        public int startHand { get; set; }
        public int drawCount { get; set; }

        public string roomName { get; set; }
        public string[] playerList { get; set; }

        public string server { get; set; }

        public static RoomInfos FromName(string roomName)
        {
            RoomInfos infos = new RoomInfos();

            if (roomName.Length < 15) return null;

            string rules = roomName.Substring(0, 7);

            infos.rule = int.Parse(rules[0].ToString());
            infos.mode = int.Parse(rules[1].ToString());
            infos.banListType = int.Parse(rules[2].ToString());
            infos.timer = int.Parse(rules[3].ToString());
            infos.enablePriority = rules[4] == 'T' || rules[4] == '1';
            infos.isNoCheckDeck = rules[5] == 'T' || rules[5] == '1';
            infos.isNoShuffleDeck = rules[6] == 'T' || rules[6] == '1';

            string data = roomName.Substring(7, roomName.Length - 7);

            if (!data.Contains(",")) return null;

            string[] list = data.Split(',');

            infos.startLp = int.Parse(list[0]);

            infos.startHand = 5;
            infos.drawCount = 1;
            if (list[1] == "RL" || list[1] == "UL")
                infos.isLocked = true;

            if (list[1] == "R" || list[1] == "RL")
            {
                infos.isRanked = true;
            }
            else
            {
                infos.isRanked = false;
            }

            infos.roomName = (list[2] == "" ? GenerateroomName() : list[2]);

            if (infos.rule >= 4) infos.isAnimemode = true;

            if (infos.enablePriority || infos.isNoCheckDeck || infos.isNoShuffleDeck ||
                (infos.mode == 2) ? infos.startLp != 16000 : infos.startLp != 8000 || infos.startHand != 5 || infos.drawCount != 1)
                infos.isIllegal = true;

            return infos;
        }

        public static string GenerateroomName()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("/", "");
            return GuidString.Substring(0, 5);
        }

        public bool Contains(string name)
        {
            if (playerList == null)
                return false;
            foreach (string player in playerList)
                if (player.ToLower().Contains(name.ToLower()))
                    return true;
            return false;
        }

        public string GenerateURI()
        {
            return rule.ToString() + mode + banListType + timer + (enablePriority ? 1 : 0) + (isNoCheckDeck ? 1 : 0) + (isNoShuffleDeck ? 1 : 0) + startLp + "," + (isRanked ? "R" : "U") + "," + roomName;
        }

        public static string GameRule(int rule)
        {
            switch (rule)
            {
                case 0:
                    return "OCG";
                case 1:
                    return "TCG";
                case 2:
                    return "OCG/TCG";
                case 4:
                    return "Anime";
                case 5:
                    return "Turbo";
            }

            return "Unkown";
        }

        public static string GameMode(int mode)
        {
            switch (mode)
            {
                case 0:
                    return "Single";
                case 1:
                    return "Match";
                case 2:
                    return "Tag";
            }

            return "Unkown";
        }
        public static bool CompareRoomInfo(RoomInfos playerinfo,RoomInfos otherroom)
        {
            
            if (playerinfo.rule == otherroom.rule && playerinfo.banListType == otherroom.banListType
                && playerinfo.mode == otherroom.mode && playerinfo.isNoCheckDeck == otherroom.isNoCheckDeck
                && playerinfo.isNoShuffleDeck == otherroom.isNoShuffleDeck && playerinfo.enablePriority == otherroom.enablePriority
                && playerinfo.startLp == otherroom.startLp && playerinfo.timer == otherroom.timer && otherroom.hasStarted == false && otherroom.isLocked == false)
            {
                string[] players = otherroom.playerList;
                if (GameMode(otherroom.mode) == "Tag")
                {
                    if (players.Length < 4)
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (players.Length < 2)
                        return true;
                    else
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        public string GetRoomName()
        {
            return server + "-" + roomName;
        }
    }
}