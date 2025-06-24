using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace bluewarp.UI
{
    public class EndUIManager : BaseUIManager
    {
        private int _finalScore;
        private GameConstants.GameEndState _gameEndState;

        private Button _playAgainButton;
        private Button _menuButton;
        private Button _exitButton;

        private Label _endMessage;
        private Label _finalScoreLabel;

        public EndUIManager(Scene scene, int finalScore, GameConstants.GameEndState gameEndState) : base(scene)
        {
            _finalScore = finalScore;
            _gameEndState = gameEndState;
            Initialize();
            Debug.Log($"[End UI manager initialized]");
            Debug.Log($"[End UI manager] score: {finalScore}");
            Debug.Log($"[End UI manager] state: {gameEndState}");
        }

        protected override void SetupTableAlignment()
        {
            Table.Center();
            Table.Pad(20);
        }

        protected override void SetupUI()
        {
            CreateEndMessage();
            Table.Row();
            CreateScoreLabel();
            NewEmptyLine();
            CreateButtons();
        }

        private void CreateEndMessage()
        {
            string message;
            Color messageColor;

            switch(_gameEndState)
            {
                case GameConstants.GameEndState.Victory:
                    message = "VICTORY ACHIEVED";
                    messageColor = Color.Gold;
                    break;
                case GameConstants.GameEndState.Defeat:
                    message = "YOU DIED";
                    messageColor = Color.DarkRed;
                    break;
                default:
                    message = "Something is wrong";
                    messageColor = Color.White;
                    break;
            }

            var messageStyle = new LabelStyle(Graphics.Instance.BitmapFont, messageColor)
            {
                FontScale = GameSettings.Scale * 2
            };

            _endMessage = Table.Add(new Label(message, messageStyle)).GetElement<Label>();
            _endMessage.SetAlignment(Align.Center);
        }

        private void CreateScoreLabel()
        {
            string text = $"Final score: {_finalScore}";
            _finalScoreLabel = Table.Add(new Label(text, DefaultLabelStyle)).Pad(5,0,5,0).GetElement<Label>();
            _finalScoreLabel.SetAlignment(Align.Center);
        }

        private void CreateButtons()
        {
            _playAgainButton = Table.Add(new TextButton("Play again", DefaultButtonStyle)).Pad(5, 0, 5, 0).GetElement<Button>();
            Table.Row();
            _menuButton = Table.Add(new TextButton("Menu", DefaultButtonStyle)).Pad(5, 0, 5, 0).GetElement<Button>();
            NewEmptyLine();
            _exitButton = Table.Add(new TextButton("Exit", DefaultButtonStyle)).GetElement<Button>();

            _playAgainButton.OnClicked += OnPlayAgainButtonClicked;
            _menuButton.OnClicked += OnMenuButtonClicked;
            _exitButton.OnClicked += OnExitButtonClicked;
        }

        private void OnPlayAgainButtonClicked(Button button)
        {
            SceneManager.LoadGameScene();
        }

        private void OnMenuButtonClicked(Button button)
        {
            SceneManager.LoadMenu();
        }

        private void OnExitButtonClicked(Button button)
        {
            Core.Exit();
        }

        public override void Dispose()
        {
            if (_playAgainButton != null) 
                _playAgainButton.OnClicked -= OnPlayAgainButtonClicked;
            if (_menuButton != null )
                _menuButton.OnClicked -= OnMenuButtonClicked;
            if (_exitButton != null)
                _exitButton.OnClicked -= OnExitButtonClicked;

            base.Dispose();
        }
    }
}
