using Nez;
using Nez.Sprites;
using Nez.Textures;
using Microsoft.Xna.Framework;
using System;

namespace bluewarp
{
    public class ProjectileHitDetector : Component, ITriggerListener, IHittable
    {
        //int _hitsUntilDead = 10;
        private int _maxHealth;
        private int _currentHealth;

        //int _hitCounter;
        private SpriteRenderer _sprite;

        public event Action<Entity, int> OnHit;
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;

        public ProjectileHitDetector(int health = 10)
        {
            _maxHealth = (health > 0) ? health : 10;
            _currentHealth = health;
        }

        public override void OnAddedToEntity()
        {
            _sprite = Entity.GetComponent<SpriteRenderer>();
        }

        void ITriggerListener.OnTriggerEnter(Collider other, Collider self)
        {
            // Ignore player event trigger
            if (other.PhysicsLayer == (1 << CollideWithLayer.PlayerEventCollider)) return;

            _currentHealth--;

            OnHit?.Invoke(Entity, _currentHealth);

            if (_currentHealth <= 0)
            {
                var destructable = Entity.GetComponent<IDestructable>();
                if (destructable != null) 
                    destructable.PlayExplosionAndDestroy();
                else
                    Entity.Destroy();
                return;
            }

            _sprite.Color = Color.Red;
            Core.Schedule(0.1f, timer => _sprite.Color = Color.White);
        }

        void ITriggerListener.OnTriggerExit(Collider other, Collider self)
        {
        }
    }
}
