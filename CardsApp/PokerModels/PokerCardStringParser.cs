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
        public static List<Card> ParseCards(string parseable)
        {
            var returnable = new List<Card>();
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

        public static Card ParseCard(string parseable)
        {
            parseable = parseable.ToLower();
            var value = parseable.Substring(0, 1);
            var suit = parseable.Substring(1, 1);

            var card = new Card(parseable);

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
