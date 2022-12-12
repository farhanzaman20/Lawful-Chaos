#nullable disable

namespace Shin {
    public class PlayableCharacters {
        // Properties
        public string Name { get; set; }
        public StatSheet RawStats { get; set; }
        public PlayerStats Stats { get; set; }
        public int Intel = 0;
        public int Level;
        public int XP = 0;
        public EquipmentManager Equipped { get; set; }

        // Constructor
        public PlayableCharacters(string name, StatSheet statSheet) {
            Level = 1;
            Name = name;
            RawStats = statSheet;
            Stats = new PlayerStats(RawStats, Level);
            Equipped = new EquipmentManager(-1, -1, -1, -1, -1, -1, -1);
        }
        public PlayableCharacters(string name, StatSheet statSheet, int intel) {
            Level = 1;
            Name = name;
            RawStats = statSheet;
            Intel = intel;
            Stats = new PlayerStats(RawStats, Level, Intel);
            Equipped = new EquipmentManager(-1, -1, -1, -1, -1, -1, -1);
        }

        public void DisplayStats() {
            Console.WriteLine("Level: " + Level);
            Console.WriteLine("HP: " + Stats.HP);
            Console.WriteLine("MP: " + Stats.MP);
            Console.WriteLine("PhysATK: " + Stats.PhysATK);
            Console.WriteLine("PhysDEF: " + Stats.PhysDEF);
            Console.WriteLine("MagDEF: " + Stats.MagDEF);
            Console.WriteLine("TurnSPD: " + Stats.TurnSPD);
            Console.WriteLine("DodgeChange: " + Stats.DodgeChance);
            Console.WriteLine("CritChange: " + Stats.CritChance);
            Console.WriteLine("Negotiation: " + Stats.Negotiation);
            Console.WriteLine("EscapeChange: " + Stats.EscapeChance);
        }

        public void UpdateStats() {
        }
    }

    public struct StatSheet {
        // Constructor
        public StatSheet(int str, int mag, int vit, int agl, int luc) {
            STR = str;
            MAG = mag;
            VIT = vit;
            AGL = agl;
            LUC = luc;
        }

        public StatSheet() {
            STR = 0;
            MAG = 0;
            VIT = 0;
            AGL = 0;
            LUC = 0;
        }

        // Stats
        public int STR;
        public int MAG;
        public int VIT;
        public int AGL;
        public int LUC;

        public override string ToString() {
            return $"STR: {STR}, MAG: {MAG}, VIT: {VIT}, AGL: {AGL}, LUC: {LUC}";
        }
    }

    public struct PlayerStats {
        // Constructor No Equipment
        public PlayerStats(StatSheet statSheet, int level) {
            // Resources
            HP = Convert.ToInt32(
                Math.Floor(3 / 2 * Convert.ToDouble(level + statSheet.VIT))) + 10;
            MP = 5 * (level + statSheet.MAG);

            // ATK and DEF Related
            PhysATK = Convert.ToInt32(Math.Floor(0.7 * Convert.ToDouble(statSheet.STR)));
            PhysDEF = 0;
            GunATK = 0;
            MagATK = statSheet.MAG * 2;
            MagDEF = Convert.ToInt32(Math.Floor(statSheet.MAG * 0.5));

            // Speed Stat
            if (statSheet.AGL * 2 < 3) {
                TurnSPD = 0;
            } else {
                TurnSPD = statSheet.AGL * 2 - 3;
            }

            DodgeChance = 0;
            CritChance = 0;
            Negotiation = 0;
            EscapeChance = 0;
        }

        public PlayerStats(StatSheet statSheet, int level, int intel) {
            // Resources
            HP = Convert.ToInt32(
                Math.Floor(( 3 * Convert.ToDouble(level + statSheet.VIT) ) / 2)) + 10;
            MP = 0;

            // ATK and DEF Related
            PhysATK = Convert.ToInt32(Math.Floor(0.7 * Convert.ToDouble(statSheet.STR)));
            PhysDEF = 0;
            GunATK = 0;
            MagATK = 0;
            MagDEF = Convert.ToInt32(Math.Floor(intel * 0.5));

            // Speed Stat
            if (statSheet.AGL * 2 < 3) {
                TurnSPD = 0;
            } else {
                TurnSPD = statSheet.AGL * 2 - 3;
            }

            DodgeChance = 0;
            CritChance = 0;
            Negotiation = 2 * intel + 2;
            EscapeChance = (intel * statSheet.LUC) / 100;
        }

        // Stats
        public int HP;
        public int MP;
        public int PhysATK;
        public int GunATK;
        public int PhysDEF;
        public int MagATK;
        public int MagDEF;
        public int TurnSPD;
        public double DodgeChance;
        public double CritChance;
        public int Negotiation;
        public double EscapeChance;
    }
}