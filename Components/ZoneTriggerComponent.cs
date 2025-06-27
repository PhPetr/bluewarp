using Nez;
using Nez.Tiled;
using Microsoft.Xna.Framework;

namespace bluewarp
{
    /// <summary>
    /// Adds a ITriggerListener to a zone from TmxMap.
    /// If triggered spawns enemies from the zone.
    /// </summary>
    public class ZoneTriggerComponent : Component, ITriggerListener
    {        
        private string _zoneName;
        private TmxMap _map;
        private Scene _scene;

        /// <summary>
        /// Add ZoneTrigger to a component.
        /// </summary>
        /// <param name="zoneName">Name of zone</param>
        /// <param name="map">Source of zone</param>
        /// <param name="scene">RunGameScene to which to spawn enemies</param>
        public ZoneTriggerComponent(string zoneName, TmxMap map, Scene scene)
        {
            _zoneName = zoneName;
            _map = map;
            _scene = scene;
        }

        /// <summary>
        /// When triggered spawns enemies from the zone.
        /// </summary>
        void ITriggerListener.OnTriggerEnter(Collider other, Collider local)
        {
            if (_zoneName == GameConstants.BossEnemy.BossZoneName)
            {
                var bossSpawner = _scene.CreateEntity("bossSpawner").AddComponent<BossMagnusSpawner>();
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
