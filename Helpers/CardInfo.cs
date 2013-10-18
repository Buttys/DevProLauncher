namespace DevProLauncher.Helpers
{
    using System;
    using Enums;

    public class CardInfos : ICloneable
    {
        public string Name { get; set; }
        public string CleanedName { get; set; }
        public string Description { get; set; }

        public int Id { get; private set; }
        public int AliasId { get; set; }
        public int Attribute { get; set; }
        public int Type { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Race { get; set; }
        public int Amount { get; set; }

        private string m_cardRace;
        private string m_cardTypes;

        public CardInfos(int id)
        {
            Id = id;
        }

        public string GetCardRace()
        {
            if (m_cardRace != null)
                return m_cardRace;

            CardRace[] raceArray = new[] { 
                CardRace.Warrior, CardRace.SpellCaster, CardRace.Fairy, CardRace.Fiend, CardRace.Zombie, CardRace.Machine, CardRace.Aqua, 
                CardRace.Pyro, CardRace.Rock, CardRace.WindBeast, CardRace.Plant, CardRace.Insect, CardRace.Thunder, CardRace.Dragon, CardRace.Beast, CardRace.BestWarrior, 
                CardRace.Dinosaur, CardRace.Fish, CardRace.SeaSerpent, CardRace.Reptile, CardRace.Psycho, CardRace.DivineBeast
            };

            CardAttribute[] attributeArray = new[] {
                CardAttribute.Dark, CardAttribute.Divine, CardAttribute.Earth, CardAttribute.Fire, CardAttribute.Light, CardAttribute.Water, CardAttribute.Wind
            };

            foreach (CardRace race in raceArray)
            {
                if ((Race & (int)race) == 0) continue;
                m_cardRace = race.ToString();
                break;
            }

            foreach (CardAttribute attribute in attributeArray)
            {
                if ((Attribute & (int)attribute) == 0) continue;
                m_cardRace += " - " + attribute;
                break;
            }

            return (m_cardRace ?? (m_cardRace = ""));
        }

        public string GetCardTypes()
        {
            if (m_cardTypes == null)
            {
                CardType[] typeArray = new[] { 
                    CardType.Monster, CardType.Spell, CardType.Trap, CardType.Normal, CardType.Effect, CardType.Fusion, CardType.Ritual, CardType.TrapMonster, 
                    CardType.Spirit, CardType.Union, CardType.Dual, CardType.Tuner, CardType.Synchro, CardType.Token, CardType.QuickPlay, CardType.Continuous, 
                    CardType.Equip, CardType.Field, CardType.Counter, CardType.Flip, CardType.Toon, CardType.Xyz
                };

                foreach (CardType type in typeArray)
                {
                    if (((Type & (int)type) == 0)) continue;

                    if (m_cardTypes == null)
                        m_cardTypes = type.ToString();
                    else
                        m_cardTypes += " / " + type;
                }
                m_cardTypes = "[" + m_cardTypes + "]";
            }
            return m_cardTypes;
        }

        public bool HasType(CardType type)
        {
            return ((Type & (int)type) != 0);
        }

        public bool IsExtraCard()
        {
            return (HasType(CardType.Fusion) || HasType(CardType.Synchro) || HasType(CardType.Xyz));
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}