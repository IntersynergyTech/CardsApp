using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardsApp.Data;

namespace CardsApp.Models
{
    public class StartGameModel
    {
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Player> Players { get; set; }

        public Guid SelectedGame { get; set; }

        public decimal? SelectedGameStake { get; set; }

        public IEnumerable<Guid> SelectedPlayers { get; set; }
    }
}
