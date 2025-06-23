using Microsoft.Xna.Framework;
using Nez;

namespace bluewarp
{
    public static class GameConstants
    {
        public const int Scale = 5;
        public const int GameWidth = 256;
        public const int GameHeight = 192;
        public const int ScaledGameWidth = Scale * GameWidth;
        public const int ScaledGameHeight = Scale * GameHeight;
        public const int XLockedOffset = 159;
        public const int TileSize = 32;

        public const float DefaultUpwardsScrollSpeed = 50f; // at 50f => 2:05 minutes for level1 
        public const float MoveStartDelay = 2f;
        
        public enum GameEndState
        {
            Victory,
            Defeat
        }

        public static class Camera
        {
            public const int DefaultStartHeightY = 200 * TileSize;
            public const int DefaultStartWidthX = 4 * TileSize;
            public const int DefaultStopHeightY = 64;
        }

        public static class Projectile
        {
            public const int DefaultTravelLimit = 160;
        }
        public static class Player
        {
            public const float ShipMoveSpeed = 125f;
            public const int ShipMaxHealth = 5;
            public const int StopHeightY = 32 * 7 - 16;
            public const int ProjectileSpeed = 300;
            public const int ProjectileSpawnOffset = -24;
            public const float ProjectileDelay = 0.2f;
            public const int SkinChance = 5; // must be 1>, chance of getting special skin == 1/(SkinChance-1)
        }

        public static class SFX 
        {
            public const float DefaultVolume = 0.3f;

            public const string Blaster = "blaster";
            public const string BlasterPath = "SFX/retro-shot-blaster";
            public const float BlasterVolume = 0.3f;

            public const string Explosion = "explosion";
            public const string ExplosionPath = "SFX/retro-space-explosion";
            public const float ExplosionVolume = 0.3f;

            public const string DamageImpact = "dmgImpact";
            public const string DamageImpactPath = "SFX/damage-impact";
            public const float DamageImpactVolume = 0.7f;
        }

        public static class BGM
        {
            public const float DefaultBGVolume = 0.1f;

            public const string BlueChill = "blueChill";
            public const string BlueChillPath = "BGM/blue_chill";

            public const string BlueTension = "blueTension";
            public const string BlueTensionPath = "BGM/high_blue_tension_loop_ver";
        }

        public static class BasicEnemy
        {
            public const int DefaultMaxHealth = 5;
            public const int RewardPoints = 100;
            public const string DefaultTexture = Nez.Content.BasicEnemy.static_enemy;
        }

        public static class BossEnemy
        {
            public const string BossZoneName = "bossZone";
            public static class Magnus
            {
                public const int MainMaxHealth = 15;
                public const int SecondaryMaxHealth = 10;
                public const int MainRewardPoints = 500;
                public const int SecondaryRewardPoints = 200;
            }
        }
    }
}
