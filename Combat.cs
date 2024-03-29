#nullable disable

namespace Shin {
    public class Combat {
        public static Game CombatLoop(Game gameController) {
            // Starting Battle State
            BattleState currentState = BattleState.PlayerTurn;

            // Main Combat Loop
            while (currentState != BattleState.PlayerWin && currentState != BattleState.EnemyWin) {
                if (currentState == BattleState.PlayerTurn) {
                    PlayerPartyTurn(gameController);
                    if (gameController.CurrentEnemies.Count <= 0) {
                        currentState = BattleState.PlayerWin;
                        PlayerWin();
                    } else {
                        currentState = BattleState.EnemyTurn;
                    }
                } else if (currentState == BattleState.EnemyTurn) {
                    EnemyTurn(gameController);
                    if (gameController.PartyOptions[0].Stats.HP.Current <= 0) {
                        currentState = BattleState.EnemyWin;
                        EnemyWin();
                    } else {
                        currentState = BattleState.PlayerTurn;
                    }
                }
            }

            return gameController;
        }

        private static void BattleGUI(Game gameController) {
            // Player's Party
            Console.WriteLine("{0, -20}{1, 15}{2, 25}{3, 35}", "Player's Party:", "HP", "MP", "XP");
            string playerParty = "";

            for (int i = 0; i < 4; i++) {
                if (gameController.Party[i] >= 0) {
                    // Pulls info from Party Members List based on active party
                    playerParty += String.Format("{0, -20}", gameController.PartyOptions[gameController.Party[i]].Name);
                    playerParty += String.Format("{0, 15}", gameController.PartyOptions[gameController.Party[i]].Stats.HP.ToString());
                    playerParty += String.Format("{0, 25}", gameController.PartyOptions[gameController.Party[i]].Stats.MP.ToString());
                    playerParty += String.Format("{0, 35}", gameController.PartyOptions[gameController.Party[i]].XP.ToString());
                } else {
                    // For the slots that don't have party members, just shows Nobody and 0/0 for HP and MP
                    playerParty += String.Format("{0, -20}", "Nobody");
                    playerParty += String.Format("{0, 15}", "0/0");
                    playerParty += String.Format("{0, 25}", "0/0");
                    playerParty += String.Format("{0, 35}", "0/0");
                }
                playerParty += "\n";
            }
            Console.WriteLine(playerParty);

            // Enemies
            Console.WriteLine("Enemies:");

            string enemyParty = "";

            // Lists the enemies, also numbers them so player can choose which to attack
            for (int i = 0; i < gameController.CurrentEnemies.Count; i++) {
                enemyParty += String.Format("{0, -20}", $"{i + 1}: {gameController.CurrentEnemies[i].Name}");
                enemyParty += String.Format("{0, 15}", gameController.CurrentEnemies[i].HP.ToString());
                enemyParty += "\n";
            }
            Console.WriteLine(enemyParty);
        }

        #region Player's Turn
        private static void PlayerPartyTurn(Game gameController) {
            Console.Clear();

            // Dictionary of Party turn speeds to sort for turn order
            Dictionary<int, int> partyTurnSpd = new Dictionary<int, int>();

            for (int i = 0; i < gameController.Party.Length; i++) {
                if (gameController.Party[i] >= 0) {
                    partyTurnSpd.Add(gameController.Party[i], gameController.PartyOptions[gameController.Party[i]].Stats.TurnSPD);
                }
            }

            var partyTurnOrdered = partyTurnSpd.OrderByDescending(key => key.Value);
            List<int> turnOrder = new List<int>();
            foreach (var item in partyTurnOrdered) {
                turnOrder.Add(item.Key);
            }

            for (int i = 0; i < turnOrder.Count; i++) {
                if (gameController.PartyOptions[turnOrder[i]].Stats.HP.Current > 0) {
                    PartyMemberTurn(gameController, turnOrder[i]);
                }
            }

            for (int i = 0; i < gameController.CurrentEnemies.Count; i++) {
                if (gameController.CurrentEnemies[i].HP.Current <= 0) {
                    for (int q = 0; q < gameController.Party.Length; q++) {
                        if (gameController.Party[q] >= 0 && gameController.PartyOptions[gameController.Party[q]].Stats.HP.Current > 0) {
                            gameController.PartyOptions[gameController.Party[q]].XP.Current += Enemy.EnemyTypes[gameController.CurrentEnemies[i].Type].XPYield;
                        }
                    }
                    gameController.CurrentEnemies.RemoveAt(i);
                }
            }
        }

