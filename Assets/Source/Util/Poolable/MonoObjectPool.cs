using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Assets.Source.Util.Poolable
{    
    public class MonoObjectPool<T> where T : class, IPoolItem
    {
        private readonly PlaceholderFactory<UnityEngine.Object, T> _poolItemFactory;
        private readonly List<T> _poolItems;

        public MonoObjectPool(PlaceholderFactory<UnityEngine.Object, T> poolItemFactory)
        {
            _poolItemFactory = poolItemFactory;
            _poolItems = new List<T>();
        }

        public T GetItem(Func<T, bool> functor, UnityEngine.Object prefab)
        {
            var freePoolItem = _poolItems
                .Where(item => item.IsFree)
                .FirstOrDefault(functor);

            return freePoolItem ?? CreatePoolItem(prefab);
        }

        private T CreatePoolItem(UnityEngine.Object prefab)
        {
            var poolItem = _poolItemFactory.Create(prefab);
            _poolItems.Add(poolItem);

            return poolItem;
        }

        // ToDo Very hacky, find a better way
        public void ForEach(Action<T> action)
        {
            _poolItems.ForEach(action);
        }
    }
}
