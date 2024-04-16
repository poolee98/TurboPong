using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboPong
{
    internal static class Globals
    {
        // <------------------------------------------- Global control variables -------------------------------------------> //
        public static int PreferredBackBufferWidth = 1280;
        public static int PreferredBackBufferHeight = 720;
        public static int BallSize = 20;
        public static int batWidth = 25;
        public static int batHeight = 100;

        // <------------------------------------------- Screen management -------------------------------------------> //
        public static ScreenManager screenManager = new ScreenManager();

        private static bool LoadMenuWithTransition = false;
        private static Screens.BattlegroundScreen battlegroundScreen;
        private static Screens.MenuScreen menuScreen;

        public static void LoadGameScreen(Game game)
        {
            menuScreen.Dispose();
            battlegroundScreen = new Screens.BattlegroundScreen(game);
            screenManager.LoadScreen(battlegroundScreen, new ExpandTransition(game.GraphicsDevice, Color.Black));
        }

        public static void LoadMenuScreen(Game game)
        {
            menuScreen = new Screens.MenuScreen(game);

            if (LoadMenuWithTransition)
            {
                battlegroundScreen.Dispose();
                screenManager.LoadScreen(menuScreen, new FadeTransition(game.GraphicsDevice, Color.Black));
            }
            else
            {
                screenManager.LoadScreen(menuScreen);
                LoadMenuWithTransition = true;
            }
        }
    }
}
