using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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

        private Song MenuTheme;

        // <------------------------------------------- Screen management -------------------------------------------> //
        public ScreenManager screenManager = new ScreenManager();

        private Screens.BattlegroundScreen battlegroundScreen;
        private Screens.MenuScreen menuScreen;
        private Screens.GametypeScreen gameTypeScreen;
        private Screens.SettingsScreen settingsScreen;

        private bool isGameLoaded = true;

        public void LoadGameScreen(bool againstAI)
        {
            MediaPlayer.Stop();
            battlegroundScreen.isPlayerTwoBot = againstAI;
            isGameLoaded = true;
            screenManager.LoadScreen(battlegroundScreen, new ExpandTransition(GraphicsDevice, Color.Black));
        }

        public void LoadChooseGameType()
        {
            screenManager.LoadScreen(gameTypeScreen, new FadeTransition(GraphicsDevice, Color.SlateGray));
        }

        public void LoadSettingsScreen()
        {
            screenManager.LoadScreen(settingsScreen, new FadeTransition(GraphicsDevice, Color.SlateGray));
        }

        public void LoadMenuScreen()
        {
            if (isGameLoaded)
            {
                if (ControlVariables.isMusicPlaying)
                {
                    MediaPlayer.Play(MenuTheme);
                }
                isGameLoaded = false;
                screenManager.LoadScreen(menuScreen, new FadeTransition(GraphicsDevice, Color.Black));
            }
            else
            {
                screenManager.LoadScreen(menuScreen, new FadeTransition(GraphicsDevice, Color.SlateGray));
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
            settingsScreen = new Screens.SettingsScreen(this);
            AddComponent(screenManager);
   
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MenuTheme = Content.Load<Song>("MenuTheme");

            LoadMenuScreen();
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
