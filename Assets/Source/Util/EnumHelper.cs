using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Util
{
    public static class EnumHelper<T>
    {
        private static IEnumerable<T> _enumerable;

        public static IEnumerable<T> Iterator => _enumerable ?? (_enumerable = Enum.GetValues(typeof(T)).Cast<T>());

        public static int Length => Iterator.ToList().Count;

        public static IEnumerable<T> IteratorExcept(params T[] valuesToExclude)
        {
            return Iterator.Where(x => !valuesToExclude.Contains(x));
        }

        public static void ForEach(Action<T> action)
        {
            Iterator.ToList().ForEach(action);
        }
    }
}
