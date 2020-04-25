using System;

namespace Yatzy
{
    /// <summary>
    /// This class tells us how a single instance of a dice will behave. You will find two properties here, one for storing the current value and one for whether a dice is held or not.
    /// The constructor builds a dice. The Roll() method rolls the dice between 1-6 (inclusive).
    /// </summary>
    public class Dice
    {
        // To avoid an identical series of random numbers, create a single Random object instead of multiple Random objects. This make the dice(s) fair and random
        public readonly Random Random;
        public int Current { get; set; } // Holds the curent value of the dice

        public bool HoldState { get; set; } // Determines whether a dice is held or not
        // TODO: You must be able to hold the current value of the dice

        public Dice()
        {
            Random = new Random();
        }

        public virtual int Roll()
        {
            return Current = Random.Next(1, 7);
        }

        public override string ToString()
        {
            return $"Current value is {Current}.";
        }
    }

    /// <summary>
    /// This class inherits from the Dice class. The BiasedDice can either be positively or negatively biased during the game.
    /// TODO: You must be able to spawn biased dice and change their degree during the game.
    /// </summary>
    public class BiasedDice : Dice
    {
        private int biasDegree { get; set; }
        private bool isNegative { get; set; }

        public override int Roll()
        {
            base.Roll();

            // The dice is negative. 0 = average, 1 = worse than average, 2 = worser than average.
            if (isNegative)
            {
                switch (biasDegree)
                {
                    case 0:
                        while (Current > 5)
                        {
                            base.Roll();
                        }
                        break;
                    case 1:
                        while (Current > 4)
                        {
                            base.Roll();
                        }
                        break;
                    case 2:
                        while (Current > 3)
                        {
                            base.Roll();
                        }
                        break;
                    default:
                        Console.WriteLine("You can set the negatively biased dice degree between 0-2.");
                        break;
                }

                // The dice is positive. 0 = average, 1 = better than average, 2 = bestest (WoW reference, heck)
                if (!isNegative)
                {
                    switch (biasDegree)
                    {
                        case 0:
                            while (Current < 3)
                            {
                                base.Roll();
                            }
                            break;
                        case 1:
                            while (Current < 4)
                            {
                                base.Roll();
                            }
                            break;
                        case 2:
                            while (Current < 5)
                            {
                                base.Roll();
                            }
                            break;
                        default:
                            Console.WriteLine("You can set the negatively biased dice degree between 0-2.");
                            break;
                    }
                }
            }
            return Current;
        }
    }
}