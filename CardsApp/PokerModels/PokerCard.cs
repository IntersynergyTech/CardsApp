using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace CardsApp.PokerModels
{
    /// <summary>
    /// A class to represent the the state of a players 13 card board at the end of the hand
    /// </summary>
    public class ChinesePokerBoard
    {
        public List<ChinesePokerHand> Hands { get; private set; }

        public ChinesePokerBoard(List<ChinesePokerHand> hands)
        {
            Hands = hands;
        }

        /// <summary>
        /// Checks to see if the layout of the board is valid. Returns false if the board fails to qualify
        /// </summary>
        public bool IsValid
        {
            get
            {
                var top = Hands.FirstOrDefault(x => x.Position == ChineseHandPosition.Top);
                var middle = Hands.FirstOrDefault(x => x.Position == ChineseHandPosition.Middle);
                var bottom = Hands.FirstOrDefault(x => x.Position == ChineseHandPosition.Bottom);
                if (top == null || middle == null || bottom == null)
                {
                    return false;
                }

                return true;

            }
        }

        public bool IsQualifying
        {
            get
            {
                var top = Hands.FirstOrDefault(x => x.Position == ChineseHandPosition.Top);
                var middle = Hands.FirstOrDefault(x => x.Position == ChineseHandPosition.Middle);
                var bottom = Hands.FirstOrDefault(x => x.Position == ChineseHandPosition.Bottom);
                if (top == null || middle == null || bottom == null)
                {
                    return false;
                }
                
                var topVsMiddle = top.Compare(middle);
                var middleVsBottom = middle.Compare(bottom);
                var topVsBottom = top.Compare(bottom);

                if (top.Cards.Count != 3) return false;
                if (middle.Cards.Count != 5) return false;
                if (bottom.Cards.Count != 5) return false;
                if (topVsMiddle != null && !topVsMiddle.Value)
                {
                    return false;
                }

                if (middleVsBottom != null && !middleVsBottom.Value)
                {
                    return false;
                }
                return topVsBottom == null || topVsBottom.Value;
            }
        }

        public bool IsFantasyLand
        {
            get
            {
                if (!IsValid) return false;
                var top = Hands.FirstOrDefault(x => x.Position == ChineseHandPosition.Top);
                if (top == null) return false;
                var topStr = top.GetStrength();
                if (topStr.HandRanking == PokerHandRanking.HighCard) return false;
                switch (topStr.HandRanking)
                {
                    case PokerHandRanking.Pair:
                        return top.Cards.Count(x => x.Rank == Rank.Queen) == 2 ||
                               top.Cards.Count(x => x.Rank == Rank.King) == 2 ||
                               top.Cards.Count(x => x.Rank == Rank.Ace) == 2;
                    case PokerHandRanking.ThreeOfAKind:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Checks if a player in fantasy can continue in fantasy
        /// </summary>
        public bool CanContinueInFantasy
        {
            get
            {
                if (!IsValid) return false;
                var top = Hands.First(x => x.Position == ChineseHandPosition.Top);
                var middle = Hands.First(x => x.Position == ChineseHandPosition.Middle);
                var bottom = Hands.First(x => x.Position == ChineseHandPosition.Bottom);
                
                if (top.GetStrength().HandRanking == PokerHandRanking.ThreeOfAKind) return true;
                if (middle.GetStrength().HandRanking >= PokerHandRanking.FullHouse) return true;
                return bottom.GetStrength().HandRanking >= PokerHandRanking.FourOfAKind;
            }
        }
    }
}