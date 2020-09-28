using System;
using System.Linq;
using static System.Console; // Import this statically for console based applications to reduce clutter.

namespace Yatzy
{
    public class Game
    {
        private readonly Dice[] diceCup = new Dice[5];
        private readonly Scoresheet _scoresheet;
        private int[] SortedDice { get; set; }
        private int Rounds { get; set; }
        private static int RollBones = 3;

        public Game()
        {
            for (int i = 0; i < diceCup.Length; i++)
            {
                diceCup[i] = new Dice();
            }
            _scoresheet = new Scoresheet();
        }
        public void GameSetup()
        {
            WriteLine("=============================================");
            ForegroundColor = ConsoleColor.Yellow;
            Write("Welcome to Yatzy 1.0 ");
            ForegroundColor = ConsoleColor.Green;
            WriteLine("(by: Ravn and Jakobsen)!");
            ResetColor();
            WriteLine("=============================================");
            WriteLine("You can get an overview of available commands by typing 'help' into the command line");
            WriteLine("Type 'roll' to start the game.");
            GameStart();
        }
        private void GameStart()
        {
            while (!GameShouldStop())
            {
                switch (ReadLine()?.ToLower())
                {
                    case "roll":
                        Roll();
                        break;
                    case "hold":
                        Hold();
                        break;
                    case "drop":
                        DropHeld();
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
                        WriteLine("Command not found, type 'help' to show available commands.");
                        break;
                }
            }
        }
        private void Roll()
        {
            while (RollBones != 0)
            {
                foreach (var aDice in diceCup)
                {
                    if (aDice.Hold == false)
                    {
                        aDice.Roll();
                    }
                    Write($"{aDice.Current} ");
                }

                CountDice();
                RollBones--;
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"\nRolls left: {RollBones}");
                ResetColor();
                break;
            }

