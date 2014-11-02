using System.Collections.Generic;
using WindowsGame6.core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame6.gamePlay
{
    public class ComputerPlayer : GameScene
    {
        protected List<Building> cardsOnHands;
        protected Character character;
        protected List<Building> constructedВuildings;
        protected CardsViewer constructedВuildingsViewer;
        protected MouseState oldMouseState;
        protected PanelButton panelButton;

        public ComputerPlayer(Game game, string name, PanelButton panel) : base(game)
        {
            cardsOnHands = new List<Building>();
            constructedВuildings = new List<Building>();
            Name = name;
            panelButton = panel;
            Components.Add(panelButton);
            constructedВuildingsViewer = new CardsViewer(game);
            constructedВuildingsViewer.SetParams(Game.Window.ClientBounds.Width/2, 220,
                new Vector2(Game.Window.ClientBounds.Width/4, Game.Window.ClientBounds.Height*3/8), new Vector2(0, 0),
                1.75f);
            constructedВuildingsViewer.Hide();
            Components.Add(constructedВuildingsViewer);
            oldMouseState = Mouse.GetState();
            Show();
        }

        public Character Character
        {
            get { return character; }
            set { character = value; }
        }

        public string Name { get; set; }

        public int CardsCount
        {
            get { return cardsOnHands.Count; }
        }

        public bool Active { get; set; }
        public int Money { get; set; }

        public void SetCharacter(Character newCharacter)
        {
            character = newCharacter;
        }

        public void ConstructBuilding(Building build) //заменить на protected
        {
            constructedВuildings.Add(build);
            cardsOnHands.Remove(build);
        }

        public void DestroyBuilding(Building build, BuildingsDeck deck)
        {
            constructedВuildings.Remove(build);
            DropCardToDeck(build, deck);
        }

        public void GetCardsFromDeck(int count, BuildingsDeck deck)
        {
            for (int i = 0; i < count; i++)
            {
                cardsOnHands.Add((Building) deck.GetCard());
            }
        }

        public void DropCardToDeck(Building card, BuildingsDeck deck)
        {
            deck.AddCard(card);
            cardsOnHands.Remove(card);
        }

        protected void ConstractBuildingButtonClick(MouseState mouseState)
        {
            if (constructedВuildingsViewer.Visible == false)
            {
                constructedВuildingsViewer.SetCards(Card.BuildingsToCards(cardsOnHands));
                //ToDo Заменить на constructedВuildings
                ShowCardsBuildings();
            }
            else
            {
                HideCardsBuildings();
            }
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            bool MouseClick = (mouseState.LeftButton == ButtonState.Released &&
                               oldMouseState.LeftButton == ButtonState.Pressed);
            if (MouseClick)
            {
                if (panelButton.IsBuildingsButtonClick(new Vector2(mouseState.X, mouseState.Y)))
                {
                    ConstractBuildingButtonClick(mouseState);
                }
            }
            oldMouseState = mouseState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


        public void ShowCardsBuildings()
        {
            constructedВuildingsViewer.Show();
            constructedВuildingsViewer.OpenDeck();
        }

        public void HideCardsBuildings()
        {
            constructedВuildingsViewer.Hide();
        }
    }
}