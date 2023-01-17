#nullable disable

namespace Shin {
    static class Program {
        static void Main () {
            Console.Clear();
            Game gameController = new Game(CharactorCreation());
            Console.WriteLine("Current Stats");
            Console.WriteLine(gameController.MainCharacter.RawStats.ToString());
        }

        static PlayableCharacter CharactorCreation() {
            Console.Write("Enter a name: ");
            string name = Console.ReadLine();
            int statTotal = 16;
            int[] stats = {5, 5, 5, 5, 5};

            while (statTotal > 0) {
                Console.Clear();
                Console.WriteLine("Choose a stat to increase or decrease, then increment");
                Console.WriteLine("Format: {stat} {+/-}{value}, eg. str +4");
                Console.WriteLine($"Remaining Stat Points: {statTotal}");
                Console.WriteLine($"STR: {stats[0]}, MAG: {stats[1]}, VIT: {stats[2]}, AGL: {stats[3]}, LUC: {stats[4]}");
                Console.Write("Input: ");
                string inputStr = Console.ReadLine();

                string[] strArray = inputStr.Split(" ", 2);
                int incrementVal = Convert.ToInt32(strArray[1]);
                if (incrementVal > statTotal) {
                    Console.Clear();
                    Console.WriteLine("Not enough stat points left");
                    continue;
                } else {
                    statTotal -= incrementVal;
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
                    for (int i = 0; i < stats.Length; i++) {
                        while (stats[i] >= 13) {
                            stats[i]--;
                            statTotal++;
                        }
                        while (stats[i] < 5) {
                            stats[i]++;
                            statTotal--;
                        }
                    }
                }
            }
            StatSheet mcStats = new StatSheet(stats[0], stats[1], stats[2], stats[3], stats[4]);
            return new PlayableCharacter(name, mcStats);
        }
    }

    public class Game {
        public PlayableCharacter MainCharacter { get; set; }
        public int[] Party = {-1, -1, -1};
        public List<PlayableCharacter> PartyOptions { get; set; }

        public Game(PlayableCharacter mc) {
            PartyOptions = new List<PlayableCharacter>();
            MainCharacter = mc;
        }
    }
}