using Nez;
using Nez.Sprites;
using Microsoft.Xna.Framework;
using Nez.Textures;
using System;

namespace bluewarp
{
    public class StationaryEnemy : BaseProjectileShooter
    {        
        static System.Random _random = new System.Random();
        float _nextFireDelay = 1f;
        const string _defaultEnemyTexture = Nez.Content.BasicEnemy.static_enemy;
        protected string EnemyTexture;
        protected int EnemyRenderLayer;

        SpriteRenderer _renderer;
        public StationaryEnemy(string enemyTexture = _defaultEnemyTexture, int renderLayer = bluewarp.RenderLayer.StationaryEnemySprite, int projectileOffset = 24)
        {
            EnemyTexture = enemyTexture;
            EnemyRenderLayer = renderLayer;
            ProjectileSpawnOffset = projectileOffset;
            ProjectileDirection = new Vector2 (0, 1);
            ProjectileSpeed = new Vector2(100);
            ProjectileDelay = 1.5f;
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

        public override void PlayExplosionAndDestroy()
        {
            _renderer.Enabled = false;
            base.PlayExplosionAndDestroy();
        }
    }
}
