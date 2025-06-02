using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Systems;
using Nez.Textures;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bluewarp
{
    internal class RunGameScene : BaseScene
    {
        const int Scale = 5;
        const int G_Width = 256;
        const int G_Height = 192;
        const int X_LockedOffset = 159;

        const int StartHeightX = 200 * 32;
        const int StartWidthY = 4 * 32;
        const float UpwardsSpeed = 100f;

        const float ShipMoveSpeed = 125f;

        private Entity _backgroundEntity;
        private Entity _mainCameraMover;

        internal static readonly string[] levelOneLayerNames = new[] { "BaseLayer", "BossLayer" };

        public RunGameScene() : base(true, true)
        { }

        public override void Initialize()
        {
            base.Initialize();
            
            SetDesignResolution(G_Width, G_Height, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(G_Width * Scale, G_Height * Scale);
            ClearColor = Color.Black;
            

            // Loading Map
            var tiledEntityMap = CreateEntity("tiled-map");
            var map = Content.LoadTiledMap(Nez.Content.Level1.levelone);
            var tiledMapRenderer = tiledEntityMap.AddComponent(new TiledMapRenderer(map, "BorderCollision"));
            tiledMapRenderer.SetLayersToRender(levelOneLayerNames);
            tiledMapRenderer.RenderLayer = 10; // render map below most of things

            // Setting camera
            // 
            var topLeft = new Vector2(map.TileWidth, map.TileWidth);
            var bottomRight = new Vector2(
                map.TileWidth * (map.Width - 1),
                map.TileWidth * (map.Height - 1));
            tiledEntityMap.AddComponent(new CameraBounds(topLeft, bottomRight, X_LockedOffset));
            
            // we only want to collide with the tilemap, which is on the default layer 0
            //Flags.SetFlagExclusive(ref collider.CollidesWithLayers, 0);

            var playerShip = CreateEntity("player-ship", new Vector2(32));
            playerShip.AddComponent(new FighterShip(StartHeightX, StartWidthY, ShipMoveSpeed, UpwardsSpeed));
            
            var shipCollider = playerShip.AddComponent<CircleCollider>();
            
            
            _mainCameraMover = CreateEntity("camera-mover");
            _mainCameraMover.AddComponent(new CameraMover(StartHeightX, StartWidthY, UpwardsSpeed));
            Camera.Entity.AddComponent(new FollowCamera(_mainCameraMover));
            
        }

        public Entity CreateProjectiles(Vector2 position, Vector2 velocity)
        {
            // create an Entity to house the projectile and its logic
            var entity = CreateEntity("projectile");
            entity.Position = position;
            entity.AddComponent(new ProjectileMover());
            entity.AddComponent(new ProjectileController(velocity));

            // add a collider so we can detect intersections
            var collider = entity.AddComponent<CircleCollider>();
            Flags.SetFlagExclusive(ref collider.CollidesWithLayers, 10);
            Flags.SetFlagExclusive(ref collider.PhysicsLayer, 1);


            // load up a Texture that contains a fireball animation and setup the animation frames
            var texture = Content.LoadTexture(Nez.Content.PlayerShip.player_main_projectile);
            var sprite = new Sprite(texture);

            // add the Sprite to the Entity and play the animation after creating it
            var spriteRenderer = entity.AddComponent(new SpriteRenderer(sprite));

            // render after (under) our player who is on renderLayer 0, the default
            spriteRenderer.RenderLayer = 2;

            // clone the projectile and fire it off in the opposite direction
            var newEntity = entity.Clone(entity.Position);
            newEntity.GetComponent<ProjectileController>().Velocity *= 1;
            AddEntity(newEntity);

            return entity;
        }
    }
}