        private static void PartyMemberTurn(Game gameController, int partyMember) {
            gameController.PartyOptions[partyMember].UpdateStats();
            bool loop = true;

            while (loop) {
                BattleGUI(gameController);
                Console.WriteLine("Player Turn: {0}", gameController.PartyOptions[partyMember].Name);
                Console.WriteLine("1. Melee Attack");
                Console.WriteLine("2. Ranged Attack");
                Console.WriteLine("3. Use Magic");
                Console.WriteLine("4. Defend");
                Console.Write("Action: ");

                string action = Console.ReadLine();

                if (action == "1") {
                    Console.Write("Choose an enemy to attack: ");
                    bool isInputInt = Int32.TryParse(Console.ReadLine(), out int enemyTarget);

                    if (enemyTarget <= gameController.CurrentEnemies.Count && enemyTarget > 0 && isInputInt) {
                        gameController.PartyOptions[partyMember].MeleeAttack(gameController.CurrentEnemies[enemyTarget - 1]);
                        loop = false;
                    } else {
                        Console.WriteLine("Invalid option");
                        Console.ReadKey(true);
                    }
                } else if (action == "2") {
                    Console.Write("Choose an enemy to attack: ");
                    bool isInputInt = Int32.TryParse(Console.ReadLine(), out int enemyTarget);
                    if (enemyTarget <= gameController.CurrentEnemies.Count && enemyTarget > 0 && isInputInt) {
                        if (gameController.PartyOptions[partyMember].Equipped.GunWeapon == -1 || gameController.PartyOptions[partyMember].Equipped.Ammo == -1) {
                            Console.WriteLine("No equipped gun or ammo");
                            Console.ReadKey(true);
                        } else {
                            gameController.PartyOptions[partyMember].GunAttack(gameController.CurrentEnemies[enemyTarget - 1]);
                            loop = false;
                        }
                    } else {
                        Console.WriteLine("Invalid option");
                        Console.ReadKey(true);
                    }
                } else if (action == "3") {
                    if (gameController.PartyOptions[partyMember].Magic.Count > 0) {
                        for (int i = 0; i < gameController.PartyOptions[partyMember].Magic.Count; i++) {
                            Console.WriteLine("{0}. {1}", i + 1, SpellManager.Spells[gameController.PartyOptions[partyMember].Magic[i]].Name);
                        }
                        Console.Write("Spell: ");
                        bool isInputInt = Int32.TryParse(Console.ReadLine(), out int spellOption);

                        if (spellOption > 0 && spellOption <= gameController.PartyOptions[partyMember].Magic.Count && isInputInt) {
                            if (gameController.PartyOptions[partyMember].Stats.MP.Current >= SpellManager.Spells[gameController.PartyOptions[partyMember].Magic[spellOption - 1]].Cost) {
                                SpellManager.Spells[gameController.PartyOptions[partyMember].Magic[spellOption - 1]].Cast(gameController);
                                gameController.PartyOptions[partyMember].Stats.MP.Current -= SpellManager.Spells[gameController.PartyOptions[partyMember].Magic[spellOption - 1]].Cost;
                                loop = false;
                            } else {
                                Console.WriteLine("Not enough MP");
                            }
                        } else {
                            Console.WriteLine("Invalid option");
                            Console.ReadKey(true);
                        }
                    }
                } else if (action == "4") {
                    gameController.PartyOptions[partyMember].Stats.PhysDEF *= 3;
                    loop = false;
                } else {
                    Console.WriteLine("Invalid option");
                    Console.ReadKey(true);
                }
                Console.Clear();
            }
        }
        #endregion

        #region Enemy's Turn
        private static void EnemyTurn(Game gameController) {
            int partyLength = gameController.PartyLenth();

            for (int i = 0; i < gameController.CurrentEnemies.Count; i++) {
                BattleGUI(gameController);
                Console.WriteLine("Enemy Turn: {0}", i + 1);
                int attackedPartyMember = MyLibrary.Random(0, partyLength);
                if (Enemy.EnemyTypes[gameController.CurrentEnemies[i].Type].Magic.Length > 0) {
                    int actionChoice = MyLibrary.Random(1, 5);
                    if (actionChoice == 1) {
                        // Magic
                    } else {
                        gameController.CurrentEnemies[i].MeleeAttack(gameController.PartyOptions[attackedPartyMember]);
                    }
                } else {
                    gameController.CurrentEnemies[i].MeleeAttack(gameController.PartyOptions[attackedPartyMember]);
                }
                Console.Write("Press any key to continue");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
        #endregion

        #region End States
        public static void PlayerWin() {
            Console.Clear();
            Console.WriteLine("You won the battle!");
        }

        public static void EnemyWin() {
            Console.Clear();
            Console.WriteLine("GAME OVER!!");
            Environment.Exit(0);
        }
        #endregion
    }

    internal enum BattleState {
        PlayerTurn,
        EnemyTurn,
        PlayerWin,
        EnemyWin
    }

    public static class RNG {
        public static int CritCalculation(int critChance, int damage) {
            int willCrit = MyLibrary.Random(1, 101);
            if (willCrit <= critChance) {
                damage = Convert.ToInt32(Math.Floor(damage * 1.25));
                Console.WriteLine("Critical hit!");
                Console.ReadKey(true);
            }
            return damage;
        }

        public static bool DodgeCalculation(int accuracy, int evasion) {
            int dodgeChance = evasion - accuracy;
            int willCrit = MyLibrary.Random(1, 101);
            if (willCrit <= dodgeChance) {
                Console.WriteLine("Attack dodged!");
                Console.ReadKey(true);
                return true;
            } else { return false; }
        }
    }
}