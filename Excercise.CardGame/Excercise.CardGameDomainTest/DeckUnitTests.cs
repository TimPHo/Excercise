using System;
using NUnit.Framework;
using Excercise.CardGameDomain;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Excercise.CardGameDomainTest
{
    [TestFixture]
    public class DeckUnitTests
    {
       
        [Test]
        public void ShouldHave52Cards()
        {
            var objectUnderTest = new Deck();
            
            Assert.That(objectUnderTest.Cards.Count, Is.EqualTo(52), "New standard deck should have 52 cards");
        }

        [Test]
        public void ShouldHaveCustomType()
        {
            var cards = new List<ICard>() { 
                new Card(11, CardSuit.Club), 
                new Card(5, CardSuit.Heart), 
                new Card(5, CardSuit.Club), 
                new Card(1, CardSuit.Spade),
                new Card(1, CardSuit.Diam),
                new Card(10, CardSuit.Spade)
            };
            var objectUnderTest = new Deck(cards);

            CollectionAssert.AreEqual(cards, objectUnderTest.Cards, "Should be able to create custom deck");
        }


        [Test]
        public void ShouldHaveOneOfEachCard()
        {
            var objectUnderTest = new Deck();

            //New standard deck should have exactly one card for each combination of number & suit
            for(var i=1; i <= 13; i++)
            {
                Assert.That(objectUnderTest.Cards.FindAll(c => c.Id == i && c.Suit == CardSuit.Spade).Count, Is.EqualTo(1));
                Assert.That(objectUnderTest.Cards.FindAll(c => c.Id == i && c.Suit == CardSuit.Heart).Count, Is.EqualTo(1));
                Assert.That(objectUnderTest.Cards.FindAll(c => c.Id == i && c.Suit == CardSuit.Club).Count, Is.EqualTo(1));
                Assert.That(objectUnderTest.Cards.FindAll(c => c.Id == i && c.Suit == CardSuit.Diam).Count, Is.EqualTo(1));
            }
        }

        [Test]
        public void ShouldBeSortedOnNewDeck()
        {
            var objectUnderTest = new Deck();

            for (var i = 0; i < objectUnderTest.Cards.Count-1; i++)
                Assert.That(TestHelper.CompareCardsOrder(objectUnderTest.Cards[i], objectUnderTest.Cards[i + 1]), "Cards of new deck should be sorted.");
        }

        [Test]
        public void ShouldSortBackToOriginalCardSet()
        {
            var objectUnderTest = new Deck();

            var originalCardsOrder = new List<ICard>(objectUnderTest.Cards);

            objectUnderTest.Shuffle();
            objectUnderTest.Sort();

            CollectionAssert.AreEqual(objectUnderTest.Cards, originalCardsOrder, "Cards should be sorted back to original set");
        }

        [Test]
        public void ShouldSortAnySetOfCards()
        {
            var cards = new List<ICard>() { 
                new Card(11, CardSuit.Club), 
                new Card(5, CardSuit.Heart), 
                new Card(5, CardSuit.Club), 
                new Card(1, CardSuit.Spade),
                new Card(1, CardSuit.Diam),
                new Card(10, CardSuit.Spade)
            };

            var objectUnderTest = new Deck(cards);
            objectUnderTest.Sort();

            CollectionAssert.AreEquivalent(objectUnderTest.Cards, cards, "Sort method should not change the cards, except of their orders");

            for (var i = 0; i < objectUnderTest.Cards.Count - 1; i++)
                Assert.That(TestHelper.CompareCardsOrder(objectUnderTest.Cards[i], objectUnderTest.Cards[i + 1]), "Sort method should sort cards");
        }

        [Test]
        public void ShouldShuffleAnySetOfCards()
        {
            //For a small set of samples, we want to lower the ratio
            var expectedRatio = 0.5;

            var originalCards = new List<ICard>() { 
                new Card(1, CardSuit.Heart), 
                new Card(1, CardSuit.Diam), 
                new Card(1, CardSuit.Club), 
                new Card(1, CardSuit.Spade), 
                new Card(2, CardSuit.Heart), 
                new Card(2, CardSuit.Diam), 
                new Card(2, CardSuit.Club), 
                new Card(2, CardSuit.Spade), 
                new Card(3, CardSuit.Heart), 
                new Card(3, CardSuit.Diam), 
                new Card(3, CardSuit.Club), 
                new Card(3, CardSuit.Spade)
            };

            var objectUnderTest = new Deck() { 
                Cards = new List<ICard>(originalCards) 
            };
            objectUnderTest.Shuffle();

            CollectionAssert.AreEquivalent(objectUnderTest.Cards, originalCards, "Shuffle method should not change the cards, except of their orders");

            var outOfPositionCount = TestHelper.ShallowCompareDecksOrder(originalCards, objectUnderTest.Cards);
            double resultShuffleRatio = (double)outOfPositionCount / (double)originalCards.Count;

            Assert.That(resultShuffleRatio, Is.GreaterThan(expectedRatio), "Shuffle method should mix some cards");
        }

        /// <summary>
        ///     Make sure the Shuffle method work well by checking the ratio of shuffled cards
        ///     For 52 cards, each card should have ~98% chance to have a new location
        /// </summary>
        [Test]
        public void ShouldShuffleCardsWell()
        {
            var numberOfTestTrials = 100;

            //Minium expected ratio of the shuffed cards after x number of test trials
            var expectedRatio = .9;

            var totalCount = 0;
            var outOfPositionCount = 0;

            for (var trial = 0; trial < numberOfTestTrials; trial++)
            { 
                var objectUnderTest = new Deck();
                var originalCards = new List<ICard>(objectUnderTest.Cards);      //Shallow copy
                objectUnderTest.Shuffle();

                totalCount += objectUnderTest.Cards.Count;
                outOfPositionCount += TestHelper.ShallowCompareDecksOrder(originalCards, objectUnderTest.Cards);
            }

            double resultShuffleRatio = totalCount > 0 ? ((double)outOfPositionCount / (double)totalCount) : 0;

            Assert.That(resultShuffleRatio, Is.GreaterThan(expectedRatio), "Shuffle method should move over 90% of cards");
        }

        [Test]
        public void ShouldShuffleDifferentlyOnNewDecks()
        {
            //Minium expected ratio for the same cards to apear at the same location
            var expectedRatio = .8;

            var deck1 = new Deck();
            var deck2 = new Deck();

            //Just an insanity check 
            int outOfPositionCount = TestHelper.DeepCompareDecksOrder(deck1.Cards, deck2.Cards);
            Assert.That(outOfPositionCount, Is.EqualTo(0), "Two new decks should start out with the same set");

            deck1.Shuffle();
            deck2.Shuffle();

            outOfPositionCount = TestHelper.ShallowCompareDecksOrder(deck1.Cards, deck2.Cards);
            double resultShuffleRatio = (double)outOfPositionCount / 52.0;

            Assert.That(resultShuffleRatio, Is.GreaterThan(expectedRatio), "Shuffle should re-arrange cards differently each time");
        }

        [Test]
        public void ShouldShuffleDifferentlyEachTime()
        {
            //Minium expected ratio for the same cards to apear at the same location
            var expectedRatio = .8;

            var objectUnderTest = new Deck();

            objectUnderTest.Shuffle();
            var firstShuffle = new List<ICard>(objectUnderTest.Cards);

            objectUnderTest.Sort();
            objectUnderTest.Shuffle();
            var secondShuffle = new List<ICard>(objectUnderTest.Cards);

            //Just some cross checks
            CollectionAssert.AreEquivalent(firstShuffle, secondShuffle, "Shuffle method should not change the cards themselves");
            CollectionAssert.AreNotEqual(firstShuffle, secondShuffle, "Shuffle method should change their orders");

            int outOfPositionCount = TestHelper.ShallowCompareDecksOrder(firstShuffle, secondShuffle);
            double resultShuffleRatio = (double)outOfPositionCount / 52.0;

            Assert.That(resultShuffleRatio, Is.GreaterThan(expectedRatio), "Shuffle should re-arrange card differently each time");
        }



    }
}
