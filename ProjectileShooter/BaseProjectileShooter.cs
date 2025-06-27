using Nez;
using Nez.Sprites;
using Nez.Textures;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Audio;

namespace bluewarp
{
    /// <summary>
    /// BaseProjectileShooter is a base for StationaryEnemy and FighterShip.
    /// </summary>
    public abstract class BaseProjectileShooter : Component,IUpdatable, ITriggerListener, IDestructable
    {
        protected float ProjectileDelay;
        protected Vector2 ProjectileDirection;
        protected Vector2 ProjectileSpeed;
        protected int ProjectileSpawnOffset;

        protected float _lastProjectileTime;
        protected bool _isDying = false;

        protected Collider _collider;
        protected SpriteAnimator _explosionAnimator;

        public event Action<Entity> OnDestroyed;

        public override void OnAddedToEntity()
        {
            _collider = Entity.GetComponent<Collider>();
            SetupVisuals();
        }

        /// <summary>
        /// Base update loop. Fires projectile when it can.
        /// </summary>
        public virtual void Update()
        {
            if (_isDying) return;

            if (CanFireProjectile())
            {
                _lastProjectileTime = Time.TotalTime;
                HandleFiring();
            }
        }

        /// <summary>
        /// Based on ProjectileDelay determines whether a projectile can be fired.
        /// </summary>
        /// <returns>Bool wether projectile can be fired.</returns>
        protected virtual bool CanFireProjectile()
        {
            return Time.TotalTime - _lastProjectileTime >= ProjectileDelay;
        }

        /// <summary>
        /// Plays Explosion animation when entity dies and destroys the entity.
        /// </summary>
        public virtual void PlayExplosionAndDestroy()
        {
            if (_isDying) return;
            _isDying = true;

            if (_collider != null) 
                _collider.SetEnabled(false);

            if (_explosionAnimator != null)
            {
                _explosionAnimator.Enabled = true;
                _explosionAnimator.OnAnimationCompletedEvent += OnExplosionComplete;
                _explosionAnimator.Play("Explosion", SpriteAnimator.LoopMode.Once);
                GameSFXManager.PlaySFX(GameConstants.SFX.Explosion, GameSettings.SFX.ExplosionVolume);
            }
            else
            {
                OnDestroyed?.Invoke(Entity);
                Entity.Destroy();
            }
        }
        
        protected virtual void OnExplosionComplete(string animationName)
        {
            if (animationName == "Explosion")
            {
                _explosionAnimator.OnAnimationCompletedEvent -= OnExplosionComplete;
                OnDestroyed?.Invoke(Entity);
                Entity.Destroy();
            }
        }

        /// <summary>
        /// Childs implementation of visuals.
        /// </summary>
        protected abstract void SetupVisuals();

        /// <summary>
        /// Childs handling of firing projectiles.
        /// </summary>
        protected abstract void HandleFiring();

        #region ITriggerListener implementation

        public virtual void OnTriggerEnter(Collider other, Collider self)
        {
            Debug.Log("triggerEnter: {0}", other.Entity.Name);
        }


        public virtual void OnTriggerExit(Collider other, Collider self)
        {
            Debug.Log("triggerExit: {0}", other.Entity.Name);
        }

        #endregion

    }
}
