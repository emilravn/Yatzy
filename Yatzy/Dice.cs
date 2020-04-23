using System;

namespace Yatzy
{
    public class Dice
    {
        // To avoid an identical series of random numbers, create a single Random object instead of multiple Random objects. This make the dice(s) fair and random
        private readonly Random Random;
        public int Current { get; set; }
        public bool HoldState { get; set; }

        public Dice()
        {
            HoldState = false;
            Random = new Random();
        }

        // Rolls the dice with a HoldState modifier
        public virtual int Roll()
        {
            if (HoldState == false)
            {
                Current = Random.Next(1, 7);
            }
            else if (HoldState)
            {
                Current = Current;
            }

            return Current;
        }

        // Returns current dice values without rolling.
        public int ReturnRoll()
        {
            return Current;
        }
    }

    // TODO: Inheritance Check with Biased Dice Class
    public class BiasedDice : Dice
    {
        
    }
}