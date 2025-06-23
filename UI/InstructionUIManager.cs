using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace bluewarp.UI
{
    public class InstructionUIManager : BaseUIManager
    {
        private Label _titleLabel;
        private Label _instructionLabel;
        private ScrollPane _scrollPane;
        private Button _menuButton;

        public InstructionUIManager(Scene scene) : base(scene)
        {
            Initialize();
            Debug.Log($"[Constructed Instruction UI");
            //Table.DebugAll();
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
            CreateInstructions();
            NewEmptyLine();
            CreateButtons();
        }

        private void CreateTitle()
        {
            var titleStyle = new LabelStyle(Graphics.Instance.BitmapFont, Color.White)
            {
                FontScale = GameConstants.Scale * 1.5f,
                Background = CreateBorderedBackground(Color.Black, Color.White, 5)
            };

            _titleLabel = Table.Add(new Label("HOW TO PLAY", titleStyle)).GetElement<Label>();
            _titleLabel.SetAlignment(Align.Center);
            Table.Row();
        }

        private void CreateInstructions()
        {
            string instructions = @"CONTROLS:
WASD or arrow keys - Move your ship
SPACE - Fire blaster
ESC - Exit game

OBJECTIVE:
Destroy enemies to earn points
Defeat boss at the end to win";
            
            _instructionLabel = new Label(instructions, DefaultLabelStyle);
            //_instructionLabel.SetWrap(true);
            _instructionLabel.SetAlignment(Align.Center);

            _scrollPane = new ScrollPane(_instructionLabel, new ScrollPaneStyle());
            _scrollPane.SetScrollingDisabled(true, false);

            Table.Add(_scrollPane).Width((GameConstants.GameWidth-10) * GameConstants.Scale).Height(100 * GameConstants.Scale).Pad(10);
            Table.Row();
        }

        private void CreateButtons()
        {
            _menuButton = Table.Add(new TextButton("Back to Menu", DefaultButtonStyle)).GetElement<Button>();
            _menuButton.OnClicked += OnBackButtonClicked;
            Table.Row();
        }

        private void OnBackButtonClicked(Button button)
        {
            SceneManager.LoadMenu();
        }

        public override void Dispose()
        {
            if (_menuButton != null) 
                _menuButton.OnClicked -= OnBackButtonClicked;

            base.Dispose();
        }
    }
}
