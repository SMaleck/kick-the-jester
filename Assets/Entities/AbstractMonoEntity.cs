using UniRx;
using UnityEngine;

namespace Assets.Entities
{
    public class AbstractMonoEntity : MonoBehaviour
    {
        private BoolReactiveProperty IsPaused = new BoolReactiveProperty(false);

        public Transform GoTransform => gameObject.transform;
    }
}
