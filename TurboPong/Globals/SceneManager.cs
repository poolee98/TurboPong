using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Screens;

namespace TurboPong.Globals
{
    internal static class SceneManagement
    {
        // <------------------------------------------- Screen management -------------------------------------------> //
        public static ScreenManager screenManager = new ScreenManager();

        private static bool LoadMenuWithTransition = false;
        private static Screens.BattlegroundScreen battlegroundScreen;
        private static Screens.MenuScreen menuScreen;
        private static bool isMenuScreenLoaded = false;
        private static bool isBattleGroundMenuLoaded = false;

        public static void LoadGameScreen(Game game)
        {
            battlegroundScreen = new Screens.BattlegroundScreen(game);
            screenManager.LoadScreen(battlegroundScreen, new ExpandTransition(game.GraphicsDevice, Color.Black));
            isBattleGroundMenuLoaded = true;

            if (isMenuScreenLoaded)
            {
                menuScreen.Dispose();
                menuScreen.UnloadContent();
                isMenuScreenLoaded = false;
            }
        }

        public static void LoadMenuScreen(Game game)
        {
            menuScreen = new Screens.MenuScreen(game);

            if (LoadMenuWithTransition)
            {                
                screenManager.LoadScreen(menuScreen, new FadeTransition(game.GraphicsDevice, Color.Black));
            }
            else
            {
                screenManager.LoadScreen(menuScreen);
                LoadMenuWithTransition = true;
            }
            isMenuScreenLoaded = true;

            if (isBattleGroundMenuLoaded)
            {
                battlegroundScreen.Dispose();
                battlegroundScreen.UnloadContent();
                isBattleGroundMenuLoaded = false;
            }
        }
    }
}
