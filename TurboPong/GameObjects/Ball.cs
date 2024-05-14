using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using TurboPong.Controller;
using TurboPong.Globals;
using AsepriteDotNet.Aseprite;
using MonoGame.Aseprite;

namespace TurboPong.GameObjects
{
    internal class Ball : DrawableGameComponent
    {
        private new Game1 game => (Game1)base.Game;

        private Texture2D whitePixel;
        private Rectangle properBall;
        private SoundEffect hitSoundEffect;

        //private int positionX, position.Y;
        private int ballWidth, ballHeight;

        private IPlayer player1;
        private IPlayer player2;

        private Vector2 direction;
        public static Vector2 position = new Vector2();

        private bool wasShotLeft = true;

        private float ballSpeed = ControlVariables.BallDefaultSpeed;

        private AsepriteFile explosionFile;
        private SpriteSheet explosionSpriteSheet;
        private AnimatedSprite explosionSprite;
        private float explosionX, explosionY;
        private bool isExplosionPlaying = false;

        //private bool isWaiting = false;
        //private Delay Timer = new Delay(500.0);

        private InterfaceObject winnerCall;

        private bool isMatchWon = false;

        public Ball(Game game) : base(game) { }

        public void SetPlayersToColide(IPlayer playerToTheLeft, IPlayer playerToTheRight)
        {
            player1 = playerToTheLeft;
            player2 = playerToTheRight; 
        }

        private Vector2 RandomPointOnMap()
        {
            Random random = new Random();

            int x = wasShotLeft ? 0 : ControlVariables.PreferredBackBufferWidth;
            wasShotLeft = !wasShotLeft;

            int y = random.Next(2 * (ControlVariables.PreferredBackBufferHeight / 9), 7* (ControlVariables.PreferredBackBufferHeight / 9));

            return new Vector2(x, y);
        }

        public void RestartPosition(GameTime gameTime)
        {
            StartPosition();
            /*
            isWaiting = true;

            Timer.Wait(gameTime, () =>
            {
                StartPosition();
                isWaiting = false;
            });
            */
        }

        public void StartPosition()
        {
            player1.ResetPosition();
            player2.ResetPosition();
            position.X = (ControlVariables.PreferredBackBufferWidth / 2) - (ballWidth / 2);
            position.Y = (ControlVariables.PreferredBackBufferHeight / 2) - (ballHeight / 2);
            direction = RandomPointOnMap() - position;
            ballSpeed = ControlVariables.BallDefaultSpeed;
        }

        private void EndExplosion(AnimatedSprite sprite)
        {
            isExplosionPlaying = false;
            sprite.Stop();
            sprite.IsVisible = false;         
        }

        private void StartExplosion()
        {
            hitSoundEffect.Play();
            explosionX = position.X;
            explosionY = position.Y;
            explosionSprite.IsVisible = true;
            isExplosionPlaying = true;
            explosionSprite.Play(1);
        }

        public override void Initialize()
        {
            ballHeight = ballWidth = ControlVariables.BallSize;
            properBall = new Rectangle();
            properBall.Width = ballWidth;
            properBall.Height = ballHeight;

            winnerCall = new InterfaceObject(game)
            {
                InterfaceFontSize = InterfaceObject.FontSize.Giga,
                TextColor = Color.Yellow,
                PositionY = ControlVariables.PreferredBackBufferHeight / 3
            };
            winnerCall.PositionX = (ControlVariables.PreferredBackBufferWidth / 2) - (int)(winnerCall.Size.Width / 2);

            StartPosition();
            player2.Points = -1;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            whitePixel = game.Content.Load<Texture2D>("WhitePixel");
            hitSoundEffect = game.Content.Load<SoundEffect>("Hit");
            explosionFile = game.Content.Load<AsepriteFile>("Explosions");
            explosionSpriteSheet = explosionFile.CreateSpriteSheet(game.GraphicsDevice);
            explosionSprite = explosionSpriteSheet.CreateAnimatedSprite("Explosion");
            explosionSprite.IsVisible = true;
            explosionSprite.OnAnimationEnd = EndExplosion;

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            game.Content.UnloadAsset("WhitePixel");
            game.Content.UnloadAsset("Hit");
            game.Content.UnloadAsset("Explosions");
            base.UnloadContent();
        }

        private void RegisterHit()
        {
            hitSoundEffect.Play();
            direction.X = -direction.X;

            if (ballSpeed < 2.0f)
            {
                ballSpeed *= 1.1f;
            }
        }

        private void ShowWinner()
        {

        }

        public override void Update(GameTime gameTime)
        {
            explosionSprite.Update(gameTime);
            // Upper paddle hit
            if (properBall.Intersects(new Rectangle((int)player1.BatPosition.X, (int)player1.BatPosition.Y, ControlVariables.batWidth, ControlVariables.batHeight / 2)) ||
                properBall.Intersects(new Rectangle((int)player2.BatPosition.X, (int)player2.BatPosition.Y, ControlVariables.batWidth, ControlVariables.batHeight / 2)))
            {
                RegisterHit();
                if (direction.Y > ControlVariables.PreferredBackBufferHeight / 2)
                {
                    direction.Y = -direction.Y;
                }
            }
            // Lower paddle hit
            if (properBall.Intersects(new Rectangle((int)player1.BatPosition.X, (int)player1.BatPosition.Y + (ControlVariables.batHeight / 2), ControlVariables.batWidth, ControlVariables.batHeight / 2)) ||
                properBall.Intersects(new Rectangle((int)player2.BatPosition.X, (int)player2.BatPosition.Y + (ControlVariables.batHeight / 2), ControlVariables.batWidth, ControlVariables.batHeight / 2)))
            {
                RegisterHit();    
                if (direction.Y < ControlVariables.PreferredBackBufferHeight / 2)
                {
                    direction.Y = -direction.Y;
                }
            }

            // Bounce off top and bottom
            if (properBall.Y <= 0 || properBall.Y >= ControlVariables.PreferredBackBufferHeight - properBall.Height)
            {
                direction.Y = (-direction.Y);
            }

            if (direction != Vector2.Zero)
            {
                position += direction * ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (properBall.X <= 0)
            {
                StartExplosion();
                player2.Points++;

                if (player2.Points < 3)
                {
                    RestartPosition(gameTime);
                }
                else
                {
                    ControlVariables.isGamePaused = true;
                    isMatchWon = true;
                }
                
            }

            if (properBall.X >= ControlVariables.PreferredBackBufferWidth)
            {
                StartExplosion();
                player1.Points++;

                if (player1.Points < 3)
                {
                    RestartPosition(gameTime);
                }
                else
                {
                    ControlVariables.isGamePaused = true;
                    isMatchWon = true;
                }
                
            }

            if (ControlVariables.isGamePaused)
            {
                properBall.X = (ControlVariables.PreferredBackBufferWidth / 2) - (ballWidth / 2);
                properBall.Y = (ControlVariables.PreferredBackBufferHeight / 2) - (ballHeight / 2);
            } 
            else
            {
                properBall.X = (int)position.X;
                properBall.Y = (int)position.Y;
            }

            winnerCall.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            if (isExplosionPlaying)
            {
                game.spriteBatch.Draw(explosionSprite, new Vector2(explosionX - (explosionSprite.Width / 2), explosionY - (explosionSprite.Height / 2)));
            }
            game.spriteBatch.Draw(whitePixel, properBall, Color.White);

            if (isMatchWon)
            {
                string playerWon = player1.Points > player2.Points ? "Player One" : "Player Two";
                winnerCall.InterfaceText = playerWon + " won!";
                winnerCall.Draw(gameTime);
            }

            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
