using Nez;

namespace bluewarp
{
    public static class SceneManager
    {
        public static void LoadGameScene()
        {
            Core.StartSceneTransition(new FadeTransition( () => new RunGameScene()));
        }

        public static void LoadGameOver()
        {
            Core.StartSceneTransition(new FadeTransition( () => new EndGameScene()));
        }
    }
}
