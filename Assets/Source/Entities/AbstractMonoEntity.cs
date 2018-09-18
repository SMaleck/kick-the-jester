using UniRx;
using UnityEngine;

namespace Assets.Source.Entities
{
    public class AbstractMonoEntity : MonoBehaviour
    {
        public Transform GoTransform => gameObject.transform;
        public BoolReactiveProperty IsPaused = new BoolReactiveProperty(false);        
    }
}
