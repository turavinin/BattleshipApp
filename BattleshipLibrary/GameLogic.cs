using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    public class GameLogic
    {
        public static void SetDefaultShotGrid(PlayerInfoModel model)
        {
            List<string> letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };

            List<int> numbers = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };

            foreach (var letter in letters)
            {
                foreach (var number in numbers)
                {
                    AddSpot(model, letter, number);
                }
            }
            
            
        }

        public static void AddShipToGrid(PlayerInfoModel player, string shipSpot)
        {
            (string letter, int number) = SplitPosition(player, shipSpot);

            GridSpotModel ship = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Ship
            };
            player.PlayerShipSpot.Add(ship);
        }

        private static (string letter, int number) SplitPosition(PlayerInfoModel player, string shipSpot)
        {
            string letterOutput = shipSpot.Substring(0, 1).ToUpper();
            int numberOutput = int.Parse(shipSpot.Substring(1));

            return (letterOutput, numberOutput);
        }

        private static void AddSpot(PlayerInfoModel model, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                RowLetter = letter,
                Status = GridSpotStatus.Empty
            };
            model.PlayerShotGrid.Add(spot);
        }

        public static void UpadateGrid(PlayerInfoModel player, PlayerInfoModel opponent, string shot)
        {
            (string letter, int number) = SplitPosition(player, shot);

            int shotIndex = SearchShotIndex(player, letter, number);
            int opponentIndex = SearchShipIndex(opponent, letter, number);

            bool isHit = CheckShotTarget(player, opponent, shot);

            if (isHit == true)
            {
                // Update Player
                player.PlayerShotGrid[shotIndex].Status = GridSpotStatus.Hit;

                // Update Opponent
                opponent.PlayerShipSpot[opponentIndex].Status = GridSpotStatus.Sunk;

            }
            else
            {
                player.PlayerShotGrid[shotIndex].Status = GridSpotStatus.Miss;
            }
        }

        public static int RemainingShips(PlayerInfoModel model)
        {
            int output = 0;

            foreach (var ship in model.PlayerShipSpot)
            {
                if (ship.Status == GridSpotStatus.Ship)
                {
                    output++;
                }
            }

            return output; 
        }

        public static bool ValidateSpot(PlayerInfoModel player, string position)
        {
            bool output = false;

            (string letter, int number) = SplitPosition(player, position);

            int index = SearchShipIndex(player, letter, number);

            // Check if index exist
            if (index < 0)
            {
                output = true;

                return output; 
            }

            return output; 
        }

        private static int SearchShipIndex(PlayerInfoModel player, string letter, int number)
        {
            int output = player.PlayerShipSpot.FindIndex(e => e.SpotLetter == letter && e.SpotNumber == number);

            return output; 
        }

        private static int SearchShotIndex(PlayerInfoModel player, string letter, int number)
        {
            int output = player.PlayerShotGrid.FindIndex(e => e.SpotLetter == letter && e.SpotNumber == number);

            return output;
        }

        public static bool CheckShotTarget(PlayerInfoModel player, PlayerInfoModel opponent, string shot)
        {
            bool output = false; 

            (string letter, int number) = SplitPosition(player, shot);

            int opponentIndex = SearchShipIndex(opponent, letter, number);

            if (opponentIndex > -1)
            {
                output = true;

                return output; 
            }

            return output; 
        }

        public static bool ValidateShotSpot(PlayerInfoModel player, string shotElection)
        {
            bool output = false;

            (string letter, int number) = SplitPosition(player, shotElection);

            int index = SearchShotIndex(player, letter, number);

            // Check if already shoted there
            if (player.PlayerShotGrid[index].Status == GridSpotStatus.Empty)
            {
                output = true;
                return output; 
            }

            return output; 
        }
    }
}
