using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace TurboPong.Screens
{
    internal class BattlegroundScreen : GameScreen
    {
        private Game game;
        private KeyboardState keyboardState;

        private GameObjects.Bat player1;
        private GameObjects.Bat player2;
        private GameObjects.Ball ball;


        public BattlegroundScreen(Game game) : base(game)
        {
            this.game = game;
        }

        public override void Initialize()
        {
            base.Initialize();

            player1 = new GameObjects.Bat(game);
            game.Components.Add(player1);
            player1.SetPosition(GameObjects.Bat.Position.Right);

            player2 = new GameObjects.Bat(game); ;
            game.Components.Add(player2);
            player2.SetPosition(GameObjects.Bat.Position.Left);

            ball = new GameObjects.Ball(game);
            game.Components.Add(ball);
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
                Globals.LoadMenuScreen(game);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
