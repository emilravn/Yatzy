using System;
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
        private readonly Scoreboard Scoreboard; // <-- value type (selve .exe filen i mappen)
        private int RollsPerTurn = 3;
        private int AmountOfRounds = 12;

        public Game()
        {
            for (var i = 0; i < diceCup.Length; i++)
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

            EnterPlayer();
            GameStart();
        }

        private void EnterPlayer()
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
            Console.WriteLine("Type 'roll' to start the game.");

            GameStart();
        }

        private void GameStart()
        {
            while (GameShouldStop())
            {
                switch (Console.ReadLine()?.ToLower())
                {
                    case "roll":
                        Turns();
                        AllScorePossibilities();
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

        private void RollAllDice()
        {
            foreach (var aDice in diceCup)
            {
                aDice.Roll();
                Console.Write($"{aDice.Current} ");
            }
        }

        private void Turns()
        {
            if (RollsPerTurn != 0)
            {
                // TODO: The player must roll all six dice in the first roll.
                // TODO: The player must then be able to hold the good dice and roll the remaining dice.
                // TODO: The player should be able to save their scores whenever.
                // TODO: You must be able to nicely print the result of the turn. 
                // TODO: Automamitcally list the possible outcomes of the current status of the each
                // TODO: Keep track of the outcomes already used by the player and the score for each
                // TODO: Keep track of the total score
                // TODO: Be able to drop an outcome (get a zero score), e.g., if the player only has full-house outcome left and does not have full-house.
                RollAllDice();
                //Console.WriteLine("\nWould you like to keep some dice? Allow for reroll.");
                RollsPerTurn--;
            }

            if (RollsPerTurn == 0)
            {
                // Force the user to save a value before doing next lines of code
                Console.WriteLine("\nOut of rolls this turn.");
                AmountOfRounds--;
                RollsPerTurn = 3;
                Scoreboard.CheckUpperSection();
            }
        }

        // TODO: Upper section: the player must do these in any order (if total score of upper is 63 points or above the player gets a 50 points bonus, SUPPORT THE BONUS!)
        // TODO: Play the two sections in order
        // TODO: Yatzy Scores: https://www.rolld6.com/2013/artikkelit/yatzy-eng/

        public int Ones()
        {
            return NumberOf(1);
        }

        public int Twos()
        {
            return NumberOf(2);
        }

        public int Threes()
        {
            return NumberOf(3);
        }

        public int Fours()
        {
            return NumberOf(4);
        }

        public int Fives()
        {
            return NumberOf(5);
        }

        public int Sixes()
        {
            return NumberOf(6);
        }


        // TODO: The remaining outcomes will be played in any order

        public void OnePair()
        {

        }

        public void TwoPair()
        {

        }

        public void ThreeOfAKind()
        {

        }

        public void FourOfAKind()
        {

        }

        public void SmallStraight()
        {

        }

        public void LargeStraight()
        {

        }

        public void FullHouse()
        {

        }

        public void Yatzy()
        {

        }

        // Returns the total sum of roll 
        private int Chance()
        {
            var sum = 0;
            foreach (var aDice in diceCup)
            {
                sum += aDice.Current;
            }
            Console.WriteLine($"\nSum of roll this turn is: {sum}");
            return sum;
        }

        // This should take an integer argument e.g, NumberOf(4) retunrs the number of fours found in the five dice.
        private int NumberOf(int number)
        {
            var count = 0;
            foreach (var aDice in diceCup)
            {
                if (aDice.Current == number)
                    count++;
            }

            if (count != 0)
            {
                Console.WriteLine($"You have {count} dice of {number}s for a scoring value of {number*count}.");
            }

            return count;
        }

        // Collect all single calculations methods in here, present it during the roll round
        private void AllScorePossibilities()
        {
            Chance();
            Ones();
            Twos();
            Threes();
            Fours();
            Fives();
            Sixes();
        }

        public void Save()
        {
            
        }

        private void Hold()
        {
            Console.WriteLine("WIP");
        }

        private void Score()
        {
            Scoreboard.ShowScoreboard();
        }

        private void Bias()
        {
            Console.WriteLine("WIP");
        }

        private void Help()
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

        private static void Exit()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Thank you very much for playing!");
            Console.ResetColor();
            Environment.Exit(1);
        }

        private bool GameShouldStop()
        {
            if (AmountOfRounds == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Your final score was: {Scoreboard.TotalSum()}");
                Console.ResetColor();
                Console.WriteLine("\nThanks for playing!");
            }
            return true;
        }
    }
}