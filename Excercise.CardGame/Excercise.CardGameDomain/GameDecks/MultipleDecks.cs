using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Excercise.CardGameDomain.GameDecks
{
    public class MultipleDecks: Deck
    {
        public MultipleDecks(int numberOfDeck)
            : base(null)
        {
            Cards = new List<ICard>();
            for (var i = 0; i < numberOfDeck; i++)
            {
                Cards.AddRange(Deck.StandardDeckCards());
            }
        }

        public virtual int DecksCount
        {
            get
            {
                return Cards == null ? 0 : Cards.Count / 52;
            }
        }
    }
}
