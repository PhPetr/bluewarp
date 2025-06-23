using bluewarp.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;
using System.Diagnostics.CodeAnalysis;

namespace bluewarp
{
    public class Game1 : Core
    {
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            LoadBGMusic();

            Window.AllowUserResizing = true;
            Core.DebugRenderEnabled = false;
            
            Scene = new MenuScene();
            //Scene = new EndGameScene(235, GameConstants.GameEndState.Victory);
            //Scene = new RunGameScene();
            //SceneManager.LoadGameScene();
        }

        private void LoadBGMusic()
        {
            BGMusicManager.LoadSong(GameConstants.BGM.BlueChill, GameConstants.BGM.BlueChillPath);
            BGMusicManager.LoadSong(GameConstants.BGM.BlueTension, GameConstants.BGM.BlueTensionPath);
        }
    }
}
