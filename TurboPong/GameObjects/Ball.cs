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

        //private int positionX, position.Y;
        private int ballWidth, ballHeight;

        private Vector2 direction;
        private Vector2 position = new Vector2();

        public Ball(Game game) : base(game)
        {
            this.game = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        private Vector2 RandomPointOnMap()
        {
            Random random = new Random();
            return new Vector2(random.Next(0, ControlVariables.PreferredBackBufferWidth),
                               random.Next(0, ControlVariables.PreferredBackBufferHeight));
        }

        public void RestartPosition()
        {
            position.X = (ControlVariables.PreferredBackBufferWidth / 2) - (ballWidth / 2);
            position.Y = (ControlVariables.PreferredBackBufferHeight / 2) - (ballHeight / 2);
            direction = RandomPointOnMap() - position;
        }

        public override void Initialize()
        {
            ballHeight = ballWidth = ControlVariables.BallSize;
            properBall = new Rectangle();
            properBall.Width = ballWidth;
            properBall.Height = ballHeight;
            RestartPosition();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            whitePixel = game.Content.Load<Texture2D>("WhitePixel");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            properBall.X = (int)position.X;
            properBall.Y = (int)position.Y;


            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                position += direction * gameTime.ElapsedGameTime.TotalMilliseconds;
            }

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
