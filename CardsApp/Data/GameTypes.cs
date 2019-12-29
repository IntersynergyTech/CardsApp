using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardsApp.Misc;

namespace CardsApp.Data
{
    public enum GameTypes
    {
        /// <summary>
        /// Where the winner is the last person to hit a threshold (ie you go over, you're out)
        /// </summary>
        [AccrualHands]
        Accrual_LastTo = 1,
        /// <summary>
        /// Where the winner is the first person to hit a threshold (ie you go over, you win)
        /// </summary>
        [AccrualHands]
        Accrual_FirstTo = 2,
        /// <summary>
        /// If the game is won by the player with the highest score after all hands are played.
        /// </summary>
        [FixedHands]
        Accrual_HighestScore = 4,
        /// <summary>
        /// if the game is won by the player with the lowest score after all hands are played.
        /// </summary>
        [FixedHands]
        Accrual_LowestScore = 8,
        /// <summary>
        /// If the game is a simple game outside of the system. 1 "hand" will be played and the user who isn't knocked out wins.
        /// </summary>
        [SimpleNoHands]
        Simple = 16


    }
}
