namespace Assets.Source.Services.Savegames
{
    public interface ISavegamePersistenceService
    {
        void EnqueueSaveRequest();
        void Save();
        void Load();
    }
}
