using Nez;
using Microsoft.Xna.Framework;
using System;

namespace bluewarp
{
    /// <summary>
	/// Moves a ProjectileMover and destroys the Entity if it hits anything or exceeds Game height.
	/// </summary>
    public class ProjectileController : Component, IUpdatable
    {
        public Vector2 Velocity;

        ProjectileMover _mover;

        public ProjectileController(Vector2 velocity) => Velocity = velocity;

        public override void OnAddedToEntity()
        {
            _mover = Entity.GetComponent<ProjectileMover>();
        }

        void IUpdatable.Update()
        {
            var _projectileTotalTimeAlive = Entity.GetComponent<TimeAliveComponent>().TotalTimeAlive;
            if (MathF.Abs(Velocity.Y) * _projectileTotalTimeAlive >= GameConstants.Projectile.DefaultTravelLimit)
                Entity.Destroy();
            if (_mover.Move(Velocity * Time.DeltaTime))
                Entity.Destroy();
            
        }
    }
}
