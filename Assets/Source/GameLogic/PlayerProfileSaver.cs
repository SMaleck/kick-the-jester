using Assets.Source.Repositories;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    class PlayerProfileSaver: MonoBehaviour
    {
        private void Awake()
        {
            // Ensures that data is saved to disk before loading next scene
            //App.Cache.screenManager.OnStartLoading(PlayerPrefs.Save);
            App.Cache.screenManager.OnStartLoading(App.Cache.playerProfile.StoreProfile);
        }
    }
}
