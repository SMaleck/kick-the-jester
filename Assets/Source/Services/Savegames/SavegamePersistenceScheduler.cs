using Assets.Source.Util;
using UniRx;
using Zenject;

namespace Assets.Source.Services.Savegames
{
    public class SavegamePersistenceScheduler : AbstractDisposable, IInitializable
    {
        private readonly ISavegamePersistenceService _savegamePersistenceService;
        private readonly SceneTransitionService _sceneTransitionService;

        public SavegamePersistenceScheduler(
            ISavegamePersistenceService savegamePersistenceService,
            SceneTransitionService sceneTransitionService)
        {
            _savegamePersistenceService = savegamePersistenceService;
            _sceneTransitionService = sceneTransitionService;
        }

        public void Initialize()
        {
            _savegamePersistenceService.Load();

            _sceneTransitionService.OnSceneLoadStarted
                .Subscribe(_ => _savegamePersistenceService.EnqueueSaveRequest())
                .AddTo(Disposer);

            Observable.OnceApplicationQuit()
                .Subscribe(_ => _savegamePersistenceService.Save())
                .AddTo(Disposer);
        }
    }
}
