using Nez;
using Microsoft.Xna.Framework;
using bluewarp.UI;

namespace bluewarp.Scenes
{
    /// <summary>
    /// How to play scene.
    /// </summary>
    internal class How2PlayScene : BaseScene
    {
        private InstructionUIManager _UIManager;
        public How2PlayScene() : base(true, true) { }

        /// <summary>
        /// Creates How to play scene and InstructionUIManager.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            SetDesignResolution(GameConstants.GameWidth, GameConstants.GameHeight, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(GameSettings.ScaledGameWidth, GameSettings.ScaledGameHeight);
            ClearColor = Color.Black;

            _UIManager = new InstructionUIManager(this);
            BGMusicManager.Play(GameConstants.BGM.BlueChill, volume: GameSettings.BGM.BGVolume);
        }

        public override void End()
        {
            _UIManager?.Dispose();
            base.End();
        }

    }
}
