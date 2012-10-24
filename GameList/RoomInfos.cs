using System.Drawing;
namespace YGOPro_Launcher
{
    public class RoomInfos
    {
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

            string rules = roomname.Substring(0, 5);

            if (!int.TryParse(rules[0].ToString(), out infos.Rule))
                return null;
            if (!int.TryParse(rules[1].ToString(), out infos.Mode))
                return null;
            infos.EnablePriority = rules[2] == 'T' || rules[2] == '1';
            infos.NoCheckDeck = rules[3] == 'T' || rules[3] == '1';
            infos.NoShuffleDeck = rules[4] == 'T' || rules[4] == '1';

            string data = roomname.Substring(5, roomname.Length - 5);

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
            return "ygpro:/" + server + "/" + port + "/" + Rule + Mode + (EnablePriority ? 1 : 0) + (NoCheckDeck ? 1 : 0) + (NoShuffleDeck ? 1 : 0) + StartLp + "," + (isRanked ? "R" : "U") + "," + RoomName;
        }
    }
}