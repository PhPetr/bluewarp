using Nez;
using System;
using System.Collections.Generic;

namespace bluewarp
{
    public static class HitObserver
    {
        /// <summary>
        /// Stores wrapper references for cleanup
        /// </summary>
        private static Dictionary<(Component, Delegate), Action<Entity, int>> _wrapperMap = new Dictionary<(Component, Delegate), Action<Entity, int>>();

        /// <summary>
        /// Subscribes to an entity's hit event if it implements IHittable.
        /// </summary>
        /// <param name="component">The component that implements IHittable</param>
        /// <param name="onHit">Callback invoked when the entity is hit</param>
        /// <returns>The wrapper function that was subscribed (for manual unsubscription)</returns>
        public static Action<Entity, int> Subscribe(Component component, Action<Entity, int> onHit)
        {
            if (component is IHittable hittable)
            {
                Action<Entity, int> wrapper = (e, currentHealth) =>
                {
                    onHit(e, currentHealth);
                };

                hittable.OnHit += wrapper;
                _wrapperMap[(component, onHit)] = wrapper;
                return wrapper;
            }
            else
            {
                Debug.Warn($"Component '{component.GetType().Name}' does not implement IHittable");
                return null;
            }
        }

        /// <summary>
        /// Unsubscribes from hit events
        /// </summary>
        /// <param name="component">The componenet that implements IHittable</param>
        /// <param name="onHit">The callback to remove</param>
        public static void Unsubscribe(Component component, Action<Entity, int> onHit)
        {
            if (component is IHittable hittable)
            {
                var key = (component, (Delegate)onHit);
                if (_wrapperMap.ContainsKey(key))
                {
                    hittable.OnHit -= _wrapperMap[key];
                    _wrapperMap.Remove(key);
                }
            }
            Debug.Log($"[Player ship hit detector unsubscribed]");
        }

        /// <summary>
        /// Unsubscribes from hit events using the wrapper function returned by Subscribe
        /// </summary>
        /// <param name="component">The component that implements IHittable</param>
        /// <param name="wrapper">The wrapper function returned by Subscribe</param>
        public static void UnsubscribeWrapper(Component component, Action<Entity, int> wrapper)
        {
            if (component is IHittable hittable && wrapper != null)
            {
                hittable.OnHit -= wrapper;

                var itemsToRemove = new List<(Component, Delegate)>();
                foreach(var kvp in _wrapperMap)
                {
                    if (kvp.Value == wrapper)
                        itemsToRemove.Add(kvp.Key);
                }
                foreach (var item in itemsToRemove)
                    _wrapperMap.Remove(item);
            }
        }
    }
}
