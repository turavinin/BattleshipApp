using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Player One
            PlayerInfoModel player = CreatePlayer("Player 1");
            // Create Player Two
            PlayerInfoModel opponent = CreatePlayer("Player 2");

            

            // LOOP 5 times
                // Ask player 1 ship-positions
                // Check if its a valid position
                // Store position
                // switch player-opponent

            // LOOP 5 times
                // Ask player 2 ship-positions
                // Check if its a valid position
                // Store position
                // switch player-opponent


            // LOOP until opponent still have ships
            // Show players shotspot
            // Ask player shot
            // Check if shot is valid
            // Store shot
            // Check if it hit a ship or water
                // if it hit a ship:
                    // update opponent shipspot
                    // update player shotspot
                // if hit water:
                    // update player shot spot
            // show opponents remaining ships
            // clean console
            // switch player / oppponent

            // anunce the winner
            // show how many shots the winner needed
            // ask if they want to play again



            Console.ReadLine();
        }

        private static PlayerInfoModel CreatePlayer(string defaultPlayerName)
        {
            // Create player instance
            PlayerInfoModel output = new PlayerInfoModel();

            // Ask Player Name
            Console.Write($"{defaultPlayerName}, what is your name: ");

            // Check that the name is not too long and handle blank-space
            bool nameIsValid = false;
            do
            {
                output.PlayerName = Console.ReadLine();

                if (output.PlayerName.Length > 10)
                {
                    Console.Write($"{defaultPlayerName}, your chosen name is too long. Try a shorter one: ");
                }
                else if (output.PlayerName == "")
                {
                    output.PlayerName = "Unnamed General";
                    nameIsValid = true;
                }
                else
                {
                    nameIsValid = true;
                }
            } while (nameIsValid == false);


            return output;
        }
    }
}
