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
            _hitsUntilDead = (health > 0) ? 2*health : 10;
        }

        public override void OnAddedToEntity()
        {
            _sprite = Entity.GetComponent<SpriteRenderer>();
        }

        void ITriggerListener.OnTriggerEnter(Collider other, Collider self)
        {
            _hitCounter++;
            if (_hitCounter >= _hitsUntilDead)
            {
                var enemy = Entity.GetComponent<StationaryEnemy>();
                if (enemy != null) 
                    enemy.PlayExplosionAndDestroy();
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
