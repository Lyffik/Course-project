using System.Collections.Generic;
using WindowsGame6.gamePlay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame6.core
{
    public class CardsViewer : GameScene
    {
        private readonly SpriteBatch spriteBatch;
        private List<Card> cards;
        private Vector2 deckPosition;
        private int height;
        private float maxScale = 2;
        private Vector2 position;
        private int selectedIndex = -1;
        private int width;


        public CardsViewer(Game game) : base(game)
        {
            spriteBatch = (SpriteBatch) Game.Services.GetService(typeof (SpriteBatch));
            Enabled = true;
            Visible = true;
            cards = new List<Card>();
        }

        public Vector2 Position
        {
            get { return position; }
        }


        public void CloseDeck()
        {
            foreach (Card card in cards)
            {
                card.MaxScale = 1;
            }
            CalculateDeckPosition();
        }

        public void OpenDeck()
        {
            foreach (Card card in cards)
            {
                card.MaxScale = maxScale;
            }
            CalculateCardsPosition();
        }

        private void CalculateDeckPosition()
        {
            CalculateCardsBounds();
            if (cards != null && cards.Count > 0)
            {
                int dX = 2;
                int dY = 2;
                int i = 0;
                while (i < cards.Count && i < 10)
                {
                    cards[i].Position = new Vector2(deckPosition.X + dX*i, deckPosition.Y - dY*i);
                    i++;
                }
                while (i < cards.Count)
                {
                    cards[i].Position = cards[i - 1].Position;
                    i++;
                }
            }
        }


        private void CalculateCardsPosition()
        {
            CalculateCardsBounds();
            if (cards != null && cards.Count > 0)
            {
                float Dx = 0;
                if (cards.Count > 1)
                {
                    Dx = (width - cards[0].Width)/(cards.Count - 1);
                }
                for (int i = 0; i < cards.Count; i++)
                {
                    cards[i].Position = new Vector2(position.X + Dx*i, position.Y);
                }
            }
        }

        private void CalculateCardsBounds()
        {
            if (cards != null && cards.Count > 0)
            {
                var cardWidth = (int) (cards[0].RatioWidthToHeight*height);
                for (int i = 0; i < cards.Count; i++)
                {
                    cards[i].Width = cardWidth;
                    cards[i].Height = height;
                }
            }
        }

        public void SetCards(List<Card> newCards)
        {
            Components.Clear();
            cards = newCards;
            CalculateCardsBounds();
            foreach (Card card in cards)
            {
                if (!Components.Contains(card))
                {
                    Components.Add(card);
                }
            }
        }

        public void SetParams(int listWidth, int cardHeight, Vector2 pos, Vector2 deckPos, float newMaxScale)
        {
            deckPosition = deckPos;
            height = cardHeight;
            maxScale = newMaxScale;
            foreach (Card card in cards)
            {
                card.MaxScale = maxScale;
            }
            position = pos;
            width = listWidth;
        }


        private void SelectCard(MouseState mouseState)
        {
            int result = -1;
            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;
            if (mouseY > position.Y && mouseY < (position.Y + height) && mouseX > position.X &&
                mouseX < (position.X + width))
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    if (i == cards.Count - 1 && cards[i].IsOverCard(new Vector2(mouseX, mouseY)))
                    {
                        result = i;
                    }
                    else if (cards[i].IsOverCard(new Vector2(mouseX, mouseY)) && mouseX < cards[i + 1].Position.X)
                    {
                        result = i;
                    }
                }
            }


            if (result != -1)
            {
                if (!cards[result].IsSelected)
                {
                    cards[result].IsSelected = true;
                    if (selectedIndex != -1)
                    {
                        cards[selectedIndex].IsSelected = false;
                    }
                }
            }
            else
            {
                if (selectedIndex != -1)
                {
                    cards[selectedIndex].IsSelected = false;
                }
            }
            selectedIndex = result;
        }

        public override void Hide()
        {
            foreach (Card card in cards)
            {
                card.Enabled = false;
                card.Visible = false;
            }
            base.Hide();
        }

        public override void Show()
        {
            foreach (Card card in cards)
            {
                card.Visible = true;
                card.Enabled = true;
            }
            base.Show();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            SelectCard(mouseState);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (selectedIndex != -1)
            {
                cards[selectedIndex].Draw(gameTime);
            }
        }
    }
}