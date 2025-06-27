using Nez;
using System;

namespace bluewarp
{
    /// <summary>
    /// Interface for destroyable entities.
    /// </summary>
    public interface IDestructable
    {
        /// <summary>
        /// Triggers Explosion animation (if any) and destroys the entity.
        /// </summary>
        void PlayExplosionAndDestroy();

        /// <summary>
        /// Event triggered when the entity is destroyed
        /// Parameter: Entity that is being destroyed
        /// </summary>
        event Action<Entity> OnDestroyed;
    }
}
