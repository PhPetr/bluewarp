using Nez;
using Nez.Sprites;
using Microsoft.Xna.Framework;
using Nez.Textures;
using System;

namespace bluewarp
{
    /// <summary>
    /// Controller of Stationary (Basic) Enemy.
    /// Child of BaseProjectileShooter.
    /// </summary>
    public class StationaryEnemy : BaseProjectileShooter
    {        
        static System.Random _random = new System.Random();
        float _nextFireDelay = 1f;
        protected string EnemyTexture;
        protected int EnemyRenderLayer;

        SpriteRenderer _renderer;

        /// <summary>
        /// Constructor of StationaryEnemy.
        /// </summary>
        /// <param name="enemyTexture">Path to enemy texture</param>
        /// <param name="renderLayer">RenderLayer of enemy</param>
        /// <param name="projectileOffset">Offset of spawned projectiles</param>
        /// <param name="projectileDelay">Delay of projectile fire</param>
        public StationaryEnemy(string enemyTexture = GameConstants.BasicEnemy.DefaultTexture, 
            int renderLayer = bluewarp.RenderLayer.StationaryEnemySprite, 
            int projectileOffset = 24, 
            float projectileDelay = GameConstants.BasicEnemy.DefaultProjectileDelay)
        {
            EnemyTexture = enemyTexture;
            EnemyRenderLayer = renderLayer;
            ProjectileSpawnOffset = projectileOffset;
            ProjectileDirection = new Vector2 (0, 1);
            ProjectileSpeed = new Vector2(100);
            ProjectileDelay = projectileDelay;
        }

        protected override void SetupVisuals()
        {
            var enemyTexture = Entity.Scene.Content.LoadTexture(EnemyTexture);
            var sprite = new Sprite(enemyTexture);
            _renderer = Entity.AddComponent(new SpriteRenderer(sprite));
            _renderer.RenderLayer = EnemyRenderLayer;
            
            var explosionTexture = Entity.Scene.Content.LoadTexture(Nez.Content.BasicEnemy.explosion);
            var explosion = Sprite.SpritesFromAtlas(explosionTexture, 32, 32);
            _explosionAnimator = Entity.AddComponent<SpriteAnimator>();
            _explosionAnimator.AddAnimation("Explosion", explosion.ToArray());
            _explosionAnimator.RenderLayer = bluewarp.RenderLayer.StationaryEnemyExplosion;
            _explosionAnimator.Enabled = false;
        }

        /// <summary>
        /// Adds a little bit of randomnes to ProjectileDelay.
        /// </summary>
        protected override void HandleFiring()
        {
                _nextFireDelay = ProjectileDelay + (float)(_random.NextDouble() * 0.5);

                var battleScene = Entity.Scene as RunGameScene;
                battleScene.CreateProjectiles(
                    new Vector2(Transform.Position.X, Transform.Position.Y + ProjectileSpawnOffset),
                    ProjectileSpeed * ProjectileDirection,
                    CollideWithLayer.StationaryEnemyProjectile,
                    PhysicsLayer.StationaryEnemyProjectile,
                    Nez.Content.BasicEnemy.enemy_projectile);
        }

        protected override bool CanFireProjectile()
        {
            return Time.TotalTime - _lastProjectileTime >= _nextFireDelay;
        }

        /// <summary>
        /// Disables renderer of enemy texture and calls base.
        /// </summary>
        public override void PlayExplosionAndDestroy()
        {
            _renderer.Enabled = false;
            base.PlayExplosionAndDestroy();
        }
    }
}
