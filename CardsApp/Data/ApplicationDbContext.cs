using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CardsApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }

        /// <summary>
        /// The board™
        ///
        /// The entries in here can be grouped with GamePlayed to get a single row, and need totalling up to see totals over time, as it's just the value difference that game made.
        /// </summary>
        public DbSet<BoardEntry> Board { get; set; }

        /// <summary>
        /// A list of games to play.
        /// </summary>
        public DbSet<Game> Games { get; set; }

        /// <summary>
        /// The history of games played. Also handy for goruping hands, and grouping together board entries
        /// </summary>
        public DbSet<GamePlayed> GamesPlayed { get; set; }

        /// <summary>
        /// Relationships between games played and who played them.
        /// </summary>
        public DbSet<GamePlayers> GamesPlayedByPlayers { get; set; }

        /// <summary>
        /// Hands which were played in games.
        /// </summary>
        public DbSet<Hand> Hands { get; set; }

        /// <summary>
        /// Players who exist in the system.
        /// </summary>
        public DbSet<Player> Players { get; set; }

        /// <summary>
        /// The details of a player's performance in a single game.
        /// </summary>
        public DbSet<PlayerHand> PlayerHands { get; set; }
    }
}
