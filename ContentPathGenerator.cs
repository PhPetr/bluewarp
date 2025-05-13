

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
		public static class Level1
		{
			public const string basetiles = @"Content\Level1\baseTiles.tsx";
			public const string bgone = @"Content\Level1\BGOne.tmx";
			public const string levelone = @"Content\Level1\LevelOne.tmx";
			public const string magnus_defeated = @"Content\Level1\magnus_defeated.tsx";
			public const string magnus_defeated_set = @"Content\Level1\magnus_defeated_set.png";
			public const string maptileset = @"Content\Level1\mapTileSet.png";
		}

		public const string content = @"Content\Content.mgcb";

    }
}

