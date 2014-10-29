using System.Collections.Generic;
using WindowsGame6.gamePlay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame6.core
{
    public class CardsViewer : DrawableGameComponent
    {
        private readonly List<BuildingCard> cards;
        private readonly SpriteBatch spriteBatch;
        private int height;
        private float maxScale = 2f;
        private Vector2 position;
        private float scale = 1;
        private int selectedIndex;
        private float speeScale = 0.08f;
        private int width;

        public CardsViewer(Game game) : base(game)
        {
            spriteBatch = (SpriteBatch) Game.Services.GetService(typeof (SpriteBatch));
            cards = new List<BuildingCard>();
        }

        public void SetParams(List<BuildingCard> newCards, int listWidth, int cardHeight, Vector2 pos, float newMaxScale)
        {
            if (newCards.Count > 0)
            {
                height = cardHeight;
                maxScale = newMaxScale;
                position = pos;
                width = listWidth;
                cards.Clear();
                var cardWidth = (int) ((float) newCards[0].Width*cardHeight/newCards[0].Height);
                float Dx = (listWidth - cardWidth)/(newCards.Count - 1);
                for (int i = 0; i < newCards.Count; i++)
                {
                    cards.Add(newCards[i]);
                    cards[i].Position = new Vector2(position.X + Dx*i, position.Y);
                    cards[i].Width = cardWidth;
                    cards[i].Height = cardHeight;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;
            if (mouseY > position.Y && mouseY < (position.Y + height) && mouseX > position.X &&
                mouseX < (position.X + width))
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    if (i == cards.Count - 1 && cards[i].IsOverCard(new Vector2(mouseX, mouseY)))
                    {
                        if (i != selectedIndex)
                        {
                            scale = 1;
                            selectedIndex = i;
                        }
                    }
                    else if (cards[i].IsOverCard(new Vector2(mouseX, mouseY)) && mouseX < cards[i + 1].Position.X)
                    {
                        if (i != selectedIndex)
                        {
                            scale = 1;
                            selectedIndex = i;
                        }
                    }
                }
            }
            else
            {
                selectedIndex = -1;
            }
            if (scale < maxScale)
            {
                scale += speeScale;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (i != selectedIndex)
                {
                    cards[i].Draw(spriteBatch, 1, Card.DrawMode.Front);
                }
            }
            if (selectedIndex >= 0)
            {
                cards[selectedIndex].Draw(spriteBatch, scale, Card.DrawMode.Front);
            }
            base.Draw(gameTime);
        }
    }
}