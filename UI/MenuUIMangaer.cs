using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace bluewarp.UI
{
    public class MenuUIMangaer : BaseUIManager
    {
        private Button _startButton;
        private Button _instructionButton;
        private Button _creditButton;
        private Button _exitButton;
        private Label _title;

        public MenuUIMangaer(Scene scene) : base(scene)
        {
            Initialize();
            Debug.Log($"[Constructed MenuUI] Scene:{scene}");
        }

        protected override void SetupTableAlignment()
        {
            Table.Center();
            Table.Pad(20);
        }

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
                FontScale = GameConstants.Scale * 2,
                Background = CreateBorderedBackground(Color.Black, Color.AntiqueWhite, 5)
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
            _creditButton = Table.Add(new TextButton("Credits", DefaultButtonStyle)).GetElement<Button>();
            NewEmptyLine();

            _exitButton = Table.Add(new TextButton("Exit", DefaultButtonStyle)).GetElement<Button>();

            _startButton.OnClicked += OnStartButtonClicked;
            _instructionButton.OnClicked += OnH2PButtonClicked;
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
            if (_creditButton != null )
                _creditButton.OnClicked -= OnCreditsButtonClicked;
            if (_exitButton != null )
                _exitButton.OnClicked -= OnExitButtonClicked;

            base.Dispose();
        }
    }
}
