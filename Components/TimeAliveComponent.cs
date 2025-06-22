using Nez;

namespace bluewarp
{
    internal class TimeAliveComponent : Component, IUpdatable
    {
        public float TotalTimeAlive { get; private set; } = 0f;

        void IUpdatable.Update()
        {
            TotalTimeAlive += Time.DeltaTime;
        }
    }
}
