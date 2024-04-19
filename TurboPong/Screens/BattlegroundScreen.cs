using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System.Threading;
using TurboPong.Controller;
using TurboPong.GameObjects;
using TurboPong.Globals;

namespace TurboPong.Screens
{
    internal class BattlegroundScreen : GameScreen
    {
        private new Game1 game => (Game1)base.Game;
        private KeyboardState keyboardState;
        private KeyboardState previousKeyboardState;

        private Human humanPlayerOne;
        private Human humanPlayerTwo;
        private Ball ball;

        private InterfaceObject playerOneScore;
        private InterfaceObject playerTwoScore;

        private Lines lines;

        public BattlegroundScreen(Game game) : base(game) { }

        public override void Initialize()
        {
            keyboardState = Keyboard.GetState();
            previousKeyboardState = keyboardState;

            lines = new Lines(game);
            game.AddComponent(lines);

            humanPlayerOne = new Human(IPlayer.Position.Right, game);
            humanPlayerOne.Initialize();

            humanPlayerTwo = new Human(IPlayer.Position.Left, game);
            humanPlayerTwo.PlayerIndex = 2;
            humanPlayerTwo.Initialize();

            playerOneScore = new InterfaceObject(game)
            {
                InterfaceFontSize = InterfaceObject.FontSize.Small,
                TextColor = Color.White,
                PositionY = 5,
            };
            playerOneScore.PositionX = ControlVariables.PreferredBackBufferWidth - (int)(playerOneScore.Size.Width / 1.5f);

            playerTwoScore = new InterfaceObject(game)
            {
                InterfaceFontSize = InterfaceObject.FontSize.Small,
                TextColor = Color.White,
                PositionY = 5,
            };
            playerTwoScore.PositionX = 10;


            ball = new GameObjects.Ball(game);
            ball.SetPlayersToColide(humanPlayerTwo, humanPlayerOne);
  
            base.Initialize();
        }

        public override void LoadContent()
        {
            game.AddComponent(ball);
            humanPlayerOne.LoadContent();
            humanPlayerTwo.LoadContent();
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            game.RemoveComponent(ball);
            game.RemoveComponent(lines);
            humanPlayerOne.UnloadContent();
            humanPlayerTwo.UnloadContent();
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

            playerOneScore.InterfaceText = "Score " + humanPlayerOne.Points;
            playerTwoScore.InterfaceText = "Score " + humanPlayerTwo.Points;

            humanPlayerOne.Update(gameTime);
            humanPlayerTwo.Update(gameTime);
            playerOneScore.Update(gameTime);
            playerTwoScore.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            game.spriteBatch.Begin();
            playerOneScore.Draw(gameTime);
            playerTwoScore.Draw(gameTime);
            game.spriteBatch.End();
        }
    }
}
