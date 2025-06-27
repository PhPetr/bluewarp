using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace bluewarp.UI
{
    /// <summary>
    /// Instruction UI manager.
    /// </summary>
    public class InstructionUIManager : BaseUIManager
    {
        private Label _instructionLabel;
        private ScrollPane _scrollPane;

        /// <summary>
        /// Create Instruction UI.
        /// </summary>
        /// <param name="scene">Scene to which to add UI</param>
        public InstructionUIManager(Scene scene) : base(scene)
        {
            Initialize();
            Debug.Log($"[Constructed Instruction UI");
            //Table.DebugAll();
        }

        /// <summary>
        /// Aligns table to center and pad.
        /// </summary>
        protected override void SetupTableAlignment()
        {
            Table.Center();
            Table.Pad(GameConstants.DefaultUIPadding);
        }

        /// <summary>
        /// Sets up Instruction UI.
        /// </summary>
        protected override void SetupUI()
        {
            CreateTitleLabel("HOW TO PLAY");
            NewEmptyLine();
            CreateInstructions();
            NewEmptyLine();
            CreateMenuButton("Back to Menu");
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

            Table.Add(_scrollPane).Width((GameConstants.GameWidth-10) * GameSettings.Scale).Height(100 * GameSettings.Scale).Pad(10);
            Table.Row();
        }
    }
}
