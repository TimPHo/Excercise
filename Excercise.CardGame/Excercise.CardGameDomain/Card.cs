using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Excercise.CardGameDomain
{
    /// <summary>
    /// A standard card for card games. Id has a value between 1 and 13 (11=J, 12=Q, 13=K)
    /// </summary>
    public class Card: ICard
    {
        private int _id;
        private CardSuit _suit;

        public Card(int id, CardSuit suit)
        {
            _id = id;
            _suit = suit;
        }

        public int Id
        {
            get { return _id; }
        }

        public CardSuit Suit
        {
            get { return _suit; }
        }

        public string Name {
            get
            {
                if (Id == 1)
                    return "A";
                else if (Id == 11)
                    return "J";
                else if (Id == 12)
                    return "Q";
                else if (Id == 13)
                    return "K";
                else
                    return Id.ToString();
            }
        }
    }

    public interface ICard
    {
        int Id { get; }       

        CardSuit Suit { get; }

        string Name { get;  }
    }


}
