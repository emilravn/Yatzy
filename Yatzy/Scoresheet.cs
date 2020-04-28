using System;
using System.Collections.Generic; // is a generic class and can store any data types in a form of keys and values. Each key must be unique in the collection.
using System.Linq; // Gives access to methods that allows do all sorts of calculation of objects that implements IEnumerable<T>. 

namespace Yatzy
{
    public class Scoresheet
    {
        public readonly Dictionary<string, int?> Scorecard; // ? for nullable values
        public Scoresheet()
        {
            Scorecard = new Dictionary<string, int?>
            {
                {"Aces", null},
                {"Twos", null},
                {"Threes", null},
                {"Fours", null},
                {"Fives", null},
                {"Sixes", null},
                {"Bonus", null},
                {"One Pair", null},
                {"Two Pair", null},
                {"Three of a Kind", null},
                {"Four of a Kind", null},
                {"Full House", null},
                {"Small Straight", null},
                {"Large Straight", null},
                {"Yatzy", null},
                {"Chance", null}
            };

        }

        public void ShowScoreboard()
        {
            foreach ((string key, int? value) in Scorecard)
            {
                Console.WriteLine($"{key} {value}");
            }
            Console.WriteLine("_____________________________");
            Console.WriteLine($"Current Score: {TotalSum()}");
        }

        public int? TotalSum()
        {
            return Scorecard.Sum(scores => scores.Value);
        }
    }
}
