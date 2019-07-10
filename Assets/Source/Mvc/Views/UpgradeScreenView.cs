using UnityEngine;

namespace Assets.Source.Mvc.Views
{
    public class UpgradeScreenView : ClosableView
    {
        [SerializeField] private Transform _upgradeItemsLayoutParent;
        public Transform UpgradeItemsLayoutParent => _upgradeItemsLayoutParent;
    }
}
