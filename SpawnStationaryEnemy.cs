using Nez;
using Nez.Tiled;
using Microsoft.Xna.Framework;
using Nez.AI.Pathfinding;

namespace bluewarp
{
    public static class SpawnStationaryEnemy
    {
        public const int DefaultStationaryEnemyMaxHealth = 5;
        public const int EnemyKillScore = 100;

        public static void SpawnEnemiesFromZone(string zoneName, TmxMap map, Scene scene, int maxHealth = DefaultStationaryEnemyMaxHealth)
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
                        battleScene.AddToScore(EnemyKillScore);
                        Debug.Log($"[Awarded {EnemyKillScore} points] Entity: {e.Name}, Current score: {battleScene.GetScore()}");
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
