using System.Drawing;
namespace YGOPro_Launcher
{
    public class RoomInfos
    {
        public int BanList;
        public int Timer;
        public int Rule;
        public int Mode;

        public bool EnablePriority;
        public bool NoCheckDeck;
        public bool NoShuffleDeck;
        public bool isRanked;

        public int StartLp;
        public int StartHand;
        public int DrawCount;

        public string RoomName;

        public string Players;
        public bool Started;

        public static RoomInfos FromName(string roomname, string players, bool started)
        {
            RoomInfos infos = new RoomInfos();

           // if (roomname.Length < 15) return null;

            string rules = roomname.Substring(0, 7);

            if (!int.TryParse(rules[0].ToString(), out infos.Rule))
                return null;
            if (!int.TryParse(rules[1].ToString(), out infos.Mode))
                return null;
            if (!int.TryParse(rules[2].ToString(), out infos.BanList))
                return null;
            if (!int.TryParse(rules[3].ToString(), out infos.Timer))
                return null;
            infos.EnablePriority = rules[4] == 'T' || rules[4] == '1';
            infos.NoCheckDeck = rules[5] == 'T' || rules[5] == '1';
            infos.NoShuffleDeck = rules[6] == 'T' || rules[6] == '1';

            string data = roomname.Substring(7, roomname.Length - 7);

            if (!data.Contains(",")) return null;

            string[] list = data.Split(',');

            if (!int.TryParse(list[0], out infos.StartLp))
                return null;
            infos.StartHand = 5;
            infos.DrawCount = 1;

            if (list[1] == "R") infos.isRanked = true; 
            else infos.isRanked = false;

            infos.RoomName = list[2];

            infos.Players = players;
            infos.Started = started;

            return infos;
        }

        public string GenerateURI(string server, int port)
        {
            return "ygpro:/" + server + "/" + port + "/" + Rule + Mode + BanList + Timer + (EnablePriority ? 1 : 0) + (NoCheckDeck ? 1 : 0) + (NoShuffleDeck ? 1 : 0) + StartLp + "," + (isRanked ? "R" : "U") + "," + RoomName;
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
            
            if (playerinfo.Rule == otherroom.Rule && playerinfo.BanList == otherroom.BanList
                && playerinfo.Mode == otherroom.Mode && playerinfo.NoCheckDeck == otherroom.NoCheckDeck
                && playerinfo.NoShuffleDeck == otherroom.NoShuffleDeck && playerinfo.EnablePriority == otherroom.EnablePriority
                && playerinfo.StartLp == otherroom.StartLp && playerinfo.Timer == otherroom.Timer && otherroom.Started == false)
            {
                string[] players = otherroom.Players.Split(',');
                if (GameMode(otherroom.Mode) == "Tag")
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