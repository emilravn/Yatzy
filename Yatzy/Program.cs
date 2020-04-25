using System;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Game Yatzy = new Game();
            Yatzy.GameSetup();
        }
    }
}