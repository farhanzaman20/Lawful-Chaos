#nullable disable

namespace Shin {
    public class PlayableCharacter {
        // Properties
        public string Name { get; set; }
        public StatSheet RawStats { get; set; }
        public PlayerStats Stats { get; set; }
        public int Level;
        public GameResources XP;
        public EquipmentManager Equipped { get; set; }

        // Constructor
        public PlayableCharacter(string name, StatSheet statSheet) {
            Level = 1;
            Name = name;
            RawStats = statSheet;
            Stats = new PlayerStats(RawStats, Level);
            Equipped = new EquipmentManager(0, -1, -1, 0, 0, 0, 0);
            XP = new GameResources(0, 100);
        }
        public PlayableCharacter(string name, StatSheet statSheet, int level) {
            Level = level;
            Name = name;
            RawStats = statSheet;
            Stats = new PlayerStats(RawStats, Level);
            Equipped = new EquipmentManager(0, -1, -1, 0, 0, 0, 0);
        }

        public void DisplayStats() {
            Console.WriteLine("Level: " + Level);
            Console.WriteLine("HP: " + Stats.HP);
            Console.WriteLine("MP: " + Stats.MP);
            Console.WriteLine("PhysATK: " + Stats.PhysATK);
            Console.WriteLine("PhysDEF: " + Stats.PhysDEF);
            Console.WriteLine("MagDEF: " + Stats.MagDEF);
            Console.WriteLine("TurnSPD: " + Stats.TurnSPD);
            Console.WriteLine("DodgeChange: " + Stats.Evasion);
            Console.WriteLine("CritChange: " + Stats.CritChance);
            Console.WriteLine("EscapeChange: " + Stats.EscapeChance);
        }
    }

    #region Stats
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

    public class PlayerStats {
        // Constructor No Equipment
        public PlayerStats(StatSheet statSheet, int level) {
            // Resources
            HP = new GameResources(Convert.ToInt32(
                Math.Floor(3 / 2 * Convert.ToDouble(level + statSheet.VIT))) + 10);
            MP = new GameResources(5 * (level + statSheet.MAG));

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

            // Misc
            Evasion = Convert.ToInt32(Math.Floor(statSheet.AGL * 0.3) + Math.Floor(statSheet.LUC * 0.5));
            CritChance = Math.Floor(statSheet.LUC * 0.01);
            EscapeChance = Convert.ToInt32(Math.Floor(statSheet.LUC * 0.05));
        }

        // Stats
        public GameResources HP;
        public GameResources MP;
        public int PhysATK;
        public int GunATK;
        public int PhysDEF;
        public int MagATK;
        public int MagDEF;
        public int TurnSPD;
        public int Evasion;
        public double CritChance;
        public double EscapeChance;
    }

    public struct GameResources {
        public GameResources(int val) {
            Current = Max = val;
        }
        public GameResources(int current, int max) {
            Current = current;
            Max = max;
        }
        public int Current;
        public int Max;
    }
    #endregion
}
