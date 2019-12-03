using Assets.Source.App;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Features.Achievements.Data
{
    [Serializable]
    [CreateAssetMenu(fileName = nameof(AchievementConfig), menuName = Constants.UMenuRoot + nameof(AchievementConfig))]
    public class AchievementConfig : ScriptableObject, IAchievementData
    {
        [Serializable]
        private class AchievementItem
        {
            [SerializeField] private AchievementId _id;
            public AchievementId Id => _id;

            [SerializeField] private AchievementRequirementType _requirementType;
            public AchievementRequirementType RequirementType => _requirementType;

            [SerializeField] private double _requirement;
            public double Requirement => _requirement;
        }

        [SerializeField] private List<AchievementItem> _achievementItems;

        public double GetRequirement(AchievementId id)
        {
            return Find(id).Requirement;
        }

        public AchievementRequirementType GetRequirementType(AchievementId id)
        {
            return Find(id).RequirementType;
        }

        private AchievementItem Find(AchievementId id)
        {
            var achievementItem = _achievementItems
                .FirstOrDefault(item => item.Id == id);

            if (achievementItem == null)
            {
                throw new NullReferenceException($"No data found for AchievementId {id}");
            }

            return achievementItem;
        }
    }
}
