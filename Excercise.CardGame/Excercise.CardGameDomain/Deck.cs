using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Excercise.CardGameDomain
{
    public class Deck : IDeck
    {
        private Random random = new Random(Seed());

        public List<ICard> Cards { get; set; }

        public Deck()
        {
            Cards = StandardDeckCards();    
        }

        public Deck(List<ICard> cards)
        {
            Cards = cards;
        }

        /// <summary>
        ///     Standard deck with 52 cards, no jokers
        /// </summary>
        /// <returns></returns>
        public static List<ICard> StandardDeckCards()
        {
            var cards = new List<ICard>();

            for (var i = 1; i <= 13; i++)
            {
                cards.Add(new Card(i, CardSuit.Spade));
                cards.Add(new Card(i, CardSuit.Heart));
                cards.Add(new Card(i, CardSuit.Club));
                cards.Add(new Card(i, CardSuit.Diam));
            }

            return cards;
        }

        /// <summary>
        ///     To sort cards by their Ids and Suits.  
        ///     Aces have Id=1 and come before Two's.  
        ///     Suits are sorted in this order: Spade, Heart, Club, then Diamond
        /// </summary>
        public virtual void Sort()
        {
            Cards = Cards.OrderBy(c=>c.Id).ThenBy(c=>c.Suit).ToList();
        }

        /// <summary>
        ///     Shuff items in Cards 
        /// </summary>
        public void Shuffle()
        {
            Cards = Cards.OrderBy(c => random.Next()).ToList();
        }

        /// <summary>
        ///     Produce seed for the Random generator.  
        ///     The concept is to let Build manager and Deploy manager to define the seed. They are usually different persons.
        /// </summary>
        /// <returns>Seed</returns>
        private static int Seed()
        {
            //This is just a default value.  Todo: Setup someting like Buid Configuration that allows Build Manager to enter the value.
            int builtTimeSeed = Guid.NewGuid().GetHashCode();

            //Configured value must be provided and encrypted
            int configuredTimeSeed = 842370;
            int.TryParse(ConfigurationManager.AppSettings["RandomSeed"], out configuredTimeSeed);   

            //Include time as well to make it randome
            var seed = builtTimeSeed + configuredTimeSeed + DateTime.Now.Millisecond;
            return seed;
        }

        public void Cut()
        {
            throw new NotImplementedException();
        }

        public ICard Draw()
        {
            throw new NotImplementedException();
        }



    }


    public interface IDeck
    {
        List<ICard> Cards { get; set; }

        void Sort();

        void Shuffle();

        void Cut();

        ICard Draw();
    }


}
