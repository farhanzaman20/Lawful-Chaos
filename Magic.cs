#nullable disable

namespace Shin {
    public enum Elements {
        Physical,
        Fire,
        Ice,
        Electric,
        Force,
        Light,
        Dark,
        Almighty,
        Ailment
    }

    public enum Ailments {
        Sleep,
        Paralysis,
        Poison,
        None
    }

    public class Spell {
        public int Power;
        public Elements Element;
        public Ailments Ailment;
    }
}