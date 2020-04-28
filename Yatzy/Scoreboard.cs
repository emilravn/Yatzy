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
        public Dictionary<string, int?> Scorecard; // ? for nullable values

        public Scoreboard()
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

        // This methods simply shows the user the current scoreboard upon typing 'score' into the command line.
        public void ShowScoreboard()
        {
            foreach (KeyValuePair<string, int?> score in Scorecard)
            {
                Console.WriteLine($"{score.Key} {score.Value}");
            }
            Console.WriteLine("_____________________________");
            Console.WriteLine($"Current Score: {TotalSum()}");
        }

        // This method calculates the total sum of all values in the scoreboard.
        public int? TotalSum()
        {
            return Scorecard.Sum(scores => scores.Value);
        }
    }
}
