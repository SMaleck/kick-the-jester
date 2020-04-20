using Assets.Source.App;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Features.Achievements.Data
{
    [Serializable]
    [CreateAssetMenu(fileName = nameof(AchievementsIconConfig), menuName = Constants.UMenuRoot + nameof(AchievementsIconConfig))]
    public class AchievementsIconConfig : ScriptableObject
    {
        [Serializable]
        private class AchievementIcon
        {
            [SerializeField] private AchievementId _id;
            public AchievementId Id => _id;

            [SerializeField] private Sprite _iconSprite;
            public Sprite IconSprite => _iconSprite;
        }

        [SerializeField] private List<AchievementIcon> _achievementIcons;

        public Sprite GetIcon(AchievementId id)
        {
            return _achievementIcons.First(item => item.Id == id).IconSprite;
        }
    }
}
