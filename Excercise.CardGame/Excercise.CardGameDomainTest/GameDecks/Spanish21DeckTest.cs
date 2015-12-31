using System;
using NUnit.Framework;
using Moq;
using Excercise.CardGameDomain.GameDecks;
using Excercise.CardGameDomain;

namespace Excercise.CardGameDomainTest.GameDecks
{
    [TestFixture]
    public class Spanish21DeckTest
    {

        [Test]
        public void ShouldHave48Cards()
        {
            var objectUnderTest = new Spanish21Deck();

            Assert.That(objectUnderTest.Cards.Count, Is.EqualTo(48), "Spanish21 should have 48 cards");
        }

        [Test]
        public void ShouldNotHaveAnyTen()
        {
            var objectUnderTest = new Spanish21Deck();

            Assert.That(objectUnderTest.Cards.FindAll(c=> c.Id==10).Count, Is.EqualTo(0) );
        }

    }
}
