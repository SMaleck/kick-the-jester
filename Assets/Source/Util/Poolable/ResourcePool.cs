using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Util.Poolable
{
    public class ResourcePool<T> where T : class, IPoolableResource
    {
        private readonly List<T> _pool;
        private readonly IFactory<T> _factory;

        public readonly bool UseSingleSlot;

        public ResourcePool(IFactory<T> factory, bool useSingleSlot = false)
        {
            _pool = new List<T>();

            _factory = factory;
            UseSingleSlot = useSingleSlot;            
        }


        public void ForEach(Action<T> action)
        {
            _pool.ForEach(action);
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
