namespace DevProLauncher.Helpers
{
    using System;
    using Enums;

    public class CardInfos: ICloneable
    {
        private string _mCardRace;
        private string _mCardTypes;

        public CardInfos(int id)
        {
            Id = id;
        }

        public string GetCardRace()
        {
            if (_mCardRace != null)
            {
                return _mCardRace;
            }
            var raceArray = new[] { 
                CardRace.Warrior, CardRace.SpellCaster, CardRace.Fairy, CardRace.Fiend, CardRace.Zombie, CardRace.Machine, CardRace.Aqua, CardRace.Pyro, CardRace.Rock, CardRace.WindBeast, CardRace.Plant, CardRace.Insect, CardRace.Thunder, CardRace.Dragon, CardRace.Beast, CardRace.BestWarrior, 
                CardRace.Dinosaur, CardRace.Fish, CardRace.SeaSerpent, CardRace.Reptile, CardRace.Psycho, CardRace.DivineBeast
             };
            var attributeArray = new[] { CardAttribute.Dark, CardAttribute.Divine, CardAttribute.Earth, CardAttribute.Fire, CardAttribute.Light, CardAttribute.Water, CardAttribute.Wind };
            foreach (CardRace race in raceArray)
            {
                if ((Race & (int) race) == 0) continue;
                _mCardRace = race.ToString();
                break;
            }
            foreach (CardAttribute attribute in attributeArray)
            {
                if ((Attribute & (int) attribute) == 0) continue;
                _mCardRace = _mCardRace + " - " + attribute;
                break;
            }
            return (_mCardRace ?? (_mCardRace = ""));
        }

        public string GetCardTypes()
        {
            if (_mCardTypes == null)
            {
                var typeArray = new[] { 
                    CardType.Monster, CardType.Spell, CardType.Trap, CardType.Normal, CardType.Effect, CardType.Fusion, CardType.Ritual, CardType.TrapMonster, CardType.Spirit, CardType.Union, CardType.Dual, CardType.Tuner, CardType.Synchro, CardType.Token, CardType.QuickPlay, CardType.Continuous, 
                    CardType.Equip, CardType.Field, CardType.Counter, CardType.Flip, CardType.Toon, CardType.Xyz
                 };
                foreach (CardType type in typeArray)
                {
                    if (((Type & (int) type) == 0)) continue;
                    if (_mCardTypes == null)
                    {
                        _mCardTypes = type.ToString();
                    }
                    else
                    {
                        _mCardTypes = _mCardTypes + " / " + type;
                    }
                }
                _mCardTypes = "[" + _mCardTypes + "]";
            }
            return _mCardTypes;
        }

        public bool HasType(CardType type)
        {
            return ((Type & (int)type) != 0);
        }

        public bool IsExtraCard()
        {
            if (!HasType(CardType.Fusion) && !HasType(CardType.Synchro))
            {
                return HasType(CardType.Xyz);
            }
            return true;
        }

        public object Clone()
        {
            return  MemberwiseClone();
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

        public int Amount { get; set; }

    }
}