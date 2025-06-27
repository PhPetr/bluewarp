using bluewarp.Scenes;
using Nez;

namespace bluewarp
{
    /// <summary>
    /// Handles Scene changes.
    /// </summary>
    public static class SceneManager
    {
        /// <summary>
        /// Fade transition into Menu scene.
        /// </summary>
        public static void LoadMenu()
        {
            Core.StartSceneTransition(new FadeTransition(() => new MenuScene()));
        }

        /// <summary>
        /// Fade transition into How to play scene.
        /// </summary>
        public static void LoadHow2Play()
        {
            Core.StartSceneTransition(new FadeTransition(() => new How2PlayScene()));
        }

        /// <summary>
        /// Fade transition into Settings scene.
        /// </summary>
        public static void LoadSettings()
        {
            Core.StartSceneTransition(new FadeTransition(() => new SettingsScene()));
        }

        /// <summary>
        /// Fade transition into Credits scene.
        /// </summary>
        public static void LoadCredits()
        {
            Core.StartSceneTransition(new FadeTransition(() => new Credits()));
        }

        /// <summary>
        /// Fade transition into Run game scene.
        /// </summary>
        public static void LoadGameScene()
        {
            Core.StartSceneTransition(new FadeTransition( () => new RunGameScene()));
        }

        /// <summary>
        /// Fade transition into End game scene with end state.
        /// </summary>
        /// <param name="finalScore">Final player score</param>
        /// <param name="gameEndState">End game state</param>
        public static void LoadGameOver(int finalScore, GameConstants.GameEndState gameEndState)
        {
            Debug.Log($"[Loading game over] final score: {finalScore}, state: {gameEndState}");
            Core.StartSceneTransition(new FadeTransition( () => new EndGameScene(finalScore, gameEndState)));
        }
    }
}
