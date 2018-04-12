using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.UI
{
    public class PauseButton : MonoBehaviour
    {
        public void PauseGame()
        {
            Singletons.userControl.PauseGame();
        }
    }
}
