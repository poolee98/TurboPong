using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System.Threading;
using TurboPong.Globals;

namespace TurboPong.Screens
{
    internal class SettingsScreen : GameScreen
    {
        private new Game1 game => (Game1)base.Game;

        private InterfaceObject MenuTitle;
        private InterfaceObject BackButton;
        private InterfaceObject Resolution;

        public SettingsScreen(Game game) : base(game) { }

        public override void Initialize()
        {
            // width, height, "is set?"
            var resolutions = new[]
            {
                (1024, 768, false),
                (1600, 800, true),
                (1920, 1080, false)
            };

            MenuTitle = new InterfaceObject(game)
            {
                InterfaceText = "Turbo Pong",
                InterfaceFontSize = InterfaceObject.FontSize.Giga,
                PositionY = ControlVariables.PreferredBackBufferWidth / 10
            };
            MenuTitle.PositionX = (ControlVariables.PreferredBackBufferWidth / 2) - ((int)MenuTitle.Size.Width / 2);
            MenuTitle.TextColor = Color.NavajoWhite;

            BackButton = new InterfaceObject(game)
            {
                InterfaceText = "Back",
                InterfaceFontSize = InterfaceObject.FontSize.Small,
                PositionY = ControlVariables.PreferredBackBufferWidth / 4
            };
            BackButton.PositionX = (ControlVariables.PreferredBackBufferWidth / 2) - ((int)BackButton.Size.Width / 2);
            BackButton.ShadowIfHoveredOver = true;
            BackButton.IsClickable = true;
            BackButton.OnClick += game.LoadMenuScreen;

            Resolution = new InterfaceObject(game)
            {
                InterfaceText = "Resolution  " + ControlVariables.PreferredBackBufferWidth + "x" + ControlVariables.PreferredBackBufferHeight,
                InterfaceFontSize = InterfaceObject.FontSize.Small,
                PositionY = ControlVariables.PreferredBackBufferWidth / 6
            };
            Resolution.PositionX = (ControlVariables.PreferredBackBufferWidth / 2) - ((int)Resolution.Size.Width / 2);
            Resolution.ShadowIfHoveredOver = true;
            Resolution.IsClickable = true;
            Resolution.OnClick += () =>
            {
                int i = 0;
                foreach (var resolution in resolutions)
                {
                    if (resolution.Item3 == false)
                    {
                        i++;
                    }
                    else
                    {
                        resolutions[i].Item3 = false;
                        if (i == 2)
                        {
                            i = 0;
                        } 
                        else
                        {
                            i = i + 1;
                        }
                        resolutions[i].Item3 = true;
                        break;
                    }
                }
                game.graphics.PreferredBackBufferWidth = ControlVariables.PreferredBackBufferWidth = resolutions[i].Item1;
                game.graphics.PreferredBackBufferHeight = ControlVariables.PreferredBackBufferHeight = resolutions[i].Item2;
                game.graphics.ApplyChanges();
                Resolution.InterfaceText = "Resolution  " + ControlVariables.PreferredBackBufferWidth + "x" + ControlVariables.PreferredBackBufferHeight;
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
            BackButton.Update(gameTime);
            MenuTitle.Update(gameTime);
            Resolution.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);
            game.spriteBatch.Begin();
            MenuTitle.Draw(gameTime);
            Resolution.Draw(gameTime);
            BackButton.Draw(gameTime);
            game.spriteBatch.End();
        }   
    }
}
