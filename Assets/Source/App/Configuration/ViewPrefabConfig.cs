using UnityEngine;

namespace Assets.Source.App.Configuration
{
    [CreateAssetMenu(fileName = "ViewPrefabConfig", menuName = "Config/ViewPrefabConfig")]
    public class ViewPrefabConfig : ScriptableObject
    {        
        public GameObject ScreenFadeView;
    }
}
