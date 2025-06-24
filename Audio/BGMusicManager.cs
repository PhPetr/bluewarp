using Nez;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;


namespace bluewarp
{
    public static class BGMusicManager
    {
        private static Dictionary<string, Song> _songs = new Dictionary<string, Song>();
        private static string _currentSongKey = null;

        /// <summary>
        /// Loads song by ID. Call once at start of game.
        /// </summary>
        /// <param name="songKey">Key to ID song</param>
        /// <param name="songContentPath">Path to song</param>
        public static void LoadSong(string songKey, string songContentPath)
        {
            if (!_songs.ContainsKey(songKey))
            {
                var song = Core.Content.Load<Song>(songContentPath);
                _songs.Add(songKey, song);
            }
        }

        /// <summary>
        /// Plays a song by key. If already playing, does nothing.
        /// </summary>
        /// <param name="songKey">Song key</param>
        /// <param name="isRepeating"></param>
        /// <param name="volume">Volume, range 0f to 1f</param>
        public static void Play(string songKey, bool isRepeating = true, float volume = GameConstants.BGM.DefaultBaseBGVolume)
        {

            if (!_songs.ContainsKey(songKey))
            {
                Debug.Warn($"[BGM manager] Song '{songKey}' not found. Forgot to load it?");
                return;
            }

            if (_currentSongKey == songKey && MediaPlayer.State == MediaState.Playing)
            {
                if (MediaPlayer.Volume != volume)
                {
                    MediaPlayer.Volume = MathHelper.Clamp(volume, 0f, 1f);
                }
                return;
            }

            var song = _songs[songKey];
            MediaPlayer.Stop();
            MediaPlayer.Volume = MathHelper.Clamp(volume, 0f, 1f);
            MediaPlayer.IsRepeating = isRepeating;
            MediaPlayer.Play(song);
            _currentSongKey = songKey;
        }

        public static void Stop()
        {
            MediaPlayer.Stop();
            _currentSongKey = null;
        }

        public static void SetVolume(float volume)
        {
            MediaPlayer.Volume = MathHelper.Clamp(volume, 0f, 1f);
        }

        public static void Pause() => MediaPlayer.Pause();
        public static void Resume() => MediaPlayer.Resume();
    }
}
