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
        private int _score;
        private Label _scoreTitleLabel;
        private Label _scoreValueLabel;

        public GameUIManager(Scene scene)
        {
            _scene = scene;
            _score = 0;
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

            var labelStyle = new LabelStyle(Graphics.Instance.BitmapFont, Color.White)
            {
                FontScale = GameConstants.Scale
            };

            _scoreTitleLabel = _table.Add(new Label($"Score:", labelStyle)).GetElement<Label>();
            _scoreTitleLabel.SetAlignment(Align.Right);

            _table.Row();

            _scoreValueLabel = _table.Add(new Label(_score.ToString(), labelStyle)).GetElement<Label>();
            _scoreValueLabel.SetAlignment(Align.Right);

            _table.Pad(10);
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

        public void Dispose()
        {
            _uiEntity?.Destroy();
        }
    }
}
