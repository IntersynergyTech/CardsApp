using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace CardsApp.PokerModels
{
    public class PokerCard
    {
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }

        public override string ToString()
        {
            var valueStr = ((int) Value) <= 11 ? ((int) Value).ToString() : Value.ToString().ToUpper()[0].ToString();
            return $"{valueStr}{Suit.ToString().ToLower()[0]}";
        }

        public PokerCard(CardSuit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }

        public PokerCard()
        {
            
        }
    }


    public interface IDefaultPokerHand
    {
        public List<PokerCard> CardsInHand { get; set; }
        public PokerHandRanking Ranking { get; set; }
        public RankingType RankingType { get; }
    }

    public class ChinesePokerHand : IDefaultPokerHand
    {
        public IOrderedEnumerable<PokerCard> CardsInOrder
        {
            get { return CardsInHand.OrderBy(x => x.Value); }
        }

        public ChineseHandPosition Position;

        public ChinesePokerHand(ChineseHandPosition position, List<PokerCard> cards, PokerHandRanking ranking)
        {
            Position = position;
            Ranking = ranking;
        }

        public List<PokerCard> CardsInHand { get; set; }
        public PokerHandRanking Ranking { get; set; }

        public RankingType RankingType => RankingType.Default;
    }

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

                if (top.CardsInHand.Count != 3) return false;
                if (middle.CardsInHand.Count != 5) return false;
                if (bottom.CardsInHand.Count != 5) return false;
                if (top.Ranking > middle.Ranking) return false; //Top beats Middle 
                if (middle.Ranking > bottom.Ranking) return false; //Middle beats Bottom
                if (top.Ranking > bottom.Ranking) return false; //Top beats Bottom

                if (top.Ranking == middle.Ranking)
                {
                    var orderedTop = top.CardsInOrder.ToImmutableList();
                    var orderedMiddle = middle.CardsInOrder.ToImmutableList();
                    for (var i = 0; i < top.CardsInHand.Count; i++)
                    {
                        var topCard = orderedTop[i];
                        var middleCard = orderedMiddle[i];
                        if (topCard.Value > middleCard.Value) return false;
                        if (topCard.Value < middleCard.Value) return true;
                    }
                }

                if (middle.Ranking != bottom.Ranking) return true;
                {
                    var orderedMiddle = middle.CardsInOrder.ToImmutableList();
                    var orderedBottom = bottom.CardsInOrder.ToImmutableList();
                    for (var i = 0; i < top.CardsInHand.Count; i++)
                    {
                        var middleCard = orderedMiddle[i];
                        var bottomCard = orderedBottom[i];
                        if (middleCard.Value > bottomCard.Value) return false;
                        if (middleCard.Value < bottomCard.Value) return true;
                    }
                }

                return true;
            }
        }

        public bool IsFantasyLand
        {
            get
            {
                if (!IsValid) return false;
                var top = Hands.FirstOrDefault(x => x.Position == ChineseHandPosition.Top);
                if (top == null) return false;
                if (top.Ranking == PokerHandRanking.HighCard) return false;
                switch (top.Ranking)
                {
                    case PokerHandRanking.Pair:
                        return top.CardsInHand.Count(x => x.Value == CardValue.Queen) == 2 ||
                               top.CardsInHand.Count(x => x.Value == CardValue.King) == 2 ||
                               top.CardsInHand.Count(x => x.Value == CardValue.Ace) == 2;
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
                if (top.Ranking == PokerHandRanking.ThreeOfAKind) return true;
                if (middle.Ranking >= PokerHandRanking.FullHouse) return true;
                return bottom.Ranking >= PokerHandRanking.FourOfAKind;
            }
        }

    }
}