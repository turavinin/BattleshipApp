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
            string letterOutput = shipSpot.Substring(0, 1);
            int numberOutput = int.Parse(shipSpot.Substring(1));

            return (letterOutput, numberOutput);
        }

        private static void AddSpot(PlayerInfoModel model, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };
            model.PlayerShotGrid.Add(spot);
        }

    }
}
