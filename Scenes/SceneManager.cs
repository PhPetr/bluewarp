using bluewarp.Scenes;
using Nez;

namespace bluewarp
{
    public static class SceneManager
    {
        public static void LoadMenu()
        {
            Core.StartSceneTransition(new FadeTransition(() => new MenuScene()));
        }

        public static void LoadHow2Play()
        {
            Core.StartSceneTransition(new FadeTransition(() => new How2PlayScene()));
        }

        public static void LoadSettings()
        {
            Core.StartSceneTransition(new FadeTransition(() => new SettingsScene()));
        }

        public static void LoadCredits()
        {
            Core.StartSceneTransition(new FadeTransition(() => new Credits()));
        }

        public static void LoadGameScene()
        {
            Core.StartSceneTransition(new FadeTransition( () => new RunGameScene()));
        }

        public static void LoadGameOver(int finalScore, GameConstants.GameEndState gameEndState)
        {
            Debug.Log($"[Loading game over] final score: {finalScore}, state: {gameEndState}");
            Core.StartSceneTransition(new FadeTransition( () => new EndGameScene(finalScore, gameEndState)));
        }
    }
}
