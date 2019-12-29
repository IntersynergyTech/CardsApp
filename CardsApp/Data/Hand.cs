using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardsApp.Data
{
    public class Hand
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The game the hand was part of.
        /// </summary>
        public GamePlayed Game { get; set; }

        /// <summary>
        /// The time the hand was played.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Scores from playesr
        /// </summary>
        public virtual IEnumerable<PlayerHand> Scores { get; set; }

    }
}
