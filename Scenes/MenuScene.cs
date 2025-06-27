using Nez;
using Nez.Tiled;
using Microsoft.Xna.Framework;
using bluewarp.UI;

namespace bluewarp.Scenes
{
    /// <summary>
    /// Menu (and starting) scene.
    /// </summary>
    internal class MenuScene : BaseScene
    {
        private Entity _tiledBackgroundEntity;
        private TmxMap _tiledBackground;

        private MenuUIMangaer _UIManager;

        public MenuScene() : base(true, true)
        { }

        /// <summary>
        /// Create Menu scene and MenuUIManager.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            SetDesignResolution(GameConstants.GameWidth, GameConstants.GameHeight, SceneResolutionPolicy.ShowAllPixelPerfect);
            Screen.SetSize(GameSettings.ScaledGameWidth, GameSettings.ScaledGameHeight);
            ClearColor = Color.Black;

            _UIManager = new MenuUIMangaer(this);

            BGMusicManager.Play(GameConstants.BGM.BlueChill, volume: GameSettings.BGM.BGVolume);
            LoadBackground();
        }

        private void LoadBackground()
        {
            _tiledBackgroundEntity = CreateEntity("background");
            _tiledBackground = Content.LoadTiledMap(Nez.Content.Level1.bgone);
            var tiledBGRenderer = _tiledBackgroundEntity.AddComponent(new TiledMapRenderer(_tiledBackground));
            tiledBGRenderer.SetLayersToRender("Tile Layer 1");
            tiledBGRenderer.RenderLayer = RenderLayer.TileMap;
            _tiledBackgroundEntity.Position = new Vector2(32, 0);
        }

        public override void End()
        {
            _UIManager?.Dispose();
            base.End();
        }
    }
}
