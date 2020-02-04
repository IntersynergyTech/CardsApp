using System.Collections.Generic;

namespace CardsApp.PokerModels
{
    public class ChinesePokerHand : Hand
    {
        public ChineseHandPosition Position;

        public ChinesePokerHand(ChineseHandPosition position, List<Card> cards)
        {
            Position = position;
            Cards = cards;
        }
    }
}