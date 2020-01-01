using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardsApp.Data;

namespace CardsApp.Models
{
    public class ViewBoardModel
    {
        public Dictionary<GamePlayed, IEnumerable<BoardEntry>> Board { get; set; }

        public List<Player> Players { get; set; }

        public Dictionary<Player, decimal> PlayerSums { get; set; }
    }
}
