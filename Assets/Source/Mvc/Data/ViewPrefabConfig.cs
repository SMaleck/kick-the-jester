using Assets.Source.Mvc.Views;
using UnityEngine;

namespace Assets.Source.Mvc.Data
{
    [CreateAssetMenu(fileName = "ViewPrefabConfig", menuName = "KTJ/Config/ViewPrefabConfig")]
    public class ViewPrefabConfig : ScriptableObject
    {
        [Header("Upgrades")]
        [SerializeField] private UpgradeScreenView _upgradeScreenViewPrefab;
        public UpgradeScreenView UpgradeScreenViewPrefab => _upgradeScreenViewPrefab;

        [SerializeField] private UpgradeItemView _upgradeItemViewPrefab;
        public UpgradeItemView UpgradeItemViewPrefab => _upgradeItemViewPrefab;
    }
}
