using Microsoft.Xna.Framework;
using Nez;
using Nez.UI;

namespace bluewarp.UI
{
    /// <summary>
    /// Base for creating individual UIs.
    /// </summary>
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
        protected LabelStyle SmallLabelStyle;

        protected TextButtonStyle DefaultButtonStyle;

        protected Label TitleLabel;
        protected Button MenuButton;
        
        protected int RenderLayer = bluewarp.RenderLayer.DefaultUIRenderLayer;

        /// <summary>
        /// Creates default styles.
        /// </summary>
        /// <param name="scene">Scene to which to add UI</param>
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

            SmallLabelStyle = new LabelStyle(Graphics.Instance.BitmapFont, Color.White)
            {
                FontScale = (GameSettings.Scale / 2)
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
        
        /// <summary>
        /// Creates UIEntity, UICanvas and Table for UI.
        /// </summary>
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
        /// Child must implement to set up their UI.
        /// </summary>
        protected abstract void SetupUI();
        /// <summary>
        /// Child must specify Table alignment.
        /// </summary>
        protected abstract void SetupTableAlignment();

        /// <summary>
        /// Helper for adding empty line.
        /// </summary>
        protected void NewEmptyLine()
        {
            Table.Row();
            Table.Add(new Label(" ", DefaultLabelStyle));
            Table.Row();
        }

        /// <summary>
        /// Helper to create scene title with DefaultTitleStyle
        /// </summary>
        /// <param name="title">Name of scene</param>
        protected void CreateTitleLabel(string title)
        {
            TitleLabel = Table.Add(new Label(title, DefaultTitleStyle)).GetElement<Label>();
            TitleLabel.SetAlignment(Align.Center);
            Table.Row();
        }

        /// <summary>
        /// Helper to create back button to Menu scene.
        /// </summary>
        /// <param name="backToMenuMessage">Text for back button</param>
        /// <param name="defaultPad">If true pads top and bottom of button</param>
        protected virtual void CreateMenuButton(string backToMenuMessage, bool defaultPad = false)
        {
            MenuButton = Table.Add(new TextButton(backToMenuMessage, DefaultButtonStyle)).GetElement<Button>();
            if (defaultPad) MenuButton.Pad(5, 0, 5, 0); 
            MenuButton.OnClicked += OnBackButtonClicked;
            Table.Row();
        }

        private void OnBackButtonClicked(Button button)
        {
            SceneManager.LoadMenu();
        }

        /// <summary>
        /// Creates solid background for UI element.
        /// </summary>
        /// <param name="bgColor">Background color</param>
        /// <returns>IDrawable background</returns>
        protected Nez.UI.IDrawable CreateSolidBG(Color bgColor)
        {
            var bgDrawable = new PrimitiveDrawable(bgColor);
            bgDrawable.MinWidth = bgDrawable.MinHeight = 0;
            return bgDrawable;
        }

        /// <summary>
        /// Creates Button background.
        /// </summary>
        /// <param name="color">Button background color</param>
        /// <returns>IDrawable background</returns>
        protected Nez.UI.IDrawable CreateButtonBG(Color color)
        {
            var drawable = new PrimitiveDrawable(color);
            drawable.MinHeight = drawable.MinWidth = 0;
            return drawable;
        }

        /// <summary>
        /// Returns bordered background around UI element.
        /// </summary>
        /// <param name="bgColor">Background color</param>
        /// <param name="borderColor">Border color</param>
        /// <param name="borderWidth">Border width in px</param>
        /// <returns>IDrawable background</returns>
        protected Nez.UI.IDrawable CreateBorderedBackground(Color bgColor, Color borderColor, int borderWidth)
        {
            return new BorderDrawable(bgColor, borderColor, borderWidth);
        }

        /// <summary>
        /// Creates Button style with responsive background.
        /// </summary>
        /// <param name="bgColor">Background color</param>
        /// <param name="hoverColor">Hover color</param>
        /// <param name="textColor">Text color</param>
        /// <returns></returns>
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

        /// <summary>
        /// Disposes UIEntity and MenuButton if created.
        /// </summary>
        public virtual void Dispose()
        {
            if (MenuButton != null)
                MenuButton.OnClicked -= OnBackButtonClicked;
            UIEntity?.Destroy();
        }
    }
}
