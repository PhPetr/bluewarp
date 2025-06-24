using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;
using bluewarp.UI;

namespace bluewarp
{
    public class GameUIManager : BaseUIManager
    {
        private int _score;
        private Label _scoreTitleLabel;
        private Label _scoreValueLabel;

        private int _playerHP;
        private Label _playerHPTitleLabel;
        private Label _playerHPValueLabel;

        public GameUIManager(Scene scene) : base(scene) 
        {
            _score = 0;
            _playerHP = GameSettings.Player.ShipMaxHealth;
            Initialize();
        }

        protected override void SetupUI()
        {
            ScoreUISetup();
            NewEmptyLine();
            HPUISetup();
        }

        protected override void SetupTableAlignment()
        {
            Table.Top().Right();
            Table.Pad(10);
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

        public void UpdatePlayerHP(int newValue)
        {
            _playerHP = newValue;
            if (_playerHPValueLabel != null)
            {
                _playerHPValueLabel.SetText(_playerHP.ToString());
            }
        }

        public void AddToPlayerHP(int points)
        {
            UpdatePlayerHP(_playerHP +  points);
        }

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

        public void UpdateScore(int newScore)
        {
            _score = newScore;
            if (_scoreValueLabel != null)
            {
                _scoreValueLabel.SetText(_score.ToString());
            }
        }

        public void AddToScore(int points)
        {
            UpdateScore(_score + points);
        }

        public int GetScore()
        {
            return _score;
        }
        #endregion
    }
}
