using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsApp.PokerModels
{
    public static class PokerCardStringParser
    {
        /// <summary>
        /// Parses multiple cards and returns a list of PokerCard objects.
        /// </summary>
        /// <param name="parseable"></param>
        /// <returns></returns>
        public static List<PokerCard> ParseCards(string parseable)
        {
            var returnable = new List<PokerCard>();
            if (parseable.Length % 2 == 0)
            {
                for (int pos = 0; pos < parseable.Length; pos+= 2)
                {
                    returnable.Add(ParseCard(parseable.Substring(pos,2)));
                }
            }
            else
            {
                throw new ArgumentNullException("The argument string must contain an even number of characters.");
            }

            return returnable;
        }

        public static PokerCard ParseCard(string parseable)
        {
            parseable = parseable.ToLower();
            var value = parseable.Substring(0, 1);
            var suit = parseable.Substring(1, 1);

            var card = new PokerCard();
            switch (suit)
            {
                case "s":
                    card.Suit = CardSuit.Spades;
                    break;
                case "d":
                    card.Suit = CardSuit.Diamonds;
                    break;
                case "c":
                    card.Suit = CardSuit.Clubs;
                    break;
                case "h":
                    card.Suit = CardSuit.Hearts;
                    break;
                default: 
                    throw new InvalidSuitException($"{suit} is not recognised as a valid suit. Valid values are S, D, C and H (case insensitive)");
            }

            switch (value)
            {
                case "a":
                case "1":
                    card.Value = CardValue.Ace;
                    break;
                case "2":
                    card.Value = CardValue.Two;
                    break;
                case "3":
                    card.Value = CardValue.Three;
                    break;
                case "4":
                    card.Value = CardValue.Four;
                    break;
                case "5":
                    card.Value = CardValue.Five;
                    break;
                case "6":
                    card.Value = CardValue.Six;
                    break;
                case "7":
                    card.Value = CardValue.Seven;
                    break;
                case "8":
                    card.Value = CardValue.Eight;
                    break;
                case "9":
                    card.Value = CardValue.Nine;
                    break;
                case "10":
                case "t":
                    card.Value = CardValue.Ten;
                    break;
                case "j":
                    card.Value = CardValue.Jack;
                    break;
                case "q":
                    card.Value = CardValue.Queen;
                    break;
                case "k":
                    card.Value = CardValue.King;
                    break;
                
                default:
                    throw new InvalidValueException($"{value} is not recognised as a valid suit. Valid values are 2-9, ATJQK (case insensitive). You can optionally use 0 for a 10 and 1 for an ace");
            }

            return card;
        }
    }

    public class InvalidValueException : Exception
    {
        public InvalidValueException(string msg) : base(msg)
        {

        }
    }
    public class InvalidSuitException : Exception
    {
        public InvalidSuitException(string msg) : base(msg)
        {

        }
    }

}
