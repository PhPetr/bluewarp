using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using System.Diagnostics.CodeAnalysis;

namespace bluewarp
{
    public class Game1 : Core
    {
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            Window.AllowUserResizing = true;
            Scene = new RunGameScene();
        }
    }
}
