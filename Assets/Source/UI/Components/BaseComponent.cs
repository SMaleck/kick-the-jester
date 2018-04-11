using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.UI.Components
{
    public class BaseComponent : MonoBehaviour
    {
        protected void RegisterWithManager()
        {
            Singletons.uiManager.Register(this);
        }
    }
}
