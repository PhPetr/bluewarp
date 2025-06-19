using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Systems;
using Nez.Textures;
using Nez.Tiled;

namespace bluewarp
{
    internal class RunGameScene : BaseScene
    {
        const int Scale = 5;
        const int G_Width = 256;
        const int G_Height = 192;
        const int X_LockedOffset = 159;

        const int EndHeightY = 64;
        const float UpwardsSpeed = 50f;
        //const float MapDuration = 100f;

        const float ShipMoveSpeed = 125f;
        
        int _startWidthX;
        int _startHeightY;

        private Entity _tiledEntityMap;
        private TmxMap _tileMap;

        private Entity _mainCameraMover;

        private Entity _playerShip;

        internal static readonly string[] levelOneLayerNames = new[] { "BaseLayer", "BossLayer" };

        public RunGameScene() : base(true, true)
        { }

        public override void Initialize()
        {
            base.Initialize();
            
            SetDesignResolution(G_Width, G_Height, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(G_Width * Scale, G_Height * Scale);
            ClearColor = Color.Black;
            
            LoadTileMap();

            LoadPlayer();

            SettingUpCamera();
        }

        private void LoadTileMap()
        {
            // Loading Map
            _tiledEntityMap = CreateEntity("tiled-map");
            _tileMap = Content.LoadTiledMap(Nez.Content.Level1.levelone);
            var tiledMapRenderer = _tiledEntityMap.AddComponent(new TiledMapRenderer(_tileMap, "BorderCollision"));
            tiledMapRenderer.SetLayersToRender(levelOneLayerNames);
            tiledMapRenderer.RenderLayer = RenderLayer.TileMap; // render map below most of things
            LoadMapZones();
        }

        private void LoadMapZones()
        {
            var zones = _tileMap.GetObjectGroup("zones").Objects;
            foreach ( var zone in zones )
            {
                var zonePosition = new Vector2(zone.X + 3*32, zone.Y+32);
                var zoneEntity = CreateEntity(zone.Name, zonePosition);

                var zoneCollider = zoneEntity.AddComponent(new BoxCollider(zone.Width, zone.Height));
                zoneCollider.IsTrigger = true;
                Flags.SetFlagExclusive(ref zoneCollider.CollidesWithLayers, CollideWithLayer.Zones);
                Flags.SetFlagExclusive(ref zoneCollider.PhysicsLayer, PhysicsLayer.Zones);
                zoneEntity.AddComponent(new ZoneTrigger(zone.Name, _tileMap, this));
                
            }
        }

        private void SettingUpCamera()
        {
            // Setting camera
            var topLeft = new Vector2(_tileMap.TileWidth, _tileMap.TileWidth);
            var bottomRight = new Vector2(
                _tileMap.TileWidth * (_tileMap.Width - 1),
                _tileMap.TileWidth * (_tileMap.Height - 1));
            _tiledEntityMap.AddComponent(new CameraBounds(topLeft, bottomRight, X_LockedOffset));

            _mainCameraMover = CreateEntity("camera-mover");
            _mainCameraMover.AddComponent(new CameraMover(_startHeightY, _startWidthX, UpwardsSpeed));
            //_mainCameraMover.AddComponent(new SmoothVerticalMover(StartHeightY, StartWidthX, EndHeightY, MapDuration));
            Camera.Entity.AddComponent(new FollowCamera(_mainCameraMover));
        }

        private void LoadPlayer()
        {
            var playerSpawn = _tileMap.GetObjectGroup("start").Objects["playerSpawn"];
            var playerSpawnPosition = new Vector2(playerSpawn.X, playerSpawn.Y);
            _startHeightY = (int)playerSpawn.Y;
            _startWidthX = (int)playerSpawn.X;

            _playerShip = CreateEntity("player-ship", playerSpawnPosition);
            _playerShip.AddComponent(new FighterShip(_startHeightY, _startWidthX, ShipMoveSpeed, UpwardsSpeed));
            var shipCollider = _playerShip.AddComponent<CircleCollider>();

            // we only want to collide with the tilemap, which is on the default layer 0
            Flags.SetFlagExclusive(ref shipCollider.CollidesWithLayers, CollideWithLayer.PlayerShipCollider);
            // move ourself to layer 1 so that we dont get hit by the projectiles that we fire
            Flags.SetFlagExclusive(ref shipCollider.PhysicsLayer, PhysicsLayer.PlayerShipCollider);

            var eventTriggerCollider = _playerShip.AddComponent<CircleCollider>();
            Flags.SetFlagExclusive(ref eventTriggerCollider.CollidesWithLayers, CollideWithLayer.PlayerEventCollider); // layer 6 will have map zone triggers
            Flags.SetFlagExclusive(ref eventTriggerCollider.PhysicsLayer, PhysicsLayer.PlayerEventCollider);
            eventTriggerCollider.IsTrigger = true;
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
            Flags.SetFlagExclusive(ref collider.CollidesWithLayers, CollideWithLayer.PlayerProjectile);
            Flags.SetFlagExclusive(ref collider.PhysicsLayer, PhysicsLayer.PlayerProjectile);


            // load up a Texture that contains a fireball animation and setup the animation frames
            var texture = Content.LoadTexture(Nez.Content.PlayerShip.player_main_projectile);
            var sprite = new Sprite(texture);

            // add the Sprite to the Entity and play the animation after creating it
            var spriteRenderer = entity.AddComponent(new SpriteRenderer(sprite));

            // render after (under) our player who is on renderLayer 1
            spriteRenderer.RenderLayer = RenderLayer.PlayerProjectile;

            // clone the projectile and fire it off in the opposite direction
            var newEntity = entity.Clone(entity.Position);
            newEntity.GetComponent<ProjectileController>().Velocity *= 1;
            AddEntity(newEntity);

            return entity;
        }
    }
}
