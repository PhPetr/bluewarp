using Nez;
using Microsoft.Xna.Framework;

namespace bluewarp
{
    internal class EndGameScene : BaseScene
    {
        public override void Initialize()
        {
            base.Initialize();

            SetDesignResolution(GameConstants.GameWidth, GameConstants.GameHeight, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(GameConstants.ScaledGameWidth, GameConstants.ScaledGameHeight);
            ClearColor = Color.Blue;

        }
    }
}
