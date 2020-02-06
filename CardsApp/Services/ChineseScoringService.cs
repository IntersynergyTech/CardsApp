using System;
using System.Collections.Generic;
using System.Linq;
using CardsApp.PokerModels;

namespace CardsApp.Services
{
    public class ChineseScoringService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Positive for player1, negative for player2</returns>
        public int Points(ChinesePokerBoard player1, ChinesePokerBoard player2)
        {
            if (!player1.IsValid || !player2.IsValid) throw new InvalidBoardException();
            if (!player1.IsQualifying && !player2.IsQualifying) return 0;
            var totalPoints = 0;
            if (player1.IsQualifying && !player2.IsQualifying) totalPoints += 6;
            else if (!player1.IsQualifying && player2.IsQualifying) totalPoints += 6;
            else
            {
                var player1Rounds = 0;
                var player2Rounds = 0;
                foreach (var hand in player1.Hands)
                {
                    var player2Hand = player2.Hands.First(x => x.Position == hand.Position);
                    var compareResult = hand.Compare(player2Hand); //True if hand1 false if hand 2
                    if(compareResult == null) continue; //Null means hands are exactly equal
                    if (compareResult.Value) player1Rounds++;
                    else player2Rounds++;
                }

                if (player1Rounds == 3) player1Rounds += 3; //Add player 1 bonus for winning all hands
                else if (player2Rounds == 3) player2Rounds += 3; //Add player 2 bonus

                totalPoints += player1Rounds;
                totalPoints -= player2Rounds;
            }

            if (player1.IsQualifying)
            {
                totalPoints += player1.Hands.Sum(CalculateBonus);
            }

            if (player2.IsQualifying)
            {
                totalPoints -= player1.Hands.Sum(CalculateBonus);
            }
            return totalPoints;
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
    public class InvalidBoardException : Exception
    {
    }

}