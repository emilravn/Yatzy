using System;

namespace Yatzy
{
    /// <summary>
    /// You must write a very short 0.5-1.0 page document that describes the overall structure of the program.
    /// In addition, you must document any assumptions that you have made.
    /// If you have use code that is not in the .NET library you must clearly document where the code is from and what you have used it for.
    /// The documentation must be added as a comment at the top of the files that deal with the game class.
    /// </summary>
    public class Program

    {
        static void Main(string[] args)
        {
            Game Yatzy = new Game();
            Yatzy.GameSetup();
        }
    }
}