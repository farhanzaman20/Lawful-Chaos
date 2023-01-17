#nullable disable

namespace Shin {
    public class EnemyBase {
        public EnemyStats Stats;
        public int XPYield;
        public List<Spell> Magic;
    }

    public class EnemyStats {
        public EnemyStats(int hp, int mp, int physAtk, int physDef, int magAtk, int magDef, int accuracy, int evasion) {
            HP = new GameResources(hp);
            MP = new GameResources(mp);
            PhysATK = physAtk;
            MagATK = magAtk;
            PhysDEF = physDef;
            MagDEF = magDef;
            Accuracy = accuracy;
            Evasion = evasion;
        }
        public GameResources HP;
        public GameResources MP;
        public int PhysATK;
        public int PhysDEF;
        public int MagATK;
        public int MagDEF;
        public int Accuracy;
        public int Evasion;
    }
}