using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TurboPong.Globals;

namespace TurboPong.GameObjects
{
    internal class Lines : DrawableGameComponent
    {
        private new Game1 game => (Game1)base.Game;
        private Texture2D whitePixel;

        private Rectangle body;
        private Vector2 position;

        List<Rectangle> lines = new List<Rectangle>();

        public Lines(Game game) : base(game) { }

        public override void Initialize()
        {
            body.Width = 5;
            body.Height = 10;
            body.X = ControlVariables.PreferredBackBufferWidth / 2 - 3;
            body.Y = 0;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(whitePixel, body, Color.White);
            game.spriteBatch.End();

            base.Draw(gameTime);
        }


        protected override void LoadContent()
        {
            whitePixel = game.Content.Load<Texture2D>("WhitePixel");
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            game.Content.UnloadAsset("WhitePixel");
            base.UnloadContent();
        }
    }
}
