using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace WindowsGame6.core
{
    public class GameScene : DrawableGameComponent
    {
        private readonly List<GameComponent> components;

        public GameScene(Game game)
            : base(game)
        {
            components = new List<GameComponent>();

            Visible = false;
            Enabled = false;
        }

        public List<GameComponent> Components
        {
            get { return components; }
        }

        public virtual void Show()
        {
            Visible = true;
            Enabled = true;
        }

        public virtual void Hide()
        {
            Visible = false;
            Enabled = false;
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].Enabled)
                {
                    components[i].Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent gc in components)
            {
                if ((gc is DrawableGameComponent) &&
                    ((DrawableGameComponent) gc).Visible)
                {
                    ((DrawableGameComponent) gc).Draw(gameTime);
                }
            }

            base.Draw(gameTime);
        }
    }
}