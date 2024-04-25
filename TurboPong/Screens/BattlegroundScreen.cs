using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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

        private Human playerOne;
        private IPlayer playerTwo;
        public bool isPlayerTwoBot;
        private Ball ball;

        private Song battleTheme;

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

            playerOne = new Human(IPlayer.Position.Right, game);
            playerOne.Initialize();

            if (isPlayerTwoBot)
            {
                playerTwo = new Bot(IPlayer.Position.Left, game);
            }
            else
            {
                playerTwo = new Human(IPlayer.Position.Left, game);
            }
            
            playerTwo.PlayerIndex = 2;
            playerTwo.Initialize();

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


            ball = new Ball(game);
            ball.SetPlayersToColide(playerTwo, playerOne);
  
            base.Initialize();
        }

        public override void LoadContent()
        {
            battleTheme = game.Content.Load<Song>("BattleTheme");
            MediaPlayer.Volume = 0.6f;
            if (ControlVariables.isMusicPlaying)
            {
                MediaPlayer.Play(battleTheme);
            }
            
            game.AddComponent(ball);
            playerOne.LoadContent();
            playerTwo.LoadContent();
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            game.RemoveComponent(ball);
            game.RemoveComponent(lines);
            playerOne.UnloadContent();
            playerTwo.UnloadContent();
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

            playerOneScore.InterfaceText = "Score " + playerOne.Points;
            playerTwoScore.InterfaceText = "Score " + playerTwo.Points;

            playerOne.Update(gameTime);
            playerTwo.Update(gameTime);
            playerOneScore.Update(gameTime);
            playerTwoScore.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            game.spriteBatch.Begin(SpriteSortMode.FrontToBack);
            playerOneScore.Draw(gameTime);
            playerTwoScore.Draw(gameTime);
            game.spriteBatch.End();
        }
    }
}
