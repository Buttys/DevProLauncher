using System.Drawing;
namespace YgoServer.NetworkData
{
    public class RoomInfos
    {
        public int banListType;
        public int timer;
        public int rule;
        public int mode;

        public bool enablePriority;
        public bool isNoCheckDeck;
        public bool isNoShuffleDeck;
        public bool isLocked;
        public bool isRanked;
        public bool isAnimemode;
        public bool isIllegal;
        public bool hasStarted;

        public int startLp;
        public int startHand;
        public int drawCount;

        public string roomName;
        public string[] playerList;

        public static RoomInfos FromName(string roomname, bool started)
        {
            RoomInfos infos = new RoomInfos();

            // if (roomname.Length < 15) return null;

            string rules = roomname.Substring(0, 7);

            if (!int.TryParse(rules[0].ToString(), out infos.rule))
                return null;
            if (!int.TryParse(rules[1].ToString(), out infos.mode))
                return null;
            if (!int.TryParse(rules[2].ToString(), out infos.banListType))
                return null;
            if (!int.TryParse(rules[3].ToString(), out infos.timer))
                return null;
            infos.enablePriority = rules[4] == 'T' || rules[4] == '1';
            infos.isNoCheckDeck = rules[5] == 'T' || rules[5] == '1';
            infos.isNoShuffleDeck = rules[6] == 'T' || rules[6] == '1';

            string data = roomname.Substring(7, roomname.Length - 7);

            if (!data.Contains(",")) return null;

            string[] list = data.Split(',');

            if (!int.TryParse(list[0], out infos.startLp))
                return null;
            infos.startHand = 5;
            infos.drawCount = 1;

            if (list[1] == "RL" || list[1] == "UL")
                infos.isLocked = true;

            if (list[1] == "R" || list[1] == "RL")
                infos.isRanked = true;
            else
                infos.isRanked = false;

            infos.roomName = list[2];

            //infos.Players = players;
            infos.hasStarted = started;

            return infos;
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

        public string GenerateURI(string server, int port)
        {
            return "ygpro:/" + server + "/" + port + "/" + rule + mode + banListType + timer + (enablePriority ? 1 : 0) + (isNoCheckDeck ? 1 : 0) + (isNoShuffleDeck ? 1 : 0) + startLp + "," + (isRanked ? "R" : "U") + "," + roomName;
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
    }
}