using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace TurboPong.Screens
{
    internal class MenuScreen : GameScreen
    {      
        private SpriteBatch spriteBatch;
        private Game game;

        private InterfaceObject MenuTitle;

        public MenuScreen(Game game) : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.game = game;
        }

        public override void Initialize()
        {
            base.Initialize();

            MenuTitle = new InterfaceObject(game);
            MenuTitle.InterfaceText = "Turbo Pong";
            MenuTitle.InterfaceFontSize = InterfaceObject.FontSize.Giga;
            MenuTitle.PositionX = (Globals.PreferredBackBufferWidth / 2) - (MenuTitle.Width / 2);
            game.Components.Add(MenuTitle);
        }

        public override void LoadContent()
        {
            base.LoadContent();

        }

        public override void Draw(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) 
            {
                Globals.LoadGameScreen(game);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                game.Exit();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
