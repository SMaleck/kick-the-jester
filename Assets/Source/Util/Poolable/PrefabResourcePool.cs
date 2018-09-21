using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Util.Poolable
{
    // ToDo Make pools more generic
    public class PrefabResourcePool<T> where T : class, INamedPoolableResource
    {
        private readonly List<T> _pool;
        private readonly IPrefabFactory<T> _factory;
        public readonly bool UseSingleSlot;

        public PrefabResourcePool(IPrefabFactory<T> factory, bool useSingleSlot = false)
        {
            _pool = new List<T>();

            _factory = factory;
            UseSingleSlot = useSingleSlot;
        }

        public void ForEach(Action<T> action)
        {
            _pool.ForEach(action);
        }

        public T GetFreeSlotFor(GameObject go)
        {
            var slot = _pool.FirstOrDefault(e => UseSingleSlot || e.IsFree && e.Name.Equals(go.name));
            slot = slot ?? AddNewSlot(go);

            return slot;
        }

        private T AddNewSlot(GameObject go)
        {
            _pool.Add(_factory.CreateResource(go));

            return _pool.Last();
        }
    }
}
