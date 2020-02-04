using System;
using System.Collections.Generic;
using CardsApp.PokerModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardsAppTestProject
{
    [TestClass]
    public class PokerUnitTests
    {
        [TestMethod]
        public void CanGetCorrectStringDef()
        {
            var eightHeartsEx = "8h";
            var kingHeartsEx = "Kh";
            var kingHearts = new PokerCard
            {
                Suit = CardSuit.Hearts,
                Value = CardValue.King
            };
            var eightHearts = new PokerCard
            {
                Suit = CardSuit.Hearts,
                Value = CardValue.Eight
            };
            Assert.AreEqual(eightHeartsEx, eightHearts.ToString());
            Assert.AreEqual(kingHeartsEx, kingHearts.ToString());
        }

        [TestMethod]
        public void CanParseCards()
        {
            var cardString = "8h";
            var result = PokerCardStringParser.ParseCard(cardString);
            Assert.AreEqual(CardSuit.Hearts, result.Suit);
            Assert.AreEqual(CardValue.Eight, result.Value);
        }
    }

    [TestClass]
    public class PokerRankingTests
    {

        [TestMethod]
        public void GetRankingForQuads()
        {
            var card1 = new Card("8h");
            var card2 = new Card("8d");
            var card3 = new Card("8c");
            var card4 = new Card("8s");
            var card5 = new Card("7s");
            
            var hand = new Hand
            {
                Cards = new List<Card>
                {
                    card1, card2, card3, card4, card5
                }
            };
            var handRanking = hand.GetStrength();
            Assert.AreEqual(PokerHandRanking.FourOfAKind, handRanking.HandRanking);
        }
        
    }

    public static class ExampleHands
    {
        /// <summary>
        /// Returns a 5 card paid of 8h8sAs2c3c
        /// </summary>
        /// <returns></returns>
        public static List<PokerCard> FiveCardPairEights()
        {
            return new List<PokerCard>
            {
                new PokerCard(CardSuit.Hearts, CardValue.Eight),
                new PokerCard(CardSuit.Spades,CardValue.Eight),
                new PokerCard(CardSuit.Spades, CardValue.Ace),
                new PokerCard(CardSuit.Clubs, CardValue.Two),
                new PokerCard(CardSuit.Clubs, CardValue.Three)
            };
        }

        public static List<PokerCard> ThreeCardPairEights()
        {
            return new List<PokerCard>
            {
                new PokerCard(CardSuit.Hearts, CardValue.Eight),
                new PokerCard(CardSuit.Spades,CardValue.Eight),
                new PokerCard(CardSuit.Spades, CardValue.Ace)
            };
        }
        
        public static List<PokerCard> FullHouse5CardAcesEights()
        {
            return new List<PokerCard>
            {
                new PokerCard(CardSuit.Hearts, CardValue.Eight),
                new PokerCard(CardSuit.Spades,CardValue.Eight),
                new PokerCard(CardSuit.Spades, CardValue.Ace),
                new PokerCard(CardSuit.Clubs, CardValue.Ace),
                new PokerCard(CardSuit.Diamonds, CardValue.Ace)
            };
        }
    }
   
}
