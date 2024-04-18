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

        public override void Draw(GameTime gameTime)
        {
          
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
