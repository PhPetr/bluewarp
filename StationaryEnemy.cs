using Nez;
using Nez.Sprites;
using Microsoft.Xna.Framework;
using Nez.Textures;

namespace bluewarp
{
    public class StationaryEnemy : Component, IUpdatable, ITriggerListener
    {
        const int ProjectileSpawnOffset = 24;
        Vector2 ProjectileDir = new(0, 1);

        SpriteRenderer _renderer;
        SpriteAnimator _explosionAnimation;

        Vector2 _projectileSpeed = new Vector2(300);

        float _elapsedTimeAfterCreation = 0f;
        float _delayMoveStart = 2f;
        bool _shouldMove = false;
        bool _stopped = false;

        const float ProjectileDelay = 0.2f;
        float _lastProjectileTime = 0f;

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

            //Transform.Position = new Vector2(_startWidthX, _startHeightY);
        }

        public void PlayExplosionAndDestroy()
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
            /*
            if (!_shouldMove)
            {
                _elapsedTimeAfterCreation += Time.DeltaTime;
                if (_elapsedTimeAfterCreation >= _delayMoveStart)
                {
                    _shouldMove = true;
                }
                return;
            }
            
            var upwardMovement = new Vector2(0, -_upwardsSpeed * Time.DeltaTime);
            if (Transform.Position.Y <= _stopHeightY) upwardMovement = new Vector2(0, 0);

            _subpixelV2.Update(ref upwardMovement);
            Transform.Position += upwardMovement;
            */
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
