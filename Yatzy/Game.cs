using System;
using System.Linq;


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
            for (var i = 0; i < diceCup.Length; i++)
            {
                diceCup[i] = new Dice();
            }

            _scoresheet = new Scoresheet();
        }
        public void GameSetup()
        {
            Console.WriteLine("=============================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Welcome to Yatzy 1.0 ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("(by: Ravn and Jakobsen)!");
            Console.ResetColor();
            Console.WriteLine("=============================================");
            Console.WriteLine("You can get an overview of available commands by typing 'help' into the command line");
            Console.WriteLine("Type 'roll' to start the game.");
            GameStart();
        }
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
                        Console.WriteLine("Command not found, type 'help' to show available commands.");
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
                    Console.Write($"{aDice.Current} ");
                }

                CountDice();
                RollBones--;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nRolls left: {RollBones}");
                Console.ResetColor();
                break;
            }

            if (RollBones != 0) return;
            Console.WriteLine("Out of rolls, you must 'save' a score from your current hand.");
            CountDice();
        }
        /// <summary>
        /// Method that checks for possible combinations in the lower section.
        /// It takes a parameter 'minimum' that checks the minimum amount of dice it should check for.
        /// It takes a second parameter 'amount' that specifices how many times a different dice value there should be.
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private int SortDice(int minimum, int amount)
        {
            var sum = 0;
            var count = 0;
            var start = SortedDice.Length - 1;
            for (var i = start; i >= 0; i--)
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
            Console.WriteLine("This is a new round. Roll the dice!");
        }
        private void FinalSave()
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Type in what you want to save by choosing the corresponding number.");
            Console.ResetColor();

            if (!CheckIfUpperShouldEnd())
            {
                try
                {
                    switch (Convert.ToInt32(Console.ReadLine()?.ToLower()))
                    {
                        case 1:
                            if (_scoresheet.Scorecard["Aces"] == null)
                            {
                                _scoresheet.Scorecard["Aces"] = UpperScores(1);
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
                            if (_scoresheet.Scorecard["Twos"] == null)
                            {
                                _scoresheet.Scorecard["Twos"] = UpperScores(2);
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
                            if (_scoresheet.Scorecard["Threes"] == null)
                            {
                                _scoresheet.Scorecard["Threes"] = UpperScores(3);
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
                            if (_scoresheet.Scorecard["Fours"] == null)
                            {
                                _scoresheet.Scorecard["Fours"] = UpperScores(4);
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
                            if (_scoresheet.Scorecard["Fives"] == null)
                            {
                                _scoresheet.Scorecard["Fives"] = UpperScores(5);
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
                            if (_scoresheet.Scorecard["Sixes"] == null)
                            {
                                _scoresheet.Scorecard["Sixes"] = UpperScores(6);
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
                catch (FormatException)
                {
                    Console.WriteLine("I asked for a number, and what you typed didn't match that.");
                }
            }
            else
            {
                try
                {
                    switch (Convert.ToInt32(Console.ReadLine()?.ToLower()))
                    {
                        case 1:
                            if (_scoresheet.Scorecard["One Pair"] == null)
                            {
                                _scoresheet.Scorecard["One Pair"] = OnePair();
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
                            if (_scoresheet.Scorecard["Two Pair"] == null)
                            {
                                _scoresheet.Scorecard["Two Pair"] = TwoPair();
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
                            if (_scoresheet.Scorecard["Three of a Kind"] == null)
                            {
                                _scoresheet.Scorecard["Three of a Kind"] = ThreeOfAKind();
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
                            if (_scoresheet.Scorecard["Four of a Kind"] == null)
                            {
                                _scoresheet.Scorecard["Four of a Kind"] = FourOfAKind();
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
                            if (_scoresheet.Scorecard["Small Straight"] == null)
                            {
                                _scoresheet.Scorecard["Small Straight"] = SmallStraight();
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
                            if (_scoresheet.Scorecard["Large Straight"] == null)
                            {
                                _scoresheet.Scorecard["Large Straight"] = LargeStraight();
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
                            if (_scoresheet.Scorecard["Full House"] == null)
                            {
                                _scoresheet.Scorecard["Full House"] = FullHouse();
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
                            if (_scoresheet.Scorecard["Yatzy"] == null)
                            {
                                _scoresheet.Scorecard["Yatzy"] = Yatzy();
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
                            if (_scoresheet.Scorecard["Chance"] == null)
                            {
                                _scoresheet.Scorecard["Chance"] = Chance();
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
                catch (FormatException)
                {
                    Console.WriteLine("I asked for a number, and what you typed didn't match that.");
                }
            }
        }
        private bool CheckIfUpperShouldEnd()
        {
            bool UpperSectionCheck = !(_scoresheet.Scorecard["Aces"] == null ||
                                       _scoresheet.Scorecard["Twos"] == null ||
                                       _scoresheet.Scorecard["Threes"] == null ||
                                       _scoresheet.Scorecard["Fours"] == null ||
                                       _scoresheet.Scorecard["Fives"] == null ||
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
                    Console.WriteLine($"You can score in (1) Aces for {UpperScores(1)} points!");
                }
                if (_scoresheet.Scorecard["Twos"] == null)
                {
                    Console.WriteLine($"You can score in (2) Twos for {UpperScores(2)} points!");
                }
                if (_scoresheet.Scorecard["Threes"] == null)
                {
                    Console.WriteLine($"You can score in (3) Threes for {UpperScores(3)} points!");
                }
                if (_scoresheet.Scorecard["Fours"] == null)
                {
                    Console.WriteLine($"You can score in (4) Fours for {UpperScores(4)} points!");
                }
                if (_scoresheet.Scorecard["Fives"] == null)
                {
                    Console.WriteLine($"You can score in (5) Fives for {UpperScores(5)} points!");
                }
                if (_scoresheet.Scorecard["Sixes"] == null)
                {
                    Console.WriteLine($"You can score in (6) Sixes for {UpperScores(6)} points!");
                }
            }
            else
            {
                if (_scoresheet.Scorecard["One Pair"] == null)
                {
                    Console.WriteLine($"You can score in (1) One Pair for {OnePair()} points!");
                }
                if (_scoresheet.Scorecard["Two Pair"] == null)
                {
                    Console.WriteLine($"You can score in (2) Two Pair for {TwoPair()} points!");
                }
                if (_scoresheet.Scorecard["Three of a Kind"] == null)
                {
                    Console.WriteLine($"You can score in (3) Three of a Kind for {ThreeOfAKind()} points!");
                }
                if (_scoresheet.Scorecard["Four of a Kind"] == null)
                {
                    Console.WriteLine($"You can score in (4) Four of a Kind for {FourOfAKind()} points!");
                }
                if (_scoresheet.Scorecard["Small Straight"] == null)
                {
                    Console.WriteLine($"You can score in (5) Small Straight for {SmallStraight()} points!");
                }
                if (_scoresheet.Scorecard["Large Straight"] == null)
                {
                    Console.WriteLine($"You can score in (6) Large Straight for {LargeStraight()} points!");
                }
                if (_scoresheet.Scorecard["Full House"] == null)
                {
                    Console.WriteLine($"You can score in (7) Full House for {FullHouse()} points!");
                }
                if (_scoresheet.Scorecard["Yatzy"] == null)
                {
                    Console.WriteLine($"You can score in (8) Yatzy for {Yatzy()} points!");
                }
                if (_scoresheet.Scorecard["Chance"] == null)
                {
                    Console.WriteLine($"You can score in (9) Chance for {Chance()} points!");
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
                Console.WriteLine("For having a sum greater than 62, you get 50 bonus points!");
            }
            else if (CheckIfUpperShouldEnd())
            {
                _scoresheet.Scorecard["Bonus"] = 0;
                Console.WriteLine("Sum is not greater than 63, therefore you don't get 50 bonus points.");
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
            var sum = SortDice(5, 1);
            if (sum < 0)
            {
                sum += 50;
            }
            return sum;
        }
        private int Chance()
        {
            return diceCup.Sum(aDice => aDice.Current);
        }
        private void Hold()
        {
            Console.WriteLine("Type in the format of '1, 2, 3' from left to right of those dice you wish to hold.");
            try
            {
                var heldList = Console.ReadLine()?.Split(',').Select(int.Parse).ToList(); // ToList --> let's make a copy, work on that and then transfer it back to the original.

                if (heldList == null) return;
                foreach (var heldDice in heldList)
                {
                    diceCup.ElementAt(heldDice - 1).Hold = true;
                }
                Console.WriteLine("Dice held successfully!\nKeep rolling.");
            }
            catch (Exception)
            {
                Console.WriteLine("You didn't hold the dice in the correct format, no dice selected for hold.");
            }
        }
        private void DropHeld()
        {
            Console.WriteLine("Type in the format of '1, 2, 3' from left to right of those dice you wish to drop.");
            try
            {
                var heldList = Console.ReadLine()?.Split(',').Select(int.Parse).ToList();

                if (heldList == null) return;
                foreach (var heldDice in heldList)
                {
                    diceCup.ElementAt(heldDice - 1).Hold = false;
                }
                Console.WriteLine("Dice dropped successfully!");
            }
            catch (Exception)
            {
                Console.WriteLine("You didn't drop the dice in the correct format, no dice selected for drop.");
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
            Console.WriteLine("Do you want a positive biased, negative biased or fair dice?\nRemember, changing the dice type mid turn will change your rolls to 0.");
            var changeDice = Console.ReadLine();
            int changeDegree;
            switch (changeDice)
            {
                case "positive":
                    Console.WriteLine("How biased do you want the positive dice to be?\nSelect from low (1) to medium (2) to high (3).");
                    changeDegree = Convert.ToInt32(Console.ReadLine()?.ToLower());
                    while (changeDegree <= 1 && changeDegree >= 3)
                    {
                        Console.WriteLine("Error invalid entry");
                        Console.Write("Enter a valid range: ");
                        changeDegree = int.Parse(Console.ReadLine()?.ToLower() ?? throw new InvalidOperationException());
                    }
                    for (var i = 0; i < diceCup.Length; i++)
                    {
                        diceCup[i] = new BiasedDice(changeDegree, false);
                    }
                    Console.WriteLine($"You have successfully equipped yourself with {changeDice} dice with a degree of {changeDegree}!");
                    break;
                case "negative":
                    Console.WriteLine("How biased do you want the negative dice to be?\nSelect from low (1) to medium (2) to high (3).");
                    changeDegree = Convert.ToInt32(Console.ReadLine()?.ToLower());
                    while (changeDegree <= 1 && changeDegree >= 3)
                    {
                        Console.WriteLine("Error invalid entry");
                        Console.Write("Enter a valid range: ");
                        changeDegree = int.Parse(Console.ReadLine()?.ToLower() ?? throw new InvalidOperationException());
                    }
                    for (var i = 0; i < diceCup.Length; i++)
                    {
                        diceCup[i] = new BiasedDice(changeDegree, true);
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
                        Console.WriteLine("Nothing changed! Have a good game!");
                        break;
                    }
            }
        }
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
            Console.WriteLine("Enter the corresponding number you need help to.");

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
                        Console.WriteLine("You can use the 'save' command to save a score to the scoreboard.\nThis is final and will result in the loss of remaining rolls.");
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
        private void Exit()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Thank you very much for playing!\nYour final score was: {_scoresheet.TotalSum()}");
            Console.ResetColor();
            Console.ReadKey();
            Environment.Exit(1);
        }
    }
}