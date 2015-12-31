using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Excercise.CardGameDomain.GameDecks
{
    public class Spanish21Deck: Deck
    {
        public Spanish21Deck()
            : base(null)
        {
            Cards = StandardDeckCards();

            //Spanish 21 does not have 10 cards
            Cards.RemoveAll(c => c.Id == 10);
        }
    }
}
