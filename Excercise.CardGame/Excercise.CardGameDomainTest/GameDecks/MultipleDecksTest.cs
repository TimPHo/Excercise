using System;
using NUnit.Framework;
using Moq;
using Excercise.CardGameDomain.GameDecks;
using Excercise.CardGameDomain;

namespace Excercise.CardGameDomainTest.GameDecks
{
    [TestFixture]
    public class MultipleDecksTest
    {

        [Test]
        public void ShouldHaveCorrectNumberOfCards()
        {
            var objectUnderTest = new MultipleDecks(5);

            Assert.That(objectUnderTest.Cards.Count, Is.EqualTo(5*52), "Multiple decks should have correct number of cards");
        }

        [Test]
        public void ShouldHaveCorrectNumberOfDecks()
        {
            var objectUnderTest = new MultipleDecks(5);

            Assert.That(objectUnderTest.DecksCount, Is.EqualTo(5), "Multiple should have correct number of decks count");
        }

 
    }
}
