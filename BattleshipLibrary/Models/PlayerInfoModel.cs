using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary.Models
{
    public class PlayerInfoModel
    {
        public string PlayerName { get; set; }
        public List<GridSpotModel> PlayerShipSpot { get; set; } = new List<GridSpotModel>();
        public List<GridSpotModel> PlayerShotGrid { get; set; } = new List<GridSpotModel>();


    }
}
