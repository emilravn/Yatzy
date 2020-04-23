using System;    
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

namespace Yatzy
{
    /// <summary>
    /// You must write a very short 0.5-1.0 page document that describes the overall structure of the program.
    /// In addition, you must document any assumptions that you have made.
    /// If you have use code that is not in the .NET library you must clearly document where the code is from and what you have used it for.
    /// The documentation must be added as a comment at the top of the files that deal with the game class.
    /// </summary>
    public class Game
    {
        private readonly Dice[] diceCup = new Dice[5];
        // public Scoreboard Scoreboard;
        public int RollsPerTurn = 3;
        public int AmountOfRounds = 12;
        public bool gameShouldStop = false;

        public Game()
        {
            for (int i = 0; i < diceCup.Length; i++)
            {
                diceCup[i] = new Dice();
            }
        }

        public void GameSetup()
        {
            Console.WriteLine("=============================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Welcome to Yatzy 1.0");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" (by: Ravn and Jakobsen)!");
            Console.ResetColor();
            Console.WriteLine("=============================================");

            // EnterPlayer();
            
            GameStart();
            
        }

        public void EnterPlayer()
        {
            Console.Write("Enter your name: ");
            string PlayerInput = Console.ReadLine();
            if (Regex.IsMatch(PlayerInput, "[^A-Za-z_ŠšČčŽžĆćĐđ]"))
            {
                Console.WriteLine("That's not a valid name. Letters only.\n");
                Console.Write("Enter your name: ");
                PlayerInput = Console.ReadLine();
            }
            else if (string.IsNullOrEmpty(PlayerInput))
            {
                Console.WriteLine("A nameless Yatzy player? Alright. Let's play Yatzy anyway.\n");
            }
            else
            {
                Console.WriteLine($"\nWelcome to Yatzy, {PlayerInput}!\n");
            }

            Console.WriteLine("You can get an overview of available commands by typing 'help' into the command line\n");

            GameStart();
        }

        public void GameStart()
        {
            // Scoreboard scoreboard = new Scoreboard();
            Console.WriteLine("Type 'roll' to start the game.\n");

            while (!GameShouldStop())
            {
                switch (Console.ReadLine()?.ToLower())
                {
                    case "roll":
                        RollAllDice();
                        break;
                    case "hold":
                        Hold();
                        break;
                    case "score":
                        Score();
                        break;
                    case "bias":
                        Bias();
                        break;
                    case "help":
                        Help();
                        break;
                    case "quit":
                        Exit();
                        break;
                    default:
                        Console.WriteLine("\nCommand not found, type 'help' to show available commands.\n");
                        break;
                }
            }
        }

        public void RollAllDice()
        {
            if (RollsPerTurn != 0)
            {
                foreach (var aDice in diceCup)
                {
                    aDice.Roll();
                    Console.Write($"{aDice.Current} ");
                }
                
                Console.WriteLine();
                RollsPerTurn--;

                if (RollsPerTurn == 0)
                {
                    Console.WriteLine("Out of rolls this turn.");
                    // Put scores into the scoreboard.
                    AmountOfRounds--;
                    RollsPerTurn =
                        3; // After each round, player will add scores to their scoreboard and it will subtract 1 to the amount of rounds, then restart
                }
            }

            Console.WriteLine("\n");

            // When rounds reach 12, the game will stop and print final scoreboard
            if (GameShouldStop())
            {
                GameShouldStop();
                Console.WriteLine("Thanks for playing!");
                Console.ReadKey();
            }
        }


        public void Hold()
        {
            Console.WriteLine("WIP");
        }
        
        public void Score()
        {
            Console.Write("Current Hand: ");
            foreach (var aDice in diceCup)
            {
                aDice.ReturnRoll();
                Console.Write($"{aDice.Current} ");
            }

            Console.WriteLine();
        }

        public void Bias()
        {
            Console.WriteLine("Feature coming soon!");
        }

        public void Help()
        {
            Console.WriteLine("\n1. Roll");
            Console.WriteLine("2. Hold");
            Console.WriteLine("3. Score");
            Console.WriteLine("4. Bias");
            Console.WriteLine("5. Quit\n");

            try
            {
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("\nYou can use the 'roll' command to roll the dices.\n");
                        break;
                    case 2:
                        Console.WriteLine("\nYou can use the 'hold' command to select a dice and hold it.\n");
                        break;
                    case 3:
                        Console.WriteLine("\nYou can type 'score' to view scoreboard.\n");
                        break;
                    case 4:
                        Console.WriteLine(
                            "\nYou can change the degree of how much the dice should be biased by typing 'bias' into the console window.\n");
                        break;
                    case 5:
                        Console.WriteLine("\nYou can type 'quit' to exit the game completely.\n");
                        break;
                    default:
                        Console.WriteLine("\nNumber out of range. Type 'help' again to enter help menu.\n");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nPlease type a number corresponding to what you need help with.\n");
                Help();
            }
        }
        
        public static void Exit()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThank you very much for playing!");
            Console.ResetColor();
            Environment.Exit(1);
        }

        public bool GameShouldStop()
        {
            if (AmountOfRounds == 0)
            {
                gameShouldStop = true;
            }

            return gameShouldStop;
        }
    }
}