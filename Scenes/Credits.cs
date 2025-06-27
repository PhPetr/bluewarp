using bluewarp.UI;
using Microsoft.Xna.Framework;
using Nez;

namespace bluewarp.Scenes
{
    /// <summary>
    /// Credits scene.
    /// </summary>
    internal class Credits : BaseScene
    {
        private CreditsUIManager _UIManager;
        public Credits() : base(true, true) 
        { }

        /// <summary>
        /// Creates Credit scene and CreditsUIManager.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            SetDesignResolution(GameConstants.GameWidth, GameConstants.GameHeight, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(GameSettings.ScaledGameWidth, GameSettings.ScaledGameHeight);
            ClearColor = Color.Black;

            _UIManager = new CreditsUIManager(this);

            BGMusicManager.Play(GameConstants.BGM.BlueChill, volume: GameSettings.BGM.BGVolume);
        }
    }
}
