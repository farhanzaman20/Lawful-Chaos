﻿// SMT 1 Logic I guess. Will port this over to Unity later
#nullable disable

namespace Shin {
    class Program {
        static void Main (string[] args) {
            /*
            Stat Allocation Planning
            All characters will start with 5 points in every stat, and then will be able to allocate
            20 points among the 5 stats. MAG will be locked at 5 for the Main Character because he 
            cannot use magic at all, and he will instead get INT.
            */
            PlayableCharacters mainPlayer = new PlayableCharacters("Flynn", new StatSheet(10, 5, 10, 10, 8), 13);
            mainPlayer.DisplayStats();
            Console.WriteLine(mainPlayer.RawStats.ToString());
        }
    }
}