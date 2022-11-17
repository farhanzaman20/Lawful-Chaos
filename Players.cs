#nullable disable

namespace Shin {
    public class PlayableCharacters {
        // Properties
        public string Name { get; set; }
        public StatSheet Stats { get; set; }
        public int Level;

        // Constructor
        public PlayableCharacters(string name, int str, int mag, int vit, int agl, int luc) {
            Name = name;
            Stats = new StatSheet(str, mag, vit, agl, luc);
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

        // Stats
        public int STR;
        public int MAG;
        public int VIT;
        public int AGL;
        public int LUC;
    }

    public struct PlayerStats {
        // Constructor
        public PlayerStats(StatSheet statSheet, int level) {
            // Resources
            HP = 3 * (level + statSheet.VIT) + 10;
            MP = 5 * (level + statSheet.MAG);

            // ATK and DEF Related
            PhysATK = statSheet.STR * 2;
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
    }
}