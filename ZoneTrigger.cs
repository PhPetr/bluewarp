using Nez;
using Nez.Tiled;
using Microsoft.Xna.Framework;

namespace bluewarp
{
    public class ZoneTrigger : Component, ITriggerListener
    {
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
                enemyEntity.AddComponent(new ProjectileHitDetector(10));
                var enemyCollider = enemyEntity.AddComponent<CircleCollider>();
                Flags.SetFlagExclusive(ref enemyCollider.CollidesWithLayers, 1);
                Flags.SetFlagExclusive(ref enemyCollider.PhysicsLayer, 0);
            }

            Entity.Destroy();
        }

        void ITriggerListener.OnTriggerExit(Collider other, Collider local)
        { }
    }
}
