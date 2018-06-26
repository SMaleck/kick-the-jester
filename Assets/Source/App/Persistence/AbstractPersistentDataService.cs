using System.Linq;
using UniRx;

namespace Assets.Source.App.Persistence
{
    public abstract class AbstractPersistentDataContext
    {
        public AbstractPersistentDataContext(SceneTransitionService sceneTransitionService)
        {
            // Save on each Scene Load       
            sceneTransitionService.IsLoadingProperty
                                    .Where(e => e)
                                    .Subscribe(_ => Save());

            // Save on Application quit
            Observable.OnceApplicationQuit().Subscribe(__ => { Save(); });
        }

        
        protected abstract void Save();
    }
}
