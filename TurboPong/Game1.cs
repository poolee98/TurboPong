using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;

using TurboPong.Globals;

namespace TurboPong
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        // <------------------------------------------- Screen management -------------------------------------------> //
        public ScreenManager screenManager = new ScreenManager();

        private bool LoadMenuWithTransition = false;
        private Screens.BattlegroundScreen battlegroundScreen;
        private Screens.MenuScreen menuScreen;
        private Screens.GametypeScreen gameTypeScreen;
        private bool isMenuScreenLoaded = false;
        private bool isBattleGroundMenuLoaded = false;

        public void LoadGameScreen()
        {
            screenManager.LoadScreen(battlegroundScreen, new ExpandTransition(GraphicsDevice, Color.Black));
            isBattleGroundMenuLoaded = true;

            if (isMenuScreenLoaded)
            {
                GraphicsDevice.Clear(Color.Transparent);
                menuScreen.UnloadContent();
                isMenuScreenLoaded = false;
            }
        }

        public void LoadChooseGameType(Game game)
        {
            screenManager.LoadScreen(gameTypeScreen);
        }

        public void LoadMenuScreen()
        {
            if (LoadMenuWithTransition)
            {
                screenManager.LoadScreen(menuScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
            else
            {
                screenManager.LoadScreen(menuScreen);
                LoadMenuWithTransition = true;
            }
            isMenuScreenLoaded = true;

            if (isBattleGroundMenuLoaded)
            {
                battlegroundScreen.Dispose();
                battlegroundScreen.UnloadContent();
                isBattleGroundMenuLoaded = false;
            }
        }
        // <------------------------------------------- End of screen management -------------------------------------------> //

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                SynchronizeWithVerticalRetrace = true,
                GraphicsProfile = GraphicsProfile.HiDef,
                PreferredBackBufferHeight = ControlVariables.PreferredBackBufferHeight,
                PreferredBackBufferWidth = ControlVariables.PreferredBackBufferWidth
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
        }

        public void AddComponent(IGameComponent component)
        {
            if (!Components.Contains(component))
            {
                Components.Add(component);
            }
        }

        public void RemoveComponent(IGameComponent component)
        {
            if (Components.Contains(component))
            {
                Components.Remove(component);
            }
        }

        protected override void Initialize()
        {
            battlegroundScreen = new Screens.BattlegroundScreen(this);
            gameTypeScreen = new Screens.GametypeScreen(this);
            menuScreen = new Screens.MenuScreen(this);
            AddComponent(screenManager);
            LoadMenuScreen();
   
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);  
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
