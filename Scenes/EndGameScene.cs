using bluewarp.UI;
using Microsoft.Xna.Framework;
using Nez;
using static bluewarp.GameConstants;

namespace bluewarp
{
    /// <summary>
    /// End game scene.
    /// </summary>
    internal class EndGameScene : BaseScene
    {
        private int _finalScore;
        private GameConstants.GameEndState _gameEndState;

        private EndUIManager _UIManager;

        /// <summary>
        /// Creates Eng game scene with end message and shows score.
        /// Creates EndUIManager.
        /// </summary>
        /// <param name="finalScore"></param>
        /// <param name="gameEndState"></param>
        public EndGameScene(int finalScore, GameConstants.GameEndState gameEndState) : base(true, true)
        {
            _finalScore = finalScore;
            _gameEndState = gameEndState;
            Debug.Log($"[End Game scene] final score: {finalScore}, state: {gameEndState}");
            
            Debug.Log($"[Initializing UI] final score: {_finalScore}, state: {_gameEndState}");
            _UIManager = new EndUIManager(this, _finalScore, _gameEndState);
            BGMusicManager.Play(GameConstants.BGM.BlueChill, volume: GameSettings.BGM.BGVolume);
        }

        public override void Initialize()
        {
            base.Initialize();
            
            SetDesignResolution(GameConstants.GameWidth, GameConstants.GameHeight, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(GameSettings.ScaledGameWidth, GameSettings.ScaledGameHeight);
            ClearColor = Color.Black;
            
        }

        public override void End()
        {
            _UIManager?.Dispose();
            base.End();
        }
    }
}
