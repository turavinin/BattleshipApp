using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BattleshipLibrary;
using BattleshipLibrary.Models;

namespace Battleship
{
    public class DataRequests
    {
        public static string AskPlayerShot(PlayerInfoModel player)
        {
            Console.WriteLine();
            Console.Write($"{player.PlayerName}, what is your shot? (ej. A3): ");
            string shotElection = Console.ReadLine();

            string output = ValidateShot(player, shotElection);

            return output;
        }

        internal static bool RestartMessage()
        {
            Console.WriteLine();
            Console.Write("Do you want to play another game (y / n): ");
            string answer = Console.ReadLine();
            bool output = ValidateAnswer(answer);

            return output; 
        }

        private static bool ValidateAnswer(string answer)
        {
            string pattern = @"(^[y]$|^[n]$)";
            Regex reg = new Regex(pattern);

            bool isValid = false;
            bool output = false;

            do
            {
                if (reg.IsMatch(answer.ToLower()) == false)
                {
                    Console.Write("Please write \"y\" or \"n\": ");
                    answer = Console.ReadLine();
                }
                else if (answer.ToLower() == "y")
                {
                    isValid = true;
                    output = true;

                }
                else if (answer.ToLower() == "n")
                {
                    isValid = true;
                    output = false;
                }

            } while (isValid == false);

            return output;
        }

        public static string AskPlayerShipSpot(PlayerInfoModel player)
        {
            Console.Write($"{player.PlayerName}, place your {player.PlayerShipSpot.Count + 1} ship (ej. A3): ");
            string position = Console.ReadLine();
            string output = ValidatePostion(player, position);

            return output;
        }

        public static string ValidateShot(PlayerInfoModel player, string shotElection)
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

        public static string ValidatePostion(PlayerInfoModel player, string position)
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
                else if (reg.IsMatch(output) == true && checkSpot == true)
                {
                    isFreeSpot = true;
                }

            } while (reg.IsMatch(output) == false || isFreeSpot == false);

            return output;
        }
    }
}
