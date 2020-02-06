using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsApp.PokerModels
{
    public enum Rank
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public class Hand
    {
        public List<Card> Cards { get; set; }

        public Hand()
        {
            Cards = new List<Card>();
        }

        public override string ToString()
        {
            return string.Join(" ", Cards.Select(card => card.ToString()));
        }

        public HandStrength GetStrength()
        {
            if (Cards.Count == 5)
            {
                var strength = new HandStrength();
                strength.Kickers = new List<int>();

                Cards = Cards.OrderBy(card => card.PrimeRank * 100 + card.PrimeSuit).ToList();

                int rankProduct = Cards.Select(card => card.PrimeRank).Aggregate((acc, r) => acc * r);
                int suitProduct = Cards.Select(card => card.PrimeSuit).Aggregate((acc, r) => acc * r);

                bool straight =
                    rankProduct == 8610 // 5-high straight
                    || rankProduct == 2310 // 6-high straight
                    || rankProduct == 15015 // 7-high straight
                    || rankProduct == 85085 // 8-high straight
                    || rankProduct == 323323 // 9-high straight
                    || rankProduct == 1062347 // T-high straight
                    || rankProduct == 2800733 // J-high straight
                    || rankProduct == 6678671 // Q-high straight
                    || rankProduct == 14535931 // K-high straight
                    || rankProduct == 31367009; // A-high straight

                bool flush =
                    suitProduct == 147008443 // Spades
                    || suitProduct == 229345007 // Hearts
                    || suitProduct == 418195493 // Diamonds
                    || suitProduct == 714924299; // Clubs

                var cardCounts = Cards.GroupBy(card => (int) card.Rank).Select(group => group).ToList();

                var fourOfAKind = -1;
                var threeOfAKind = -1;
                var onePair = -1;
                var twoPair = -1;

                foreach (var group in cardCounts)
                {
                    var rank = group.Key;
                    var count = group.Count();
                    if (count == 4) fourOfAKind = rank;
                    else if (count == 3) threeOfAKind = rank;
                    else if (count == 2)
                    {
                        twoPair = onePair;
                        onePair = rank;
                    }
                }

                if (straight && flush)
                {
                    strength.HandRanking = PokerHandRanking.StraightFlush;
                    strength.Kickers = Cards.Select(card => (int) card.Rank).Reverse().ToList();
                }
                else if (fourOfAKind >= 0)
                {
                    strength.HandRanking = PokerHandRanking.FourOfAKind;
                    strength.Kickers.Add(fourOfAKind);
                    strength.Kickers.AddRange(Cards
                        .Where(card => (int) card.Rank != fourOfAKind)
                        .Select(card => (int) card.Rank));
                }
                else if (threeOfAKind >= 0 && onePair >= 0)
                {
                    strength.HandRanking = PokerHandRanking.FullHouse;
                    strength.Kickers.Add(threeOfAKind);
                    strength.Kickers.Add(onePair);
                }
                else if (flush)
                {
                    strength.HandRanking = PokerHandRanking.Flush;
                    strength.Kickers.AddRange(Cards
                        .Select(card => (int) card.Rank)
                        .Reverse());
                }
                else if (straight)
                {
                    strength.HandRanking = PokerHandRanking.Straight;
                    strength.Kickers.AddRange(Cards
                        .Select(card => (int) card.Rank)
                        .Reverse());
                }
                else if (threeOfAKind >= 0)
                {
                    strength.HandRanking = PokerHandRanking.ThreeOfAKind;
                    strength.Kickers.Add(threeOfAKind);
                    strength.Kickers.AddRange(Cards
                        .Where(card => (int) card.Rank != threeOfAKind)
                        .Select(card => (int) card.Rank));
                }
                else if (twoPair >= 0)
                {
                    strength.HandRanking = PokerHandRanking.TwoPair;
                    strength.Kickers.Add(Math.Max(twoPair, onePair));
                    strength.Kickers.Add(Math.Min(twoPair, onePair));
                    strength.Kickers.AddRange(Cards
                        .Where(card => (int) card.Rank != twoPair && (int) card.Rank != onePair)
                        .Select(card => (int) card.Rank));
                }
                else if (onePair >= 0)
                {
                    strength.HandRanking = PokerHandRanking.Pair;
                    strength.Kickers.Add(onePair);
                    strength.Kickers.AddRange(Cards
                        .Where(card => (int) card.Rank != onePair)
                        .Select(card => (int) card.Rank));
                }
                else
                {
                    strength.HandRanking = PokerHandRanking.HighCard;
                    strength.Kickers.AddRange(Cards
                        .Select(card => (int) card.Rank)
                        .Reverse());
                }

                return strength;
            }
            else if (Cards.Count == 3)
            {
                var strength = new HandStrength();
                strength.Kickers = new List<int>();
                Cards = Cards.OrderBy(card => card.PrimeRank * 100 + card.PrimeSuit).ToList();
                
                var cardCounts = Cards.GroupBy(card => (int) card.Rank).Select(group => group).ToList();
                
                var threeOfAKind = -1;
                var onePair = -1;

                foreach (var group in cardCounts)
                {
                    var rank = group.Key;
                    var count = group.Count();
                    if (count == 3) threeOfAKind = rank;
                    else if (count == 2)
                    {
                        onePair = rank;
                    }
                }
                if (threeOfAKind >= 0)
                {
                    strength.HandRanking = PokerHandRanking.ThreeOfAKind;
                    strength.Kickers.Add(threeOfAKind);
                    strength.Kickers.AddRange(Cards
                        .Where(card => (int) card.Rank != threeOfAKind)
                        .Select(card => (int) card.Rank));
                }
                else if (onePair >= 0)
                {
                    strength.HandRanking = PokerHandRanking.Pair;
                    strength.Kickers.Add(onePair);
                    strength.Kickers.AddRange(Cards
                        .Where(card => (int) card.Rank != onePair)
                        .Select(card => (int) card.Rank));
                }
                else
                {
                    strength.HandRanking = PokerHandRanking.HighCard;
                    strength.Kickers.AddRange(Cards
                        .Select(card => (int) card.Rank)
                        .Reverse());
                }

                return strength;
            }

            else
            {
                return null;
            }
        }

        /// <summary>
        /// Compares this hand to another hand
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Winning true if this hand, false if other, null if matching </returns>
        public bool? Compare(Hand other)
        {
            var otherStr = other.GetStrength();
            var thisStr = this.GetStrength();
            if (thisStr.HandRanking > otherStr.HandRanking) return true;
            if (thisStr.HandRanking < otherStr.HandRanking) return false;

            for (var i = 0; i < thisStr.Kickers.Count; i++)
            {
                if (thisStr.Kickers[i] > otherStr.Kickers[i]) return true;
                if (thisStr.Kickers[i] < otherStr.Kickers[i]) return false;
            }

            return null;
        }
    }

    public class Card
    {
        public Rank Rank { get; set; }
        public CardSuit Suit { get; set; }

        private static int[] rankPrimes = new int[] {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41};
        private static int[] suitPrimes = new int[] {43, 47, 53, 59};

        public int PrimeRank => rankPrimes[(int) Rank];

        public int PrimeSuit => suitPrimes[(int) Suit];
        
        /// <summary>
        /// Creates the card from the string
        /// </summary>
        /// <param name="s"></param>
        /// <exception cref="ArgumentException"></exception>
        public Card(string s)
        {
            var chars = s.ToUpper().ToCharArray();
            if (chars.Length != 2) throw new ArgumentException("Card string must be length 2");
            switch (chars[0])
            {
                case '2':
                    this.Rank = Rank.Two;
                    break;
                case '3':
                    this.Rank = Rank.Three;
                    break;
                case '4':
                    this.Rank = Rank.Four;
                    break;
                case '5':
                    this.Rank = Rank.Five;
                    break;
                case '6':
                    this.Rank = Rank.Six;
                    break;
                case '7':
                    this.Rank = Rank.Seven;
                    break;
                case '8':
                    this.Rank = Rank.Eight;
                    break;
                case '9':
                    this.Rank = Rank.Nine;
                    break;
                case 'T':
                    this.Rank = Rank.Ten;
                    break;
                case 'J':
                    this.Rank = Rank.Jack;
                    break;
                case 'Q':
                    this.Rank = Rank.Queen;
                    break;
                case 'K':
                    this.Rank = Rank.King;
                    break;
                case 'A':
                    this.Rank = Rank.Ace;
                    break;
                default: throw new ArgumentException("Card string rank not valid");
            }

            switch (chars[1])
            {
                case 'S':
                    this.Suit = CardSuit.Spades;
                    break;
                case 'H':
                    this.Suit = CardSuit.Hearts;
                    break;
                case 'D':
                    this.Suit = CardSuit.Diamonds;
                    break;
                case 'C':
                    this.Suit = CardSuit.Clubs;
                    break;
                default: throw new ArgumentException("Card string suit not valid");
            }
        }

        public override string ToString()
        {
            char[] ranks = "23456789TJQKA".ToCharArray();
            char[] suits = {'h', 'c', 'd', 's'};

            return ranks[(int) Rank].ToString() + suits[(int) Suit].ToString();
        }
    }

    public class HandStrength
    {
        public PokerHandRanking HandRanking { get; set; }
        public List<int> Kickers { get; set; }
    }
}