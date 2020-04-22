// using System;
//
// namespace Yatzy
// {
//     // A scoreboard to keep track of the scores during the game.Sorted in arrays.
//     // Presents Rounds in a string array.
//     // Presents scores in an int array.
//     public class Scoreboard
//     {
//         public static string[] Rounds =
//         {
//             "Ones", "Twos", "Threes", "Fours", "Fives", "Sixes", "Bonus eligible", "One pair", "Two pairs",
//             "Three of a kind", "Four of a kind", "Small straight", "Large straight", "Full house", "Chance", "Yatzy",
//             "Final result"
//         };
//
//         public static int[] Scores = new int[16];
//
//         // X represents the Variable we will use to get the scores from each round
//         Scores[0] = X
//             Scores[1] = X
//             Scores[2] = X
//             Scores[3] = X
//             Scores[4] = X
//             Scores[5] = X
//             Scores[6] = X
//             Scores[7] = X
//             Scores[8] = X
//             Scores[9] = X
//             Scores[10] = X
//             Scores[11] = X
//             Scores[12] = X
//             Scores[13] = X
//             Scores[14] = X
//             Scores[15] = X
//             Method to show the Scoreboard using the arrays.For this we will use a for loop
//
//         public static void ShowScoreboard()
//         {
//             for (int i = 0; i < Scores.Length; i++)
//             {
//                 Console.Write(Rounds[i] + " ");
//                 Console.WriteLine(Scores[i]);
//             }
//         }
//     }
// }