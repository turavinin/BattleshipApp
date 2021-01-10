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
            // Create Player One
            PlayerInfoModel player = CreatePlayer("Player 1");
            // Create Player Two
            PlayerInfoModel opponent = CreatePlayer("Player 2");

            Console.Clear();

            // Set default shotgrid
            GameLogic.SetDefaultShotGrid(player);
            GameLogic.SetDefaultShotGrid(opponent);


            // Ask ship positions
            do
            {
                DisplayShotGrid(player);
                Console.WriteLine();
                Console.WriteLine();

                // Ask player ship-positions
                string shipSpot = AskPlayerShipSpot(player);

                // Store position FALTA VER SI YA HAY UN BARCO EN ESA POSICION
                GameLogic.AddShipToGrid(player, shipSpot);

                // switch player-opponent
                (player, opponent) = (opponent, player);

                Console.Clear();
            } while (player.PlayerShipSpot.Count < 5);


            




            // LOOP until opponent still have ships
            // Show players shotspot
            //DisplayShotGrid(player);
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

        private static string ValidatePostion(PlayerInfoModel player, string position)
        {
            string output = position;

            // Validate string
            string pattern = @"(^[A-Ea-e][1-5]$)";
            Regex reg = new Regex(pattern);

            // Validate if spot is free
            bool isFreeSpot = GameLogic.ValidateSpot(player, position);


            while (reg.IsMatch(output) == false)
            {
                    Console.Write("Invalid position. Please try again: ");
                    output = Console.ReadLine();
            }

            while (isFreeSpot == false)
            {
                Console.Write("Already Ship. Please try again: ");
                output = Console.ReadLine();
                isFreeSpot = GameLogic.ValidateSpot(player, output);
            }

            return output; 
        }


        private static string AskPlayerShipSpot(PlayerInfoModel player)
        {
            Console.Write($"{player.PlayerName}, place your {player.PlayerShipSpot.Count + 1} ship (ej. A3): ");
            string position = Console.ReadLine();
            string output = ValidatePostion(player, position);

            return output; 
        }

        private static void DisplayShotGrid(PlayerInfoModel player)
        {
            string currentRow = player.PlayerShotGrid[0].SpotLetter;

            foreach (var spot in player.PlayerShotGrid)
            {
                if (currentRow != spot.SpotLetter)
                {
                    Console.WriteLine();
                    currentRow = spot.SpotLetter;
                }

                Console.Write($" {spot.SpotLetter}{spot.SpotNumber} ");
            }
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
