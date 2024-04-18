using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TurboPong.GameObjects;

namespace TurboPong.Controller
{
    internal class Human : IPlayer
    {
        private IPlayer.Position _playerPosition;
        private int _points;
        private new Game1 game;

        private Bat bat;

        public Vector2 BatPosition
        {
            get { return bat.BatPosition; }
        }
        

        private Keys keyUp = Keys.Up;
        private Keys keyDown = Keys.Down;

        private int _playerIndex = 1;

        public int PlayerIndex
        {
            get
            {
                return _playerIndex;
            }
            set
            {
                if (value == 2)
                {
                    _playerIndex = value;
                }
                else
                {
                    _playerIndex = 1;
                }
            }
        }
   
        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }
        public IPlayer.Position PlayerPosition
        {
            get { return _playerPosition; }
            set { _playerPosition = value; }
        }
        public IPlayer.PlayerType playerType
        {
            get { return IPlayer.PlayerType.Human; }
        }

        public Human(IPlayer.Position position, Game game)
        {
            PlayerPosition = position;
            this.game = (Game1)game;
        }

        public void Initialize()
        {
            bat = new Bat(game);
            if (PlayerPosition == IPlayer.Position.Left)
            {
                bat.SetPosition(Bat.Position.Left);
            }
            else
            {
                bat.SetPosition(Bat.Position.Right);
            }

            if (_playerIndex == 2)
            {
                keyUp = Keys.W;
                keyDown = Keys.S;
            } 
        }

        public void LoadContent()
        {
            game.AddComponent(bat);
        }

        public void UnloadContent()
        {
            game.RemoveComponent(bat);
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(keyUp))
            {
                bat.Move(Bat.MoveDirection.Up, gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(keyDown))
            {
                bat.Move(Bat.MoveDirection.Down, gameTime);
            }
        }
    }
}
