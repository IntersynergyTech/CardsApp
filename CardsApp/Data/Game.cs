using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardsApp.Data
{
    public class Game
    {

        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the game
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A quick summary fo the game
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The stake that players will usually play into this game 
        /// </summary>
        public decimal DefaultStake { get; set; }

        /// <summary>
        /// if it's a game with only 1 winner
        /// </summary>
        public bool SingleWinner { get; set; }

        /// <summary>
        /// On games with accruing score, the maximum score where anything above this will trigger whatever action for the game. For example, going over may knock a player out.
        /// </summary>
        public Int32 MaximumScore { get; set; }

        /// <summary>
        /// The draw position, where if all remaining players are over this but still in, the game is drawn.
        /// </summary>
        public Int32 DrawPosition { get; set; }

        /// <summary>
        /// If it's a game with a fixed amount of hands, how many hands.
        /// </summary>
        public Int32 HandsPlayed { get; set; }

        /// <summary>
        /// If it's a game with a fixed amount of hands, if "HandsPlayed" is a fixed amount, or that many per player.
        /// </summary>
        public bool HandsPlayedPerPlayer { get; set; }

        /// <summary>
        /// Instances of this game being played.
        /// </summary>
        public virtual IEnumerable<GamePlayed> Games { get; set; }
    }
}
