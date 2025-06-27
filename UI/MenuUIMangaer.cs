using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace bluewarp.UI
{
    /// <summary>
    /// Menu UI manager.
    /// </summary>
    public class MenuUIMangaer : BaseUIManager
    {
        private Button _startButton;
        private Button _instructionButton;
        private Button _settingsButton;
        private Button _creditButton;
        private Button _exitButton;
        private Label _title;

        /// <summary>
        /// Creates Menu UI.
        /// </summary>
        /// <param name="scene">Scene to which to add UI</param>
        public MenuUIMangaer(Scene scene) : base(scene)
        {
            Initialize();
            Debug.Log($"[Constructed MenuUI] Scene:{scene}");
        }

        /// <summary>
        /// Aligns Table to center and pad.
        /// </summary>
        protected override void SetupTableAlignment()
        {
            Table.Center();
            Table.Pad(GameConstants.DefaultUIPadding);
        }

        /// <summary>
        /// Sets up Menu UI.
        /// </summary>
        protected override void SetupUI()
        {
            CreateTitle();
            NewEmptyLine();
            CreateButtons();
        }

        private void CreateTitle()
        {
            var titleStyle = new LabelStyle(Graphics.Instance.BitmapFont, Color.White)
            {
                FontScale = GameSettings.Scale * 2,
                Background = CreateBorderedBackground(Color.Black, Color.White, 2 * GameSettings.Scale)
            };

            _title = Table.Add(new Label("BLUEWARP", titleStyle)).GetElement<Label>();
            _title.SetAlignment(Align.Center);
            Debug.Log($"[Created Title]");
        }

        private void CreateButtons()
        {
            _startButton = Table.Add(new TextButton("Start Game", DefaultButtonStyle)).Pad(5, 0, 5, 0).GetElement<Button>();
            Table.Row();
            _instructionButton = Table.Add(new TextButton("How 2 play", DefaultButtonStyle)).Pad(5, 0, 5, 0).GetElement<Button>();
            Table.Row();
            _settingsButton = Table.Add(new TextButton("Settings", DefaultButtonStyle)).Pad(5, 0, 5, 0).GetElement<Button>();
            Table.Row();
            _creditButton = Table.Add(new TextButton("Credits", DefaultButtonStyle)).Pad(5, 0, 5, 0).GetElement<Button>();
            NewEmptyLine();

            _exitButton = Table.Add(new TextButton("Exit", DefaultButtonStyle)).GetElement<Button>();

            _startButton.OnClicked += OnStartButtonClicked;
            _instructionButton.OnClicked += OnH2PButtonClicked;
            _settingsButton.OnClicked += OnSettingsButtonClicked;
            _creditButton.OnClicked += OnCreditsButtonClicked;
            _exitButton.OnClicked += OnExitButtonClicked;
            Debug.Log($"[Created Buttons]");
        }

        private void OnStartButtonClicked(Button button)
        {
            SceneManager.LoadGameScene();
        }

        private void OnH2PButtonClicked(Button button)
        {
            SceneManager.LoadHow2Play();
        }

        private void OnSettingsButtonClicked(Button button)
        {
            SceneManager.LoadSettings();
        }

        private void OnCreditsButtonClicked(Button button)
        {
            SceneManager.LoadCredits();
        }

        private void OnExitButtonClicked(Button button)
        {
            Core.Exit();
        }

        public override void Dispose()
        {
            if (_startButton != null )
                _startButton.OnClicked -= OnStartButtonClicked;
            if (_instructionButton != null )
                _instructionButton.OnClicked -= OnH2PButtonClicked;
            if (_settingsButton != null )
                _settingsButton.OnClicked -= OnSettingsButtonClicked;
            if (_creditButton != null )
                _creditButton.OnClicked -= OnCreditsButtonClicked;
            if (_exitButton != null )
                _exitButton.OnClicked -= OnExitButtonClicked;

            base.Dispose();
        }
    }
}
