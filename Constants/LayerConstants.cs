namespace bluewarp
{
    public static class RenderLayer // top = 0
    {
        public const int TileMap = 10;
        public const int PlayerProjectile = 2;
        public const int StationaryEnemySprite = 3;
        public const int StationaryEnemyExplosion = 2;
        public const int PlayerAnimator = 1;
        public const int BossHands = 1;
        public const int BossBody = 3;

        public const int DefaultUIRenderLayer = 999;
    }

    public static class PhysicsLayer // if on same layer, not affected
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

    public static class CollideWithLayer // set to which physics layer to affect
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
