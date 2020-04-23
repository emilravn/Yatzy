using System;
using System.Collections.Generic; // is a generic class and can store any data types in a form of keys and values. Each key must be unique in the collection.
using System.Linq; // Gives access to methods that allows do all sorts of calculation of objects that implements IEnumerable<T>. 



namespace Yatzy
{
    /// <summary>
    /// This class is responsible for the scoreboard throughout the entirety of a Yatzy Game. The constructor initiates what a scoreboard will include and as in this it resembles a scorecard of a Yatzy game.
    /// </summary>
    public class Scoreboard
    {
        private readonly Dictionary<string, int> Scorecard;

        public Scoreboard()
        {
            Scorecard = new Dictionary<string, int>
            {
                {"Aces", 0},
                {"Twos", 0},
                {"Threes", 0},
                {"Fours", 0},
                {"Fives", 0},
                {"Sixes", 0},
                {"One Pair", 0},
                {"Two Pair", 0},
                {"Three of a Kind", 0},
                {"Four of a Kind", 0},
                {"Full House", 0},
                {"Small Straight", 0},
                {"Large Straight", 0},
                {"Yatzy", 0},
                {"Chance", 0}
            };
        }

        public void CheckUpperSection()
        {
            if (Scorecard["Aces"] != 0 &&
                Scorecard["Twos"] != 0 &&
                Scorecard["Threes"] != 0 &&
                Scorecard["Fours"] != 0 &&
                Scorecard["Fives"] != 0 &&
                Scorecard["Sixes"] != 0)
            {
                Bonus();
                LowerSection();
            }
        }

        public int Aces()
        {
          
        }

        public void LowerSection()
        {
            Console.WriteLine("Lower Section Initiated");
        }

        // Computes the sum of the sequence of the int values in the dictionary above.
        public int TotalUpperSectionSum()
        {
            return Scorecard.Sum(scores => scores.Value);
        }

        // Checks whether the user is eligible for a bonus
        public void Bonus()
        {
            if (TotalUpperSectionSum() >= 63)
            {
                Scorecard.Add("Bonus", 50);
                Console.WriteLine("Because you got a total score equal to or above 63 points in the upper section, you have been awarded an extra bonus of 50 points!");
            }
        }

        public void ShowScoreboard()
        {
            foreach (KeyValuePair<string, int> score in Scorecard)
            {
                Console.WriteLine($"{score.Key} {score.Value}");
            }
        }
    }
}
