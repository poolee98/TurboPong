using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using TurboPong.Globals;

namespace TurboPong.Screens
{
    internal class BattlegroundScreen : GameScreen
    {
        private Game game;
        private KeyboardState keyboardState;

        private GameObjects.Bat player1;
        private GameObjects.Bat player2;
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
            player1 = new GameObjects.Bat(game);
            game.Components.Add(player1);
            player1.SetPosition(GameObjects.Bat.Position.Right);

            player2 = new GameObjects.Bat(game);
            game.Components.Add(player2);
            player2.SetPosition(GameObjects.Bat.Position.Left);

            ball = new GameObjects.Ball(game);
            game.Components.Add(ball);

            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                player1.Move(GameObjects.Bat.MoveDirection.Down, gameTime);
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                player1.Move(GameObjects.Bat.MoveDirection.Up, gameTime);
            }

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                SceneManagement.LoadMenuScreen(game);
            }
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
