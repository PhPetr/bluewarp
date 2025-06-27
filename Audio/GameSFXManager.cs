using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Nez;
using System.Collections.Generic;

namespace bluewarp
{
    /// <summary>
    /// SFX manager for RunGameScene.
    /// </summary>
    public static class GameSFXManager
    {
        private static Dictionary<string, SoundEffect> _soundEffects = new Dictionary<string, SoundEffect>();

        /// <summary>
        /// Load preset default SFXs. Call this once at the start of game scene.
        /// </summary>
        public static void LoadContent()
        {
            LoadSFX(GameConstants.SFX.Blaster, GameConstants.SFX.BlasterPath);
            LoadSFX(GameConstants.SFX.Explosion, GameConstants.SFX.ExplosionPath);
            LoadSFX(GameConstants.SFX.DamageImpact, GameConstants.SFX.DamageImpactPath);
        }

        /// <summary>
        /// Manually loads SFX by ID.
        /// </summary>
        /// <param name="SFXKey">Key to ID SFX</param>
        /// <param name="SFXContentPath">Path to SFX</param>
        public static void LoadSFX(string SFXKey, string SFXContentPath)
        {
            if (!_soundEffects.ContainsKey(SFXKey))
            {
                var sfx = Core.Content.Load<SoundEffect>(SFXContentPath);
                _soundEffects.Add(SFXKey, sfx);
            }
        }

        /// <summary>
        /// Plays SFX if loaded. Allows overlapping multiple SFX.
        /// </summary>
        /// <param name="SFXKey">SFX key</param>
        /// <param name="volume">Volume of played SFX</param>
        public static void PlaySFX(string SFXKey, float volume = GameConstants.SFX.DefaultBaseVolume)
        {
            if (!_soundEffects.ContainsKey(SFXKey))
            {
                Debug.Warn($"[SFX manager] SFX '{SFXKey}' not found.");
                return;
            }
            var sfx = _soundEffects[SFXKey];
            var instance = sfx.CreateInstance();
            instance.Volume = MathHelper.Clamp(volume, 0.0f, 1.0f);
            instance.Play();
        }
    }
}
