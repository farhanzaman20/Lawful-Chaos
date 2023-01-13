#nullable disable

namespace Shin {
    static class Program {
        static void Main () {
            Console.Clear();
            CharactorCreation();
        }

        static async void CharactorCreation() {
            int statTotal = 20;
            int strStat, magStat, vitStat, aglStat, lucStat;
            strStat = magStat = vitStat = aglStat = lucStat = 5;

            while (statTotal != 0) {
                Console.Clear();
                Console.WriteLine("Choose a stat to increase or decrease, then increment");
                Console.WriteLine("Format: {stat} {+/-}{value}, eg. str +4");
                Console.WriteLine($"Remaining Stat Points: {statTotal}");
                Console.WriteLine($"Str: {strStat}, Mag: {magStat}, Vit: {vitStat}, Agl: {aglStat}, Luc: {lucStat}");
                Console.Write("Input: ");
                string inputStr = Console.ReadLine();

                string[] strArray = inputStr.Split(" ", 3);
                int incrementVal = Convert.ToInt32(strArray[1]);
                if (strArray.Length == 2) {
                    if (incrementVal > statTotal) {
                        Console.WriteLine("Not enough stat points left");
                        await Task.Delay(1);
                        continue;
                    } else {
                        statTotal -= incrementVal;
                        if (strArray[0].ToLower() == "str") {
                            strStat += incrementVal;
                        } else if (strArray[0].ToLower() == "mag") {
                            magStat += incrementVal;
                        } else if (strArray[0].ToLower() == "vit") {
                            vitStat += incrementVal;
                        } else if (strArray[0].ToLower() == "agl") {
                            aglStat += incrementVal;
                        } else if (strArray[0].ToLower() == "luc") {
                            lucStat += incrementVal;
                        } else {
                            Console.WriteLine("Invalid input");
                            await Task.Delay(1);
                            continue;
                        }
                    }
                } else {
                    Console.WriteLine("Invalid input");
                    await Task.Delay(1);
                    continue;
                }
            }
        }
    }

    class Game {
        PlayableCharacters MainCharacter { get; set; }
        PlayableCharacters[] Party = new PlayableCharacters[4];
        List<PlayableCharacters> PartyOptions { get; set; }
    }
}
