using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using TurboPong.GameObjects;

namespace TurboPong.Controller
{

    internal class Bot : IPlayer
    {
        private IPlayer.Position _playerPosition;
        private int _points;
        private new Game1 game;

        private Bat bat;

        public Vector2 BatPosition
        {
            get { return bat.BatPosition; }
        }

        private int _playerIndex = 2;

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
            get { return IPlayer.PlayerType.Bot; }
        }

        public Bot(IPlayer.Position position, Game game)
        {
            PlayerPosition = position;
            this.game = (Game1)game;
        }

        public void ResetPosition()
        {
            bat.ResetPosition();
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
            if (bat.BatPosition.Y > Ball.position.Y)
            {
                bat.Move(Bat.MoveDirection.Up, gameTime);
            }
            else if (bat.BatPosition.Y < Ball.position.Y)
            {
                bat.Move(Bat.MoveDirection.Down, gameTime);
            }
        }
    }
}
