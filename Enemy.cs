#nullable disable

namespace Shin {
    public class Enemy {
        public Enemy(int hp, int mp, int type, string name) {
            HP = new GameResources(hp);
            MP = new GameResources(mp);
            Type = type;
            Name = name;
        }
        public GameResources HP { get; set; }
        public GameResources MP { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }

        public void MeleeAttack(PlayableCharacter playerTarget) {
            int damage = 0;
            int playerDef = playerTarget.Stats.PhysDEF;
            int enemyAtk = EnemyTypes[Type].PhysATK;
            damage = Convert.ToInt32(Math.Floor(Convert.ToDouble(enemyAtk * enemyAtk) / playerDef));

            // Dodge Calculation
            if (RNG.DodgeCalculation(EnemyTypes[Type].Accuracy, playerTarget.Stats.Evasion)) {
                damage = 0;
            }

            playerTarget.Stats.HP.Current -= damage;
            if (playerTarget.Stats.HP.Current < 0) {
                playerTarget.Stats.HP.Current = 0;
            }
        }
        public static EnemyStats[] EnemyTypes = {
            new EnemyStats(5, 3, 0, 3, 20, 4, 20, new int[0]), // Goblin
            new EnemyStats(20, 50, 0, 50, 100, 100, 0, new int[0]) // Demon Knight
        };
    }

    public struct EnemyStats {
        public EnemyStats(int physAtk, int physDef, int magAtk, int magDef, int accuracy, int evasion, int xp, int[] magic) {
            PhysATK = physAtk;
            PhysDEF = physDef;
            MagPOT = magAtk;
            MagDEF = magDef;
            Accuracy = accuracy;
            Evasion = evasion;
            XPYield = xp;
            Magic = magic;
        }
        public int PhysATK { get; set; }
        public int PhysDEF { get; set; }
        public int MagPOT { get; set; }
        public int MagDEF { get; set; }
        public int Accuracy { get; set; }
        public int Evasion { get; set; }
        public int XPYield { get; set; }
        public int[] Magic { get; set; }
    }
}