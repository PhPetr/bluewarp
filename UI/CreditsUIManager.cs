using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace bluewarp.UI
{
    public class CreditsUIManager : BaseUIManager
    {
        private Label _titleLabel;
        private Label _creditsLabel;
        private ScrollPane _scrollPane;
        private Button _menuButton;

        public CreditsUIManager(Scene scene) : base(scene)
        {
            Initialize();
            Debug.Log("[Constructed Credits UI]");
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
            CreateCredits();
            NewEmptyLine();
            CreateButtons();
        }

        private void CreateTitle()
        {
            _titleLabel = Table.Add(new Label("CREDITS", DefaultTitleStyle)).GetElement<Label>();
            _titleLabel.SetAlignment(Align.Center);
            Table.Row();
        }

        private void CreateCredits()
        {
            string credits = @"[BLUEWARP]
Created by Quang Thanh Pham
as a credit assignment program
for Programming course NPRG031

Art: Quang Thanh Pham

Music:
'Blue chill' by Jakub Jurik
'Blue tension' by Jakub Jurik

SFX:
retro shot blaster by JavierZumer 
-- https://freesound.org/s/257232/ 
-- License: Attribution 4.0

Retro, Space Explosion Or Death Sound 
Effect.wav by LilMati 
-- https://freesound.org/s/515005/ 
-- License: Creative Commons 0

8-bit damage-impact-break sounds.wav 
by EVRetro 
-- https://freesound.org/s/519072/ 
-- License: Creative Commons 0

Special thanks:
To my dear friend Jakub Jurik 
for composing such amazing tracks.

Made with Nez and Monogame framework";

            _creditsLabel = new Label(credits, DefaultLabelStyle);
            //_creditsLabel.SetWrap(true);
            _creditsLabel.SetAlignment(Align.Center);

            _scrollPane = new ScrollPane(_creditsLabel, new ScrollPaneStyle());
            _scrollPane.SetScrollingDisabled(true, false);

            Table.Add(_scrollPane).Width((GameConstants.GameWidth-10) * GameSettings.Scale).Height(100 * GameSettings.Scale).Pad(10);
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
