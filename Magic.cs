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
            new Spell("Heal", 10, 10, Elements.Bless, Ailments.None),
            new Spell("Fire", 10, 5, Elements.Fire, Ailments.None),
            new Spell("Ice", 10, 5, Elements.Ice, Ailments.None),
            new Spell("Lightning", 10, 5, Elements.Electric, Ailments.None),
            new Spell("Poison Breath", 7, 5, Elements.Ailment, Ailments.Poison)
        };
    }

    public class Spell {
        public Spell(string name, int power, int cost, Elements element, Ailments ailment) {
            Name = name;
            Power = power;
            Cost = cost;
            Element = element;
            Ailment = ailment;
        }
        public string Name { get; set; }
        public int Power { get; set; }
        public int Cost { get; set; }
        public Elements Element { get; set; }
        public Ailments Ailment { get; set; }
        public void Cast(Game gameController) {
            if (Element == Elements.Bless) {
                Console.Write("Choose player: ");

                bool isInputInt = Int32.TryParse(Console.ReadLine(), out int chosenPlayer);

                if (chosenPlayer > 0 && chosenPlayer <= gameController.PartyLenth() && isInputInt) {
                    gameController.PartyOptions[gameController.Party[chosenPlayer - 1]].Stats.HP.Current += Power + gameController.PartyOptions[gameController.Party[chosenPlayer - 1]].Stats.MagPOT;
                    while (gameController.PartyOptions[gameController.Party[chosenPlayer - 1]].Stats.HP.Current > gameController.PartyOptions[gameController.Party[chosenPlayer - 1]].Stats.HP.Max) {
                        gameController.PartyOptions[gameController.Party[chosenPlayer - 1]].Stats.HP.Current--;
                    }
                } else {
                    Console.WriteLine("Invalid option");
                }
            }
        }
    }
}