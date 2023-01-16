#nullable disable

namespace Shin {
    public class Equipment {
        // Properties
        public string Name { get; set; }
        public object StatMod { get; set; }

        // Constructor
        public Equipment(string name, object statMod) {
            Name = name;
            StatMod = statMod;
        }
    }

    #region Equipment Stat Modifiers
    public struct MeleeMod {
        public MeleeMod(int power, double critChance, int accuracy, int noh, StatSheet mod) {
            Power = power;
            CritChance = critChance;
            Accuracy = accuracy;
            NumberOfHits = noh;
            StatMod = mod;
        }
        public int Power;
        public double CritChance;
        public int Accuracy;
        public int NumberOfHits;
        public StatSheet StatMod;
    }

    public struct GunMod {
        public GunMod(int power, double critChance, int accuracy, GunHitType hitType, StatSheet mod) {
            Power = power;
            CritChance = critChance;
            Accuracy = accuracy;
            HitType = hitType;
            StatMod = mod;
        }
        public int Power;
        public double CritChance;
        public int Accuracy;
        public GunHitType HitType;
        public StatSheet StatMod;
    }

    public enum GunHitType {
        Single,
        AoE,
        Random,
        None 
    }

    public struct AmmoMod {
        public AmmoMod(int multiplier, Elements type) {
            Multiplier = multiplier;
            Type = type;
        }
        public int Multiplier;
        public Elements Type;
    }

    public struct ArmourMod {
        public ArmourMod(int def, int magDef, int evasion, StatSheet statMod) {
            DEF = def;
            MagDEF = magDef;
            Evasion = evasion;
            StatMod = statMod;
        }
        public int DEF;
        public int MagDEF;
        public int Evasion;
        public StatSheet StatMod; 
    }
    #endregion

    public class EquipmentManager {
        public EquipmentManager(int melee, int gun, int ammo, int head, int body, int legs, int foot) {
            MeleeWeapon = melee;
            GunWeapon = gun;
            Ammo = ammo;
            Headwear = head;
            BodyArmour = body;
            Bracers = legs;
            Footwear = foot;
            TotalStatMod = new StatSheet(
                                
            );
        }
        // Properties
        public int MeleeWeapon;
        public int GunWeapon;
        public int Ammo;
        public int Headwear;
        public int BodyArmour;
        public int Bracers;
        public int Footwear;
        public StatSheet TotalStatMod;

        public static Equipment[] MeleeWeapons = {
            new Equipment("Attack Knife", new MeleeMod(6, 0.1, 1, 1, new StatSheet()))
        };

        public static Equipment[] GunWeapons = {
            new Equipment("Pistol", new GunMod(3, 0.05, 2, GunHitType.Single, new StatSheet()))
        };
        public static Equipment[] Ammos = {
            new Equipment("Bullet", new AmmoMod(2, Elements.Physical))
        };
        public static Equipment[] Headwears = {
            new Equipment("Headgear", new ArmourMod(3, 2, 2, new StatSheet()))
        };
        public static Equipment[] BodyArmours = {
            new Equipment("Body Armour", new ArmourMod(5, 1, 1, new StatSheet()))
        };
        public static Equipment[] BracerArmours = {
            new Equipment("Basic Leggings", new ArmourMod(4, 2, 2, new StatSheet()))
        };
        public static Equipment[] Footwears = {
            new Equipment("Running Shoes", new ArmourMod(2, 1, 4, new StatSheet()))
        };
    }
}