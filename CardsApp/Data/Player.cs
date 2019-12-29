using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardsApp.Data
{
    public class Player
    {
        /// <summary>
        /// Player ID
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the player to show up.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The initial to use for showing bigger.
        /// </summary>
        public string Initial { get; set; }

        /// <summary>
        /// The last time the user played
        /// </summary>
        public DateTime LastPlayed { get; set; } = DateTime.MinValue;

        /// <summary>
        /// The last time the user settled their balance.
        /// </summary>
        public DateTime LastPaid { get; set; }

        /// <summary>
        /// The user's current balance. Probably.
        /// </summary>
        public Decimal CurrentBalance { get; set; }


        /// <summary>
        /// Games the player has played.
        /// </summary>
        public virtual IEnumerable<GamePlayers> Games { get; set; }
    }
}
