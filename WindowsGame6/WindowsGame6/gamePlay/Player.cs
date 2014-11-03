using System;
using WindowsGame6.core;
using WindowsGame6.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame6.gamePlay
{
    public class Player : ComputerPlayer
    {
        private readonly CardsViewer cardsOnHandsViewer;

        private Deck currentDeck;

        public Player(Game game, ActionScene actionScene, string name, PanelButton panel)
            : base(game, actionScene, name, panel)
        {
            cardsOnHandsViewer = new CardsViewer(game);
            Components.Add(cardsOnHandsViewer);
        }

        public override void SelectCharacter(CharactersDeck deck)
        {
            currentDeck = deck;
            deck.ShowDeck = true;
            if (character != null)
            {
                CharacterIsSelect = true;
                character.Mode = Card.DrawMode.Front;
                character.Visible = true;
                character.Enabled = true;
                character.Height = 180;
                character.MaxScale = 1.75f;
                character.IsSelected = false;
                character.Width = (int) (character.Height*character.RatioWidthToHeight);
                character.Position = new Vector2(20, Game.Window.ClientBounds.Height - character.Height);
                actionScene.Components.Insert(1, character);
                currentDeck.ShowDeck = false;
            }
        }

     

        public Card SelectCard(MouseState mouseState, Deck deck)
        {
            Card card = null;
            card = deck.ReturnSelectedCard(mouseState);
            return card;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            bool MouseClick = (mouseState.LeftButton == ButtonState.Released &&
                               oldMouseState.LeftButton == ButtonState.Pressed);
            if (Active)
            {
                if (MouseClick)
                {
                    character = (Character) SelectCard(mouseState, currentDeck);
                }
            }
            base.Update(gameTime);
        }

        public void HideCardsOnHands()
        {
            cardsOnHandsViewer.Hide();
        }
    }
}