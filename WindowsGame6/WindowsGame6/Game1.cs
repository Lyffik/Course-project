using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WindowsGame6.core;
using WindowsGame6.gamePlay;
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

            var helpList = new List<Texture2D>();
            helpList.Add(Content.Load<Texture2D>("Help/Help_1"));
            helpList.Add(Content.Load<Texture2D>("Help/Help23"));
            helpList.Add(Content.Load<Texture2D>("Help/Help45"));
            helpList.Add(Content.Load<Texture2D>("Help/Help67"));
            helpList.Add(Content.Load<Texture2D>("Help/Help_8"));
            helpBackgroundTexture = Content.Load<Texture2D>("Help/HelpGround");
            helpScene = new HelpScene(this, helpBackgroundTexture, helpList);
            Components.Add(helpScene);

            actionBackgroundTexture = Content.Load<Texture2D>("Action/GameTable2");
            var playersList = new List<Texture2D>();
            playersList.Add(Content.Load<Texture2D>("Character/assasin"));
            playersList.Add(Content.Load<Texture2D>("Character/thief"));
            playersList.Add(Content.Load<Texture2D>("Character/magician"));
            playersList.Add(Content.Load<Texture2D>("Character/king"));
            playersList.Add(Content.Load<Texture2D>("Character/bishop"));
            playersList.Add(Content.Load<Texture2D>("Character/merchant"));
            playersList.Add(Content.Load<Texture2D>("Character/architect"));
            playersList.Add(Content.Load<Texture2D>("Character/warlord"));
            var cardList = new List<BuildingCard>();

            //Load Buildings
            string[] text = File.ReadAllLines("D:\\test.txt", Encoding.UTF8);
            foreach (string s in text)
            {
                List<string> subStrings = s.Split(',').Select(x => x.Trim()).ToList();
                var front = Content.Load<Texture2D>(subStrings[0]);
                var shirt = Content.Load<Texture2D>(subStrings[1]);
                string name = Convert.ToString(subStrings[2]);
                int cost = Convert.ToInt32(subStrings[3]);
                cardList.Add(new BuildingCard(front, shirt, name, cost));
            }
            var buttons = new List<Texture2D>();
            var panelTexture = Content.Load<Texture2D>("Action/panel");
            buttons.Add(Content.Load<Texture2D>("Action/panelButtonRed"));
            buttons.Add(Content.Load<Texture2D>("Action/panelButtonGreen"));
            buttons.Add(Content.Load<Texture2D>("Action/panelButtonBlue"));
            buttons.Add(Content.Load<Texture2D>("Action/panelButtonYellow"));
            buttons.Add(Content.Load<Texture2D>("Action/panelButtonViolet"));
            buttons.Add(Content.Load<Texture2D>("Action/panelButtonMoney"));
            buttons.Add(Content.Load<Texture2D>("Action/button"));
            smallFont = Content.Load<SpriteFont>("Action/numberFont");
            actionScene = new ActionScene(this, actionBackgroundTexture, playersList, cardList, panelTexture, buttons,
                smallFont);
            Components.Add(actionScene);


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
            activeScene = startScene;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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
                    switch (gameModeScene.GetMenuSelectedIndex())
                    {
                        case 0:

                            break;
                        case 1:

                            break;
                        case 2:

                            break;
                        case 3:

                            break;
                        case 4:

                            break;
                        case 5:

                            break;
                        case 6:

                            break;
                        case 7:

                            break;
                    }
                    activeScene.Hide();
                    activeScene = actionScene;
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