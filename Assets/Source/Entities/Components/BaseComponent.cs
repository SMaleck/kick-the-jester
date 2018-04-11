using UnityEngine;

namespace Assets.Source.Entities.Components
{
    public class BaseComponent : MonoBehaviour
    {
        private BaseEntity _Entity;
        public BaseEntity Entity
        {
            get
            {
                if(_Entity == null)
                {
                    _Entity = gameObject.GetComponent<BaseEntity>();
                }

                return _Entity;
            }
        }
    }
}
