#nullable disable

namespace Shin {
    public enum Elements {
        Physical,
        Fire,
        Ice,
        Electric,
        Bless,
        Ailment
    }

    public enum Ailments {
        Sleep,
        Paralysis,
        Poison,
        None
    }

    public static class SpellManager {
        public static Spell[] Spells = {
            new Spell("Heal", 10, Elements.Bless, Ailments.None),
            new Spell("Fire", 10, Elements.Fire, Ailments.None),
            new Spell("Ice", 10, Elements.Ice, Ailments.None),
            new Spell("Lightning", 10, Elements.Electric, Ailments.None),
            new Spell("Poison Breath", 7, Elements.Ailment, Ailments.Poison)
        };
    }

    public class Spell {
        public Spell(string name, int power, Elements element, Ailments ailment) {
            Name = name;
            Power = power;
            Element = element;
            Ailment = ailment;
        }
        public string Name { get; set; }
        public int Power { get; set; }
        public int Cost { get; set; }
        public Elements Element { get; set; }
        public Ailments Ailment { get; set; }
        public void Cast(Game gameController, Enemy enemy) {
            if (Element = Elements.Bless) {
                Console.Write("Choose player: ");
            }
        }
    }
}