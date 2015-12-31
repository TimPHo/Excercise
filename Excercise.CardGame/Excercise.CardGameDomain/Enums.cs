using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Excercise.CardGameDomain
{
    public enum CardSuit
    {
        Spade = 1,
        Heart = 2,
        Club = 3,
        Diam = 4
    }

    public enum DeckType 
    {
        Standard = 0,
        Spanish21 = 1,
        ChinesePoker = 2,
        MultipleDecks = 3,
        //..
        Custom = 100
    }

}
