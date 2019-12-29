using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardsApp.Data
{
    public class PlayerHand
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The player who played this hand.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// The hand the game was played on.
        /// </summary>
        public Hand Hand { get; set; }

        /// <summary>
        /// if the player was knocked out in this hand.
        /// </summary>
        public bool KnockedOut { get; set; }

        /// <summary>
        /// The amount of points the player got this hand.
        /// </summary>
        public Int32 Score { get; set; }
    }
}
