using Nez;
using Nez.Tiled;
using Microsoft.Xna.Framework;

namespace bluewarp
{
    public class ZoneTrigger : Component, ITriggerListener
    {
        public const int BasicEnemyMaxHealth = 5;
        
        private string _zoneName;
        private TmxMap _map;
        private Scene _scene;

        public ZoneTrigger(string zoneName, TmxMap map, Scene scene)
        {
            _zoneName = zoneName;
            _map = map;
            _scene = scene;
        }

        void ITriggerListener.OnTriggerEnter(Collider other, Collider local)
        {
            var objectGroup = _map.GetObjectGroup(_zoneName);
            if (objectGroup == null)
            {
                Debug.Warn($"No object group named {_zoneName} found.");
                return;
            }
            
            foreach (var obj in objectGroup.Objects)
            {
                var objPosition = new Vector2(obj.X + 16, obj.Y + 16);
                var enemyEntity = _scene.CreateEntity(obj.Name, objPosition);
                enemyEntity.AddComponent(new StationaryEnemy());
                enemyEntity.AddComponent(new ProjectileHitDetector(BasicEnemyMaxHealth));
                var enemyCollider = enemyEntity.AddComponent<CircleCollider>();
                Flags.SetFlagExclusive(ref enemyCollider.CollidesWithLayers, CollideWithLayer.StationaryEnemy);
                Flags.SetFlagExclusive(ref enemyCollider.PhysicsLayer, PhysicsLayer.StationaryEnemy);
            }

            Entity.Destroy();
        }

        void ITriggerListener.OnTriggerExit(Collider other, Collider local)
        { }
    }
}
