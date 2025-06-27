using Nez;
using Nez.Tiled;
using Microsoft.Xna.Framework;
using Nez.AI.Pathfinding;

namespace bluewarp
{
    /// <summary>
    /// Handles spawning of Stationary (Basic) Enemy.
    /// </summary>
    public static class SpawnStationaryEnemy
    {
        /// <summary>
        /// Spawns Enemies from the zone.
        /// Adds DestructionObserver to the enemies.
        /// </summary>
        /// <param name="zoneName">Name of the zone</param>
        /// <param name="map">TmxMap to get enemies from</param>
        /// <param name="scene">Scene to which to add enemies</param>
        /// <param name="maxHealth">Max HP of the enemy</param>
        public static void SpawnEnemiesFromZone(string zoneName, 
            TmxMap map, 
            Scene scene, 
            int maxHealth = GameConstants.BasicEnemy.DefaultMaxHealth)
        {
            var objectGroup = map.GetObjectGroup(zoneName);
            if (objectGroup == null)
            {
                Debug.Warn($"No object group named {zoneName} found.");
                return;
            }
            
            var battleScene = scene as RunGameScene;
            if (battleScene == null)
            {
                Debug.Warn("Not correct RunGameScene");
            }

            foreach (var obj in objectGroup.Objects)
            {
                var objPosition = new Vector2(obj.X + 16, obj.Y + 16);
                var enemyEntity = scene.CreateEntity(obj.Name, objPosition);
                var turret = new StationaryEnemy();

                DestructionObserver.Subscribe(turret, e =>
                {
                    Debug.Log($"[Basic enemy destroyed] Entity: {e.Name}");
                    if (battleScene != null)
                    {
                        battleScene.AddToScore(GameConstants.BasicEnemy.RewardPoints);
                        Debug.Log($"[Awarded {GameConstants.BasicEnemy.RewardPoints} points] Entity: {e.Name}, Current score: {battleScene.GetScore()}");
                    }

                });

                enemyEntity.AddComponent(turret);
                enemyEntity.AddComponent(new ProjectileHitDetector(maxHealth));
                var enemyCollider = enemyEntity.AddComponent<CircleCollider>();
                Flags.SetFlagExclusive(ref enemyCollider.CollidesWithLayers, CollideWithLayer.StationaryEnemy);
                Flags.SetFlagExclusive(ref enemyCollider.PhysicsLayer, PhysicsLayer.StationaryEnemy);
            }
        }
    }
}
