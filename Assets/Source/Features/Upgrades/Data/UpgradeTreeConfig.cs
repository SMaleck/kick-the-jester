using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Features.Upgrades.Data
{
    [CreateAssetMenu(fileName = "UpgradeTreeConfig", menuName = "KTJ/Config/UpgradeTreeConfig")]
    public class UpgradeTreeConfig : ScriptableObject
    {
        [Serializable]
        public class UpgradePath
        {
            public UpgradePathType PathType;
            public List<UpgradeStep> Steps;

            public int MaxLevel => Steps.Count - 1;
        }

        [Serializable]
        public class UpgradeStep
        {
            public int Cost;
            public float Value;
        }

        [SerializeField] private List<UpgradePath> _upgradePaths;

        public UpgradePath GetUpgradePath(UpgradePathType pathType)
        {
            return _upgradePaths.First(e => e.PathType == pathType);
        }
    }
}
