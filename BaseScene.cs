using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Nez.UI;
using Microsoft.Xna.Framework.Graphics;
using Nez.Tweens;
using System.Linq;
//using Nez.ImGuiTools;
using Nez.Console;
using Nez;

namespace bluewarp
{
    public abstract class BaseScene : Scene, IFinalRenderDelegate
    {
        public const int ScreenSpaceRenderLayer = 999;
        public UICanvas Canvas;

        ScreenSpaceRenderer _screenSpaceRenderer;
        static bool _needsFullRenderSizeForUi;
        public RenderLayerRenderer _layerRenderer = new RenderLayerRenderer(1, 1);

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

        IEnumerable<Type> GetTypesWithBaseSceneAttribute()
        {
            var assembly = typeof(BaseScene).Assembly;
            var scenes = assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(BaseSceneAttribute), true).Length > 0)
                .OrderBy(t =>
                    ((BaseSceneAttribute)t.GetCustomAttributes(typeof(BaseSceneAttribute), true)[0]).Order);
            foreach (var s in scenes)
                yield return s;
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

    [AttributeUsage(AttributeTargets.Class)]
    public class BaseSceneAttribute : Attribute
    {
        public string ButtonName;
        public int Order;
        public string InstructionText;

        public BaseSceneAttribute(string buttonName, int order, string instructionText = null)
        {
            ButtonName = buttonName;
            Order = order;
            InstructionText = instructionText;
        }
    }
}
