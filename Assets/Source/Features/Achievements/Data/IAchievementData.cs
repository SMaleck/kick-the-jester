namespace Assets.Source.Features.Achievements.Data
{
    public interface IAchievementData
    {
        double GetRequirement(AchievementId id);
        AchievementRequirementType GetRequirementType(AchievementId id);
    }
}
