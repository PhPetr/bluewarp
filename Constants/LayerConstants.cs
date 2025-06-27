namespace bluewarp
{
    /// <summary>
    /// Constant RenderLayer values of individual renderable elements.
    /// Lower number is rendered on top of higher numbers. (Apart from UI)
    /// </summary>
    public static class RenderLayer // top = 0
    {
        public const int TileMap = 10;
        public const int PlayerProjectile = 2;
        public const int StationaryEnemySprite = 3;
        public const int StationaryEnemyExplosion = 2;
        public const int PlayerAnimator = 1;
        public const int BossHands = 1;
        public const int BossBody = 3;
        /// <summary>
        /// UI is different. 999 means it is drawn on the top.
        /// </summary>
        public const int DefaultUIRenderLayer = 999;
    }

    /// <summary>
    /// Constant PhysicsLayer values of individual entities with colliders.
    /// Used for SetFlagExclusive.
    /// If on the same layer, they do not collide with.
    /// </summary>
    public static class PhysicsLayer 
    {
        public const int Zones = 6;
        public const int PlayerShipCollider = 1;
        public const int PlayerEventCollider = 5;
        public const int PlayerProjectile = 1;
        public const int StationaryEnemy = 0;
        public const int StationaryEnemyProjectile = 0;
        public const int BossMainBodyInactive = 1;
        public const int BossMainBodyActive = 0;
    }

    /// <summary>
    /// Constant CollideWithLayer values of individual entities with colliders.
    /// Used for SetFlagExclusive.
    /// It is set to which physics layer they affect.
    /// </summary>
    public static class CollideWithLayer 
    {
        public const int Zones = 5;
        public const int PlayerShipCollider = 0;
        public const int PlayerEventCollider = 6;
        public const int PlayerProjectile = 0;
        public const int StationaryEnemy = 1;
        public const int StationaryEnemyProjectile = 1;
        public const int BossMainBody = 1;
    }
}