            if (RollBones != 0) return;
            WriteLine("Out of rolls, you must 'save' a score from your current hand.");
            CountDice();
        }
        /// <summary>
        /// Method that checks for possible combinations in the lower section.
        /// It takes a parameter 'minimum' that checks the minimum amount of dice it should check for.
        /// It takes a second parameter 'amount' that specifies how many times a different dice value there should be.
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private int SortDice(int minimum, int amount)
        {
            int sum = 0;
            int count = 0;
            int start = SortedDice.Length - 1;
            for (int i = start; i >= 0; i--)
            {
                if (SortedDice[i] < minimum) continue;
                sum += (i + 1) * minimum;
                count++;
                if (count == amount)
                {
                    break;
                }
            }

            if (count < amount)
            {
                sum = 0;
            }
            return sum;
        }
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
        private void SaveMethods()
        {
            ScorePossibilities();
            FinalSave();
            Score();
            WriteLine("This is a new round. Roll the dice!");
        }
        private void FinalSave()
        {

            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Type in what you want to save by choosing the corresponding number.");
            ResetColor();

            if (!CheckIfUpperShouldEnd())
            {
                try
                {
                    switch (Convert.ToInt32(ReadLine()?.ToLower()))
                    {
                        case 1:
                            if (_scoresheet.Scorecard["Aces"] == null)
                            {
                                _scoresheet.Scorecard["Aces"] = UpperScores(1);
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Aces saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Aces has already been scored.");
                            }

                            break;
                        case 2:
                            if (_scoresheet.Scorecard["Twos"] == null)
                            {
                                _scoresheet.Scorecard["Twos"] = UpperScores(2);
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Twos saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Twos has already been scored.");
                            }

                            break;
                        case 3:
                            if (_scoresheet.Scorecard["Threes"] == null)
                            {
                                _scoresheet.Scorecard["Threes"] = UpperScores(3);
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Threes saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Threes has already been scored.");
                            }

                            break;
                        case 4:
                            if (_scoresheet.Scorecard["Fours"] == null)
                            {
                                _scoresheet.Scorecard["Fours"] = UpperScores(4);
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Fours saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Fours has already been scored.");
                            }
                            break;
                        case 5:
                            if (_scoresheet.Scorecard["Fives"] == null)
                            {
                                _scoresheet.Scorecard["Fives"] = UpperScores(5);
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Fives saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Fives has already been scored.");
                            }
                            break;
                        case 6:
                            if (_scoresheet.Scorecard["Sixes"] == null)
                            {
                                _scoresheet.Scorecard["Sixes"] = UpperScores(6);
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Sixes saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Sixes has already been scored.");
                            }
                            break;
                        default:
                            WriteLine("Invalid option, did you type your query correctly?");
                            FinalSave();
                            break;
                    }
                }
                catch (FormatException)
                {
                    WriteLine("I asked for a number, and what you typed didn't match that.");
                }
            }
            else
            {
                try
                {
                    switch (Convert.ToInt32(ReadLine()?.ToLower()))
                    {
                        case 1:
                            if (_scoresheet.Scorecard["One Pair"] == null)
                            {
                                _scoresheet.Scorecard["One Pair"] = OnePair();
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("One Pair saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("One pair has already been scored.");
                            }
                            break;
                        case 2:
                            if (_scoresheet.Scorecard["Two Pair"] == null)
                            {
                                _scoresheet.Scorecard["Two Pair"] = TwoPair();
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Two Pair saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Two Pair has already been scored.");
                            }
                            break;
                        case 3:
                            if (_scoresheet.Scorecard["Three of a Kind"] == null)
                            {
                                _scoresheet.Scorecard["Three of a Kind"] = ThreeOfAKind();
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Three of a Kind saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Three of a Kind has already been scored.");
                            }
                            break;
                        case 4:
                            if (_scoresheet.Scorecard["Four of a Kind"] == null)
                            {
                                _scoresheet.Scorecard["Four of a Kind"] = FourOfAKind();
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Four of a Kind saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Four of a Kind has already been scored.");
                            }
                            break;
                        case 5:
                            if (_scoresheet.Scorecard["Small Straight"] == null)
                            {
                                _scoresheet.Scorecard["Small Straight"] = SmallStraight();
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Small Straight saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Small Straight has already been scored.");
                            }
                            break;
                        case 6:
                            if (_scoresheet.Scorecard["Large Straight"] == null)
                            {
                                _scoresheet.Scorecard["Large Straight"] = LargeStraight();
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Small Straight saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Large Straight has already been scored.");
                            }
                            break;
                        case 7:
                            if (_scoresheet.Scorecard["Full House"] == null)
                            {
                                _scoresheet.Scorecard["Full House"] = FullHouse();
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Full House saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Full House has already been scored.");
                            }
                            break;
                        case 8:
                            if (_scoresheet.Scorecard["Yatzy"] == null)
                            {
                                _scoresheet.Scorecard["Yatzy"] = Yatzy();
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Yatzy saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Yatzy has already been scored.");
                            }
                            break;
                        case 9:
                            if (_scoresheet.Scorecard["Chance"] == null)
                            {
                                _scoresheet.Scorecard["Chance"] = Chance();
                                ForegroundColor = ConsoleColor.Green;
                                WriteLine("Chance saved!");
                                ResetColor();
                                ResetCurrentAndHold();
                            }
                            else
                            {
                                WriteLine("Chance has already been scored.");
                            }
                            break;
                        default:
                            WriteLine("Invalid option, did you type your query correctly?");
                            FinalSave();
                            break;
                    }
                }
                catch (FormatException)
                {
                    WriteLine("I asked for a number, and what you typed didn't match that.");
                }
            }
        }
        private bool CheckIfUpperShouldEnd()
        {
            bool UpperSectionCheck = !(_scoresheet.Scorecard["Aces"] == null |
                                       _scoresheet.Scorecard["Twos"] == null |
                                       _scoresheet.Scorecard["Threes"] == null |
                                       _scoresheet.Scorecard["Fours"] == null |
                                       _scoresheet.Scorecard["Fives"] == null |
                                       _scoresheet.Scorecard["Sixes"] == null);

            return UpperSectionCheck;
        }
        private int NumberOf(int number)
        {
            return diceCup.Count(aDice => aDice.Current == number);
        }
        private void CountDice()
        {
            var diceCount = new int[6];
            for (var i = 0; i < diceCount.Length; i++)
            {
                diceCount[i] = NumberOf(i + 1);
            }

            SortedDice = diceCount;
        }
        private void ScorePossibilities()
        {
            if (!CheckIfUpperShouldEnd())
            {
                if (_scoresheet.Scorecard["Aces"] == null)
                {
                    WriteLine($"You can score in (1) Aces for {UpperScores(1)} points!");
                }
                if (_scoresheet.Scorecard["Twos"] == null)
                {
                    WriteLine($"You can score in (2) Twos for {UpperScores(2)} points!");
                }
                if (_scoresheet.Scorecard["Threes"] == null)
                {
                    WriteLine($"You can score in (3) Threes for {UpperScores(3)} points!");
                }
                if (_scoresheet.Scorecard["Fours"] == null)
                {
                    WriteLine($"You can score in (4) Fours for {UpperScores(4)} points!");
                }
                if (_scoresheet.Scorecard["Fives"] == null)
                {
                    WriteLine($"You can score in (5) Fives for {UpperScores(5)} points!");
                }
                if (_scoresheet.Scorecard["Sixes"] == null)
                {
                    WriteLine($"You can score in (6) Sixes for {UpperScores(6)} points!");
                }
            }
            else
            {
                if (_scoresheet.Scorecard["One Pair"] == null)
                {
                    WriteLine($"You can score in (1) One Pair for {OnePair()} points!");
                }
                if (_scoresheet.Scorecard["Two Pair"] == null)
                {
                    WriteLine($"You can score in (2) Two Pair for {TwoPair()} points!");
                }
                if (_scoresheet.Scorecard["Three of a Kind"] == null)
                {
                    WriteLine($"You can score in (3) Three of a Kind for {ThreeOfAKind()} points!");
                }
                if (_scoresheet.Scorecard["Four of a Kind"] == null)
                {
                    WriteLine($"You can score in (4) Four of a Kind for {FourOfAKind()} points!");
                }
                if (_scoresheet.Scorecard["Small Straight"] == null)
                {
                    WriteLine($"You can score in (5) Small Straight for {SmallStraight()} points!");
                }
                if (_scoresheet.Scorecard["Large Straight"] == null)
                {
                    WriteLine($"You can score in (6) Large Straight for {LargeStraight()} points!");
                }
                if (_scoresheet.Scorecard["Full House"] == null)
                {
                    WriteLine($"You can score in (7) Full House for {FullHouse()} points!");
                }
                if (_scoresheet.Scorecard["Yatzy"] == null)
                {
                    WriteLine($"You can score in (8) Yatzy for {Yatzy()} points!");
                }
                if (_scoresheet.Scorecard["Chance"] == null)
                {
                    WriteLine($"You can score in (9) Chance for {Chance()} points!");
                }
            }
        }
        private int UpperScores(int number)
        {
            var count = diceCup.Count(aDice => aDice.Current == number);

            return count * number;
        }
        private void Bonus()
        {
            if (_scoresheet.TotalSum() >= 63 && CheckIfUpperShouldEnd())
            {
                _scoresheet.Scorecard["Bonus"] = 50;
                WriteLine("For having a sum greater than 62, you get 50 bonus points!");
            }
            else if (CheckIfUpperShouldEnd())
            {
                _scoresheet.Scorecard["Bonus"] = 0;
                WriteLine("Sum is not greater than 63, therefore you don't get 50 bonus points.");
            }
        }
        private int OnePair()
        {
            var sum = SortDice(2, 1);
            return sum;
        }
        private int TwoPair()
        {
            var sum = SortDice(2, 2);
            return sum;
        }
        private int ThreeOfAKind()
        {
            var sum = SortDice(3, 1);
            return sum;
        }
        private int FourOfAKind()
        {
            var sum = SortDice(4, 1);
            return sum;
        }
        private int SmallStraight()
        {
            var sum = 0;
            if (UpperScores(1) == 1 && UpperScores(2) == 2 && UpperScores(3) == 3 && UpperScores(4) == 4 && UpperScores(5) == 5)
            {
                sum = 15;
            }
            return sum;
        }
        private int LargeStraight()
        {
            var sum = 0;
            if (UpperScores(2) == 2 && UpperScores(3) == 3 && UpperScores(4) == 4 && UpperScores(5) == 5 && UpperScores(6) == 6)
            {
                sum = 20;
            }
            return sum;
        }
        private int FullHouse() 
        {
            var sum = 0;
            var threeKind = ThreeOfAKind();
            var onePair = OnePair();
            var twoPair = TwoPair();
            if (threeKind == 0 || twoPair == 0)
            {
                return sum;
            }

            sum = threeKind / 3 == onePair / 2 ? threeKind + (twoPair - onePair) : threeKind + onePair;
            return sum;
        }
        private int Yatzy()
        {
            int sum = SortDice(5, 1);
            if (sum > 0)
            {
                sum = 50;
            }
            return sum;
        }
        private int Chance()
        {
            return diceCup.Sum(aDice => aDice.Current);
        }
        private void Hold()
        {
            WriteLine("Type in the format of '1, 2, 3' from left to right of those dice you wish to hold.");
            try
            {
                var heldList = ReadLine()?.Split(',').Select(int.Parse).ToList(); // ToList --> let's make a copy, work on that and then transfer it back to the original.

                if (heldList == null) return;
                foreach (var heldDice in heldList)
                {
                    diceCup.ElementAt(heldDice - 1).Hold = true;
                }
                WriteLine("Dice held successfully!\nKeep rolling.");
            }
            catch (Exception)
            {
                WriteLine("You didn't hold the dice in the correct format, no dice selected for hold.");
            }
        }
        private void DropHeld()
        {
            WriteLine("Type in the format of '1, 2, 3' from left to right of those dice you wish to drop.");
            try
            {
                var heldList = ReadLine()?.Split(',').Select(int.Parse).ToList();

                if (heldList == null) return;
                foreach (var heldDice in heldList)
                {
                    diceCup.ElementAt(heldDice - 1).Hold = false;
                }
                WriteLine("Dice dropped successfully!");
            }
            catch (Exception)
            {
                WriteLine("You didn't drop the dice in the correct format, no dice selected for drop.");
            }
        }
        private void ResetCurrentAndHold()
        {
            foreach (var aDice in diceCup)
            {
                aDice.Current = 0;
                aDice.Hold = false;
            }
            RollBones = 3;
            Rounds++;
        }
        private void Score()
        {
            _scoresheet.ShowScoreboard();
        }
        private void Bias()
        {
            WriteLine("Do you want a positive biased, negative biased or fair dice?\nRemember, changing the dice type mid turn will change your rolls to 0.");
            var changeDice = ReadLine();
            int changeDegree;
            switch (changeDice)
            {
                case "positive":
                    WriteLine("How biased do you want the positive dice to be?\nSelect from low (1) to medium (2) to high (3).");
                    changeDegree = Convert.ToInt32(ReadLine()?.ToLower());
                    while (changeDegree <= 1 & changeDegree >= 3)
                    {
                        WriteLine("Error invalid entry");
                        Write("Enter a valid range: ");
                        changeDegree = int.Parse(ReadLine()?.ToLower() ?? throw new InvalidOperationException());
                    }
                    for (int i = 0; i < diceCup.Length; i++)
                    {
                        diceCup[i] = new BiasedDice(changeDegree, false);
                    }
                    WriteLine($"You have successfully equipped yourself with {changeDice} dice with a degree of {changeDegree}!");
                    break;
                case "negative":
                    WriteLine("How biased do you want the negative dice to be?\nSelect from low (1) to medium (2) to high (3).");
                    changeDegree = Convert.ToInt32(ReadLine()?.ToLower());
                    while (changeDegree <= 1 & changeDegree >= 3)
                    {
                        WriteLine("Error invalid entry");
                        Write("Enter a valid range: ");
                        changeDegree = int.Parse(ReadLine()?.ToLower() ?? throw new InvalidOperationException());
                    }
                    for (int i = 0; i < diceCup.Length; i++)
                    {
                        diceCup[i] = new BiasedDice(changeDegree, true);
                    }
                    WriteLine($"You have successfully equipped yourself with {changeDice} dice with a degree of {changeDegree}!");
                    break;
                case "fair":
                    for (int i = 0; i < diceCup.Length; i++)
                    {
                        diceCup[i] = new Dice();
                    }
                    WriteLine($"You have successfully equipped yourself with {changeDice} dice!");
                    break;
                default:
                    {
                        WriteLine("Nothing changed! Have a good game!");
                        break;
                    }
            }
        }
        private static void Help()
        {
            WriteLine("1. Roll");
            WriteLine("2. Hold");
            WriteLine("3. Drop");
            WriteLine("4. Save");
            WriteLine("5. Score");
            WriteLine("6. Options");
            WriteLine("7. Bias");
            WriteLine("8. Exit");
            WriteLine("Enter the corresponding number you need help to.");

            try
            {
                switch (Convert.ToInt32(ReadLine()))
                {
                    case 1:
                        WriteLine("You can use the 'roll' command to roll the dice.");
                        break;
                    case 2:
                        WriteLine("You can use the 'hold' command to hold a die.");
                        break;
                    case 3:
                        WriteLine("You can use the 'drop' command to drop a held die.");
                        break;
                    case 4:
                        WriteLine("You can use the 'save' command to save a score to the scoreboard.\nThis is final and will result in the loss of remaining rolls.");
                        break;
                    case 5:
                        WriteLine("You can use the 'score' command to view the current scoreboard.");
                        break;
                    case 6:
                        WriteLine("You can use the 'options' command to view possible scores without losing rolls for the turn. ");
                        break;
                    case 7:
                        WriteLine("You can use the 'bias' to use biased dice.");
                        break;
                    case 8:
                        WriteLine("You can type 'exit' to exit the game completely.");
                        break;
                    default:
                        WriteLine("Number out of range. Type 'help' again to enter the help menu.");
                        break;
                }
            }
            catch (FormatException)
            {
                WriteLine("Please type a number corresponding to what you need help with.");
                Help();
            }
        }
        private bool GameShouldStop()
        {
            bool GameShouldStop = true;
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
        private void Exit()
        {
            ForegroundColor = ConsoleColor.Green;
            WriteLine($"Thank you very much for playing!\nYour final score was: {_scoresheet.TotalSum()}");
            ResetColor();
            ReadKey();
            Environment.Exit(1);
        }
    }
}