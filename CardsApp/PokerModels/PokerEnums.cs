namespace CardsApp.PokerModels
{
    public enum CardSuit
    {
        Hearts,
        Clubs,
        Diamonds,
        Spades
    }

    public enum CardValue : int
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14
    }

    public enum ChineseHandPosition
    {
        Top,
        Middle,
        Bottom
    }

    public enum PokerHandRanking
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    public enum RankingType
    {
        Default,
        TwoSevenLowBall,
        Stud,
        Omaha
    }
}