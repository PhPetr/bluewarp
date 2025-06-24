using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace bluewarp.UI
{
    public class SettingsUIManager : BaseUIManager
    {
        private Label _title;
        private Label _gameScaleLabel;
        private Label _sfxVolumeLabel;
        private Label _bgmVolumeLabel;
        private Label _difficultyLabel;
        private string _scaleText;
        private string _sfxVolumeText;
        private string _bgmVolumeText;
        private string _difficultyText;
        
        private Slider _gameScaleSlider;
        private Slider _sfxVolumeSlider;
        private Slider _bgmVolumeSlider;
        private Slider _difficultySlider;

        private Button _resetSettingsButton;
        private Button _menuButton;

        public SettingsUIManager(Scene scene) : base(scene)
        {
            Initialize();
            Debug.Log($"[COnstructed SettingsUI] Scene:{scene}");
        }

        protected override void SetupTableAlignment()
        {
            Table.Center();
            Table.Pad(20);
            //Table.DebugAll();
        }

        protected override void SetupUI()
        {
            CreateTitle();
            NewEmptyLine();
            CreateSlidersAndLabels();
            NewEmptyLine();
            CreateButtons();
        }

        private void CreateTitle()
        {
            _title = Table.Add(new Label("Settings", DefaultTitleStyle)).GetElement<Label>();
            _title.SetAlignment(Align.Center);
            Table.Row();
        }

        private void CreateSlidersAndLabels()
        {
            _scaleText = $"Game scale: {GameSettings.Scale}";
            _sfxVolumeText = $"SFX volume: {GameSettings.SFX.SFXMasterVolume:F1}";
            _bgmVolumeText = $"BGM volume: {GameSettings.BGM.BGMasterVolume:F1}";
            _difficultyText = $"Dificulty (HP mult): {GameSettings.Player.HealthMultiplier}";

            _gameScaleLabel = Table.Add(new Label(_scaleText, DefaultLabelStyle)).GetElement<Label>();
            Table.Row();
            _gameScaleSlider = new Slider(
                0, 
                GameConstants.MaxScale, 
                1, 
                false, 
                SliderStyle.Create(Color.AliceBlue, Color.Red));
            _gameScaleSlider.SetValue(GameSettings.Scale);
            Table.Add(_gameScaleSlider);
            Table.Row();

            NewEmptyLine();

            _bgmVolumeLabel = Table.Add(new Label(_bgmVolumeText, DefaultLabelStyle)).GetElement<Label>();
            Table.Row();
            _bgmVolumeSlider = new Slider(
                0.0f, 
                GameConstants.BGM.MaxBGMasterVolume, 
                0.1f, 
                false, 
                SliderStyle.Create(Color.AliceBlue, Color.Red));
            _bgmVolumeSlider.SetValue(GameSettings.BGM.BGMasterVolume);
            Table.Add(_bgmVolumeSlider);
            Table.Row();
            
            NewEmptyLine();
            
            _sfxVolumeLabel = Table.Add(new Label(_sfxVolumeText, DefaultLabelStyle)).GetElement<Label>();
            Table.Row();
            _sfxVolumeSlider = new Slider(
                0.0f, 
                GameConstants.SFX.MaxSFXMasterVolume, 
                0.1f, 
                false, 
                SliderStyle.Create(Color.AliceBlue, Color.Red));
            _sfxVolumeSlider.SetValue(GameSettings.SFX.SFXMasterVolume);
            Table.Add(_sfxVolumeSlider);
            Table.Row();

            NewEmptyLine();

            _difficultyLabel = Table.Add(new Label(_difficultyText, DefaultLabelStyle)).GetElement<Label>();
            Table.Row();
            _difficultySlider = new Slider(
                1,
                GameConstants.Player.MaxHealtMultiplier,
                1,
                false,
                SliderStyle.Create(Color.AliceBlue, Color.Red));
            _difficultySlider.SetValue(GameSettings.Player.HealthMultiplier);
            Table.Add(_difficultySlider);
            Table.Row();

            _gameScaleSlider.OnChanged += OnScaleSliderChanged;
            _bgmVolumeSlider.OnChanged += OnBGMVolumeSliderChanged;
            _sfxVolumeSlider.OnChanged += OnSFXVolumeSliderChanged;
            _difficultySlider.OnChanged += OnDifficultySliderChanged;
        }

        private void OnDifficultySliderChanged(float value)
        {
            int newMult = (int)Mathf.Round(value);
            _difficultyText = $"Dificulty (HP mult): {newMult}";
            GameSettings.Player.HealthMultiplier = newMult;
            if (_difficultyLabel != null )
                _difficultyLabel.SetText(_difficultyText);
        }

        private void OnScaleSliderChanged(float value)
        {
            int newScale = (int)Mathf.Round(value);
            _scaleText = $"Game scale: {newScale}";
            GameSettings.Scale = newScale;
            if (_gameScaleLabel != null)
                _gameScaleLabel.SetText(_scaleText);
        }

        private void OnBGMVolumeSliderChanged(float value)
        {
            GameSettings.BGM.SetBGMMasterVolume(value);
            _bgmVolumeText = $"BGM volume: {GameSettings.BGM.BGMasterVolume:F1}";
            if (_bgmVolumeLabel != null)
                _bgmVolumeLabel.SetText(_bgmVolumeText);
        }

        private void OnSFXVolumeSliderChanged(float value)
        {
            GameSettings.SFX.SetSFXMasterVolume(value);
            _sfxVolumeText = $"SFX volume: {GameSettings.SFX.SFXMasterVolume:F1}";
            if (_sfxVolumeLabel != null)
                _sfxVolumeLabel.SetText(_sfxVolumeText);
        }

        private void CreateButtons()
        {
            _resetSettingsButton = Table.Add(new TextButton("Reset settings", DefaultButtonStyle)).GetElement<Button>();
            NewEmptyLine();
            _menuButton = Table.Add(new TextButton("Apply & Back to Menu", DefaultButtonStyle)).GetElement<Button>();

            _resetSettingsButton.OnClicked += OnResetButtonClicked;
            _menuButton.OnClicked += OnBackButtonClicked;
            Table.Row();
        }

        private void OnResetButtonClicked(Button button)
        {
            _gameScaleSlider.SetValue(4);
            _bgmVolumeSlider.SetValue(1.0f);
            _sfxVolumeSlider.SetValue(1.0f);
            _difficultySlider.SetValue(1);
        }

        private void OnBackButtonClicked(Button button)
        {
            SceneManager.LoadMenu();
        }

        public override void Dispose()
        {
            if (_resetSettingsButton != null) 
                _resetSettingsButton.OnClicked -= OnResetButtonClicked;
            if (_menuButton != null)
                _menuButton.OnClicked -= OnBackButtonClicked;

            if (_sfxVolumeSlider != null)
                _sfxVolumeSlider.OnChanged -= OnSFXVolumeSliderChanged;
            if (_bgmVolumeSlider != null)
                _bgmVolumeSlider.OnChanged -= OnBGMVolumeSliderChanged;
            if (_gameScaleSlider != null)
                _gameScaleSlider.OnChanged -= OnScaleSliderChanged;
            if (_difficultySlider != null)
                _difficultySlider.OnChanged -= OnDifficultySliderChanged;

            base.Dispose();
        }
    }
}
