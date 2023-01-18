#nullable disable

namespace Shin {
    public struct MeleeWeapon {
        public MeleeWeapon(string name, int power, int cC, int accuracy, int nOH, StatSheet mod) {
            Name = name;
            Power = power;
            CritChance = cC;
            Accuracy = accuracy;
            NumberOfHits = nOH;
            StatMod = mod;
        }
        public string Name { get; set; }
        public int Power { get; set; }
        public int CritChance { get; set; }
        public int Accuracy { get; set; }
        public int NumberOfHits { get; set; }
        public StatSheet StatMod { get; set; }
    }

    public struct GunWeapon {
        public GunWeapon(string name, int power, int cC, int accuracy, GunHitType hT, StatSheet mod) {
            Name = name;
            Power = power;
            CritChance = cC;
            Accuracy = accuracy;
            HitType = hT;
            StatMod = mod;
        }
        public string Name { get; set; }
        public int Power { get; set; }
        public int CritChance { get; set; }
        public int Accuracy { get; set; }
        public GunHitType HitType { get; set; }
        public StatSheet StatMod { get; set; }
    }

    public enum GunHitType {
        Single,
        AoE,
        Random,
        None 
    }

    public struct Ammo {
        public Ammo(string name, double mult, Elements elem, Ailments ail) {
            Name = name;
            Multiplier = mult;
            Element = elem;
            Ailment = ail;
        }
        public string Name { get; set; }
        public double Multiplier { get; set; }
        public Elements Element { get; set; }
        public Ailments Ailment { get; set; }
    }

    public struct Equipment {
        public Equipment(string name, int physDef, int magDef, int evade, StatSheet mod) {
            Name = name;
            PhysDEF = physDef;
            MagDEF = magDef;
            Evasion = evade;
            StatMod = mod;
        }
        public string Name { get; set; }
        public int PhysDEF { get; set; }
        public int MagDEF { get; set; }
        public int Evasion { get; set; }
        public StatSheet StatMod { get; set; }
    }

    public class EquipmentManager {
        public EquipmentManager(int melee, int gun, int ammo, int head, int body, int legs, int foot) {
            MeleeWeapon = melee;
            GunWeapon = gun;
            Ammo = ammo;
            Headwear = head;
            BodyArmour = body;
            Legging = legs;
            Footwear = foot;
            CalculateTotalArmourStats();
        }
        // Properties
        public int MeleeWeapon { get; set; }
        public int GunWeapon { get; set; }
        public int Ammo { get; set; }
        public int Headwear { get; set; }
        public int BodyArmour { get; set; }
        public int Legging { get; set; }
        public int Footwear { get; set; }

        public int TotalPhysDEF { get; set; }
        public int TotalMagDEF { get; set; }
        public int TotalEvasion { get; set; }

        public void CalculateTotalArmourStats() {
            // Set these values to 0
            TotalPhysDEF = 0;
            TotalMagDEF = 0;
            TotalEvasion = 0;

            // Test for if armours equipped, if not, then only can it add values
            if (Headwear >= 0) {
                TotalPhysDEF += Headwears[Headwear].PhysDEF;
                TotalMagDEF += Headwears[Headwear].MagDEF;
                TotalEvasion += Headwears[Headwear].Evasion;
            }
            if (BodyArmour >= 0) {
                TotalPhysDEF += BodyArmours[BodyArmour].PhysDEF;
                TotalMagDEF += BodyArmours[BodyArmour].MagDEF;
                TotalEvasion += BodyArmours[BodyArmour].Evasion;
            }
            if (Legging >= 0) {
                TotalPhysDEF += Leggings[Legging].PhysDEF;
                TotalMagDEF += Leggings[Legging].MagDEF;
                TotalEvasion += Leggings[Legging].Evasion;
            }
            if (Footwear >= 0) {
                TotalPhysDEF += Footwears[Footwear].PhysDEF;
                TotalMagDEF += Footwears[Footwear].MagDEF;
                TotalEvasion += Footwears[Footwear].Evasion;
            }
        }

        // Equipments are saved in static arrays, and the actual players save the indexes from the arrays and use those to get the stats from the array
        public static MeleeWeapon[] MeleeWeapons = {
            new MeleeWeapon("Attack Knife", 6, 10, 1, 1, new StatSheet())
        };

        public static GunWeapon[] GunWeapons = {
            new GunWeapon("Pistol", 3, 5, 2, GunHitType.Single, new StatSheet())
        };
        public static Ammo[] Ammos = {
            new Ammo("Normal Bullet", 2, Elements.Physical, Ailments.None)
        };
        public static Equipment[] Headwears = {
            new Equipment("Headgear", 1, 0, 0, new StatSheet())
        };
        public static Equipment[] BodyArmours = {
            new Equipment("Hunting Vest", 2, 0, 1, new StatSheet())
        };
        public static Equipment[] Leggings = {
            new Equipment("Hunting Pants", 2, 0, 1, new StatSheet())
        };
        public static Equipment[] Footwears = {
            new Equipment("Plain Boots", 0, 0, 2, new StatSheet())
        };
    }
}