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

        private PokerHandRanking DefaultRanker(IDefaultPokerHand pokerHand)
        {
            return PokerHandRanking.HighCard;
        }
    }
}