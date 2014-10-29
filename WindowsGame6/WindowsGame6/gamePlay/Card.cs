using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame6.gamePlay
{
    [Serializable]
    public abstract class Card
    {
        public enum DrawMode
        {
            Front,
            Shirt
        }

        public enum GameClass
        {
            Red,
            Gree,
            Blue,
            Yellow,
            Gray
        }

        [XmlElement] protected Texture2D frontTexture;
        [XmlElement] protected GameClass gameClass;
     protected string name;
        protected Vector2 position;
      protected Texture2D shirtTexture;

        public Card()
        {
        }

        public Card(Texture2D front, Texture2D shirt, string cardName)
        {
            frontTexture = front;
            shirtTexture = shirt;
            name = cardName;
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

        public bool IsOverCard(Vector2 vector)
        {
            if ((vector.X > Position.X && (vector.X < Position.X + Width)) && ((vector.Y > Position.Y) &&
                                                                               (vector.Y < Position.Y + Height)))
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, float scale, DrawMode mode)
        {
            spriteBatch.Begin();
            var rect = new Rectangle((int) Position.X, (int) (Position.Y - Height*(scale - 1)),
                (int) (Width*scale), (int) (Height*scale));
            switch (mode)
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