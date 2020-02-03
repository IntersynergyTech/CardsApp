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
    }
}
