using BattleshipLibrary;
using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {

            bool playGame = true;

            do
            {
                Game.StartGame();

                // Ask for restart
                Console.WriteLine();
                playGame = DataRequests.RestartMessage();
                Console.Clear();
               
            } while (playGame == true);
           
        }
    }
}
