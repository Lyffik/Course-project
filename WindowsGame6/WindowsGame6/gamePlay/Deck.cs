using System;
using System.Collections.Generic;
using WindowsGame6.core;
using Microsoft.Xna.Framework;

namespace WindowsGame6.gamePlay
{
    public abstract class Deck : GameScene
    {
        protected List<Card> cards;
        protected CardsViewer cardsViewer;


        protected Deck(Game game) : base(game)
        {
            cardsViewer = new CardsViewer(game);
            Components.Add(cardsViewer);
            
        }

        public bool ShowDeck { get; set; }

        public void DiscardOpenly()
        {
            Card card = GetCard();
            cards.Remove(card);
            cardsViewer.SetCards(cards);
            card.Mode = Card.DrawMode.Shirt;
            card.Position = cardsViewer.Position + new Vector2(0, card.Height + 10);
        }

        public void DiscardClosed(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Card card = GetCard();
                cards.Remove(card);
                cardsViewer.SetCards(cards);
                card.Mode = Card.DrawMode.Front;
                card.Position = cardsViewer.Position + new Vector2(0, card.Height + 10);
            }
        }

        public void Open()
        {
            foreach (Card character in cards)
            {
                character.Mode = Card.DrawMode.Front;
            }
            cardsViewer.OpenDeck();
        }

        public void Close()
        {
            foreach (Card character in cards)
            {
                character.Mode = Card.DrawMode.Shirt;
            }
            cardsViewer.CloseDeck();
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
            if (cards != null && cards.Count > 0)
            {
                Card result = cards[0];
                cards.Remove(result);
                return result;
            }
            return null;
        }

        public override void Update(GameTime gameTime)
        {
            cardsViewer.SetCards(cards);
            if (ShowDeck)
            {
                Open();
            }
            else
            {
                Close();
            }
            base.Update(gameTime);
        }

        private void UpdateDrawing()
        {
        }

        public void AddCard(Card card)
        {
            if (cards != null)
            {
                cards.Add(card);
            }
        }
    }
}