using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TurboPong.Controller;
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

        private IPlayer player1;
        private IPlayer player2;

        private Vector2 direction;
        private Vector2 position = new Vector2();

        public Ball(Game game) : base(game)
        {
            this.game = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public void SetPlayersToColide(IPlayer playerToTheLeft, IPlayer playerToTheRight)
        {
            this.player1 = playerToTheLeft;
            this.player2 = playerToTheRight; 
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

            // Bounce off top and bottom
            if (properBall.Y <= 0 || properBall.Y >= ControlVariables.PreferredBackBufferHeight - properBall.Height)
            {
                direction.Y = (-direction.Y);
            }

            if (direction != Vector2.Zero)
            {
                position += direction * ControlVariables.BallDefaultSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // TODO: FIX COLLISIONS
            // Bounce off player's bat's 
            // Player 1
            if (properBall.X + properBall.Width <= player1.BatPosition.X + ControlVariables.batWidth)
            {
                if (properBall.Y >= player1.BatPosition.Y && properBall.Y <= player1.BatPosition.Y + ControlVariables.batHeight)
                {
                    Console.WriteLine("Collision with player to the left");
                    direction.X = (-direction.X);
                    direction.Y = (-direction.Y);
                }
            }
            // Player2
            if (properBall.X > player2.BatPosition.X)
            {
                if (properBall.Y >= player2.BatPosition.Y && properBall.Y <= player2.BatPosition.Y + ControlVariables.batHeight)
                {
                    Console.WriteLine("Collision with player to the right!");
                    direction.X = (-direction.X);
                    direction.Y = (-direction.Y);
                }
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
