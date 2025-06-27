using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace bluewarp
{
    /// <summary>
    /// Base Scene to create real scenes. Makes adding scenes easier.
    /// </summary>
    public abstract class BaseScene : Scene, IFinalRenderDelegate
    {
        public const int ScreenSpaceRenderLayer = 999;
        public UICanvas Canvas;

        ScreenSpaceRenderer _screenSpaceRenderer;
        static bool _needsFullRenderSizeForUi;
        public RenderLayerRenderer _layerRenderer = new RenderLayerRenderer(1, 1);

        /// <summary>
        /// Base scene constructor. Must be called.
        /// </summary>
        /// <param name="addExcludeRenderer"></param>
        /// <param name="needsFullRenderSizeForUi"></param>
        public BaseScene(bool addExcludeRenderer = true, bool needsFullRenderSizeForUi = false)
        {
            _needsFullRenderSizeForUi = needsFullRenderSizeForUi;

            // setup one renderer in screen space for the UI and then (optionally) another renderer to render everything else
            if (needsFullRenderSizeForUi)
            {
                // dont actually add the renderer since we will manually call it later
                _screenSpaceRenderer = new ScreenSpaceRenderer(100, ScreenSpaceRenderLayer);
                _screenSpaceRenderer.ShouldDebugRender = false;
                FinalRenderDelegate = this;
            }
            else
            {
                AddRenderer(new ScreenSpaceRenderer(100, ScreenSpaceRenderLayer));
                AddRenderer(_layerRenderer);
            }

            if (addExcludeRenderer)
                AddRenderer(new RenderLayerExcludeRenderer(0, ScreenSpaceRenderLayer));

            // create our canvas and put it on the screen space render layer
            Canvas = CreateEntity("ui").AddComponent(new UICanvas());
            Canvas.IsFullScreen = true;
            Canvas.RenderLayer = ScreenSpaceRenderLayer;
        }

        #region IFinalRenderDelegate

        private Scene _scene;

        public void OnAddedToScene(Scene scene) => _scene = scene;

        public void OnSceneBackBufferSizeChanged(int newWidth, int newHeight) => _screenSpaceRenderer.OnSceneBackBufferSizeChanged(newWidth, newHeight);

        public void HandleFinalRender(RenderTarget2D finalRenderTarget, Color letterboxColor, RenderTarget2D source,
                                      Rectangle finalRenderDestinationRect, SamplerState samplerState)
        {
            Core.GraphicsDevice.SetRenderTarget(null);
            Core.GraphicsDevice.Clear(letterboxColor);
            Graphics.Instance.Batcher.Begin(BlendState.Opaque, samplerState, DepthStencilState.None, RasterizerState.CullNone, null);
            Graphics.Instance.Batcher.Draw(source, finalRenderDestinationRect, Color.White);
            Graphics.Instance.Batcher.End();

            _screenSpaceRenderer.Render(_scene);
        }

        #endregion
    }
}
