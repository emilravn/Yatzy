using System;

namespace Yatzy
{
    /// <summary>
    /// This class tells us how a single instance of a dice will behave. You will find two properties here, one for storing the current value and one for whether a dice is held or not.
    /// The constructor builds a dice. The Roll() method rolls the dice between 1-6 (inclusive). The ReturnRoll() method returns what is rolled without rolling it.
    /// The ToString() methods is how a dice will be represented if writing out the dice to the console without methods on it.
    /// </summary>
    public class Dice
    {
        // To avoid an identical series of random numbers, create a single Random object instead of multiple Random objects. This make the dice(s) fair and random
        private readonly Random Random;
        public int Current { get; set; }
        public bool HoldState { get; set; }

        public Dice()
        {
            Random = new Random();
        }

        public virtual int Roll()
        {
            return Current = Random.Next(1, 7);
        }

        public int ReturnRoll()
        {
            return Current;
        }

        public override string ToString()
        {
            return "Current Value of Dice: ";
        }
    }

    // TODO: Inheritance Check with Biased Dice Class
    public class BiasedDice : Dice
    {
        
    }
}