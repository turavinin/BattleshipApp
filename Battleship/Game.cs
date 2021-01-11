using BattleshipLibrary;
using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Game
    {
        public static void StartGame()
        {
            // Welcome Message
            ConsoleMessages.WelcomeMessage();

            // Create Player One
            PlayerInfoModel player = Game.CreatePlayer("Player 1");
            // Create Player Two
            PlayerInfoModel opponent = Game.CreatePlayer("Player 2");

            Console.Clear();

            // Set default shotgrid
            GameLogic.SetDefaultShotGrid(player);
            GameLogic.SetDefaultShotGrid(opponent);

            // Ship positions
            ShipPositions(player);
            Console.Clear();
            ShipPositions(opponent);
            Console.Clear();

            // Game
            PlayerInfoModel winner = StartBattle(player, opponent);
            
            // Message to winner
            ConsoleMessages.WinnerMessage(winner);
        }

        private static PlayerInfoModel StartBattle(PlayerInfoModel player, PlayerInfoModel opponent)
        {
            PlayerInfoModel winner = null;

            do
            {
                // DisplayShotGrid(player);
                ConsoleMessages.DisplayPlayerName(player);
                Console.WriteLine();
                DisplayShotGrid(player);
                Console.WriteLine();

                // Ask player shot - validate
                string shot = DataRequests.AskPlayerShotSpot(player);
                Console.WriteLine();


                // Check if it hit a ship or water
                ConsoleMessages.ShowShotResult(player, opponent, shot);

                // Update player shotgrid and opponent ship status
                GameLogic.UpadateGrid(player, opponent, shot);

                // Show opponents remaining ships
                ConsoleMessages.ShowShipsNumber(player, opponent);

                if (GameLogic.RemainingShips(opponent) == 0)
                {
                    winner = player;
                }
                else
                {
                    (player, opponent) = (opponent, player);
                }

                ConsoleMessages.PressAnyKeyMessage();
                Console.Clear();

            } while (winner == null);

            return winner; 
        }

        private static void ShipPositions(PlayerInfoModel player)
        {
            ConsoleMessages.PostionMessage(player);
            DisplayShotGrid(player);
            Console.WriteLine();
            Console.WriteLine();

            do
            {
                // Ask player ship-positions
                string shipSpot = DataRequests.AskPlayerShipSpot(player);

                // Store position
                GameLogic.AddShipToGrid(player, shipSpot);

            } while (player.PlayerShipSpot.Count < 5);
        }

        public static PlayerInfoModel CreatePlayer(string defaultPlayerName)
        {
            // Create player instance
            PlayerInfoModel output = new PlayerInfoModel();

            // Ask Player Name
            Console.WriteLine($" Information of {defaultPlayerName}");
            Console.Write($" What is your name: ");

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

            Console.WriteLine();
            return output;
        }

        public static void DisplayShotGrid(PlayerInfoModel player)
        {

            string currentRow = player.PlayerShotGrid[0].RowLetter;

            foreach (var spot in player.PlayerShotGrid)
            {
                if (currentRow != spot.RowLetter)
                {
                    Console.WriteLine();
                    currentRow = spot.RowLetter;
                }

                if (spot.Status == GridSpotStatus.Hit)
                {
                    Console.Write($" XX ");
                }
                else if (spot.Status == GridSpotStatus.Miss)
                {
                    Console.Write($" OO ");
                }
                else
                {
                    Console.Write($" {spot.SpotLetter}{spot.SpotNumber} ");
                }


            }
        }

    }
}
