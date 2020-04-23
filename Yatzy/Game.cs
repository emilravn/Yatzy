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
        public Scoreboard Scoreboard; // <-- value type (selve .exe filen i mappen)
        public int RollsPerTurn = 3;
        public int AmountOfRounds = 12;
        public bool gameShouldStop = false;

        public Game()
        {
            for (int i = 0; i < diceCup.Length; i++)
            {
                diceCup[i] = new Dice();
            }
            Scoreboard = new Scoreboard(); // <-- reference type (shortcut på dit skrivebord)
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
                Console.WriteLine("That's not a valid name. Letters only.");
                Console.Write("Enter your name: ");
                PlayerInput = Console.ReadLine();
            }
            else if (string.IsNullOrEmpty(PlayerInput))
            {
                Console.WriteLine("A nameless Yatzy player? Alright. Let's play Yatzy anyway.");
            }
            else
            {
                Console.WriteLine($"Welcome to Yatzy, {PlayerInput}!");
            }

            Console.WriteLine("You can get an overview of available commands by typing 'help' into the command line");

            GameStart();
        }

        public void GameStart()
        {

            Console.WriteLine("Type 'roll' to start the game.");

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
                        Console.WriteLine("Command not found, type 'help' to show available commands.");
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

                if (RollsPerTurn == RollsPerTurn - 1)
                {
                    // ask the user whether they want to hold any dice, if no roll all dice again, if yes enter a sequence of asking which dice the user would like to keep
                    Console.WriteLine("HERE THE USER SHOULD BE ABLE TO REROLL");
                    foreach (var aDice in diceCup)
                    {
                        aDice.Roll();
                        Console.Write($"{aDice.Current} ");
                    }
                }

                if (RollsPerTurn == 0)
                {
                    Console.WriteLine("Out of rolls this turn.");

                    // Force the user to save a value before doing next lines of code
                    AmountOfRounds--;
                    RollsPerTurn = 3;
                    Scoreboard.CheckUpperSection();
                }
            }

            // When rounds reach 12, the game will stop and print final scoreboard
            if (GameShouldStop())
            {
                GameShouldStop();
                Console.WriteLine("Thanks for playing!");
                Console.ReadKey();
            }
        }


        // Calculations and/or methods to check what is rolled

        public int Chance()
        {
            int sum = 0;
            foreach (var aDice in diceCup )
            {
                sum += aDice.Current;
            }

            return sum;
        }


        public void Hold()
        {
            Console.WriteLine("WIP");
        }

        public void Score()
        {
            Scoreboard.ShowScoreboard();
        }

        public void Bias()
        {
            Console.WriteLine("Feature coming soon!");
        }

        public void Help()
        {
            Console.WriteLine("1. Roll");
            Console.WriteLine("2. Hold");
            Console.WriteLine("3. Score");
            Console.WriteLine("4. Bias");
            Console.WriteLine("5. Quit");

            try
            {
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("You can use the 'roll' command to roll the dices.");
                        break;
                    case 2:
                        Console.WriteLine("You can use the 'hold' command to select a dice and hold it.");
                        break;
                    case 3:
                        Console.WriteLine("You can type 'score' to view scoreboard.");
                        break;
                    case 4:
                        Console.WriteLine(
                            "You can change the degree of how much the dice should be biased by typing 'bias' into the console window.");
                        break;
                    case 5:
                        Console.WriteLine("You can type 'quit' to exit the game completely.");
                        break;
                    default:
                        Console.WriteLine("Number out of range. Type 'help' again to enter help menu.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Please type a number corresponding to what you need help with.");
                Help();
            }
        }

        public static void Exit()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Thank you very much for playing!");
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

// TODO: Automatically list the possible outcomes of the current status of the turn
// TODO: Keep track of the outcomes already used by the player and the score for each
// TODO: Keep track of the total score
// TODO: Be able to drop an outcome(get a zero score), e.g., if the player only has full-house outcome left and does not have full-house