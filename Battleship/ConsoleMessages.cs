using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class ConsoleMessages
    {
        public static void GameMessage()
        {
            Console.WriteLine();
            Console.WriteLine("THE GAME STARTS");
            Console.WriteLine();
        }

        public static void PostionMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Each player will position his five ships.");
            Console.WriteLine();
        }

        public static void WelcomeMessage()
        {
            Console.WriteLine();
            Console.WriteLine("WELCOME TO BATTLESHIP!");
            Console.WriteLine();
        }

        public static void WinnerMessage(PlayerInfoModel winner)
        {
            Console.WriteLine();
            Console.WriteLine($"{winner.PlayerName}, you win the game! You are a true captain!");
            Console.WriteLine();
        }
    }
}
