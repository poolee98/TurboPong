using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using TurboPong.Globals;
using static TurboPong.GameObjects.Bat;

namespace TurboPong.Screens
{
    internal class MenuScreen : GameScreen
    {      
        private SpriteBatch spriteBatch;
        private Game game;
        private ContentManager menuScreenContentManager;

        private InterfaceObject MenuTitle;
        private InterfaceObject StartButton;
        private InterfaceObject SettingsButton;
        private InterfaceObject ExitButton;

        public MenuScreen(Game game) : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            menuScreenContentManager = new ContentManager(Content.ServiceProvider, Content.RootDirectory);
            this.game = game;
            this.game.Content = menuScreenContentManager;
        }

        public override void Initialize()
        {
            // <--------------------------------- Menu title ---------------------------------> //
            MenuTitle = new InterfaceObject(game)
            {
                InterfaceText = "Turbo Pong",
                InterfaceFontSize = InterfaceObject.FontSize.Giga,
                PositionY = ControlVariables.PreferredBackBufferWidth / 10
            };
            MenuTitle.PositionX = (ControlVariables.PreferredBackBufferWidth / 2) - ((int)MenuTitle.Size.Width / 2);
            MenuTitle.TextColor = Color.NavajoWhite;
            game.Components.Add(MenuTitle);
            // <--------------------------------- Start Button ---------------------------------> //
            StartButton = new InterfaceObject(game)
            {
                InterfaceText = "Start game",
                InterfaceFontSize = InterfaceObject.FontSize.Medium,
                PositionY = ControlVariables.PreferredBackBufferWidth / 6
            };
            StartButton.PositionX = (ControlVariables.PreferredBackBufferWidth / 2) - ((int)StartButton.Size.Width / 2);
            StartButton.IsClickable = true;
            StartButton.ShadowIfHoveredOver = true;
            StartButton.OnClick += () =>
            {
                SceneManagement.LoadGameScreen(game);
            };
            game.Components.Add(StartButton);
            // <--------------------------------- Settings button ---------------------------------> //
            SettingsButton = new InterfaceObject(game)
            {
                InterfaceText = "Settings",
                InterfaceFontSize = InterfaceObject.FontSize.Small,
                PositionY = ControlVariables.PreferredBackBufferWidth / 5
            };
            SettingsButton.PositionX = (ControlVariables.PreferredBackBufferWidth / 2) - ((int)SettingsButton.Size.Width / 2);
            SettingsButton.ShadowIfHoveredOver = true;
            SettingsButton.IsClickable = true;
            SettingsButton.OnClick += () =>
            {
                SettingsButton.TextColor = Color.Red;
            };
            game.Components.Add(SettingsButton);
            // <--------------------------------- Exit Button ---------------------------------> //
            ExitButton = new InterfaceObject(game)
            {
                InterfaceText = "Exit",
                InterfaceFontSize = InterfaceObject.FontSize.Small,
                PositionY = ControlVariables.PreferredBackBufferWidth / 4
            };
            ExitButton.PositionX = (ControlVariables.PreferredBackBufferWidth / 2) - ((int)ExitButton.Size.Width / 2);
            ExitButton.ShadowIfHoveredOver = true;
            ExitButton.IsClickable = true;
            ExitButton.OnClick += () => 
            { 
                game.Exit();
            };
            game.Components.Add(ExitButton);

            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Dispose()
        {
            menuScreenContentManager.Unload();
            menuScreenContentManager.Dispose();
            base.Dispose();
        }
    }
}
