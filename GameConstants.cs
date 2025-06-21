namespace bluewarp
{
    public static class GameConstants
    {
        public const int Scale = 5;
        public const int GameWidth = 256;
        public const int GameHeight = 192;
        public const int ScaledGameWidth = Scale * GameWidth;
        public const int ScaledGameHeight = Scale * GameHeight;

        public const float ShipMoveSpeed = 125f;
        public const int ShipMaxHealth = 5;

        public const float UpwardsScrollSpeed = 50f;
    }
}
