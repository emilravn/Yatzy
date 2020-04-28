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
    ///
    /// Assumptions:
    /// It is assumed that the player will overall behave accordingly as a game of Yatzhee in real life. The player can, if smart, break some of the core features if they play around with user defined commands.
    /// To avoid too much cheating/breaking the game code, setting the biased dice will set rolls for 0 so that the user cannot keep rolling dice over and over again when selecting a bias or fair dice.
    ///
    /// 
    /// </summary>
    public class Game
    {
        private readonly Dice[] diceCup = new Dice[5];
        private readonly Scoreboard Scoreboard;
        public int Rounds { get; set; }
        private int RollBones { get; set; } = 3;
        // TODO: Ændres pt. to steder i programmet


        public Game()
        {
            for (var i = 0; i < diceCup.Length; i++)
            {
                diceCup[i] = new Dice();
                // diceCup[i] = new BiasedDice(1, true);
            }

            Scoreboard = new Scoreboard();
        }

        // TODO: Description
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

        // TODO: Description
        private void GameStart()
        {
            while (!GameShouldStop())
            {
                switch (Console.ReadLine()?.ToLower())
                {
                    case "roll":
                        Roll();
                        break;
                    case "hold":
                        Hold();
                        break;
                    case "drop":
                        ResetHold();
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
                    case "exit":
                        Exit();
                        break;
                    default:
                        Console.WriteLine("Command not found, type 'help' to show available commands.");
                        break;
                }
            }
        }

        // TODO: Description
        private void Roll()
        {
            if (RollBones != 0)
            {
                foreach (var aDice in diceCup)
                {
                    if (aDice.Hold == false)
                    {
                        aDice.Roll();
                    }
                    Console.Write($"{aDice.Current} ");
                }

                RollBones--;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nRolls left: {RollBones}");
                Console.ResetColor();
            }

            else if (RollBones == 0)
            {
                Console.WriteLine("Out of rolls, you must 'save' a score from your current hand:");
                foreach (var aDice in diceCup)
                {
                    Console.Write($"{aDice.Current} ");
                }
            }
        }

        // TODO: Description
        private int NumberOf(int number)
        {
            var count = 0;
            foreach (var aDice in diceCup)
            {
                if (aDice.Current == number)
                    count++;
            }

            return count * number;
        }

        // TODO: Description
        private void Save()
        {
            if (!CheckIfUpperShouldEnd())
            {
                SaveMethods();
                Bonus();
            }
            else
            {
                SaveMethods();
            }
        }

        // TODO: Description
        private void SaveMethods()
        {
            ScorePossibilities();
            FinalSave();
            Score();
            Console.WriteLine("A new round has begun.");
        }

        // TODO: Description
        private void ScorePossibilities()
        {
            if (!CheckIfUpperShouldEnd())
            {
                if (Scoreboard.Scorecard["Aces"] == null)
                {
                    Console.WriteLine($"You can score in (1) Aces for {NumberOf(1)} points!");
                }
                if (Scoreboard.Scorecard["Twos"] == null)
                {
                    Console.WriteLine($"You can score in (2) Twos for {NumberOf(2)} points!");
                }
                if (Scoreboard.Scorecard["Threes"] == null)
                {
                    Console.WriteLine($"You can score in (3) Threes for {NumberOf(3)} points!");
                }
                if (Scoreboard.Scorecard["Fours"] == null)
                {
                    Console.WriteLine($"You can score in (4) Fours for {NumberOf(4)} points!");
                }
                if (Scoreboard.Scorecard["Fives"] == null)
                {
                    Console.WriteLine($"You can score in (5) Fives for {NumberOf(5)} points!");
                }
                if (Scoreboard.Scorecard["Sixes"] == null)
                {
                    Console.WriteLine($"You can score in (6) Sixes for {NumberOf(6)} points!");
                }
            }
            else
            {
                //    if (Scoreboard.Scorecard["One Pair"] == null)
                //    {
                //        Console.WriteLine($"You can score in (1) One Pair for {OnePair()} points!");
                //    }
                //    if (Scoreboard.Scorecard["Two Pair"] == null)
                //    {
                //        Console.WriteLine($"You can score in (2) Two Pair for {TwoPair()} points!");
                //    }
                //    if (Scoreboard.Scorecard["Three of a Kind"] == null)
                //    {
                //        Console.WriteLine($"You can score in (3) Three of a Kind for {ThreeOfAKind()} points!");
                //    }
                //    if (Scoreboard.Scorecard["Four of a Kind"] == null)
                //    {
                //        Console.WriteLine($"You can score in (4) Four of a Kind for {FourOfAKind()} points!");
                //    }
                //    if (Scoreboard.Scorecard["Small Straight"] == null)
                //    {
                //        Console.WriteLine($"You can score in (5) Full House for {SmallStraight()} points!");
                //    }
                //    if (Scoreboard.Scorecard["Large Straight"] == null)
                //    {
                //        Console.WriteLine($"You can score in (6) Small Straight for {LargeStraight()} points!");
                //    }
                //    if (Scoreboard.Scorecard["Full House"] == null)
                //    {
                //        Console.WriteLine($"You can score in (7) Large Straight for {FullHouse()} points!");
                //    }
                //    if (Scoreboard.Scorecard["Yatzy"] == null)
                //    {
                //        Console.WriteLine($"You can score in (8) Yatzy for {Yatzy()} points!");
                //    }
                if (Scoreboard.Scorecard["Chance"] == null)
                {
                    Console.WriteLine($"You can score in (9) Chance for {Chance()} points!");
                }
            }
        }

        // TODO: Description
        public void FinalSave()
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Type in what you want to save by choosing the appropriate number.");
            Console.ResetColor();

            if (!CheckIfUpperShouldEnd())
            {
                switch (Convert.ToInt32(Console.ReadLine()?.ToLower()))
                {
                    case 1:
                        if (Scoreboard.Scorecard["Aces"] == null)
                        {
                            Scoreboard.Scorecard["Aces"] = NumberOf(1);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Aces saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Aces has already been scored.");
                        }

                        break;
                    case 2:
                        if (Scoreboard.Scorecard["Twos"] == null)
                        {
                            Scoreboard.Scorecard["Twos"] = NumberOf(2);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Twos saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Twos has already been scored.");
                        }

                        break;
                    case 3:
                        if (Scoreboard.Scorecard["Threes"] == null)
                        {
                            Scoreboard.Scorecard["Threes"] = NumberOf(3);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Threes saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Threes has already been scored.");
                        }

                        break;
                    case 4:
                        if (Scoreboard.Scorecard["Fours"] == null)
                        {
                            Scoreboard.Scorecard["Fours"] = NumberOf(4);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Fours saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Fours has already been scored.");
                        }

                        break;
                    case 5:
                        if (Scoreboard.Scorecard["Fives"] == null)
                        {
                            Scoreboard.Scorecard["Fives"] = NumberOf(5);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Fives saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Fives has already been scored.");
                        }

                        break;
                    case 6:
                        if (Scoreboard.Scorecard["Sixes"] == null)
                        {
                            Scoreboard.Scorecard["Sixes"] = NumberOf(6);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Sixes saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Sixes has already been scored.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid option, did you type your query correctly?");
                        FinalSave();
                        break;
                }
            }
            else
            {
                switch (Convert.ToInt32(Console.ReadLine()?.ToLower()))
                {
                    case 1:
                        if (Scoreboard.Scorecard["One Pair"] == null)
                        {
                            //Scoreboard.Scorecard["One Pair"] = OnePair();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("One Pair saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("One pair has already been scored.");
                        }

                        break;
                    case 2:
                        if (Scoreboard.Scorecard["Two Pair"] == null)
                        {
                            //Scoreboard.Scorecard["Two Pair"] = TwoPair();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Two Pair saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Two Pair has already been scored.");
                        }

                        break;
                    case 3:
                        if (Scoreboard.Scorecard["Three of a Kind"] == null)
                        {
                            //Scoreboard.Scorecard["Three of a Kind"] = ThreeOfAKind();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Three of a Kind saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Three of a Kind has already been scored.");
                        }

                        break;
                    case 4:
                        if (Scoreboard.Scorecard["Four of a Kind"] == null)
                        {
                            //Scoreboard.Scorecard["Four of a Kind"] = FourOfAKind();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Four of a Kind saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Four of a Kind has already been scored.");
                        }

                        break;
                    case 5:
                        if (Scoreboard.Scorecard["Small Straight"] == null)
                        {
                            //Scoreboard.Scorecard["Small Straight"] = SmallStraight();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Small Straight saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Small Straight has already been scored.");
                        }

                        break;
                    case 6:
                        if (Scoreboard.Scorecard["Large Straight"] == null)
                        {
                            //Scoreboard.Scorecard["Large Straight"] = LargeStraight();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Small Straight saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Large Straight has already been scored.");
                        }
                        break;
                    case 7:
                        if (Scoreboard.Scorecard["Full House"] == null)
                        {
                            //Scoreboard.Scorecard["Full House"] = FullHouse();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Full House saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Full House has already been scored.");
                        }
                        break;
                    case 8:
                        if (Scoreboard.Scorecard["Yatzy"] == null)
                        {
                            //Scoreboard.Scorecard["Yatzy"] = Yatzy();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Yatzy saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Yatzy has already been scored.");
                        }
                        break;
                    case 9:
                        if (Scoreboard.Scorecard["Chance"] == null)
                        {
                            Scoreboard.Scorecard["Chance"] = Chance();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Chance saved!");
                            Console.ResetColor();
                            ResetCurrentAndHold();
                        }
                        else
                        {
                            Console.WriteLine("Chance has already been scored.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid option, did you type your query correctly?");
                        FinalSave();
                        break;
                }
            }
        }

        // TODO: Description
        public void ResetCurrentAndHold()
        {
            foreach (var aDice in diceCup)
            {
                aDice.Current = 0;
                aDice.Hold = false;
            }

            RollBones = 3;
            Rounds++;
        }

        // Method that checks if the user is eligible for a bonus once the round ends.
        public void Bonus()
        {
            if (Scoreboard.TotalSum() >= 63 && CheckIfUpperShouldEnd())
            {
                Scoreboard.Scorecard["Bonus"] = 50;
                Console.WriteLine("Sum is equal to or greater than 63, therefore you get a bonus of 50 points!");
            }
            else if (CheckIfUpperShouldEnd())
            {
                Scoreboard.Scorecard["Bonus"] = 0;
                Console.WriteLine("Sum is not equal to or greater than 63, therefore you get don't a bonus of 50 points.");
            }
        }

        // Method that checks if the upper section of Yatzy is done. Method returns true if there has been a score in all of the scoring possibilities in the upper section.
        public bool CheckIfUpperShouldEnd()
        {
            bool UpperSectionCheck = !(Scoreboard.Scorecard["Aces"] == null ||
                                       Scoreboard.Scorecard["Twos"] == null ||
                                       Scoreboard.Scorecard["Threes"] == null ||
                                       Scoreboard.Scorecard["Fours"] == null ||
                                       Scoreboard.Scorecard["Fives"] == null ||
                                       Scoreboard.Scorecard["Sixes"] == null);

            return UpperSectionCheck;
        }

        // TODO: Description
        public void OnePair() // alle lower sections skal returnere en int for deres calculation, ingen bool, da det stadig skal være muligt at score 0
        {
            // 2 dice with the same faces. The score is the total of these two die faces.
        }

        // TODO: Description
        public void TwoPair()
        {
            // One pair, and another pair of dice with different faces from each other. The score is the total of these four die faces. Example: 6+6+5+5=22
        }

        // TODO: Description
        public void ThreeOfAKind()
        {
            //  For 3 of a kind, 3 die faces must be the same; for 4 of a kind, 4 must be the same. The score is the total of all the 3 or 4 dice
        }

        // TODO: Description
        public void FourOfAKind()
        {
            //  For 3 of a kind, 3 die faces must be the same; for 4 of a kind, 4 must be the same. The score is the total of all the 3 or 4 dice
        }

        // TODO: Description
        public void SmallStraight()
        {
            //  A straight is a sequence of consecutive die faces; a small straight is made up of die faces 1-2-3-4-5 and scores 15 points; a large straight is made up of die faces 2-3-4-5-6 and scores 20 points.
        }

        // TODO: Description
        public void LargeStraight()
        {
            //  A straight is a sequence of consecutive die faces; a small straight is made up of die faces 1-2-3-4-5 and scores 15 points; a large straight is made up of die faces 2-3-4-5-6 and scores 20 points.
        }

        // TODO: Description
        public void FullHouse()
        {
            //  A Full House as in poker is a combination of 3 of a kind and 2 of a kind. The score is the total of the die faces. Example: 6+6+6+5+5=28
        }

        // TODO: Description
        public void Yatzy()
        {
            // Yatzy is 5 of a kind and scores 50 points, but you can choose to score the roll in other categories instead. Example: You roll 6-6-6-6-6. You can choose to score this as a Yatzy (50), 4 of a kind (24) or in the Upper Section ‘6’ (30).
        }

        // Returns the total sum of all dice in a given roll 
        private int Chance() // Roll anything and put it into the Chance category, the score is the total of the die faces.
        {
            var sum = 0;
            foreach (var aDice in diceCup)
            {
                sum += aDice.Current;
            }

            return sum;
        }

        // Method to hold dice.
        private void Hold()
        {
            Console.WriteLine("Type in the format of '1, 2, 3' from left to right of those dice you wish to hold.");
            try
            {
                var heldList = Console.ReadLine()?.Split(',').Select(int.Parse).ToList(); // ToList --> let's make a copy, work on that and then transfer it back to the original.

                if (heldList != null)
                {
                    foreach (var heldDice in heldList)
                    {
                        diceCup.ElementAt(heldDice - 1).Hold = true;
                    }

                    Console.WriteLine("Dice held successfully");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("You didn't hold dice in the correct format, no dice selected for hold.");
            }
        }

        // Method to rest held dice.
        public void ResetHold()
        {
            Console.WriteLine("Type in the format of '1, 2, 3' from left to right of those dice you wish to drop.");
            try
            {
                var heldList = Console.ReadLine()?.Split(',').Select(int.Parse).ToList();

                if (heldList != null)
                {
                    foreach (var heldDice in heldList)
                    {
                        diceCup.ElementAt(heldDice - 1).Hold = false;
                    }

                    Console.WriteLine("Dice dropped successfully");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("You didn't drop dice in the correct format, no dice selected for drop.");
            }
        }

        // Method to show the scoreboard.
        private void Score()
        {
            Scoreboard.ShowScoreboard();
        }

        // Method that allows user to change to biased dice.
        private void Bias()
        {
            Console.WriteLine("Do you want a positive biased, negative biased or fair dice?\n Remember, changing the dice type mid turn will change your rolls to 0. ");
            string changeDice = Console.ReadLine();
            int changeDegree;
            switch (changeDice)
            {
                case "positive":
                    Console.WriteLine(
                        "How biased do you want the positive dice to be?\nSelect from low (1) to high (3).");
                    changeDegree = Convert.ToInt32(Console.ReadLine());
                    while (changeDegree <= 1 && changeDegree >= 3)
                    {
                        Console.WriteLine("Error invalid entry");
                        Console.Write("Enter a valid range: ");
                        changeDegree = int.Parse(Console.ReadLine());
                    }
                    for (var i = 0; i < diceCup.Length; i++)
                    {
                        diceCup[i] = new BiasedDice(changeDegree, false);
                    }
                    Console.WriteLine($"You have successfully equipped yourself with {changeDice} dice with a degree of {changeDegree}!");
                    break;
                case "negative":
                    Console.WriteLine("How biased do you want the negative dice?\nSelect from low (1) to high (3).");
                    changeDegree = Convert.ToInt32(Console.ReadLine());
                    while (changeDegree <= 1 && changeDegree >= 3)
                    {
                        Console.WriteLine("Error invalid entry");
                        Console.Write("Enter a valid range: ");
                        changeDegree = int.Parse(Console.ReadLine());
                    }
                    for (var i = 0; i < diceCup.Length; i++)
                    {
                        diceCup[i] = new BiasedDice(changeDegree, false);
                    }
                    Console.WriteLine($"You have successfully equipped yourself with {changeDice} dice with a degree of {changeDegree}!");
                    break;
                case "fair":
                    for (var i = 0; i < diceCup.Length; i++)
                    {
                        diceCup[i] = new Dice();
                    }
                    Console.WriteLine($"You have successfully equipped yourself with {changeDice} dice!");
                    break;
                default:
                    {
                        Console.WriteLine("Nothing changed! Good day to you!");
                        break;
                    }
            }
        }

        // Method that checks if the game should stop.
        private bool GameShouldStop()
        {
            var GameShouldStop = true;
            if (Rounds != 15)
            {
                GameShouldStop = false;
            }
            else
            {
                Exit();
            }
            return GameShouldStop;
        }

        // NON-GAMEPLAY FEATURES BELOW. THESE ARE ONLY HELPER METHODS FOR THE PLAYER AND EXTRA FLUFFY STUFF. //

        // A method that gives information about what each command does that is callable by the user upon entering 'help' into the command line.
        private static void Help()
        {
            Console.WriteLine("1. Roll");
            Console.WriteLine("2. Hold");
            Console.WriteLine("3. Drop");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Score");
            Console.WriteLine("6. Options");
            Console.WriteLine("7. Bias");
            Console.WriteLine("8. Exit");

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
                        Console.WriteLine("You can use the 'drop' command to drop a held die.");
                        break;
                    case 4:
                        Console.WriteLine("You can use the 'save' command to save a score to the scoreboard. This is final.");
                        break;
                    case 5:
                        Console.WriteLine("You can use the 'score' command to view the current scoreboard.");
                        break;
                    case 6:
                        Console.WriteLine("You can use the 'options' command to view possible scores without losing rolls for the turn. ");
                        break;
                    case 7:
                        Console.WriteLine("You can use the 'bias' to use biased dice.");
                        break;
                    case 8:
                        Console.WriteLine("You can type 'exit' to exit the game completely.");
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