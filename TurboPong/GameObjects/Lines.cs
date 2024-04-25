using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TurboPong.Globals;

namespace TurboPong.GameObjects
{
    internal class Lines : DrawableGameComponent
    {
        private new Game1 game => (Game1)base.Game;
        private Texture2D whitePixel;

        private Rectangle body;
        private Vector2 position;

        private int linesOnScreen;

        public Lines(Game game) : base(game) { }

        public override void Initialize()
        {
            body.Width = 5;
            body.Height = 10;
            body.X = ControlVariables.PreferredBackBufferWidth / 2 - 3;
            body.Y = 1;
            linesOnScreen = ControlVariables.PreferredBackBufferHeight / body.Height;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            for (int i = 0; i <= linesOnScreen; i++)
            {
                if (i%2 == 0)
                {
                    game.spriteBatch.Draw(whitePixel, body, Color.White);
                    body.Y += (3 * body.Height);
                }
            }
            body.Y = 1;
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
