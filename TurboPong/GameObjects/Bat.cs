using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.CompilerServices;

using TurboPong.Globals;

namespace TurboPong.GameObjects
{
    internal class Bat : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Rectangle rectangle;
        private Texture2D whitePixel;

        private int batWidth = ControlVariables.batWidth;
        private int batHeight = ControlVariables.batHeight;
        private double positionY;

        private Game game;

        public double MovementSpeed = 0.3;

        public enum MoveDirection
        {
            Up,
            Down
        }

        public enum Position
        {
            Left,
            Right
        }

        public void SetPosition(Position position)
        {
            rectangle.X = (position == Position.Left) ? 10 : (ControlVariables.PreferredBackBufferWidth - 30);
        }

        public Bat(Game game) : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.game = game;

        }

        public override void Initialize()
        {
            base.Initialize();

            positionY = (ControlVariables.PreferredBackBufferHeight / 2) - (batHeight / 2);
            rectangle = new Rectangle(rectangle.X, (int)positionY, batWidth, batHeight);
        }

        public void Move(MoveDirection moveDirection, GameTime gameTime)
        {
            if (moveDirection == MoveDirection.Up)
            {
                if (positionY > 20)
                {
                    positionY -= MovementSpeed * gameTime.ElapsedGameTime.TotalMilliseconds;
                }            
            }
            else
            {
                if ((positionY + batHeight) < (ControlVariables.PreferredBackBufferHeight - 20))
                {
                    positionY += MovementSpeed * gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            whitePixel = game.Content.Load<Texture2D>("WhitePixel");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            rectangle.Y = (int)positionY;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(whitePixel, rectangle, Color.Turquoise);
            spriteBatch.End();
        }
    }
}
