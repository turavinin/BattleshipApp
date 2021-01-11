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
            // Welcome Message
            WelcomeMessage();

            // Create Player One
            PlayerInfoModel player = CreatePlayer("Player 1");
            // Create Player Two
            PlayerInfoModel opponent = CreatePlayer("Player 2");

            Console.Clear();

            // Set default shotgrid
            GameLogic.SetDefaultShotGrid(player);
            GameLogic.SetDefaultShotGrid(opponent);

            // SHIP POSITIONS
            do
            {
                PostionMessage();

                DisplayShotGrid(player);
                Console.WriteLine();
                Console.WriteLine();

                // Ask player ship-positions
                string shipSpot = AskPlayerShipSpot(player);

                // Store position
                GameLogic.AddShipToGrid(player, shipSpot);

                // switch player-opponent
                (player, opponent) = (opponent, player);

                Console.Clear();
            } while (player.PlayerShipSpot.Count < 5);

            // GAME
            GameMessage();

            PlayerInfoModel winner = null;

            do
            {
                // DisplayShotGrid(player);
                DisplayShotGrid(player);

                // Ask player shot - validate
                string shot = AskPlayerShot(player);

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

                Console.ReadLine();

            } while (winner == null);


            




            // LOOP until opponent still have ships =>



                // clean console
                // switch player / oppponent

            // anunce the winner
            // show how many shots the winner needed
            // ask if they want to play again


            Console.ReadLine();
        }

        private static void ShowShipsNumber(PlayerInfoModel player, PlayerInfoModel opponent)
        {
            Console.WriteLine($"Your remaining ships: {GameLogic.RemainingShips(player)} ");
            Console.WriteLine($"{opponent.PlayerName} remaining ships: {GameLogic.RemainingShips(opponent)} ");
        }

        private static void ShowShotResult(PlayerInfoModel player, PlayerInfoModel opponent, string shot)
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

        private static string AskPlayerShot(PlayerInfoModel player)
        {
            Console.WriteLine();
            Console.Write($"{player.PlayerName}, what is your shot? (ej. A3): ");
            string shotElection = Console.ReadLine();

            string output = ValidateShot(player, shotElection);

            return output; 
        }

        private static string ValidateShot(PlayerInfoModel player, string shotElection)
        {
            // Validate string
            string pattern = @"(^[A-Ea-e][1-5]$)";
            Regex reg = new Regex(pattern);

            // Validate shot position
            bool isFreeSpot = false;
            bool checkSpot = false;

            do
            {
                if (reg.IsMatch(shotElection) == false)
                {
                    Console.Write("Invalid shot. Please try again: ");
                    shotElection = Console.ReadLine();
                }
                else if (reg.IsMatch(shotElection) == true && checkSpot == false)
                {
                    isFreeSpot = GameLogic.ValidateShotSpot(player, shotElection);
                    checkSpot = true;
                }
                else if (isFreeSpot == false)
                {
                    Console.Write("You had already shot there. Please try again: ");
                    shotElection = Console.ReadLine();
                    checkSpot = false;
                }
                else if (reg.IsMatch(shotElection) == true && checkSpot == true)
                {
                    isFreeSpot = true;
                }

            } while (reg.IsMatch(shotElection) == false || isFreeSpot == false);

            return shotElection;
        }

        private static void GameMessage()
        {
            Console.WriteLine();
            Console.WriteLine("THE GAME STARTS");
            Console.WriteLine();
        }

        private static void PostionMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Each player will position his five ships.");
            Console.WriteLine();
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine();
            Console.WriteLine("WELCOME TO BATTLESHIP!");
            Console.WriteLine();
        }

        private static string ValidatePostion(PlayerInfoModel player, string position)
        {
            string output = position;

            // Validate string
            string pattern = @"(^[A-Ea-e][1-5]$)";
            Regex reg = new Regex(pattern);

            // Validate if spot is free
            bool isFreeSpot = false;
            bool checkSpot = false;

            do
            {
                if (reg.IsMatch(output) == false)
                {
                    Console.Write("Invalid position. Please try again: ");
                    output = Console.ReadLine();
                }
                else if (reg.IsMatch(output) == true && checkSpot == false)
                {
                    isFreeSpot = GameLogic.ValidateSpot(player, output);
                    checkSpot = true;
                }
                else if (isFreeSpot == false)
                {
                    Console.Write("There is already a ship in that position. Please try again: ");
                    output = Console.ReadLine();
                    checkSpot = false;
                }
                else if(reg.IsMatch(output) == true && checkSpot == true)
                {
                    isFreeSpot = true; 
                }

            } while (reg.IsMatch(output) == false || isFreeSpot == false);

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
