using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace WindowsGame6.gamePlay
{
    public abstract class DeckOfCards : DrawableGameComponent
    {
        protected List<Card> cards;

        protected DeckOfCards(Game game) : base(game)
        {
        }

        public void Shuffle()
        {
            var random = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        public void SetDeckCards(List<Card> deck)
        {
            cards = deck;
        }

        public Card GetCard()
        {
            Card result = cards[0];
            cards.Remove(result);
            return result;
        }

        public void DropCard(Card card)
        {
            cards.Add(card);
        }
    }
}