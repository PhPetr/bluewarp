using Microsoft.Xna.Framework;

namespace bluewarp
{
    /// <summary>
    /// Stores default game constants for easier access.
    /// </summary>
    public static class GameConstants
    {
        /// <summary>
        /// Caps max Scale value.
        /// </summary>
        public const int MaxScale = 8;
        /// <summary>
        /// Caps min Scale to 2. Less than that UI breaks.
        /// </summary>
        public const int MinScale = 2;
        /// <summary>
        /// Default Scale set to 4. Perfect for 1080p monitors.
        /// </summary>
        public const int DefaultScale = 4;
        /// <summary>
        /// Game width at scale 1.
        /// </summary>
        public const int GameWidth = 256;
        /// <summary>
        /// Game height at scale 1.
        /// </summary>
        public const int GameHeight = 192;
        /// <summary>
        /// Default tile size of TmxMap.
        /// </summary>
        public const int TileSize = 32;

        /// <summary>
        /// Upwards move speed for CameraMover and FighterShip.
        /// At 50f, Level1 takes 2:05 minutes.
        /// </summary>
        public const float DefaultUpwardsScrollSpeed = 50f; 
        /// <summary>
        /// Delay before CameraMover start moving. In seconds.
        /// </summary>
        public const float MoveStartDelay = 2f;

        public const int DefaultUIPadding = 20;
        public const int GameUIPadding = 10;
        
        /// <summary>
        /// Possible game end states.
        /// </summary>
        public enum GameEndState
        {
            Victory,
            Defeat
        }

        /// <summary>
        /// Default values for Camera.
        /// </summary>
        public static class Camera
        {
            public const int XLockedOffset = 159;
            public const int DefaultStartHeightY = 200 * TileSize;
            public const int DefaultStartWidthX = 4 * TileSize;
            public const int DefaultStopHeightY = 64;
        }

        /// <summary>
        /// Default values for Projectiles.
        /// </summary>
        public static class Projectile
        {
            /// <summary>
            /// Limits projectile travel distance. In pixels.
            /// </summary>
            public const int DefaultTravelLimit = 160;
        }

        /// <summary>
        /// Default values for Player.
        /// </summary>
        public static class Player
        {
            /// <summary>
            /// Ship move speed for input. Ideally more than 2 times UpwardMoveSpeed.
            /// </summary>
            public const float ShipMoveSpeed = 140f;
            /// <summary>
            /// Cap for max ship multiplier.
            /// </summary>
            public const int MaxHealtMultiplier = 10;
            public const int ShipBaseHealth = 3;
            /// <summary>
            /// Default stop value before Boss enemy.
            /// </summary>
            public const int StopHeightY = 32 * 7 - 16;

            /// <summary>
            /// Ships blaster projectile speed.
            /// </summary>
            public const int ProjectileSpeed = 300;
            /// <summary>
            /// Offset to not spawn blaster projectiles inside ship.
            /// </summary>
            public const int ProjectileSpawnOffset = -24;
            /// <summary>
            /// Sets fire rate. Delay in seconds.
            /// </summary>
            public const float ProjectileDelay = 0.2f;
            /// <summary>
            /// For proper size of collider.
            /// </summary>
            public const int MainProjectileRadius = 4;
            /// <summary>
            /// Use for proper collider alignment.
            /// </summary>
            public const int MainProjectileColliderYOffset = -3;

            /// <summary>
            /// Sets chance of ship getting special skin. 
            /// Chance is 1 in (SkinChance - 1).
            /// If higher => less likely to get skin.
            /// Must be set to 1 or greater.
            /// </summary>
            public const int SkinChance = 5;
        }

        /// <summary>
        /// Default values for SFX.
        /// </summary>
        public static class SFX 
        {
            public const float MaxSFXMasterVolume = 2.0f;
            public const float DefaultSFXMasterVolume = 1.0f;
            public const float DefaultBaseVolume = 0.3f;
            public const float BlasterBaseVolume = 0.3f;
            public const float ExplosionBaseVolume = 0.3f;
            public const float DamageImpactBaseVolume = 0.7f;

            /// <summary>
            /// Blaster SFX ID, use for playback.
            /// </summary>
            public const string Blaster = "blaster";
            public const string BlasterPath = "SFX/retro-shot-blaster";
            /// <summary>
            /// Explosion SFX ID, use for playback.
            /// </summary>
            public const string Explosion = "explosion";
            public const string ExplosionPath = "SFX/retro-space-explosion";
            /// <summary>
            /// DamageImpact SFX ID, use for playback.
            /// </summary>
            public const string DamageImpact = "dmgImpact";
            public const string DamageImpactPath = "SFX/damage-impact";
        }

        /// <summary>
        /// Default values for BGM.
        /// </summary>
        public static class BGM 
        {
            public const float MaxBGMasterVolume = 2.0f;
            public const float DefaultBGMMasterVolume = 1.0f;
            public const float DefaultBaseBGVolume = 0.1f;

