using System.Collections.Generic;
using WindowsGame6.gamePlay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame6.core
{
    public class ActionScene : GameScene
    {
        private readonly CardsViewer buildings;
        private readonly CardsViewer cards;
        private readonly List<BuildingCard> listBuildings;
        private readonly PanelButton panel;
        private KeyboardState oldKeyboardState;
        private MouseState oldMouseState;

        private int selectedIndex;

        public ActionScene(Game game, Texture2D textureBack, List<Texture2D> playersList, List<BuildingCard> cardsList,
            Texture2D panelTexture, List<Texture2D> buttonsTextures, SpriteFont font)
            : base(game)
        {
            Components.Add(new ImageComponent(game, textureBack, ImageComponent.DrawMode.Stretch));

            panel = new PanelButton(game, panelTexture, font);
            panel.SetParams(panelTexture.Width, panelTexture.Height, buttonsTextures);
            panel.Position = new Vector2((Game.Window.ClientBounds.Width - panelTexture.Width)/2,
                Game.Window.ClientBounds.Height - panelTexture.Height);
            Components.Add(panel);

            cards = new CardsViewer(game);
            var cardsTextures = new List<BuildingCard>();
            for (int i = 0; i < 8; i++)
            {
                cardsTextures.Add(cardsList[i]);
            }
            cards.SetParams(cardsTextures, panelTexture.Width, 150, panel.Position - new Vector2(0, 150), 2.5f);
            Components.Add(cards);

            buildings = new CardsViewer(game);
            Components.Add(buildings);
            buildings.Enabled = false;
            buildings.Visible = false;
            listBuildings = cardsList;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            bool MouseClick = (mouseState.LeftButton == ButtonState.Released &&
                               oldMouseState.LeftButton == ButtonState.Pressed);
            if (MouseClick)
            {
                if (panel.IsBuildingsButtonClick(new Vector2(mouseState.X, mouseState.Y)) && buildings.Visible == false)
                {
                    buildings.SetParams(listBuildings, 1100, 200, new Vector2(100, 300), 2);
                    cards.Enabled = false;

                    buildings.Enabled = true;
                    buildings.Visible = true;
                }
                else if (panel.IsBuildingsButtonClick(new Vector2(mouseState.X, mouseState.Y)))
                {
                    cards.Enabled = true;
                    buildings.Enabled = false;
                    buildings.Visible = false;
                }
            }
            oldMouseState = mouseState;
            base.Update(gameTime);
        }
    }
}