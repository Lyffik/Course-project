using System.Collections.Generic;
using WindowsGame6.core;
using Microsoft.Xna.Framework;

namespace WindowsGame6.gamePlay
{
    public class CharactersDeck : Deck
    {
        private readonly List<Card> discardCards;
        private readonly CardsViewer discardDeckViewer;

        public CharactersDeck(Game game, int cardHeight)
            : base(game)
        {
            discardCards = new List<Card>();
            cardsViewer.SetParams(Game.Window.ClientBounds.Width/2, 220,
                new Vector2(Game.Window.ClientBounds.Width/4,
                    (Game.Window.ClientBounds.Height - cardHeight)/2),
                new Vector2(10, Game.Window.ClientBounds.Height - 240), 1.75f);
            discardDeckViewer = new CardsViewer(game);
            discardDeckViewer.SetParams(200, 220, cardsViewer.DeckPosition - new Vector2(0, 240),
                cardsViewer.DeckPosition + new Vector2(0, 240), 1.5f);
            discardDeckViewer.Show();
            Components.Add(discardDeckViewer);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void DiscardOpenly(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Card card = GetCard();
                if (card != null)
                {
                    if (card.Class == Card.GameClass.Yellow)
                    {
                        AddCard(card);
                        card = GetCard();
                    }
                    if (card != null)
                    {
                        card.Mode = Card.DrawMode.Front;
                        card.Position = discardDeckViewer.Position + new Vector2(discardCards.Count*15, 0);
                        discardCards.Add(card);
                    }
                }
            }
            discardDeckViewer.SetCards(discardCards);
        }

        public void DiscardClosed(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Card card = GetCard();
                if (card != null)
                {
                    cards.Remove(card);
                    card.Mode = Card.DrawMode.Shirt;
                    card.Position = discardDeckViewer.Position + new Vector2(discardCards.Count*15, 0);
                    discardCards.Add(card);
                }
                else
                {
                    break;
                }
            }
            discardDeckViewer.SetCards(discardCards);
        }
    }
}