            /// <summary>
            /// BlueChill track ID, use for playback.
            /// </summary>
            public const string BlueChill = "blueChill";
            public const string BlueChillPath = "BGM/blue_chill";

            /// <summary>
            /// BlueTension track ID, use for playback.
            /// </summary>
            public const string BlueTension = "blueTension";
            public const string BlueTensionPath = "BGM/high_blue_tension_loop_ver";
        }

        /// <summary>
        /// Default values for BasicEnemy type.
        /// </summary>
        public static class BasicEnemy
        {
            public const int DefaultMaxHealth = 5;
            /// <summary>
            /// Amout that is rewarded after destroying BasicEnemy.
            /// </summary>
            public const int RewardPoints = 100;
            public const string DefaultTexture = Nez.Content.BasicEnemy.static_enemy;
            /// <summary>
            /// Determines BasicEnemy fire rate.
            /// </summary>
            public const float DefaultProjectileDelay = 1.0f;
        }

        /// <summary>
        /// Default values for BossEnemy type.
        /// </summary>
        public static class BossEnemy
        {
            public const string BossZoneName = "bossZone";

            /// <summary>
            /// Default values for Boss Magnus.
            /// </summary>
            public static class Magnus
            {
                /// <summary>
                /// Magnus main body max health points.
                /// </summary>
                public const int MainMaxHealth = 15;
                /// <summary>
                /// Magnus hands max health points.
                /// </summary>
                public const int SecondaryMaxHealth = 10;
                /// <summary>
                /// Reward for destroying Magnus main body.
                /// </summary>
                public const int MainRewardPoints = 500;
                /// <summary>
                /// Reward for destroying Magnus hand.
                /// </summary>
                public const int SecondaryRewardPoints = 200;
                /// <summary>
                /// Determines Magnus main body fire rate.
                /// </summary>
                public const float MainProjectileDelay = 0.8f;
                /// <summary>
                /// Determines Magnus hand fire rate.
                /// </summary>
                public const float SecondaryProjectileDelay = 0.5f;
            }
        }
    }

    /// <summary>
    /// Stores global game settings, which can be changed by player.
    /// </summary>
    public static class GameSettings
    {
        /// <summary>
        /// Scales game, window and UI size.\
        /// Scale 1 is too small. 4 to 5 is ideal for 1080p monitors.
        /// </summary>
        public static int Scale { get; set; } = GameConstants.DefaultScale;
        public static int ScaledGameWidth => Scale * GameConstants.GameWidth;
        public static int ScaledGameHeight => Scale * GameConstants.GameHeight;

        /// <summary>
        /// Modifiable Player settings.
        /// </summary>
        public static class Player
        {
            /// <summary>
            /// Multiplies ShipBaseHealth to get final ShipMaxHealth.
            /// Used to lower game difficulty.
            /// </summary>
            public static int HealthMultiplier { get; set; } = 1;
            public static int ShipMaxHealth => GameConstants.Player.ShipBaseHealth * HealthMultiplier;
        }

        /// <summary>
        /// Modifiable SFX volume settings.
        /// </summary>
        public static class SFX
        {
            /// <summary>
            /// Master SFX volume. Every SFX base volume is scaled by SFXMasterVolume.
            /// </summary>
            public static float SFXMasterVolume { get; set; } = GameConstants.SFX.DefaultSFXMasterVolume;

            public static float DefaultVolume => GameConstants.SFX.DefaultBaseVolume * SFXMasterVolume;
            public static float BlasterVolume => GameConstants.SFX.BlasterBaseVolume * SFXMasterVolume;
            public static float ExplosionVolume => GameConstants.SFX.ExplosionBaseVolume * SFXMasterVolume;
            public static float DamageImpactVolume => GameConstants.SFX.DamageImpactBaseVolume * SFXMasterVolume;

            /// <summary>
            /// Sets SFXMasterVolume and clamps it to MaxSFXMasterVolume.
            /// </summary>
            /// <param name="volume">New SFX volume</param>
            public static void SetSFXMasterVolume(float volume)
            {
                SFXMasterVolume = MathHelper.Clamp(volume, 0.0f, GameConstants.SFX.MaxSFXMasterVolume);
            }
        }

        /// <summary>
        /// Modifiable BGM volume settings.
        /// </summary>
        public static class BGM
        {
            /// <summary>
            /// Master BGM volume. DefaultBaseBGVolume is scaled by BGMasterVolume.
            /// </summary>
            public static float BGMasterVolume { get; set; } = GameConstants.BGM.DefaultBGMMasterVolume;

            public static float BGVolume => GameConstants.BGM.DefaultBaseBGVolume * BGMasterVolume;

            /// <summary>
            /// Sets BGMasterVolume and clamps it to MaxBGMasterVolume.
            /// </summary>
            /// <param name="volume"></param>
            public static void SetBGMMasterVolume(float volume)
            {
                BGMasterVolume = MathHelper.Clamp(volume, 0.0f, GameConstants.BGM.MaxBGMasterVolume);
            }
        }
    }
}
