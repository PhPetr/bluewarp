using bluewarp.Scenes;
using Nez;

namespace bluewarp
{
    /// <summary>
    /// Game itself.
    /// </summary>
    public class Game1 : Core
    {
        /// <summary>
        /// Initializes Nez.Core, thus starting game.
        /// Also loads BGM.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            LoadBGMusic();

            Window.AllowUserResizing = true;
            Core.DebugRenderEnabled = false;
            
            Scene = new MenuScene();
        }

        private void LoadBGMusic()
        {
            BGMusicManager.LoadSong(GameConstants.BGM.BlueChill, GameConstants.BGM.BlueChillPath);
            BGMusicManager.LoadSong(GameConstants.BGM.BlueTension, GameConstants.BGM.BlueTensionPath);
        }
    }
}
