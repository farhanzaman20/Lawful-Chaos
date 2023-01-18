#nullable disable

namespace Shin {
    public static class Dungeon1 {
        public static Game FirstFloor(Game gameController) {
            Console.Clear();
            Console.WriteLine("You enter a Dungeon");
            Console.WriteLine("On the first floor, you find a skeleton");
            gameController.CurrentEnemies.Add(new Enemy(25, 0, 1, "Skeleton"));
            gameController = Combat.CombatLoop(gameController);

            Console.Clear();
            Console.WriteLine("You find a man sitting in a room");

            return gameController;
        }
    }
}