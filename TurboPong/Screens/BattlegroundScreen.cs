using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using TurboPong.Controller;

namespace TurboPong.Screens
{
    internal class BattlegroundScreen : GameScreen
    {
        private new Game1 game => (Game1)base.Game;
        private KeyboardState keyboardState;
        private KeyboardState previousKeyboardState;

        private Human humanPlayer1;
        private Human humanPlayer2;
        private GameObjects.Ball ball;

        public BattlegroundScreen(Game game) : base(game) { }

        public override void Initialize()
        {
            keyboardState = Keyboard.GetState();
            previousKeyboardState = keyboardState;

            humanPlayer1 = new Human(IPlayer.Position.Right, game);
            humanPlayer1.Initialize();

            humanPlayer2 = new Human(IPlayer.Position.Left, game);
            humanPlayer2.PlayerIndex = 2;
            humanPlayer2.Initialize();

            ball = new GameObjects.Ball(game);
            ball.SetPlayersToColide(humanPlayer2, humanPlayer1);
  
            base.Initialize();
        }

        public override void LoadContent()
        {
            game.AddComponent(ball);
            humanPlayer1.LoadContent();
            humanPlayer2.LoadContent();
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            game.RemoveComponent(ball);
            humanPlayer1.UnloadContent();
            humanPlayer2.UnloadContent();
            base.UnloadContent();
        }

        private bool WasKeyPressed(Keys key)
        {
            return (keyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key) ? true : false);
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (WasKeyPressed(Keys.Escape))
            {
                UnloadContent();
                game.LoadMenuScreen();          
            }

            if (WasKeyPressed(Keys.Space))
            {
                ball.RestartPosition();
            }

            previousKeyboardState = keyboardState; 
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            humanPlayer1.Update(gameTime);
            humanPlayer2.Update(gameTime);
        }
    }
}
