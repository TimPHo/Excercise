using Excercise.CardGameDomain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Excercise.CardGameDomainTest
{
    public class TestHelper
    {
        /// <summary>
        ///     Verify order of two cards by checking their Ids and Suits
        /// </summary>
        /// <param name="card1">Card with lower order</param>
        /// <param name="card2">Card with higher order</param>
        /// <returns>
        ///     If card1 comes first in order then returns true; Otherwise returns false
        /// </returns>
        static public bool CompareCardsOrder(ICard card1, ICard card2)
        {
            if (card1.Id > card2.Id)
                return false;
            else if (card1.Id < card2.Id)
                return true;
            else if (card1.Suit > card2.Suit)
                return false;
            else
                return true;
        }

        /// <summary>
        ///     Shallow compare of two decks, by comparing the objects
        ///     Count of how many cards that are not in the same order.  
        /// </summary>
        /// <param name="deck1">Cards of deck 1</param>
        /// <param name="deck2">Cards of deck 2</param>
        /// <returns>Number of cards that not in same order</returns>
        static public int ShallowCompareDecksOrder(List<ICard> deck1, List<ICard> deck2)
        {
            int OutOfOrderCount = 0;

            for (var i = 0; i < (deck1.Count - 1) && i < (deck2.Count - 1); i++)
                if (deck1[i] != deck2[i])
                    OutOfOrderCount++;

            return OutOfOrderCount;
        }

        /// <summary>
        ///     Deep compare of two decks, by comparing cards using their value and suit
        ///     Count of how many cards that are not in the same order.  
        /// </summary>
        /// <param name="deck1">Cards of deck 1</param>
        /// <param name="deck2">Cards of deck 2</param>
        /// <returns>Number of cards that not in same order</returns>
        static public int DeepCompareDecksOrder(List<ICard> deck1, List<ICard> deck2)
        {
            int OutOfOrderCount = 0;

            for (var i = 0; i < (deck1.Count - 1) && i < (deck2.Count - 1); i++)
                if (deck1[i].Id != deck2[i].Id || deck1[i].Suit != deck2[i].Suit)
                    OutOfOrderCount++;

            return OutOfOrderCount;
        }
    }
}
