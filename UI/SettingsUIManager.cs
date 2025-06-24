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
        private string _scaleText;
        private string _sfxVolumeText;
        private string _bgmVolumeText;
        
        private Slider _gameScaleSlider;
        private Slider _sfxVolumeSlider;
        private Slider _bgmVolumeSlider;

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
            _scaleText = $"Game scale: {GameConstants.Scale}";
            _sfxVolumeText = $"SFX volume: {GameConstants.SFX.SFXMasterVolume}";
            _bgmVolumeText = $"BGM volume: {GameConstants.BGM.BGMasterVolume}";

            _gameScaleLabel = Table.Add(new Label(_scaleText, DefaultLabelStyle)).GetElement<Label>();
            Table.Row();
            _gameScaleSlider = new Slider(
                0, 
                GameConstants.MaxScale, 
                1, 
                false, 
                SliderStyle.Create(Color.AliceBlue, Color.Red));
            _gameScaleSlider.SetValue(GameConstants.Scale);
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
            _bgmVolumeSlider.SetValue(GameConstants.BGM.BGMasterVolume);
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
            _sfxVolumeSlider.SetValue(GameConstants.SFX.SFXMasterVolume);
            Table.Add(_sfxVolumeSlider);
            Table.Row();

            _gameScaleSlider.OnChanged += OnScaleSliderChanged;
            _bgmVolumeSlider.OnChanged += OnBGMVolumeSliderChanged;
            _sfxVolumeSlider.OnChanged += OnSFXVolumeSliderChanged;
        }

        private void OnScaleSliderChanged(float value)
        {
            int newScale = (int)Mathf.Round(value);
            _scaleText = $"Game scale: {newScale}";
            GameConstants.Scale = newScale;
            if (_gameScaleLabel != null)
                _gameScaleLabel.SetText(_scaleText);
        }

        private void OnBGMVolumeSliderChanged(float value)
        {
            GameConstants.BGM.SetBGMMasterVolume(value);
            _bgmVolumeText = $"BGM volume: {GameConstants.BGM.BGMasterVolume:F1}";
            if (_bgmVolumeLabel != null)
                _bgmVolumeLabel.SetText(_bgmVolumeText);
        }

        private void OnSFXVolumeSliderChanged(float value)
        {
            GameConstants.SFX.SetSFXMasterVolume(value);
            _sfxVolumeText = $"SFX volume: {GameConstants.SFX.SFXMasterVolume:F1}";
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

            base.Dispose();
        }
    }
}
