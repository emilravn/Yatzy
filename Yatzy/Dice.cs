using System;

namespace Yatzy
{
    public class Dice
    {
        public readonly Random Random;
        public int Current { get; set; }
        public bool Hold { get; set; } // TODO: You must be able to hold the current value of the dice.

        // The normal dice is not affected by any behaviour of the user, therefore the default behaviour of a normal dice is just a random dice.
        public Dice()
        {
            Hold = false;
            Random = new Random();
        }

        public virtual int Roll()
        {
            if (Hold == false)
            {
                Current = Random.Next(1, 7);

            }
            return Current;
        }

        public override string ToString()
        {
            return $"Current value is {Current}.";
        }
    }

    public class BiasedDice : Dice
    {
        public int DieDegree { get; set; }
        public bool DieNegative { get; set; } = true;

        // When biased dice is being selected for use, this will be the default behaviour of those dice.
        public BiasedDice()
        {
            DieDegree = 1; // Selectable from 1-3 where 1 averagely does better/worse than a fair dice. The further the number (2-3) the more unfair the dice becomes.
            DieNegative = DieNegative;
        }

        public BiasedDice(int DieDegree, bool dieNegative)
        {
            this.DieDegree = DieDegree;
            this.DieNegative = dieNegative;
        }

        public override int Roll()
        {
            base.Roll();
            if (DieNegative) // The die will be negative (true).
            {
                switch (DieDegree)
                {
                    case 1: // You can hit 5 but you can't hit over 5.
                        while (Current > 5)
                        {
                            base.Roll();
                        }
                        break;
                    case 2: // You can hit 4 but you can't hit over 4.
                        while (Current > 4)
                        {
                            base.Roll();
                        }
                        break;
                    case 3: // You can hit 3 but you can't hit over 3.
                        while (Current > 3)
                        {
                            base.Roll();
                        }
                        break;
                    default:
                        return Current;

                }
            }
            else if (!DieNegative) // The die will be positive (true).
            {
                switch (DieDegree)
                {
                    case 0: // You can't hit less than 3.
                        while (Current < 3)
                        {
                            base.Roll();
                        }
                        break;
                    case 1: // You can't hit less than 4.
                        while (Current < 4)
                        {
                            base.Roll();
                        }
                        break;
                    case 2: // You can't hit less than 5.
                        while (Current < 5)
                        {
                            base.Roll();
                        }
                        break;
                    default:
                        return Current;

                }
            }
            return Current;
        }
    }
}