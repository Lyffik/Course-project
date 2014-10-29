using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame6.core
{
    public class ImageComponent : DrawableGameComponent
    {
        public enum DrawMode
        {
            Center = 1,
            Stretch,
            HelpCenter,
            HelpLeft,
            HelpRight,
            Position
        };

        protected readonly DrawMode drawMode;
        protected readonly Texture2D texture;
        protected Rectangle imageRect;
        protected SpriteBatch spriteBatch = null;

        public ImageComponent(Game game, Texture2D texture, DrawMode drawMode)
            : base(game)
        {
            this.texture = texture;
            this.drawMode = drawMode;

            spriteBatch = (SpriteBatch) Game.Services.GetService(typeof (SpriteBatch));
            int rectHeight;
            int rectWidth;
            switch (drawMode)
            {
                case DrawMode.Center:
                    imageRect = new Rectangle((Game.Window.ClientBounds.Width - texture.Width)/2,
                        (Game.Window.ClientBounds.Height - texture.Height)/2, texture.Width, texture.Height);
                    break;
                case DrawMode.Stretch:
                    imageRect = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
                    break;
                case DrawMode.HelpCenter:
                    rectHeight = Game.Window.ClientBounds.Height - 10;
                    rectWidth = (int) ((((float) texture.Width/texture.Height))*rectHeight);
                    imageRect = new Rectangle((Game.Window.ClientBounds.Width - rectWidth)/2, 5, rectWidth, rectHeight);
                    break;
                case DrawMode.HelpLeft:
                    rectHeight = Game.Window.ClientBounds.Height - 10;
                    rectWidth = (int) ((((float) texture.Width/texture.Height))*rectHeight);
                    imageRect = new Rectangle((Game.Window.ClientBounds.Width/2 - rectWidth), 5, rectWidth, rectHeight);
                    break;
                case DrawMode.HelpRight:
                    rectHeight = Game.Window.ClientBounds.Height - 10;
                    rectWidth = (int) ((((float) texture.Width/texture.Height))*rectHeight);
                    imageRect = new Rectangle((Game.Window.ClientBounds.Width/2), 5, rectWidth, rectHeight);
                    break;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, imageRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}