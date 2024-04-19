using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TurboPong.Controller;
using TurboPong.Globals;

namespace TurboPong.GameObjects
{
    internal class Ball : DrawableGameComponent
    {
        private new Game1 game => (Game1)base.Game;

        private Texture2D whitePixel;
        private Rectangle properBall;

        //private int positionX, position.Y;
        private int ballWidth, ballHeight;

        private IPlayer player1;
        private IPlayer player2;

        private Vector2 direction;
        private Vector2 position = new Vector2();

        public Ball(Game game) : base(game) { }

        public void SetPlayersToColide(IPlayer playerToTheLeft, IPlayer playerToTheRight)
        {
            player1 = playerToTheLeft;
            player2 = playerToTheRight; 
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

        protected override void UnloadContent()
        {
            game.Content.UnloadAsset("WhitePixel");
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            properBall.X = (int)position.X;
            properBall.Y = (int)position.Y;

            // Bounce off top and bottom
            if (properBall.Y <= 0 || properBall.Y >= ControlVariables.PreferredBackBufferHeight - properBall.Height)
            {
                direction.Y = (-direction.Y);
            }

            if (direction != Vector2.Zero)
            {
                position += direction * ControlVariables.BallDefaultSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (properBall.Intersects(new Rectangle((int)player1.BatPosition.X, (int)player1.BatPosition.Y, ControlVariables.batWidth, ControlVariables.batHeight)))
            {
                Console.WriteLine("Ball contact player 1!");
                properBall.X = 500;
                direction.X = -direction.X;
            }

            if (properBall.Intersects(new Rectangle((int)player2.BatPosition.X, (int)player2.BatPosition.Y, ControlVariables.batWidth, ControlVariables.batHeight)))
            {
                Console.WriteLine("Ball contact player 2!");
                direction.X = -direction.X;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(whitePixel, properBall, Color.White);
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
