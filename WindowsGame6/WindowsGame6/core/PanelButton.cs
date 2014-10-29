using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame6.core
{
    /// <summary>
    ///     This is a game component that implements IUpdateable.
    /// </summary>
    public class PanelButton : DrawableGameComponent
    {
        private readonly List<Button> buttons;
        private readonly Texture2D panel;
        private readonly SpriteBatch spriteBatch;
        private readonly SpriteFont spriteFont;
        private MouseState oldMouseState;
        private int panelHeight;
        private int panelWidth;
        private Vector2 position;


        public PanelButton(Game game, Texture2D panelTexture, SpriteFont font)
            : base(game)
        {
            spriteBatch = (SpriteBatch) Game.Services.GetService(typeof (SpriteBatch));

            spriteFont = font;
            panel = panelTexture;
            buttons = new List<Button>();
            oldMouseState = Mouse.GetState();
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public void SetParams(int width, int height, List<Texture2D> btnTextures)
        {
            panelWidth = width;
            panelHeight = height;
            int btnSize;
            if (width/btnTextures.Count >= height)
            {
                btnSize = (height - height/2);
            }
            else
            {
                btnSize = (width - width/2)/btnTextures.Count;
            }
            buttons.Clear();
            foreach (Texture2D btnTexture in btnTextures)
            {
                var button = new Button(btnTexture);
                button.Height = button.Width = btnSize;
                buttons.Add(button);
            }
        }

        public bool IsBuildingsButtonClick(Vector2 vector)
        {
            if (buttons[buttons.Count - 1].IsOverButton(vector))
            {
                return true;
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            bool MouseClick = (mouseState.LeftButton == ButtonState.Released &&
                               oldMouseState.LeftButton == ButtonState.Pressed);

            float dx = 0;
            foreach (Button button in buttons)
            {
                dx += button.Width;
            }
            dx = (panelWidth - dx - panelWidth/6)/(buttons.Count + 1);
            float x = position.X + panelWidth/8;

            foreach (Button button in buttons)
            {
                float y = position.Y + (panelHeight - button.Height)/2;
                button.position = new Vector2(x, y);
                button.TextPosition = new Vector2(
                    (int) (button.position.X + (button.Width - spriteFont.MeasureString(button.Text).X)/2),
                    (int) (button.position.Y + (button.Height - spriteFont.MeasureString(button.Text).Y)/2));
                x += dx + button.Width;
            }
            oldMouseState = mouseState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(panel, new Rectangle((int) position.X, (int) position.Y, panelWidth, panelHeight),
                Color.White);
            spriteBatch.End();
            for (int i = 0; i < buttons.Count; i++)
            {
                Button button = buttons[i];
                switch (i)
                {
                    case 5:
                        button.TextPosition.Y += button.Height/6;
                        button.Draw(spriteBatch, Button.DrawMode.Text, spriteFont);
                        break;
                    case 6:
                        button.Draw(spriteBatch, Button.DrawMode.Default, spriteFont);
                        break;
                    default:
                        button.Draw(spriteBatch, Button.DrawMode.Text, spriteFont);
                        break;
                }
            }

            base.Draw(gameTime);
        }


        private class Button
        {
            public enum DrawMode
            {
                Text,
                Default
            }

            public readonly int count;
            public readonly Texture2D texture;
            public int Height;
            public Vector2 TextPosition;
            public int Width;
            public Vector2 position;

            public Button(Texture2D btnTexture)
            {
                texture = btnTexture;
                count = 9;
            }

            public string Text
            {
                get { return count.ToString(); }
            }

            public bool IsOverButton(Vector2 vector)
            {
                if ((vector.X > position.X && (vector.X < position.X + Width)) && ((vector.Y > position.Y) &&
                                                                                   (vector.Y < position.Y + Height)))
                {
                    return true;
                }
                return false;
            }

            public void Draw(SpriteBatch spriteBatch, DrawMode mode, SpriteFont font)
            {
                switch (mode)
                {
                    case DrawMode.Text:
                        spriteBatch.Begin();
                        spriteBatch.Draw(texture, new Rectangle((int) position.X, (int) position.Y, Width, Height),
                            Color.White);
                        spriteBatch.DrawString(font, Text, TextPosition, Color.Black);
                        spriteBatch.End();
                        break;
                    default:
                        spriteBatch.Begin();
                        spriteBatch.Draw(texture, new Rectangle((int) position.X, (int) position.Y, Width, Height),
                            Color.White);
                        spriteBatch.End();
                        break;
                }
            }
        }
    }
}