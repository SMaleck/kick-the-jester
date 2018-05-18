using System.Linq;
using UniRx;

namespace Assets.Source.App.Storage
{
    public abstract class AbstractPersistentDataService
    {
        public AbstractPersistentDataService()
        {
            // Save on each Scene Load       
            Kernel.SceneTransitionService.IsLoadingProperty
                                         .Where(e => e)
                                         .Subscribe(_ => Save());

            // Save on Application quit
            Observable.OnceApplicationQuit().Subscribe(__ => { Save(); });
        }


        public abstract void Save();
    }
}
