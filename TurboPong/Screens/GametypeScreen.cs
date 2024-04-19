using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using System;
using TurboPong.Globals;

namespace TurboPong.Screens
{
    internal class GametypeScreen : GameScreen
    {
        private new Game1 game => (Game1)base.Game;
        private InterfaceObject MenuTitle;
        private InterfaceObject againstPlayer;
        private InterfaceObject againstBot;
        private InterfaceObject BackButton;

        public GametypeScreen(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            MenuTitle = new InterfaceObject(game)
            {
                InterfaceText = "Turbo Pong",
                InterfaceFontSize = InterfaceObject.FontSize.Giga,
                PositionY = ControlVariables.PreferredBackBufferWidth / 10
            };
            MenuTitle.PositionX = (ControlVariables.PreferredBackBufferWidth / 2) - ((int)MenuTitle.Size.Width / 2);
            MenuTitle.TextColor = Color.NavajoWhite;

            againstPlayer = new InterfaceObject(game)
            {
                InterfaceText = "Play against other player",
                InterfaceFontSize = InterfaceObject.FontSize.Small,
                PositionY = ControlVariables.PreferredBackBufferWidth / 6
            };
            againstPlayer.PositionX = (ControlVariables.PreferredBackBufferWidth / 2) - ((int)againstPlayer.Size.Width / 2);
            againstPlayer.IsClickable = true;
            againstPlayer.ShadowIfHoveredOver = true;
            againstPlayer.OnClick += () =>
            {
                game.LoadGameScreen();
            };

            againstBot = new InterfaceObject(game)
            {
                InterfaceText = "Play against other Bot",
                InterfaceFontSize = InterfaceObject.FontSize.Small,
                PositionY = ControlVariables.PreferredBackBufferWidth / 5
            };
            againstBot.PositionX = (ControlVariables.PreferredBackBufferWidth / 2) - ((int)againstBot.Size.Width / 2);
            againstBot.IsClickable = true;
            againstBot.ShadowIfHoveredOver = true;
            againstBot.OnClick += () =>
            {
                againstBot.TextColor = Color.Red;
            };

            BackButton = new InterfaceObject(game)
            {
                InterfaceText = "Back",
                InterfaceFontSize = InterfaceObject.FontSize.Small,
                PositionY = ControlVariables.PreferredBackBufferWidth / 4
            };
            BackButton.PositionX = (ControlVariables.PreferredBackBufferWidth / 2) - ((int)BackButton.Size.Width / 2);
            BackButton.ShadowIfHoveredOver = true;
            BackButton.IsClickable = true;
            BackButton.OnClick += () =>
            {
                game.LoadMenuScreen();
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
            againstPlayer.Update(gameTime);
            againstBot.Update(gameTime);
            BackButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);
            game.spriteBatch.Begin();
                MenuTitle.Draw(gameTime);
                againstBot.Draw(gameTime);
                againstPlayer.Draw(gameTime);
                BackButton.Draw(gameTime);  
            game.spriteBatch.End();
        }

    }
}
