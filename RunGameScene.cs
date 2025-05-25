using Microsoft.Xna.Framework;
using Nez;
using Nez.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bluewarp
{
    internal class RunGameScene : BaseScene
    {
        const int SCALE = 4;
        const int G_WIDTH = 256;
        const int G_HEIGHT = 192;
        const int X_LOCKED_OFFSET = 159;

        const int startHeightX = 200 * 32;
        const int startWidthY = 4 * 32;

        internal static readonly string[] levelOneLayerNames = new[] { "BaseLayer", "BossLayer" };

        public RunGameScene() : base(true, true)
        { }

        public override void Initialize()
        {
            base.Initialize();
            
            SetDesignResolution(G_WIDTH, G_HEIGHT, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(G_WIDTH * SCALE, G_HEIGHT * SCALE);
            var scale = new Vector2(SCALE);
            ClearColor = Color.Black;

            // Loading Map
            var tiledEntityMap = CreateEntity("tiled-map");
            var map = Content.LoadTiledMap(Nez.Content.Level1.levelone);
            
            var tiledMapRenderer = tiledEntityMap.AddComponent(new TiledMapRenderer(map, "BorderCollision"));
            tiledMapRenderer.SetLayersToRender(levelOneLayerNames);
            tiledMapRenderer.RenderLayer = 10; // render map below most of things

            // Setting camera
            // TODO: camera works partialy
            var topLeft = new Vector2(map.TileWidth, map.TileWidth);
            var bottomRight = new Vector2(
                map.TileWidth * (map.Width - 1),
                map.TileWidth * (map.Height - 1));
            tiledEntityMap.AddComponent(new CameraBounds(topLeft, bottomRight, X_LOCKED_OFFSET));
            Entity cameraMover = CreateEntity("camera-mover");
            cameraMover.AddComponent(new CameraMover());
            

            // we only want to collide with the tilemap, which is on the default layer 0
            //Flags.SetFlagExclusive(ref collider.CollidesWithLayers, 0);

            Camera.Entity.AddComponent(new FollowCamera(cameraMover));
        }
    }
}
