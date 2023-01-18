#nullable disable

namespace Shin {
    static class Program {
        static void Main () {
            Console.Clear();

            // Initialize Game Object
            Game gameController = new Game(CharactorCreation());
            Console.WriteLine("Current Stats");
            Console.WriteLine(gameController.PartyOptions[0].RawStats.ToString());
            gameController.PartyOptions[0].Magic.Add(0);
            Console.ReadKey(true);

            Console.Clear();
            Console.WriteLine("As you walk down a road, 2 Goblins appear");
            gameController.CurrentEnemies.Add(new Enemy(20, 0, 0, "Goblin"));
            gameController.CurrentEnemies.Add(new Enemy(20, 0, 0, "Goblin"));
            Console.ReadKey(true);

            gameController = Combat.CombatLoop(gameController);

            gameController = Dungeon1.FirstFloor(gameController);
        }

        static PlayableCharacter CharactorCreation() {
            // Ask for Player name
            Console.Write("Enter a name (12 character limit): ");
            string name = MyLibrary.String(12);

            // Data Structure for Stat Input
            int statTotal = 16;
            int[] stats = {5, 5, 5, 5, 5};

            // Stat Input Loop
            while (statTotal > 0) {
                Console.Clear();
                Console.WriteLine("Choose a stat to increase or decrease, then increment");
                Console.WriteLine("Format: {stat} {+/-}{value}, eg. str +4");
                Console.WriteLine($"Remaining Stat Points: {statTotal}");
                Console.WriteLine($"STR: {stats[0]}, MAG: {stats[1]}, VIT: {stats[2]}, AGL: {stats[3]}, LUC: {stats[4]}");
                Console.Write("Input: ");
                string inputStr = Console.ReadLine();

                // Splits the input
                string[] strArray = inputStr.Split(" ", 2);
                int incrementVal = Convert.ToInt32(strArray[1]);

                // Measures to prevent getting more than stat total
                if (incrementVal > statTotal) {
                    Console.Clear();
                    Console.WriteLine("Not enough stat points left");
                    continue;
                } else {
                    statTotal -= incrementVal;
                    // Adds Stats to Stats Array
                    if (strArray[0].ToLower() == "str") {
                        stats[0] += incrementVal;
                    } else if (strArray[0].ToLower() == "mag") {
                        stats[1] += incrementVal;
                    } else if (strArray[0].ToLower() == "vit") {
                        stats[2] += incrementVal;
                    } else if (strArray[0].ToLower() == "agl") {
                        stats[3] += incrementVal;
                    } else if (strArray[0].ToLower() == "luc") {
                        stats[4] += incrementVal;
                    } else {
                        Console.Clear();
                        Console.WriteLine("Invalid input");
                        continue;
                    }

                    // Makes sure no stats go above the character creation stat cap of 12 or below the minimum of 5
                    for (int i = 0; i < stats.Length; i++) {
                        while (stats[i] >= 13) {
                            stats[i]--;
                            statTotal++; } while (stats[i] < 5) { stats[i]++;
                            statTotal--;
                        }
                    }
                }
            }

            // Creates and returns the Stat Sheet to use in creating the Main Character
            StatSheet mcStats = new StatSheet(stats[0], stats[1], stats[2], stats[3], stats[4]);
            return new PlayableCharacter(name, mcStats);
        }
    }

    public class Game {
        // Main Character

        // Party Array only holds the index of the actual party member, which is stored in Party Options List
        public int[] Party = {-1, -1, -1, -1};
        public List<PlayableCharacter> PartyOptions { get; set; }

        // List of current Enemies
        public List<Enemy> CurrentEnemies { get; set; }
        public int BattleXP { get; set; }

        // Constructor
        public Game(PlayableCharacter mc) {
            PartyOptions = new List<PlayableCharacter>();
            CurrentEnemies = new List<Enemy>();
            PartyOptions.Add(mc);
            Party[0] = 0;

        }

        public int PartyLenth() {
            int len = 0;
            for (int i = 0; i < Party.Length; i++) {
                if (Party[i] >= 0) {
                    len++;
                }
            }

            return len;
        }
    }

    public static class MyLibrary {
        // Method to take input string but limit the number of characters
        public static string String(int limit) {
            string tempStr = "";
            do {
                char c = Console.ReadKey().KeyChar;
                if (c == '\r') {break;}

                tempStr += c;
            } while (tempStr.Length < limit);
            Console.WriteLine();
            return tempStr;
        }

        public static int Random(int minInc, int maxExc) {
            Random rnd = new Random();
            return rnd.Next(minInc, maxExc);
        }
    }
}