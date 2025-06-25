using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;
using Nez.UI;
using System;

namespace bluewarp
{
    internal class RunGameScene : BaseScene
    {        
        int _startWidthX;
        int _startHeightY;

        private Entity _tiledEntityMap;
        private TmxMap _tileMap;

        private Entity _mainCameraMover;

        private Entity _playerShip;

        private GameUIManager _UIManager;

        internal static readonly string[] levelOneLayerNames = new[] { "BaseLayer", "BossLayer" };

        public RunGameScene() : base(true, true)
        { }

        public override void Initialize()
        {
            base.Initialize();
            
            SetDesignResolution(GameConstants.GameWidth, GameConstants.GameHeight, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(GameSettings.ScaledGameWidth, GameSettings.ScaledGameHeight);
            ClearColor = Color.Black;

            BGMusicManager.Play(GameConstants.BGM.BlueTension, volume: GameSettings.BGM.BGVolume);

            _UIManager = new GameUIManager(this);

            GameSFXManager.LoadContent();

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
                var zonePosition = new Vector2(zone.X + 3*32, zone.Y+16);
                var zoneEntity = CreateEntity(zone.Name, zonePosition);

                var zoneCollider = zoneEntity.AddComponent(new BoxCollider(zone.Width, zone.Height));
                zoneCollider.IsTrigger = true;
                Flags.SetFlagExclusive(ref zoneCollider.CollidesWithLayers, CollideWithLayer.Zones);
                Flags.SetFlagExclusive(ref zoneCollider.PhysicsLayer, PhysicsLayer.Zones);
                zoneEntity.AddComponent(new ZoneTriggerComponent(zone.Name, _tileMap, this));
                
            }
        }

        private void LoadPlayer()
        {
            var playerSpawn = _tileMap.GetObjectGroup("start").Objects["playerSpawn"];
            var playerSpawnPosition = new Vector2(playerSpawn.X, playerSpawn.Y);
            _startHeightY = (int)playerSpawn.Y;
            _startWidthX = (int)playerSpawn.X;

            _playerShip = CreateEntity("player-ship", playerSpawnPosition);
            var ship = new FighterShip(GameConstants.Player.ShipMoveSpeed, GameConstants.DefaultUpwardsScrollSpeed);
            _playerShip.AddComponent(ship);

            var hitDetector = new ProjectileHitDetector(GameSettings.Player.ShipMaxHealth);
            _playerShip.AddComponent(hitDetector);

            Action<Entity, int> hitCallback = (entity, currentHealth) =>
            {
                Debug.Log($"[Player ship hit] Current HP: {currentHealth}");
                UpdatePlayerHP(currentHealth);
                GameSFXManager.PlaySFX(GameConstants.SFX.DamageImpact, GameSettings.SFX.DamageImpactVolume);
            };

            var hitWrapper = HitObserver.Subscribe(hitDetector, hitCallback);

            DestructionObserver.Subscribe(ship, e =>
            {
                Debug.Log($"[Player ship destroyed] Entity: {e.Name}");
                HitObserver.Unsubscribe(hitDetector, hitCallback);
                var finalScore = GetScore();
                var endState = GameConstants.GameEndState.Defeat;
                Debug.Log($"[Calling Load game over] final score: {finalScore}, state: {endState}");
                SceneManager.LoadGameOver(finalScore, endState);
            });

            var shipCollider = _playerShip.AddComponent<CircleCollider>();

            // we only want to collide with the tilemap, which is on the default layer 0
            Flags.SetFlagExclusive(ref shipCollider.CollidesWithLayers, CollideWithLayer.PlayerShipCollider);
            // move ourself to layer 1 so that we dont get hit by the projectiles that we fire
            Flags.SetFlagExclusive(ref shipCollider.PhysicsLayer, PhysicsLayer.PlayerShipCollider);

            var eventTriggerCollider = _playerShip.AddComponent<CircleCollider>();
            Flags.SetFlagExclusive(ref eventTriggerCollider.CollidesWithLayers, CollideWithLayer.PlayerEventCollider); 
            Flags.SetFlagExclusive(ref eventTriggerCollider.PhysicsLayer, PhysicsLayer.PlayerEventCollider);
            eventTriggerCollider.IsTrigger = true;
        }

        private void SettingUpCamera()
        {
            // Setting camera
            var topLeft = new Vector2(_tileMap.TileWidth, _tileMap.TileWidth);
            var bottomRight = new Vector2(
                _tileMap.TileWidth * (_tileMap.Width - 1),
                _tileMap.TileWidth * (_tileMap.Height - 1));
            _tiledEntityMap.AddComponent(new CameraBounds(topLeft, bottomRight, GameConstants.XLockedOffset));

            _mainCameraMover = CreateEntity("camera-mover");
            _mainCameraMover.AddComponent(new CameraMover(_startHeightY, _startWidthX));
            Camera.Entity.AddComponent(new FollowCamera(_mainCameraMover));
            //Camera.Entity.AddComponent(new FollowCamera(_playerShip)); //TODO: remove before release
        }

        public Entity CreateProjectiles(Vector2 position, Vector2 velocity, int projectileCollideWithLayer, int projectilePhysicsLayer, string textureSource)
        {
            // create an Entity to house the projectile and its logic
            var entity = CreateEntity("projectile");
            entity.Position = position;
            entity.AddComponent(new ProjectileMover());
            entity.AddComponent(new ProjectileController(velocity));
            entity.AddComponent(new TimeAliveComponent());

            // add a collider so we can detect intersections
            var collider = entity.AddComponent<CircleCollider>();
            Flags.SetFlagExclusive(ref collider.CollidesWithLayers, projectileCollideWithLayer);
            Flags.SetFlagExclusive(ref collider.PhysicsLayer, projectilePhysicsLayer);


            // load up a Texture that contains a fireball animation and setup the animation frames
            var texture = Content.LoadTexture(textureSource);
            var sprite = new Sprite(texture);

            // add the Sprite to the Entity and play the animation after creating it
            var spriteRenderer = entity.AddComponent(new SpriteRenderer(sprite));

            // render after (under) our player who is on renderLayer 1
            spriteRenderer.RenderLayer = RenderLayer.PlayerProjectile;
            
            return entity;
        }

        #region HP UI
        public void UpdatePlayerHP(int newHP)
        {
            _UIManager?.UpdatePlayerHP(newHP);
        }
        
        public void AddToPlayerHP(int points)
        {
            _UIManager?.AddToPlayerHP(points);
        }

        public int GetPlayerHP()
        {
            return _UIManager?.GetPlayerHP() ?? 0;
        }
        #endregion

        #region Score methods
        public void AddToScore(int points)
        {
            _UIManager?.AddToScore(points);
        }
        public int GetScore()
        {
            return _UIManager?.GetScore() ?? 0;
        }
        #endregion

        public override void End()
        {
            _UIManager?.Dispose();
            base.End();
        }
    }
}
