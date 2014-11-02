using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WindowsGame6.core;
using WindowsGame6.gamePlay;
using WindowsGame6.gamePlay.Characters;
using WindowsGame6.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame6
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private Texture2D actionBackgroundTexture;
        private ActionScene actionScene;
        private GameScene activeScene;
        private Texture2D gameModeBackground;
        private GameModeScene gameModeScene;
        private Texture2D helpBackgroundTexture, helpForegroundTexture;
        private HelpScene helpScene;
        private SpriteFont largeFont;
        private KeyboardState oldKeyboardState;
        private MouseState oldMouseState;
        private int playersCount;
        private SpriteFont smallFont;
        private SpriteBatch spriteBatch;
        private Texture2D startBackGround;
        private Texture2D startElementsTexture;
        private StartScene startScene;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
            oldKeyboardState = Keyboard.GetState();
            oldMouseState = Mouse.GetState();
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            Services.AddService(typeof (SpriteBatch), spriteBatch);

            #region Load Scene of Help

            var helpList = new List<Texture2D>();
            helpList.Add(Content.Load<Texture2D>("Help/Help_1"));
            helpList.Add(Content.Load<Texture2D>("Help/Help23"));
            helpList.Add(Content.Load<Texture2D>("Help/Help45"));
            helpList.Add(Content.Load<Texture2D>("Help/Help67"));
            helpList.Add(Content.Load<Texture2D>("Help/Help_8"));
            helpBackgroundTexture = Content.Load<Texture2D>("Help/HelpGround");
            helpScene = new HelpScene(this, helpBackgroundTexture, helpList);
            Components.Add(helpScene);

            #endregion

            #region Load Cards Characters

            var charactersCards = new List<Character>();
            string[] text = File.ReadAllLines("Content\\Character\\Characters.txt", Encoding.UTF8);
            for (int i = 0; i < text.Length; i++)
            {
                string str = text[i];
                List<string> substrings = str.Split(',').Select(x => x.Trim()).ToList();
                if (substrings.Count == 5)
                {
                    var front = Content.Load<Texture2D>(substrings[0]);
                    var shirt = Content.Load<Texture2D>(substrings[1]);
                    string name = Convert.ToString(substrings[2]);
                    int rank = Convert.ToInt32(substrings[3]);
                    Card.GameClass gameClass = Card.GetGameClass(substrings[4]);
                    switch (i)
                    {
                        case 0:
                            charactersCards.Add(new Assasin(this, front, shirt, name, rank, gameClass));
                            break;
                        case 1:
                            charactersCards.Add(new Thief(this, front, shirt, name, rank, gameClass));
                            break;
                        case 2:
                            charactersCards.Add(new Magician(this, front, shirt, name, rank, gameClass));
                            break;
                        case 3:
                            charactersCards.Add(new King(this, front, shirt, name, rank, gameClass));
                            break;
                        case 4:
                            charactersCards.Add(new Merchant(this, front, shirt, name, rank, gameClass));
                            break;
                        case 5:
                            charactersCards.Add(new Bishop(this, front, shirt, name, rank, gameClass));
                            break;
                        case 6:
                            charactersCards.Add(new Architect(this, front, shirt, name, rank, gameClass));
                            break;
                        case 7:
                            charactersCards.Add(new WarLord(this, front, shirt, name, rank, gameClass));
                            break;
                    }
                }
            }

            #endregion

            #region Load Cards Buildings

            var buildingsCards = new List<Building>();
            text = File.ReadAllLines("Content\\Buildings\\BuildingsCard.txt", Encoding.UTF8);
            foreach (string s in text)
            {
                List<string> substrings = s.Split(',').Select(x => x.Trim()).ToList();
                if (substrings.Count == 6)
                {
                    int count = Convert.ToInt32(substrings[0]);
                    for (int i = 0; i < count; i++)
                    {
                        var front = Content.Load<Texture2D>(substrings[1]);
                        var shirt = Content.Load<Texture2D>(substrings[2]);
                        string name = Convert.ToString(substrings[3]);
                        int cost = Convert.ToInt32(substrings[4]);
                        Card.GameClass gameClass = Card.GetGameClass(substrings[5]);
                        buildingsCards.Add(new Building(this, front, shirt, name, cost, gameClass));
                    }
                }
            }

            #endregion

            #region Load Scene of Action

            actionBackgroundTexture = Content.Load<Texture2D>("Action/GameTable2");
            var buttons = new List<Texture2D>();
            var panelTexture = Content.Load<Texture2D>("Action/panel");
            buttons.Add(Content.Load<Texture2D>("Action/panelButtonRed"));
            buttons.Add(Content.Load<Texture2D>("Action/panelButtonGreen"));
            buttons.Add(Content.Load<Texture2D>("Action/panelButtonBlue2"));
            buttons.Add(Content.Load<Texture2D>("Action/panelButtonYellow1"));
            buttons.Add(Content.Load<Texture2D>("Action/panelButtonMoney"));
            buttons.Add(Content.Load<Texture2D>("Action/button"));
            smallFont = Content.Load<SpriteFont>("Action/numberFont");
            actionScene = new ActionScene(this, actionBackgroundTexture, buildingsCards, charactersCards, panelTexture,
                buttons, smallFont);
            Components.Add(actionScene);

            #endregion

            #region Load Menu

            smallFont = Content.Load<SpriteFont>("Menu/modeMenuSmall");
            largeFont = Content.Load<SpriteFont>("Menu/modeMenuLarge");
            gameModeBackground = Content.Load<Texture2D>("Menu/MenuBackGround");
            gameModeScene = new GameModeScene(this, gameModeBackground, smallFont, largeFont);
            Components.Add(gameModeScene);

            smallFont = Content.Load<SpriteFont>("Menu/menuSmall");
            largeFont = Content.Load<SpriteFont>("Menu/menuLarge");
            startBackGround = Content.Load<Texture2D>("Menu/MenuBackGround");
            startScene = new StartScene(this, startBackGround, smallFont, largeFont);
            Components.Add(startScene);

            #endregion

            activeScene = startScene;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            if (keyboardState.IsKeyDown(Keys.Q) && keyboardState.IsKeyDown(Keys.LeftAlt))
                Exit();
            bool MouseClick = (mouseState.LeftButton == ButtonState.Released &&
                               oldMouseState.LeftButton == ButtonState.Pressed);
            if (activeScene == gameModeScene)
            {
                if (MouseClick)
                {
                    int selectedIndex = gameModeScene.GetMenuSelectedIndex();
                    selectedIndex += 2;
                    if (selectedIndex > 1)
                    {
                        actionScene.NewGame(selectedIndex);

                        activeScene.Hide();
                        activeScene = actionScene;
                    }
                }
            }
            if (activeScene == startScene)
            {
                if (MouseClick)
                {
                    switch (startScene.GetMenuSelectedIndex())
                    {
                        case 0:
                            activeScene.Hide();
                            activeScene = gameModeScene;
                            break;
                        case 1:
                            activeScene.Hide();
                            activeScene = helpScene;
                            break;
                        case 2:
                            Exit();
                            break;
                        default:
                            break;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                activeScene.Hide();
                activeScene = startScene;
            }
            oldMouseState = mouseState;
            oldKeyboardState = keyboardState;
            activeScene.Show();
            base.Update(gameTime);
        }
    }
}