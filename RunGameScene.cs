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
        const int SCALE = 1;
        const int G_WIDTH = 256;
        const int G_HEIGHT = 192;
        internal static readonly string[] levelOneLayerNames = new[] { "BaseLayer", "BossLayer" };

        public RunGameScene() : base(true, true)
        { }

        public override void Initialize()
        {
            base.Initialize();
            SetDesignResolution(G_WIDTH * SCALE, G_HEIGHT * SCALE, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(G_WIDTH * SCALE * 3, G_HEIGHT * SCALE * 3);
            var scale = new Vector2(SCALE);
            ClearColor = Color.Blue;

            // Loading Map
            var tiledEntityMap = CreateEntity("tiled-map");
            var map = Content.LoadTiledMap(Nez.Content.Level1.levelone);
            tiledEntityMap.Transform.Scale = scale;
            tiledEntityMap.Transform.Position = new Vector2(0);
            var tiledMapRenderer = tiledEntityMap.AddComponent(new TiledMapRenderer(map, "BorderCollision"));
            tiledMapRenderer.SetLayersToRender(levelOneLayerNames);
            tiledMapRenderer.RenderLayer = 10; // render map below everything


            // Setting camera
            // TODO: not workingu!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var topLeft = new Vector2(map.TileWidth*SCALE, map.TileWidth*SCALE);
            var bottomRight = new Vector2(
                map.TileWidth * (map.Width - 1) * SCALE,
                map.TileWidth * (map.Height - 1) * SCALE);
            tiledEntityMap.AddComponent(new CameraBounds(topLeft, bottomRight));
            Entity cameraMover = CreateEntity("camera-mover");
            cameraMover.AddComponent(new CameraMover());
            //var collider = cameraMover.AddComponent<CircleCollider>();

            // we only want to collide with the tilemap, which is on the default layer 0
            //Flags.SetFlagExclusive(ref collider.CollidesWithLayers, 0);

            Camera.Entity.AddComponent(new FollowCamera(cameraMover));
        }
    }
}
