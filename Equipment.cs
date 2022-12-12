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
        public int Power;
        public double CritChance;
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
            new Equipment("Attack Knife", new MeleeMod(6, 0.1, 1, 1, new StatSheet())),
            new Equipment("Tonfa", new MeleeMod(6, 0.05, 2, 2, new StatSheet()))
        };

        public static Equipment[] GunWeapons = {

        };
        public static Equipment[] Ammos = {

        };
        public static Equipment[] Headwears = {

        };
        public static Equipment[] BodyArmours = {

        };
        public static Equipment[] BracerArmours = {

        };
        public static Equipment[] Footwears = {

        };
    }
}