using System;
using Nez;

namespace bluewarp
{
    public interface IDestructable
    {
        void PlayExplosionAndDestroy();

        event Action<Entity> OnDestroyed;
    }
}
