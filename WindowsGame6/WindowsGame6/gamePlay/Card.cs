using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame6.gamePlay
{
    public abstract class Card : DrawableGameComponent
    {
        public enum DrawMode
        {
            Front,
            Shirt
        }

        public enum GameClass
        {
            Red,
            Green,
            Blue,
            Yellow,
            Violet,
            Gray
        }

        protected DrawMode drawMode;
        protected Texture2D frontTexture;
        protected GameClass gameClass;
        protected bool isSelected = false;
        protected string name;
        protected Vector2 position;
        protected float scale;
        protected Texture2D shirtTexture;
        protected float speedScale = 0.08f;
        protected SpriteBatch spriteBatch;

        protected Card(Game game, Texture2D front, Texture2D shirt, string cardName, GameClass cardClass) : base(game)
        {
            spriteBatch = (SpriteBatch) Game.Services.GetService(typeof (SpriteBatch));
            gameClass = cardClass;
            frontTexture = front;
            shirtTexture = shirt;
            name = cardName;
            MaxScale = 2;
            scale = 1;
            Visible = true;
            Enabled = true;
            isSelected = false;
        }

        public float MaxScale { get; set; }

        public float RatioWidthToHeight
        {
            get { return (float) frontTexture.Width/frontTexture.Height; }
        }

        public GameClass Class
        {
            get { return gameClass; }
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public string Name
        {
            get { return name; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                scale = 1;
                isSelected = value;
            }
        }

        public DrawMode Mode
        {
            set { drawMode = value; }
        }


        public static List<Card> BuildingsToCards(List<Building> buildings)
        {
            var result = new List<Card>();
            foreach (Building building in buildings)
            {
                result.Add(building);
            }
            return result;
        }

        public static List<Card> CharactersToCards(List<Character> characters)
        {
            var result = new List<Card>();
            foreach (Character character in characters)
            {
                result.Add(character);
            }
            return result;
        }

        public static GameClass GetGameClass(string str)
        {
            switch (str.ToLower())
            {
                case "red":
                    return GameClass.Red;
                case "green":
                    return GameClass.Green;
                case "blue":
                    return GameClass.Blue;
                case "yellow":
                    return GameClass.Yellow;
                case "violet":
                    return GameClass.Violet;
                default:
                    return GameClass.Gray;
            }
        }

        public bool IsOverCard(Vector2 vector)
        {
            if ((vector.X > Position.X && (vector.X < Position.X + Width)) && ((vector.Y > Position.Y) &&
                                                                               (vector.Y < Position.Y + Height)))
            {
                return true;
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            if (isSelected)
            {
                if (scale < MaxScale)
                {
                    scale += speedScale;
                }
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            var rect = new Rectangle((int) Position.X, (int) (Position.Y - Height*(scale - 1)), (int) (Width*scale),
                (int) (Height*scale));
            switch (drawMode)
            {
                case DrawMode.Front:
                    spriteBatch.Draw(frontTexture, rect, Color.White);
                    break;
                case DrawMode.Shirt:
                    spriteBatch.Draw(shirtTexture, rect, Color.White);
                    break;
            }
            spriteBatch.End();
        }
    }
}