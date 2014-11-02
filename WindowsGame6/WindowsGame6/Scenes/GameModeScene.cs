using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame6.core
{
    public class GameModeScene : GameScene
    {
        private readonly TextMenuComponent modeMenu;

        public GameModeScene(Game game, Texture2D textureBack, SpriteFont smallFont, SpriteFont largeFont)
            : base(game)
        {
            Components.Add(new ImageComponent(game, textureBack, ImageComponent.DrawMode.Stretch));
            string[] items = {"2 Players", "3 Players", "4 Players", "5 Players", "6 Players", "7 Players"};
            modeMenu = new TextMenuComponent(game, smallFont, largeFont);
            modeMenu.SetMenuItems(items);
            Components.Add(modeMenu);
        }

        public int GetMenuSelectedIndex()
        {
            return modeMenu.SelectedIndex;
        }

        public override void Show()
        {
            modeMenu.Position = new Vector2(100, 300);
            modeMenu.Visible = true;
            modeMenu.Enabled = true;
            base.Show();
        }
    }
}