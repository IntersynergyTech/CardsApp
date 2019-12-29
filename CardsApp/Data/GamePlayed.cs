using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardsApp.Data
{
    public class GamePlayed
    {

        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// When the game was played
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The game that was played
        /// </summary>
        public Game Game { get; set; }
        
        /// <summary>
        /// The winner of the game (if there was one)
        /// </summary>
        public Player Winner { get; set; }

        /// <summary>
        /// If the game is finished
        /// </summary>
        public bool GameComplete { get; set; }

        /// <summary>
        /// If the game ended in a draw.
        /// </summary>
        public bool IsDraw { get; set; }

        /// <summary>
        /// If this is a draw game, what game drew
        /// </summary>
        public GamePlayed DrawParent { get; set; }

        /// <summary>
        /// Hands which took place in this game
        /// </summary>
        public virtual IEnumerable<Hand> Hands { get; set; }

        /// <summary>
        /// The amount staked on this game
        /// </summary>
        public decimal Stake { get; set; }

        /// <summary>
        /// Players who played the game.
        /// </summary>
        public virtual IEnumerable<Player> Players { get; set; }
    }
}
