using Nez;
using Nez.Tiled;
using Microsoft.Xna.Framework;

namespace bluewarp
{
    public class ZoneTriggerComponent : Component, ITriggerListener
    {        
        private string _zoneName;
        private TmxMap _map;
        private Scene _scene;

        public ZoneTriggerComponent(string zoneName, TmxMap map, Scene scene)
        {
            _zoneName = zoneName;
            _map = map;
            _scene = scene;
        }

        void ITriggerListener.OnTriggerEnter(Collider other, Collider local)
        {
            if (_zoneName == GameConstants.BossEnemy.BossZoneName)
            {
                var bossSpawner = _scene.CreateEntity("bossSpawner").AddComponent<BossSpawner>();
                var bossPhaseHandler = bossSpawner.AddComponent<MagnusPhaseHandler>();
                bossSpawner.SpawnBossMagnus(_zoneName, _map, _scene, bossPhaseHandler);
            }
            else
            {
                SpawnStationaryEnemy.SpawnEnemiesFromZone(_zoneName, _map, _scene);
            }

            Entity.Destroy();
        }

        void ITriggerListener.OnTriggerExit(Collider other, Collider local)
        { }
    }
}
