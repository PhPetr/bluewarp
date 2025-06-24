using Nez;
using Microsoft.Xna.Framework;
using bluewarp.UI;

namespace bluewarp.Scenes
{
    internal class Credits : BaseScene
    {
        private CreditsUIManager _UIManager;
        public Credits() : base(true, true) 
        { }

        public override void Initialize()
        {
            base.Initialize();
            SetDesignResolution(GameConstants.GameWidth, GameConstants.GameHeight, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(GameConstants.ScaledGameWidth, GameConstants.ScaledGameHeight);
            ClearColor = Color.Black;

            _UIManager = new CreditsUIManager(this);

            BGMusicManager.Play(GameConstants.BGM.BlueChill, volume: GameConstants.BGM.BGVolume);
        }
    }
}
