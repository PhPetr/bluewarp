using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace bluewarp.UI
{
    public abstract class BaseUIManager
    {
        /// <summary>
        /// Scene to attach UI to
        /// </summary>
        protected Scene Scene;
        protected Entity UIEntity;
        protected UICanvas UICanvas;
        protected Table Table;
        protected LabelStyle DefaultLabelStyle;
        protected LabelStyle DefaultBorderedLabelStyle;
        protected LabelStyle DefaultTitleStyle;
        protected TextButtonStyle DefaultButtonStyle;
        protected int RenderLayer = bluewarp.RenderLayer.DefaultUIRenderLayer;

        public BaseUIManager(Scene scene)
        {
            Scene = scene;
            DefaultLabelStyle = new LabelStyle(Graphics.Instance.BitmapFont, Color.White)
            {
                FontScale = GameSettings.Scale
            };

            DefaultBorderedLabelStyle = new LabelStyle(Graphics.Instance.BitmapFont, Color.White)
            {
                FontScale = GameSettings.Scale,
                Background = CreateBorderedBackground(Color.Black, Color.White, 1 * GameSettings.Scale)
            };

            DefaultTitleStyle = new LabelStyle(Graphics.Instance.BitmapFont, Color.White)
            {
                FontScale = GameSettings.Scale * 1.5f,
                Background = CreateBorderedBackground(Color.Black, Color.White, 1 * GameSettings.Scale)
            };

            DefaultButtonStyle = CreateButtonStyleWithBG(
                bgColor: Color.DarkBlue,
                hoverColor: Color.Blue,
                textColor: Color.White
                );
        }

        /// <summary>
        /// Initializes UI. Must be called first in UI manager
        /// </summary>
        protected virtual void Initialize()
        {
            CreateBaseUI();
            SetupUI();
        }
        
        private void CreateBaseUI()
        {
            UIEntity = Scene.CreateEntity("UI");

            UICanvas = UIEntity.AddComponent(new UICanvas());
            UICanvas.IsFullScreen = true;
            UICanvas.RenderLayer = RenderLayer;

            Table = UICanvas.Stage.AddElement(new Table());
            Table.SetFillParent(true);
            SetupTableAlignment();
            //Table.DebugAll();
        }

        /// <summary>
        /// Child must implement to set up their UI
        /// </summary>
        protected abstract void SetupUI();
        /// <summary>
        /// Child must specify alignment
        /// </summary>
        protected abstract void SetupTableAlignment();

        protected void NewEmptyLine()
        {
            Table.Row();
            Table.Add(new Label(" ", DefaultLabelStyle));
            Table.Row();
        }

        protected Nez.UI.IDrawable CreateSolidBG(Color bgColor)
        {
            var bgDrawable = new PrimitiveDrawable(bgColor);
            bgDrawable.MinWidth = bgDrawable.MinHeight = 0;
            return bgDrawable;
        }

        protected Nez.UI.IDrawable CreateButtonBG(Color color)
        {
            var drawable = new PrimitiveDrawable(color);
            drawable.MinHeight = drawable.MinWidth = 0;
            return drawable;
        }

        protected Nez.UI.IDrawable CreateBorderedBackground(Color bgColor, Color borderColor, int borderWidth)
        {
            return new BorderDrawable(bgColor, borderColor, borderWidth);
        }

        protected TextButtonStyle CreateButtonStyleWithBG(Color bgColor, Color hoverColor, Color textColor)
        {
            return new TextButtonStyle()
            {
                Font = Graphics.Instance.BitmapFont,
                FontColor = textColor,
                FontScale = GameSettings.Scale,
                Up = CreateButtonBG(bgColor),
                Over= CreateButtonBG(hoverColor),
                Down = CreateButtonBG(hoverColor * 0.8f)
            };

        }

        public virtual void Dispose()
        {
            UIEntity?.Destroy();
        }
    }
}
