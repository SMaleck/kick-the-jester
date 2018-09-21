using UnityEngine;

namespace Assets.Source.Util
{
    public interface IPrefabFactory<out T> where T : class
    {
        T CreateResource(GameObject prefab);
    }
}
