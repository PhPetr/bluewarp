using Nez;
using Nez.UI;
using Microsoft.Xna.Framework.Input;
using bluewarp.UI;

namespace bluewarp
{
    /// <summary>
    /// Game UI manager.
    /// </summary>
    public class GameUIManager : BaseUIManager
    {
        private int _score;
        private Label _scoreTitleLabel;
        private Label _scoreValueLabel;

        private int _playerHP;
        private Label _playerHPTitleLabel;
        private Label _playerHPValueLabel;

        private Button _restartGameRunButton;
        //private Button _menuButton;

        /// <summary>
        /// Create Game UI.
        /// </summary>
        /// <param name="scene">Scene to which to add UI</param>
        public GameUIManager(Scene scene) : base(scene) 
        {
            _score = 0;
            _playerHP = GameSettings.Player.ShipMaxHealth;
            Initialize();
        }

        /// <summary>
        /// Sets up game UI.
        /// </summary>
        protected override void SetupUI()
        {
            ScoreUISetup();
            NewEmptyLine();
            HPUISetup();
            NewEmptyLine();
            SetupButtons();
        }

        /// <summary>
        /// Align Table to Top right and pad.
        /// </summary>
        protected override void SetupTableAlignment()
        {
            Table.Top().Right();
            Table.Pad(GameConstants.GameUIPadding);
        }

        private void SetupButtons()
        {
            _restartGameRunButton = Table.Add(new TextButton("Restart", DefaultButtonStyle)).GetElement<Button>();
            NewEmptyLine();
            CreateMenuButton("Menu");            
            _restartGameRunButton.OnClicked += OnRestartClicked;
        }

        private void OnRestartClicked(Button button)
        {
            SceneManager.LoadGameScene();
        }

        public override void Dispose()
        {
            if (_restartGameRunButton != null)
                _restartGameRunButton.OnClicked -= OnRestartClicked;

            base.Dispose();
        }

        #region HP UI
        private void HPUISetup()
        {
            _playerHPTitleLabel = Table.Add(new Label("HP:", DefaultLabelStyle)).GetElement<Label>();
            _playerHPTitleLabel.SetAlignment(Align.Right);

            Table.Row();

            _playerHPValueLabel = Table.Add(new Label(_playerHP.ToString(), DefaultLabelStyle)).GetElement<Label>();
            _playerHPValueLabel.SetAlignment(Align.Right);
        }

        /// <summary>
        /// Updates Player HP to newValue
        /// </summary>
        /// <param name="newValue">New player HP</param>
        public void UpdatePlayerHP(int newValue)
        {
            _playerHP = newValue;
            if (_playerHPValueLabel != null)
            {
                _playerHPValueLabel.SetText(_playerHP.ToString());
            }
        }

        /// <summary>
        /// Adds points to player HP.
        /// </summary>
        /// <param name="points">Points to add</param>
        public void AddToPlayerHP(int points)
        {
            UpdatePlayerHP(_playerHP +  points);
        }

        /// <summary>
        /// Returns player HP.
        /// </summary>
        /// <returns>Player HP</returns>
        public int GetPlayerHP()
        {
            return _playerHP;
        }
        #endregion

        #region ScoreUI
        private void ScoreUISetup()
        {
            _scoreTitleLabel = Table.Add(new Label($"Score:", DefaultLabelStyle)).GetElement<Label>();
            _scoreTitleLabel.SetAlignment(Align.Right);

            Table.Row();

            _scoreValueLabel = Table.Add(new Label(_score.ToString(), DefaultLabelStyle)).GetElement<Label>();
            _scoreValueLabel.SetAlignment(Align.Right);
        }

        /// <summary>
        /// Updates player score to new score.
        /// </summary>
        /// <param name="newScore">New player score</param>
        public void UpdateScore(int newScore)
        {
            _score = newScore;
            if (_scoreValueLabel != null)
            {
                _scoreValueLabel.SetText(_score.ToString());
            }
        }

        /// <summary>
        /// Add points to player score.
        /// </summary>
        /// <param name="points">Points to add</param>
        public void AddToScore(int points)
        {
            UpdateScore(_score + points);
        }

        /// <summary>
        /// Returns player score.
        /// </summary>
        /// <returns>Player score</returns>
        public int GetScore()
        {
            return _score;
        }
        #endregion
    }
}
