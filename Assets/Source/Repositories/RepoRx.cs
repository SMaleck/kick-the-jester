using UnityEngine;

namespace Assets.Source.Repositories
{
    public class RepoRx : MonoBehaviour
    {
        private GameStateRepository _GameStateRepository;
        public GameStateRepository GameStateRepository
        {
            get
            {
                if(_GameStateRepository == null)
                {
                    _GameStateRepository = new GameStateRepository();
                }

                return _GameStateRepository;
            }
        }
    }
}
