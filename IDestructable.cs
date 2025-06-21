using System;
using Nez;

namespace bluewarp
{
    public interface IDestructable
    {
        void PlayExplosionAndDestroy();

        /// <summary>
        /// Event triggered when the entity is destroyed
        /// Parameter: Entity that is being destroyed
        /// </summary>
        event Action<Entity> OnDestroyed;
    }
}
