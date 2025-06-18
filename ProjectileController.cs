using Nez;
using Microsoft.Xna.Framework;

namespace bluewarp
{
    public class ProjectileController : Component, IUpdatable
    {
        public Vector2 Velocity;

        ProjectileMover _mover;

        public ProjectileController(Vector2 velocity) => Velocity = velocity;

        public override void OnAddedToEntity() => _mover = Entity.GetComponent<ProjectileMover>();

        void IUpdatable.Update()
        {
            if (_mover.Move(Velocity * Time.DeltaTime))
                Entity.Destroy();
        }
    }
}
