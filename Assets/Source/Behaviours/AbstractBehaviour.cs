using Assets.Source.GameLogic;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours
{
    public abstract class AbstractBehaviour : MonoBehaviour
    {
        public Transform goTransform
        {
            get
            {
                return gameObject.transform;
            }
        }
    }
}
