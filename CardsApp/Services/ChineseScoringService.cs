using System;
using System.Collections.Generic;
using System.Linq;
using CardsApp.PokerModels;

namespace CardsApp.Services
{
    public class ChineseScoringService
    {
        public int Points()
        {
            return 0;
        }

        public int CalculateBonus(ChinesePokerHand pokerHand)
        {
            var ranking = pokerHand.GetStrength();
            switch (pokerHand.Position)
            {
                case ChineseHandPosition.Top:
                    switch (ranking.HandRanking)
                    {
                        case PokerHandRanking.HighCard:
                            return 0;
                        case PokerHandRanking.Pair:
                        case PokerHandRanking.ThreeOfAKind:
                            var pairScoring = new Dictionary<Rank, int> //Hardcoded Bonuses for pairs
                            {
                                {Rank.Six, 1},
                                {Rank.Seven, 2},
                                {Rank.Eight, 3},
                                {Rank.Nine, 4},
                                {Rank.Ten, 5},
                                {Rank.Jack, 6},
                                {Rank.Queen, 7},
                                {Rank.King, 8},
                                {Rank.Ace, 9},
                                
                            }; 
                            var tripsScoring = new Dictionary<Rank, int> //Hardcoded Bonuses for Trips
                            {
                                {Rank.Two, 10},
                                {Rank.Three, 11},
                                {Rank.Four, 12},
                                {Rank.Five, 13},
                                {Rank.Six, 14},
                                {Rank.Seven, 15},
                                {Rank.Eight, 16},
                                {Rank.Nine, 17},
                                {Rank.Ten, 18},
                                {Rank.Jack, 19},
                                {Rank.Queen, 20},
                                {Rank.King, 21},
                                {Rank.Ace, 22},
                                
                            }; 
                            foreach (var grouping in pokerHand.Cards.GroupBy(x => x.Rank).Where(x => x.Count() != 1)) //Filters out the single cards
                            {
                                return grouping.Count() switch
                                {
                                    2 when grouping.Key >= Rank.Five => 0, //Less than 66 so no bonus points
                                    2 => pairScoring[grouping.Key], //One Pair
                                    3 => tripsScoring[grouping.Key], //Trips
                                    _ => 0
                                };
                            }
                            break;
                        case PokerHandRanking.TwoPair:
                        case PokerHandRanking.Straight:
                        case PokerHandRanking.Flush:
                        case PokerHandRanking.FullHouse:
                        case PokerHandRanking.FourOfAKind:
                        case PokerHandRanking.StraightFlush:
                        case PokerHandRanking.RoyalFlush:
                        default:
                            return 0;
                    }

                    return 0;
                case ChineseHandPosition.Middle:
                    return ranking.HandRanking switch
                    {
                        PokerHandRanking.HighCard => 0,
                        PokerHandRanking.Pair => 0,
                        PokerHandRanking.TwoPair => 0,
                        PokerHandRanking.ThreeOfAKind => 2,
                        PokerHandRanking.Straight => 4,
                        PokerHandRanking.Flush => 8,
                        PokerHandRanking.FullHouse => 12,
                        PokerHandRanking.FourOfAKind => 20,
                        PokerHandRanking.StraightFlush => 30,
                        PokerHandRanking.RoyalFlush => 50,
                        _ => 0
                    };
                case ChineseHandPosition.Bottom:
                    return ranking.HandRanking switch
                    {
                        PokerHandRanking.HighCard => 0,
                        PokerHandRanking.Pair => 0,
                        PokerHandRanking.TwoPair => 0,
                        PokerHandRanking.ThreeOfAKind => 0,
                        PokerHandRanking.Straight => 2,
                        PokerHandRanking.Flush => 4,
                        PokerHandRanking.FullHouse => 6,
                        PokerHandRanking.FourOfAKind => 10,
                        PokerHandRanking.StraightFlush => 15,
                        PokerHandRanking.RoyalFlush => 25,
                        _ => 0
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}