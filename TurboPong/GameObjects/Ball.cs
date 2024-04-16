using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TurboPong.Globals;

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

        private Vector2 RandomizeDirection()
        {
            Random random = new Random();
            return new Vector2(random.Next(0, ControlVariables.PreferredBackBufferWidth),
                               random.Next(0, ControlVariables.PreferredBackBufferHeight));
        }

        public override void Initialize()
        {
            ballHeight = ballWidth = ControlVariables.BallSize;         
            positionX = (ControlVariables.PreferredBackBufferWidth / 2) - (ballWidth / 2);
            positionY = (ControlVariables.PreferredBackBufferHeight / 2) - (ballHeight / 2);
            properBall = new Rectangle(positionX, positionY, ballWidth, ballHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            whitePixel = game.Content.Load<Texture2D>("WhitePixel");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {      
            spriteBatch.Begin();
            spriteBatch.Draw(whitePixel, properBall, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
