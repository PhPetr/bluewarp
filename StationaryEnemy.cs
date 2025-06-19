using Nez;
using Nez.Sprites;
using Microsoft.Xna.Framework;
using Nez.Textures;
using System;

namespace bluewarp
{
    public class StationaryEnemy : Component, IUpdatable, ITriggerListener, IDestructable
    {
        const int ProjectileSpawnOffset = 24;
        Vector2 ProjectileDownDir = new(0, 1);
        Vector2 _projectileSpeed = new Vector2(100);
        const float ProjectileDelay = 1.5f;
        float _lastProjectileTime = 0f;
        static System.Random _random = new System.Random();
        float _nextFireDelay = 1f;

        SpriteRenderer _renderer;
        SpriteAnimator _explosionAnimation;

        float _elapsedTimeAfterCreation;
        float _delayMoveStart = 2f;
        bool _shouldMove = false;
        bool _stopped = false;

        bool _isDying = false;

        public StationaryEnemy()
        {
            _elapsedTimeAfterCreation = 0f;
        }

        public override void OnAddedToEntity()
        {
            var enemyTexture = Entity.Scene.Content.LoadTexture(Nez.Content.BasicEnemy.static_enemy);
            var sprite = new Sprite(enemyTexture);
            _renderer = Entity.AddComponent(new SpriteRenderer(sprite));
            _renderer.RenderLayer = RenderLayer.StationaryEnemySprite;

            var explosionTexture = Entity.Scene.Content.LoadTexture(Nez.Content.BasicEnemy.explosion);
            var explosion = Sprite.SpritesFromAtlas(explosionTexture, 32, 32);
            _explosionAnimation = Entity.AddComponent<SpriteAnimator>();
            _explosionAnimation.AddAnimation("Explosion", explosion.ToArray());
            _explosionAnimation.RenderLayer = RenderLayer.StationaryEnemyExplosion;
            _explosionAnimation.Enabled = false;
        }

        void IDestructable.PlayExplosionAndDestroy()
        {
            if (_isDying) return;
            _isDying = true;

            _renderer.Enabled = false;
            _shouldMove = false;

            var collider = Entity.GetComponent<Collider>();
            if (collider != null)
                collider.SetEnabled(false);

            _explosionAnimation.Enabled = true;
            
            void OnExplosionComplete(string animationName)
            {
                if (animationName == "Explosion")
                {
                    _explosionAnimation.OnAnimationCompletedEvent -= OnExplosionComplete;
                    Entity.Destroy();
                }
            }
            _explosionAnimation.OnAnimationCompletedEvent += OnExplosionComplete;

            _explosionAnimation.Play("Explosion", SpriteAnimator.LoopMode.Once);
        }

        void IUpdatable.Update()
        {
            if (!_isDying && CanFireProjectile())
            {
                _lastProjectileTime = Time.TotalTime;
                _nextFireDelay = ProjectileDelay + (float)(_random.NextDouble() * 0.5);

                var battleScene = Entity.Scene as RunGameScene;
                battleScene.CreateProjectiles(
                    new Vector2(Transform.Position.X, Transform.Position.Y + ProjectileSpawnOffset),
                    _projectileSpeed * ProjectileDownDir,
                    CollideWithLayer.StationaryEnemyProjectile,
                    PhysicsLayer.StationaryEnemyProjectile,
                    Nez.Content.BasicEnemy.enemy_projectile);
            }
        }

        bool CanFireProjectile()
        {
            return Time.TotalTime - _lastProjectileTime >= _nextFireDelay;
        }

        #region ITriggerListener implementation

        void ITriggerListener.OnTriggerEnter(Collider other, Collider self)
        {
            Debug.Log("ENEMY triggerEnter: {0}", other.Entity.Name);
        }


        void ITriggerListener.OnTriggerExit(Collider other, Collider self)
        {
            Debug.Log("ENEMY triggerExit: {0}", other.Entity.Name);
        }

        #endregion
    }
}
