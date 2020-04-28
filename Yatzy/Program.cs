namespace Yatzy
{
    /// <summary>
    ///
    /// Yatzy: https://en.wikipedia.org/wiki/Yatzy
    ///
    /// This game was developed by: Emil Ravn and Lasse Jakobsen.
    /// 
    /// This program is build single player in mind. The game is divided into an upper and a lower section.
    /// The player will at all times roll the five dice. The player can choose to hold these dice and go for a specific combination in each round.
    /// Once all scores has been set, the game terminates and prints the final score.
    ///
    /// Classes: Dice, BiasedDice, Scoresheet, Game, Program.
    ///
    /// Dice Class: This class defines how a dice from the real world would behave and its random value cannot be affected by the player. A die is has a current value and can be held or unheld by the player.
    ///
    /// BiasedDice Class: This class inherits from the Dice Class and can be manipulated by the player to either be positive or negative with a selectable degree ranging from 1 to 3.
    ///
    /// Scoresheet Class: This class is responsible for the scoresheet in a Yatzy game.  
    ///
    /// Game Class: This is the core class, which contains all the Yatzy logic.
    ///
    /// Program Class: This class starts the game.
    ///
    /// Assumptions:
    ///
    /// 1. If the player chooses to use biased dice, the player will lose all remaining rolls for the given round.
    /// 2. If the player does not hit the required combination for a specific round, then the given round is set to 0 points.
    /// 3. To get a clearer image of how the program works, the player can type "help" in the console which will provide a list with possible commands.
    /// 
    /// </summary>
    public static class Program
    {
        private static void Main()
        {
            Game Yatzy = new Game();
            Yatzy.GameSetup();
        }
    }
}