using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace bluewarp
{
    public class Game1 : Core
    {
        const int G_WIDTH = 512;
        const int G_HEIGHT = 384;
        public Game1() : base(G_WIDTH, G_HEIGHT)
        { }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            Scene.SetDefaultDesignResolution(G_WIDTH, G_HEIGHT, Scene.SceneResolutionPolicy.ShowAllPixelPerfect);
            Window.AllowUserResizing = true;
            Scene = new RunGameScene();
        }
    }
}
