using Assets.Source.Services.Savegames.Models;

namespace Assets.Source.Services.Savegames
{
    public interface ISavegameService
    {
        Savegame Savegame { get; }
        void Reset();
    }
}
