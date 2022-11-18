#nullable disable

namespace Shin {
    public class Equipment {
        // Properties
        public string Type { get; set; }
        public string Name { get; set; }
        public object StatMod { get; set; }

        // Constructor
        public Equipment(string type, string name) {
            Type = type;
            if (Type == "melee") {
                StatMod = new MeleeMod();
            } else if (Type == "gun") {
                StatMod = new GunMod();
            } else if (Type == "ammo") {
                StatMod = new AmmoMod();
            } else if (Type == "armour") {
                StatMod = new ArmourMod();
            }
        }
    }

    public struct MeleeMod {
        public int Power;
        public int CritChance;
        public StatSheet StatMod;
    }

    public struct GunMod {
        public int Power;
        public int CritChance;
        public StatSheet StatMod;
    }

    public struct AmmoMod {
        public double Multiplier;
        public Elements Type;
    }

    public struct ArmourMod {
        public int DEF;
        public int MagDEF;
        public StatSheet StatMod; 
    }

    public struct CurrentEquipment {
        // Properties
        public Equipment MeleeWeapon;
        public Equipment GunWeapon;
        public Equipment Ammo;
        public Equipment Head;
        public Equipment Body;
        public Equipment Legs;
        public Equipment Feet;
    }
}