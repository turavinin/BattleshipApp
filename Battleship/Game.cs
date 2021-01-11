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

            // SHIP POSITIONS
            do
            {
                ConsoleMessages.PostionMessage();

                DisplayShotGrid(player);
                Console.WriteLine();
                Console.WriteLine();

                // Ask player ship-positions
                string shipSpot = DataRequests.AskPlayerShipSpot(player);

                // Store position
                GameLogic.AddShipToGrid(player, shipSpot);

                // switch player-opponent
                (player, opponent) = (opponent, player);

                Console.Clear();
            } while (player.PlayerShipSpot.Count < 5);

            // GAME
            ConsoleMessages.GameMessage();

            PlayerInfoModel winner = null;

            do
            {
                // DisplayShotGrid(player);
                Console.WriteLine();
                DisplayShotGrid(player);
                Console.WriteLine();

                // Ask player shot - validate
                string shot = DataRequests.AskPlayerShot(player);
                Console.WriteLine();


                // Check if it hit a ship or water
                ShowShotResult(player, opponent, shot);

                // Update player shotgrid and opponent ship status
                GameLogic.UpadateGrid(player, opponent, shot);

                // Show opponents remaining ships
                ShowShipsNumber(player, opponent);

                if (GameLogic.RemainingShips(opponent) == 0)
                {
                    winner = player;
                }
                else
                {
                    (player, opponent) = (opponent, player);
                }

                Console.WriteLine();
                Console.Write("Press any key");
                Console.ReadLine();
                Console.Clear();

            } while (winner == null);

            ConsoleMessages.WinnerMessage(winner);
        }
        public static PlayerInfoModel CreatePlayer(string defaultPlayerName)
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

        public static void ShowShotResult(PlayerInfoModel player, PlayerInfoModel opponent, string shot)
        {
            bool isHit = GameLogic.CheckShotTarget(player, opponent, shot);

            if (isHit == true)
            {
                Console.WriteLine($"{player.PlayerName}, you destroyed a ship!");
            }
            else
            {
                Console.WriteLine($"{player.PlayerName}, you missed!");
            }

        }

        public static void ShowShipsNumber(PlayerInfoModel player, PlayerInfoModel opponent)
        {
            Console.WriteLine($"Your remaining ships: {GameLogic.RemainingShips(player)} ");
            Console.WriteLine($"{opponent.PlayerName} remaining ships: {GameLogic.RemainingShips(opponent)} ");
        }
    }
}
