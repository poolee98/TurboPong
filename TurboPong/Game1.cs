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

        private Screens.BattlegroundScreen battlegroundScreen;
        private Screens.MenuScreen menuScreen;
        private Screens.GametypeScreen gameTypeScreen;

        public void LoadGameScreen()
        {
            screenManager.LoadScreen(battlegroundScreen, new ExpandTransition(GraphicsDevice, Color.Black));
        }

        public void LoadChooseGameType()
        {
            screenManager.LoadScreen(gameTypeScreen, new FadeTransition(GraphicsDevice, Color.SlateGray));
        }

        public void LoadMenuScreen()
        {
            screenManager.LoadScreen(menuScreen, new FadeTransition(GraphicsDevice, Color.Black));
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
