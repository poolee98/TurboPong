using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using TurboPong.Globals;

namespace TurboPong.Screens
{
    internal class MenuScreen : GameScreen
    {      
        private new Game1 game => (Game1)base.Game;

        private InterfaceObject MenuTitle;
        private InterfaceObject StartButton;
        private InterfaceObject SettingsButton;
        private InterfaceObject ExitButton;

        private bool IsMenuScreenLoaded = true;

        public MenuScreen(Game game) : base(game) { }

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
                game.LoadChooseGameType();
            };
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
            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void UnloadContent()
        {

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            MenuTitle.Update(gameTime);
            ExitButton.Update(gameTime);
            SettingsButton.Update(gameTime);
            StartButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.SlateGray);
            game.spriteBatch.Begin();
                MenuTitle.Draw(gameTime);
                ExitButton.Draw(gameTime);
                SettingsButton.Draw(gameTime);
                StartButton.Draw(gameTime);
            game.spriteBatch.End();
        }

    }
}
