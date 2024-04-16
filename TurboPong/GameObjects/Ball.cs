using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TurboPong.GameObjects
{
    internal class Ball : DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;

        private Texture2D whitePixel;
        private Rectangle properBall;

        private int positionX, positionY;
        private int ballWidth, ballHeight;

        public Ball(Game game) : base(game)
        {
            this.game = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        private void RandomizeDirection()
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            ballHeight = ballWidth = Globals.BallSize;         
            positionX = (Globals.PreferredBackBufferWidth / 2) - (ballWidth / 2);
            positionY = (Globals.PreferredBackBufferHeight / 2) - (ballHeight / 2);
            properBall = new Rectangle(positionX, positionY, ballWidth, ballHeight);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            whitePixel = game.Content.Load<Texture2D>("WhitePixel");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(whitePixel, properBall, Color.White);
            spriteBatch.End();
        }
    }
}
