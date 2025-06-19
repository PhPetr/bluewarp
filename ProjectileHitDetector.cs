using Nez;
using Nez.Sprites;
using Nez.Textures;
using Microsoft.Xna.Framework;

namespace bluewarp
{
    public class ProjectileHitDetector : Component, ITriggerListener
    {
        int _hitsUntilDead = 10;

        int _hitCounter;
        SpriteRenderer _sprite;

        public ProjectileHitDetector(int health = 10)
        {
            _hitsUntilDead = (health > 0) ? health : 10;
        }

        public override void OnAddedToEntity()
        {
            _sprite = Entity.GetComponent<SpriteRenderer>();
        }

        void ITriggerListener.OnTriggerEnter(Collider other, Collider self)
        {
            // Ignore player event trigger
            if (other.PhysicsLayer == (1 << CollideWithLayer.PlayerEventCollider)) return;

            _hitCounter++;
            if (_hitCounter >= _hitsUntilDead)
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
