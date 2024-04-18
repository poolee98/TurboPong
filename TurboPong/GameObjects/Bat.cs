using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TurboPong.Controller;
using TurboPong.Globals;

namespace TurboPong.GameObjects
{
    internal class Bat : DrawableGameComponent
    {
        private new Game1 game => (Game1)base.Game;
        private Rectangle rectangle;
        private Texture2D whitePixel;

        private int batWidth = ControlVariables.batWidth;
        private int batHeight = ControlVariables.batHeight;
        private float positionY;

        public float MovementSpeed = 0.3f;

        public Vector2 BatPosition
        {
            get { return new Vector2(rectangle.X, positionY); }
        }

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

        public Bat(Game game) : base(game) { }

        public override void Initialize()
        {
            positionY = (ControlVariables.PreferredBackBufferHeight / 2) - (batHeight / 2);
            rectangle = new Rectangle(rectangle.X, (int)positionY, batWidth, batHeight);
            base.Initialize();
        }

        public void Move(MoveDirection moveDirection, GameTime gameTime)
        {
            if (moveDirection == MoveDirection.Up)
            {
                if (positionY > 20)
                {
                    positionY -= MovementSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }            
            }
            else
            {
                if ((positionY + batHeight) < (ControlVariables.PreferredBackBufferHeight - 20))
                {
                    positionY += MovementSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
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
            rectangle.Y = (int)positionY;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin(SpriteSortMode.BackToFront);
            game.spriteBatch.Draw(whitePixel, new Rectangle(rectangle.X, rectangle.Y, batWidth, batHeight), Color.Turquoise);
            game.spriteBatch.End();        
            base.Draw(gameTime);
        }
    }
}
