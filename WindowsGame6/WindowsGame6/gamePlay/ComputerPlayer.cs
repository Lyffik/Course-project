using System.Collections.Generic;
using WindowsGame6.core;
using WindowsGame6.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame6.gamePlay
{
    public class ComputerPlayer : GameScene
    {
        public delegate void HeroAbility(ComputerPlayer currentPlayer, ComputerPlayer targetPlayer);

        public HeroAbility Ability;

        protected ActionScene actionScene;
        protected List<Building> cardsOnHands;
        protected Character character;
        protected List<Building> constructedВuildings;
        protected CardsViewer constructedВuildingsViewer;
        protected int money = 0;
        protected MouseState oldMouseState;
        protected PanelButton panelButton;

        public ComputerPlayer(Game game, ActionScene action, string name, PanelButton panel)
            : base(game)
        {
            Components.Clear();
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
            Show();
            oldMouseState = Mouse.GetState();
            actionScene = action;
        }

        public bool CharacterIsSelect { get; set; }

        public int Rank
        {
            get { return character.Rank; }
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

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        public bool IsKilled { get; set; }
        public bool IsRobbed { get; set; }

        public event HeroAbility StartTurn;

        public void OnStartTurn(ComputerPlayer currentPlayer, ComputerPlayer targetPlayer)
        {
            StartTurn(currentPlayer, targetPlayer);
        }

        public void SetCharacter(Character newCharacter)
        {
            character = newCharacter;
        }

        public void ConstructBuilding(Building build) //TODO заменить на protected
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
                actionScene.Pause();
                Enabled = true;
                ShowCardsBuildings();
            }
            else
            {
                actionScene.Continiue();
                HideCardsBuildings();
            }
        }

        public virtual void SelectCharacter(CharactersDeck deck)
        {
            character = (Character) deck.GetCard();
            Ability = character.Ability;
            character.Mode = Card.DrawMode.Shirt;

            character.Visible = true;
            character.Enabled = true;
            character.Height = 180;
            character.IsSelected = false;
            character.Width = (int) (character.Height*character.RatioWidthToHeight);
            character.Position = panelButton.Position +
                                 new Vector2(((panelButton.Width - character.Width)/2), panelButton.Height + 10);
            actionScene.Components.Insert(1, character);
            CharacterIsSelect = true;
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
            FillPanelButtons();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        private void FillPanelButtons()
        {
            int blue = 0;
            int red = 0;
            int yellow = 0;
            int green = 0;
            foreach (Building card in cardsOnHands) //TODO заменить на constructedBuildings
            {
                switch (card.Class)
                {
                    case Card.GameClass.Blue:
                        blue++;
                        break;
                    case Card.GameClass.Green:
                        green++;
                        break;
                    case Card.GameClass.Red:
                        red++;
                        break;
                    case Card.GameClass.Yellow:
                        yellow++;
                        break;
                }
            }
            panelButton.GreenBuildingsCount = green;
            panelButton.BlueBuildingsCount = blue;
            panelButton.RedBuildingsCount = red;
            panelButton.YellowBuildingsCount = yellow;
            panelButton.Money = money;
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