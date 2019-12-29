using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardsApp.Data
{
    public class GamePlayers
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The player who played the game
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// The game the player played.
        /// </summary>
        public GamePlayed GamePlayed { get; set; }

    }
}
