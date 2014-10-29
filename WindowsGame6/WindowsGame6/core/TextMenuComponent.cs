using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame6.core
{
    /// <summary>
    ///     This is a game component that implements IUpdateable.
    /// </summary>
    public class TextMenuComponent : DrawableGameComponent
    {
        private readonly List<Item> menuItems;
        protected readonly SpriteFont regularFont, selectedFont;
        private readonly List<int> textPositions;
        protected int height;
        protected Vector2 position = new Vector2();
        protected Color regularColor = Color.LightGray, selectedColor = Color.Red;
        protected int selectedIndex = 0;
        protected SpriteBatch spriteBatch = null;
        protected int width;

        public TextMenuComponent(Game game, SpriteFont normalFont, SpriteFont selectedFont) : base(game)
        {
            spriteBatch = (SpriteBatch) Game.Services.GetService(typeof (SpriteBatch));

            regularFont = normalFont;
            this.selectedFont = selectedFont;
            menuItems = new List<Item>();
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }

        public Color RegularColor
        {
            get { return regularColor; }
            set { regularColor = value; }
        }

        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        protected void CalculateBounds()
        {
            width = 0;
            height = 0;
            foreach (Item item in menuItems)
            {
                if (item.Width > width)
                {
                    width = item.Width;
                }
                height += selectedFont.LineSpacing;
            }
        }

        public void SetMenuItems(string[] items)
        {
            menuItems.Clear();
            foreach (string item in items)
            {
                menuItems.Add(new Item(regularFont, item));
            }
            CalculateBounds();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;

            selectedIndex = -1;
            int i = 0;
            if (mouseX >= position.X && mouseX < (position.X + width))
            {
                foreach (Item menuItem in menuItems)
                {
                    if (mouseY > menuItem.Position.Y && mouseY < (menuItem.Position.Y + menuItem.Height) &&
                        (mouseX < menuItem.Position.X + menuItem.Width))
                    {
                        selectedIndex = i;
                    }
                    i++;
                }
            }
            float y = position.Y;
            i = 0;
            foreach (Item menuItem in menuItems)
            {
                if (i == SelectedIndex)
                {
                    menuItem.Font = selectedFont;
                    menuItem.Color = selectedColor;
                }
                else
                {
                    menuItem.Font = regularFont;
                    menuItem.Color = regularColor;
                }
                menuItem.Position.X = position.X;
                menuItem.Position.Y = y;
                y += menuItem.Height;
                i++;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Item menuItem in menuItems)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(menuItem.Font, menuItem.Text, menuItem.Position + new Vector2(3, 3), Color.Black);
                spriteBatch.DrawString(menuItem.Font, menuItem.Text, menuItem.Position, menuItem.Color);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        private class Item
        {
            public readonly string Text;
            public Color Color;
            public SpriteFont Font;
            public Vector2 Position;

            public Item(SpriteFont font, string text)
            {
                Text = text;
                Font = font;
            }

            public int Width
            {
                get { return (int) Font.MeasureString(Text).X; }
            }

            public int Height
            {
                get { return Font.LineSpacing; }
            }
        }
    }
}