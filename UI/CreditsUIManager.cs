using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace bluewarp.UI
{
    /// <summary>
    /// Credits UI manager.
    /// </summary>
    public class CreditsUIManager : BaseUIManager
    {
        private Label _creditsLabel;
        private ScrollPane _scrollPane;

        /// <summary>
        /// Create credits UI.
        /// </summary>
        /// <param name="scene"></param>
        public CreditsUIManager(Scene scene) : base(scene)
        {
            Initialize();
            Debug.Log("[Constructed Credits UI]");
            //Table.DebugAll();
        }

        /// <summary>
        /// Aligns Table to center with padding.
        /// </summary>
        protected override void SetupTableAlignment()
        {
            Table.Center();
            Table.Pad(GameConstants.DefaultUIPadding);
        }

        /// <summary>
        /// Sets up credits UI.
        /// </summary>
        protected override void SetupUI()
        {
            CreateTitleLabel("CREDITS");
            NewEmptyLine();
            CreateCredits();
            NewEmptyLine();
            CreateMenuButton("Back to Menu");
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

    }
}
