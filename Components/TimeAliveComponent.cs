using Nez;

namespace bluewarp
{
    /// <summary>
    /// Tracks total time alive of a component.
    /// </summary>
    internal class TimeAliveComponent : Component, IUpdatable
    {
        /// <summary>
        /// Total time alive of a component.
        /// </summary>
        public float TotalTimeAlive { get; private set; } = 0f;

        void IUpdatable.Update()
        {
            TotalTimeAlive += Time.DeltaTime;
        }
    }
}
