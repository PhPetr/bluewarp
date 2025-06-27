using bluewarp.ProjectileShooter;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace bluewarp
{
    /// <summary>
    /// Handles spawning of Boss Magnus.
    /// </summary>
    public class BossMagnusSpawner: Component
    {
        private MagnusPhaseHandler _phaseHandler;
        private CircleCollider _mainBodyCollider;
        private RunGameScene _battleScene;

        /// <summary>
        /// Spawns Boss Magnus.
        /// </summary>
        /// <param name="zoneName">For checking if triggered zone is correct BossZone</param>
        /// <param name="map">TmxMap source for placing boss</param>
        /// <param name="scene">Scene to which to add boss</param>
        /// <param name="phaseHandler">Phase handler of boss Magnus</param>
        public void SpawnBossMagnus(string zoneName, TmxMap map, Scene scene, MagnusPhaseHandler phaseHandler)
        {
            if (zoneName != GameConstants.BossEnemy.BossZoneName)
            {
                Debug.Warn("Not a bossZone triggered boss spawn!");
                return;
            }
            _phaseHandler = phaseHandler;

            _battleScene = scene as RunGameScene;
            if (_battleScene == null)
            {
                Debug.Warn("Not correct RunGameScene");
                return;
            }

            var magnusGroup = map.GetObjectGroup(zoneName);
            var magnusRightHand = magnusGroup.Objects["magnusRight"];
            var magnusLeftHand = magnusGroup.Objects["magnusLeft"];
            var magnusMainBody = magnusGroup.Objects["magnusMain"];

            SpawnMagnusMainBody(magnusMainBody, scene);
            if (_mainBodyCollider == null)
            {
                Debug.Warn("MAIN BODY COLLIDER NOT CREATED!");
                return;
            }

            SpawnMagnusHands(magnusLeftHand, scene, true);
            SpawnMagnusHands(magnusRightHand, scene, false);
        }

        /// <summary>
        /// Handles spawning Magnus hands.
        /// </summary>
        /// <param name="hand">TmxObject of the hand</param>
        /// <param name="scene">Scene to which to add hand</param>
        /// <param name="leftHand">If spawned hand is the left one</param>
        private void SpawnMagnusHands(TmxObject hand, Scene scene, bool leftHand)
        {
            var handTexture = (leftHand) ? Nez.Content.BossEnemy.Magnus.magnus_boss_left : Nez.Content.BossEnemy.Magnus.magnus_boss_right;
            var handPosition = new Vector2 (hand.X + 16, hand.Y + 16);
            var handEntity = scene.CreateEntity(hand.Name, handPosition);
            var enemyHand = new StationaryEnemy(handTexture, RenderLayer.BossHands, 16, GameConstants.BossEnemy.Magnus.SecondaryProjectileDelay);
            handEntity.AddComponent(enemyHand);

            DestructionObserver.Subscribe(enemyHand, e =>
            {
                Debug.Log($"[Boss Hand Destroyed] Entity: {e.Name}");

                _battleScene.AddToScore(RewardCalculator.CalculateRewardBasedOnDifficulty(GameConstants.BossEnemy.Magnus.SecondaryRewardPoints));
                Debug.Log($"[Awarded {GameConstants.BossEnemy.Magnus.SecondaryRewardPoints} points] Entity: {e.Name}, Current score: {_battleScene.GetScore()}");
                
                _phaseHandler.HandDestroyed(e.Name, _mainBodyCollider);
            });

            handEntity.AddComponent(new ProjectileHitDetector(GameConstants.BossEnemy.Magnus.SecondaryMaxHealth));
            var handCollider = handEntity.AddComponent(new CircleCollider(10));
            Flags.SetFlagExclusive(ref handCollider.CollidesWithLayers, CollideWithLayer.StationaryEnemy);
            Flags.SetFlagExclusive(ref handCollider.PhysicsLayer, PhysicsLayer.StationaryEnemy);
        }

        /// <summary>
        /// Handles spawning of Magnus main body.
        /// </summary>
        /// <param name="body">TmxObject of the main body</param>
        /// <param name="scene">Scene to which to add main body</param>
        private void SpawnMagnusMainBody(TmxObject body, Scene scene)
        {
            var bodyTexture = Nez.Content.BossEnemy.Magnus.magnus_boss_main;
            var bodyPosition = new Vector2(body.X + 64, body.Y + 32);
            var bodyEntity = scene.CreateEntity(body.Name, bodyPosition);
            var mainBody = new StationaryEnemy(bodyTexture, RenderLayer.BossBody, 5, GameConstants.BossEnemy.Magnus.MainProjectileDelay);
            bodyEntity.AddComponent(mainBody);

            DestructionObserver.Subscribe(mainBody, e =>
            {
                Debug.Log($"[Main Boss body destroyed] Entity: {e.Name}");
                _battleScene.AddToScore(RewardCalculator.CalculateRewardBasedOnDifficulty(GameConstants.BossEnemy.Magnus.MainRewardPoints));
                Debug.Log($"[Awarded {GameConstants.BossEnemy.Magnus.MainRewardPoints} points] Entity: {e.Name}, Current score: {_battleScene.GetScore()}");
                var battleScene = scene as RunGameScene;
                SceneManager.LoadGameOver(battleScene.GetScore(), GameConstants.GameEndState.Victory);
            });

            bodyEntity.AddComponent(new ProjectileHitDetector(GameConstants.BossEnemy.Magnus.MainMaxHealth));
            _mainBodyCollider = bodyEntity.AddComponent(new CircleCollider(16));
            _mainBodyCollider.LocalOffset = new Vector2(0, -10);
            _mainBodyCollider.SetEnabled(false);
            Flags.SetFlagExclusive(ref _mainBodyCollider.CollidesWithLayers, CollideWithLayer.StationaryEnemy);
            Flags.SetFlagExclusive(ref _mainBodyCollider.PhysicsLayer, PhysicsLayer.StationaryEnemy);
        }
    }
}
