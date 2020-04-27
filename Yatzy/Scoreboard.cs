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
        public Dictionary<string, int> Scorecard;

        public Scoreboard()
        {
            Scorecard = new Dictionary<string, int>
            {
                {"Aces", -1},
                {"Twos", -1},
                {"Threes", -1},
                {"Fours", -1},
                {"Fives", -1},
                {"Sixes", -1},
                {"One Pair", -1},
                {"Two Pair", -1},
                {"Three of a Kind", -1},
                {"Four of a Kind", -1},
                {"Full House", -1},
                {"Small Straight", -1},
                {"Large Straight", -1},
                {"Yatzy", -1},
                {"Chance", -1}
            };
        }

        // This methods simply shows the user the current scoreboard upon typing 'score' into the command line.
        public void ShowScoreboard()
        {
            foreach (KeyValuePair<string, int> score in Scorecard)
            {
                Console.WriteLine($"{score.Key} {score.Value}");
            }
            Console.WriteLine("_____________________________");
            Console.WriteLine($"Current Score: {TotalSum()}");
        }

        // This method calculates the total sum of all values in the scoreboard.
        public int TotalSum()
        {
            return Scorecard.Sum(scores => scores.Value);
        }
    }
}
