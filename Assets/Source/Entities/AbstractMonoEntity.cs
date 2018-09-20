using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Source.Entities
{
    public abstract class AbstractMonoEntity : MonoBehaviour, IInitializable
    {
        public Transform GoTransform => gameObject.transform;
        public BoolReactiveProperty IsPaused = new BoolReactiveProperty(false);

        public abstract void Initialize();
    }
}
