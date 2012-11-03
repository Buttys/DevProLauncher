namespace YGOPro_Launcher.CardDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.IO;
    using System.Reflection;

    public class CardsManager
    {
        private IDictionary<int, CardInfos> m_cards;

        public CardInfos FromId(int id)
        {
            if (m_cards.ContainsKey(id))
            {
                return m_cards[id];
            }
            return null;
        }

        public void Init()
        {
            m_cards = new Dictionary<int, CardInfos>();
            string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            string str2 = Path.Combine(str, @"cards.cdb");
            if (!File.Exists(str2)) return;
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + str2);
            connection.Open();
            SQLiteDataReader reader = new SQLiteCommand("SELECT id, alias, type, level, race, attribute, atk, def FROM datas", connection).ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                CardInfos infos = new CardInfos(id)
                {
                    AliasId = reader.GetInt32(1),
                    Type = reader.GetInt32(2),
                    Level = reader.GetInt32(3),
                    Race = reader.GetInt32(4),
                    Attribute = reader.GetInt32(5),
                    Atk = reader.GetInt32(6),
                    Def = reader.GetInt32(7)
                };
                m_cards.Add(id, infos);
            }
            reader.Close();
            reader = new SQLiteCommand("SELECT id, name, desc FROM texts", connection).ExecuteReader();
            while (reader.Read())
            {
                int key = reader.GetInt32(0);
                if (m_cards.ContainsKey(key))
                {
                    m_cards[key].Name = reader.GetString(1);
                    m_cards[key].CleanedName = m_cards[key].Name.Trim().ToLower().Replace("-", " ").Replace("'", " ").Replace("   ", " ").Replace("  ", " ");
                    m_cards[key].Description = reader.GetString(2);
                }
            }
            connection.Close();
        }

        public IList<CardInfos> Search(string text)
        {
            text = text.Trim().ToLower();
            IList<CardInfos> list = new List<CardInfos>();
            foreach (CardInfos infos in m_cards.Values)
            {
                if (string.IsNullOrEmpty(text) || infos.CleanedName.Contains(text))
                {
                    list.Add(infos);
                }
            }
            return list;
        }
    }
}

