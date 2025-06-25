using Nez;
using Microsoft.Xna.Framework;
using bluewarp.UI;

namespace bluewarp.Scenes
{
    internal class SettingsScene : BaseScene
    {
        private SettingsUIManager _UIManager;

        public SettingsScene() : base(true,true) 
        { }

        public override void Initialize()
        {
            base.Initialize();

            SetDesignResolution(GameConstants.GameWidth, GameConstants.GameHeight, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(GameSettings.ScaledGameWidth, GameSettings.ScaledGameHeight);
            ClearColor = Color.Black;

            _UIManager = new SettingsUIManager(this);

            BGMusicManager.Play(GameConstants.BGM.BlueChill, volume: GameSettings.BGM.BGVolume);
        }

        public override void End()
        {
            _UIManager?.Dispose();
            base.End();
        }
    }
}
