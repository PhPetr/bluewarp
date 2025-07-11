﻿using Nez;
using System;

namespace bluewarp
{
    /// <summary>
    /// Inteface for hittable entities.
    /// </summary>
    public interface IHittable
    {
        /// <summary>
        /// Event triggered when the entity is hit
        /// Parameters: Entity that was hit, Current health after hit
        /// </summary>
        event Action<Entity, int> OnHit;

        int CurrentHealth { get; }

        int MaxHealth { get; }
    }
}
