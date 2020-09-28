using System;

namespace Yatzy
{
    public class Dice
    {
        private readonly Random Random;
        public int Current { get; set; }
        public bool Hold { get; set; }
        public Dice()
        {
            Hold = false;
            Random = new Random();
        }

        public virtual void Roll()
        {
            if (Hold == false)
            {
                Current = Random.Next(1, 7);
            }
        }
    }

    public class BiasedDice : Dice
    {
        private int DieDegree { get; }
        private bool DieNegative { get; }

        public BiasedDice(int DieDegree, bool dieNegative)
        {
            this.DieDegree = DieDegree;
            DieNegative = dieNegative;
        }

        public override void Roll()
        {
            base.Roll();
            if (DieNegative) // The dice will be negative (true).
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
                        return;

                }
            }
            else if (!DieNegative) // The dice will be positive (true).
            {
                switch (DieDegree)
                {
                    case 1: // You can't hit less than 2.
                        while (Current < 2)
                        {
                            base.Roll();
                        }
                        break;
                    case 2: // You can't hit less than 3.
                        while (Current < 3)
                        {
                            base.Roll();
                        }
                        break;
                    case 3: // You can't hit less than 4.
                        while (Current < 4)
                        {
                            base.Roll();
                        }
                        break;
                    default:
                        return;
                }
            }
        }
    }
}