using Excercise.CardGameDomain;
using Excercise.CardGameDomain.GameDecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Excercise.DeckVisualInspect.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }

        public ActionResult NewDeck(DeckType id)
        {
            IDeck model;
            switch(id) {
                case (DeckType.MultipleDecks):
                    model = new MultipleDecks(5);
                    break;
                case (DeckType.Spanish21):
                    model = new Spanish21Deck();
                    break;
                default:
                    model = new Deck();
                    break;
            }

            Session["Deck"] = model;
            return PartialView("_Deck", model);
        }

        public ActionResult Shuffle()
        {
            var deck = (Deck)Session["Deck"];
            if (deck != null)
            {
                deck.Shuffle();
                Session["Deck"] = deck;
            }
            return PartialView("_Deck", deck);
        }


        public ActionResult Sort()
        {
            var deck = (Deck)Session["Deck"];
            if (deck != null)
            {
                deck.Sort();
                Session["Deck"] = deck;
            }
            return PartialView("_Deck", deck);
        }
    }
}
