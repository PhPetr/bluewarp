using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;

namespace bluewarp
{
    public class GameUIManager
    {
        private Scene _scene;
        private Entity _uiEntity;
        private UICanvas _uiCanvas;
        private Table _table;

        private LabelStyle _defaultLabelStyle;
        private int _score;
        private Label _scoreTitleLabel;
        private Label _scoreValueLabel;

        private int _playerHP;
        private Label _playerHPTitleLabel;
        private Label _playerHPValueLabel;

        public GameUIManager(Scene scene)
        {
            _scene = scene;
            _score = 0;
            _playerHP = GameConstants.Player.ShipMaxHealth;
            _defaultLabelStyle = new LabelStyle(Graphics.Instance.BitmapFont, Color.White)
            {
                FontScale = GameConstants.Scale
            };
            CreateUi();
        }

        private void CreateUi()
        {
            _uiEntity = _scene.CreateEntity("UI");

            _uiCanvas = _uiEntity.AddComponent(new UICanvas());
            _uiCanvas.IsFullScreen = true;
            _uiCanvas.RenderLayer = 999;

            _table = _uiCanvas.Stage.AddElement(new Table());
            _table.SetFillParent(true);
            _table.Top().Right();

            ScoreUISetup();

            NewEmptyLine();

            HPUISetup();

            _table.Pad(10);
        }

        private void NewEmptyLine()
        {
            _table.Row();
            _table.Add(new Label(" ", _defaultLabelStyle));
            _table.Row();
        }

        #region HP UI
        private void HPUISetup()
        {
            _playerHPTitleLabel = _table.Add(new Label("HP:", _defaultLabelStyle)).GetElement<Label>();
            _playerHPTitleLabel.SetAlignment(Align.Right);

            _table.Row();

            _playerHPValueLabel = _table.Add(new Label(_playerHP.ToString(), _defaultLabelStyle)).GetElement<Label>();
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
            _scoreTitleLabel = _table.Add(new Label($"Score:", _defaultLabelStyle)).GetElement<Label>();
            _scoreTitleLabel.SetAlignment(Align.Right);

            _table.Row();

            _scoreValueLabel = _table.Add(new Label(_score.ToString(), _defaultLabelStyle)).GetElement<Label>();
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

        public void Dispose()
        {
            _uiEntity?.Destroy();
        }
    }
}
