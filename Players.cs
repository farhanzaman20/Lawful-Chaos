#nullable disable

namespace Shin {
    public class PlayableCharacter {
        // Properties
        public string Name { get; set; }
        public StatSheet RawStats { get; set; }
        public PlayerStats Stats { get; set; }
        public int Level { get; set; }
        public GameResources XP { get; set; }
        public EquipmentManager Equipped { get; set; }
        public List<int> Magic { get; set; }

        // Constructor
        public PlayableCharacter(string name, StatSheet statSheet, int level = 1) {
            Level = level;
            Name = name;
            RawStats = statSheet;
            Stats = new PlayerStats(RawStats, Level);
            Equipped = new EquipmentManager(0, -1, -1, 0, 0, 0, 0);
            XP = new GameResources(0, 100);
            Magic = new List<int>();
            UpdateStats();
        }

        // Methods
        public void DisplayStats() {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Level: " + Level);
            Console.WriteLine("Stats: " + RawStats.ToString());
            Console.WriteLine("HP: " + Stats.HP);
            Console.WriteLine("MP: " + Stats.MP);
            Console.WriteLine("PhysATK: " + Stats.PhysATK);
            Console.WriteLine("PhysDEF: " + Stats.PhysDEF);
            Console.WriteLine("MagPOT: " + Stats.MagPOT);
            Console.WriteLine("MagDEF: " + Stats.MagDEF);
            Console.WriteLine("TurnSPD: " + Stats.TurnSPD);
            Console.WriteLine("DodgeChance: " + Stats.Evasion);
            Console.WriteLine("CritChance: " + Stats.CritChance);
        }

        public void UpdateResources() {
            Stats.HP.Max = Stats.HP.Current = Convert.ToInt32(Math.Floor(1.5 * Convert.ToDouble(Level + RawStats.VIT * 1.5))) + 5;
            Stats.MP.Max = Stats.MP.Current = 5 * (Level + RawStats.MAG);
        }

        public void UpdateStats() {
            // Test if weapon equipped
            if (Equipped.MeleeWeapon >= 0) {
                Stats.PhysATK = Convert.ToInt32(Math.Floor(0.7 * Convert.ToDouble(RawStats.STR))) + EquipmentManager.MeleeWeapons[Equipped.MeleeWeapon].Power;
            }

            Stats.PhysDEF = Equipped.TotalPhysDEF;

            // Test if weapon equipped
            if (Equipped.GunWeapon >= 0 && Equipped.Ammo >= 0) {
                Stats.GunATK = Convert.ToInt32(Math.Floor(EquipmentManager.GunWeapons[Equipped.GunWeapon].Power * EquipmentManager.Ammos[Equipped.Ammo].Multiplier));
            }

            // Magic Stats
            Stats.MagPOT = Convert.ToInt32(Math.Floor(Convert.ToDouble(RawStats.MAG) * 1.2 + 3));
            Stats.MagDEF = Convert.ToInt32(Math.Floor(RawStats.MAG * 0.5)) + Equipped.TotalMagDEF;

            // Other Stats
            if (RawStats.AGL * 2 < 3) {
                Stats.TurnSPD = 0;
            } else {
                Stats.TurnSPD = RawStats.AGL * 2 - 3;
            }
            Stats.Evasion = Convert.ToInt32(RawStats.AGL + Math.Floor(RawStats.LUC * 1.5)) - 3 + Equipped.TotalEvasion;
            Stats.CritChance = Convert.ToInt32(Math.Floor(RawStats.LUC * 0.2));
        }

        public void MeleeAttack(Enemy targetEnemy) {
            // Damage Calculation
            int damage = 0;
            int enemyDef = Enemy.EnemyTypes[targetEnemy.Type].PhysDEF;
            damage = Convert.ToInt32(Math.Floor(1.5 * Stats.PhysATK / enemyDef));
            
            // Crit Calculation
            damage = RNG.CritCalculation(Stats.CritChance + EquipmentManager.MeleeWeapons[Equipped.MeleeWeapon].CritChance, damage);

            // Dodge Calculation
            if (RNG.DodgeCalculation(0, Enemy.EnemyTypes[targetEnemy.Type].Evasion)) {
                damage = 0;
            }

            targetEnemy.HP.Current -= damage;
            if (targetEnemy.HP.Current < 0) {
                targetEnemy.HP.Current = 0;
            }
        }
        
        public void GunAttack(Enemy targetEnemy) {
            // Damage Calculation
            int damage = 0;
            int enemyDef = Enemy.EnemyTypes[targetEnemy.Type].PhysDEF;
            damage = Convert.ToInt32(Math.Floor(1.5 * Stats.GunATK / (enemyDef * 0.75))); // Guns slightly pierce through armour

            // Crit Calculation
            damage = RNG.CritCalculation(Stats.CritChance + EquipmentManager.GunWeapons[Equipped.GunWeapon].CritChance, damage);

            // Dodge Calculation
            if (RNG.DodgeCalculation(0, Enemy.EnemyTypes[targetEnemy.Type].Evasion)) {
                damage = 0;
            }

            targetEnemy.HP.Current -= damage;
            if (targetEnemy.HP.Current < 0) {
                targetEnemy.HP.Current = 0;
            }
        }
    }

    #region Stats
    public struct StatSheet {
        // Constructor
        public StatSheet(int str = 0, int mag = 0, int vit = 0, int agl = 0, int luc = 0) {
            STR = str;
            MAG = mag;
            VIT = vit;
            AGL = agl;
            LUC = luc;
        }

        // Stats
        public int STR { get; set; }
        public int MAG { get; set; }
        public int VIT { get; set; }
        public int AGL { get; set; }
        public int LUC { get; set; }

        public override string ToString() {
            return $"STR: {STR}, MAG: {MAG}, VIT: {VIT}, AGL: {AGL}, LUC: {LUC}";
        }
    }

    public class PlayerStats {
        // Constructor No Equipment
        public PlayerStats(StatSheet statSheet, int level) {
            // Resources
            HP = new GameResources(Convert.ToInt32(
                Math.Floor(1.5 * Convert.ToDouble(level + statSheet.VIT * 1.5))) + 5);
            MP = new GameResources(5 * (level + statSheet.MAG));
        }

        // Stats
        public GameResources HP { get; set; }
        public GameResources MP { get; set; }
        public int PhysATK { get; set; }
        public int GunATK { get; set; }
        public int PhysDEF { get; set; }
        public int MagPOT { get; set; }
        public int MagDEF { get; set; }
        public int TurnSPD { get; set; }
        public int Evasion { get; set; }
        public int CritChance { get; set; }
    }

    // This class is for managing resources that have a max and current value, such as HP and MP
    public class GameResources {
        public GameResources(int val) {
            Current = Max = val;
        }
        public GameResources(int current, int max) {
            Current = current;
            Max = max;
        }
        public int Current { get; set; }
        public int Max { get; set; }
        public override string ToString() {
            return $"{Current}/{Max}";
        }
    }
    #endregion
}
