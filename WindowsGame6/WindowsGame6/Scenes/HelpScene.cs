using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame6.core
{
    public class HelpScene : GameScene
    {
        private readonly List<ImageComponent> helpImages;
        private readonly List<Texture2D> helpTextures;
        private ImageComponent currentImage;
        protected KeyboardState oldKeyboardState;
        protected int selectedIndex = 0;
        protected SpriteBatch spriteBatch = null;

        public HelpScene(Game game, Texture2D textureBack, List<Texture2D> helpList)
            : base(game)
        {
            helpTextures = helpList;
            Components.Add(new ImageComponent(game, textureBack,
                ImageComponent.DrawMode.Stretch));
            helpImages = new List<ImageComponent>();
            foreach (Texture2D helpTexture in helpTextures)
            {
                currentImage = new ImageComponent(game, helpTexture, ImageComponent.DrawMode.HelpCenter);
                currentImage.Enabled = false;
                currentImage.Visible = false;
                helpImages.Add(currentImage);
                Components.Add(currentImage);
            }
            currentImage = helpImages[0];
            currentImage.Visible = true;
            currentImage.Enabled = true;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            bool right = (oldKeyboardState.IsKeyDown(Keys.Right) &&
                          (keyboardState.IsKeyUp(Keys.Right)));
            bool left = (oldKeyboardState.IsKeyDown(Keys.Left) &&
                         (keyboardState.IsKeyUp(Keys.Left)));


            if (right)
            {
                selectedIndex++;
                if (selectedIndex == helpTextures.Count)
                {
                    selectedIndex = 0;
                }
            }
            if (left)
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = helpTextures.Count - 1;
                }
            }
            if (currentImage != helpImages[selectedIndex])
            {
                currentImage.Enabled = false;
                currentImage.Visible = false;
                currentImage = helpImages[selectedIndex];
                currentImage.Enabled = true;
                currentImage.Visible = true;
            }
            currentImage = helpImages[selectedIndex];
            oldKeyboardState = keyboardState;

            base.Update(gameTime);
        }
    }
}