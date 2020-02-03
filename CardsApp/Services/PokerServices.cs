using System;
using System.Collections.Generic;
using CardsApp.PokerModels;

namespace CardsApp.Services
{
    public class PokerHandRankingService
    {
        public PokerHandRanking GetRanking(IDefaultPokerHand cardsInHand)
        {
            switch (cardsInHand.RankingType)
            {
                case RankingType.Default:
                    break;
                case RankingType.TwoSevenLowBall:
                    break;
                case RankingType.Stud:
                    break;
                case RankingType.Omaha:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return PokerHandRanking.HighCard;
        }

        /// <summary>
        /// Ranks the hands according to the default ranking system. Includes support for 3 card hands
        /// </summary>
        /// <param name="pokerHand"></param>
        /// <returns></returns>
        private PokerHandRanking DefaultRanker(IDefaultPokerHand pokerHand)
        {
            switch (pokerHand.CardsInHand.Count)
            {
                case 3:
                    //Chinese
                    break;
                case 5:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(message:"Incorrect number of cards in the given hand", null);
            }
            return PokerHandRanking.HighCard;
        }
    }
}