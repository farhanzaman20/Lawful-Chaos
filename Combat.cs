#nullable disable

namespace Shin {
    public class Combat {
        public static void CombatLoop(Game gameController, object[] enemies) {
            BattleState currentState = BattleState.PlayerTurn;

            while (currentState != BattleState.PlayerWin || currentState != BattleState.EnemyWin) {
                if (currentState == BattleState.PlayerTurn) {
                    PlayerTurn(gameController, enemies);
                }
            }
        }

        public static void PlayerTurn(Game gameController, object[] enemies) {
            Console.WriteLine("It is your turn. Please choose an action.");
            Console.WriteLine("1. Melee Attack");
            Console.WriteLine("2. Ranged Attack");
            Console.WriteLine("3. Use Magic");
            Console.WriteLine("4. Defend");
            Console.Write("Action: ");

            string action = Console.ReadLine();
        }
    }

    public enum BattleState {
        PlayerTurn,
        EnemyTurn,
        PlayerWin,
        EnemyWin
    }
}