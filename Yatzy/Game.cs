using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Yatzy
{
    /// <summary>
    /// You must write a very short 0.5-1.0 page document that describes the overall structure of the program. In addition, you must document any assumptions that you have made.
    /// If you have use code that is not in the .NET library you must clearly document where the code is from and what you have used it for.
    /// The documentation must be added as a comment at the top of the files that deal with the game class.
    /// </summary>
    public class Game
    {
        private readonly Dice[] diceCup = new Dice[5];
        private readonly Scoreboard Scoreboard;
        private int Rolls = 3;

        public Game()
        {
            for (var i = 0; i < diceCup.Length; i++)
            {
                diceCup[i] = new Dice();
            }

            Scoreboard = new Scoreboard();
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

            Console.WriteLine("You can get an overview of available commands by typing 'help' into the command line");
            Console.WriteLine("Type 'roll' to start the game.");

            GameStart();
        }

        private void GameStart()
        {
            while (GameShouldStop() == false)
            {
                switch (Console.ReadLine()?.ToLower())
                {
                    case "roll":
                        Roll();
                        break;
                    case "hold":
                        Hold();
                        break;
                    case "options":
                        ScorePossibilities();
                        break;
                    case "save":
                        Save();
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

        private void Roll()
        {
            if (Rolls != 0)
            {
                foreach (var aDice in diceCup)
                {
                    if (aDice.Hold == false)
                    {
                        aDice.Roll();
                    }
                    Console.Write($"{aDice.Current} ");
                }

                Console.WriteLine("");

                Rolls--;
            }

            else if (Rolls == 0)
            {
                Console.WriteLine("Out of rolls, you must 'save' a score from your current hand:");
                foreach (var aDice in diceCup)
                {
                    Console.Write($"{aDice.Current} ");
                }
            }
        }

        public int NumberOf(int number)
        {
            var count = 0;
            foreach (var aDice in diceCup)
            {
                if (aDice.Current == number)
                    count++;
            }

            return count * number;
        }

        public void Save()
        {
            if (CheckIfUpperShouldEnd() == false)
            {
                ScorePossibilities();
                FinalUpperSave();
                Score();
            }
            else if (CheckIfLowerSectionShouldEnd() == false)
            {
                Console.WriteLine("You accessed this method!");
            }
        }

        public void ScorePossibilities()
        {
            if (Scoreboard.Scorecard["Aces"] == -1)
            {
                Console.WriteLine($"You can score in Aces for {NumberOf(1)} points!");
            }
            if (Scoreboard.Scorecard["Twos"] == -1)
            {
                Console.WriteLine($"You can score in Twos for {NumberOf(2)} points!");
            }
            if (Scoreboard.Scorecard["Threes"] == -1)
            {
                Console.WriteLine($"You can score in Threes for {NumberOf(3)} points!");
            }
            if (Scoreboard.Scorecard["Fours"] == -1)
            {
                Console.WriteLine($"You can score in Fours for {NumberOf(4)} points!");
            }
            if (Scoreboard.Scorecard["Fives"] == -1)
            {
                Console.WriteLine($"You can score in Fives for {NumberOf(5)} points!");
            }
            if (Scoreboard.Scorecard["Sixes"] == -1)
            {
                Console.WriteLine($"You can score in Sixes for {NumberOf(6)} points!");
            }
        }

        public void FinalUpperSave()
        {

            Console.WriteLine("Type in what you want to save. 'Aces' for Aces etc.");

            if (CheckIfUpperShouldEnd() == false)
            {
                switch (Console.ReadLine()?.ToLower())
                {
                    case "aces":
                        if (Scoreboard.Scorecard["Aces"] == -1)
                        {
                            Scoreboard.Scorecard["Aces"] += NumberOf(1) + 1;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Aces saved!");
                            Console.ResetColor();
                            Rolls = 3;
                            DropHold();
                        }
                        else
                        {
                            Console.WriteLine("Aces has already been scored.");
                        }
                        break;
                    case "twos":
                        break;
                }
            }
            else
            {
                switch (Console.ReadLine().ToLower())
                {
                    // same shizzle, with methods for calculating tghe other stuf
                }
            }

            // TODO: Reset holdstate
            // TODO: Save score
        }

        // Method that checks if the user is eligible for a bonus once the round ends.
        public void Bonus()
        {
            if (Scoreboard.TotalSum() >= 63)
            {
                Scoreboard.Scorecard.Add("Bonus", 50);
                Console.WriteLine("Because you got a total score equal to or above 63 points in the upper section, you have been awarded an extra bonus of 50 points!");
            }
            else
            {
                Console.WriteLine("You didn't get at total score equal to or above 63 points in the upper section, so no bonus  of 50 points was given.");
            }
        }

        // Method that checks if the upper section of Yatzy is done. Method returns true if there has been a score in all of the scoring possibilities in the upper section.
        public bool CheckIfUpperShouldEnd()
        {
            bool UpperSectionCheck = true;
            if (Scoreboard.Scorecard["Aces"] <= 0 ||
                Scoreboard.Scorecard["Twos"] <= 0 ||
                Scoreboard.Scorecard["Threes"] <= 0 ||
                Scoreboard.Scorecard["Fours"] <= 0 ||
                Scoreboard.Scorecard["Fives"] <= 0 ||
                Scoreboard.Scorecard["Sixes"] <= 0)
            {
                UpperSectionCheck = false;
            }
            else
            {
                Bonus();
            }

            return UpperSectionCheck;
        }

        public bool CheckIfLowerSectionShouldEnd()
        {
            bool UpperSectionCheck = !(Scoreboard.Scorecard["One Pair"] <= 0 ||
                                       Scoreboard.Scorecard["Two Pair"] <= 0 ||
                                       Scoreboard.Scorecard["Three of a Kind"] <= 0 ||
                                       Scoreboard.Scorecard["Four of a Kind"] <= 0 ||
                                       Scoreboard.Scorecard["Full House"] <= 0 ||
                                       Scoreboard.Scorecard["Small Straight"] <= 0 ||
                                       Scoreboard.Scorecard["Large Straight"] <= 0 ||
                                       Scoreboard.Scorecard["Yatzy"] <= 0 ||
                                       Scoreboard.Scorecard["Chance"] <= 0);

            return UpperSectionCheck;
        }

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

            return sum;
        }

        private void Hold()
        {
            Console.WriteLine("Type in the format of '1, 2, 3' of those dice you wish to hold.");
            var heldList = Console.ReadLine()?.Split(',').Select(int.Parse).ToList();
            foreach (var heldDice in heldList)
            {
                diceCup.ElementAt(heldDice - 1).Hold = true;
            }
        }

        public void DropHold()
        {
            foreach (var aDice in diceCup)
            {
                aDice.Hold = false;
            }
        }

        private void Score()
        {
            Scoreboard.ShowScoreboard();
        }

        private void Bias()
        {
            Console.WriteLine("WIP");
        }

        // Method that checks if the game should stop.
        private bool GameShouldStop()
        {
            var GameShouldStop = true;
            foreach (var score in Scoreboard.Scorecard)
            {
                if (score.Value <= 0)
                {
                    GameShouldStop = false;
                }
                else if (CheckIfUpperShouldEnd() && CheckIfLowerSectionShouldEnd())
                {
                    Exit();
                }
            }
            return GameShouldStop;
        }

        // NON-GAMEPLAY FEATURES BELOW. THESE ARE ONLY HELPER METHODS FOR THE PLAYER AND EXTRA FLUFFY STUFF. //

        // A method that gives information about what each command does that is callable by the user upon entering 'help' into the command line.
        private static void Help()
        {
            Console.WriteLine("1. Roll");
            Console.WriteLine("2. Hold");
            Console.WriteLine("3. Save");
            Console.WriteLine("4. Score");
            Console.WriteLine("5. Bias");
            Console.WriteLine("6. Quit");

            try
            {
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("You can use the 'roll' command to roll the dice.");
                        break;
                    case 2:
                        Console.WriteLine("You can use the 'hold' command to hold a die.");
                        break;
                    case 3:
                        Console.WriteLine("You can use the 'save' command to save a score to the scoreboard.");
                        break;
                    case 4:
                        Console.WriteLine("You can use the 'score' command to view the current scoreboard.");
                        break;
                    case 5:
                        Console.WriteLine("You can use the 'bias' to use biased dice.");
                        break;
                    case 6:
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

        // Method that is callable by the user that terminates the game and prints the final score upon exit.
        private void Exit()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Thank you very much for playing!\nYour final score was: {Scoreboard.TotalSum()}");
            Console.ResetColor();
            Environment.Exit(1);
        }
    }
}