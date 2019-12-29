using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardsApp.Data
{
    public class BoardEntry
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The game the board entry applies to.
        /// </summary>
        public GamePlayed Game { get; set; }

        /// <summary>
        /// The player who won/lost the money
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// The amount the user has gone up or down on this game.
        /// </summary>
        public decimal Difference { get; set; }



    }
}
