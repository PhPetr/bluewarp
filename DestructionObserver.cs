using Nez;
using System;

namespace bluewarp
{
    public static class DestructionObserver
    {
        /// <summary>
        /// Subscribes to an enity's desctruction event if it implements IDestructable.
        /// </summary>
        /// <param name="component">The component that implements IDestructable</param>
        /// <param name="onDestroyed">Callback invoked when the entity is destroyed</param>
        public static void Subscribe(Component component, Action<Entity> onDestroyed)
        {
            if (component is IDestructable destructable)
            {
                void Wrapper(Entity e)
                {
                    onDestroyed(e);
                    destructable.OnDestroyed -= Wrapper;
                }
                destructable.OnDestroyed += Wrapper;
            }
            else
            {
                Debug.Warn($"Component '{component.GetType().Name}' does not implement IDestructable");
            }
        }
    }
}
