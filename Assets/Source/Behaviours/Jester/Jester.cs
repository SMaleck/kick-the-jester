using UnityEngine;

namespace Assets.Source.Behaviours.Jester
{    
    public class Jester : AbstractBodyBehaviour
    {
        public T GetBehaviour<T>()
        {
            return gameObject.GetComponent<T>();
        }
    }
}
