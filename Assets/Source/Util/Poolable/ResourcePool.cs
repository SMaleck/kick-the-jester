using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Util.Poolable
{
    // ToDo implement maxSlots
    public class ResourcePool<T> where T : class, IPoolableResource
    {
        private readonly List<T> _pool;
        private readonly IFactory<T> _factory;

        public readonly int MaxSlots;
        private bool UseSingleSlot => MaxSlots == 1;

        public ResourcePool(IFactory<T> factory, int maxSlots = -1)
        {
            _pool = new List<T>();

            _factory = factory;
            MaxSlots = maxSlots;            
        }


        public void ForEach(Action<T> action)
        {
            _pool.ForEach(action);
        }


        public IEnumerable<T> AsEnumerable()
        {
            return _pool.AsEnumerable();
        }


        public T GetFreeSlot()
        {            
            var slot = _pool.FirstOrDefault(e => UseSingleSlot || e.IsFree);
            slot = slot ?? AddNewSlot();

            return slot;
        }


        private T AddNewSlot()
        {
            _pool.Add(_factory.CreateResource());

            return _pool.Last();
        }        
    }
}
