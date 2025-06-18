

namespace Nez
{
    /// <summary>
    /// class that contains the names of all of the files processed by the Pipeline Tool
    /// </summary>
    /// <remarks>
    /// Nez includes a T4 template that will auto-generate the content of this file.
    /// See: https://github.com/prime31/Nez/blob/master/FAQs/ContentManagement.md#auto-generating-content-paths"
    /// </remarks>
    class Content
    {
		public static class BasicEnemy
		{
			public const string explosion = @"Content\BasicEnemy\explosion.png";
			public const string static_enemy = @"Content\BasicEnemy\static_enemy.png";
		}

		public static class Level1
		{
			public const string basetiles = @"Content\Level1\baseTiles.tsx";
			public const string bgone = @"Content\Level1\BGOne.tmx";
			public const string levelone = @"Content\Level1\LevelOne.tmx";
			public const string magnus_defeated = @"Content\Level1\magnus_defeated.tsx";
			public const string magnus_defeated_set = @"Content\Level1\magnus_defeated_set.png";
			public const string maptileset = @"Content\Level1\mapTileSet.png";
		}

		public static class PlayerShip
		{
			public const string player_main_projectile = @"Content\PlayerShip\player_main_projectile.png";
			public const string player_secondary_projectile = @"Content\PlayerShip\player_secondary_projectile.png";
			public const string playership = @"Content\PlayerShip\player-ship.png";
		}

		public const string content = @"Content\Content.mgcb";

    }
}

