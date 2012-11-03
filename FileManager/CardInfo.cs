namespace YGOPro_Launcher.CardDatabase
{
    using YGOPro_Launcher.CardEnums;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class CardInfos
    {
        private string m_cardRace;
        private string m_cardTypes;

        public CardInfos(int id)
        {
            this.Id = id;
        }

        public string GetCardRace()
        {
            if (this.m_cardRace != null)
            {
                return this.m_cardRace;
            }
            CardRace[] raceArray = new CardRace[] { 
                CardRace.Warrior, CardRace.SpellCaster, CardRace.Fairy, CardRace.Fiend, CardRace.Zombie, CardRace.Machine, CardRace.Aqua, CardRace.Pyro, CardRace.Rock, CardRace.WindBeast, CardRace.Plant, CardRace.Insect, CardRace.Thunder, CardRace.Dragon, CardRace.Beast, CardRace.BestWarrior, 
                CardRace.Dinosaur, CardRace.Fish, CardRace.SeaSerpent, CardRace.Reptile, CardRace.Psycho, CardRace.DivineBeast
             };
            CardAttribute[] attributeArray = new CardAttribute[] { CardAttribute.Dark, CardAttribute.Divine, CardAttribute.Earth, CardAttribute.Fire, CardAttribute.Light, CardAttribute.Water, CardAttribute.Wind };
            foreach (CardRace race in raceArray)
            {
                if ((this.Race & (int)race) != 0)
                {
                    this.m_cardRace = race.ToString();
                    break;
                }
            }
            foreach (CardAttribute attribute in attributeArray)
            {
                if ((this.Attribute & (int)attribute) != 0)
                {
                    this.m_cardRace = this.m_cardRace + " - " + attribute;
                    break;
                }
            }
            return (this.m_cardRace ?? (this.m_cardRace = ""));
        }

        public string GetCardTypes()
        {
            if (this.m_cardTypes == null)
            {
                CardType[] typeArray = new CardType[] { 
                    CardType.Monster, CardType.Spell, CardType.Trap, CardType.Normal, CardType.Effect, CardType.Fusion, CardType.Ritual, CardType.TrapMonster, CardType.Spirit, CardType.Union, CardType.Dual, CardType.Tuner, CardType.Synchro, CardType.Token, CardType.QuickPlay, CardType.Continuous, 
                    CardType.Equip, CardType.Field, CardType.Counter, CardType.Flip, CardType.Toon, CardType.Xyz
                 };
                foreach (CardType type in typeArray)
                {
                    if (((this.Type & (int)type) != 0))
                    {
                        if (this.m_cardTypes == null)
                        {
                            this.m_cardTypes = type.ToString();
                        }
                        else
                        {
                            this.m_cardTypes = this.m_cardTypes + " / " + type;
                        }
                    }
                }
                this.m_cardTypes = "[" + this.m_cardTypes + "]";
            }
            return this.m_cardTypes;
        }

        public bool HasType(CardType type)
        {
            return ((this.Type & (int)type) != 0);
        }

        public bool IsExtraCard()
        {
            if (!this.HasType(CardType.Fusion) && !this.HasType(CardType.Synchro))
            {
                return this.HasType(CardType.Xyz);
            }
            return true;
        }

        public int AliasId { get; set; }

        public int Atk { get; set; }

        public int Attribute { get; set; }

        public string CleanedName { get; set; }

        public int Def { get; set; }

        public string Description { get; set; }

        public int Id { get; private set; }

        public int Level { get; set; }

        public string Name { get; set; }

        public int Race { get; set; }

        public int Type { get; set; }

    }
}