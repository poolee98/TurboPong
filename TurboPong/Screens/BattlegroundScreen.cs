using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using TurboPong.Controller;
using TurboPong.Globals;

namespace TurboPong.Screens
{
    internal class BattlegroundScreen : GameScreen
    {
        private Game game;
        private KeyboardState keyboardState;
        private KeyboardState previousKeyboardState;

        private Human humanPlayer1;
        private Human humanPlayer2;
        private GameObjects.Ball ball;

        private ContentManager battlegroundContentManager;

        public BattlegroundScreen(Game game) : base(game)
        {
            this.game = game;
            battlegroundContentManager = new ContentManager(Content.ServiceProvider, Content.RootDirectory);
            this.game.Content = battlegroundContentManager;
        }

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
            game.Components.Add(ball);

            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        private bool WasKeyPressed(Keys key)
        {
            if (keyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            humanPlayer1.Update(gameTime);
            humanPlayer2.Update(gameTime);

            if (WasKeyPressed(Keys.Escape))
            {
                SceneManagement.LoadMenuScreen(game);
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
        }

        public override void Dispose()
        {
            battlegroundContentManager.Unload();
            battlegroundContentManager.Dispose();
            base.Dispose();
        }
    }
}
