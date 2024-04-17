﻿using Microsoft.Xna.Framework;

namespace TurboPong.Controller
{
    interface IPlayer
    {
        public int Points { get; set; } 
        public Position PlayerPosition { get; set; }
        public PlayerType playerType { get; }

        public Vector2 BatPosition { get; }
        
        public enum Position
        {
            Left,
            Right
        }

        public enum PlayerType
        {
            Human,
            Bot
        }

        public void Initialize();
        public void Update(GameTime gameTime);

    }
}
