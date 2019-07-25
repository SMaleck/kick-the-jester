using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Assets.Source.Util.MonoObjectPooling
{
    public class MonoObjectPool<T> where T : class, IPoolItem
    {
        private readonly PlaceholderFactory<UnityEngine.Object, T> _poolItemFactory;
        private readonly int _maxSize;

        private readonly List<T> _poolItems;

        public MonoObjectPool(
            PlaceholderFactory<UnityEngine.Object, T> poolItemFactory,
            int maxSize = 0)
        {
            _poolItemFactory = poolItemFactory;
            _maxSize = maxSize;
            _poolItems = new List<T>();
        }

        public T GetItem(UnityEngine.Object prefab, Func<T, bool> functor = null)
        {
            functor = functor ?? (item => true);

            var freePoolItem = _poolItems
                .Where(item => item.IsFree)
                .FirstOrDefault(functor);

            return freePoolItem ?? CreatePoolItem(prefab);
        }

        private T CreatePoolItem(UnityEngine.Object prefab)
        {
            if (_maxSize > 0 && _poolItems.Count >= _maxSize)
            {
                return _poolItems.Last();
            }

            var poolItem = _poolItemFactory.Create(prefab);
            _poolItems.Add(poolItem);

            return poolItem;
        }

        // ToDo [OBJECTPOOL] Very hacky, find a better way
        public void ForEach(Action<T> action)
        {
            _poolItems.ForEach(action);
        }
    }
}
