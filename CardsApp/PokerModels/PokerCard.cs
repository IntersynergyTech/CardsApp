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

    public class ChinesePokerBoard
    {
        public List<ChinesePokerHand> Hands { get; private set; }

        public ChinesePokerBoard(List<ChinesePokerHand> hands)
        {
            
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
                if (top.Ranking > middle.Ranking) return false;
                if (middle.Ranking > bottom.Ranking) return false;
                if (top.Ranking > bottom.Ranking) return false;
                if (top.Ranking == middle.Ranking)
                {
                    var orderedTop = top.CardsInOrder.ToImmutableList();
                    var orderedMiddle = middle.CardsInOrder.ToImmutableList();
                    for (var i = 0; i < top.CardsInHand.Count; i++)
                    {
                        var topCard = orderedTop[i];
                        var middleCard = orderedTop[i];
                    }
                    
                }

                if (middle.Ranking == bottom.Ranking)
                {
                    
                }

                return true;
            }
        }
    }

}