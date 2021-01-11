using BattleshipLibrary;
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

        public static void PostionMessage(PlayerInfoModel player)
        {
            Console.WriteLine();
            Console.WriteLine($" {player.PlayerName}, it's your turn to position your ships.");
            Console.WriteLine();
        }

        public static void WelcomeMessage()
        {
            Console.WriteLine();
            Console.WriteLine(" Welcome to Battleship!");
            Console.WriteLine(" Created by Anton Turavinin");
            Console.WriteLine();
        }

        public static void WinnerMessage(PlayerInfoModel winner)
        {
            Console.WriteLine();
            Console.WriteLine($" {winner.PlayerName}, you win the game! You are a true captain!");
        }

        internal static void DisplayPlayerName(PlayerInfoModel player)
        {
            Console.WriteLine();
            Console.WriteLine($" CAPTAIN {player.PlayerName.ToUpper()}");
        }

        public static void ShowShotResult(PlayerInfoModel player, PlayerInfoModel opponent, string shot)
        {
            bool isHit = GameLogic.CheckShotTarget(player, opponent, shot);

            if (isHit == true)
            {
                Console.WriteLine($" {player.PlayerName}, you destroyed a ship!");
            }
            else
            {
                Console.WriteLine($" {player.PlayerName}, you missed!");
            }

        }

        public static void ShowShipsNumber(PlayerInfoModel player, PlayerInfoModel opponent)
        {
            Console.WriteLine($" Your remaining ships: {GameLogic.RemainingShips(player)} ");
            Console.WriteLine($" {opponent.PlayerName} remaining ships: {GameLogic.RemainingShips(opponent)} ");
        }

        internal static void PressAnyKeyMessage()
        {
            Console.WriteLine();
            Console.Write(" Press any key");
            Console.ReadLine();
        }
    }
}
