using UnityEngine;

namespace Assets.Source.Entities.Jester
{
    public class Jester : BaseEntity
    {
        public Transform GetTransform()
        {
            return gameObject.transform;
        }
    }
}
