using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame6.core
{
    /// <summary>
    ///     This is a game component that implements IUpdateable.
    /// </summary>
    public class StartScene : GameScene
    {
        private readonly TextMenuComponent menu;
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private SpriteBatch spriteBatch;

        public StartScene(Game game, Texture2D backGround, SpriteFont smallFont, SpriteFont largeFont) : base(game)
        {
            Components.Add(new ImageComponent(game, backGround,
                ImageComponent.DrawMode.Stretch));
            string[] items = {"New Game", "Help", "Quit"};
            menu = new TextMenuComponent(game, smallFont, largeFont);
            menu.SetMenuItems(items);
            Components.Add(menu);

            spriteBatch = (SpriteBatch) Game.Services.GetService(typeof (SpriteBatch));
        }

        public int GetMenuSelectedIndex()
        {
            return menu.SelectedIndex;
        }

        public override void Show()
        {
            menu.Position = new Vector2(50,Game.Window.ClientBounds.Height/2);
            menu.Visible = true;
            menu.Enabled = true;

            base.Show();
        }
    }
}