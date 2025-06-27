using Microsoft.Xna.Framework;
using Nez;

namespace bluewarp
{
    /// <summary>
    /// Component to set camera bounds.
    /// </summary>
    public class CameraBounds: Component, IUpdatable
    {
        public Vector2 Min, Max;
        public int lockedX;

        /// <summary>
        /// Constructs CameraBounds. Sets update order to last.
        /// </summary>
        public CameraBounds()
        {
            // make sure we run last so the camera is already moved before we evaluate its position
            SetUpdateOrder(int.MaxValue);
        }

        /// <summary>
        /// Constructs CameraBounds with bounds values and camera offset. Sets update order to last.
        /// </summary>
        /// <param name="min">Set bound minimal value</param>
        /// <param name="max">Set bound maximal value</param>
        /// <param name="lockedX">Set X offset of camera</param>
        public CameraBounds(Vector2 min, Vector2 max, int lockedX) : this()
        {
            Min = min;
            Max = max;
            this.lockedX = lockedX;
            SetUpdateOrder(int.MaxValue);
        }

        /// <summary>
        /// Sets update order to last.
        /// </summary>
        public override void OnAddedToEntity()
        {
            Entity.UpdateOrder = int.MaxValue;
        }


        void IUpdatable.Update()
        {
            var cameraBounds = Entity.Scene.Camera.Bounds;
            Entity.Scene.Camera.Position = new Vector2(lockedX, Entity.Scene.Camera.Position.Y);
            if (cameraBounds.Top < Min.Y)
                Entity.Scene.Camera.Position += new Vector2(0, Min.Y - cameraBounds.Top);
            if (cameraBounds.Bottom > Max.Y)
                Entity.Scene.Camera.Position += new Vector2(0, Max.Y - cameraBounds.Bottom);
        }
    }
}